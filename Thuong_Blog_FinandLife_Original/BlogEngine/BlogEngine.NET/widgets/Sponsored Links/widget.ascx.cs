using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using App_Code.Controls;


#region ASP Cache Concreat Adapter

public class ASPCache: ICache 
    {
        private System.Web.UI.Page _page;

        public ASPCache(System.Web.UI.Page page)
        {
            _page = page;
        }

        public void Insert<T>(string key, T obj)
        {
            _page.Cache.Insert(key, obj);
        }

        public T GetObject<T>(string key)
        {
            return (T)_page.Cache[key];
        }

    }

#endregion

public partial class themes_Refresh_AdRotator : WidgetBase
{
    #region Properties

    AdManagementBase adManager;
    int totalWeight = 0;

    #endregion

    #region WidgetBase Interface implementation

    public override string Name
        {
            get { return AdManagemenConstants.WidgetName; }
        }
    
    public override bool IsEditable
        {
            get { return true; }
        }
    
    public override void LoadWidget()
    {
            ASPCache aspCache=new ASPCache(this.Page);
            StringDictionary settings = GetSettings();
            List<Ad> ads = AdManagementBase.DeSerializeAds(settings[AdManagemenConstants.AdCollectionKey]);
            ads.ForEach(delegate(Ad ad) { totalWeight += ad.RWeight; });
            
            Boolean hideForAuthenticatedUsers= true;
            Boolean.TryParse(settings[AdManagemenConstants.HideAdsForAuthZUsersKey],out hideForAuthenticatedUsers);

            adManager = new AdManagementBase(aspCache, ads);

            if (!IsPostBack)
            {
                if ((!System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated) || (!hideForAuthenticatedUsers))
                    ShowAds(totalWeight);
                else
                    adHolder.InnerHtml = AdManagemenConstants.AuthZUserMessage;

            }
    }

    #endregion

    #region Helper Method

    private void ShowAds(int weight)
        {
            Ad current = weight==0?adManager.GetNextRandomAd():adManager.GetNextRandomAdWithWeightedRatio(weight);
            if (current != null)
            {
                AdID.Value = current.ID.ToString();
                adHolder.InnerHtml = HttpUtility.HtmlDecode(current.Script.ToString());
            }
        }

    #endregion

}

