using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using VfsInformationFeedService.Configuration;
using System.Configuration;

using VfsCustomerService.Entities;
using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Utility;

namespace VfsInformationFeedService
{
    public class SendEmailSession : SendMessage
    {
        public SendEmailSession(int commandBlockSize) : base(commandBlockSize)
        {
            Initialize();
        }

        public SendEmailSession(int commandBlockSize, string userName, string password): base(commandBlockSize, userName, password)
        {                      
            Initialize();
        }

        private int SendAnEmail(MessageContent emailMessage)
        {
            int result;
            try
            {                  
                VfsCustomerService.Business.SendTemplateEmailWithParam sendTemplateEmailWithParam = new SendTemplateEmailWithParam();                
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
                emailMessagesCollection = MessageContentService.GetMessageContentList((Int32)EmailCommandStatus.NotStart, (int)MessageServiceType.Email, MessageContentColumns.CreatedDate, "DESC", 1, commandBlockSize, out totalRow);
                Ultility.Info("Process " + totalRow.ToString() + " email(s)");
                //while (emailMessagesCollection.Count > 0)
                //{
                    foreach (MessageContent email in emailMessagesCollection)
                    {                       
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
                        }
                        else
                        {
                            email.Status = (int)EmailCommandStatus.FailAndFinish;
                            MessageContentService.UpdateMessageContent(email);                            
                        }
                        numberOfEmails++;
                    }
                  //  emailMessagesCollection = MessageContentService.GetMessageContentList((Int32)EmailCommandStatus.NotStart, (int)MessageServiceType.Email, MessageContentColumns.CreatedDate, "DESC", 1, commandBlockSize, out totalRow);
                //};
                finishSendEmailTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                listError.Add(ex);
            }
        }              
    }

    public class SendSMSSession : SendMessage
    {

        public SendSMSSession(int commandBlockSize) : base(commandBlockSize)
        {
            Initialize();
        }

        public SendSMSSession(int commandBlockSize, string userName, string password): base(commandBlockSize, userName, password)
        {            
            Initialize();
        }

        public int SendAnSMS(MessageContent emailMessage)
        {
            int result = 0;
            try
            {
                if(emailMessage.BodyMessage.Length <= 160)
                {
                    VfsCustomerService.Business.SendSMS sendSMS = new SendSMS(this.userName, this.password);
                    result = sendSMS.Send(emailMessage);               
                }
                else
                {
                    VfsCustomerService.Business.SendSMS sendSMS;
                    string FirstMessage = Ultility.CutAddressHead(emailMessage.BodyMessage);
                    string SecondMessage = Ultility.CutAddressEnd(emailMessage.BodyMessage);
                    emailMessage.BodyMessage = FirstMessage;
                    sendSMS = new SendSMS(this.userName, this.password);
                    result = sendSMS.Send(emailMessage);

                    emailMessage.BodyMessage = SecondMessage;
                    sendSMS = new SendSMS(this.userName, this.password);
                    result = sendSMS.Send(emailMessage);    
                }
            }
            catch (Exception ex)
            {
                SendEmailException sendEmailException = new SendEmailException(emailMessage, ex);
                listError.Add(sendEmailException);                
            }
            return result;
        }

        public int SendAnSMS()
        {
            int result = 0;
            //try
            //{
            //    MTSpam.MTSender mTSender = new VfsInformationFeedService.MTSpam.MTSender();
            //    if (mTSender.doSendMTSPAM("0989023010", "test", this.userName, this.password) == 1)
            //    {

            //        result = (int)EmailCommandStatus.SuccessAndFinish;
            //    }
            //    else
            //    {
            //        result = (int)EmailCommandStatus.FailAndFinish;
            //    }
            //}
            //catch (Exception ex)
            //{
            //   // SendEmailException sendEmailException = new SendEmailException(emailMessage, ex);
            //    //listError.Add(sendEmailException);
            //    result = (int)EmailCommandStatus.FailAndFinish;
            //}
            return result;
        }

        public void SendAllSMSNotStart()
        {
            MessageContentCollection emailMessagesCollection = null;
            Int32 totalRow = 0;
            Int32 result;
            try
            {              
                emailMessagesCollection = MessageContentService.GetMessageContentList((Int32)EmailCommandStatus.NotStart, (int)MessageServiceType.Sms, MessageContentColumns.CreatedDate, "DESC", 1, commandBlockSize, out totalRow);
                Ultility.Info("Process " + totalRow.ToString() + " SMS message(s)" );
                //while (emailMessagesCollection.Count > 0)
                //{
                    foreach (MessageContent email in emailMessagesCollection)
                    {
                        result = SendAnSMS(email);
                        if (result != (int)SMSCommandStatus.SuccessAndFinish)
                        {
                            email.Status = (int)EmailCommandStatus.FailAndFinish;
                            MessageContentService.UpdateMessageContent(email);
                            log4net.Util.LogLog.Error("Error sending SMS with error code: " + result.ToString());
                           // emailCommandService.UpdateStatusEmailCommand(email.EmailMessageId, (int)EmailCommandStatus.FailAndFinish, DateTime.Now);
                        }
                        else
                        {
                            log4net.Util.LogLog.Error("Successfully sending SMS with no errore:");
                        }
                        numberOfEmails++;
                    }
                  //  emailMessagesCollection = MessageContentService.GetMessageContentList((Int32)EmailCommandStatus.NotStart, (int)MessageServiceType.Sms, MessageContentColumns.CreatedDate, "DESC", 1, commandBlockSize, out totalRow);
               // };
                //finishSendEmailTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                listError.Add(ex);
            }
        }
    }
}
