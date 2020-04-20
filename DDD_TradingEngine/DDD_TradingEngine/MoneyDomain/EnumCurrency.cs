using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain
{
    public enum EnumCurrency
    {
        [Description("USD")]
        Usd = 1,
        [Description("EUR")]
        Eur = 2,
        [Description("CAD")]
        Cad = 3
    }

}
