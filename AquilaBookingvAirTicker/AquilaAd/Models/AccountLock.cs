using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquilaAd.Models
{
    public class AccountLock
    {
         public string Id { get; set; }
        public string Name { get; set; }

        public AccountLock(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public AccountLock()
        {
         
        }

        public List<AccountLock> listAllHotDealHotel()
        {
            List<AccountLock> list = new List<AccountLock>();
            list.Add(new AccountLock("False", "False"));
            list.Add(new AccountLock("True", "True"));


            return list;
        }
    }
}