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
    public class ServicesController : Controller
    {
        //
        // GET: /Services/

        public ActionResult Index()
        {
            IList<Facility> FacilityList = new List<Facility>();
            FacilityList = GetFacilityList();
            return View(FacilityList);
        }

        private IList<Facility> GetFacilityList()
        {
            IRepository<Facility> iprH = new FacilityRepository();
            IList<Facility> FacilityList = new List<Facility>();
            FacilityList = iprH.GetAll();
            
            return FacilityList;
        }

        //
        // GET: /Services/Details/5

        public ActionResult Details(Int16 servicesid)
        {
            Facility facility = new Facility();
            IFacilityRepository iprH = new FacilityRepository();
            facility = iprH.GetById(servicesid);
            return View(facility);
        }

        //
        // GET: /Services/Create

        public ActionResult Create()
        {
            Facility facility = new Facility();
            facility.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            return View(facility);
        }

        //
        // POST: /Services/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
           
                // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                CreateNewFacility(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void CreateNewFacility(FormCollection fc)
        {

            Facility ht = new Facility();
            ht.Name = fc["Name"];
            ht.CreatedDate = fc["CreatedDate"].Replace("-", "");
            ht.CreatedBy = fc["CreatedBy"];
            IRepository<Facility> iprH = new FacilityRepository();
            try
            {
                iprH.Save(ht);
            }
            catch 
            {
                
                
            }
            
        }

        //
        // GET: /Services/Edit/5

        public ActionResult Edit(Int16 servicesid)
        {
            Facility facility = new Facility();
            IFacilityRepository iprH = new FacilityRepository();
            facility = iprH.GetById(servicesid);
            
            return View(facility);
        }

        //
        // POST: /Services/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            if(ModelState.IsValid)
            {
                // TODO: Add update logic here
                UpdateFacility(collection);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        private void UpdateFacility( FormCollection fc)
        {
            Facility facility = new Facility();
            IFacilityRepository iprH = new FacilityRepository();
            IRepository<Facility> iprHO = new FacilityRepository();
            facility = iprH.GetById(short.Parse(fc["GetFacilityId"]));
            facility.Name = fc["Name"];
            if (string.IsNullOrEmpty(fc["CreatedDate"]))
            {
                facility.CreatedDate = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                facility.CreatedDate = fc["CreatedDate"].Replace("-", "");
            }
            facility.CreatedBy = fc["CreatedBy"];
            
            try
            {
                iprHO.Update(facility);
            }
            catch
            {


            }
        }

      
        //
        // GET: /Services/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Services/Delete/5

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
