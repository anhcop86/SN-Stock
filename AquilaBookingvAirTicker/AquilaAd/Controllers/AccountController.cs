using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AquilaAd.Models;
using System.Web.Security;
using ShareHolderCore;
using ShareHolderCore.Domain.Repositories;
using AquilaAd.Ext;
using ShareHolderCore.Domain.Model;

namespace AquilaAd.Controllers
{
      [Authorize]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("MyProfile", "Account");
        }

        [AllowAnonymous]
        public ActionResult LogOn(string token)
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogOn

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (System.Web.Security.Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/LogOn

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl) // you must config the customerProvider in webconfig
            {

            if (ModelState.IsValid)
            {
                if (System.Web.Security.Membership.ValidateUser(model.UserName, model.Password))
                {
                    
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                   
                        IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                        ShareHolderCore.Domain.Model.Membership membership = null;
                        membership = userRep.GetByLoginId(model.UserName);
                        @Session["AdminLoginObject"] = membership;
                        @Session["UserType"] = checkUserType(membership.MemberId);
                        return RedirectToAction("Index", "Home", new { pageNumber = 1 });
                    
                }
                else
                {
                    ModelState.AddModelError("", Resources.UserInterface.incorrectUserNamePassword);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private HotelOwner checkUserType(int MemberId)
        {
            IHotelOwnerRepository irpHower = new HotelOwnerRepository();
            HotelOwner hotelOwner = irpHower.getHotelOwnerByMemberShipId(MemberId);

            if (hotelOwner == null)
            {
                return null;
            }
            else
            {
                return hotelOwner;
            }

        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            //////////////////////////////////////////////////////////////////////////////////////////
            //Session["YourKey"] = "Test";  // creates the key                                      //
            //Session.Remove("YourKey");    // removes the key                                      //
            //bool gone = (Session["YourKey"] == null);   // tests that the remove worked           //
            //////////////////////////////////////////////////////////////////////////////////////////

            FormsAuthentication.SignOut();

            @Session.Remove("AdminLoginObject");
            @Session.Remove("UserType");
            return RedirectToAction("Logon", "Account");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                System.Web.Security.Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                System.Web.Security.Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);

                    ShareHolderCore.Domain.Model.Membership membership = null;
                    IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                    membership = userRep.GetByLoginId(model.UserName);
                    @Session["AdminLoginObject"] = membership;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = true;
                try
                {
                    CustomMembershipProvider custP = new CustomMembershipProvider();
                    //System.Web.Security.MembershipUser currentUser = System.Web.Security.Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    //IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRepo = new ShareHolderCore.Domain.Repositories.MembershipRepository();
                    //userRepo.ChangePassword(currentUser.UserName, CustomMembershipProvider.GetMD5Hash(model.OldPassword), CustomMembershipProvider.GetMD5Hash(model.NewPassword));*/
                    changePasswordSucceeded = custP.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }

                catch (ApplicationException appEx)
                {
                    changePasswordSucceeded = false;
                    ModelState.AddModelError("", appEx.Message);
                }
                catch (Exception ex)
                {
                    changePasswordSucceeded = false;
                    ModelState.AddModelError("", ex.Message);
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = true;
                try
                {
                    CustomMembershipProvider custP = new CustomMembershipProvider();
                    //System.Web.Security.MembershipUser currentUser = System.Web.Security.Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    //IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRepo = new ShareHolderCore.Domain.Repositories.MembershipRepository();
                    //userRepo.ChangePassword(currentUser.UserName, CustomMembershipProvider.GetMD5Hash(model.OldPassword), CustomMembershipProvider.GetMD5Hash(model.NewPassword));*/

                    if ("EmailNotFound" == custP.ResetPassword(model.Email, ""))
                    {

                        ModelState.AddModelError("", "không xác định được địa chỉ email trong hệ thống, Quý khách  vui lòng kiểm tra lại thông tin địa chỉ email.");
                        changePasswordSucceeded = false;
                        return View(model);
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ResetPasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", Resources.ValidationMessage.email);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            ProfileModel model = new ProfileModel();
            if (Request.IsAuthenticated)
            {
                IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
                ShareHolderCore.Domain.Model.Membership membership = null;
                membership = userRep.GetByLoginId(User.Identity.Name);
                model.LoginId = membership.LoginId;
                model.Email = membership.Email;
                model.IsLockedOut = !(membership.IsLockedOut);
                model.LastLoginDate = membership.LastLoginDate;
                model.UserName = membership.MemberName;
            }
            else
            {
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult MyProfile(ProfileModel model)
        {
            bool updateSucceeded = true;
            IMembershipRepository<ShareHolderCore.Domain.Model.Membership> userRep = new MembershipRepository();
            ShareHolderCore.Domain.Model.Membership membership = null;
            try
            {
                membership = userRep.GetByLoginId(User.Identity.Name);
            }
            catch (Exception)
            {
                updateSucceeded = false;
            }

            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    try
                    {
                        membership.Email = model.Email;
                        userRep.Update(membership);

                        model.LastLoginDate = membership.LastLoginDate;
                        model.IsLockedOut = membership.IsLockedOut;
                        model.UserName = membership.MemberName;
                        model.LoginId = membership.LoginId;

                    }
                    catch (Exception)
                    {
                        updateSucceeded = false;
                    }

                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }
            return View(model);
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors
                .Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Tên đăng nhập bị trùng, vui lòng chọn một tên khác";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Email bị trùng hoặc không đúng định dạng, ví dụ: abc@xyz.com";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
