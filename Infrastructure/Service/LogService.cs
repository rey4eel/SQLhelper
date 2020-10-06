using KYSQLhelper.ViewModels;
using KYSQLhelper.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace KYSQLhelper.Infrastructure.Service
{
    class LogService 
    {
       
        public static void UpdateSuccess(ref string StatusBar , ref string StatusDetails)
        {
            StatusBar = "Connected";
            StatusDetails = string.Empty;
        }

        public static void UpdateFail(ref string StatusBar, ref string StatusDetails)
        {
            StatusBar = "Error";
            StatusDetails = "Chcek the Credentials = Connection Failed";
        }
    }
}
