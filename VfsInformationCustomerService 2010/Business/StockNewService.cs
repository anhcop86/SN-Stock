
using System;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Data;
    
namespace Vfs.WebCrawler.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class StockNewService
    {
        #region StockNew

        public static void CreateStockNew(StockNew stockNew)
        {            
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                stockNewDAO.CreateStockNew(stockNew);
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
                throw new ApplicationException(SR.BusinessCreateStockNewException, ex);
            }
        }        

        public static void UpdateStockNew(StockNew stockNew)
        {            
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                stockNewDAO.UpdateStockNew(stockNew);
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
                throw new ApplicationException(SR.BusinessUpdateStockNewException, ex);
            }
        }        

        public static void DeleteStockNew(int newsId)
        {            
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                stockNewDAO.DeleteStockNew(newsId);
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
                throw new ApplicationException(SR.BusinessDeleteStockNewException, ex);
            }
        }
        
        public static StockNew GetStockNew(int newsId)
        {            
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNew(newsId);                
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
                throw new ApplicationException(SR.BusinessGetStockNewException, ex);
            }
        }                

        public static StockNewCollection GetStockNewList(StockNewColumns orderBy, string orderDirection)
        {            
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNewList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetStockNewListException, ex);
            }
        }        

        public static StockNewCollection GetStockNewList(StockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNewList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetStockNewListException, ex);
            }
        }

        public static StockNewCollection GetStockNewList(int linkId, StockNewColumns orderBy, string orderDirection)
        {
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNewListByLink(linkId, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetStockNewListException, ex);
            }
        }

        public static StockNewCollection GetStockNewList(int linkId, StockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNewListByLink(linkId, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetStockNewListException, ex);
            }
        }

        public static StockNewCollection GetStockNewListBySource(int sourceId, StockNewColumns orderBy, string orderDirection)
        {
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNewListBySource(sourceId, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetStockNewListException, ex);
            }
        }

        public static StockNewCollection GetStockNewListBySource(int sourceId, StockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                StockNewDAO stockNewDAO = new StockNewDAO();
                return stockNewDAO.GetStockNewListBySource(sourceId, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetStockNewListException, ex);
            }
        }        
        #endregion
    }
}