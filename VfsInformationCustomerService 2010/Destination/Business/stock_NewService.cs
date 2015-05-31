
using System;
using System.Collections;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
    
namespace Vfs.WebCrawler.Destination.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class stock_NewService
    {
        #region stock_New

        public static void Createstock_New(stock_New stock_New)
        {            
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                stock_NewDAO.Createstock_New(stock_New);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessCreatestock_NewException, ex);
            }
        }        

        public static void Updatestock_New(stock_New stock_New)
        {            
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                stock_NewDAO.Updatestock_New(stock_New);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessUpdatestock_NewException, ex);
            }
        }        

        public static void Deletestock_New(int newsID)
        {            
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                stock_NewDAO.Deletestock_New(newsID);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessDeletestock_NewException, ex);
            }
        }
        
        public static stock_New Getstock_New(int newsID)
        {            
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                return stock_NewDAO.Getstock_New(newsID);                
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_NewException, ex);
            }
        }                

        public static stock_NewCollection Getstock_NewList(stock_NewColumns orderBy, string orderDirection)
        {            
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                return stock_NewDAO.Getstock_NewList(orderBy, orderDirection);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_NewListException, ex);
            }
        }        

        public static stock_NewCollection Getstock_NewList(stock_NewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                return stock_NewDAO.Getstock_NewList(orderBy, orderDirection, page, pageSize, out totalRecords);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_NewListException, ex);
            }
        }

        public static stock_NewCollection Getstock_NewList(string StockCode, string customerID, stock_NewColumns orderBy, string orderDirection)
        {
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                return stock_NewDAO.Getstock_NewList(StockCode, customerID, orderBy, orderDirection);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_NewListException, ex);
            }
        }

        public static stock_NewCollection Getstock_NewList(string StockCode, string customerID, stock_NewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                return stock_NewDAO.Getstock_NewList(StockCode, customerID, orderBy, orderDirection, page, pageSize, out totalRecords);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_NewListException, ex);
            }
        }

        public static stock_NewCollection Getstock_NewList(string StockCode, string customerID)
        {
            try
            {
                stock_NewDAO stock_NewDAO = new stock_NewDAO();
                return stock_NewDAO.Getstock_NewList(StockCode, customerID);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessGetstock_NewListException, ex);
            }
        }
        

        #endregion
    }
}