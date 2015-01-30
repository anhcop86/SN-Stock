using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;
using ShareHoderFrontEndV2.Ext;

namespace ShareHoderFrontEndV2.Controllers
{
    public class CompareListController : BaseController
    {
        //
        // GET: /CompareList/

        public ActionResult Index()
        {
            IList<Hotel> listComparehotel = new List<Hotel>();
            if (User.Identity.IsAuthenticated)
            {
                IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                ShareHolderCore.Domain.Model.Membership membership = null;
                membership = userRep.GetByLoginId(User.Identity.Name);   

                ICompareListDetailRepository<CompareListDetail> rpCompareListDetai = new CompareListDetailRepository();
                IList<CompareListDetail> ListCompareListDetail = rpCompareListDetai.GetListCompareListDetailFromMember(membership.MemberId);
                IList<Hotel> listhotel = new List<Hotel>();
                IRepository<Hotel> rpHotel = new HotelRepository();
                foreach (var item in ListCompareListDetail)
                {
                    Hotel hotel = rpHotel.GetById(item.Hotel.HotelId);
                    listhotel.Add(hotel);
                }

                ViewData["listhotelcompare"]  = listhotel;
                listComparehotel = listhotel;
            }
            else
            {
                if(ApplicationHelper.getlistComparehotel() == null)
                {
                     ViewData["listhotelcompare"] = new List<Hotel>();
                     listComparehotel = new List<Hotel>(); 
                }
                else
                {
                    ViewData["listhotelcompare"]  = ApplicationHelper.getlistComparehotel();
                    listComparehotel = ApplicationHelper.getlistComparehotel();
                }
            }

                       
            //return View(rp.GetListCompareListDetailFromMember(1));

            #region danh sach dich vu
            IRepository<Facility> repFacility = new FacilityRepository();
            IFacilityRepository ifr = new FacilityRepository();
            IList<Facility> facility = repFacility.GetAll();
            IList<FacilityExtent> listFacilityExtent = new List<FacilityExtent>();

            foreach (var item in facility)
            {
                FacilityExtent FacilityExtent = new FacilityExtent();
                //FacilityExtent.CheckFacility = ifr.CheckFacility(id, item.FacilityId);
                FacilityExtent.CreatedDate = item.CreatedDate;
                FacilityExtent.FacilityId = item.FacilityId;
                FacilityExtent.ListHotelFacility = item.ListHotelFacility;
                FacilityExtent.Name = item.Name;

                listFacilityExtent.Add(FacilityExtent);
            }

            ViewData["listFacility"] = listFacilityExtent;
            #endregion
       
            #region khach san xem gan nhat
            IHotelViewlastestRepository<Hotel> reposiHotelViewlastest = new HotelRepository();
            ViewData["reposiHotelViewlastest"] = reposiHotelViewlastest.getListHotelMostview();
            #endregion

            return View(listComparehotel);


        }
        public ActionResult Add(int hotelid)
        {
            
            string result = string.Empty;
            if (!User.Identity.IsAuthenticated) // not login
            {
                var listcompare = (List<int>)Session["comparelistold"];
                if (listcompare == null)
                {
                    listcompare = new List<int>();
                }
                if (listcompare.Contains(hotelid))
                {
                    listcompare.Remove(hotelid);
                    result = "R";
                }
                else if (listcompare.Count == 3)
                {

                    result = "M";
                }
                else
                {
                    listcompare.Add(hotelid);
                    result = "A";
                }


                Session["comparelistold"] = listcompare;
                return Content(result);
                
            }
            else // login
            {
                //var listcompareLogin = (List<int>)Session["comparelistlogin"];
                //if (listcompareLogin == null)
                //{
                //    listcompareLogin = new List<int>();
                //}
                IRepository<CompareListDetail> irCompareListDetail = new CompareListDetailRepository();
                IRepository<Hotel> irHotet = new HotelRepository();

                ShareHolderCore.Domain.Model.Membership membership = (Membership)Session["LoginObject"];

                ICompareListDetailRepository<CompareListDetail> rpCompareListDetai = new CompareListDetailRepository();
                IRepository<CompareList> IrpCompareList = new CompareListRepository();
                IList<CompareListDetail> ListCompareListDetail = rpCompareListDetai.GetListCompareListDetailFromMember(membership.MemberId);

                ICompareListRepository rpCompareList = new CompareListRepository();

                var checkExist = ListCompareListDetail.Where(m => m.Hotel.HotelId == hotelid).FirstOrDefault<CompareListDetail>();
                if (checkExist != null)
                {
                    // remove khoi danh sach so sanh
                    //CompareListDetail itemdelete = irCompareListDetail.GetById(
                    irCompareListDetail.Delete(checkExist);
                    return Content("R");

                }
                else if (ListCompareListDetail.Count >= 3)
                {
                    //Maxi number of compare hotel
                    return Content("M");
                }
                else
                {
                   // thêm khach san vao database
                    
                    CompareList compareL = rpCompareList.getCompareListfromMemberId(membership.MemberId);

                    if (compareL == null)
                    {
                        CompareList compareLitem = new CompareList();

                        compareLitem.Membership = membership;
                        //compareLitem.MemberId = membership.MemberId;
                        compareLitem.CreatedDate = DateTime.Now.ToShortDateString();
                        compareLitem.CreatedBy =  "Aquila";
                        IrpCompareList.Save(compareLitem);
                        compareL = rpCompareList.getCompareListfromMemberId(membership.MemberId);
                    }

                    CompareListDetail item = new CompareListDetail();
                    //item.CompareListId = compareL.CompareListId;
                    //item.HotelId = Convert.ToInt32(hotelid);
                    item.CompareList = compareL;
                    item.Hotel = irHotet.GetById(hotelid);
                    item.CreatedDate = DateTime.Now.ToShortDateString();
                    item.CreatedBy = "Aquila";
                    irCompareListDetail.Save(item);
 
                    return Content("A");
                }
            }
        }

        public ActionResult remove(int id)
        {
            if (Request.IsAuthenticated)
            {
                Membership membership = (Membership)Session["LoginObject"];
                IRepository<CompareListDetail> irCompareListDetail = new CompareListDetailRepository();
                ICompareListDetailRepository<CompareListDetail> rpCompareListDetai = new CompareListDetailRepository();
                IList<CompareListDetail> ListCompareListDetail = rpCompareListDetai.GetListCompareListDetailFromMember(membership.MemberId);
                var checkExist = ListCompareListDetail.Where(m => m.Hotel.HotelId == id).FirstOrDefault<CompareListDetail>();
                if (checkExist != null)
                {
                    // remove khoi danh sach so sanh
                    //CompareListDetail itemdelete = irCompareListDetail.GetById(
                    irCompareListDetail.Delete(checkExist);                    

                }

            }
            else
            {
                var listcompare = (List<int>)Session["comparelistold"];
                listcompare.Remove(id);
            }
            

            return RedirectToAction("");
        }


    }
}
