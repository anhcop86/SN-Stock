
using System;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Data;
    
namespace Vfs.WebCrawler.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class RejectedStockNewService
    {
        #region RejectedStockNew

        public static void CreateRejectedStockNew(RejectedStockNew rejectedStockNew)
        {            
            try
            {
                RejectedStockNewDAO rejectedStockNewDAO = new RejectedStockNewDAO();
                rejectedStockNewDAO.CreateRejectedStockNew(rejectedStockNew);
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
                throw new ApplicationException(SR.BusinessCreateRejectedStockNewException, ex);
            }
        }        

        public static void UpdateRejectedStockNew(RejectedStockNew rejectedStockNew)
        {            
            try
            {
                RejectedStockNewDAO rejectedStockNewDAO = new RejectedStockNewDAO();
                rejectedStockNewDAO.UpdateRejectedStockNew(rejectedStockNew);
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
                throw new ApplicationException(SR.BusinessUpdateRejectedStockNewException, ex);
            }
        }        

        public static void DeleteRejectedStockNew(int newsId)
        {            
            try
            {
                RejectedStockNewDAO rejectedStockNewDAO = new RejectedStockNewDAO();
                rejectedStockNewDAO.DeleteRejectedStockNew(newsId);
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
                throw new ApplicationException(SR.BusinessDeleteRejectedStockNewException, ex);
            }
        }
        
        public static RejectedStockNew GetRejectedStockNew(int newsId)
        {            
            try
            {
                RejectedStockNewDAO rejectedStockNewDAO = new RejectedStockNewDAO();
                return rejectedStockNewDAO.GetRejectedStockNew(newsId);                
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
                throw new ApplicationException(SR.BusinessGetRejectedStockNewException, ex);
            }
        }                

        public static RejectedStockNewCollection GetRejectedStockNewList(RejectedStockNewColumns orderBy, string orderDirection)
        {            
            try
            {
                RejectedStockNewDAO rejectedStockNewDAO = new RejectedStockNewDAO();
                return rejectedStockNewDAO.GetRejectedStockNewList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetRejectedStockNewListException, ex);
            }
        }        

        public static RejectedStockNewCollection GetRejectedStockNewList(RejectedStockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                RejectedStockNewDAO rejectedStockNewDAO = new RejectedStockNewDAO();
                return rejectedStockNewDAO.GetRejectedStockNewList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetRejectedStockNewListException, ex);
            }
        }        

        #endregion
    }
}