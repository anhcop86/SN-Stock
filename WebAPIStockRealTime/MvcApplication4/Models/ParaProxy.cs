using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PorfolioInvesment.Models
{
    public class ParaProxy
    {        
        [Required]
        [MaxLength(16)] 
        public string IPAddress { get; set; }

         [Required]     
        public string IPPort { get; set; }

        [Required]        
        public string StatusIP { get; set; }

        [Required]
        public string CreateDate { get; set; }
    }
}