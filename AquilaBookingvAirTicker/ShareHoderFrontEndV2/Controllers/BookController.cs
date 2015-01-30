using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using System.Collections;
using ShareHoderFrontEndV2.Ext;
using ShareHoderFrontEndV2.Models;


namespace ShareHoderFrontEndV2.Controllers
{
    public class BookController : BaseController
    {
        //
        // GET: /Book/

        public ActionResult Index()
        {
            ICompareListDetailRepository<CompareListDetail> rp = new CompareListDetailRepository();
            IList<CompareListDetail> modal = rp.GetListCompareListDetailFromMember(1);

            return View(modal);
        }

        public ActionResult BookingDetails(int id)
        {
            List<CartItem> cartItem = ShoppingCart.Instance.Items;
            return View(cartItem);
        }

        private const string ImageURLAvataDefault = "/images/avatar_default.jpg";
        public ActionResult Details(int id)
        {

            ViewBag.AvataEmage = ImageURLAvataDefault;
            IRepository<Hotel> rp = new HotelRepository();            
            //get image of Hotel           

            IHotelImageRepository<HotelImage> iHotelImageR = new HotelImageRepository();
            HotelImage hotelImageMain = iHotelImageR.HotelImageofMain(id); // get main image of hotel
            //get name of room type
            //IList<HotelImage> imageListPathOfRoom = iHotelImageR.ImageOfHotelRoom(id); //get list image of type of room hotel
            IList<HotelImage> imageListPathOfRoom = iHotelImageR.GetAllImageOfHotel(id); 
            IList<HotelImageExtent> listHotelImageExtent = new List<HotelImageExtent>();

            foreach (var item in imageListPathOfRoom)
            {
                HotelImageExtent it = new HotelImageExtent();
                it.CreatedBy = item.CreatedBy;
                it.CreatedDate = item.CreatedDate;
                it.Hotel = item.Hotel;
                //it.HotelId = item.HotelId;
                it.HotelImageID = item.HotelImageID;
                it.ImagesStore = item.ImagesStore;
                //it.ImagesStoreId = item.ImagesStoreId;
                it.RoomTypeId = item.RoomTypeId;
                it.SortOrder = item.SortOrder;
                it.NameOfRoomType = returnNameofRoomtype(item.RoomTypeId);

                listHotelImageExtent.Add(it);
            }
            //end name of room type
            string imagepath;
            if (hotelImageMain != null)
                imagepath = hotelImageMain.ImagesStore.ImagePath;
            else
                imagepath = "default.jpg";

            ViewData["imagepath"] = imagepath;

            ViewData["imageListPathOfRoom"] = listHotelImageExtent; // danh sách hình của phòng
            ViewData["price"] = getPrice(id);
            //end get image of hotel

            #region Lay danh sach dich vu
            IRepository<Facility> repFacility = new FacilityRepository();
            IFacilityRepository ifr = new FacilityRepository();
            IList<Facility> facility = repFacility.GetAll();

            IList<FacilityExtent> listFacilityExtent = new List<FacilityExtent>();

            foreach (var item in facility)
            {
                FacilityExtent FacilityExtent = new FacilityExtent();
                FacilityExtent.CheckFacility = ifr.CheckFacility(id, item.FacilityId);
                FacilityExtent.CreatedDate = item.CreatedDate;
                FacilityExtent.FacilityId = item.FacilityId;
                FacilityExtent.ListHotelFacility = item.ListHotelFacility;
                FacilityExtent.Name = item.Name;

                listFacilityExtent.Add(FacilityExtent);
            }

            ViewData["listFacility"] = listFacilityExtent;

            ViewData["CountlistFacility"] = facility.Count;
            #endregion

            ///////////////////////////////////////
            #region get compare list when login

            IList<Hotel> listComparehotel = ApplicationHelper.getlistComparehotel() == null ? null : ApplicationHelper.getlistComparehotel();
            ViewData["listhotelcompare"] = listComparehotel;
            

            #endregion

      

            #region danh sach xem gan nhat

            IRepository<HotelViewlastest> rpHotelViewlastest = new HotelViewlastestRepository();
            //HotelViewlastest hotelViewlastest = new HotelViewlastest();
            List<HotelViewlastest> ListHotelViewlastest = (List<HotelViewlastest>)rpHotelViewlastest.GetAll();
            HotelViewlastest HotelViewlastest = rpHotelViewlastest.GetById(id);

            var checkHotel = ListHotelViewlastest.Where(m => m.HotelId == id).ToList();
            try
            {
                if (checkHotel.Count >= 1)
                {
                    HotelViewlastest.Review += 1;
                    rpHotelViewlastest.Delete(HotelViewlastest);
                    rpHotelViewlastest.Save(HotelViewlastest);
                }
                else
                {
                    HotelViewlastest = new HotelViewlastest();
                    HotelViewlastest.Review += 1;
                    HotelViewlastest.HotelId = id;
                    rpHotelViewlastest.Save(HotelViewlastest);
                }
            }
            catch (Exception)
            {
                throw;
            }

            ViewData["Review"] = HotelViewlastest.Review; // the number of review

            IHotelViewlastestRepository<Hotel> reposiHotelViewlastest = new HotelRepository();
            ViewData["reposiHotelViewlastest"] = reposiHotelViewlastest.getListHotelMostview();

            
            #endregion

            #region lay danh sach phong tu seesion ben tim kiem phong DestinationSearchResultControler
            //
            if (Session["listShowAvailability"] == null) Session["listShowAvailability"] = new List<Availability>();
            IList<Availability> listAvailability = (List<Availability>)Session["listShowAvailability"];
            ViewData["listAvailability"] = listAvailability.Where(m => m.Hotel.HotelId.Equals(id) ).ToList();


            // End
            #endregion

            Hotel hotel = rp.GetById(id);
            hotel.LongDesc = System.Net.WebUtility.HtmlDecode(hotel.LongDesc);
            Session["textboxSeachValue"] = hotel.Name;

            ViewData["hotelIdResearch"] = id;
            Session["HotelNameofShortSeach"] = hotel.Name;
            ViewData["HotelNameForMap"] = hotel.Name;
            ViewData["HotelAddressForMap"] = hotel.HotelAddress;
            // load comment
            //IHotel
            IHotelCommentRepository<HotelComment> irHComment = new HotelCommentRepository();
            ViewData["HotelCommentData"] = irHComment.getListHotelCommentFromHotelId(id);
            ViewData["HotelCommentDataCount"] = (ViewData["HotelCommentData"] as List<HotelComment>).Count;

            IHotelRatingRepository hoteRatingRepo = new HotelRatingRepository();
            ViewData["HotelRatingAvg"] = decimal.Round(hoteRatingRepo.getAverageofRatingWithHotel(id),1);
            // end comment
            //Session["hotelId"] = id;

            #region load lại compare list

            if (Request.IsAuthenticated)
            {
                var listcompare = new List<int>();


                ICompareListDetailRepository<CompareListDetail> rpCompareListDetai = new CompareListDetailRepository();
                IRepository<CompareList> IrpCompareList = new CompareListRepository();
                IList<CompareListDetail> ListCompareListDetail = rpCompareListDetai.GetListCompareListDetailFromMember(((Membership)@Session["LoginObject"]).MemberId);


                foreach (var item in ListCompareListDetail)
                {
                    listcompare.Add(item.Hotel.HotelId);
                }

                ViewData["listcompareLogin"] = listcompare;
            }

            #endregion

            return View(hotel);
        }

