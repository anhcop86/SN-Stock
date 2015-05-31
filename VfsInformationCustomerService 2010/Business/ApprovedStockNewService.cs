
using System;
using System.Collections;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Data;
    
namespace Vfs.WebCrawler.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class ApprovedStockNewService
    {
        #region ApprovedStockNew

        public static void CreateApprovedStockNew(ApprovedStockNew approvedStockNew)
        {            
            try
            {
                ApprovedStockNewDAO approvedStockNewDAO = new ApprovedStockNewDAO();
                approvedStockNewDAO.CreateApprovedStockNew(approvedStockNew);
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
                throw new ApplicationException(SR.BusinessCreateApprovedStockNewException, ex);
            }
        }        

        public static void UpdateApprovedStockNew(ApprovedStockNew approvedStockNew)
        {            
            try
            {
                ApprovedStockNewDAO approvedStockNewDAO = new ApprovedStockNewDAO();
                approvedStockNewDAO.UpdateApprovedStockNew(approvedStockNew);
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
                throw new ApplicationException(SR.BusinessUpdateApprovedStockNewException, ex);
            }
        }        

        public static void DeleteApprovedStockNew(int newsId)
        {            
            try
            {
                ApprovedStockNewDAO approvedStockNewDAO = new ApprovedStockNewDAO();
                approvedStockNewDAO.DeleteApprovedStockNew(newsId);
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
                throw new ApplicationException(SR.BusinessDeleteApprovedStockNewException, ex);
            }
        }
        
        public static ApprovedStockNew GetApprovedStockNew(int newsId)
        {            
            try
            {
                ApprovedStockNewDAO approvedStockNewDAO = new ApprovedStockNewDAO();
                return approvedStockNewDAO.GetApprovedStockNew(newsId);                
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
                throw new ApplicationException(SR.BusinessGetApprovedStockNewException, ex);
            }
        }                

        public static ApprovedStockNewCollection GetApprovedStockNewList(ApprovedStockNewColumns orderBy, string orderDirection)
        {            
            try
            {
                ApprovedStockNewDAO approvedStockNewDAO = new ApprovedStockNewDAO();
                return approvedStockNewDAO.GetApprovedStockNewList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetApprovedStockNewListException, ex);
            }
        }        

        public static ApprovedStockNewCollection GetApprovedStockNewList(ApprovedStockNewColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                ApprovedStockNewDAO approvedStockNewDAO = new ApprovedStockNewDAO();
                return approvedStockNewDAO.GetApprovedStockNewList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetApprovedStockNewListException, ex);
            }
        }        

        #endregion
    }
}