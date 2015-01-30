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
    [Authorize]
    public class BookingController : Controller
    {
        //
        // GET: /Booking/
        protected int sessionBookingPage
        {
            get
            {
                if (Session["sessionBookingPage"] == null) return 1;
                else return (int)Session["sessionBookingPage"];
            }
            set { Session["sessionBookingPage"] = value; }
        }
        public ActionResult Index()
        {
            IList<Booking> List = new List<Booking>();
            List = GetList();

            List<BookingModelEJ> listBookingModelEJ = new List<BookingModelEJ>();
            listBookingModelEJ = maptoEJ(List);

            ViewBag.datasource = listBookingModelEJ;

            return View();
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index",new {pageNumber = @Session["pageNumberBooking"] });
        }


        //
        // GET: /Booking/Details/5

        public ActionResult Details(int id)
        {
            Booking booking = LoadViewEdit(id);
            return View(booking);
        }

        //
        // GET: /Booking/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Booking/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index",new {pageNumber = @Session["pageNumberBooking"] });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Booking/Edit/5

        public ActionResult Edit(long id)
        {
            Booking booking = LoadViewEdit(id);
            return View();
        }

        private Booking LoadViewEdit(long id)
        {
            Booking booking = new Booking();
            IBookingRepository IrpBooking = new BookingRepository();
            booking = IrpBooking.getBookingId(id);
            booking.BookingDate = CustomHelpers.parstFormatTo_DD_MM_YYYY(booking.BookingDate);

            IBookingDetailRepository<BookingDetail> irBd = new BookingDetailRepository();
            IList<BookingDetail> listBd = new List<BookingDetail>();
            listBd = irBd.getListByBookingid(booking.BookingId);
            ViewData["ListBookingDetail"] = listBd;


            return booking;
        }

        //
        // POST: /Booking/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index", new { pageNumber = @Session["pageNumberBooking"] });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Booking/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Booking/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index", new { pageNumber = @Session["pageNumberBooking"] });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index1()
        {
            IList<Booking> List = new List<Booking>();
            List = GetList();

            List<BookingModelEJ> listBookingModelEJ = new List<BookingModelEJ>();
            listBookingModelEJ = maptoEJ(List);

            ViewBag.datasource = listBookingModelEJ;


            return View();
        }

        private List<BookingModelEJ> maptoEJ(IList<Booking> List)
        {
            List<BookingModelEJ> listBookingModelEJ = new List<BookingModelEJ>();
            foreach (var item in List)
            {
                listBookingModelEJ.Add(new BookingModelEJ()
                {
                    ArrivalDate = item.ArrivalDate,
                    BookingDate = CustomHelpers.parstFormatToDate(item.BookingDate),
                    ClientName = "<span style=\"color:#09f\">" + item.FullName + "</span>",
                    Email = item.Email,
                    Mobile = item.PhoneNumber,
                    DetailURL = "<a href=\"/Booking/Details/{idBooking}\">Detail</a>".Replace("{idBooking}", item.BookingId.ToString())
                });
            }

            return listBookingModelEJ;
        }

        private IList<Booking> GetList()
        {
            IRepository<Booking> iprH = new BookingRepository();
            IList<Booking> list = new List<Booking>();
            IBookingRepository iprB = new BookingRepository();
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as HotelOwner) != null)
            {
                list = iprB.getBookingFromOwnerId((@Session["UserType"] as HotelOwner).HotelOwnerId);                
            }
            else
            {
                list = iprH.GetAll();
            }

            return list;
        }
    }
}
