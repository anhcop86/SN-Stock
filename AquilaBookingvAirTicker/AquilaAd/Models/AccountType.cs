using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class AccountType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public AccountType(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public AccountType()
        {
         
        }

        public List<AccountType> listAllHotDealHotel()
        {
            List<AccountType> list = new List<AccountType>();
            list.Add(new AccountType("H", "Hotel"));
            list.Add(new AccountType("B", "Admin"));
            //list.Add(new AccountType("F", "FrontEnd"));

            return list;
        }
    }
}