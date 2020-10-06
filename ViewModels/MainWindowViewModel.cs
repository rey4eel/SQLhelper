using KYSQLhelper.Infrastructure.Commands;
using KYSQLhelper.Infrastructure.Service;
using KYSQLhelper.Models;
using KYSQLhelper.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace KYSQLhelper.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public SQLService SqlConnection { get; set; }

        #region Tiltle

        /// <summary>
        /// WindowTitle
        /// </summary>
        private string _Title = "SQLhelper";

        public string Title
		{
			get => _Title;
			set => Set(ref _Title, value);
		}
        #endregion

        #region CompareInput

        /// <summary>
        /// WindowTitle
        /// </summary>
        private string _compareInput;

        public string CompareInput
        {
            get => _compareInput;
            set => Set(ref _compareInput, value);
        }
        #endregion

        #region Ipadress
        private string _ipAdress;

        public string IpAdress
        {
            get { return _ipAdress; }
            set => Set(ref _ipAdress, value);
        }
        #endregion

        #region User Name
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set => Set(ref _userName, value);
        }
        #endregion

        #region Password
        private string _password;

        public string Password
        {
            get { return _password; }
            set => Set(ref _password, value);
        }
        #endregion

        #region BtnColor
        private Brush _ConnectionBtnColor = Brushes.Gray;

        public Brush ConnectionBtnColor 
        {
            get { return _ConnectionBtnColor; }
            set => Set(ref _ConnectionBtnColor, value);
        }
        #endregion

        #region data bases from the server
        private ObservableCollection<string> _dbNames = new ObservableCollection<string>();

        public ObservableCollection<string> DbNames
        {
            get { return _dbNames; }
            set => Set(ref _dbNames, value);
        }

        #endregion

        #region Tables from the DB
        private ObservableCollection<string> _tableNames = new ObservableCollection<string>();

        public ObservableCollection<string> TableNames
        {
            get { return _tableNames; }
            set => Set(ref _tableNames, value);
        }

        #endregion

        #region Column name from table
        private ObservableCollection<string> _columnName = new ObservableCollection<string>();

        public ObservableCollection<string> ColumnName
        {
            get { return _columnName; }
            set => Set(ref _columnName, value);
        }

        private ObservableCollection<string> _compareType = new ObservableCollection<string>() {"LIKE","BETWEEN","EQUAL" };

        public ObservableCollection<string> CompareType
        {
            get { return _compareType; }
            set => Set(ref _compareType, value);
        }

        private ObservableCollection<string> _orderBy = new ObservableCollection<string>() { "ASC", "DESC"};

        public ObservableCollection<string> OrderBy
        {
            get { return _orderBy; }
            set => Set(ref _orderBy, value);
        }

        private DataTable _queryData = new DataTable();

        public DataTable QueryData
        {
            get { return _queryData; }
            set => Set(ref _queryData, value);
        }

        #endregion

        #region ComBoxFromSelectedvalue
        private string _fromSelected;

        public string FromSelected
        {
            get { return _fromSelected; }
            set => Set(ref _fromSelected, value);
        }

        #endregion

        #region ComBoxFromTableSelectedvalue
        private string _fromTableSelected;

        public string FromTableSelected
        {
            get { return _fromTableSelected; }
            set => Set(ref _fromTableSelected, value);
        }

        private string _fromColumnSelectedPrime;

        public string FromColumnSelectedPrime
        {
            get { return _fromColumnSelectedPrime; }
            set => Set(ref _fromColumnSelectedPrime, value);
        }

        private string _fromColumnSelectedSecond;

        public string FromColumnSelectedSecond
        {
            get { return _fromColumnSelectedSecond; }
            set => Set(ref _fromColumnSelectedSecond, value);
        }

        private string _fromColumnSelectedThird;

        public string FromColumnSelectedThird
        {
            get { return _fromColumnSelectedThird; }
            set => Set(ref _fromColumnSelectedThird, value);
        }

        private string _compareTypeSelected;

        public string CompareTypeSelected
        {
            get { return _compareTypeSelected; }
            set => Set(ref _compareTypeSelected, value);
        }

        private string _fromWhereSelected;

        public string FromWhereSelected
        {
            get { return _fromWhereSelected; }
            set => Set(ref _fromWhereSelected, value);
        }

        private string _sortBySelected;

        public string SortBySelected
        {
            get { return _sortBySelected; }
            set => Set(ref _sortBySelected, value);
        }

        private string _orderBySelected;

        public string OrderBySelected
        {
            get { return _orderBySelected; }
            set => Set(ref _orderBySelected, value);
        }

        #endregion

        #region Commands

        #region Get setting from KYconfig
        public ICommand SettingPreLoadCommand { get; }

        private bool CanSettingPreLoadCommandExecute(object p) => true;

        private void OnSettingPreLoadCommandExecuted(object p)
        {
            SQLService.GetCredentialsFromConfig();
            IpAdress = SQLService.IpAdress;
            UserName = SQLService.UserName;
            Password = SQLService.Password;
        }
        #endregion

        #region SQLconnect
        public ICommand SqlConnectCommand { get; }

        private bool CanSqlConnectCommandExecute(object p)
        {
            return true;
        }

        private void OnSqlConnectCommandExecute(object p)
        {
            if(DbNames.Count > 0)
                DbNames.Clear();

            SqlConnection = new SQLService(IpAdress, UserName, Password);

            if (SqlConnection.IsConnected)
            {
                ConnectionBtnColor = Brushes.Green;

                string query = "select name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb','KY_CodeLib');";

                DataTable SqlResponce = SqlConnection.ExecuteQuery(query);

                for (int i = 0; i < SqlResponce.Rows.Count; i++)
                {
                    DbNames.Add(SqlResponce.Rows[i]["name"].ToString());
                }
            }
            else
            {
                ConnectionBtnColor = Brushes.Gray;
                if (DbNames.Count > 0)
                {
                    DbNames.Clear();
                    FromSelected = string.Empty;
                }
                    
            }

        }

        #endregion

        #region DataBaseChangedCommand
        public ICommand DataBaseChangedCommand { get; }

        private bool CanDataBaseChangedCommandExecute(object p)
        {
            return true;
        }

        private void OnDataBaseChangedCommandExecute(object p)
        {
            if(TableNames.Count >0)
                TableNames.Clear();

            string query = string.Format("SELECT name FROM {0}.sys.tables WHERE name LIKE '%TB%'", FromSelected);
            DataTable SqlResponce = SqlConnection.ExecuteQuery(query);

            for (int i = 0; i < SqlResponce.Rows.Count; i++)
            {
                TableNames.Add(SqlResponce.Rows[i]["name"].ToString());
            }
        }

        #endregion

        #region TableChangedCommand
        public ICommand TableChangedCommand { get; }

        private bool CanTableChangedCommandExecute(object p)
        {
            return true;
        }

        private void OnTableChangedCommandExecute(object p)
        {
            if (ColumnName.Count > 0)
                ColumnName.Clear();

            string query = string.Format("SELECT TOP 1 * FROM {0}.dbo.{1}", FromSelected, FromTableSelected);
            DataTable SqlResponce = SqlConnection.ExecuteQuery(query);

            for (int i = 0; i < SqlResponce.Columns.Count; i++)
            {
                ColumnName.Add(SqlResponce.Columns[i].ColumnName);
            }
        }

        #endregion


        #region ExecuteQueryCommand
        public ICommand ExecuteQueryCommand { get; }

        private bool CanExecuteQueryCommandExecute(object p)
        {
            return true;
        }

        private void OnExecuteQueryCommandExecute(object p)
        {

            string selectType = string.Empty;
            string compareSign = string.Empty;
            string query = string.Empty;
            string compareInput = CompareInput;


            switch (CompareTypeSelected)
            {
                case "EQUAL": compareSign = "="; break;
                case "LIKE": compareSign = " " + "LIKE"; compareInput = string.Format("%{0}%", compareInput); break;
            }

            query = string.Format("SELECT {0} ", FromColumnSelectedPrime);


            if (!string.IsNullOrWhiteSpace(FromColumnSelectedSecond))
                query += string.Format(",{0}", FromColumnSelectedSecond);

            if (!string.IsNullOrWhiteSpace(FromColumnSelectedThird))
                query += string.Format(",{0}", FromColumnSelectedThird);


            query += string.Format(" FROM {1}.dbo.{2} ", selectType, FromSelected, FromTableSelected);


            if (!string.IsNullOrWhiteSpace(FromWhereSelected))
                query += string.Format(" WHERE {0}{1}'{2}' ", FromWhereSelected, compareSign, compareInput);

            if (!string.IsNullOrWhiteSpace(SortBySelected))
                query += string.Format("ORDER BY {0} ", SortBySelected);

            if (!string.IsNullOrWhiteSpace(OrderBySelected))
                query += " " + OrderBySelected;


            Debug.Print($" Current query will be executed -> {query}");

            QueryData = SqlConnection.ExecuteQuery(query);
            //QueryData = SqlConnection.ExecuteQuery("SELECT TOP 1 * FROM KY_AOI.dbo.TB_AOIPCB ORDER BY StartDateTime DESC");
            

        }

        #endregion


        #region GetLastResultCommand
        public ICommand GetLastResultCommand { get; }

        private bool CanGetLastResultCommandExecute(object p)
        {
            return true;
        }

        private void OnGetLastResultCommandExecute(object p)
        {

            if (QueryData.Rows.Count > 0)
                QueryData.Clear();

            QueryData = SqlConnection.ExecuteQuery("SELECT TOP 1 * FROM KY_AOI.dbo.TB_AOIPCB ORDER BY StartDateTime DESC");
        }

        #endregion

        #region SaveCsvFileCommand
        public ICommand SaveCsvFileCommand { get; }

        private bool CanSaveCsvFileCommandExecute(object p)
        {
            return true;
        }

        private void OnSaveCsvFileCommandExecute(object p)
        {

            Debug.Print("called");
                    StringBuilder sb = new StringBuilder();

                    IEnumerable<string> columnNames = QueryData.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName);

                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in QueryData.Rows)
                    {
                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                        sb.AppendLine(string.Join(",", fields));
                    }

            string path = @"C:\Users\vardan.saakian\Desktop\TextData.txt";

            File.WriteAllText(path, sb.ToString());

        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            

            SettingPreLoadCommand = new LambdaCommand(OnSettingPreLoadCommandExecuted, CanSettingPreLoadCommandExecute);
            SqlConnectCommand = new LambdaCommand(OnSqlConnectCommandExecute, CanSqlConnectCommandExecute);
            DataBaseChangedCommand = new LambdaCommand(OnDataBaseChangedCommandExecute, CanDataBaseChangedCommandExecute);
            TableChangedCommand = new LambdaCommand(OnTableChangedCommandExecute, CanTableChangedCommandExecute);
            ExecuteQueryCommand = new LambdaCommand(OnExecuteQueryCommandExecute, CanExecuteQueryCommandExecute);
            GetLastResultCommand = new LambdaCommand(OnGetLastResultCommandExecute, CanExecuteQueryCommandExecute);
            SaveCsvFileCommand = new LambdaCommand(OnSaveCsvFileCommandExecute, CanSaveCsvFileCommandExecute);
        }

    }
}
