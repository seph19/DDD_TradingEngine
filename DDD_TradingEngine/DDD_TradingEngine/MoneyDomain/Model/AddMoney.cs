using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class AddMoney
    {
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
