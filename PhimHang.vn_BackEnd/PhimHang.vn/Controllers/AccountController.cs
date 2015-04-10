using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PhimHang.Models;
using System.IO;
using System.Drawing;

namespace PhimHang.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private const string ImageURLAvata = "images/avatar/";
        private const string ImageURLCover = "images/cover/";
                
         public AccountController()
             : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
         {
             
         }

        public AccountController( UserManager<ApplicationUser> userManager)
        {
            
            UserManager = userManager;
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager) { AllowOnlyAlphanumericUserNames = false };
        }
        public UserManager<ApplicationUser> UserManager { get; private set; }

        //private testEntities db;// = new testEntities();
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            if (returnUrl != null)
            {
                ViewBag.ReturnUrl = returnUrl;
            }

            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

       
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl); // Returun URL
                    //eturn RedirectToAction(""); // Hieu

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName.Replace(" ","").Trim(),
                                                    //AvataImage = "default_avatar_medium.jpg",
                                                     //FullName = model.FullName,
                                                     //   CreatedDate = DateTime.Now,
                                                     //   Verify = Verify.NO
                };
                user.UserExtentLogin = new UserExtentLogin { KeyLogin = user.Id, CreatedDate = DateTime.Now, FullName = model.FullName, Verify = Verify.NO, UserNameCopy = model.UserName.Replace(" ", "").Trim() };
                
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }
      
        public ActionResult Profile(ManageMessageId? message)
        {

            ViewBag.StatusMessage =
               message == ManageMessageId.UpdateSucess ? "Cập nhật tài khoản thành công."               
               : "";
            ProfileUserViewModel profile = new ProfileUserViewModel();
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null) // if session of user has expire
            {
                return RedirectToAction("Login");
            }
            else // user not null
            {
                profile.UserName = user.UserName;
                profile.FullName = user.UserExtentLogin.FullName;
                profile.Email = user.UserExtentLogin.Email;
                profile.BirthDay = user.UserExtentLogin.BirthDate;
                profile.CreatedDate = user.UserExtentLogin.CreatedDate.ToString("dd/MM/yyyy");
                profile.Verify = user.UserExtentLogin.Verify;//== null? Verify.NO: Verify.YES;
                profile.Status = user.UserExtentLogin.Status;
            }
            ViewBag.ImageUrl = ImageURLAvata + user.UserExtentLogin.AvataImage;
            ViewBag.ImageUrlCover = ImageURLCover + user.UserExtentLogin.AvataCover;
            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profile(ProfileUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    user.UserExtentLogin.FullName = model.FullName;
                    user.UserExtentLogin.Email = model.Email;
                    user.UserExtentLogin.BirthDate = model.BirthDay;
                    user.UserExtentLogin.Status = model.Status;
                }
                else
                {
                    return RedirectToAction("Login");
                }
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", new { Message = ManageMessageId.UpdateSucess });
                }
                else
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<string> AvataUpload(HttpPostedFileBase uploadfileid_avata)
        {

            #region check valid file

            var validImageTypes = new string[]
                                                {
                                                    "image/gif",
                                                    "image/jpeg",
                                                    "image/pjpeg",
                                                    "image/png"
                                                };
            if (uploadfileid_avata == null || uploadfileid_avata.ContentLength == 0) // check file null or file corrupt
            {
                return "Chưa chọn file upload";
            }

            if (!validImageTypes.Contains(uploadfileid_avata.ContentType)) // check file type
            {
                return "Please choose either a GIF, JPG or PNG image.";
            }

            if (uploadfileid_avata.ContentLength > 716800) // check file size
            {
                return "File's very larg: File must be less than 700KB";
            }

            #endregion
            else
            {
                //save file
                #region get directory

                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                var uploadDir = "~/" + ImageURLAvata;
                string NameFiletimeupload = user.Id + DateTime.Now.ToString("HHmmss") + "_avata";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), NameFiletimeupload + Path.GetExtension(uploadfileid_avata.FileName));
                var imageUrl = ImageURLAvata + NameFiletimeupload + Path.GetExtension(uploadfileid_avata.FileName);
                uploadfileid_avata.SaveAs(imagePath);

                
               
                #endregion
                //delete old avata image

                #region delete old avata image

                string fullPath = Server.MapPath(uploadDir) + user.UserExtentLogin.AvataImage;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                #endregion
                //
                #region update new avata on server

                user.UserExtentLogin.AvataImage = NameFiletimeupload + Path.GetExtension(uploadfileid_avata.FileName);
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //ViewBag.imageUrlAvata = imageUrl;
                    return "YES|" + imageUrl;
                }
                else
                {
                    return "Cập nhật dữ liệu thất bại";
                }
                #endregion

            }

        }

        [HttpPost]
        public async Task<string> CoverUpload(HttpPostedFileBase uploadfileid_cover)
        {

            #region check valid file

            var validImageTypes = new string[]
                                                {
                                                    "image/gif",
                                                    "image/jpeg",
                                                    "image/pjpeg",
                                                    "image/png"
                                                };
            if (uploadfileid_cover == null || uploadfileid_cover.ContentLength == 0) // check file null or file corrupt
            {
                return "Chưa chọn file upload";
            }

            if (!validImageTypes.Contains(uploadfileid_cover.ContentType)) // check file type
            {
                return "Please choose either a GIF, JPG or PNG image.";
            }

            if (uploadfileid_cover.ContentLength > 716800) // check file size
            {
                return "File's very large: File must be less than 700KB";
            }

            #endregion
            else
            {
                //save file
                #region get directory

                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId()); // get user's logging
                var uploadDir = "~/" + ImageURLCover;
                string NameFiletimeupload = user.Id + DateTime.Now.ToString("HHmmss") + "_cover";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), NameFiletimeupload + Path.GetExtension(uploadfileid_cover.FileName));
                var imageUrl = ImageURLCover + NameFiletimeupload + Path.GetExtension(uploadfileid_cover.FileName);
                uploadfileid_cover.SaveAs(imagePath);



                #endregion
                //delete old avata image

                #region delete old avata image

                string fullPath = Server.MapPath(uploadDir) + user.UserExtentLogin.AvataCover;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                #endregion
                //
                #region update new avata on server

                user.UserExtentLogin.AvataCover = NameFiletimeupload + Path.GetExtension(uploadfileid_cover.FileName);
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //ViewBag.imageUrlAvata = imageUrl;
                    return "YES|" + imageUrl;
                }
                else
                {
                    return "Cập nhật dữ liệu thất bại";
                }
                #endregion

            }

        }
      
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error,
            UpdateSucess
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}