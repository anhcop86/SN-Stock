
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class IncomingMessageContentService
    {
        #region IncomingMessageContent

        public static void CreateIncomingMessageContent(IncomingMessageContent incomingMessageContent)
        {            
            try
            {
                IncomingMessageContentDAO incomingMessageContentDAO = new IncomingMessageContentDAO();
                incomingMessageContentDAO.CreateIncomingMessageContent(incomingMessageContent);
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
                throw new ApplicationException(SR.BusinessCreateIncomingMessageContentException, ex);
            }
        }        

        public static void UpdateIncomingMessageContent(IncomingMessageContent incomingMessageContent)
        {            
            try
            {
                IncomingMessageContentDAO incomingMessageContentDAO = new IncomingMessageContentDAO();
                incomingMessageContentDAO.UpdateIncomingMessageContent(incomingMessageContent);
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
                throw new ApplicationException(SR.BusinessUpdateIncomingMessageContentException, ex);
            }
        }        

        public static void DeleteIncomingMessageContent(int incomingMessageContentID)
        {            
            try
            {
                IncomingMessageContentDAO incomingMessageContentDAO = new IncomingMessageContentDAO();
                incomingMessageContentDAO.DeleteIncomingMessageContent(incomingMessageContentID);
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
                throw new ApplicationException(SR.BusinessDeleteIncomingMessageContentException, ex);
            }
        }
        
        public static IncomingMessageContent GetIncomingMessageContent(int incomingMessageContentID)
        {            
            try
            {
                IncomingMessageContentDAO incomingMessageContentDAO = new IncomingMessageContentDAO();
                return incomingMessageContentDAO.GetIncomingMessageContent(incomingMessageContentID);                
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
                throw new ApplicationException(SR.BusinessGetIncomingMessageContentException, ex);
            }
        }                

        public static IncomingMessageContentCollection GetIncomingMessageContentList(IncomingMessageContentColumns orderBy, string orderDirection)
        {            
            try
            {
                IncomingMessageContentDAO incomingMessageContentDAO = new IncomingMessageContentDAO();
                return incomingMessageContentDAO.GetIncomingMessageContentList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetIncomingMessageContentListException, ex);
            }
        }        

        public static IncomingMessageContentCollection GetIncomingMessageContentList(IncomingMessageContentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                IncomingMessageContentDAO incomingMessageContentDAO = new IncomingMessageContentDAO();
                return incomingMessageContentDAO.GetIncomingMessageContentList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetIncomingMessageContentListException, ex);
            }
        }        

        #endregion
    }
}