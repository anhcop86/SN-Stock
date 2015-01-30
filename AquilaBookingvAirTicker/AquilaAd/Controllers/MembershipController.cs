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
    public class MembershipController : Controller
    {
        //
        // GET: /Membership/

        public ActionResult Index()
        {
            IList<Membership> List = new List<Membership>();
            List = GetList();

            return View(List);
        }

        private IList<Membership> GetList()
        {
            IMembershipRepository<Membership> iprH = new MembershipRepository();
            IList<Membership> list = new List<Membership>();
            list = iprH.GetAll();

            return list;
        }

        //
        // GET: /Membership/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Membership/Create

        public ActionResult Create()
        {
            Membership membership = LoadViewCreate();
            return View(membership);
        }

        private Membership LoadViewCreate()
        {
            Membership membership = new Membership();

            IRepository<HotelOwner> rpho = new HotelOwnerRepository();
            //IList<HotelOwner> HotelOwner = new List<HotelOwner>();
            //HotelOwner = rpho.GetAll();

            AccountType accountType = new AccountType();
            IList<AccountType> listAccountType = accountType.listAllHotDealHotel();
            ViewData["listAccountType"] = new SelectList(listAccountType, "Id", "Name");

            AccountLock accountLock = new AccountLock();
            IList<AccountLock> listaccountLock = accountLock.listAllHotDealHotel();
            ViewData["listaccountLock"] = new SelectList(listaccountLock, "Id", "Name");

            membership.LastLoginDate = DateTime.Now;
            membership.CreateDate = DateTime.Now;
            membership.LastPasswordChangedDate = DateTime.Now;

           
            //AccountType accountType = new AccountType();
            //IList<AccountType> listAccountType = accountType.listAllHotDealHotel();
            //ViewData["listAccountType"] = new SelectList(listAccountType, "Id", "Name");



            return membership;
        }
        //
        // POST: /Membership/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Createmembership(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void Createmembership(FormCollection fc)
        {
            Membership membership = new Membership();

            membership.LoginId = fc["LoginId"];
            membership.Password = CustomHelpers.GetMD5Hash(fc["Password"]);
            membership.MemberName = fc["MemberName"];
            membership.MobilePIN = fc["MobilePIN"];
            membership.Email = fc["Email"];
            membership.IsLockedOut = bool.Parse(fc["IsLockedOut"]);
            membership.Type = fc["Type"];
            membership.CreateDate = DateTime.Parse(fc["CreateDate"]);
            membership.LastLoginDate = DateTime.Parse(fc["LastLoginDate"]);
            membership.LastPasswordChangedDate = DateTime.Parse(fc["LastPasswordChangedDate"]);
            membership.Comment = fc["Comment"];
            membership.LoginCount = int.Parse(fc["LoginCount"]);
            membership.LastLockoutDate = DateTime.Parse(fc["CreateDate"]);
            membership.FailedPasswordAttemptCount = 1;
            membership.FailedPasswordAttemptWindowStart = DateTime.Parse(fc["CreateDate"]);
            membership.FailedPasswordAnswerAttemptCount = 1;
            membership.FailedPasswordAnswerAttemptWindowStart = DateTime.Parse(fc["CreateDate"]);
            membership.PasswordFormat = 1;
            membership.PasswordSalt = "1233";

            HotelOwner hotelOwner = new HotelOwner();
            
            IRepository<HotelOwner> irho = new HotelOwnerRepository();
            IRepository<Hotel> irp = new HotelRepository();
            if (fc["Type"] == "H")
            {                
                hotelOwner.Address = fc["AddressMember"];
                hotelOwner.ActiveDate = DateTime.Parse(fc["CreateDate"]).ToString("yyyyMMdd");
                hotelOwner.CloseDate = DateTime.Parse(fc["CreateDate"]).ToString("yyyyMMdd");
                hotelOwner.CreatedBy = "Aquila";
                hotelOwner.CreatedDate = DateTime.Parse(fc["CreateDate"]).ToString("yyyyMMdd");
                hotelOwner.Email = fc["Email"];
                hotelOwner.FullName = fc["MemberName"];
                hotelOwner.PhoneNumber = fc["MobilePIN"];
                hotelOwner.Remark = fc["Comment"];
                hotelOwner.PaymentType = "PT";
                
               
            }
            IMembershipRepository<Membership> iprH = new MembershipRepository();
            try
            {
                iprH.Save(membership);
                if (fc["Type"] == "H")
                {
                    int MemberId = membership.MemberId;
                    hotelOwner.MemberId = MemberId;
                    irho.Save(hotelOwner);                       

                }

            }
            catch
            {

            }
        }

        //
        // GET: /Membership/Edit/5

        public ActionResult Edit(int id)
        {
            Membership membership = LoadViewEdit(id);
            return View(membership);
        }

        private Membership LoadViewEdit(int id)
        {

            IMembershipRepository<Membership> iprH = new MembershipRepository();
            Membership membership = iprH.GetByMemberId(id);
            IRepository<HotelOwner> rpho = new HotelOwnerRepository();


            AccountType accountType = new AccountType();
            IList<AccountType> listAccountType = accountType.listAllHotDealHotel();
            ViewData["listAccountType"] = new SelectList(listAccountType, "Id", "Name", membership.Type);

            AccountLock accountLock = new AccountLock();
            IList<AccountLock> listaccountLock = accountLock.listAllHotDealHotel();
            ViewData["listaccountLock"] = new SelectList(listaccountLock, "Id", "Name", membership.IsLockedOut);

            IHotelRepository<Hotel> rpht = new HotelRepository(); // load list hotel

            
            

            HotelOwner hotelOwner = new HotelOwner();
            Hotel hotel = new Hotel();

            IRepository<HotelOwner> irho = new HotelOwnerRepository();
            IHotelRepository<Hotel> repIHotel = new HotelRepository();
            IHotelOwnerRepository irpho = new HotelOwnerRepository();
            IRepository<Hotel> irp = new HotelRepository();

            if (membership.Type == "H")
            {
                hotelOwner = irpho.getHotelOwnerByMemberShipId(membership.MemberId);               
                
                //ViewData["listHotel"] = new SelectList(listhotel, "HotelId", "Name");
                if (hotelOwner != null)
                {
                    ViewData["AddressMember"] = hotelOwner.Address;
                }
                
            }
        

            return membership;
        }

        //
        // POST: /Membership/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                UpdateMembership(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void UpdateMembership(FormCollection fc)
        {
            bool isNew = false;
            Membership membership = new Membership();
            IMembershipRepository<Membership> iprH = new MembershipRepository();
            membership = iprH.GetByMemberId(int.Parse(fc["GetMemberId"]));

            membership.LoginId = fc["LoginId"];

            //membership.Password = CustomHelpers.GetMD5Hash(fc["Password"]); reset khong doi pass dc

            membership.MemberName = fc["MemberName"];
            membership.MobilePIN = fc["MobilePIN"];
            membership.Email = fc["Email"];
            membership.IsLockedOut = bool.Parse(fc["IsLockedOut"]);
            //membership.Type = fc["Type"]; khong cho chon nen cung khong can update
            membership.CreateDate = DateTime.Parse(fc["CreateDate"]);
            membership.LastLoginDate = DateTime.Parse(fc["LastLoginDate"]);
            membership.LastPasswordChangedDate = DateTime.Parse(fc["LastPasswordChangedDate"]);
            membership.Comment = fc["Comment"];
            membership.LoginCount = int.Parse(fc["LoginCount"]);
            membership.LastLockoutDate = DateTime.Parse(fc["CreateDate"]);
            membership.FailedPasswordAttemptCount = 1;
            membership.FailedPasswordAttemptWindowStart = DateTime.Parse(fc["CreateDate"]);
            membership.FailedPasswordAnswerAttemptCount = 1;
            membership.FailedPasswordAnswerAttemptWindowStart = DateTime.Parse(fc["CreateDate"]);
            membership.PasswordFormat = 1;
            membership.PasswordSalt = "1233";

            HotelOwner hotelOwner = new HotelOwner();
            
            IRepository<HotelOwner> irho = new HotelOwnerRepository();
            IHotelOwnerRepository irhoInterface = new HotelOwnerRepository();
            IRepository<Hotel> irp = new HotelRepository();
            if (membership.Type == "H")
            {
                hotelOwner = irhoInterface.getHotelOwnerByMemberShipId(membership.MemberId);
                if (hotelOwner == null)
                {
                    hotelOwner = new HotelOwner();
                    isNew = true;
                }
                hotelOwner.Address = fc["AddressMember"];
                hotelOwner.ActiveDate = DateTime.Parse(fc["CreateDate"]).ToString("yyyyMMdd");
                hotelOwner.CloseDate = DateTime.Parse(fc["CreateDate"]).ToString("yyyyMMdd");
                hotelOwner.CreatedBy = "Aquila";
                hotelOwner.CreatedDate = DateTime.Parse(fc["CreateDate"]).ToString("yyyyMMdd");
                hotelOwner.Email = fc["Email"];
                hotelOwner.FullName = fc["MemberName"];
                hotelOwner.PhoneNumber = fc["MobilePIN"];
                hotelOwner.Remark = fc["Comment"];
                hotelOwner.PaymentType = "PT";
                hotelOwner.MemberId = membership.MemberId;
                

                //hotel.HotelOwnerId = 
            }
            
            try
            {
                iprH.Update(membership);
                if (membership.Type == "H")
                {
                    //int MemberId = membership.MemberId;
                    //hotelOwner.MemberId = MemberId;
                    if (isNew)
                    {
                        irho.Save(hotelOwner);
                    }
                    else
                    {
                        irho.Update(hotelOwner);
                    }
                }
               

            }
            catch
            {

            }
        }

        //
        // GET: /Membership/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Membership/Delete/5

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
    }
}
