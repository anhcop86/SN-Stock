using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Utility;
using Vfs.WebCrawler.Utility;

public partial class SnapShot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.SettingInterfaceLoad();
    }

    private void SettingInterfaceLoad()
    {
        this.importUpdateButton.Value = Resources.UIResource.CreateSnapShot;
        this.Page.Title = Resources.UIResource.CreateSnapShotTitle;
    }

    private void SettingInterface()
    {                
       this.successMessage.Text = string.Empty;
       this.snapShotFileRequiredFieldValidator.ErrorMessage = Resources.UIResource.SnapShotErrorMessage;
    }

    protected void CreateSnapShotButton_Click(object sender, EventArgs e)
    {
        this.SettingInterface();
        if (!Page.IsValid) return;
        try
        {
            //Vfs.WebCrawler.Destination.Business.SnapShotService snapShotService = new SnapShotService();
            //snapShotService.CreateSnapShot(snapShotFile.FileContent, ApplicationHelper.GetFullPath(ApplicationHelper.SnapShotSampleFolderPath), ApplicationHelper.GetFullPath(ApplicationHelper.SnapShotHoseOutPutFolderPath), ApplicationHelper.GetFullPath(ApplicationHelper.SnapShotHnxOutPutFolderPath));            
            //successMessage.Text = Resources.UIResource.CreateSnapShotSuccessMessage;
        }
        catch (Exception ex)
        {
            log4net.Util.LogLog.Error(ex.Message, ex);
            successMessage.Text = ex.Message;
        }
    }    
}
