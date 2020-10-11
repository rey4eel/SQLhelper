using KYSQLhelper.Infrastructure.Commands;
using KYSQLhelper.Infrastructure.Service;
using KYSQLhelper.Models;
using KYSQLhelper.ViewModels.Base;
using System;
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
        private DataBaseModel _dataBaseModel = new DataBaseModel();
        private LogData _log = new LogData();
        private DataTable _queryData = new DataTable();
        private string _Title = "SQLhelper";
        private string _connectionStatus = "Offline";
        private string _compareInput;
        private string _betweenInput;
        private Brush _ConnectionBtnColor = Brushes.Gray;
        private bool _secondSelectedIsAvalible = true;
        private string _manualQueryInput;


        public DataBaseModel DataBaseModel
        {
            get { return _dataBaseModel; }
            set => Set(ref _dataBaseModel, value);
        }
        public LogData log
        {
            get { return _log; }
            set => Set(ref _log, value);
        }
        public DataTable QueryData
        {
            get { return _queryData; }
            set => Set(ref _queryData, value);
        }
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => Set(ref _connectionStatus, value);
        }
        public string CompareInput
        {
            get => _compareInput;
            set => Set(ref _compareInput, value);
        }
        public string BetweenInput
        {
            get => _betweenInput;
            set => Set(ref _betweenInput, value);
        }
        public Brush ConnectionBtnColor
        {
            get { return _ConnectionBtnColor; }
            set => Set(ref _ConnectionBtnColor, value);
        }
        public bool SecondSelectedIsAvalible
        {
            get { return _secondSelectedIsAvalible; }
            set => Set(ref _secondSelectedIsAvalible, value);
        }
        public string ManualQueryInput
        {
            get { return _manualQueryInput; }
            set => Set(ref _manualQueryInput, value);
        }

        #region ComBoxFromTableSelectedvalue

        private string _fromSelected;

        public string FromSelected
        {
            get { return _fromSelected; }
            set => Set(ref _fromSelected, value);
        }

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
            if (DataBaseModel.DbNames.Count > 0)
                DataBaseModel.DbNames.Clear();

            DataBaseModel.CheckConnection();

            if (DataBaseModel.IsConnected)
            {
                ConnectionStatus = "Online";
                ConnectionBtnColor = Brushes.Green;
                log.ConnectSuccess();

                string query = "select name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb','KY_CodeLib');";

                DataTable SqlResponce = DataBaseModel.ExecuteQuery(query);

                for (int i = 0; i < SqlResponce.Rows.Count; i++)
                {
                    DataBaseModel.DbNames.Add(SqlResponce.Rows[i]["name"].ToString());
                }
            }
            else
            {
                log.ConnectFail();
                ConnectionBtnColor = Brushes.Gray;
                if (DataBaseModel.DbNames.Count > 0)
                {
                    DataBaseModel.DbNames.Clear();
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
            if (DataBaseModel.TableNames.Count > 0)
                DataBaseModel.TableNames.Clear();

            string query = string.Format("SELECT name FROM {0}.sys.tables WHERE name LIKE '%TB%'", FromSelected);

            DataTable SqlResponce = DataBaseModel.ExecuteQuery(query);
       
            for (int i = 0; i < SqlResponce.Rows.Count; i++)
            {
                DataBaseModel.TableNames.Add(SqlResponce.Rows[i]["name"].ToString());
            }

            log.QueryExecuteSuccess(query);
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
            if (DataBaseModel.ColumnName.Count > 0)
                DataBaseModel.ColumnName.Clear();

            string query = string.Format("SELECT TOP 1 * FROM {0}.dbo.{1}", FromSelected, FromTableSelected);

            DataTable SqlResponce = DataBaseModel.ExecuteQuery(query);

            for (int i = 0; i < SqlResponce.Columns.Count; i++)
            {
                DataBaseModel.ColumnNamePrime.Add(SqlResponce.Columns[i].ColumnName);
                DataBaseModel.ColumnName.Add(SqlResponce.Columns[i].ColumnName);
            }

            log.QueryExecuteSuccess(query);
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

            string selectType = FromColumnSelectedPrime;
            string compareSign = string.Empty;
            string query = string.Empty;
            string compareInput = CompareInput;

            if (string.IsNullOrEmpty(selectType) || selectType == "ALL")
                selectType = "*";
             

            switch (CompareTypeSelected)
            {
                case "EQUAL": compareSign = "="; break;
                case "LIKE": compareSign = " " + "LIKE"; compareInput = string.Format("%{0}%", compareInput); break;
                case "BETWEEN": compareSign = " " + "BETWEEN" + " ";compareInput = string.Format("{0}\' AND \'{1}",CompareInput,BetweenInput); break;
            }

            query = string.Format("SELECT {0} ", selectType);


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

            
            QueryData = DataBaseModel.ExecuteQuery(query);

            log.QueryExecuteSuccess(query);
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

            string lastDataQuery = "SELECT TOP 1 * FROM KY_AOI.dbo.TB_AOIPCB ORDER BY StartDateTime DESC";

            QueryData = DataBaseModel.ExecuteQuery(lastDataQuery);

            log.QueryExecuteSuccess(lastDataQuery);
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

            if (QueryData.Rows.Count == 0)
            {
                log.ExportFail();
                return;
            }

            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = QueryData.Columns.Cast<DataColumn>().
                                            Select(column => column.ColumnName);

            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in QueryData.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            string desctopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = string.Format("{0}\\sqlData.csv",desctopPath);

            File.WriteAllText(path, sb.ToString());

            log.ExportSuccess(path);

        }
        #endregion

        #region ClearComboBoxSelected
        public ICommand ClearComboBoxSelectedCommnad { get; }
        private bool CanClearComboBoxSelectedCommnadExecute(object p)
        {
            return true;
        }
        private void OnClearComboBoxSelectedCommnadExecute(object p)
        {

        }
        #endregion

        #region ExecuteQueryManualCommand
        public ICommand ExecuteQueryManualCommand { get; }
        private bool CanExecuteQueryManualCommandExecute(object p)
        {
            return true;
        }
        private void OnExecuteQueryManualCommandExecute(object p)
        {
            if (QueryData.Rows.Count > 0)
                QueryData.Clear();

            QueryData = DataBaseModel.ExecuteQuery(ManualQueryInput);

            log.QueryExecuteSuccess(ManualQueryInput);
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
            ClearComboBoxSelectedCommnad = new LambdaCommand(OnClearComboBoxSelectedCommnadExecute, CanClearComboBoxSelectedCommnadExecute);
            ExecuteQueryManualCommand = new LambdaCommand(OnExecuteQueryManualCommandExecute, CanExecuteQueryManualCommandExecute);
        }

    }
}
