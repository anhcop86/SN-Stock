using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PorfolioInvesment.Models
{
    public class StockHis
    {
        public string Code { get; set; }

        public decimal? CeilingPrice { get; set; }

        public decimal? FloorPrice { get; set; }

        public decimal? DiffPrice { get; set; }

        public decimal? OpenPrice { get; set; }

        public decimal? HighPrice { get; set; }

        public decimal? LowPrice { get; set; }

        public decimal? ClosePrice { get; set; }

        public DateTime? TradingDate { get; set; }



    }
}