using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Data.Entity;

namespace PhimHang.Models
{
    public sealed class FilterKeyworkSingleton
    {
        static FilterKeyworkSingleton instance = null;
        static readonly object padlock = new object();
        private readonly Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(200000);
        private readonly List<string> listFilter = new List<string>();

        FilterKeyworkSingleton()
        {
            getListFilterFromDatabase("");
            _timer = new Timer(getListFilterFromDatabase, null, _updateInterval, _updateInterval);
        }

        public static FilterKeyworkSingleton Instance
        {
            get 
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new FilterKeyworkSingleton();
                        }
                    }
                }
                return instance;
            }
        }
        public List<string> getListKeyWord()
        {
            return listFilter;
        }
        private void getListFilterFromDatabase(object state)
        {
            using (testEntities db = new testEntities())
            {
                var listtemp = (from f in db.FilterKeyWords.ToList()
                                select new
                                {
                                    Word = f.KeyWord

                                }).ToList();
                if (listtemp.Count > 0)
                {
                    listFilter.Clear();
                    listtemp.ForEach(t => listFilter.Add(t.Word));
                }
            }
            //var listtemp =
        }
    }
}