using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.DataFolder.DataModel
{
    public class UserBalance
    {
        public int UserId { get;  set; }
        public string Username { get; set; }
        public decimal Amount { get;  set; }
        public int CurrencyId { get;  set; }
        public string Currency { get;  set; }
        public decimal Ratio { get;  set; }
        
    }
}
