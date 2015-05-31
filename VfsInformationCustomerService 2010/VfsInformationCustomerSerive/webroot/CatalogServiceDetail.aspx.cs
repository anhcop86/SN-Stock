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
using Vfs.WebCrawler.Utility;

public partial class CatalogServiceDetail : System.Web.UI.Page
{
    int serviceTypeId;
    string clicked;
    string action;
    string callback;
    ServiceType serviceType;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setTextPageload();
        if (!IsPostBack)
        {
            this.Parameter();
            this.UpdateInterface();
            if (this.action == "modify") this.LoadCatalogService();
        }
    }

    private void setTextPageload()
    {
        this.ButtonSave.Value = Resources.UIResource.Save;
        this.ButtonDelete.Value = Resources.UIResource.Delete;
        this.ButtonCallBack.Value = Resources.UIResource.Cancel;
    }

    private void LoadCatalogService()
    {
        this.serviceType = ServiceTypeService.GetServiceType(this.serviceTypeId);
        if (this.serviceType != null)
        {
            this.inputNameCatalog.Value = this.serviceType.ServiceTypeDescription;
        }
    }

    private void UpdateInterface()
    {
        if (this.action == "new")
        {
            this.ButtonDelete.Visible = false;
        }
        else
        {
            this.ButtonDelete.Visible = true;
        }
    }

    private void Parameter()
    {
        this.action = AppConstants.GetString(AppConstants.QS_ACTION);
        this.clicked = AppConstants.GetString(AppConstants.QS_CLICK_ED);
        this.callback = AppConstants.GetString(AppConstants.QS_CALL_BACK);
        if (action == "modify")
        {
            this.serviceTypeId = Convert.ToInt32(AppConstants.GetString(AppConstants.QS_SERVICE_TYPE_ID));
        }
        if (this.clicked == "messagercontent")
        {
            this.ButtonDelete.Disabled = true;
            this.ButtonSave.Disabled = true;
        }
    }

    protected void buttonSave_ServerClick(object sender, EventArgs e)
    {
        this.Parameter();
        if (this.inputNameCatalog.Value == "")
        {
            this.lableInfor.InnerText = Resources.UIResource.MissingInfor;
            this.lableInfor.Visible = true;
            return;
        }

        ///////////////////////
        if (this.action == "modify")
        {
            serviceType = ServiceTypeService.GetServiceType(this.serviceTypeId);
        }
        else
        {
            serviceType = new ServiceType();
        }
        ////////////////////////
        if (serviceType != null)
        {
            serviceType.ServiceTypeDescription = this.inputNameCatalog.Value;


        }

        ////////////////////////
        if (this.action == "modify")
        {
            serviceType.ModifiedDate = DateTime.Now;
            ServiceTypeService.UpdateServiceType(serviceType);
        }
        else
        {
            if (ExistNameCatalog(this.inputNameCatalog.Value) == true)
            {
                this.lableInfor.InnerText = Resources.UIResource.ErrorExistNameServiceType;
                this.lableInfor.Visible = true;
                return;
            }
            serviceType.CreatedDate = serviceType.ModifiedDate = DateTime.Now;
            ServiceTypeService.CreateServiceType(serviceType);
        }
        Response.Redirect("CatalogService.aspx");
    }
    protected void ButtonDelete_onserverclick(object sender, EventArgs e)
    {
        this.Parameter();
        if (Common.ExistServiceTypeIdForMessageContent(Convert.ToInt32(this.serviceTypeId)) == true || Common.ExistsServiceTypeForContentTemplate(Convert.ToInt32(this.serviceTypeId)) == true || Common.ExistsServiceTypeForMessageContentSent(Convert.ToInt32(this.serviceTypeId)) == true)
        {
            this.ButtonDelete.Disabled = true;
            return;
        }
        ServiceTypeService.DeleteServiceType(this.serviceTypeId);
        Response.Redirect("CatalogService.aspx");
    }
    protected bool ExistNameCatalog(string NameCatalog)
    {
        bool resual = false;
        ServiceTypeCollection serviceTypeCollection = new ServiceTypeCollection();
        serviceTypeCollection = ServiceTypeService.ExistServiceTypeIdForServiceType(NameCatalog);
        if (serviceTypeCollection.Count > 0) resual = true;
        return resual;
    }

    protected void ButtonCallBack_ServerClick(object sender, EventArgs e)
    {
        this.Parameter();
        if (this.callback == "messagersent")
        {
            Response.Redirect("MessengerSent.aspx");
        }
        else if (this.callback == "messagesending")
        {
            Response.Redirect("MessengerSending.aspx");
        }
        else
        {
            Response.Redirect("CatalogService.aspx");
        }
    }
}
