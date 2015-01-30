using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ShareHoderFrontEndV2.Models;
using Payoo.Lib;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore.Domain.Model;

namespace ShareHoderFrontEndV2.Ext
{
    public static class PaymentHelper
    {
        public static string BusinessUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["BusinessUsername"];
            }
        }
        public static string ShippingDays
        {
            get
            {
                return ConfigurationManager.AppSettings["ShippingDays"];
            }
        }
        public static string ShopBackUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopBackUrl"];
            }
        }
        public static string ShopDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopDomain"];
            }
        }
        public static string ShopID
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopID"];
            }
        }
        public static string ShopTitle
        {
            get
            {
                return ConfigurationManager.AppSettings["ShopTitle"];
            }
        }
        public static string NotifyUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["NotifyUrl"];
            }
        }
        public static string PayooCheckout
        {
            get
            {
                return ConfigurationManager.AppSettings["PayooCheckout"];
            }
        }


        public static PaymentPayoo GetOrderInformation(string orderid) 
        {
            PaymentPayoo paymentPayoo = new PaymentPayoo();
            Credential credential = new Credential();
            credential.APIUsername = ConfigurationManager.AppSettings["APIUsername"];
            credential.APIPassword = ConfigurationManager.AppSettings["APIPassword"];
            credential.APISignature = ConfigurationManager.AppSettings["APISignature"];
            Caller caller = new Caller();
            caller.InitCall(ConfigurationManager.AppSettings["PayooBusinessAPILive"], credential,
                System.Web.HttpContext.Current.Server.MapPath(@"..\App_Data\Certificates\biz_pkcs12.p12"), "biz", System.Web.HttpContext.Current.Server.MapPath(@"..\App_Data\Certificates\payoo_public_cert.pem"));
            GetOrderInformationRequestType request = new GetOrderInformationRequestType();
            request.OrderID = orderid;
            request.ShopID = long.Parse(ConfigurationManager.AppSettings["ShopID"]);
            GetOrderInformationResponseType response = (GetOrderInformationResponseType)caller.Call("GetOrderInformation", request);
           
            if (response.Ack == AckCodeType.Success || response.Ack == AckCodeType.SuccessWithWarning)
            {
                ///lblResult.Text = "Result:";
                paymentPayoo.CheckOrder = true;
                paymentPayoo.DeliveryDate = response.DeliveryDate;
                paymentPayoo.OrderCash = response.OrderCash;
                paymentPayoo.OrderFee = response.OrderFee;
                paymentPayoo.OrderStatus = response.OrderStatus.ToString();
                paymentPayoo.PaymentDate = response.PaymentDate;
                paymentPayoo.ShippingDate = response.ShippingDate;
            }
            else
            {
                paymentPayoo.CheckOrder = false;
                paymentPayoo.SeverityCode = response.Error.SeverityCode;
                paymentPayoo.LongMessage = response.Error.LongMessage;

            }

            return paymentPayoo;
        }

        public static decimal DispositPercent
        {
            
            get
            {
                IRepository<SystemParameter> irPay = new SystemParameterRepository();
                return decimal.Parse(irPay.GetById(new Guid("EF4273F7-B40D-4550-ACBE-5909772A16AA")).ParameterValue);
            }
        }

        public static void SendEmailforBooking(string bookingCode, string receiver, string nameClient)
        {         
            try
            {
                BookingEmail bookingEmail = new BookingEmail();
                bookingEmail.FileName = ApplicationHelper.ChangePasswordEmailTemplatePath;
                bookingEmail.IsHtmlMail = true;
                bookingEmail.SmtpServer = ApplicationHelper.SmtpServer;
                bookingEmail.SmtpPort = ApplicationHelper.SmtpPort;
                bookingEmail.EnableSSL = ApplicationHelper.EnableSSL;
                bookingEmail.Sender = ApplicationHelper.ApplicationEmailAddress;
                bookingEmail.SmtpEmail = ApplicationHelper.ApplicationEmailAddress;
                bookingEmail.SmtpPassword = ApplicationHelper.ApplicationEmailPassword;
                bookingEmail.Receiver = receiver;
                bookingEmail.Subject = Resources.UserInterface.changePassEmailSubject;
                bookingEmail.UseContentTemplate = true;
                bookingEmail.SenderName = Resources.UserInterface.senderName;
                bookingEmail.ReceiverName = nameClient;
                bookingEmail.BookingCode = bookingCode;
                
                string contentAttachment = PDFUtility.getContentXML(ApplicationHelper.BookingEmailTemplatePath);
                contentAttachment = contentAttachment.Replace("{$bookingCode$}", bookingEmail.BookingCode);
                bookingEmail.SendEmailWithAttachment(contentAttachment);
            }
            catch (Exception)
            {                
                throw;
            }
            
        }
    }
}