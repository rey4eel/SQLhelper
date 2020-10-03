using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Input validation
//

namespace KYSQLhelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region windowLoaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //default value
            IpAdress.Text = "127.0.0.1";
            UserName.Text = "sa";
            Password.Text = "koh1234";

        }
        #endregion

        /// <summary>
        /// Connecting to SQl and check the Credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SqlConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            SQLhandler SqlConnection = new SQLhandler(IpAdress.Text, UserName.Text, Password.Text);

            if (SQLhandler.CheckConnection())
            {
                writeLog("Connected");
                SqlConnectBtn.Background = Brushes.Green;

                string query = "select name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb','KY_CodeLib');";

                DataTable DbNames = SQLhandler.ExecuteQuery(query);

                for (int i = 0; i < DbNames.Rows.Count; i++)
                {
                    ComboBoxDB.Items.Add(DbNames.Rows[i]["name"]);
                }
            }
            else
            {
                writeLog("Check the connection details");
                SqlConnectBtn.Background = Brushes.Gray;
                ComboBoxDB.Items.Clear();
            }
        }

        private void ComboBoxDB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query = string.Format("SELECT name FROM {0}.sys.tables WHERE name LIKE '%TB%'", ComboBoxDB.SelectedItem);
            DataTable TableNames = SQLhandler.ExecuteQuery(query);

            for (int i = 0; i < TableNames.Rows.Count; i++)
            {
                ComboBoxTable.Items.Add(TableNames.Rows[i]["name"]);
            }
        }

        private void ComboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query = string.Format("SELECT TOP 1 * FROM {0}.dbo.{1}", ComboBoxDB.SelectedItem, ComboBoxTable.SelectedItem);
            DataTable ColumnNames = SQLhandler.ExecuteQuery(query);

            for (int i = 0; i < ColumnNames.Columns.Count; i++)
            {
                ComboBoxSelectPrime.Items.Add(ColumnNames.Columns[i].ColumnName);
                ComboBoxSelectSecond.Items.Add(ColumnNames.Columns[i].ColumnName);
                ComboBoxSelectThird.Items.Add(ColumnNames.Columns[i].ColumnName);
                ComboBoxSelectWhere.Items.Add(ColumnNames.Columns[i].ColumnName);
                ComboBoxSort.Items.Add(ColumnNames.Columns[i].ColumnName);
            }
        }

        private void ExecuteQuery_Click(object sender, RoutedEventArgs e)
        {
            string selectType = string.Empty;
            string compareSign = string.Empty;
            string query = string.Empty;
            string compareInput = CompareInput.Text;


            switch (ComboBoxCompareType.Text)
            {
                case "EQUAL": compareSign = "="; break;
                case "LIKE": compareSign = " " + "LIKE"; compareInput = string.Format("%{0}%",compareInput); break;
            }

            if (ComboBoxSelectPrime.Text == "All")
                selectType = "*";
            else
                selectType = ComboBoxSelectPrime.SelectedItem.ToString();

            query = string.Format("SELECT {0} ", selectType);


            if (ComboBoxSelectSecond.SelectedItem != null)
                query += string.Format(",{0}", ComboBoxSelectSecond.SelectedItem);

            if (ComboBoxSelectThird.SelectedItem != null)
                query += string.Format(",{0}", ComboBoxSelectThird.SelectedItem);

            query += string.Format(" FROM {1}.dbo.{2} ",selectType,ComboBoxDB.SelectedItem,ComboBoxTable.SelectedItem);


            if(ComboBoxSelectWhere.SelectedItem != null)
                query += string.Format(" WHERE {0}{1}'{2}' ",ComboBoxSelectWhere.SelectedItem,compareSign,compareInput);

            if(ComboBoxSort.SelectedItem != null)
                query += string.Format("ORDER BY {0} ",ComboBoxSort.SelectedItem.ToString());

            if(!string.IsNullOrEmpty(ComboBoxOrder.Text))
                query += " " + ComboBoxOrder.Text;

            writeLog($" Current query will be executed{query}");

            DataTable GridData = SQLhandler.ExecuteQuery(query);

            dataGrid.ItemsSource = GridData.DefaultView;
        }

        private void ClearTable_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
        }

        public void writeLog(string message)
        {
            LogBox.Text += Environment.NewLine + message;
        }


    }
}
