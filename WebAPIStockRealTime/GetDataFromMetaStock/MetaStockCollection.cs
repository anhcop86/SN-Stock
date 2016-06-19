using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromMetaStock
{
    public class MetaStockCollection : List<MetaStockTable>
    {
        public void AddSymbol(string s, MetaStockReader MR)
        {
            this.Add(MR.ReadSymbol(s));
        }

        public MetaStockCollection FilterPeriod(DateTime Start, DateTime End)
        {
            MetaStockCollection R = new MetaStockCollection();
            foreach (MetaStockTable tab in this) {
                MetaStockTable newtab = tab.FilterPeriod(Start, End);
                newtab.Symbol = tab.Symbol;
                R.Add(newtab);
            }
            return R;
        }

        public MetaStockCollection IntersectDates()
        {
            // create valid dates list
            List<DateTime> ListaDate = this[0].GetDates();
            foreach (MetaStockTable tab in this) {
                ListaDate = IntersectDates(ListaDate, tab.GetDates());
            }

            // create return value (all tables)
            MetaStockCollection R = new MetaStockCollection();
            foreach (MetaStockTable tab in this) {
                // create single table
                MetaStockTable rt = new MetaStockTable();
                rt.Symbol = tab.Symbol;
                foreach (MetaStockRow rr in tab) {
                    if (ListaDate.Contains(rr.Day)) rt.Add(rr);
                }
                R.Add(rt);
            }
            return R;
        }

        private static List<DateTime> IntersectDates(List<DateTime> l1, List<DateTime> l2)
        {
            List<DateTime> res = new List<DateTime>();
            foreach (DateTime day in l1) {
                if (l2.Contains(day)) res.Add(day);
            }
            return res;
        }
    }
}
