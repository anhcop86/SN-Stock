using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class StockServices
    {
        static StockDao stockDao;
        public static List<Stock> GetINDEXHSX(string listTicker)
        {

            try
            {
                stockDao = new StockDao();
                return stockDao.GetStockPriceHSX(listTicker);
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {

                throw;
            }


        }
        public static List<Stock> GetINDEXHNX(string listTicker)
        {
            try
            {
                stockDao = new StockDao();
                return stockDao.GetStockPriceHNX(listTicker);
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<Stock> GetAllStock(string listTicker)
        {
            try
            {
                stockDao = new StockDao();
                List<Stock> list = new List<Stock>();
                list = GetINDEXHSX(listTicker);
                foreach (var item in GetINDEXHNX(listTicker))
                {
                    list.Add(item);
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
