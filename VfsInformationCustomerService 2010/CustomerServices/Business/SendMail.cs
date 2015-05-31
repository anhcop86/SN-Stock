using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Collections.Specialized;
using System.IO;
using System.Net;

using VfsCustomerService.Data;
using VfsCustomerService.Entities;
 

namespace VfsCustomerService.Business
{

    public enum SMSCommandStatus
    {
        ErrorOnMessage = 0, //0
        SuccessAndFinish = 1, //1
        InvalidUserNameOrPass = -1,
        InvalidMessageContent = -2,
        InvalidPhoneNumber = -3,
        ExcessNumberOfMessagesPerday = -4,
        Other = -5
    }

    public class SendEmail
    {
        protected string sender = string.Empty;
        protected string senderName = string.Empty;
        protected string websiteUrl = string.Empty;
        protected string cc = string.Empty;
        protected string receiver = string.Empty;
        protected string subject = string.Empty;
        protected string receiverName = string.Empty;
        protected string bodyText = string.Empty;
        protected string fileName = string.Empty;
        protected string smtpServer = string.Empty;
        protected int smtpPort = 25;
        protected bool isHtmlMail = true;
        protected bool useContentTemplate = false;

        public string Sender
        {
            set { this.sender = value; }
            get { return this.sender; }
        }

        public string SenderName
        {
            set { this.senderName = value; }
            get { return this.senderName; }
        }

        public string WebsiteUrl
        {
            set { this.websiteUrl = value; }
            get { return this.websiteUrl; }
        }

        public string Cc
        {
            set { this.cc = value; }
            get { return this.cc; }
        }

        public string Receiver
        {
            set { this.receiver = value; }
            get { return this.receiver; }
        }

        public string Subject
        {
            set { this.subject = value; }
            get { return this.subject; }
        }

        public string ReceiverName
        {
            set { this.receiverName = value; }
            get { return this.receiverName; }
        }

        public string FileName
        {
            set { this.fileName = value; }
            get { return this.fileName; }
        }

        public bool IsHtmlMail
        {
            set { this.isHtmlMail = value; }
            get { return this.isHtmlMail; }
        }

        public string SmtpServer
        {
            set { this.smtpServer = value; }
            get { return this.smtpServer; }
        }

        public int SmtpPort
        {
            set { this.smtpPort = value; }
            get { return this.smtpPort; }
        }

        public string BodyText
        {
            set { this.bodyText = value; }
            get { return this.bodyText; }
        }

        public bool UseContentTemplate
        {
            set { this.useContentTemplate = value; }
            get { return this.useContentTemplate; }
        }
        public SendEmail() { }

        public void Send()
        {
            this.GetParamaters();
            MailMessage message = new MailMessage();
            //message.Bcc = this.bcc;
            //message.Cc = this.cc;
            MailAddress sender = new MailAddress(this.sender);
            MailAddress receiver = new MailAddress(this.receiver);
            
            message.From = sender;
            message.Sender = sender;
            message.To.Add(receiver);
            message.Subject = this.subject;
            message.IsBodyHtml = this.isHtmlMail;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = this.bodyText;
            message.Priority = MailPriority.High;

            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = this.smtpPort;
            smtpClient.Host = this.smtpServer;
            smtpClient.UseDefaultCredentials = true;

            smtpClient.Credentials = new NetworkCredential(ContentParameterService.GetContentParameter(1).ContentParameterValue, ContentParameterService.GetContentParameter(2).ContentParameterValue);
            smtpClient.Send(message);
        }

        protected virtual void GetParamaters()
        {
            NameValueCollection parameters = new NameValueCollection();
            StreamReader reader = null;

            if (useContentTemplate == true)
            {
                reader = new StreamReader(this.fileName, System.Text.UTF8Encoding.UTF8);
                this.bodyText = reader.ReadToEnd();
                reader.Close();

                parameters.Add("SenderName", this.senderName);
                parameters.Add("WebsiteUrl", this.websiteUrl);

                for (int i = 0; i < parameters.Keys.Count; i++)
                {
                    string replaceKey = "${" + parameters.GetKey(i) + "}";
                    this.bodyText = this.bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
                }
            }
        }
    }

