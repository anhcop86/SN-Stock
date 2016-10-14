using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStockPriceFromURL.Models
{
    public struct StockDto
    {
        /// <summary>
        /// Company ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Basic price
        /// </summary>
        public decimal Basic { get; set; }

        /// <summary>
        /// Floor price : giá sàn
        /// </summary>
        public decimal Floor { get; set; }

        /// <summary>
        /// Ceiling price : giá trần
        /// </summary>
        public decimal Ceiling { get; set; }
    }

}