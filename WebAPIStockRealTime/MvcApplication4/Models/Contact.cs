using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication4.Models
{
    public class Contact
    {

        public int Id { get; set; }


        [Required]
        public string Name { get; set; }
    }
}