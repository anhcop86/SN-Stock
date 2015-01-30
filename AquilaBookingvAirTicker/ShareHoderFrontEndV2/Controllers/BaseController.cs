using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShareHoderFrontEndV2.Helper;
using ShareHolderCore;
using ShareHolderCore.Domain.Model;
using ShareHolderCore.Domain.Repositories;
using ShareHoderFrontEndV2.Ext;

namespace ShareHoderFrontEndV2.Controllers
{
    public class BaseController : Controller
    {
        protected override void ExecuteCore()
        {

            int culture = 0;
            if (this.Session == null || this.Session["CurrentCulture"] == null)
            {
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                this.Session["CurrentCulture"] = culture;
            }
            else
            {
                culture = (int)this.Session["CurrentCulture"];
            }
            //
            SessionManager.CurrentCulture = culture;
            //
            // Invokes the action in the current controller context.
            //
            #region Location Tag


            #endregion
            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }
    }
}
