using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DDD_TradingEngine.MoneyDomain.Model;
using System.ComponentModel;

namespace DDD_TradingEngine.UserDomain.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public Balance Balance { get; set; }

        public User(int userId,string username, Balance balance)
        {
            this.UserId = userId;
            this.Username = username;
            this.Balance = balance;
        }

        //no parameter  constructor to hibernate
        public User()
        {
        }

        public string GetUsername()
        {
            return Username;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
       
        //public Dictionary<string,double> GetBalance()
        //{
        //    var dictionary = new Dictionary<string, double>();
        //    dictionary.Add(balance.GetAllMoney().GetCurrency().GetName(), balance.GetAllMoney().amount);

        //    return dictionary;
        //}

    }
}
