using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class AGNews_ArticlesServices
    {
        static AGNews_ArticlesDAO aGNews_ArticlesDAO;
        public static List<AGNews_Articles> GetAGNews_ArticlesListByTickerList(string tickerList, int page, int pageSize, out int totalRecords)
        {
            
            try
            {
                aGNews_ArticlesDAO = new AGNews_ArticlesDAO();
                return aGNews_ArticlesDAO.GetAGNews_ArticlesListByTickerList(tickerList,page,pageSize, out totalRecords);
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {
                
                throw;
            }

            
        }
    }
}
