using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public decimal Ratio { get; set; }

        //constructor to hibernate
        public Currency()
        {
        }
        public Currency(int currencyId,string name, decimal ratio)
        {
            this.CurrencyId = currencyId;
            this.Name = name;
            this.Ratio = ratio;
        }

        public string GetName()
        {
            return Name;
        }

        public decimal GetRatio()
        {
            return Ratio;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


        //public string ToString()
        //{
        //    return "Currency{" +
        //        "name='" + name + '\'' +
        //        ", ratio=" + ratio +
        //        '}';
        //}
    }
}
