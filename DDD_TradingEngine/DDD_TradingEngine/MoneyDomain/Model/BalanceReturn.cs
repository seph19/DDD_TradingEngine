using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class BalanceReturn
    {
        public Currency currency { get; set; }
        public decimal amount { get; set; }
    }
}
