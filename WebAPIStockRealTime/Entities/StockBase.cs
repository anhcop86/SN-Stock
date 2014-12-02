using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class StockBase
    {
        public string CompanyID { get; set; }      
        public decimal FinishPrice { get; set; }
        public int FinishAmount { get; set; }
        public int TotalAmount { get; set; }
        public decimal Diff { get; set; }
        public decimal RefPrice { get; set; }

    }
}
