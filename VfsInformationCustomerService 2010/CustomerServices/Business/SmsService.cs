using System;
using System.Collections.Generic;
using System.Text;
using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;
using VfsCustomerService.Utility;
using System.IO;

namespace VfsCustomerService.Business
{
    public class SmsService
    {
        public static string ReceviveIncomingMessage(string UserID, 
                                                    string ServiceID, 
                                                    string CommandCode, 
                                                    string Message, 
                                                    string RequestID, 
                                                    string MoID, 
                                                    string Username, 
                                                    string Password, 
                                                    Int32 transferMoneyContentTemplateId, 
                                                    string email, 
                                                    Int32 invalidSMSContentTemplateId, 
                                                    Int32 ReplyRejectRelatedMessageContentTemplateId, 
                                                    Int32 invalidAccountSMSContentTemplateId)
        {
            string returnValue = "1";
            //if (Username.ToUpper() != VfsCustomerService.Utility.Encyption.DoMD5("vfsuser").ToUpper() || Password.ToUpper() != VfsCustomerService.Utility.Encyption.DoMD5("vfsuser2009sms353").ToUpper())
            if (Username.ToLower() != "vfsuser" || Password.ToLower() != "vfsuser2009sms353")            
            {
                throw new ApplicationException(SR.BusinessSendSMSToCoreSecuritiesException);
            }

            string serviceCode = ServiceCodePraser(Message);
            try
            {
                switch (serviceCode)
                {
                    case "DK100":
                        returnValue = registerPhiGiaoDich(Message, UserID);
                        break;
                    case "CT":
                        returnValue = SendMessage(UserID, ServiceID, CommandCode, Message, RequestID, MoID, Username, Password, transferMoneyContentTemplateId, email);
                        break;
                    case "KQ":
                        returnValue = ForwardMessage2CoreSecurity(UserID, ServiceID, CommandCode, Message, RequestID, MoID, "vfsuser", "vfsuser2009sms353");
                        break;
                    case "SD":
                        returnValue = ForwardMessage2CoreSecurity(UserID, ServiceID, CommandCode, Message, RequestID, MoID, "vfsuser", "vfsuser2009sms353");
                        break;
                    case "TC":
                        returnValue = RejectMessagePraser(UserID, ServiceID, CommandCode, Message, RequestID, MoID, "vfsuser", "vfsuser2009sms353", transferMoneyContentTemplateId, email, invalidSMSContentTemplateId, ReplyRejectRelatedMessageContentTemplateId, invalidAccountSMSContentTemplateId);
                        break;
                    default:
                        returnValue = SendInValidResponseMessage(UserID, ServiceID, CommandCode, Message, RequestID, MoID, Username, Password, invalidSMSContentTemplateId);
                        break;
                }
            }
            catch (Exception ex)
            {
                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                //throw new ApplicationException(SR.BusinessSendSMSToCoreSecuritiesException, ex);
                return "-1";
            }
            return returnValue;
        }

        private static string registerPhiGiaoDich(string Message, string userId)
        {
            //string[] messagePrased;
            //messagePrased = Message.Normalize().Split(' ');
            //string mobilenumber = messagePrased[2];

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                FileStream fs = new FileStream("D:\\lognew.txt", FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine(DateTime.Now.ToString() + "-" + userId);
                writer.Flush();
                writer.Close();
                fs.Close();
            }
            catch
            {
            }
            return "1";

        }

        private static string registerPhiGiaoDich()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public static string SendMessage(string UserID, string ServiceID, string CommandCode, string Message, string RequestID, string MoID, string Username, string Password, Int32 transferMoneyContentTemplateId, string email)
        {
            string returnValue = "1";
            int result;
            try
            {
                IncomingMessageContent incomingMessageContent = new IncomingMessageContent();
                incomingMessageContent.ServiceTypeID = 2;
                incomingMessageContent.CommandCode = CommandCode;
                incomingMessageContent.ServiceID = ServiceID;
                incomingMessageContent.Sender = UserID;
                incomingMessageContent.MoID = MoID;
                incomingMessageContent.CreatedDate = DateTime.Now;
                incomingMessageContent.ModifiedDate = DateTime.Now;
                incomingMessageContent.BodyMessage = Message;
                incomingMessageContent.Request = RequestID;
                incomingMessageContent.BodyContentType = "PlainText";
                incomingMessageContent.BodyEncoding = "UTF-8";
                incomingMessageContent.Status = 0;

                IncomingMessageContentService.CreateIncomingMessageContent(incomingMessageContent);

                SendEmail sendMail = new SendEmail();
                sendMail.Sender = ContentParameterService.GetContentParameter(1).ContentParameterValue;
                sendMail.Receiver = email;
                sendMail.Subject = "Dich Vu chuyen tien:" + UserID;
                sendMail.SmtpServer = ContentParameterService.GetContentParameter(4).ContentParameterValue;
                sendMail.IsHtmlMail = true;
                sendMail.BodyText = "Yeu cau thuc hien dich vu chuyen tien cua: " + UserID + "<br />" + Message;

                bool success = Int32.TryParse(ContentParameterService.GetContentParameter(5).ContentParameterValue, out result);

                if (success == true)
                {
                    sendMail.SmtpPort = result;
                }

                sendMail.Send();
                ContentTemplate contentTemplate = null;
                contentTemplate = ContentTemplateService.GetContentTemplate(transferMoneyContentTemplateId);
                MessageContent messageContent = null;

                if (contentTemplate != null)
                {
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    messageContent.Receiver = UserID;
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = contentTemplate.BodyMessage;
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;
                    messageContent.Request = RequestID;
                    messageContent.MoID = MoID;
                    messageContent.ServiceID = ServiceID;
                    messageContent.CommandCode = CommandCode;
                }

                MessageContentService.CreateMessageContent(messageContent);
                return returnValue;
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
                throw new ApplicationException(SR.BusinessSendSMSToCoreSecuritiesException, ex);
            }
        }

