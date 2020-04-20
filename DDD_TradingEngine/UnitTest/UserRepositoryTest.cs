using AutoFixture;
using DDD_TradingEngine.MoneyDomain.DataFolder;
using DDD_TradingEngine.MoneyDomain.Repository;
using DDD_TradingEngine.MoneyDomain.Model;
using DDD_TradingEngine.UserDomain.Model;
using DDD_TradingEngine.UserDomain.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    class UserRepositoryTest
    {
        private readonly Mock<ICurrencyRepository> _currencyRepository = new Mock<ICurrencyRepository>();
        private readonly Mock<IUserDataManager> _userDataManager = new Mock<IUserDataManager>();

        private readonly UserRepository userRepository;
        public UserRepositoryTest()
        {
            userRepository = new UserRepository(
            _currencyRepository.Object,
            _userDataManager.Object);
        }

        [Test]
        public void whenGetting_UserBalance_ThenResultShould_BeTheSameWithExpeted()
        {
            var fixture = new Fixture();
            var userId = fixture.Create<int>();
            var dataResult = fixture.Create<User>();
            var expected = new List<UserBalance>();
            var user = _userDataManager.Setup(x => x.GetUserBalances(userId)).Returns(dataResult);

            var result = userRepository.GetUserBalance(userId);

            Assert.AreEqual(typeof(List<UserBalance>), result.GetType());
            Assert.IsNotNull(result);
        }

        
        [Test]
        public void When_Adding_UserBalance()
        {
            var fixture = new Fixture();
            var userId = 1;
            var currencyId = 1;

            var currencyResult = new Currency(currencyId, "USD", (decimal)1);
            var balance = new Balance();
            balance.AddMoney(new Money(currencyResult, 10));
            var user = userRepository.Add(new User(userId, "test", balance));

            var param = new AddMoney() { CurrencyId = 1, Amount = 10};

            var users = _userDataManager.Setup(x => x.GetUserBalance(userId, currencyId)).Returns(user);
            var currency = _userDataManager.Setup(x => x.GetCurrency(currencyId)).Returns(currencyResult);

            _userDataManager.Setup(x => x.UpdateUserBalance(user));

            var result = userRepository.AddUserBalance(userId, param);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void When_Sending_Money()
        {
            var fixture = new Fixture();
            var userId = 1;
            var currencyId = 1;

            var currencyResult = new Currency(currencyId, "USD", (decimal)1);
            var balance = new Balance();
            balance.AddMoney(new Money(currencyResult, 10));
            var user = userRepository.Add(new User(userId, "test", balance));

            var param = new SendMoney() { CurrencyId = 1,ToUserId = 1, Amount = 10};

            var dataResult = _userDataManager.Setup(x => x.GetUserBalance(userId, currencyId)).Returns(user);
            var currency = _userDataManager.Setup(x => x.GetCurrency(currencyId)).Returns(currencyResult);

            _userDataManager.Setup(x => x.UpdateUserBalance(user));

            var result = userRepository.SendMoney(param, userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void When_Exhanging_Money()
        {
            var fixture = new Fixture();
            var userId = 1;
            var currencyId = 1;

            var currencyResult = new Currency(currencyId, "EUR", (decimal)2);
            var balance = new Balance();
            balance.AddMoney(new Money(currencyResult, 10));
            var dataResult = userRepository.Add(new User(userId, "test", balance));

            var param = new ExchangeMoney() {FromCurrencyId = currencyId, ToCurrencyId = 2, Amount = 10};

            var user = _userDataManager.Setup(x => x.GetUserBalances(userId)).Returns(dataResult);
            var currency = _userDataManager.Setup(x => x.GetCurrency(currencyId)).Returns(currencyResult);
            _userDataManager.Setup(x => x.UpdateUserBalance(dataResult));

            var result = userRepository.ExchangeMoney(param, userId);

            Assert.IsNotNull(result);
        }
    }
}
