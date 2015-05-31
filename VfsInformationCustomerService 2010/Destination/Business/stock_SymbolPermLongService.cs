
using System;
using System.Collections;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
    
namespace Vfs.WebCrawler.Destination.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class stock_SymbolPermLongService
    {
        #region stock_SymbolPermLong

        public static void Createstock_SymbolPermLong(stock_SymbolPermLong stock_SymbolPermLong)
        {            
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                stock_SymbolPermLongDAO.Createstock_SymbolPermLong(stock_SymbolPermLong);
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
                throw new ApplicationException(SR.BusinessCreatestock_SymbolPermLongException, ex);
            }
        }        

        public static void Updatestock_SymbolPermLong(stock_SymbolPermLong stock_SymbolPermLong)
        {            
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                stock_SymbolPermLongDAO.Updatestock_SymbolPermLong(stock_SymbolPermLong);
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
                throw new ApplicationException(SR.BusinessUpdatestock_SymbolPermLongException, ex);
            }
        }        

        public static void Deletestock_SymbolPermLong(int symbolID, DateTime permDate)
        {            
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                stock_SymbolPermLongDAO.Deletestock_SymbolPermLong(symbolID, permDate);
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
                throw new ApplicationException(SR.BusinessDeletestock_SymbolPermLongException, ex);
            }
        }
        
        public static stock_SymbolPermLong Getstock_SymbolPermLong(int symbolID, DateTime permDate)
        {            
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                return stock_SymbolPermLongDAO.Getstock_SymbolPermLong(symbolID, permDate);                
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongException, ex);
            }
        }                

        public static stock_SymbolPermLongCollection Getstock_SymbolPermLongList(stock_SymbolPermLongColumns orderBy, string orderDirection)
        {            
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                return stock_SymbolPermLongDAO.Getstock_SymbolPermLongList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }        

        public static stock_SymbolPermLongCollection Getstock_SymbolPermLongList(stock_SymbolPermLongColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                return stock_SymbolPermLongDAO.Getstock_SymbolPermLongList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }
        public static stock_SymbolPermLongExtensionCollection Export_SymbolPermLongList(DateTime permDate,string market)
        {
            try
            {
                stock_SymbolPermLongDAO stock_symbolPermLongDAO = new stock_SymbolPermLongDAO();
                return stock_symbolPermLongDAO.Export_SymbolPermLongList(permDate, market);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }
        public static stock_SymbolPermLongCollection GetUpdated_SymbolPermLongGetList(DateTime permDate)
        {
            try
            {
                stock_SymbolPermLongDAO stock_symbolPermLongDAO = new stock_SymbolPermLongDAO();
                return stock_symbolPermLongDAO.GetUpdated_SymbolPermLongGetList(permDate);
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
                throw new ApplicationException(SR.BusinessGetstock_SymbolPermLongListException, ex);
            }
        }
        public static void UpdateStock_SymbolPermLongDataForeign(DateTime permDate)
        {
            try
            {
                stock_SymbolPermLongDAO stock_SymbolPermLongDAO = new stock_SymbolPermLongDAO();
                stock_SymbolPermLongDAO.UpdateStock_SymbolPermLongDataForeign(permDate);
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
                throw new ApplicationException(SR.BusinessDeletestock_SymbolPermLongException, ex);
            }
        }

        #endregion
    }
}