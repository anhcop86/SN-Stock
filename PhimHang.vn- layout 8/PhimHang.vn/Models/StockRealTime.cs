using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class StockRealTime
    {
        /// <summary>
        /// TYPE: S: Stock, I: Index, O: other 
        /// </summary>
        public string Type { get; set; }
        public string CompanyID { get; set; }

        public decimal FinishPrice { get; set; }

        public decimal Diff { get; set; }

        public decimal DiffRate { get; set; }
    }
}