using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHoderFrontEndV2.Ext;
using System.Collections;

namespace ShareHoderFrontEndV2.Controllers
{

    public class DestinationSearchResultController : BaseController
    {
        //
        // GET: /DestinationSearchResult/

        int pageSize = ApplicationHelper.pageSize;
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(string searchInput, string txtFromDate, string txtToDate, string FavouritDestination)
        {


            

            Session["textboxSeachValue"] = searchInput.TrimEnd().TrimStart().Trim();
            Session["txtFromDate"] = txtFromDate;
            Session["txtToDate"] = txtToDate;

            if (Session["textboxSeachValue"] == null) Session["textboxSeachValue"] = "";
            if (Session["txtFromDate"] == null) Session["txtFromDate"] = "";
            if (Session["txtToDate"] == null) Session["txtToDate"] = "";

            //ViewData["LocationTag"] = ApplicationHelper.LocationTag(); // tag of location

            IList<HotelExtent> hotelwithImage = new List<HotelExtent>();
            IHotelRepository<Hotel> rp = new HotelRepository();
            //IList<Hotel> listhotel = rp.searchHolte("Ho chi minh");

            #region find list hotels with fromdate to todate
            IAvailabilityRepository<Availability> rpIAvailabilityRepository = new AvailabilityRepository();
            IList<HotelIdAndRoomtypeId> listSearchAvailability;
            if (string.IsNullOrEmpty(txtFromDate) || string.IsNullOrEmpty(txtToDate) || !string.IsNullOrEmpty(FavouritDestination))
            {
                listSearchAvailability = new List<HotelIdAndRoomtypeId>();
            }
            else
            {
                listSearchAvailability = rpIAvailabilityRepository.searchHotel(ApplicationHelper.ConvertToNonUnicode(Session["textboxSeachValue"].ToString().Trim()), ApplicationHelper.ParseDatetoInt(txtFromDate), ApplicationHelper.ParseDatetoInt(txtToDate));
            }

            IList<Availability> listShowAvailability = new List<Availability>();

            IBookingDetailRepository<BookingDetail> rpIBookingDetailRepository = new BookingDetailRepository();
            RoomTypeRepository RoomTypeRepository = new RoomTypeRepository();
            IList<Int32> listidHotel = new List<Int32>();
            IList<Hotel> listhotel = new List<Hotel>();
            IRepository<Hotel> rpHotel = new HotelRepository();
            // tim duoc ngay dat phong trong khoang thoi gian do
            if (string.IsNullOrEmpty(txtFromDate) || string.IsNullOrEmpty(txtToDate) || !string.IsNullOrEmpty(FavouritDestination) || listSearchAvailability.Count == 0)
            {
                Session["listShowAvailability"] = new List<Availability>();
                listhotel = rp.searchHolte(ApplicationHelper.ConvertToNonUnicode(Session["textboxSeachValue"].ToString().Trim()));
                if (listhotel.Count == 0)
                {
                    string[] a = Session["textboxSeachValue"].ToString().Split(' ');
                    listhotel = rp.searchHolte(ApplicationHelper.ConvertToNonUnicode(a[0].Trim()));
                }
            }
            else
            {

                foreach (var item in listSearchAvailability)
                {
                    Availability availability = new Availability();
                    availability.Quantity = item.Quantity;

                    IList<SearchBooking> listBookingDetail = rpIBookingDetailRepository.searchBooking(item.HotelId, item.RoomTypeId, ApplicationHelper.ParseDatetoInt(Session["txtFromDate"].ToString()), ApplicationHelper.ParseDatetoInt(Session["txtToDate"].ToString()));

                    availability.AvailabilityId = item.AvailabilityId;
                    availability.RoomType = RoomTypeRepository.GetById(item.RoomTypeId);
                    availability.Hotel = rpHotel.GetById(item.HotelId);
                    availability.Price = item.Price;


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
                    if (!listidHotel.Contains(item.HotelId))
                    {
                        //if (availability.Quantity > 0)
                        //{
                        listidHotel.Add(item.HotelId);
                        //}
                    }
                }

                //save list Avaibability into session with the changed of quantity
                if (listShowAvailability.Count == 0)
                {

                }


                Session["listShowAvailability"] = listShowAvailability;

            #endregion
               
                foreach (var item in listidHotel)
                {

                    Hotel lasthotel = rpHotel.GetById(item);
                    listhotel.Add(lasthotel);
                }
            }

            //IList<Hotel> listhotel = rp.searchHolte(ApplicationHelper.ConvertToNonUnicode(Session["textboxSeachValue"].ToString()));

            IHotelCommentRepository<HotelComment> irHC = new HotelCommentRepository();
            foreach (var item in listhotel)
            {

                HotelExtent itm = new HotelExtent();
                itm.CreatedBy = item.CreatedBy;
                itm.CreatedDate = item.CreatedDate;
                itm.HotelAddress = item.HotelAddress;
                itm.HotelId = item.HotelId;
                itm.ImagePath = getImage(item.HotelId);
                itm.ListAvailability = item.ListAvailability;
                itm.ListAvailabilityHist = item.ListAvailabilityHist;
                itm.ListCompareListDetail = item.ListCompareListDetail;
                itm.ListHotelFacility = item.ListHotelFacility;
                itm.ListHotelImage = item.ListHotelImage;
                itm.Location = item.Location;
                itm.LongDesc = item.LongDesc;
                itm.Name = item.Name;
                itm.Province = item.Province;
                itm.ProvinceId = item.ProvinceId;
                itm.ShortDesc = item.ShortDesc;
                itm.Star = item.Star;
                itm.price = getPrice(item.HotelId);
                itm.review = getReview(item.HotelId);
                itm.CountComment = irHC.countComment(item.HotelId);

                hotelwithImage.Add(itm);
            }

            // lay danh sach thang va nam trong tim kiem
            //SelectList selectMonthOption = new SelectList(ApplicationHelper.getListMonthOption(), "ValueMonthYear", "TextMonthYear");
            //ViewData["MonthYear"] = selectMonthOption;

            // end

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

            //paging test by linq
            //ViewData["countSearchHotdeal"] = paginghotel.Count;
            //ViewData["pageNo"] = 1;
            hotelwithImage = hotelwithImage.OrderBy(m => m.price).ToList<HotelExtent>();
            @Session["hotelwithImagePaging"] = hotelwithImage; // lưu kết quả tìm kiếm của KS vào session để phân trang bên dưới HotelWithPaging
            
            int pageNumber = 1;
            if (hotelwithImage.Count > 0) // lần đầu tiên tìm kiếm luôn luôn load trang 1
            {
                ViewData["hotelwithImagePagingCount"] = hotelwithImage.Count;
                hotelwithImage = hotelwithImage.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<HotelExtent>();
                //end test
            }
            @ViewData["PriceSortActive"] = "DefaultSort";
            @ViewData["SortOrderbyPrice"] = "SortClickImage";
            @ViewData["StarSoftActive"] = "DefaultSort";
            @ViewData["SortOrderbyStar"] = "SortClickImage";

            @ViewData["PriceSortActiveTitle"] = "Price";
            @ViewData["SortOrderbyPriceTitle"] = "ASC";
            @ViewData["StarSoftActiveTitle"] = "";
            @ViewData["SortOrderbyStarTitle"] = "ASC";


            return View("Index", hotelwithImage);

        }

