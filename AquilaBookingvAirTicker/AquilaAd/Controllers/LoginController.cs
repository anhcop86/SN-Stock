using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquilaAd.Models;
using System.Web.Security;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;

namespace AquilaAd.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LogOnModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (System.Web.Security.Membership.ValidateUser(model.UserName, model.Password))
                {
                    Session["LoginByFacebook"] = "N";
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        string test = User.Identity.Name;
                        IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                        ShareHolderCore.Domain.Model.Membership membership = null;
                        membership = userRep.GetByLoginId(model.UserName);
                        @Session["LoginObject"] = membership;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Resources.UserInterface.incorrectUserNamePassword);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
       

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        [HttpPost]
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
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Login/Edit/5

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
        // GET: /Login/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Login/Delete/5

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
