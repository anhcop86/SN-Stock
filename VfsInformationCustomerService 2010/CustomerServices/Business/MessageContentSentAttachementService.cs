
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class MessageContentSentAttachementService
    {
        #region MessageContentSentAttachement

        public static void CreateMessageContentSentAttachement(MessageContentSentAttachement messageContentSentAttachement)
        {            
            try
            {
                MessageContentSentAttachementDAO messageContentSentAttachementDAO = new MessageContentSentAttachementDAO();
                messageContentSentAttachementDAO.CreateMessageContentSentAttachement(messageContentSentAttachement);
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
                throw new ApplicationException(SR.BusinessCreateMessageContentSentAttachementException, ex);
            }
        }        

        public static void UpdateMessageContentSentAttachement(MessageContentSentAttachement messageContentSentAttachement)
        {            
            try
            {
                MessageContentSentAttachementDAO messageContentSentAttachementDAO = new MessageContentSentAttachementDAO();
                messageContentSentAttachementDAO.UpdateMessageContentSentAttachement(messageContentSentAttachement);
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
                throw new ApplicationException(SR.BusinessUpdateMessageContentSentAttachementException, ex);
            }
        }        

        public static void DeleteMessageContentSentAttachement(int messageContentSentAttachementID)
        {            
            try
            {
                MessageContentSentAttachementDAO messageContentSentAttachementDAO = new MessageContentSentAttachementDAO();
                messageContentSentAttachementDAO.DeleteMessageContentSentAttachement(messageContentSentAttachementID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentSentAttachementException, ex);
            }
        }
        
        public static MessageContentSentAttachement GetMessageContentSentAttachement(int messageContentSentAttachementID)
        {            
            try
            {
                MessageContentSentAttachementDAO messageContentSentAttachementDAO = new MessageContentSentAttachementDAO();
                return messageContentSentAttachementDAO.GetMessageContentSentAttachement(messageContentSentAttachementID);                
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentAttachementException, ex);
            }
        }                

        public static MessageContentSentAttachementCollection GetMessageContentSentAttachementList(MessageContentSentAttachementColumns orderBy, string orderDirection)
        {            
            try
            {
                MessageContentSentAttachementDAO messageContentSentAttachementDAO = new MessageContentSentAttachementDAO();
                return messageContentSentAttachementDAO.GetMessageContentSentAttachementList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentAttachementListException, ex);
            }
        }        

        public static MessageContentSentAttachementCollection GetMessageContentSentAttachementList(MessageContentSentAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                MessageContentSentAttachementDAO messageContentSentAttachementDAO = new MessageContentSentAttachementDAO();
                return messageContentSentAttachementDAO.GetMessageContentSentAttachementList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentAttachementListException, ex);
            }
        }        

        #endregion
    }
}