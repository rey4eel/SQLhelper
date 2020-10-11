using KYSQLhelper.Infrastructure.Service;
using KYSQLhelper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KYSQLhelper.Models
{
    class LogData : INotifyPropertyChanged
    {

        private string _status;
        private string _statusDetails;
        private int _progressBar = 0;


        public string Status
        {
            get { return _status; }
            set => Set(ref _status, value);
        }
        public string StatusDetails
        {
            get { return _statusDetails; }
            set => Set(ref _statusDetails, value);
        }
        public int ProgressBar
        {
            get { return _progressBar; }
            set => Set(ref _progressBar, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public LogData()
        {
            
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

        public void ConnectSuccess()
        {
            Status = "Connected";
            StatusDetails = string.Empty;
        }

        public void ConnectFail()
        {
            Status = "Error";
            StatusDetails = "Check the Credentials = Connection Failed";
        }

        public void ExportSuccess(string exportPath)
        {
            Status = "Done";
            StatusDetails = "File saved in:" + exportPath;
        }

        public void ExportFail()
        {
            Status = "Error";
            StatusDetails = "No data to export create table first";
        }

        public void QueryExecuteSuccess(string query)
        {
            Status = "Executed";
            StatusDetails = $"Execute query => {query}";
        }

        private void QueryExecuteFail(Exception ex)
        {
            StatusDetails = ex.Message;
        }

    }
}
