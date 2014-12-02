using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PorfolioInvesment.Models
{
    public class ParaStock
    {
        [Required]
        public string PI_tickerList { get; set; }

    }
}