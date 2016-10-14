using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStockPriceFromURL.Models
{
    public struct MarketDto
    {
        /// <summary>
        /// TYPE: S: Stock, I: Index, O: other 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal FinishPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Diff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal DiffRate { get; set; }
    }
}