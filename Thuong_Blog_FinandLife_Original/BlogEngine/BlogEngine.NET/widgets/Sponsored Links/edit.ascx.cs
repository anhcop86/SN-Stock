using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using BlogEngine.Core;
using App_Code.Controls;

public partial class widgets_SponsoredLinks_edit : WidgetEditBase
{

    #region Properties

    private List<Ad> adCollection;
    private int _currentIndex= 0;
    private int CurrentIndex
    {
        set
        {
            HttpRuntime.Cache[AdManagemenConstants.CurrentIndex] = _currentIndex = value;        
        }

        get
        {
            if (HttpRuntime.Cache[AdManagemenConstants.CurrentIndex] != null)
                Int32.TryParse(HttpRuntime.Cache[AdManagemenConstants.CurrentIndex].ToString(), out _currentIndex);
            else
                HttpRuntime.Cache[AdManagemenConstants.CurrentIndex] = _currentIndex;

            return _currentIndex;
        }
    }

    #endregion

    #region Page LifeCycle

    protected override void  OnLoad(EventArgs e)
    {

 	 base.OnLoad(e);

     if (!Page.IsPostBack)
        {

        CurrentIndex = 0;
        StringDictionary settings = GetSettings();
        adCollection = AdManagementBase.DeSerializeAds(settings[AdManagemenConstants.AdCollectionKey]);
                
        if ((adCollection == null) || (adCollection.Count < 1))
        {
            adCollection = new List<Ad>();
            Ad defaultAd = new Ad(Guid.NewGuid(), AdManagemenConstants.DefaultAdText, AdManagemenConstants.DefaultAdName, AdManagemenConstants.DefaultAdWeight);
            adCollection.Add(defaultAd);
        }
             cbAuthZAds.Checked = ((settings[AdManagemenConstants.HideAdsForAuthZUsersKey] != null) && (settings[AdManagemenConstants.HideAdsForAuthZUsersKey].ToLower() == true.ToString().ToLower())) ? true : false;
             DisplayData();
             HttpRuntime.Cache[AdManagemenConstants.SerializedAdsKey] = AdManagementBase.SerializeAds(adCollection);
        }
        else
            adCollection = AdManagementBase.DeSerializeAds(HttpRuntime.Cache[AdManagemenConstants.SerializedAdsKey].ToString());


    }

    #endregion

    #region WidgetEditBase Interface Implementation

    public override void Save()
	{
        doUpdate();
        StringDictionary settings = GetSettings();
        settings[AdManagemenConstants.AdCollectionKey] = AdManagementBase.SerializeAds(adCollection);
        settings[AdManagemenConstants.HideAdsForAuthZUsersKey] = cbAuthZAds.Checked.ToString();
        SaveSettings(settings);
        HttpRuntime.Cache.Remove(AdManagemenConstants.WidgetSettingsKey);
        HttpRuntime.Cache.Remove(AdManagemenConstants.SerializedAdsKey);
        CurrentIndex = 0;

    }

    #endregion

    #region Event Handlers

    protected void BtnNext_Click(object sender, EventArgs e)
    {
        doUpdate();
        CurrentIndex++;
        DisplayData();
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        doUpdate();
        CurrentIndex--;
        DisplayData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Guid id=Guid.NewGuid();
        Ad newAd = new Ad(id,AdManagemenConstants.EmptyAdText,AdManagemenConstants.EmptyAdName+adCollection.Count, AdManagemenConstants.EmptyAdWeight);
        adCollection.Add(newAd);
        doUpdate();
        CurrentIndex = adCollection.FindIndex(delegate(Ad ad) { return ad.ID == id;  });
        DisplayData();

        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        adCollection.RemoveAt(CurrentIndex);
        HttpRuntime.Cache[AdManagemenConstants.SerializedAdsKey] = AdManagementBase.SerializeAds(adCollection);
        CurrentIndex--;
        if ((adCollection.Count > 1) && (CurrentIndex < 0)) CurrentIndex = 0;

        DisplayData();
    }

    #endregion

    #region Helper Methods

    protected void DisplayData()
    {
        if (CurrentIndex > -1)
        {
            btnDelete.Enabled = btnPrev.Enabled = btnNext.Enabled = txtName.Enabled = txtTag.Enabled = txtWeight.Enabled = true;
            txtName.Text = adCollection[CurrentIndex].TagName;
            txtTag.Text = adCollection[CurrentIndex].Script;
            txtWeight.Text = adCollection[CurrentIndex].RWeight.ToString();
            previewArea.InnerHtml = HttpUtility.HtmlDecode(adCollection[CurrentIndex].Script);

            if (CurrentIndex == 0) btnPrev.Enabled = false;
            if (CurrentIndex == adCollection.Count - 1) btnNext.Enabled = false;
            btnDelete.Enabled = adCollection.Count > 0;


        }
        else
        {
            btnDelete.Enabled = btnPrev.Enabled = btnNext.Enabled = txtName.Enabled = txtTag.Enabled = txtWeight.Enabled = false;
            txtName.Text = txtTag.Text = txtWeight.Text = string.Empty;
        }
    }

    protected void doUpdate()
    {
        adCollection[CurrentIndex].TagName = txtName.Text;
        adCollection[CurrentIndex].Script = txtTag.Text;
        int rw = 0;
        Int32.TryParse(txtWeight.Text, out rw);
        adCollection[CurrentIndex].RWeight = rw;
        HttpRuntime.Cache[AdManagemenConstants.SerializedAdsKey] = AdManagementBase.SerializeAds(adCollection);
        DisplayData();
    }

    #endregion
}
