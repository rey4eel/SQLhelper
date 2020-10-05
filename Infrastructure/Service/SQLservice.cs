using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Data.SqlClient;
using System.Data;
using Nini.Ini;
using Nini.Config;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace KYSQLhelper.Infrastructure.Service
{
   
    class SQLService
    {

        private bool _IsConnected = false;

        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; }
        }


        private static string _ipAdress;

        public static string IpAdress
        {
            get { return _ipAdress; }
            set { _ipAdress = value;}
            
        }

        private static string _userName;

        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private static string _password;

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private static string strConnectionString { get; set; }

        public  SQLService(string ipAdress,string userName,string password)
        {

            if (ipAdress == string.Empty)
                throw new ArgumentNullException();

            if (userName == string.Empty)
                throw new ArgumentNullException();

            if (password == string.Empty)
                throw new ArgumentNullException();

            IpAdress = ipAdress;
            UserName = userName;
            Password = password;

            strConnectionString = string.Format("Server={0};user id={1};password={2};", IpAdress, UserName, Password);

            CheckConnection();
        }

        public void CheckConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnectionString))
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
                    catch
                    {
                        IsConnected = false;
                    }
                    connection.Close();
                }
            }
            catch
            {
                IsConnected = false;
            }           
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable RcvData = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnectionString))
                {
                    connection.Open();

                    SqlDataAdapter DataWriter = new SqlDataAdapter(query,strConnectionString);

                    DataWriter.Fill(RcvData);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return RcvData;
        }

        public static void GetCredentialsFromConfig()
        {
            string defaultConfigPath = @"C:\KohYoung\AOI\AOIGUISetup.ini";

            IConfigSource source = new IniConfigSource(defaultConfigPath);

            IpAdress = source.Configs["RESULT"].Get("DBServer");
            UserName = source.Configs["RESULT"].Get("DBID");
            Password = source.Configs["RESULT"].Get("DBPassword");
        }

    }
}
