using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataFromMetaStock
{
    public class MetaStockTable : List<MetaStockRow>
    {
        public string Symbol;

        public MetaStockTable FilterPeriod(DateTime Start, DateTime End)
        {
            MetaStockTable M = new MetaStockTable();
            for (int t = 0; t < this.Count; t++) {
                if (this[t].Day >= Start && this[t].Day <= End) M.Add(this[t]);
            }
            return M;
        }

        public List<DateTime> GetDates()
        {
            List<DateTime> res = new List<DateTime>();
            foreach (MetaStockRow row in this) {
                res.Add(row.Day);
            }
            return res;
        }

        public double[] GetOpenArray()
        {
            double[] open = new double[this.Count];
            for (int t = 0; t < open.Length; t++) open[t] = this[t].Open;
            return open;
        }

        public double[] GetCloseArray()
        {
            double[] cl = new double[this.Count];
            for (int t = 0; t < cl.Length; t++) cl[t] = this[t].Close;
            return cl;
        }

        public double[] GetOpenCloseArray()
        {
            double[] opcl = new double[this.Count * 2];
            for (int t = 0; t < this.Count; t++) {
                opcl[t * 2] = this[t].Open;
                opcl[t * 2 + 1] = this[t].Close;
            }
            return opcl;
        }

    }
}
