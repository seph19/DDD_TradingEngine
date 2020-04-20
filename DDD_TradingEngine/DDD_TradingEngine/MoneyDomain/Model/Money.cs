using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class Money
    {
        public Currency Currency { get; }
        public decimal Amount { get; }
        public Money()
        {

        }

        public Money(Currency currency, decimal amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public Currency GetCurrency()
        {
            return Currency;
        }
        public decimal GetAmount()
        {
            return Amount;
        }

    }
}
