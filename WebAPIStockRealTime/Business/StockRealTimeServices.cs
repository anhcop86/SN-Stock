using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class StockRealTimeServices
    {
        public static List<StockRealTime> GetGetALLTwoMarket()
        {

            try
            {

                return StockRealTimeDAO.GetALLTwoMarket();
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
