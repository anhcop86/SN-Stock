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

public partial class PorfolioSms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {      
     
    }

    private void LoadInfo()
    {
        this.SendMessengerButton.Value = Resources.UIResource.create;
        this.Title = Resources.UIResource.PorfolioSMS;
        this.DeleteMessengerButton.Value = Resources.UIResource.Delete;
        this.infoError.InnerText = "";
       
        
        LoadExtensionMessages();
    }

    private void SettingInterface1()
    {
        this.TitleTextBoxValidate.ErrorMessage = string.Empty;
        this.ContentTextBoxValidate.ErrorMessage = string.Empty;
    }
    private void SettingInterface2()
    {
        this.TitleTextBoxValidate.ErrorMessage = Resources.UIResource.ErrorInfo;
        this.ContentTextBoxValidate.ErrorMessage = Resources.UIResource.ErrorInfo;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.LoadInfo();       
    }

    private void LoadExtensionMessages()
    {
        try
        {
            var collection = ExtensionMessageService.GetExtensionMessageList(ExtensionMessageColumns.CreatedDate, "DESC");
            this.RepeaterData.DataSource = ExtensionMessageService.GetExtensionMessageList(ExtensionMessageColumns.CreatedDate, "DESC");
            this.RepeaterData.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void AddNewExtensionMessage()
    {        
        ExtensionMessage extensionMessage = new ExtensionMessage();
        extensionMessage.Content = this.ContentTextBox.Text;
        extensionMessage.CreatedDate = DateTime.Now.Date;
        extensionMessage.Title = this.TitleTextBox.Text;
        ExtensionMessageService.CreateExtensionMessage(extensionMessage);
        LoadExtensionMessages();
    }
    protected void SendMessengerButton_Click(object sender, EventArgs e)
    {
        SettingInterface2();
        if (Page.IsValid == false) return;
        this.AddNewExtensionMessage();
    }
    protected void DeleteMessengerButton_Click(object sender, EventArgs e)
    {
        Int64 extensionMessageID;
        string selectedItems = Request.Form["CheckBoxDelete"];
        if (selectedItems == string.Empty || selectedItems == null) return;
        foreach (string selectedItemId in selectedItems.Split(','))
        {
            extensionMessageID = Convert.ToInt64(selectedItemId);
            ExtensionMessageService.DeleteExtensionMessage(extensionMessageID);
        }
        LoadExtensionMessages();
    }
 
}