        public static string RejectRelatedSmsMessage(string UserID, string ServiceID, string CommandCode, string Message, string RequestID, string MoID, string Username, string Password, Int32 invalidSMSContentTemplateId, Int32 replyRejectRelatedMessageContentTemplateId, Int32 invalidAccountSMSContentTemplateId)
        {
            string returnValue = "1";
            int result;
            string[] messagePrased;
            ContentTemplate contentTemplate = null;
            try
            {
                messagePrased = Message.Normalize().Split(' ');
                if (messagePrased.Length == 4)
                {
                    Customer customer = null;
                    customer = CustomerService.GetCustomer(messagePrased[3]);
                    if (customer != null)
                    {
                        customer.Mobile =  "84" + customer.Mobile.Substring(1); 
                        if (customer.Mobile.ToString().Trim().Equals(UserID.ToString().Trim()))
                        {
                            customer.ReceiveRelatedStockSms = "N";
                            CustomerService.UpdateCustomer(customer);
                            contentTemplate = ContentTemplateService.GetContentTemplate(replyRejectRelatedMessageContentTemplateId);
                            MessageContent messageContent = null;

                            if (contentTemplate != null)
                            {
                                messageContent = new MessageContent();
                                messageContent.Sender = contentTemplate.Sender;
                                messageContent.Receiver = UserID;
                                messageContent.Subject = contentTemplate.Subject;
                                messageContent.BodyContentType = contentTemplate.BodyContentType;
                                messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                                messageContent.BodyMessage = contentTemplate.BodyMessage;
                                messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                                messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                                messageContent.ModifiedDate = DateTime.Now;
                                messageContent.CreatedDate = DateTime.Now;
                                messageContent.Status = 0;
                                messageContent.Request = RequestID;
                                messageContent.MoID = MoID;
                                messageContent.ServiceID = ServiceID;
                                messageContent.CommandCode = CommandCode;
                            }
                            MessageContentService.CreateMessageContent(messageContent);
                        }
                        else
                        {
                            SendInValidResponseMessage(UserID, ServiceID, CommandCode, Message, RequestID, MoID, Username, Password, invalidAccountSMSContentTemplateId);
                        }
                    }
                    else
                    {
                        SendInValidResponseMessage(UserID, ServiceID, CommandCode, Message, RequestID, MoID, Username, Password, invalidSMSContentTemplateId);
                    }
                }
            }
            catch(Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                //throw new ApplicationException(SR.BusinessSendSMSToCoreSecuritiesException, ex);
                return "-1";
            }
            return returnValue;
        }

