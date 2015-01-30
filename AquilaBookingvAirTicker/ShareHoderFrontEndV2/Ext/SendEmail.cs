using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace ShareHoderFrontEndV2.Ext.Utility
{
    public class SendEmail
    {
        protected string sender = string.Empty;
        protected string senderName = string.Empty;
        protected string websiteUrl = string.Empty;
        protected string cc = string.Empty;
        protected string receiver = string.Empty;
        protected string subject = string.Empty;
        protected bool enableSSL = false;
        protected string receiverName = string.Empty;
        protected string bodyText = string.Empty;
        protected string fileName = string.Empty;
        protected string smtpServer = string.Empty;
        protected int    smtpPort = 25;
        protected bool isHtmlMail = true;
        protected bool useContentTemplate = false;
		private string smtpPassword = string.Empty;
		private string smtpEmail = string.Empty;


        public bool EnableSSL
        {
            set { this.enableSSL = value; }
            get { return this.enableSSL; }
        }

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
            set { this.bodyText = value;}
            get { return this.bodyText;}
        }

		public string SmtpPassword
		{
			set { this.smtpPassword = value; }
			get { return this.smtpPassword; }
		}

		public string SmtpEmail
		{
			set { this.smtpEmail = value; }
			get { return this.smtpEmail; }
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
            smtpClient.EnableSsl = this.enableSSL;

			smtpClient.Credentials = new NetworkCredential(this.smtpEmail, this.smtpPassword);
			smtpClient.Send(message);
         }
        

        public void SendEmailWithAttachment(string contentAttachment)
        {
            this.GetParamaters();
            MailMessage message = new MailMessage();
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

            #region add attachment into the template

            //FileStream fs = new FileStream(directoryOfTemplate, FileMode.Open, FileAccess.Read);
            //string content = PDFUtility.getContentXML(directoryOfTemplate);
            //Attachment attachment;
            //attachment = new Attachment(fs, "BookingAquilaConfirm.doc");
            //attachment.Name = "BookingAquilaConfirm.doc";           
            /////////////////////////////////////
            byte[] a = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(contentAttachment);
            System.IO.MemoryStream m = new System.IO.MemoryStream(a);
            Attachment matt = new Attachment(m, "WordDoc.doc");

            message.Attachments.Add(matt); // file đính kèm vào Email
            //end
            #endregion

            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = this.smtpPort;
            smtpClient.Host = this.smtpServer;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = this.enableSSL;

            smtpClient.Credentials = new NetworkCredential(this.smtpEmail, this.smtpPassword);
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
                    string replaceKey = "${" + parameters.GetKey(i) + "}$";
                    this.bodyText = this.bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
                }
            }
        }
    }

    public class SendRegisterPropertyEmail : SendEmail
    {
        protected string address = string.Empty;
        protected string telephone = string.Empty;
        protected string fax = string.Empty;
        protected string name = string.Empty;
        protected string description = string.Empty;
        protected string propertyAddress = string.Empty;
        protected string city = string.Empty;
        protected string location = string.Empty;
        protected string purpose = string.Empty;
        protected string type = string.Empty;
        protected Int32 price = 0;
        protected string currency = string.Empty;
        protected string category = string.Empty;
        protected int bedRoom = 0;
        protected int bathRoom = 0;
        protected string facilities = string.Empty;
        protected string direction = string.Empty;
        protected string startDate = string.Empty;
        protected string nearBy = string.Empty;
        protected string legalDocument = string.Empty;
        protected string image1 = string.Empty;
        protected string image2 = string.Empty;
        protected string image3 = string.Empty;
        protected string image4 = string.Empty;
        protected string legalDocument02 = string.Empty;
        protected string other = string.Empty;
        protected string unit = string.Empty;

        public string Address
        {
            set
            {
                this.address = value;
            }
            get
            {
                return this.address;
            }
        }

        public string Telephone
        {
            set
            {
                this.telephone = value;
            }

            get
            {
                return this.telephone; 
            }
        }

        public string Fax
        {
            set
            {
                this.fax = value;
            }
            get
            {
                return this.fax;
            }
        }

        public string Name
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }

        public string Description
        {
            set
            {
                this.description = value;
            }

            get
            {
                return this.description;
            }
        }

        public string PropertyAddress
        {
            set
            {
                this.propertyAddress = value;
            }

            get
            {
                return this.propertyAddress;
            }
        }
       
        public string City
        {
            set
            {
                this.city = value;
            }

            get
            {
                return this.city;
            }
        }

        public string Location
        {
            set
            {
                this.location = value;
            }

            get
            {
                return this.location;
            }
        }

        public string Purpose
        {
            set
            {
                this.purpose = value;
            }
            get
            {
                return this.purpose;
            }
        }

        public string Type
        {
            set
            {
                this.type = value;
            }
            get
            {
                return this.type;
            }
        }

        public Int32 Price
        {
            set
            {
                this.price = value;
            }
            get
            {
                return this.price;
            }
        }

        public string Currency
        {
            set
            {
                this.currency = value;
            }
            get
            {
                return this.currency;
            }
        }

        public string Category
        {
            set
            {
                this.category = value;
            }
            get
            {
                return this.category;
            }
        }

        public int BedRoom
        {
            set
            {
                this.bedRoom = value;
            }
            get
            {
                return this.bedRoom;
            }
        }

        public int BathRoom
        {
            set
            {
                this.bathRoom = value;
            }
            get
            {
                return this.bathRoom;
            }
        }

        public string Facilities
        {
            set
            {
                this.facilities = value;
            }
            get
            {
                return this.facilities;
            }
        }

        public string Direction
        {
            set
            {
                this.direction = value;
            }
            get
            {
                return this.direction;
            }
        }

        public string StartDate
        {
            set
            {
                this.startDate = value;
            }
            get
            {
                return this.startDate;
            }
        }

        public string NearBy
        {
            set
            {
                this.nearBy = value;
            }
            get
            {
                return this.nearBy;
            }
        }

        public string LegalDocument
        {
            set
            {
                this.legalDocument = value;
            }
            get
            {
                return this.legalDocument;
            }
        }

        public string LegalDocument02
        {
            set
            {
                this.legalDocument02 = value;
            }
            get
            {
                return this.legalDocument02;
            }
        }

        public string Other
        {
            set
            {
                this.other = value;
            }
            get
            {
                return this.other;
            }
        }

        public string Unit
        {
            set
            {
                this.unit = value;
            }
            get
            {
                return this.unit;
            }
        }

        public string Image1
        {
            set
            {
                this.image1 = value;
            }
            get
            {
                return this.image1;
            }
        }

        public string Image2
        {
            set
            {
                this.image2 = value;
            }
            get
            {
                return this.image2;
            }
        }

        public string Image3
        {
            set
            {
                this.image3 = value;
            }
            get
            {
                return this.image3;
            }
        }

        public string Image4
        {
            set
            {
                this.image4 = value;
            }
            get
            {
                return this.image4;
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
                parameters.Add("Address", this.address);
                parameters.Add("Telephone", this.telephone);
                parameters.Add("Fax", this.fax);
                parameters.Add("PropertyName", this.name);
                parameters.Add("PropertyDescription", this.description);
                parameters.Add("PropertyAddress", this.propertyAddress);
                parameters.Add("PropertyCity", this.city);
                parameters.Add("PropertyLocation", this.location);
                parameters.Add("Demand", this.purpose);
                parameters.Add("Type", this.type);
                parameters.Add("Price", this.price.ToString());
                parameters.Add("Currency", this.currency);
                parameters.Add("Category", this.category);
                parameters.Add("BedRoom", this.bedRoom.ToString());
                parameters.Add("BathRoom", this.bathRoom.ToString());
                parameters.Add("Facilities", this.facilities);
                parameters.Add("Direction", this.direction);
                parameters.Add("StartDate", this.startDate);
                parameters.Add("NearBy", this.nearBy);
                parameters.Add("LegalDocument", this.legalDocument);
                parameters.Add("LegalDocument02", this.legalDocument02);
                parameters.Add("Other", this.other);
                parameters.Add("Unit", this.unit);
                parameters.Add("Image1", this.image1);
                parameters.Add("Image2", this.image2);
                parameters.Add("Image3", this.image3);
                parameters.Add("Image4", this.image4);

                for (int i = 0; i < parameters.Keys.Count; i++)
                {
                    string replaceKey = "${" + parameters.GetKey(i) + "}";
                    this.bodyText = this.bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
                }
            }
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
