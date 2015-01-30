 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHoderFrontEndV2.Models;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;
using System.IO;
using System.Net.Sockets;
using System.Net;
using ShareHoderFrontEndV2.Ext;
using Payoo.Lib;
using System.Text;
using System.Configuration;

namespace ShareHoderFrontEndV2.Controllers
{
    public class PaymentController : BaseController
    {
        //
        // GET: /Payment/

        public ActionResult Index()
        {
            List<CartItem> cartItem = ShoppingCart.Instance.Items;
            return View(cartItem);
        }

        public void Book(string FirstNameClient, string LastNameClient, string MailAddressClient,
                                string MobileClient, string ArrivalTimeClient, string RemarkClient,
                                string vpc_CardNum, string monthExpire, string yearExpire,
                                string CardAddress, string CardBankName, string vpc_AVS_PostCode,
                                string vpc_AVS_Country, string vpc_CardHoderName, string vpc_CardSecurityCode, string PayMetholHiddenText)
        {
            string Status = string.Empty;
            StringBuilder orderTableInfo = new StringBuilder();
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            List<long> listCartRemove = new List<long>();
            IRepository<Booking> IrpBooking = new BookingRepository();
            IRepository<BookingDetail> IrpBookingDetail = new BookingDetailRepository();
            ICurrencyTypeRepository IrpCurrencyType = new CurrencyTypeRepository();

            //string IPAddress = ApplicationHelper.GetIPAddressFromMachineName(); // get ip clients

            Booking Booking = new Booking();
            Booking.BookingDate = currentDate;
            Booking.FullName = FirstNameClient + " " + LastNameClient;
            Booking.PaymentType = "1";
            Booking.Email = MailAddressClient;
            Booking.PhoneNumber = MobileClient;
            Booking.ArrivalDate = ArrivalTimeClient;
            Booking.Remark = RemarkClient;
            Booking.ViewCode = Guid.NewGuid().ToString().Substring(25, 10).ToUpper();
            Booking.CreatedDate = currentDate;
            Booking.CreatedBy = "AQUILA";
            Booking.OrderStatus = "N";
            Booking.IpAddress = ApplicationHelper.GetIPAddressFromMachineName();
            // save viewcode to search order
            string viewCode = Booking.ViewCode;

            if (User.Identity.IsAuthenticated)
            {
                Booking.Membership = (Membership)@Session["LoginObject"];

            }
            IrpBooking.Save(Booking); // save booking
            decimal total = decimal.Zero;
          
            foreach (var item in ShoppingCart.Instance.Items)
            {
                total += (decimal)item.Subtotal;
                BookingDetail BookingDetail = new BookingDetail();

                // asign booking detail
                BookingDetail.Booking = Booking;
                BookingDetail.HotelId = item.Prod.Availability.Hotel.HotelId;
                BookingDetail.RoomType = item.Prod.Availability.RoomType;
                BookingDetail.Quantity = item.NoRoom;
                BookingDetail.Price = item.Prod.Availability.Price;
                BookingDetail.CurrencyType = IrpCurrencyType.GetById(1);
                BookingDetail.FromDate = (ApplicationHelper.ParseDatetoInt(item.FromDate)).ToString();
                BookingDetail.ToDate = (ApplicationHelper.ParseDatetoInt(item.ToDate)).ToString();
               // BookingDetail.Remark = item.Subtotal.ToString();
                BookingDetail.CreatedDate = currentDate;
                BookingDetail.CreatedBy = "AQUILA";
                BookingDetail.BookingType = "H";

                // insert booking into database 
                if (BookingDetail != null)
                { //booking detail
                    IrpBookingDetail.Save(BookingDetail);
                }
                listCartRemove.Add(item.ProductId);

            }

            total = total + (total * Convert.ToDecimal(0.1));
            // remove all item in cart
            foreach (var item in listCartRemove)
            {
                ShoppingCart.Instance.RemoveItem(item);
            }
            //

            // luu gia tri thanh toan vao bang payment
            Payment payment = new Payment();
            IRepository<Payment> irppayment = new PaymentRepository();

            payment.Address = CardAddress;
            //payment.BankName = CardBankName;
            payment.BookingCode = Booking.ViewCode;
            payment.BookingId = Booking.BookingId;
            payment.CardHolderName = vpc_CardHoderName;
            payment.CardNumber = vpc_CardNum;
           // payment.CheckInTime = DateTime.Now.ToString("yyyyMMdd");
            payment.CreatedBy = "Aquila System";
            payment.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            payment.CurrencyTypeId = 1;
            payment.CVCCode = vpc_CardSecurityCode;
            payment.DeliveryDateTime = ArrivalTimeClient;
            payment.DispositAmount = (total * PaymentHelper.DispositPercent) / 100;
            payment.Email = MailAddressClient;
            payment.ExpiryDate = yearExpire + monthExpire;
            payment.FirstName = FirstNameClient;
            payment.LastName = LastNameClient;
            payment.IpAddress = "";
            if (User.Identity.IsAuthenticated)
            {
                payment.MemberId = (@Session["LoginObject"] as Membership).MemberId;
            }
            payment.OrderAmount = total;
            payment.OrderFee = decimal.Zero;
            //payment.PaymentAmount = 0;
            payment.PaymentDate = DateTime.Now.ToString("yyyyMMdd");
            payment.PaymentTime = DateTime.Now.ToString("hhmmss");
            payment.PaymentGateWay = "P";
            payment.PaymentMethod = PayMetholHiddenText;
            payment.PaymentType = "O";
            payment.PhoneNumber = MobileClient;
            payment.Remark = RemarkClient;
            payment.PayooOrderStatus = 1;
            try
            {
                irppayment.Save(payment);
            }
            catch (Exception)
            {
                throw;
            }

            // end

            orderTableInfo.Append("<table width='100%' border='1' cellspacing='0'>");
            orderTableInfo.Append("<thead>");
            orderTableInfo.Append("<tr>");
            orderTableInfo.Append("<td width='40%' align='center'>");
            orderTableInfo.Append("<b>" + "Gia tri Don hang" + "</b></td>");
            orderTableInfo.Append("<td width='20%' align='right'>");
            orderTableInfo.Append("<b>" + string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}", total) + "</b></td>");
            orderTableInfo.Append("</tr>");
            orderTableInfo.Append("<tr>");
            orderTableInfo.Append("<td width='40%' align='center'>");
            orderTableInfo.Append("<b>" + "Thanh toan truoc" + "</b></td>");
            orderTableInfo.Append("<td width='20%' align='right'>");
            orderTableInfo.Append("<b>" + string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}", payment.DispositAmount) + "</b></td>");
            orderTableInfo.Append("</tr>");
            orderTableInfo.Append("<tr>");
            orderTableInfo.Append("<td width='40%' align='center'>");
            orderTableInfo.Append("<b>" + "So tien con lai" + "</b></td>");
            orderTableInfo.Append("<td width='20%' align='right'>");
            orderTableInfo.Append("<b>" + string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0,0:N0}", (total - payment.DispositAmount)) + "</b></td>");
            orderTableInfo.Append("</tr>");

            

            // call http post payoo
            PayooOrder order = new PayooOrder();
            //order.Session = payment.BookingCode;
            order.Session = Booking.ViewCode;
            order.BusinessUsername = PaymentHelper.BusinessUsername;
            order.OrderCashAmount = Convert.ToInt64(payment.DispositAmount);
           //order.OrderCashAmount = Convert.ToInt64(1);
          //  order.OrderNo = payment.BookingCode;
            order.OrderNo = Booking.ViewCode;
            order.ShippingDays = short.Parse(PaymentHelper.ShippingDays);
            order.ShopBackUrl = PaymentHelper.ShopBackUrl + "?viewcode=" + order.Session;
            order.ShopDomain = PaymentHelper.ShopDomain;
            order.ShopID = long.Parse(PaymentHelper.ShopID);
            order.ShopTitle = PaymentHelper.ShopTitle;
            order.StartShippingDate = DateTime.Now.ToString("dd/MM/yyyy");
           
            order.NotifyUrl = PaymentHelper.NotifyUrl;            
            order.OrderDescription = HttpUtility.UrlEncode(orderTableInfo.ToString());

            string XML = PaymentXMLFactory.GetPaymentXML(order, Server.MapPath(@"..\App_Data\Certificates\biz_pkcs12.p12"), "biz", Server.MapPath(@"..\App_Data\Certificates\payoo_public_cert.pem"));
            RedirectToProvider(PaymentHelper.PayooCheckout, XML);

            

            // end

            //return RedirectToAction("Result", "Payment", new { viewcode = viewCode });
        }

