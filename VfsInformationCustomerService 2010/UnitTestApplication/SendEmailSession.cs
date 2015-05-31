using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
//using VfsInformationFeedService.Configuration;

using VfsCustomerService.Entities;
using VfsCustomerService.Business;
using VfsCustomerService.Data;

namespace VfsInformationFeedService
{
    public class SendEmailSession
    {
        private IList<Exception> listError;
        private DateTime startSendEmailTime;
        private DateTime finishSendEmailTime;
        private int numberOfEmails;
                
        private int commandBlockSize;
                
        private enum EmailCommandStatus
        {
            NotStart,
            SuccessAndFinish,
            FailAndFinish
        }

        #region Property
        public DateTime StartSendEmailTime
        {
            get { return startSendEmailTime; }
        }

        public DateTime FinishSendEmailTime
        {
            get { return finishSendEmailTime; }
        }

        public IList<Exception> ListError
        {
            get { return listError; }
        }

        public int NumberOfEmails
        {
            get { return numberOfEmails; }
        }                

        public int CommandBlockSize
        {
            get { return commandBlockSize; }
        }
        #endregion

        public SendEmailSession(int commandBlockSize)
        {            
            this.commandBlockSize = commandBlockSize;

            Initialize();
        }

        private int SendAnEmail(MessageContent emailMessage)
        {
            int result;
            try
            {
               // EmailMessageService.EmailMessageWS emailMessageService = new EmailMessageService.EmailMessageWS();
                //emailMessageService.SendAnEmail(emailMessage);
                VfsCustomerService.Business.SendTemplateEmailWithParam sendTemplateEmailWithParam = new SendTemplateEmailWithParam();
                sendTemplateEmailWithParam.SmtpServer = "mail.vfs.com.vn";
                sendTemplateEmailWithParam.SmtpPort = 25;

                sendTemplateEmailWithParam.Send(emailMessage);
                
                result = (int)EmailCommandStatus.SuccessAndFinish;
            }
            catch (Exception ex)
            {
                SendEmailException sendEmailException = new SendEmailException(emailMessage, ex);
                listError.Add(sendEmailException);                
                result = (int)EmailCommandStatus.FailAndFinish;
            }
            return result;
        }

        public void SendAllMailNotStart()
        {                           
            MessageContentCollection emailMessagesCollection = null;
            Int32 totalRow = 0;
            try
            {                
                //EmailMessageService.EmailMessageWS emailMessageService = new EmailMessageService.EmailMessageWS();
                emailMessagesCollection = MessageContentService.GetMessageContentList((Int32)EmailCommandStatus.NotStart, MessageContentColumns.CreatedDate, "DESC", 1, commandBlockSize, out totalRow);
                while (emailMessagesCollection.Count > 0)
                {
                    foreach (MessageContent email in emailMessagesCollection)
                    {
                       // EmailCommandService.EmailCommandWS emailCommandService = new EmailCommandService.EmailCommandWS();
                        //SendTemplateEmailWithParam sendTemplateEmailWithParam = new SendTemplateEmailWithParam();
                        if (SendAnEmail(email) == (int)EmailCommandStatus.SuccessAndFinish)
                        {
                            MessageContentSent messageContentSent = new MessageContentSent();
                            messageContentSent.MessageContentID = email.MessageContentID;
                            messageContentSent.ContentTemplateID = email.ContentTemplateID;
                            messageContentSent.BodyContentType = email.BodyContentType;
                            messageContentSent.BodyEncoding = email.BodyEncoding;
                            messageContentSent.BodyMessage = email.BodyMessage;
                            messageContentSent.CreatedDate = email.CreatedDate;
                            messageContentSent.ModifiedDate = email.ModifiedDate;
                            messageContentSent.Receiver = email.Receiver;
                            messageContentSent.Sender = email.Sender;
                            messageContentSent.ServiceTypeID = email.ServiceTypeID;                            
                            messageContentSent.Subject = email.Subject;
                            MessageContentSentService.CreateMessageContentSent(messageContentSent);
                            //emailCommandService.UpdateStatusEmailCommand(email.EmailMessageId, (int)EmailCommandStatus.SuccessAndFinish, DateTime.Now);
                        }
                        else
                        {
                            email.Status = (int)EmailCommandStatus.FailAndFinish;
                            MessageContentService.UpdateMessageContent(email);
                            //emailCommandService.UpdateStatusEmailCommand(email.EmailMessageId, (int)EmailCommandStatus.FailAndFinish, DateTime.Now);
                        }
                        numberOfEmails++;
                    }
                    emailMessagesCollection = MessageContentService.GetMessageContentList((Int32)EmailCommandStatus.NotStart, MessageContentColumns.CreatedDate, "DESC", 0, commandBlockSize, out totalRow);
                };
                finishSendEmailTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                listError.Add(ex);
            }
        }

        private void Initialize()
        {
            listError = new List<Exception>();
            startSendEmailTime = DateTime.Now;
            finishSendEmailTime = DateTime.MaxValue;
            numberOfEmails = 0;
        }                        
    }
}
