using DDD_TradingEngine.MoneyDomain.Model;
using DDD_TradingEngine.UserDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.UserDomain.Repository
{
    public interface IUserRepository
    {
        User Add<TUser>(TUser user) where TUser : User;
        List<User> FindAll();
        List<UserBalance> GetUserBalance(int userId);
        bool? ExchangeMoney(ExchangeMoney exhangeMoney, int userId);
        bool? SendMoney(SendMoney sendMoney, int userId);
        bool? AddUserBalance(int userId, AddMoney money);
    }
}
