
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class IncomingMessageContentSentService
    {
        #region IncomingMessageContentSent

        public static void CreateIncomingMessageContentSent(IncomingMessageContentSent incomingMessageContentSent)
        {            
            try
            {
                IncomingMessageContentSentDAO incomingMessageContentSentDAO = new IncomingMessageContentSentDAO();
                incomingMessageContentSentDAO.CreateIncomingMessageContentSent(incomingMessageContentSent);
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
                throw new ApplicationException(SR.BusinessCreateIncomingMessageContentSentException, ex);
            }
        }        

        public static void UpdateIncomingMessageContentSent(IncomingMessageContentSent incomingMessageContentSent)
        {            
            try
            {
                IncomingMessageContentSentDAO incomingMessageContentSentDAO = new IncomingMessageContentSentDAO();
                incomingMessageContentSentDAO.UpdateIncomingMessageContentSent(incomingMessageContentSent);
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
                throw new ApplicationException(SR.BusinessUpdateIncomingMessageContentSentException, ex);
            }
        }        

        public static void DeleteIncomingMessageContentSent(long incomingMessageContentSentID)
        {            
            try
            {
                IncomingMessageContentSentDAO incomingMessageContentSentDAO = new IncomingMessageContentSentDAO();
                incomingMessageContentSentDAO.DeleteIncomingMessageContentSent(incomingMessageContentSentID);
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
                throw new ApplicationException(SR.BusinessDeleteIncomingMessageContentSentException, ex);
            }
        }
        
        public static IncomingMessageContentSent GetIncomingMessageContentSent(long incomingMessageContentSentID)
        {            
            try
            {
                IncomingMessageContentSentDAO incomingMessageContentSentDAO = new IncomingMessageContentSentDAO();
                return incomingMessageContentSentDAO.GetIncomingMessageContentSent(incomingMessageContentSentID);                
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
                throw new ApplicationException(SR.BusinessGetIncomingMessageContentSentException, ex);
            }
        }                

        public static IncomingMessageContentSentCollection GetIncomingMessageContentSentList(IncomingMessageContentSentColumns orderBy, string orderDirection)
        {            
            try
            {
                IncomingMessageContentSentDAO incomingMessageContentSentDAO = new IncomingMessageContentSentDAO();
                return incomingMessageContentSentDAO.GetIncomingMessageContentSentList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetIncomingMessageContentSentListException, ex);
            }
        }        

        public static IncomingMessageContentSentCollection GetIncomingMessageContentSentList(IncomingMessageContentSentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                IncomingMessageContentSentDAO incomingMessageContentSentDAO = new IncomingMessageContentSentDAO();
                return incomingMessageContentSentDAO.GetIncomingMessageContentSentList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetIncomingMessageContentSentListException, ex);
            }
        }        

        #endregion
    }
}