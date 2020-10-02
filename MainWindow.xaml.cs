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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //default value
            IpAdress.Text = "127.0.0.1";
            UserName.Text = "sa";
            Password.Text = "koh1234";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLhandler SqlConnection = new SQLhandler(IpAdress.Text, UserName.Text, Password.Text);

            if (SQLhandler.CheckConnection())
            {
                MessageBox.Show("Connected");
                string query = "select name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb','KY_CodeLib');";

                DataTable DbNames = SQLhandler.ExecuteQuery(query);

                for (int i = 0; i < DbNames.Rows.Count; i++)
                {
                    ComboBoxDB.Items.Add(DbNames.Rows[i]["name"]);
                }
            }
            else
            {
                MessageBox.Show("Check the connection details");
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

            switch (ComboBoxCompareType.Text)
            {
                case "EQUAL": compareSign = "="; break;
                case "LIKE": compareSign = "LIKE"; break;
            }

            if (ComboBoxSelectPrime.Text == "ALL")
            {
                selectType = "*";
            }
            else
            {
                selectType = ComboBoxSelectPrime.SelectedItem.ToString();
            }
                
            
            string query = string.Format("SELECT {0} FROM {1}.dbo.{2} WHERE {3}{4}'{5}' ORDER BY {6} {7}",
                                            "*",
                                            ComboBoxDB.SelectedItem,
                                            ComboBoxTable.SelectedItem,
                                            ComboBoxSelectWhere.SelectedItem,
                                            compareSign,
                                            CompareInput.Text,
                                            ComboBoxSort.SelectedItem.ToString(),
                                            ComboBoxOrder.Text
                                            );
            LogBox.Text = query;

            DataTable GridData = SQLhandler.ExecuteQuery(query);

            dataGrid.ItemsSource = GridData.DefaultView;
        }
    }
}
