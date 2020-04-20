using DDD_TradingEngine.MoneyDomain.Model;
using DDD_TradingEngine.UserDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.DataFolder
{
    public interface IUserDataManager
    {
        User GetUserBalances(int userId);
        User GetUserInfo(int userId);
        Currency GetCurrency(int currencyId);
        bool UpdateUserBalance(User user);
        User GetUserBalance(int userId, int currencyId);
    }
}