    public class SendTemplateEmailWithParam : SendEmail
    {
        public SendTemplateEmailWithParam(): base() {}

        public void Send(MessageContent messageContent)
        {
            this.GetParamaters();
            Int32 totalRow;
            Int32 result;
            bool success = false;
            MailMessage message = new MailMessage();
            //message.Bcc = this.bcc;
            //message.Cc = this.cc;
            MailAddress sender = new MailAddress(messageContent.Sender);
            MailAddress receiver = new MailAddress(messageContent.Receiver);

            MessageContentAttachementCollection messageContentAttachementCollection = null;

            messageContentAttachementCollection = MessageContentAttachementService.GetMessageContentAttachementList(messageContent.MessageContentID, MessageContentAttachementColumns.ModifiedDate, "DESC" ,1 , 10000, out totalRow);
            log4net.Util.LogLog.Error(totalRow.ToString());
            foreach (MessageContentAttachement messAttach in messageContentAttachementCollection)
            {
                Attachment item = new Attachment(ContentParameterService.GetContentParameter(3).ContentParameterValue + messAttach.AttachementDocument);
                message.Attachments.Add(item);
            }                     

            message.From = sender;
            message.Sender = sender;
            message.To.Add(receiver);
            message.Subject = messageContent.Subject;
            message.IsBodyHtml = this.isHtmlMail;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = messageContent.BodyMessage;
            message.Priority = MailPriority.High;            
            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();            
            success = Int32.TryParse(ContentParameterService.GetContentParameter(5).ContentParameterValue, out result);
            
            if (success == true)
            {
               smtpClient.Port = result;
            }

            smtpClient.Host = ContentParameterService.GetContentParameter(4).ContentParameterValue;
            smtpClient.UseDefaultCredentials = true;

            smtpClient.Credentials = new NetworkCredential(ContentParameterService.GetContentParameter(1).ContentParameterValue, ContentParameterService.GetContentParameter(2).ContentParameterValue);                      
            smtpClient.Send(message);

            foreach (MessageContentAttachement messAttach in messageContentAttachementCollection)
            {
                MessageContentAttachementService.DeleteMessageContentAttachement(messAttach.MessageContentAttachementID);
            }
            MessageContentService.DeleteMessageContent(messageContent.MessageContentID);
            //MessageContentService.DeleteMessageContentAndAttachement(messageContent.MessageContentID);
        }

        public void Send(ContentTemplate contentTemplate)
        {
            this.GetParamaters();
            Int32 totalRow;
            Int32 result;
            bool success = false;
            MailMessage message = new MailMessage();

            MessageContent messageContent = new MessageContent();
            messageContent.BodyContentType = contentTemplate.BodyContentType;
            messageContent.BodyEncoding = contentTemplate.BodyEncoding;
            messageContent.BodyMessage = contentTemplate.BodyMessage;
            messageContent.ContentTemplateID = contentTemplate.ContentTemplateID;
            messageContent.CreatedDate = DateTime.Now;
            messageContent.ModifiedDate = DateTime.Now;
            messageContent.Receiver = contentTemplate.Receiver;
            messageContent.Sender = contentTemplate.Sender;
            messageContent.ServiceTypeID = contentTemplate.ServiceTypeID;
            messageContent.Subject = contentTemplate.Subject;

            MailAddress sender = new MailAddress(messageContent.Sender);
            MailAddress receiver = new MailAddress(messageContent.Receiver);


            ContentTemplateAttachementCollection contentTemplateAttachementCollection = null;

            contentTemplateAttachementCollection = ContentTemplateAttachementService.GetContentTemplateAttachementList(contentTemplate.ContentTemplateID, ContentTemplateAttachementColumns.ModifiedDate, "DESC", 1, 10000, out totalRow);

            foreach (ContentTemplateAttachement messAttach in contentTemplateAttachementCollection)
            {
                Attachment item = new Attachment(ContentParameterService.GetContentParameter(3).ContentParameterValue + messAttach.AttachementDocument);
                message.Attachments.Add(item);
            }

            message.From = sender;
            message.Sender = sender;
            message.To.Add(receiver);
            message.Subject = messageContent.Subject;
            message.IsBodyHtml = this.isHtmlMail;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = messageContent.BodyMessage;
            message.Priority = MailPriority.High;
            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
            success = Int32.TryParse(ContentParameterService.GetContentParameter(5).ContentParameterValue, out result);

            if (success == true)
            {
                smtpClient.Port = result;
            }

            smtpClient.Host = ContentParameterService.GetContentParameter(4).ContentParameterValue;
            smtpClient.UseDefaultCredentials = true;

            smtpClient.Credentials = new NetworkCredential(ContentParameterService.GetContentParameter(1).ContentParameterValue, ContentParameterService.GetContentParameter(2).ContentParameterValue);
            smtpClient.Send(message);
        }        
    }

