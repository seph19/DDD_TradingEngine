using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class UserBalance
    {
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }

        public UserBalance(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }
    }
}