        [HttpPost]
        public PartialViewResult UploadComment(FormCollection fc)
        {
            string comment = fc["txtContentCommentView"];
            int hotelId = int.Parse(fc["HoteIdComment"]);
            IHotelCommentRepository<HotelComment> irHComment = new HotelCommentRepository();
            IRepository<HotelComment> irHCommentUpdate = new HotelCommentRepository();
            // thêm comment vào database
            HotelComment hotelComment = new HotelComment();
            hotelComment.HotelId = hotelId;
            hotelComment.MemberId = (@Session["LoginObject"] as Membership).MemberId;
            hotelComment.Rate = 1;
            hotelComment.comment = comment;
            hotelComment.CommentDate = DateTime.Now;

            try
            {
                irHCommentUpdate.Save(hotelComment);
            }
            catch (Exception)
            {
                
                throw;
            }
            //

            IList<HotelComment> returList = new List<HotelComment>();
            returList.Add(hotelComment);
            ViewData["HotelCommentData"] = returList;
            ViewData["HotelCommentDataCount"] = 1;
            return PartialView("_PartialAjaxCommentList"); 
        }

        public ActionResult DeleteComment(long CommentId)
        {
            string result = "S";
            IHotelCommentRepository<HotelComment> fr = new HotelCommentRepository();
            try
            {
                fr.deleteCommnet(CommentId);
            }
            catch (Exception)
            {
                result = "E";
                throw;
            }
            return Content(result);
        }

