using KYSQLhelper.Infrastructure.Commands;
using KYSQLhelper.Infrastructure.Service;
using KYSQLhelper.Models;
using KYSQLhelper.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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
        private List<string> _dbNames = new List<string>();

        public List<string> DbNames
        {
            get { return _dbNames; }
            set => Set(ref _dbNames, value);
        }

        #endregion

        #region Tables from the DB
        private List<string> _tableNames = new List<string>();

        public List<string> TableNames
        {
            get { return _tableNames; }
            set => Set(ref _tableNames, value);
        }

        #endregion

        #region Column name from table
        private List<string> _columnName = new List<string>();

        public List<string> ColumnName
        {
            get { return _columnName; }
            set => Set(ref _columnName, value);
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
            string query = string.Format("SELECT TOP 1 * FROM {0}.dbo.{1}", FromSelected, FromTableSelected);
            DataTable SqlResponce = SqlConnection.ExecuteQuery(query);

            for (int i = 0; i < SqlResponce.Columns.Count; i++)
            {
                ColumnName.Add(SqlResponce.Columns[i].ColumnName);
            }
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            

            SettingPreLoadCommand = new LambdaCommand(OnSettingPreLoadCommandExecuted, CanSettingPreLoadCommandExecute);
            SqlConnectCommand = new LambdaCommand(OnSqlConnectCommandExecute, CanSqlConnectCommandExecute);
            DataBaseChangedCommand = new LambdaCommand(OnDataBaseChangedCommandExecute, CanDataBaseChangedCommandExecute);
            TableChangedCommand = new LambdaCommand(OnTableChangedCommandExecute, CanTableChangedCommandExecute);
        }

    }
}
