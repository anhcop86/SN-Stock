using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class IPProxyBase
    {
        public int Id { get; set; }

        public string IPAddress { get; set; }

        public int IPPort { get; set; }

        public bool StatusIP { get; set; }

        public int CreateDate { get; set; }

    }
}