        //public PartialViewResult returnCommentWhenAdd()  //ajax of comment
        //{
        //    return PartialView("_CommentMemberPartial"); 
        //}

        public string returnNameofRoomtype(byte? roomTypeId)
        {
            string name = Resources.Resource.hotelImage;
            IRoomTypeRepository<RoomType> roomTypeRes = new RoomTypeRepository();
            RoomType rmType = roomTypeRes.GetById(roomTypeId);
            if (rmType != null)
            {
                name = rmType.Name;
            }
            return name;
        }
        public decimal? getPrice(int idHotel)
        {
            decimal? price = Decimal.Zero;
            IAvailabilityRepository<Availability> irp = new AvailabilityRepository();

            price = irp.getPrice(idHotel);
            
            return price;
        }

        public PartialViewResult CheckPriceOFRoomSearch(string txtFromDateRecheck, string txtToDateRecheck, int hotelIdSearchRoom) //ajax of CheckPriceOfRoomSearch
        {
            IList<Availability> listShowAvailability = new List<Availability>();
            listShowAvailability = searchRoom(txtFromDateRecheck, txtToDateRecheck, hotelIdSearchRoom); // goi ham tim kiem phong                      

            ViewData["listAvailability"] = listShowAvailability;
            return PartialView("_AvailabilityPartial");

        }

        public IList<Availability> searchRoom(string txtFromDate, string txtToDate, int hotelIdSearchRoom)
        {
            Session["txtFromDate"] = txtFromDate;
            Session["txtToDate"] = txtToDate;
            IList<HotelIdAndRoomtypeId> listSearchAvailability = new List<HotelIdAndRoomtypeId>();  
            IAvailabilityRepository<Availability> rpIAvailabilityRepository = new AvailabilityRepository();
            IBookingDetailRepository<BookingDetail> rpIBookingDetailRepository = new BookingDetailRepository();
            IRepository<Hotel> rpHotel = new HotelRepository();
            RoomTypeRepository roomTypeRepository = new RoomTypeRepository();
            // tim khiem phong trong bang Availability
            listSearchAvailability = rpIAvailabilityRepository.searchHotel(hotelIdSearchRoom, ApplicationHelper.ParseDatetoInt(txtFromDate), ApplicationHelper.ParseDatetoInt(txtToDate));

            IList<Availability> listShowAvailability = new List<Availability>(); // show phong trong sau khi tru voi Booking
            foreach (var item in listSearchAvailability)
            {
                Availability availability = new Availability();
                availability.Quantity = item.Quantity;
                availability.AvailabilityId = item.AvailabilityId;
                availability.RoomType = roomTypeRepository.GetById(item.RoomTypeId);
                availability.Hotel = rpHotel.GetById(item.HotelId);
                availability.Price = item.Price;

                // tim kiem cac phong da booking tuong ung voi id khách sạn và Id phòng đang tìm
                IList<SearchBooking> listBookingDetail = rpIBookingDetailRepository.searchBooking(item.HotelId, item.RoomTypeId, ApplicationHelper.ParseDatetoInt(txtFromDate), ApplicationHelper.ParseDatetoInt(txtToDate));
                if (listBookingDetail.Count > 0)
                {
                    foreach (var booking in listBookingDetail)
                    {
                        availability.Quantity -= booking.Quantity;  // khach sạn đã đặt phòng thì trừ đi số phòng trống
                    }
                    listShowAvailability.Add(availability);
                }
                else
                {
                    listShowAvailability.Add(availability);
                }
               
            }

            return listShowAvailability;
        }

        [HttpGet]
        public string GetImageById(Int32 hotelImageId)
        {
            String imagepath = "default.jpg";

            IRepository<HotelImage> rp = new HotelImageRepository();
            imagepath = rp.GetById(hotelImageId).ImagesStore.ImagePath;
            return ShareHoderFrontEndV2.Ext.ApplicationHelper.ImageDirectoryHotel + "/" + imagepath;
        }

        [HttpPost]
        public JsonResult SendRating(byte r, int h)
        {
            IRepository<HotelRating> rp = new HotelRatingRepository();
            HotelRating hotelRating = new HotelRating();
            hotelRating.HotelId = h;
            hotelRating.Rate = r;
            hotelRating.Type = 1;

            try
            {
                rp.Save(hotelRating);
            }
            catch (Exception)
            {
                return Json("<br />Lỗi! Không update được vào database !");
                throw;
            }

            return Json("<br />You rated " + r + " star(s), thanks !");
        }

    }
}
