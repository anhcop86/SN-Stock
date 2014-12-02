using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Stock : StockBase
    {
        public Stock()
        {
        }

        /*
        public static List<Stock> operator +(List<Stock> a, List<Stock> b)
        {
            List<Stock> list = new List<Stock>();
            foreach (var itema in a)
            {
                list.Add(itema);
            }
            foreach (var itemb in b)
            {
                list.Add(itemb);
            }

            return list;
        }
         * */
    }
}
