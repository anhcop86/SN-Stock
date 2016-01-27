using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class StockCodeModel
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string LongName { get; set; }
        [Required]
        public Nullable<short> MarketType { get; set; }
        [Required]
        public string IndexName { get; set; }
    }
}