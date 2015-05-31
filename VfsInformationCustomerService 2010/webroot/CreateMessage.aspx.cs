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

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using VfsCustomerService.Utility;
using Vfs.WebCrawler.Utility;


public partial class CreateMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.LoadTemplates();
    }

    private void LoadTemplates()
    {
        this.templateDropDownList.DataSource = ContentTemplateService.GetContentTemplateList(ContentTemplateColumns.ModifiedDate, "DESC");
        this.templateDropDownList.DataTextField = "Description";
        this.templateDropDownList.DataValueField = "ContentTemplateID";
        this.templateDropDownList.DataBind();
    }

    protected void importCreateMessageButton_Click(object sender, EventArgs e)
    {
        //this.SettingInterface();
        if (Page.IsValid == false) return;
        try
        {
            ImportService.CreateMessageWithEmail(this.uploadFile.FileContent, ApplicationHelper.GetFullPath(ApplicationHelper.UploadFolderPath), this.uploadFile.FileName, ContentTemplateService.GetContentTemplate(Convert.ToInt32(this.templateDropDownList.SelectedValue.ToString())));
            successMessage.Text = Resources.UIResource.importUpdateSuccessMessage;
        }
        catch (Exception ex)
        {
            log4net.Util.LogLog.Error(ex.Message, ex);
            successMessage.Text = ex.Message;
        }
    }
}