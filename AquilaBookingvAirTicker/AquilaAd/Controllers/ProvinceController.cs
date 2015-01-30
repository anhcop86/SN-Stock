using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderCore.Domain.Model;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore.Domain;

namespace AquilaAd.Controllers
{
    [Authorize]
    public class ProvinceController : Controller
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            IList<Province> List = new List<Province>();
            List = GetList();
            return View(List);
        }

        private IList<Province> GetList()
        {
            IRepository<Province> iprH = new ProvinceRepository();
            IList<Province> list = new List<Province>();
            list = iprH.GetAll();

            return list;
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            Province province = LoadViewCreate();
            return View(province);
        }

        private Province LoadViewCreate()
        {
            Province province = new Province();
            province.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            Country country = new Country();
            IRepository<Country> iprC = new CountryRepository();
            ViewData["listCountry"] = new SelectList(iprC.GetAll(), "CountryId", "Name");

            return province;
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                CreateProvince(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void CreateProvince(FormCollection fc)
        {
            Province province = new Province();
            ICountryRepository iprCT = new CountryRepository();
            province.Name = fc["Name"];
            province.EnglishName = fc["EnglishName"];
            province.Country = iprCT.getCountryById(short.Parse(fc["Country.CountryId"]));
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                province.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                province.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            province.CreatedBy = fc["CreatedBy"];

            IRepository<Province> iprH = new ProvinceRepository();

            try
            {
                iprH.Save(province);
            }
            catch
            {

            }
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id)
        {
            Province province = LoadView(id);
            return View(province);
        }

        private Province LoadView(int id)
        {
            Province province = new Province();
            IRepository<Province> iprH = new ProvinceRepository();
            province = iprH.GetById(id);

            Country country = new Country();
            IRepository<Country> iprC = new CountryRepository();

            ViewData["listCountry"] = new SelectList(iprC.GetAll(), "CountryId", "Name", province.Country.CountryId);

            
            return province;
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                UpdateProvince(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void UpdateProvince(FormCollection fc)
        {
            Province province = new Province();
            //IRoomTypeRepository<RoomType> iprH = new RoomTypeRepository();
            IRepository<Province> iprHO = new ProvinceRepository();
            ICountryRepository iprCT = new CountryRepository();

            province = iprHO.GetById(int.Parse(fc["GetProvinceIdId"]));
            province.Name = fc["Name"];
            province.EnglishName = fc["EnglishName"];
            
            province.Country = iprCT.getCountryById(short.Parse(fc["Country.CountryId"]));
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                province.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                province.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            province.CreatedBy = fc["CreatedBy"];

            try
            {
                iprHO.Update(province);
            }
            catch
            {


            }
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id)
        {
           
            try
            {
                // TODO: Add delete logic here
                DeleteProvince(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void DeleteProvince(int id)
        {
            Province roomType = new Province();

            IRepository<Province> iprHO = new ProvinceRepository();
            roomType = iprHO.GetById(id);
            try
            {
                iprHO.Delete(roomType);
            }
            catch
            {

            }
        }

        //
        // POST: /Default1/Delete/5

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
