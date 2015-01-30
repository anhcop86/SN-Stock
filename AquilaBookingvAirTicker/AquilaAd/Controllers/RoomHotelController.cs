using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;

namespace AquilaAd.Controllers
{
    [Authorize]
    public class RoomHotelController : Controller
    {
        //
        // GET: /RoomHotel/

        public ActionResult Index()
        {
            IList<RoomType> List = new List<RoomType>();
            List = GetList();

            return View(List);
        }

        private IList<RoomType> GetList()
        {
            IRepository<RoomType> iprH = new RoomTypeRepository();
            IList<RoomType> list = new List<RoomType>();
            list = iprH.GetAll();

            return list;
        }

        //
        // GET: /RoomHotel/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RoomHotel/Create

        public ActionResult Create()
        {
            RoomType roomType = new RoomType();
            roomType.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            return View(roomType);
        }

        //
        // POST: /RoomHotel/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                CreateNewRoomType(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void CreateNewRoomType(FormCollection fc)
        {
            RoomType ht = new RoomType();
            ht.Name = fc["Name"];
            ht.EnglishName = fc["EnglishName"];
            ht.CreatedDate = fc["CreatedDate"].Replace("-", "");
            ht.CreatedBy = fc["CreatedBy"];
            IRepository<RoomType> iprH = new RoomTypeRepository();
            try
            {
                iprH.Save(ht);
            }
            catch
            {
                
            }
            
        }

        //
        // GET: /RoomHotel/Edit/5

        public ActionResult Edit(byte id)
        {
            RoomType roomType = new RoomType();
            IRoomTypeRepository<RoomType> iprH = new RoomTypeRepository();
            roomType = iprH.GetById(id);

            return View(roomType);
        }

        //
        // POST: /RoomHotel/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                UpdateRoomType(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void UpdateRoomType(FormCollection fc)
        {
            RoomType roomType = new RoomType();
            IRoomTypeRepository<RoomType> iprH = new RoomTypeRepository();
            IRepository<RoomType> iprHO = new RoomTypeRepository();
            roomType = iprH.GetById(byte.Parse(fc["GetRoomTypeId"]));
            roomType.Name = fc["Name"];
            roomType.EnglishName = fc["EnglishName"];
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                roomType.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                roomType.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            roomType.CreatedBy = fc["CreatedBy"];

            try
            {
                iprHO.Update(roomType);
            }
            catch
            {


            }
        }

        //
        // GET: /RoomHotel/Delete/5

        public ActionResult Delete(byte id)
        {
            
            try
            {
                // TODO: Add delete logic here
                DeleteRoomtype(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /RoomHotel/Delete/5

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
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

        private void DeleteRoomtype(byte id)
        {
            RoomType roomType = new RoomType();
            IRoomTypeRepository<RoomType> iprH = new RoomTypeRepository();
            IRepository<RoomType> iprHO = new RoomTypeRepository();
            roomType = iprH.GetById(id);
            try
            {
                iprHO.Delete(roomType);
            }
            catch
            {

            }
        }
    }
}
