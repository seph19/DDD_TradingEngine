using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class SendMoney
    {
        public int CurrencyId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
