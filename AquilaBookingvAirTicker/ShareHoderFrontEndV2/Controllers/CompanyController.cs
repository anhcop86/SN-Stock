using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;
using ShareHolderCore;
using ShareHoderFrontEndV2.Models;


namespace ShareHoderFrontEndV2.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Company/
         [AllowAnonymous]
        public ActionResult Index()
        {
            IRepository<Company> repo = new CompanyRepository();
            
            return View(repo.GetAll());
        }

        //
        // GET: /Company/Details/5
         [AllowAnonymous]
        public ActionResult Details(int id)
        {
           /* var serviceURI = new Uri("http://localhost:9898/ShareHolderDataService.svc");
            var context = new ShareHolderServiceReference.ShareHolderManagerEntities(serviceURI);
            var query = from c in context.Companies
                         where c.CompanyId == id
                        select c;
            var result = query.ToList();*/
           // var  comp = query.OfType<ShareHoderFrontEndV2.ShareHolderServiceReference.Company>();
            IRepository<Company> repo = new CompanyRepository();
            return View(repo.GetById(id)) ;
        
        }

        //
        // GET: /Company/Create
         public ActionResult Create()
         {
             return View();
         }
       
        //
        // GET: /Company/Create

        [HttpPost]       
        public ActionResult Create(ShareHolderCore.Domain.Model.Company model)
        {
            if (ModelState.IsValid)
            {
                IRepository<Company> repo = new CompanyRepository();
                repo.Save(model);
                return RedirectToAction("Index");
              
            }
            return View();
        } 

        //
        // POST: /Company/Create

       /* [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
        
        //
        // GET: /Company/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Company/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Company/Delete/5

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
