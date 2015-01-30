using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using AquilaAd.Models;
using AquilaAd.Ext;

namespace AquilaAd.Controllers
{
    [Authorize]
    public class AvailabilityController : Controller
    {
        //
        // GET: /Availability/
        int pageSize = ApplicationHelper.pageSize;
        public int pageNumberAvailability
        {
            get
            {
                if (Session["pageNumberAvailability"] == null) return 1;
                else return (int)Session["pageNumberAvailability"];
            }
            set { Session["pageNumberAvailability"] = value; }
        }
        public string TypeAvailabilitySession
        {
            get
            {
                if (Session["TypeAvailabilitySession"] == null) return "";
                else return (string)Session["TypeAvailabilitySession"];
            }
            set { Session["TypeAvailabilitySession"] = value; }
        }
        public string ValueAvailabilitySession
        {
            get
            {
                if (Session["ValueAvailabilitySession"] == null) return "";
                else return (string)Session["ValueAvailabilitySession"];
            }
            set { Session["ValueAvailabilitySession"] = value; }
        }
        public ActionResult Index()
        {            
            IList<Availability> ListAvailability = new List<Availability>();
            ListAvailability = GetList();
           

            List<AvailabilityModel> listAvailabilityModel = new List<AvailabilityModel>();
            listAvailabilityModel = maptoEJ(ListAvailability);

            ViewBag.datasource = listAvailabilityModel;

            return View();
        }



        private IList<Availability> GetList()
        {
            IRepository<Availability> iprH = new AvailabilityRepository();
            IAvailabilityRepository<Availability> iprAInterface = new AvailabilityRepository();            
            IList<Availability> list = new List<Availability>();
            
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner) != null)
            {

                list = iprAInterface.getAvailabilityFilterFromHotelOwner((@Session["UserType"] as HotelOwner).HotelOwnerId);                

            }
            else
            {
             
                list = iprH.GetAll();
            }            
            

