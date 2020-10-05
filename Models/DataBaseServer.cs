using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYSQLhelper.Models
{
    class DataBaseServer
    {
		private List<string> _databases;

		public List<string> DataBases
		{
			get { return _databases; }
			set { _databases = value; }
		}

		private List<string> _dbTablesName;

		public List<string> DbTablesName
		{
			get { return _dbTablesName; }
			set { _dbTablesName = value; }
		}

		private List<string> _tableColumnName;

		public List<string> TableColumnName
		{
			get { return _tableColumnName; }
			set { _tableColumnName = value; }
		}

	}
}
