using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class StockCodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public Nullable<short> MarketType { get; set; }
        public string IndexName { get; set; }
    }
}