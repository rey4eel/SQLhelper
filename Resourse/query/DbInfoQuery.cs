﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYSQLhelper.Resourse.query
{
    class DbInfoQuery
    {
        /// <summary>
        /// This tool allowes only view data and not change it
        /// </summary>
        public enum NotAllowedQuery
        {
            DROP,
            DELETE,
            TRUNCATE,
            INSERT,
            UPDATE
        }

        public static string DbInfo { get; set; } = "SELECT [serverName] = @@SERVERNAME,[databaseName] =  instance_name,dataSizeInKB = SUM(CASE WHEN counter_name = 'Data File(s) Size (KB)' THEN cntr_value END),logSizeInKB = SUM(CASE WHEN counter_name = 'Log File(s) Size (KB)' THEN cntr_value END) FROM sys.dm_os_performance_counters WHERE counter_name IN ('Data File(s) Size (KB)', 'Log File(s) Size (KB)') AND instance_name<> '_Total' AND instance_name LIKE '%KY%' GROUP BY instance_name;";
    }
}
