using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DDD_TradingEngine.MoneyDomain.Model
{
    public class Balance
    {
        private Dictionary<Currency, decimal> Currencies = new Dictionary<Currency, decimal>();

        public List<Money> GetAllMoney()
        {
            var result = Currencies.Select(a => new Money(a.Key, a.Value)).ToList();

            return result;
        }
        public bool? AddMoneyBalance(Money money)
        {
            try
            {
                if (Currencies.ContainsKey(money.Currency))
                {
                    Currencies[money.Currency] = Currencies.GetValueOrDefault(money.Currency) + money.Amount;
                }
                else
                {
                    Currencies[money.GetCurrency()] = money.GetAmount();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool? AddMoney(Money money)
        {
            try
            {
                decimal? currentBalance = Currencies.Values.FirstOrDefault();
                if (money.Amount > 0)
                {
                    Currencies[money.Currency] = currentBalance + money.Amount ?? 0;
                }
                else
                {
                    Currencies[money.GetCurrency()] = money.GetAmount();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ChargeMoney(Money money)
        {
            decimal currentBalance = Currencies.Values.FirstOrDefault();
            if (currentBalance > money.Amount)
            {
                Currencies[money.Currency] = currentBalance - money.Amount;
            }


        }

        public bool? ExchangeMoney(Money money, Currency toCurrency)
        {
            try
            {
                decimal ratioBetweenCurrencies = Math.Round(money.Currency.Ratio / toCurrency.Ratio, 2);
                var result = AddMoneyBalance(new Money(toCurrency, (decimal)Math.Round(money.Amount * ratioBetweenCurrencies * 100) / 100));
                ChargeMoney(money);

                return result;
            }
            catch
            {
                return false;
            }

            
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        
    }
}