    public class SendSMS : SendEmail
    {
        protected string userName;
        protected string password;

        public string UserName
        {
            set { this.userName = value; }
            get { return userName; }
        }

        public string Password
        {
            set { this.password = value; }
            get { return password; }
        }

        public SendSMS() : base() { }

        public SendSMS(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public int Send(MessageContent messageContent)
        {
            int returnValue;
            MTSpam.MTSender mTSender = new MTSpam.MTSender();
            if(messageContent.MoID != string.Empty)
                returnValue = mTSender.doSendMT(messageContent.Receiver, messageContent.BodyMessage, messageContent.ServiceID, messageContent.CommandCode, "0", messageContent.Request, "1", "1", "0", "0", Convert.ToInt64(messageContent.MoID), this.userName, this.password);
            else
                returnValue = mTSender.doSendMTSPAM(messageContent.Receiver, messageContent.BodyMessage, this.userName, this.password);

            if(returnValue == (int)SMSCommandStatus.SuccessAndFinish)
                MessageContentService.DeleteMessageContent(messageContent.MessageContentID);
            return returnValue;
        }

        public int Send(MessageContent messageContent, bool checkSendType)
        {
            int returnValue;
            MTSpam.MTSender mTSender = new MTSpam.MTSender();
            if (checkSendType == false)
            {
                returnValue = mTSender.doSendMTSPAM(messageContent.Receiver, messageContent.BodyMessage, this.userName, this.password);
            }
            else
            {
                if (messageContent.Request != string.Empty)
                    returnValue = mTSender.doSendMT(messageContent.Receiver, messageContent.BodyMessage, messageContent.ServiceID, messageContent.CommandCode, "0", messageContent.Request, "1", "1", "1", "0", Convert.ToInt64(messageContent.MoID), this.userName, this.password);
                else
                    returnValue = mTSender.doSendMTSPAM(messageContent.Receiver, messageContent.BodyMessage, this.userName, this.password);
            }
            if (returnValue == (int)SMSCommandStatus.SuccessAndFinish)
                MessageContentService.DeleteMessageContent(messageContent.MessageContentID);
            return returnValue;
        }
    }

    /*public class SendtoFriendEmail : SendEmail
    {
        protected Int32 propertyID = 0;
        protected string message = string.Empty;

        public Int32 PropertyId
        {
            set
            {
                this.propertyID = value;
            }
            get
            {
                return this.propertyID;
            }
            
        }

        public string Message
        {
            set
            {
                this.message = value;
            }
            get 
            {
                return this.message;
            }
        }

        protected override void GetParamaters()
        {
            NameValueCollection parameters = new NameValueCollection();
            StreamReader reader = null;

            if (useContentTemplate == true)
            {
                reader = new StreamReader(this.fileName, System.Text.UTF8Encoding.UTF8);
                this.bodyText = reader.ReadToEnd();
                reader.Close();

                parameters.Add("SenderName", this.senderName);
                parameters.Add("ReceiverName", this.ReceiverName);
                parameters.Add("WebsiteUrl", this.WebsiteUrl);
                parameters.Add("Message", this.message);

                for (int i = 0; i < parameters.Keys.Count; i++)
                {
                    string replaceKey = "${" + parameters.GetKey(i) + "}";
                    this.bodyText = this.bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
                }
            }
        }
    }*/

}
