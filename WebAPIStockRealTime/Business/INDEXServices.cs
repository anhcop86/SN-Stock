using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class INDEXServices
    {
        static INDEXDAO iNDEXDAO;

        public static INDEX GetINDEXHSX()
        {

            try
            {
                iNDEXDAO = new INDEXDAO();
                return iNDEXDAO.GetINDEXHSX();
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static INDEX GetINDEXHNX()
        {

            try
            {
                iNDEXDAO = new INDEXDAO();
                return iNDEXDAO.GetINDEXHNX();
                //return aGNews_ArticlesDAO.
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
