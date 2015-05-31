
using System;
using System.Collections;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
    
namespace VfsCustomerService.Business
{   
	/// <summary>
	/// 
	/// </summary> 
    public class MessageContentSentService
    {
        #region MessageContentSent

        public static void CreateMessageContentSent(MessageContentSent messageContentSent)
        {            
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                messageContentSentDAO.CreateMessageContentSent(messageContentSent);
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
                throw new ApplicationException(SR.BusinessCreateMessageContentSentException, ex);
            }
        }        

        public static void UpdateMessageContentSent(MessageContentSent messageContentSent)
        {            
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                messageContentSentDAO.UpdateMessageContentSent(messageContentSent);
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
                throw new ApplicationException(SR.BusinessUpdateMessageContentSentException, ex);
            }
        }        

        public static void DeleteMessageContentSent(int messageContentID)
        {            
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                messageContentSentDAO.DeleteMessageContentSent(messageContentID);
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
                throw new ApplicationException(SR.BusinessDeleteMessageContentSentException, ex);
            }
        }
        
        public static MessageContentSent GetMessageContentSent(int messageContentID)
        {            
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.GetMessageContentSent(messageContentID);                
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentException, ex);
            }
        }                

        public static MessageContentSentCollection GetMessageContentSentList(MessageContentSentColumns orderBy, string orderDirection)
        {            
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.GetMessageContentSentList(orderBy, orderDirection);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentListException, ex);
            }
        }        

        public static MessageContentSentCollection GetMessageContentSentList(MessageContentSentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {            
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.GetMessageContentSentList(orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentListException, ex);
            }
        }        

        #endregion
        public static MessageContentSentCollection ExistsServiceTypeForMessageContentSent(int serviceTypeID)
        {
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.ExistsServiceTypeForMessageContentSent(serviceTypeID);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentListException, ex);
            }
        }
        public static MessageContentSentCollection ExistsMessageContentForMessageContentSent(int MessageContentID)
        {
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.ExistsMessageContentForMessageContentSent(MessageContentID);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentListException, ex);
            }
        }
        public static MessageContentSentCollection ExistsContentTemplateForMessageContentSent(int ContentTemplateID)
        {
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.ExistsContentTemplateForMessageContentSent(ContentTemplateID);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentListException, ex);
            }
        }



        #region Extention
        public static MessageContentSentCollection MessageContentSentGetListFilterByServiceTypeID(int ServiceTypeId, string Sender, string Receiver, DateTime fromDate,DateTime toDate, MessageContentSentColumns orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            try
            {
                MessageContentSentDAO messageContentSentDAO = new MessageContentSentDAO();
                return messageContentSentDAO.MessageContentSentGetListFilterByServiceTypeID(ServiceTypeId, Sender, Receiver, fromDate, toDate, orderBy, orderDirection, page, pageSize, out totalRecords);
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
                throw new ApplicationException(SR.BusinessGetMessageContentSentListException, ex);
            }
        }     
        #endregion

    }
}