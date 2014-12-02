using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class IPProxyServices
    {
        static IPProxyDao iPProxyDao;
        public static void CreateIPProxy(IPProxy iPProxy)
        {

            try
            {
                iPProxyDao = new IPProxyDao();
                iPProxyDao.CreateIPProxy(iPProxy);
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static List<IPProxy> GetIPProxyListAll()
        {

            try
            {
                iPProxyDao = new IPProxyDao();
                return iPProxyDao.GetIPProxyListAll();
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