        public static string ForwardMessage2CoreSecurity(string UserID, string ServiceID, string CommandCode, string Message, string RequestID, string MoID, string Username, string Password)
        {
            string returnValue = "-1";
            try
            {
                CoreSecuritiesSms.MOReceiver mOReceiver = new CoreSecuritiesSms.MOReceiver();
                returnValue = mOReceiver.messageReceiver(UserID, ServiceID, CommandCode, Message, RequestID, MoID, Username, Password);
                return returnValue;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {                // log this exception
                log4net.Util.LogLog.Error(ex.Message, ex);
                // wrap it and rethrow
                throw new ApplicationException(SR.BusinessSendSMSToCoreSecuritiesException, ex);
            }
        }

        public static string SendInValidResponseMessage(string UserID, string ServiceID, string CommandCode, string Message, string RequestID, string MoID, string Username, string Password, Int32 invalidSMSContentTemplateId)
        {
            string returnValue = "1";
          //  int result;
            try
            {
                IncomingMessageContent incomingMessageContent = new IncomingMessageContent();
                incomingMessageContent.ServiceTypeID = 2;
                incomingMessageContent.CommandCode = CommandCode;
                incomingMessageContent.ServiceID = ServiceID;
                incomingMessageContent.Sender = UserID;
                incomingMessageContent.MoID = MoID;
                incomingMessageContent.CreatedDate = DateTime.Now;
                incomingMessageContent.ModifiedDate = DateTime.Now;
                incomingMessageContent.BodyMessage = Message;
                incomingMessageContent.Request = RequestID;
                incomingMessageContent.BodyContentType = "PlainText";
                incomingMessageContent.BodyEncoding = "UTF-8";
                incomingMessageContent.Status = 0;

                IncomingMessageContentService.CreateIncomingMessageContent(incomingMessageContent);
                ContentTemplate contentTemplate = null;
                contentTemplate = ContentTemplateService.GetContentTemplate(invalidSMSContentTemplateId);
                MessageContent messageContent = null;

                if (contentTemplate != null)
                {
                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    messageContent.Receiver = UserID;
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = contentTemplate.BodyMessage;
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;
                    messageContent.Request = RequestID;
                    messageContent.MoID = MoID;
                    messageContent.ServiceID = ServiceID;
                    messageContent.CommandCode = CommandCode;
                }

                MessageContentService.CreateMessageContent(messageContent);
                return returnValue;
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
                throw new ApplicationException(SR.BusinessSendSMSToCoreSecuritiesException, ex);
            }
        }

        public static void SendBirthdaySmsMessage(Int32 birthdaySmsMessageTemplateId)
        {
            ContentTemplate contentTemplate = null;

            if (DateTime.Now.Hour < 8 && DateTime.Now.Hour > 17) return;
            if (BirthdayMessageLogService.GetBirthdayMessageLog(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0')) != null) return;
            
            CustomerCollection customerCollection = CustomerService.GetCustomerListByBirthDay();
            contentTemplate = ContentTemplateService.GetContentTemplate(birthdaySmsMessageTemplateId);
            if (contentTemplate != null)
            {
                foreach (Customer cust in customerCollection)
                {
                    MessageContent messageContent = null;

                    messageContent = new MessageContent();
                    messageContent.Sender = contentTemplate.Sender;
                    messageContent.Receiver = cust.Mobile;
                    messageContent.Subject = contentTemplate.Subject;
                    messageContent.BodyContentType = contentTemplate.BodyContentType;
                    messageContent.BodyEncoding = contentTemplate.BodyEncoding;
                    messageContent.BodyMessage = contentTemplate.BodyMessage;
                    messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
                    messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
                    messageContent.ModifiedDate = DateTime.Now;
                    messageContent.CreatedDate = DateTime.Now;
                    messageContent.Status = 0;

                    MessageContentService.CreateMessageContent(messageContent);
                }
                BirthdayMessageLog birthdayMessageLog = new BirthdayMessageLog();
                birthdayMessageLog.BirthdayMessageDay = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2,'0') + DateTime.Now.Day.ToString().PadLeft(2,'0');;
                birthdayMessageLog.ProccessYN = "Y";
                BirthdayMessageLogService.CreateBirthdayMessageLog(birthdayMessageLog);
            }
        }

        public static string ServiceCodePraser(String messageContent)
        {
            string returnvalue = "void";
            string[] messageContentArray;
            if (messageContent != String.Empty)
            {
                messageContent = HtmlRemoval.NormalizeWhitespace(messageContent.TrimEnd().TrimStart());
                messageContentArray = messageContent.Split(' ');
                returnvalue = messageContentArray[1];
            }
            return returnvalue.ToUpper();
        }

        public static string RejectMessagePraser(string UserID, string ServiceID, string CommandCode, string Message, string RequestID, string MoID, string Username, string Password, Int32 transferMoneyContentTemplateId, string email, Int32 invalidSMSContentTemplateId, Int32 replyRejectRelatedMessageContentTemplateId, Int32 invalidAccountSMSContentTemplateId)
        {
            string returnValue = "-1";
            string[] messagePrased;
            messagePrased = Message.Normalize().Split(' ');
            if (messagePrased[2] == "TINDMDT")
            {
                returnValue = RejectRelatedSmsMessage(UserID, ServiceID, CommandCode, Message, RequestID, MoID, "vfsuser", "vfsuser2009sms353", invalidSMSContentTemplateId, replyRejectRelatedMessageContentTemplateId, invalidAccountSMSContentTemplateId);
            }
            else
            {
                returnValue = SendInValidResponseMessage(UserID, ServiceID, CommandCode, Message, RequestID, MoID, Username, Password, invalidSMSContentTemplateId);
            }

            return returnValue;            
        }

        public static string[] MessagePraser(String messageContent)
        {
            string[] messagePrased;
            messagePrased = messageContent.Normalize().Split(' ');
            return messagePrased;
        }

    }
}
