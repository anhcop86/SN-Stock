using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApiReatime.Models
{
    public class StockPrice
    {
        public StockPrice()
        {

        }
       
        public string CompanyID { get; set; }
        public int FinishAmount { get; set; }
        public decimal FinishPrice { get; set; }      
        public int TotalAmount { get; set; }
        public decimal Diff { get; set; }
        public decimal RefPrice { get; set; }

    }
}