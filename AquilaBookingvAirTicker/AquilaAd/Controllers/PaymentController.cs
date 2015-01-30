using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using AquilaAd.Ext;
using AquilaAd.Models;

namespace AquilaAd.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/
        int pageSize = ApplicationHelper.pageSize;
        protected int sessionpaynumber
        {
            get {
                if (Session["sessionpaynumber"] == null) return 1;
                else return (int)Session["sessionpaynumber"]; 
            }
            set { Session["sessionpaynumber"] = value; }
        }
        public ActionResult Index()
        {
            IList<Payment> listpayment = new List<Payment>();
            listpayment = getlist();

            List<PaymentModel> listPaymentModelEJ = new List<PaymentModel>();
            listPaymentModelEJ = maptoEJ(listpayment);

            ViewBag.datasource = listPaymentModelEJ;

            return View();
        }

        private IList<Payment> getlist(int pageNumber)
        {
            IRepository<Payment> iprH = new PaymentRepository();
            IPaymentRepository<Payment> iExtentPayment = new PaymentRepository();
            //IBookingDetailRepository<BookingDetail> iExtentBookingDetail = new BookingDetailRepository();
            IList<Payment> listpayment = new List<Payment>();
            
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner) != null)
            {
               // listpayment khi dang nhap user quản lý
                listpayment = iExtentPayment.getPaymentFilterFromHotelOwner((@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner).HotelOwnerId);
                int countHotel = listpayment.Count;
                ViewData["CountAll"] = ApplicationHelper.PageNumber(countHotel == 0 ? 1 : countHotel);
                listpayment = listpayment.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<Payment>();
            }
            else
            {
                // khi dang nhap quyen admin
                //var bookingfilter = iExtentBookingDetail.getListFilterByHotelOwner(1);
                int countHotel = iExtentPayment.countAllPayment();
                ViewData["CountAll"] = ApplicationHelper.PageNumber(countHotel == 0 ? 1 : countHotel);
                int pageNumberPayment = (pageNumber - 1) * pageSize;
                listpayment = iExtentPayment.getPaymentwithPaging(pageNumberPayment, pageSize);

            }
            return listpayment;
        }

        //
        // GET: /Payment/Details/5

        public ActionResult Details(string id)
        {
            Payment payment = LoadView(id);

            return View(payment);
        }

        private Payment LoadView(string id)
        {
            IPaymentRepository<Payment> iExtentPayment = new PaymentRepository();
            Payment payment = new Payment();
            payment = iExtentPayment.GetByBookingCode(id); // get payment by view code

            IBookingRepository iExtentbooking = new BookingRepository();
            Booking booking = new Booking();
            booking = iExtentbooking.getBookingId(payment.BookingId);
            ViewData["booking"] = booking;

            IBookingDetailRepository<BookingDetail> iExtentBookingDetail = new BookingDetailRepository();
            IList<BookingDetail> listBookingDetail = new List<BookingDetail>();
            listBookingDetail = iExtentBookingDetail.getListByBookingid(payment.BookingId);
            ViewData["listBookingDetail"] = listBookingDetail;

            payment.PaymentDate = CustomHelpers.parstFormatTo_DD_MM_YYYY(payment.PaymentDate);
            payment.CheckInTime = payment.CheckInTime == null ? "": CustomHelpers.parstFormatTo_DD_MM_YYYY(payment.CheckInTime);
            payment.Remark = payment.Remark == null ? "" : payment.Remark;

            

           

            return payment;
        }

        //
        // GET: /Payment/Create

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Cancel()
        {

            //return RedirectToAction("Index");
            return RedirectToAction("Index", new { pageNumber = sessionpaynumber });
        }
        //
        // POST: /Payment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Payment/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Payment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Payment/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Payment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Index1()
        {
            IList<Payment> listpayment = new List<Payment>();
            listpayment = getlist();

            List<PaymentModel> listPaymentModelEJ = new List<PaymentModel>();
            listPaymentModelEJ = maptoEJ(listpayment);

            ViewBag.datasource = listPaymentModelEJ;

            return View();
        }

        private List<PaymentModel> maptoEJ(IList<Payment> List)
        {
            List<PaymentModel> listPaymentModelEJ = new List<PaymentModel>();
            foreach (var item in List)
            {
                listPaymentModelEJ.Add(new PaymentModel()
                {
                    PaymentDateFormatDate = CustomHelpers.parstFormatToDate(item.PaymentDate),
                    BookingCode = item.BookingCode,
                    OrderAmount = item.OrderAmount,
                    PaymentAmount = item.PaymentAmount,
                    MustPaymentAmount = item.OrderAmount - @item.PaymentAmount,
                    PaymentStatusString = Enum.GetName(typeof(AquilaAd.Controllers.PaymentStatus), @item.PaymentStatus),
                    DetailURL = "<a href=\"/Payment/Details/{idPayment}\">Detail</a>".Replace("{idPayment}", item.BookingCode)
                });
            }

            return listPaymentModelEJ;
        }

        private IList<Payment> getlist()
        {
            IRepository<Payment> iprH = new PaymentRepository();
            IPaymentRepository<Payment> iExtentPayment = new PaymentRepository();            
            IList<Payment> listpayment = new List<Payment>();

            if (User.Identity.IsAuthenticated && (@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner) != null)
            {
                // listpayment khi dang nhap user quản lý
                listpayment = iExtentPayment.getPaymentFilterFromHotelOwner((@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner).HotelOwnerId);                
            }
            else
            {
                listpayment = iprH.GetAll();

            }
            return listpayment;
        }
       
        

    }
    public enum PaymentStatus
    {
        Processing = 1, Hanging = 2, Shipping = 3, Cancelled = 4, Finished = 5, NonPayment = 0
    }
   
}
