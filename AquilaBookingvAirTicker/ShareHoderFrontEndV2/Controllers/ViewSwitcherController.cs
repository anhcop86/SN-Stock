using System.Web.Mvc;
using System.Web.WebPages;

public class ViewSwitcherController : ShareHoderFrontEndV2.Controllers.BaseController
{
    public RedirectResult SwitchView(bool mobile, string returnUrl)
    {
        if (Request.Browser.IsMobileDevice == mobile)
            HttpContext.ClearOverriddenBrowser();
        else
            HttpContext.SetOverriddenBrowser(mobile ? BrowserOverride.Mobile : BrowserOverride.Desktop);

        return Redirect(returnUrl);
    }
}