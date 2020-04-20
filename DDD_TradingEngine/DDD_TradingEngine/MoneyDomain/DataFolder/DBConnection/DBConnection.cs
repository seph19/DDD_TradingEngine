using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.DataFolder.DBConnection
{
    public static class DBConnection
    {
        public static string DbConnectionString { get; private set; }
        public static void ConnectionString(string connectionString)
        {
            DbConnectionString = connectionString;
        }
    }
}
