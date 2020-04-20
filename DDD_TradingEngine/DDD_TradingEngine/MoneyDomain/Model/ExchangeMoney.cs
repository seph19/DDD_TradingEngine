using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class ExchangeMoney
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
