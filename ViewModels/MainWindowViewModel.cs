using KYSQLhelper.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
