using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStockPriceFromURL.Models
{
    public struct StockResult
    {
        public string CompanyID { get; set; }

        public decimal FinishPrice { get; set; }

        public decimal Diff { get; set; }

        public decimal DiffRate { get; set; }
    }
}