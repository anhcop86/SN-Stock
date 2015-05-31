
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class MessageContentAttachementService
    {
        #region MessageContentAttachement

        public static void CreateMessageContentAttachement(MessageContentAttachement messageContentAttachement)
        {            
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                messageContentAttachementDAO.CreateMessageContentAttachement(messageContentAttachement);
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
                throw new ApplicationException(SR.BusinessCreateMessageContentAttachementException, ex);
            }
        }        

        public static void UpdateMessageContentAttachement(MessageContentAttachement messageContentAttachement)
        {            
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                messageContentAttachementDAO.UpdateMessageContentAttachement(messageContentAttachement);
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
                throw new ApplicationException(SR.BusinessUpdateMessageContentAttachementException, ex);
            }
        }        

        public static void DeleteMessageContentAttachement(int messageContentAttachementID)
        {            
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                messageContentAttachementDAO.DeleteMessageContentAttachement(messageContentAttachementID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentAttachementException, ex);
            }
        }
        
        public static MessageContentAttachement GetMessageContentAttachement(int messageContentAttachementID)
        {            
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                return messageContentAttachementDAO.GetMessageContentAttachement(messageContentAttachementID);                
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
                throw new ApplicationException(SR.BusinessGetMessageContentAttachementException, ex);
            }
        }                

        public static MessageContentAttachementCollection GetMessageContentAttachementList(MessageContentAttachementColumns orderBy, string orderDirection)
        {            
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                return messageContentAttachementDAO.GetMessageContentAttachementList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentAttachementListException, ex);
            }
        }        

        public static MessageContentAttachementCollection GetMessageContentAttachementList(MessageContentAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                return messageContentAttachementDAO.GetMessageContentAttachementList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentAttachementListException, ex);
            }
        }

        public static MessageContentAttachementCollection GetMessageContentAttachementList(Int32 messageContentID, MessageContentAttachementColumns orderBy, string orderDirection)
        {
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                return messageContentAttachementDAO.GetMessageContentAttachementList(messageContentID, orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentAttachementListException, ex);
            }
        }

        public static MessageContentAttachementCollection GetMessageContentAttachementList(Int32 messageContentID, MessageContentAttachementColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                return messageContentAttachementDAO.GetMessageContentAttachementList(messageContentID, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentAttachementListException, ex);
            }
        }        

        #endregion
        public static MessageContentAttachementCollection ExistsMessageContentForMessageContentAttachement(int messageContentId)
        {
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                return messageContentAttachementDAO.ExistsMessageContentForMessageContentAttachement(messageContentId);
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
                throw new ApplicationException(SR.BusinessGetMessageContentAttachementListException, ex);
            }
        }
        public static void DeleteMessageContentAttachementByMessageContent(int messageContentID)
        {
            try
            {
                MessageContentAttachementDAO messageContentAttachementDAO = new MessageContentAttachementDAO();
                messageContentAttachementDAO.DeleteMessageContentAttachementByMessageContent(messageContentID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentAttachementException, ex);
            }
        }
    }
}