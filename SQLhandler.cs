using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Data.SqlClient;
using System.Data;

namespace KYSQLhelper
{
   
    class SQLhandler
    {
        private static string IpAdress { get; set; }
        private static string UserName { get; set; }
        private static string Password { get; set; }
        private static string strConnectionString { get; set; }

        public SQLhandler(string ipAdress,string userName,string password)
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
        }

        public static bool CheckConnection()
        {
            bool flag = false;
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
                            flag = true;
                        }
                    }
                    catch
                    {
                        flag = false;
                    }
                    connection.Close();
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static DataTable ExecuteQuery(string query)
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

    }
}
