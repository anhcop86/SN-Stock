using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class INDEXBase
    {
        public string IndexName { get; set; }
        public DateTime SessionDate { get; set; }

        public decimal Index { get; set; }

        public decimal Diff { get; set; }


        public decimal DiffRate { get; set; }

        public decimal Total { get; set; }

        public int TotalShare { get; set; }


        public int Advances { get; set; }

        public int NoChange { get; set; }

        public int Declines { get; set; }

       
    }
}
