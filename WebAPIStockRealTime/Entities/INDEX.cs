using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class INDEX: INDEXBase
    {
        public INDEX()
        {

        }
        public static List<INDEX> operator +(INDEX a, INDEX b)
        {
            var list = new List<INDEX>() { a, b };

            return list;
        }
    }
}
