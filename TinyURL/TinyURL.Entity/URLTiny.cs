using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyURL.Entity
{
    public class URLTiny
    {
        public long Id { get; set; }
        public string URLName { get; set; }
        public System.DateTime PostedDate { get; set; }
    }
}
