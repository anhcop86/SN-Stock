using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHoderFrontEndV2.Ext;
using System.Text;
using ShareHoderFrontEndV2.Models;
using Newtonsoft.Json;
using System.IO;
using ShareHoderFrontEndV2.Helper;
using System;

namespace ShareHoderFrontEndV2.Controllers
{

    public class HomeController : BaseController
    {
        int pageSize = 3;

      
      
        public ActionResult Index(string searchInput, string txtFromDate, string txtToDate)
        {
            WriteJSON();
            

            if (Session["textboxSeachValue"] == null) 
                Session["textboxSeachValue"] = "";
            else
                searchInput = Session["textboxSeachValue"].ToString();
            if (Session["txtFromDate"] == null) 
                Session["txtFromDate"] = "";
            else
                txtFromDate = Session["txtFromDate"].ToString();
            if (Session["txtToDate"] == null) 
                Session["txtToDate"] = "";
            else
                  txtToDate = Session["txtToDate"].ToString();

            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            IRepository<Facility> repo = new FacilityRepository();

            #region KS  ua chuong
            IHotelRepository<Hotel> repoHotel = new HotelRepository();
            ViewData["ListMostView"] = repoHotel.getHolteMostview();
            #endregion

            //#region Tao ngay thang tim kiem
            //SelectList selectMonthOption = new SelectList(ApplicationHelper.getListMonthOption(), "ValueMonthYear", "TextMonthYear");
            //ViewData["MonthYear"] = selectMonthOption;
            //#endregion

            #region Diem den yeu thich: Favoutrist Destination 

            IRepository<FavouritDestination> repoFavouritDestination = new FavouritDestinationRepository();
            ViewData["FavouritDestination"] = repoFavouritDestination.GetAll();


            #endregion

            #region hodeal select
            //string[] locationStringSlipt;
            StringBuilder locationStringSlipt = new StringBuilder();
            StringBuilder resultInSelect = new StringBuilder();
            IList<Hotel> listhotelHodeal = repoHotel.getHotDealHotel();
            foreach (var item in listhotelHodeal)
            {
                locationStringSlipt.Append(item.Location);
                locationStringSlipt.Append(";");
            }
            IList<LocationHotdeal> LocationHotdealList = new List<LocationHotdeal>(); ;
            SelectListItem hh = new SelectListItem();
            foreach (var item in locationStringSlipt.ToString().Split(';'))
            {
                if (!resultInSelect.ToString().Contains(item))
                {
                    resultInSelect.Append(item);
                    resultInSelect.Append(";");
                    LocationHotdeal LocationHotdeal = new LocationHotdeal();
                    LocationHotdeal.Id = item;
                    LocationHotdeal.Name = item;
                    LocationHotdealList.Add(LocationHotdeal);
                }
            }

            
            ViewData["SelectedListHotelHotdeal"] = new SelectList(LocationHotdealList, "Id", "Name");

            #endregion
                        
            #region Hotdeal paging

            IList<Hotel> paginghotel = repoHotel.getHotDealHotel();
            IList<HotDealModel> hotDealModel = new List<HotDealModel>();

            foreach (var item in paginghotel)
            {
                HotDealModel sub = new HotDealModel(item.HotelId, item.Name);
                hotDealModel.Add(sub);
            }
            hotDealModel = hotDealModel.OrderBy(m => m.MinPrice).ToList<HotDealModel>();

            ViewData["countSearchHotdeal"] = hotDealModel.Count;
            ViewData["pageNo"] = 1;
            ViewData["returnvalueHotdeal"] = hotDealModel.Skip((1 - 1) * pageSize).Take(pageSize); ;
            #endregion

            return View(repo.GetAll());
        }


        public PartialViewResult SearchHotdeal(string SelectedHotdeal, string page)
        {
            if (Session["SelectedHotdealValue"] == null) 
                Session["SelectedHotdealValue"] = SelectedHotdeal;
            if (SelectedHotdeal != Session["SelectedHotdealValue"].ToString()) 
                Session["SelectedHotdealValue"] = SelectedHotdeal;
            if (page == null) page = "1";
            IHotelRepository<Hotel> rp = new HotelRepository();
            IList<Hotel> listhotel = rp.getHotDealHotel(ApplicationHelper.ConvertToNonUnicode(Session["SelectedHotdealValue"].ToString()));
            
            // order by hotdeal
            IList<HotDealModel> hotDealModel = new List<HotDealModel>();

            foreach (var item in listhotel)
            {
                HotDealModel sub = new HotDealModel(item.HotelId, item.Name);
                hotDealModel.Add(sub);
            }
            hotDealModel = hotDealModel.OrderBy(m => m.MinPrice).ToList<HotDealModel>();
            /* end orderby hotdeal */
            ViewData["countSearchHotdeal"] = hotDealModel.Count;
            ViewData["pageNo"] = page;
            ViewData["returnvalueHotdeal"] = hotDealModel.Skip((int.Parse(page) - 1) * pageSize).Take(pageSize);
            return PartialView("_HotdealPartial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult FetchLocationList(string data)
        {

            IList<LocationModel> listL = ApplicationHelper.ReadJson();
            //IList<LocationModel> listL = GetLocationPartFromhotel();

            //ApplicationHelper.WirteJson(listL);  write JSON to file
            //IList<LocationModel> listL = ApplicationHelper.ReadJson(); // read JSON from file to entity LocationModel

            var listFiter = listL.Where(m => m.LocationName.ToLower().Contains(ApplicationHelper.ConvertToNonUnicode(data.ToLower())));
            return Json(listFiter.ToList());
        }
        public static List<LocationModel> GetLocationPartFromhotel()
        {
            
            IRepository<Hotel> repoHotel = new HotelRepository();
            IList<Hotel> listHotel = repoHotel.GetAll();
             
            List<LocationModel> listLocation = new List<LocationModel>();
            List<string> listTemp = new List<string>();
            foreach (var item in listHotel)
            {
                foreach (var itemLocation in item.Location.ToString().Split(';'))
                {
                    if (!listTemp.Contains(itemLocation))
                    {
                        LocationModel location = new LocationModel(ApplicationHelper.ConvertToNonUnicode(itemLocation), itemLocation);
                        listTemp.Add(itemLocation);
                        listLocation.Add(location);
                    }
                }
            }
            
            return listLocation;
            
        }
        public void WriteJSON()
        {
            IList<LocationModel> listL = GetLocationPartFromhotel();
            ApplicationHelper.WirteJson(listL); 

            //return View();
        }

        public ActionResult PaymentInfo()
        {
            return View();
        }

        public ActionResult ChangeCurrentCulture(int id)
        {
            //
            // Change the current culture for this user.
            //
            SessionManager.CurrentCulture = id;
            //
            // Cache the new current culture into the user HTTP session. 
            //
            Session["CurrentCulture"] = id;
            //
            // Redirect to the same page from where the request was made! 
            //
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Terms()
        {
            return View();
        }
      
    }
}
