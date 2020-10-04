using KYSQLhelper.Infrastructure.Commands;
using KYSQLhelper.ViewModels.Base;
using System.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace KYSQLhelper.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
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

        #region Commands

        #region Get setting from KYconfig
        public ICommand SettingPreLoadCommand { get; }

        private bool CanSettingPreLoadCommandExecute(object p) => true;

        private void OnSettingPreLoadCommandExecuted(object p)
        {
            SQLhandler.GetCredentialsFromConfig();
            IpAdress = SQLhandler.IpAdress;
            UserName = SQLhandler.UserName;
            Password = SQLhandler.Password;
        } 
        #endregion





        public ICommand SqlConnectCommand { get; }

        private bool CanSqlConnectCommandExecute(object p)
        {
            return true;
        }

        private void OnSqlConnectCommandExecute(object p)
        {
            SQLhandler SqlConnection = new SQLhandler(IpAdress, UserName, Password);

            if (SQLhandler.CheckConnection())
            {
                //writeLog("Connected");
                ConnectionBtnColor = Brushes.Green;

                string query = "select name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb','KY_CodeLib');";

                DataTable DbNames = SQLhandler.ExecuteQuery(query);

                //for (int i = 0; i < DbNames.Rows.Count; i++)
                //{
                //    ComboBoxDB.Items.Add(DbNames.Rows[i]["name"]);
                //}
            }
            else
            {
                //writeLog("Check the connection details");
                //SqlConnectBtn.Background = Brushes.Gray;
                //ComboBoxDB.Items.Clear();
            }

        }

        #endregion

        public MainWindowViewModel()
        {
            SqlConnectCommand = new LambdaCommand(OnSqlConnectCommandExecute, CanSqlConnectCommandExecute);
            SettingPreLoadCommand = new LambdaCommand(OnSettingPreLoadCommandExecuted, CanSettingPreLoadCommandExecute);

        }

    }
}
