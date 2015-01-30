using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using AquilaAd.Ext;
using System.Web.Helpers;
using AquilaAd.Models;
using System.IO;
using System.Text;

namespace AquilaAd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        int pageSize = ApplicationHelper.pageSize;

       
        public ActionResult Index()
        {
            List<AvailabilityModel> listAvailabilityModel = new List<AvailabilityModel>();
            IList<HotelExtent> hotelwithImage = getListHotel();

            List<HotelModelEJ> hotelHotelModelEJ = new List<HotelModelEJ>();
            hotelHotelModelEJ = maptoEJ(hotelwithImage);

            ViewBag.datasource = hotelHotelModelEJ;

            return View();
            //test svn
        }

        

        protected IList<HotelExtent> getListHotel(int pageNumber)
        {
            IList<Hotel> listhotel = new List<Hotel>();
            IList<HotelExtent> hotelwithImage = new List<HotelExtent>();
            IRepository<Hotel> rpHotel = new HotelRepository();
            IHotelRepository<Hotel> irHotelI = new HotelRepository();
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as HotelOwner) != null)
            {
                listhotel = irHotelI.getHotelFilterByOwnerId((@Session["UserType"] as HotelOwner).HotelOwnerId);
                int countHotel = listhotel.Count;
                ViewData["CountAllHotel"] = ApplicationHelper.PageNumber(countHotel == 0 ? 1 : countHotel);
                listhotel = listhotel.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList<Hotel>();

            }
            else
            {
                int countHotel = irHotelI.countAllHotel();
                ViewData["CountAllHotel"] = ApplicationHelper.PageNumber(countHotel == 0 ? 1 : countHotel);
                int pageNumberHotel = (pageNumber - 1) * pageSize;
                listhotel = irHotelI.getListHotelwithPaging(pageNumberHotel, pageSize);
            }


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

                hotelwithImage.Add(itm);
            }

            return hotelwithImage;
        }
        public ActionResult New_Hotel() // load new interface hotel
        {
            GetQualInViewBag();
            HotelModel HotelModel = new HotelModel();
            return View(HotelModel);
        }

        #region Add New Hotel
        [HttpPost, ValidateInput(false)]
        public ActionResult SaveNew(FormCollection fc, HotelModel htm)
        {
            GetQualInViewBag();
            if (ModelState.IsValid)// check validation
            {
                
                AddHotel(fc, htm);
                return RedirectToAction("Index", new { pageNumber = @Session["pageNumberIndex"] });
            }
            else
            {
                return View("New_Hotel");
            }
        }

        private static void AddHotel(FormCollection fc, HotelModel htm)
        {
            IRepository<Province> iprP = new ProvinceRepository();
            IRepository<Hotel> iprH = new HotelRepository();
            Hotel ht = new Hotel();
            fc["LongDesc"] = System.Net.WebUtility.HtmlDecode(fc["LongDesc"]);
            ht.Name = fc["Name"];
            ht.HotelAddress = fc["HotelAddress"];
            ht.Star = short.Parse(fc["Star"]);
            ht.ShortDesc = fc["ShortDesc"];
            ht.LongDesc = fc["LongDesc"];
            //ht.LongDesc = fc["LongDesc"];
            ht.Location = fc["Location"];
            ht.Province = iprP.GetById(int.Parse(fc["ProvinceId"]));
            ht.MostView = fc["MostView"];
            ht.CreatedDate = fc["CreatedDate"].Replace("-", "");
            ht.CreatedBy = fc["CreatedBy"];
            ht.BookingCondition = System.Net.WebUtility.HtmlDecode(htm.BookingCondition);
            ht.Display = fc["Display"];
            try
            {
                if (fc["SelectHotelOwner"] != null)
                {
                    ht.HotelOwnerId = int.Parse(fc["SelectHotelOwner"]);
                }
                iprH.Save(ht);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private static void AddHotel(FormCollection fc)
        {
            IRepository<Province> iprP = new ProvinceRepository();
            IRepository<Hotel> iprH = new HotelRepository();
            Hotel ht = new Hotel();          

            ht.Name = fc["Name"];
            ht.HotelAddress = fc["HotelAddress"];
            ht.Star = short.Parse(fc["Star"]);
            ht.ShortDesc = fc["ShortDesc"];
            ht.LongDesc = System.Net.WebUtility.HtmlDecode(fc["LongDesc"]);
            //ht.LongDesc = fc["LongDesc"];
            ht.Location = fc["Location"];
            ht.Province = iprP.GetById(int.Parse(fc["ProvinceId"]));
            ht.MostView = fc["MostView"];
            ht.CreatedDate = fc["CreatedDate"].Replace("-", "");
            ht.CreatedBy = fc["CreatedBy"];
            ht.BookingCondition = System.Net.WebUtility.HtmlDecode(fc["BookingCondition"]);
            ht.Display = fc["Display"];
            try
            {     
                if (fc["SelectHotelOwner"] != null)
                {
                    ht.HotelOwnerId = int.Parse( fc["SelectHotelOwner"]);
                }
                iprH.Save(ht);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        public ActionResult Edit_Detail_Hotel(string idhotel) // load edit interface hotel
        {
           
            HotelModel ht = LoadViewDataForEdit(idhotel);
            ht.LongDesc = System.Net.WebUtility.HtmlDecode(ht.LongDesc);
            ht.BookingCondition = System.Net.WebUtility.HtmlDecode(ht.BookingCondition);
           // ht.LongDesc = HtmlHelper.     
            return View(ht);
        }
        #region LoadViewDataForEdit
        private HotelModel LoadViewDataForEdit(string idhotel)
        {
            HotelModel ht = new HotelModel();
            IRepository<Hotel> htrp = new HotelRepository();
            Hotel edithotel = new Hotel();            
            // get all province and show in select option
            IRepository<Province> pvrp = new ProvinceRepository();
            IList<Province> listAllProvince = pvrp.GetAll();
            // end get all province in select option       
            //get list the star
            StarModel sm = new StarModel();
            IList<StarModel> liststar = sm.listAllStar();            
            // end
            //get list the most view hotel
            MostViewHotel MostViewHotel = new MostViewHotel();
            IList<MostViewHotel> listsMostViewHotel = MostViewHotel.listAllMostViewHotel();
            IList<Display> listsDisplay = Display.listAllDisplay();
            edithotel = htrp.GetById(int.Parse(idhotel));

            if (@Session["UserType"] != null && edithotel.HotelOwnerId != (@Session["UserType"] as HotelOwner).HotelOwnerId)
            {
                return new HotelModel();
            }

            ViewData["listAllProvince"] = new SelectList(listAllProvince, "ProvinceId", "Name", edithotel.Province.ProvinceId);
            ViewData["StarHotel"] = new SelectList(liststar, "Id", "Name", edithotel.Star);
            ViewData["createdDate"] = CustomHelpers.parstFormatToYYYY_MM_DD(edithotel.CreatedDate);
            ViewData["MostViewHotel"] = new SelectList(listsMostViewHotel, "id", "name", edithotel.MostView);
            ViewData["DislayConditon"] = new SelectList(listsDisplay, "id", "name", edithotel.Display);
            ht = CustomHelpers.MapHotelModel_Hotel(edithotel);

            // danh muc tai khoản quản lý khách sạn
            IList<HotelOwner> listHotelOwner = new List<HotelOwner>();
            IRepository<HotelOwner> irpho = new HotelOwnerRepository();
            listHotelOwner = irpho.GetAll();
            ViewData["listHotelOwner"] = new SelectList(listHotelOwner, "HotelOwnerId", "FullName", edithotel.HotelOwnerId);

            IList<Facility> FacilityList = new List<Facility>();
            FacilityList = GetFacilityList();

            IList<FacilityExtent> listFacilityExtent = GetListFacilityExtent(idhotel, FacilityList);
            
            ViewData["servicesOfHotel"] = listFacilityExtent;

            // load loại phòng cho danh muc hinh
            IRepository<RoomType> iprRoomType = new RoomTypeRepository();
            IList<RoomType> listRoomType = new List<RoomType>();
            listRoomType = iprRoomType.GetAll();

            RoomType addRoomtypeMain = new RoomType();
            addRoomtypeMain.RoomTypeId = 0;
            addRoomtypeMain.Name = "Hình đại diện";

            listRoomType.Insert(0, addRoomtypeMain);

            ViewData["listRoomType"] = new SelectList(listRoomType, "RoomTypeId", "Name");
            //end

            return ht;
        }

        private static IList<FacilityExtent> GetListFacilityExtent(string idhotel, IList<Facility> FacilityList)
        {
            IList<FacilityExtent> listFacilityExtent = new List<FacilityExtent>();
            IFacilityRepository ifr = new FacilityRepository();
            foreach (var item in FacilityList)
            {
                FacilityExtent FacilityExtent = new FacilityExtent();
                FacilityExtent.CheckFacility = ifr.CheckFacility(int.Parse(idhotel), item.FacilityId);
                FacilityExtent.CreatedDate = item.CreatedDate;
                FacilityExtent.FacilityId = item.FacilityId;
                FacilityExtent.ListHotelFacility = item.ListHotelFacility;
                FacilityExtent.Name = item.Name;

                listFacilityExtent.Add(FacilityExtent);
            }
            return listFacilityExtent;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveEdit(FormCollection fc)
        {
            UpdateHotel(fc);
            if (ModelState.IsValid) // check validation
            {
                return RedirectToAction("Index" , new { pageNumber = @Session["pageNumberIndex"] });// success and return Index page
            }
            else
            {
                return View("SaveEdit"); // return with don't action
            }
        } 
           
        private void UpdateHotel(FormCollection fc)
        {
            IRepository<Province> iprP = new ProvinceRepository();
            IRepository<Hotel> iprH = new HotelRepository();

            int hotelid = int.Parse(fc["GetHotelId"]);
            Hotel ht = iprH.GetById(hotelid);
            ht.Name = fc["Name"];
            ht.HotelAddress = fc["HotelAddress"];
            ht.Star = short.Parse(fc["Star"]);
            ht.ShortDesc = fc["ShortDesc"];
            ht.LongDesc = System.Net.WebUtility.HtmlEncode(fc["LongDesc"]);
            ht.Location = fc["Location"];
            ht.Province = iprP.GetById(int.Parse(fc["ProvinceId"]));
            ht.MostView = fc["MostView"];
            ht.BookingCondition = System.Net.WebUtility.HtmlEncode(fc["BookingCondition"]);
            ht.Display = fc["Display"];
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                ht.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                ht.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            ht.CreatedBy = fc["CreatedBy"];
            try
            {
                if ((@Session["UserType"] as HotelOwner) != null)
                {
                    // no change
                }
                else if (fc["SelectHotelOwner"] != "")
                {
                    ht.HotelOwnerId = int.Parse(fc["SelectHotelOwner"]);
                }
                else
                {
                    ht.HotelOwnerId = null;
                }
                iprH.Update(ht);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        public ActionResult CancelHotel(FormCollection fc)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void Delete(string id)
        {
            
            if (!string.IsNullOrEmpty(id))
            {
                //Delete hotel with array id
                IRepository<Hotel> rpHotel = new HotelRepository();
                string[] listintHotel = id.Split(';');
                if (listintHotel.Length > 0)
                {
                    foreach (var item in listintHotel)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            try
                            {
                                rpHotel.Delete(rpHotel.GetById(int.Parse(item)));
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                }
            }
        }

       

        public decimal? getPrice(int idHotel)
        {
            decimal? price = Decimal.Zero;
            IAvailabilityRepository<Availability> irp = new AvailabilityRepository();

            price = irp.getPrice(idHotel);
            return price;
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
        private void GetQualInViewBag()
        {
            #region get list province
            // get all province and show in select option
            IRepository<Province> pvrp = new ProvinceRepository();
            IList<Province> listAllProvince = pvrp.GetAll();
            // end get all province in select option

            #endregion

            #region get list the star
            StarModel sm = new StarModel();
            IList<StarModel> liststar = sm.listAllStar();
            #endregion
            // end

            #region get list the most view hotel
            MostViewHotel MostViewHotel = new MostViewHotel();
            IList<MostViewHotel> listsMostViewHotel = MostViewHotel.listAllMostViewHotel();
            #endregion

            ViewData["listAllProvince"] = new SelectList(listAllProvince, "ProvinceId", "Name");
            ViewData["star"] = new SelectList(liststar, "Id", "Name");
            ViewData["MostViewHotel"] = new SelectList(listsMostViewHotel, "Id", "Name");
            ViewData["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            IList<Display> listsDisplay = Display.listAllDisplay(); 
            ViewData["DislayConditon"] = new SelectList(listsDisplay, "id", "name", "Y");
            // load danh muc dich vu
            IList<HotelOwner> listHotelOwner = new List<HotelOwner>();
            IRepository<HotelOwner> irpho = new HotelOwnerRepository();
            listHotelOwner = irpho.GetAll();
            ViewData["listHotelOwner"] = new SelectList(listHotelOwner, "HotelOwnerId", "FullName");

            // end

          
        }

        [HttpPost]
        public string UploadImage(HttpPostedFileBase uploadfileid, FormCollection fc)
        {
            string result = string.Empty;
            if (uploadfileid == null)
            {
                return "Chưa chọn file hình để upload ";
            }
            if (fc["SelectRoomType"] == "")
            {
                return "Upload lỗi vì chưa chọn loại phòng thích hợp";
            }
            else
            {
                byte roomType = byte.Parse(fc["SelectRoomType"]);
                int holtelid = int.Parse(fc["GetHotelIdUploadImage"]);
                //HttpPostedFileBase uploadfileid1 =  fc["uploadfileid"] as HttpPostedFileBase ;
                if (uploadfileid != null)
                {
                    string path = ApplicationHelper.ImageDirectoryHotel;

                    if (uploadfileid.ContentLength > 200240)
                    {
                        return "Kích thướt file hình phải nhỏ hơn 200 KB";                        
                    }

                    var supportedTypes = new[] { "jpg", "jpeg", "png" };

                    var fileExt = System.IO.Path.GetExtension(uploadfileid.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {                        
                        return "Chỉ hỗ trợ loại file hình (jpg, jpeg, png)";                       
                    }

                    //uploadfileid.SaveAs(path + "131212.jpg");
                    UpLoadFileIndatebase(uploadfileid, path, fileExt, holtelid, roomType);
                }
                result = GetImageofRoomType(holtelid, roomType) + "<br/> <span style='color:green'>Upload hình thành công</span>";
            }
           

            return result;
        }

        private void UpLoadFileIndatebase(HttpPostedFileBase uploadfileid, string path, string fileExtent, int holtelid, byte roomType)
        {
            string filename = Guid.NewGuid().ToString();
            uploadfileid.SaveAs(path + filename + "." + fileExtent);
            
                IHotelImageRepository<HotelImage> iHotelImageR = new HotelImageRepository();
                IRepository<ImagesStore> irImagesStore = new ImagesStoreRepository();
                IRepository<Hotel> irHotel = new HotelRepository();
                IRepository<HotelImage> iRHotelImage = new HotelImageRepository();

                ImagesStore imagesStore = new ImagesStore();
                imagesStore.ImagePath = filename + "." + fileExtent;
                imagesStore.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
                imagesStore.CreatedBy = "Aquila";

                try
                {
                    irImagesStore.Save(imagesStore);
                    long ImagesStoreId = imagesStore.ImagesStoreId; // lấy id ImagesStore mới vừa insert vào database
                    HotelImage hotelImage = new HotelImage();
                    hotelImage.ImagesStore = imagesStore;
                    if (roomType == 0)
                    {
                        hotelImage.RoomTypeId = null;
                    }
                    else
                    {
                        hotelImage.RoomTypeId = roomType;
                    }
                    hotelImage.Hotel = irHotel.GetById(holtelid);
                    hotelImage.SortOrder = 1;
                    hotelImage.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
                    hotelImage.CreatedBy = "Aquila";

                    iRHotelImage.Save(hotelImage);
                }
                catch (Exception)
                {
                    throw;
                }


      
        }

     
        private IList<Facility> GetFacilityList()
        {
            IRepository<Facility> iprH = new FacilityRepository();
            IList<Facility> FacilityList = new List<Facility>();
            FacilityList = iprH.GetAll();

            return FacilityList;
        }

        public ActionResult UpdateServicesOfHotel(short servicesid, int hotelid)
        {
            string result = "S";
            IHotelFacilityRepository fr = new HotelFacilityRepository();
            try
            {
                fr.InsertFacility(hotelid, servicesid);
            }
            catch (Exception)
            {
                result = "E";
                throw;
            }
            return Content(result);
        }
        public ActionResult DeleteServicesOfHotel(short servicesid, int hotelid)
        {
            string result = "S";
            IHotelFacilityRepository fr = new HotelFacilityRepository();
            try
            {
                fr.DeleteFacility(hotelid, servicesid);
            }
            catch (Exception)
            {
                result = "E";
                throw;
            }
            
            return Content(result);
        }

        IHotelImageRepository<HotelImage> iHotelImageR = new HotelImageRepository();
        public string GetImageofRoomType(int hotelId, byte roomtypeid)
        {
            StringBuilder stringresult = new StringBuilder();

          
            IList<HotelImage> imageListPathOfRoom = new List<HotelImage>();
            if (roomtypeid == 0)
            {
                imageListPathOfRoom = iHotelImageR.HotelImageofAllMain(hotelId);
                 //get list image of type of room hotel
            }
            else
            {
                imageListPathOfRoom = iHotelImageR.ImageOfHotelRoom(hotelId, roomtypeid);
            }

            if (imageListPathOfRoom.Count > 0)
            {
                foreach (var item in imageListPathOfRoom)
                {
                    TagBuilder img = new TagBuilder("img");
                    img.Attributes.Add("src", "../../images/Hotel/" + @item.ImagesStore.ImagePath);
                    img.Attributes.Add("alt", item.RoomTypeId.ToString());
                    img.Attributes.Add("width", "150px");
                    img.Attributes.Add("height", "150px");
                    img.Attributes.Add("style", "padding:5px");

                    TagBuilder checkbox = new TagBuilder("input"); // check box for delete Image
                    checkbox.Attributes.Add("type", "checkbox");
                    checkbox.Attributes.Add("name", "checkboxForDelete");
                    //checkbox.Attributes.Add("class", "checkboxForDelete");
                    checkbox.Attributes.Add("value", item.ImagesStore.ImagesStoreId.ToString());                    

                    stringresult.Append(MvcHtmlString.Create(img.ToString(TagRenderMode.Normal)));
                    stringresult.Append(MvcHtmlString.Create(checkbox.ToString(TagRenderMode.Normal)));
                }
            }
            //}
            return stringresult.ToString();
        }

        [HttpGet]
        public ActionResult DeleteImageOfHotel(string imageStoreId, int HotelId, byte roomtypeid)
        {
            string result = string.Empty;
            int checkdeletenumber = 0;
            IHotelImageRepository<HotelImage> irhotel = new HotelImageRepository();
            IRepository<HotelImage> irht = new HotelImageRepository();
            IImagesStoreRepository iris = new ImagesStoreRepository();
            IRepository<ImagesStore> iristore = new ImagesStoreRepository();
            foreach (var item in imageStoreId.Split(';'))
            {
                // delete hotel image
                if (item != "")
                {
                    HotelImage HotelImage = irhotel.getHotelImageByImagesStoreId(int.Parse(item));
                    ImagesStore ImagesStore = iris.getImagesStoreById(long.Parse(item));
                    try
                    {
                        DeleteImageFromDirectory(ImagesStore.ImagePath);
                        irht.Delete(HotelImage);
                        iristore.Delete(ImagesStore);
                        checkdeletenumber += 1;
                        
                    }
                    catch (Exception)
                    {
                        result = "E";
                        throw;
                    }
                }
               

            }
            if (checkdeletenumber>0)
            {
                result = GetImageofRoomType(HotelId, roomtypeid) + "<br/> <span style='color:green'>Xóa hình thành công</span>";
            }
            else
            {
                result = GetImageofRoomType(HotelId, roomtypeid) +  "<br/> <span style='color:red'>Bạn chưa chọn hình nào để xóa</span>"; ;
            }
            return Content(result);
        }

        private void DeleteImageFromDirectory(string imageName)
        {
            string path = ApplicationHelper.ImageDirectoryHotel;
            if (System.IO.File.Exists(path + imageName))
            {                
                System.IO.File.Delete(path + imageName);
            }
        }


        public ActionResult Index1()
        {

            List<AvailabilityModel> listAvailabilityModel = new List<AvailabilityModel>();
            IList<HotelExtent> hotelwithImage = getListHotel();

            List<HotelModelEJ> hotelHotelModelEJ = new List<HotelModelEJ>();
            hotelHotelModelEJ = maptoEJ(hotelwithImage);

            ViewBag.datasource = hotelHotelModelEJ;

            return View();
            //test svn
        }

        private List<HotelModelEJ> maptoEJ(IList<HotelExtent> listAvailability)
        {
            List<HotelModelEJ> listHotelModelEJ = new List<HotelModelEJ>();
            foreach (var item in listAvailability)
            {                
                listHotelModelEJ.Add(new HotelModelEJ() 
                                    {  MostView = item.MostView == "Y" ? "Y" : "N", 
                                        Price = item.price, Star = item.Star,
                                        URLImange = "<img style=\"width:63px;height:63px\" src=\"../../images/Hotel/{imageURL}\" alt=\"Image Khach sạn Legend Hotel 11\">".Replace("{imageURL}", item.ImagePath),
                                        HotelName = item.Name + "<br/>" + Utils.TinyAddress(item.HotelAddress),
                                        DetailURL = "<a href=\"/Home/Edit_Detail_Hotel?idhotel={idHotel}\">Edit</a>".Replace("{idHotel}",item.HotelId.ToString())   
                                    });
            }

            return listHotelModelEJ;
        }

        protected IList<HotelExtent> getListHotel()
        {
            IList<Hotel> listhotel = new List<Hotel>();
            IList<HotelExtent> hotelwithImage = new List<HotelExtent>();
            IRepository<Hotel> rpHotel = new HotelRepository();
            IHotelRepository<Hotel> irHotelI = new HotelRepository();
            if (User.Identity.IsAuthenticated && (@Session["UserType"] as HotelOwner) != null)
            {
                listhotel = irHotelI.getHotelFilterByOwnerId((@Session["UserType"] as HotelOwner).HotelOwnerId);               

            }
            else
            {
                listhotel = rpHotel.GetAll();
            }


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
                itm.MostView = item.MostView;

                hotelwithImage.Add(itm);
            }

            return hotelwithImage;
        }

     

    }
}
