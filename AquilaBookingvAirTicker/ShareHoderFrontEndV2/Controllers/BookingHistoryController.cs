using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHoderFrontEndV2.Ext;

namespace ShareHoderFrontEndV2.Controllers
{
    public class BookingHistoryController : BaseController
    {        
        //
        // GET: /BookingHistory/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("InfoServicesMember", "BookingHistory");
            }
            return View();
        }

        public ActionResult CheckServices(string CodeNumberService)
        {
            // get booking
            
           
            // when user login system => redirect to profile
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("InfoServicesMember", "BookingHistory");
            }
            else
            {
                IBookingRepository ibkr = new BookingRepository();
                Booking booking = ibkr.getBookingFromViewCode(CodeNumberService);
                //
                if (booking == null)
                {
                    ViewData["checkbooking"] = "Không tìm thấy"; //not found 
                    return View();
                }

                return RedirectToAction("InfoServices", "BookingHistory", new { CodeNumberService = CodeNumberService });
            }


            
        }
        public ActionResult InfoServices(string CodeNumberService)
        {
            IBookingRepository ibkr = new BookingRepository();
            Booking booking = ibkr.getBookingFromViewCode(CodeNumberService);

            IPaymentRepository<Payment> irPayment = new PaymentRepository();
            var Payment =  irPayment.GetByBookingCode(CodeNumberService);
            IRepository<Payment> irPaymentAction = new PaymentRepository();
            //var resultPayment = PaymentHelper.GetOrderInformation(CodeNumberService);
            if (Payment.PaymentStatus == 1 )
            {
                ViewData["viewOrderStatus"] = "Thanh toán thành công";
            }
            else
            {
                ViewData["viewOrderStatus"] = "Thanh toán thất bại";
            }
            ViewData["viewSumOrder"] = Payment.OrderAmount;
            ViewData["viewDispositOrder"] = Payment.DispositAmount;
            return View(booking);
        }


        public ActionResult InfoServicesMember()
        {
            IBookingRepository ibkr = new BookingRepository();
            IList<Booking> listBooking = new List<Booking>();
            listBooking = ibkr.getBookingFromMemberId((@Session["LoginObject"] as ShareHolderCore.Domain.Model.Membership).MemberId);

            return View(listBooking);
        }

    }
}
