using DDD_TradingEngine.MoneyDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Repository
{
    public interface ICurrencyRepository
    {
        void Add<TCurrency>(TCurrency currency) where TCurrency : Currency;
        List<Currency> FindAll();
    }
}
