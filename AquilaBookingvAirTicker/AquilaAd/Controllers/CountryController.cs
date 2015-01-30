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
    public class CountryController : Controller
    {
        //
        // GET: /Country/
        
        public ActionResult Index()
        {
            IList<Country> List = new List<Country>();
            List = GetList();

            return View(List);
            
        }
        private IList<Country> GetList()
        {
            IRepository<Country> iprH = new CountryRepository();
            IList<Country> list = new List<Country>();
            list = iprH.GetAll();

            return list;
        }


        //
        // GET: /Country/Details/5

        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        //
        // GET: /Country/Create

        public ActionResult Create()
        {
            Country country = new Country();
            country.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            return View(country);
        }

        //
        // POST: /Country/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection, Country country)
        {
            if (ModelState.IsValid)
            {
                //CreateNewCountry(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void CreateNewCountry(FormCollection fc)
        {
            Country item = new Country();
            item.Name = fc["Name"];
            item.EnglishName = fc["EnglishName"];
            item.CreatedDate = fc["CreatedDate"].Replace("-", "");
            item.CreatedBy = fc["CreatedBy"];
            IRepository<Country> iprH = new CountryRepository();
            try
            {
                iprH.Save(item);
            }
            catch
            {

            }
            
        }

        //
        // GET: /Country/Edit/5

        public ActionResult Edit(short id)
        {
            Country country = new Country();
            ICountryRepository iprH = new CountryRepository();
            country = iprH.getCountryById(id);

            return View(country);
        }

        //
        // POST: /Country/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                UpdateCountry(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void UpdateCountry(FormCollection fc)
        {
            Country country = new Country();
            ICountryRepository iprH = new CountryRepository();
            IRepository<Country> iprHO = new CountryRepository();
            country = iprH.getCountryById(short.Parse(fc["GetCountryId"]));
            country.Name = fc["Name"];
            country.EnglishName = fc["EnglishName"];
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                country.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                country.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            country.CreatedBy = fc["CreatedBy"];

            try
            {
                iprHO.Update(country);
            }
            catch
            {


            }
        }

        //
        // GET: /Country/Delete/5

        public ActionResult Delete(short id)
        {
            try
            {
                // TODO: Add delete logic here
                DeleteCountry(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void DeleteCountry(short id)
        {
            Country country = new Country();
            ICountryRepository iprH = new CountryRepository();
            IRepository<Country> iprHO = new CountryRepository();
            country = iprH.getCountryById(id);
            try
            {
                iprHO.Delete(country);
            }
            catch
            {

            }
        }

        //
        // POST: /Country/Delete/5

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
