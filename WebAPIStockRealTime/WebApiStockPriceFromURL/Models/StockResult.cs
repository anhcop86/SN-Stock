using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStockPriceFromURL.Models
{
    public struct StockResult
    {
        public string Name { get; set; }
        public decimal Finish { get; set; }
        public decimal Diff { get; set; }
        public decimal Rate { get; set; }
    }
}