using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;
using System.Web.Providers.Entities;

namespace ShareHoderFrontEndV2.Controllers
{
    public class HotelController : BaseController
    {
        //session save        
        // GET: /Hotels/

        public ActionResult Index()
        {
            IRepository<Hotel> rp = new HotelRepository();
            var listHotel = rp.GetAll();
         
            return View(listHotel);
        }
        public ActionResult Details(int id)
        {
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
                //it.HotelId = item.Hotel.HotelId;
                it.HotelImageID = item.HotelImageID;
                it.ImagesStore = item.ImagesStore;
                //it.ImagesStoreId = item.ImagesStore.ImagesStoreId;
                it.RoomTypeId = item.RoomTypeId;
                it.SortOrder = item.SortOrder;
                //it.NameOfRoomType = returnNameofRoomtype(item.RoomTypeId);
                
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

            Hotel hotel = rp.GetById(id);

            //Session["hotelId"] = id;
            return View(hotel);
        }
        public string returnNameofRoomtype(byte? roomTypeId)
        {
            string name = string.Empty;
            IRoomTypeRepository<RoomType> roomTypeRes = new RoomTypeRepository();

            name = roomTypeRes.GetById(roomTypeId).Name;

            return name;
        }
        public decimal? getPrice(int idHotel)
        {
            decimal? price = Decimal.Zero;
            IAvailabilityRepository<Availability> irp = new AvailabilityRepository();

            price = irp.getPrice(idHotel);



            return price;
        }

    }
}