        public ActionResult HotelWithPaging(int pageNumber, string price, string orderbyprice, string star, string orderbystar)
        {
            #region fill class css


            //2
            if (orderbyprice == "ASC")
            {
                @ViewData["SortOrderbyPrice"] = "SortClickImage";
            }
            else
            {
                @ViewData["SortOrderbyPrice"] = "DefaultSortImage";
            }
            //3

            //4
            if (orderbystar == "ASC")
            {
                @ViewData["SortOrderbyStar"] = "SortClickImage";

            }
            else
            {
                @ViewData["SortOrderbyStar"] = "DefaultSortImage";
            }
            @ViewData["PriceSortActiveTitle"] = price;
            @ViewData["SortOrderbyPriceTitle"] = orderbyprice;
            @ViewData["StarSoftActiveTitle"] = star;
            @ViewData["SortOrderbyStarTitle"] = orderbystar;



            #endregion


            IList<HotelExtent> hotelwithImage = @Session["hotelwithImagePaging"] as IList<HotelExtent>;

            if (orderbyprice == "ASC" && orderbystar == "ASC")
            {
                hotelwithImage = (from x in hotelwithImage
                                  orderby x.price ascending, x.Star ascending
                                  select x).ToList<HotelExtent>();
            }
            else if (orderbyprice == "ASC" && orderbystar == "DESC")
            {
                hotelwithImage = (from x in hotelwithImage
                                  orderby x.price ascending, x.Star descending
                                  select x).ToList<HotelExtent>();
            }
            else if (orderbyprice == "DESC" && orderbystar == "ASC")
            {
                //hotelwithImage = hotelwithImage.OrderByDescending(m => m.price).ToList<HotelExtent>();
                hotelwithImage = (from x in hotelwithImage
                                  orderby x.price descending, x.Star ascending
                                  select x).ToList<HotelExtent>();
            }
            else if (orderbyprice == "DESC" && orderbystar == "DESC")
            {
                hotelwithImage = (from x in hotelwithImage
                                  orderby x.price descending, x.Star descending
                                  select x).ToList<HotelExtent>();
            }

            ViewData["hotelwithImagePagingCount"] = hotelwithImage.Count;
            hotelwithImage = hotelwithImage.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<HotelExtent>();
            return View("Index", hotelwithImage);
        }

        
        public string getImage(int idHotel)
        {
            
            IHotelImageRepository<HotelImage> iHotelImageR = new HotelImageRepository();
            HotelImage hotelImageMain = iHotelImageR.HotelImageofMain(idHotel); // get main image of hotel
            string imagepath;
            if (hotelImageMain != null)
                imagepath = hotelImageMain.ImagesStore.ImagePath;
            else
                imagepath = "default.jpg";


            return imagepath;

        }

        public decimal? getPrice(int idHotel)
        {
            decimal? price = Decimal.Zero ;
            IAvailabilityRepository<Availability> irp = new AvailabilityRepository();
            price = irp.getPrice(idHotel);
            return price;
        }

        public int getReview(int idHotel)
        {
            int review = 0;
            IRepository<HotelViewlastest> reviewRps = new HotelViewlastestRepository();
            HotelViewlastest hotelViewlastest = reviewRps.GetById(idHotel);
            
            if (hotelViewlastest != null)
            {
                review = hotelViewlastest.Review;
            }
            return review;
        }

    }
}