            return list;
        }

        private IList<Availability> GetListPaging(IList<Availability> list,int pageNumber)
        {
            int countHotel = list.Count;
            ViewData["CountAll"] = ApplicationHelper.PageNumber(countHotel == 0 ? 1 : countHotel);
            list = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<Availability>();            
            return list;
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index", new { pageNumber = pageNumberAvailability });
        }

        //
        // GET: /Availability/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Availability/Create

        public ActionResult Create()
        {
            Availability availability = LoadViewCreate();
            return View(availability);
            
        }

        private Availability LoadViewCreate()
        {
            Availability availability = new Availability();
            availability.CreatedDate = DateTime.Now.ToString("yyyyMMdd");

            HotDealHotel hotDealHotel = new HotDealHotel();
            IList<HotDealHotel> listsMostViewHotel = hotDealHotel.listAllHotDealHotel();
            ViewData["HotDealHotel"] = new SelectList(listsMostViewHotel, "Id", "Name");


            IRepository<Hotel> rpht = new HotelRepository(); // load list hotel
            IHotelRepository<Hotel> irHotelI = new HotelRepository();
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner) != null) // đăng nhập bằng người quản lý KS
            {
                ViewData["listHotel"] = new SelectList(irHotelI.getHotelFilterByOwnerId((@Session["UserType"] as HotelOwner).HotelOwnerId), "HotelId", "Name");
            }
            else
            {
                ViewData["listHotel"] = new SelectList(rpht.GetAll(), "HotelId", "Name");
            }

            IRepository<RoomType> rprt = new RoomTypeRepository(); // load roomtype
            ViewData["listRoomType"] = new SelectList(rprt.GetAll(), "RoomTypeId", "Name");

            IRepository<CurrencyType> rpct = new CurrencyTypeRepository(); // load listCurrencyType
            ViewData["listCurrencyType"] = new SelectList(rpct.GetAll(), "CurrencyTypeId", "CurrencyCode");
            
            return availability;
        }

        //
        // POST: /Availability/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                CreateNewAvailability(collection);
                return RedirectToAction("Index", new { pageNumber = pageNumberAvailability });
            }
            else
            {
                return View();
            }
        }

        private void CreateNewAvailability(FormCollection fc)
        {
            Availability availability = new Availability();
            IRepository<Availability> rpav = new AvailabilityRepository();
            ICurrencyTypeRepository rpct = new CurrencyTypeRepository();
            IRepository<Hotel> rpht = new HotelRepository();
            IRoomTypeRepository<RoomType> rprt = new RoomTypeRepository();

            availability.CurrencyType = rpct.GetById(byte.Parse(fc["CurrencyType.CurrencyTypeId"]));
            availability.FromDate = CustomHelpers.parstFormatToYYYYMMDD(fc["FromDate"]);
            availability.ToDate = CustomHelpers.parstFormatToYYYYMMDD(fc["ToDate"]);
            availability.Hotel = rpht.GetById(int.Parse(fc["Hotel.HotelId"]));
            availability.IsHotDeal = fc["IsHotDeal"];
            availability.Price = decimal.Parse(fc["Price"]);
            availability.Quantity = int.Parse(fc["Quantity"]);
            availability.RoomType = rprt.GetById(byte.Parse(fc["RoomType.RoomTypeId"]));            
            availability.CreatedDate = fc["CreatedDate"].Replace("-", "");
            availability.CreatedBy = fc["CreatedBy"];
            
            try
            {
                rpav.Save(availability);
            }
            catch
            {

            }
        }

        //
        // GET: /Availability/Edit/5

        public ActionResult Edit(int id)
        {

            Availability availability = LoadViewEdit(id);

            return View(availability);
        }

        private Availability LoadViewEdit(long id)
        {
            IAvailabilityRepository<Availability> rpav = new AvailabilityRepository();
            Availability availability = rpav.getAvailabilityById(id);

            availability.FromDate = CustomHelpers.parstFormatTo_DD_MM_YYYY(availability.FromDate);
            availability.ToDate = CustomHelpers.parstFormatTo_DD_MM_YYYY(availability.ToDate);

            availability.Price = decimal.Round(availability.Price.Value);

            HotDealHotel hotDealHotel = new HotDealHotel();
            IList<HotDealHotel> listsMostViewHotel = hotDealHotel.listAllHotDealHotel();
            ViewData["HotDealHotel"] = new SelectList(listsMostViewHotel, "Id", "Name", availability.IsHotDeal);

            IRepository<Hotel> rpht = new HotelRepository(); // load list hotel
            IHotelRepository<Hotel> irHotelI = new HotelRepository();
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as ShareHolderCore.Domain.Model.HotelOwner) != null) // đăng nhập bằng người quản lý KS
            {
                ViewData["listHotel"] = new SelectList(irHotelI.getHotelFilterByOwnerId((@Session["UserType"] as HotelOwner).HotelOwnerId), "HotelId", "Name", availability.Hotel.HotelId);
            }
            else
            {
                ViewData["listHotel"] = new SelectList(rpht.GetAll(), "HotelId", "Name", availability.Hotel.HotelId);
            }
                       

            IRepository<RoomType> rprt = new RoomTypeRepository(); // load roomtype
            ViewData["listRoomType"] = new SelectList(rprt.GetAll(), "RoomTypeId", "Name", availability.RoomType.RoomTypeId);

            IRepository<CurrencyType> rpct = new CurrencyTypeRepository(); // load listCurrencyType
            ViewData["listCurrencyType"] = new SelectList(rpct.GetAll(), "CurrencyTypeId", "CurrencyCode", availability.CurrencyType.CurrencyTypeId);

            return availability;
        }

        //
        // POST: /Availability/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                UpdateAvailability(collection);
                return RedirectToAction("Index", new { pageNumber = pageNumberAvailability });
            }
            else
            {
                return View();
            }
        }

        private void UpdateAvailability(FormCollection fc)
        {
            IAvailabilityRepository<Availability> rpIav = new AvailabilityRepository();
            Availability availability = rpIav.getAvailabilityById(long.Parse(fc["GetAvailabilityId"]));

            
            IRepository<Availability> rpav = new AvailabilityRepository();
            ICurrencyTypeRepository rpct = new CurrencyTypeRepository();
            IRepository<Hotel> rpht = new HotelRepository();
            IRoomTypeRepository<RoomType> rprt = new RoomTypeRepository();

            availability.CurrencyType = rpct.GetById(byte.Parse(fc["CurrencyType.CurrencyTypeId"]));
            availability.FromDate = CustomHelpers.parstFormatToYYYYMMDD(fc["FromDate"]);
            availability.ToDate = CustomHelpers.parstFormatToYYYYMMDD(fc["ToDate"]);
            availability.Hotel = rpht.GetById(int.Parse(fc["Hotel.HotelId"]));
            availability.IsHotDeal = fc["IsHotDeal"];
            availability.Price = decimal.Parse(fc["Price"]);
            availability.Quantity = int.Parse(fc["Quantity"]);
            availability.RoomType = rprt.GetById(byte.Parse(fc["RoomType.RoomTypeId"]));
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                availability.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                availability.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            availability.CreatedBy = fc["CreatedBy"];

            try
            {
                rpav.Update(availability);
            }
            catch
            {

            }
        }

        //
        // GET: /Availability/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteAvailability(id);
                return RedirectToAction("Index", new { pageNumber = pageNumberAvailability });
            }
            catch
            {
                return View();
            }
        }

        private void DeleteAvailability(int id)
        {
            Availability availability = new Availability();
            AvailabilityRepository iprH = new AvailabilityRepository();
            IRepository<Availability> iprHO = new AvailabilityRepository();
            availability = iprHO.GetById(id);
            try
            {
                iprHO.Delete(availability);
            }
            catch
            {

            }
        }

        //
        // POST: /Availability/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", new { pageNumber = pageNumberAvailability });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index1(string ValueAvailability, string TypeAvailability, int pageNumber = 1)
        {
            //List<Person> Persons = new List<Person>();
            //Persons.Add(new Person() { FirstName = "John", LastName = "Beckett", Email = "john@syncfusion.com" });
            //Persons.Add(new Person() { FirstName = "Ben", LastName = "Beckett", Email = "ben@syncfusion.com" });
            //Persons.Add(new Person() { FirstName = "Andrew", LastName = "Beckett", Email = "andrew@syncfusion.com" });
            //ViewBag.datasource = Persons;

            IRepository<Availability> iprH = new AvailabilityRepository();
            IList<Availability> listAvailability = new List<Availability>();
            
            listAvailability = iprH.GetAll();

            List<AvailabilityModel> listAvailabilityModel = new List<AvailabilityModel>();
            listAvailabilityModel = maptoEJ(listAvailability);

            ViewBag.datasource = listAvailabilityModel;

            return View();
        }

        private List<AvailabilityModel> maptoEJ(IList<Availability> listAvailability)
        {
            List<AvailabilityModel> listAvailabilityModel = new List<AvailabilityModel>();
            foreach (var item in listAvailability)
            {
                listAvailabilityModel.Add(new AvailabilityModel() { DetailURL = "<a href=\"/Availability/Edit/{idAvailability}\">Detail</a>".Replace("{idAvailability}", item.AvailabilityId.ToString()), FromDate = CustomHelpers.parstFormatToDate(item.FromDate), ToDate = CustomHelpers.parstFormatToDate(item.ToDate), HotelName = item.Hotel.Name, Number = item.Quantity, Price = item.Price, RoomName = item.RoomType.Name });
            }

            return listAvailabilityModel;
        }
    }
}
