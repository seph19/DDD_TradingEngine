using DDD_TradingEngine.MoneyDomain;
using DDD_TradingEngine.MoneyDomain.Model;
using DDD_TradingEngine.MoneyDomain.Repository;
using DDD_TradingEngine.MoneyDomain.DataFolder;
using DDD_TradingEngine.UserDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.UserDomain.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ICurrencyRepository  _currencyRepository;
        private readonly IUserDataManager _userDataManager;
        private List<User> users = new List<User>();

        public UserRepository(ICurrencyRepository currencyRepository, IUserDataManager userDataManager)
        {
            _currencyRepository = currencyRepository;
            _userDataManager = userDataManager;
        }
        public User Add<TUser>(TUser user) where TUser : User
        {
           this.users.Add(user);
            return users.FirstOrDefault();
        }

        public List<User> FindAll()
        {
            return this.users;
        }
        

        public bool? AddUserBalance(int userId,AddMoney money)
        {
            var user = _userDataManager.GetUserBalance(userId,money.CurrencyId);

            var currency = _userDataManager.GetCurrency(money.CurrencyId);
           
            var addMoney = new Money(currency, money.Amount);

            var result = user.Balance.AddMoney(addMoney);

            _userDataManager.UpdateUserBalance(user);

            return result;
        }

        public List<UserBalance> GetUserBalance(int userId)
        {
           
            var user = _userDataManager.GetUserBalances(userId);

            var result = new List<UserBalance>();

            var balance = user.Balance.GetAllMoney();

            foreach (var value in balance)
            {
                result.Add(new UserBalance(value.Currency.Name, value.Amount));
            }

            return result;
        }

        public bool? ExchangeMoney(ExchangeMoney exhangeMoney,int userId)
        {
            var user = _userDataManager.GetUserBalances(userId);

            var fromCurrency = _userDataManager.GetCurrency(exhangeMoney.FromCurrencyId);
            var toCurrency = _userDataManager.GetCurrency(exhangeMoney.ToCurrencyId);

            var exchange = new Money(fromCurrency, exhangeMoney.Amount);

            var result = user.Balance.ExchangeMoney(exchange, toCurrency);
            _userDataManager.UpdateUserBalance(user);

            return result;
        }

        public bool? SendMoney(SendMoney sendMoney, int userId)
        {
            var fromUser = _userDataManager.GetUserBalance(userId, sendMoney.CurrencyId);
            var toUser = _userDataManager.GetUserBalance(sendMoney.ToUserId, sendMoney.CurrencyId);

            var currency = _userDataManager.GetCurrency(sendMoney.CurrencyId);

            var balance = new Balance();
            var sendFrom = new Money(currency, sendMoney.Amount);
            var sendTo = new Money(currency, sendMoney.Amount);

            fromUser.Balance.ChargeMoney(sendFrom);
            _userDataManager.UpdateUserBalance(fromUser);

            var result = toUser.Balance.AddMoney(sendTo);
            _userDataManager.UpdateUserBalance(toUser);

            return result;
        }

    }
}
