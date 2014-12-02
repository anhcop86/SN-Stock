using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApiReatime.Nhibernate.Models
{
    public class IPProxy
    {
        public virtual int Id { get; set; }
        public virtual string IPAddress { get; set; }
        public virtual string IPPort { get; set; }
        public virtual bool StatusIP { get; set; }
        public virtual int CreateDate { get; set; }
    }
}