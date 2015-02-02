using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class StockRealTime
    {
        public string CompanyID { get; set; }

        public decimal FinishPrice { get; set; }

        public decimal Diff { get; set; }

        public decimal DiffRate { get; set; }
    }
}