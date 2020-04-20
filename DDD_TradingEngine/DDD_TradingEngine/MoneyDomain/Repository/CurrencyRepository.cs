using DDD_TradingEngine.MoneyDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private List<Currency> currencies = new List<Currency>();
        public void Add<TCurrency>(TCurrency currency) where TCurrency : Currency
        {
            this.currencies.Add(currency);
        }

        public List<Currency> FindAll()
        {
            return this.currencies;
        }
    }
}
