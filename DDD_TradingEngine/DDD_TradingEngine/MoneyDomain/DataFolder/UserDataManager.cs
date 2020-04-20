using DDD_TradingEngine.MoneyDomain.DataFolder.DataModel;
using DDD_TradingEngine.UserDomain.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DDD_TradingEngine.MoneyDomain.Model;

namespace DDD_TradingEngine.MoneyDomain.DataFolder
{
    public class UserDataManager: IUserDataManager
    {
        public User GetUserBalances(int userId)
        {
            string query = @"SELECT U.UserId,U.UserName,b.Amount,C.CurrencyId,C.Name Currency,C.Ratio
                            FROM dbo.[User] U
                            INNER JOIN dbo.[Balance] b ON b.UserId = U.UserId
                            INNER JOIN dbo.[Currency] c ON c.CurrencyId = b.CurrencyId
                            WHERE U.UserId = @UserId";

            var userBalance = new List<DataModel.UserBalance>();
            
            using (var connection = CreateConnection())
            {
                userBalance = connection.Query<DataModel.UserBalance>(query, new { UserId = userId }).ToList();
            }
            var user = GetUserInfo(userId);

            var balance = new Balance();
            if (userBalance != null && userBalance.Count() > 0)
            {
                foreach (var item in userBalance)
                {
                    balance.AddMoneyBalance(
                        new Money(new Currency(item.CurrencyId,item.Currency, item.Ratio), item.Amount));
                }
            }

            return new User(user.UserId,user.Username,balance);

        }
        public User GetUserBalance(int userId,int currencyId)
        {
            string query = @"SELECT U.UserId,U.UserName,b.Amount,C.CurrencyId,C.Name Currency,C.Ratio
                            FROM dbo.[User] U
                            INNER JOIN dbo.[Balance] b ON b.UserId = U.UserId
                            INNER JOIN dbo.[Currency] c ON c.CurrencyId = b.CurrencyId
                            WHERE U.UserId = @UserId and b.CurrencyId = @CurrencyId";

            var userBalance = new List<DataModel.UserBalance>();

            using (var connection = CreateConnection())
            {
                userBalance = connection.Query<DataModel.UserBalance>(query, new { UserId = userId, CurrencyId = currencyId }).ToList();
            }
            var user = GetUserInfo(userId);

            var balance = new Balance();
            if (userBalance != null && userBalance.Count() > 0)
            {
                foreach (var item in userBalance)
                {
                    balance.AddMoney(
                        new Money(new Currency(item.CurrencyId, item.Currency, item.Ratio), item.Amount));
                }
            }

            return new User(user.UserId, user.Username, balance);

        }
        public User GetUserInfo(int userId)
        {
            string query = @"SELECT UserId,UserName
                            FROM dbo.[User]
                            WHERE UserId = @UserId";

            var userInfo = new User();
            var connection = CreateConnection();

            if (connection != null && connection.State == ConnectionState.Open)
            {
                userInfo = connection.Query<User>(query, new { UserId = userId }).FirstOrDefault();
            }

            return userInfo;
        }

        protected IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(DataFolder.DBConnection.DBConnection.DbConnectionString);
            conn.Open();
            return conn;
        }

        public Currency GetCurrency(int currencyId)
        {
            string query = @"SELECT CurrencyId,Name, Ratio
                            FROM dbo.[Currency]
                            WHERE CurrencyId = @CurrencyId";

            var currency = new Currency();
            
            using (var connection = CreateConnection())
            {
                currency = connection.Query<Currency>(query, new { CurrencyId = currencyId }).FirstOrDefault();
            }

            return currency;
        }
        public bool UpdateUserBalance(User user)
        {
            var balance = user.Balance.GetAllMoney();

            foreach (var item in balance)
            {
                var currencyExist = CheckCurrency(user.UserId, item.Currency.CurrencyId);

                if (currencyExist)
                {
                    var update = $"UPDATE [Balance] SET Amount = @amount WHERE UserId = @userId AND CurrencyId = @currencyId";
                    using (var connection = CreateConnection())
                    {
                        connection.Execute(update, new { amount = item.Amount, userId = user.UserId, currencyId = item.Currency.CurrencyId });
                    }

                }
                else
                {
                    var insert = $"INSERT INTO [Balance] (UserId,CurrencyId,Amount) Values (@userId,@currencyId,@amount)";
                    using (var connection = CreateConnection())
                    {
                        connection.Execute(insert, new { userId = user.UserId, currencyId = item.Currency.CurrencyId, amount = item.Amount });
                    }
                }

            }
            return true;

        }
        private bool CheckCurrency(int userId, int CurrencyId)
        {
            string sql = @"SELECT 1
                            FROM dbo.Balance b
                            INNER JOIN dbo.Currency c ON b.CurrencyId = c.CurrencyId
                            WHERE b.UserId = @userId AND c.CurrencyId = @currencyId";

            using (var connection = CreateConnection())
            {
                var result = connection.Query<int>(sql, new { userId, CurrencyId }).FirstOrDefault();

                return result > 0 ? true : false;
            }
        }
           
    }
}
