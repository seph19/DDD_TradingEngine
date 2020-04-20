using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.DataFolder.DBConnection
{
    public static class DapperConnection
    {
        public static IServiceCollection InitializeDapperConnectionString(this IServiceCollection services, string connectionString)
        {
            DBConnection.ConnectionString(connectionString);

            return services;
        }
    }
}