        private void RedirectToProvider(string ProviderUrl, string XMLCheckout)
        {
            string redirect = "<html><head><title></title></head>";
            redirect += "<body><form action='" + ProviderUrl + "' method='post' style='margin-top: 50px; text-align: center;'>";
            redirect += "<noscript><input type='submit' value='Click if not redirected' /></noscript>";
            redirect += "<div id='ContinueButton' style='display: none;'><input type='submit' value='Click if not redirected' />";
            redirect += "</div><input type='hidden' name='OrdersForPayoo' value='" + XMLCheckout + "'/></form>";
            redirect += "<script type='text/javascript'>window.onload = function() ";
            redirect += "{document.forms[0].submit();setTimeout(function() {document.getElementById('ContinueButton').style.display = '';}, 1000);}";
            redirect += "</script></body></html>";
            Response.Write(redirect);
        }

        public ActionResult Result(string viewcode)
        {
           
            if (String.IsNullOrEmpty(viewcode))
                ViewData["viewcode"] = "";
            else
                ViewData["viewcode"] = viewcode;

            var resultPayment = PaymentHelper.GetOrderInformation(viewcode);
           
            if (resultPayment.CheckOrder)
            {
                if (resultPayment.OrderStatus.Equals("Processing") || resultPayment.OrderStatus.Equals("Finished"))
                {
                    ViewData["viewOrderStatus"] = Resources.Resource.paidSuccessfully;
                }
                else if (resultPayment.OrderStatus.Equals("Cancelled"))
                {
                    ViewData["viewOrderStatus"] = Resources.Resource.orderCancelled;
                }
                else if (resultPayment.OrderStatus.Equals("Shipping"))
                {
                    ViewData["viewOrderStatus"] = Resources.Resource.orderShipping;
                }
                else
                {
                    ViewData["viewOrderStatus"] = Resources.Resource.paidError;
                }
                //ViewData["viewOrderStatus"] = resultPayment.OrderStatus;
                ViewData["viewCheckOrder"] = resultPayment.CheckOrder;
                ViewData["viewDeliveryDate"] = resultPayment.DeliveryDate;
                ViewData["viewLongMessage"] = resultPayment.LongMessage;
                ViewData["viewOrderCash"] = resultPayment.OrderCash;
                ViewData["viewOrderFee"] = resultPayment.OrderFee;
                ViewData["viewPaymentDate"] = resultPayment.PaymentDate;
                ViewData["viewSeverityCode"] = resultPayment.SeverityCode;
                ViewData["viewShippingDate"] = resultPayment.ShippingDate;
            }
            else
            {
                ViewData["viewOrderStatus"] = resultPayment.LongMessage;
            }
            #region save payment status  in database
            // 
            IPaymentRepository<Payment> irPayment = new PaymentRepository();
            IRepository<Payment> irPaymentAction = new PaymentRepository();
            Booking updateBooking = new Booking();

            IBookingRepository IrpBooking = new BookingRepository();
            IRepository<Booking> irBookingAction = new BookingRepository();

            var pamentUpdate = irPayment.GetByBookingCode(viewcode);
            pamentUpdate.PayooOrderStatus = CodeOrderStatus(resultPayment.OrderStatus);
            updateBooking = IrpBooking.getBookingFromViewCode(viewcode);

            try
            {
               
                if (pamentUpdate.PayooOrderStatus == 1)
                {
                    pamentUpdate.PaymentStatus = 1;
                    updateBooking.OrderStatus = "P";
                    pamentUpdate.PaymentAmount = resultPayment.OrderCash;
                }
                else if (pamentUpdate.PayooOrderStatus == 4)
                {
                    updateBooking.OrderStatus = "C";                    
                }
                try
                {
                    irBookingAction.Update(updateBooking);
                    irPaymentAction.Update(pamentUpdate);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //
            #endregion

            #region send the email when customer payment finish

            PaymentHelper.SendEmailforBooking(viewcode, pamentUpdate.Email, pamentUpdate.FirstName + " " + pamentUpdate.LastName);



            #endregion
            return View();
        }

        public int CodeOrderStatus(string code)
        {
            int returnCode = 0;
            switch (code)
            {
                case "Processing":
                    returnCode = 1;
                    break;
                case "Hanging":
                    returnCode = 2;
                    break;
                case "Shipping":
                    returnCode = 3;
                    break;
                case "Cancelled":
                    returnCode = 4;
                    break;
                case "Finished":
                    returnCode = 5;
                    break;
                case "Error":
                    returnCode = 6;
                    break;
                default:
                    returnCode = 8;
                    break;
            }
            return returnCode;           
        }
       
        public ActionResult Notify()
        {
            string NotifyMessage = Request.Form.Get("NotifyData");

            NotifyMessage = "<?xml version='1.0'?><PayooConnectionPackage xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><Data>PFBheW1lbnROb3RpZmljYXRpb24+PHNob3BzPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxzaG9wPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8c2Vzc2lvbj4xNjg2MjQ0OTIzPC9zZXNzaW9uPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dXNlcm5hbWU+cG5obG9uZzwvdXNlcm5hbWU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxzaG9wX2lkPjM4MDwvc2hvcF9pZD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHNob3BfdGl0bGU+VmlldGtpdGU8L3Nob3BfdGl0bGU+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxzaG9wX2RvbWFpbj5odHRwOi8vdmlldGtpdGUuY29tLzwvc2hvcF9kb21haW4+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxzaG9wX2JhY2tfdXJsPmh0dHA6Ly92aWV0a2l0ZS5jb20vcGF5b28vdGhhbmt5b3UuYXNweDwvc2hvcF9iYWNrX3VybD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPG9yZGVyX25vPjE2ODYyNDQ5MjM8L29yZGVyX25vPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8b3JkZXJfY2FzaF9hbW91bnQ+Mjwvb3JkZXJfY2FzaF9hbW91bnQ+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxvcmRlcl9zaGlwX2RhdGU+MzAvMDEvMjAxMzwvb3JkZXJfc2hpcF9kYXRlPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8b3JkZXJfc2hpcF9kYXlzPjE8L29yZGVyX3NoaXBfZGF5cz4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPG9yZGVyX2Rlc2NyaXB0aW9uPkNoaSt0aSVlMSViYSViZnQraCVjMyViM2ErJWM0JTkxJWM2JWExbit0aGFuaCt0byVjMyVhMW4rbmh1K3NhdSUzYUhQK1BhdmlsaW9uK0RWMy0zNTAyVFgrR2klYzMlYTElM2ErMjMxMDkyMTAuRkFOK05vdGVib29rKyhCNCkrR2klYzMlYTElM2ErMjY2ODUwLihTb21lK25vdGVzK2Zvcit0aGUrb3JkZXIpPC9vcmRlcl9kZXNjcmlwdGlvbj4KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPG5vdGlmeV91cmw+aHR0cDovL3ZpZXRraXRlLmNvbS9wYXlvby9Ob3RpZnlMaXN0ZW5lci5hc3B4PC9ub3RpZnlfdXJsPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwvc2hvcD4KICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwvc2hvcHM+PFN0YXRlPlBBWU1FTlRfUkVDRUlWRUQ8L1N0YXRlPjwvUGF5bWVudE5vdGlmaWNhdGlvbj4=</Data><Signature>MIIBbQYJKoZIhvcNAQcCoIIBXjCCAVoCAQExCzAJBgUrDgMCGgUAMAsGCSqGSIb3DQEHATGCATkwggE1AgEBMIGSMIGEMQswCQYDVQQGEwJWVTEMMAoGA1UECBMDSENNMQwwCgYDVQQHEwNIQ00xEjAQBgNVBAoTCVZpZXRVbmlvbjEOMAwGA1UECxMFUGF5b28xDjAMBgNVBAMTBVBheW9vMSUwIwYJKoZIhvcNAQkBFhZwYXlvb0B2aWV0dW5pb24uY29tLnZuAgkA673T+Q8894cwCQYFKw4DAhoFADANBgkqhkiG9w0BAQEFAASBgE5KARgcs45SfKa9+FYzwrjkPPpspY++aEnHaN48yK/Sd9I1yH4m9bFNWSH9W9Yg1SV7UnjeT+9HfjGE3L6Eo9ElRyymdQTpLXY8WH9YmuKEe53YqbH6mxmoxAwVXS69OlkaTivEOKffxP7QytwqMG31Ti40dTIYdGMIkJ0qJReT</Signature><PayooSessionID>DI0jRfI1Am1xbc9q+oFU3eJ9zCRCKNUSTTcjsL3psZltUb/bhIUObx8TjZ572KpHXkOKt9SwrBEVdh4qiVXo0Q==</PayooSessionID></PayooConnectionPackage>";

            if (NotifyMessage == null || "".Equals(NotifyMessage))
            {
                return View();
            }

            Credential credential = new Credential();
            credential.APIUsername = ConfigurationManager.AppSettings["APIUsername"];
            credential.APIPassword = ConfigurationManager.AppSettings["APIPassword"];
            credential.APISignature = ConfigurationManager.AppSettings["APISignature"];
          
            PayooNotify listener = new PayooNotify(NotifyMessage,
                ConfigurationManager.AppSettings["PayooBusinessAPILive"],
                credential, Server.MapPath(@"..\App_Data\Certificates\biz_pkcs12.p12"), "biz",
                Server.MapPath(@"..\App_Data\Certificates\payoo_public_cert.pem"));
            if (listener.CheckNotifyMessage())
            {
                PaymentNotification invoice = listener.GetPaymentNotify();
                if (listener.ConfirmToPayoo())
                {
                    string LogPath = Server.MapPath(@"..\App_Data\invoice.txt");
                    LogWriter.WriteLog(LogPath, "OrderNo: " + invoice.OrderNo);
                    LogWriter.WriteLog(LogPath, "OrderCashAmount: " + invoice.OrderCashAmount);
                    //...so on ...
                    LogWriter.WriteLog(LogPath, "State: " + invoice.State);
                }
                else
                {
                    //ConfirmToPayoo fail. Log for manual investigation.
                    string LogPath = Server.MapPath(@"..\App_Data\log.txt");
                    LogWriter.WriteLog(LogPath, "ConfirmToPayoo fail.");
                }
            }
            else
            {
                //Invalid digital signature. Log for manual investigation.
                string LogPath = Server.MapPath(@"..\App_Data\log.txt");
                LogWriter.WriteLog(LogPath, "Invalid digital signature.");
            }
            return View();
        }
    }
}
