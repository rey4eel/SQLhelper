using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Nini.Config;
using System.Diagnostics;

namespace KYSQLhelper.Models 
{
    public delegate void ExceptionDelegate(string exception);

    class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
    class DataBaseModel : INotifyPropertyChanged
    {

        private ObservableCollection<string> _dbNames = new ObservableCollection<string>();
        private ObservableCollection<string> _tableNames = new ObservableCollection<string>();
        private ObservableCollection<string> _columnNamePrime = new ObservableCollection<string>() {"ALL"};
        private ObservableCollection<string> _compareType = new ObservableCollection<string>() {"LIKE","BETWEEN","EQUAL"};
        private ObservableCollection<string> _columnName = new ObservableCollection<string>();
        private ObservableCollection<string> _orderBy = new ObservableCollection<string>() {"ASC","DESC"};
        private ObservableCollection<string> _exportType = new ObservableCollection<string>() {"PCBGUID","BARCODE","RECENT"};
        private bool _IsConnected = false;
        private string _connectionString;
        private string _ipAdress;
        private string _userName;
        private string _password;

        public string IpAdress
        {
            get { return _ipAdress; }
            set => Set(ref _ipAdress, value);
        }
        public string UserName
        {
            get { return _userName; }
            set => Set(ref _userName, value);
        }
        public string Password
        {
            get { return _password; }
            set => Set(ref _password, value);
        }
        public ObservableCollection<string> DbNames
        {
            get { return _dbNames; }
            set => Set(ref _dbNames, value);
        }
        public ObservableCollection<string> TableNames
        {
            get { return _tableNames; }
            set => Set(ref _tableNames, value);
        }
        public ObservableCollection<string> ColumnNamePrime
        {
            get { return _columnNamePrime; }
            set => Set(ref _columnNamePrime, value);
        }
        public ObservableCollection<string> ColumnName
        {
            get { return _columnName; }
            set => Set(ref _columnName, value);
        }
        public ObservableCollection<string> CompareType
        {
            get { return _compareType; }
            set => Set(ref _compareType, value);
        }
        public ObservableCollection<string> OrderBy
        {
            get { return _orderBy; }
            set => Set(ref _orderBy, value);
        }
        public ObservableCollection<string> ExportType
        {
            get { return _exportType; }
            set => Set(ref _exportType, value);
        }
        public string ConnectionString
        {
            get { return _connectionString; }
            set {_connectionString = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        public delegate void SqlExceptionEventHandler(object source, ExceptionEventArgs args);
        public event SqlExceptionEventHandler ExceptionCatched;

        public DataBaseModel()
        {
            GetCredentialsFromConfig();
        }

        public void CheckConnection()
        {
            try
            {
                ConnectionString = string.Format("Server={0};user id={1};password={2};", IpAdress, UserName, Password);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand("SELECT 1", connection))
                        {
                            sqlCommand.ExecuteNonQuery();
                            IsConnected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        OnExceptionCatched(ex);
                        IsConnected = false;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                OnExceptionCatched(ex);
                IsConnected = false;
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable RcvData = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlDataAdapter DataWriter = new SqlDataAdapter(query, ConnectionString);

                    DataWriter.Fill(RcvData);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                OnExceptionCatched(ex);
            }
            return RcvData;
        }

        public void GetCredentialsFromConfig()
        {
            try
            {
                string defaultConfigPath = @"C:\KohYoung\AOI\AOIGUISetup.ini";

                IConfigSource source = new IniConfigSource(defaultConfigPath);

                IpAdress = source.Configs["RESULT"].Get("DBServer");
                UserName = source.Configs["RESULT"].Get("DBID");
                Password = source.Configs["RESULT"].Get("DBPassword");
 
            }
            catch (Exception ex)
            {

            }

        }

        protected virtual void OnExceptionCatched(Exception ex)
        {
            ExceptionCatched?.Invoke(this, new ExceptionEventArgs() { Exception = ex });
        }

    }
}
