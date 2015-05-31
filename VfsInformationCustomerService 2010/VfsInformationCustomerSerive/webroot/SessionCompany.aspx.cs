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

using CoreSecurityService.Entities;
using CoreSecurityService.Data;
using CoreSecurityService.Business;
using Vfs.WebCrawler.Utility;

public partial class SessionCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.SettingInterface();
    }

    private void SettingInterface()
    {
        this.exportButton.Value = Resources.UIResource.Export;
        this.ErrorDateInput.ErrorMessage = Resources.UIResource.NotDate;
        Page.Title = Resources.UIResource.SessionCompanyTitle;
    }
    protected void exportButton_ServerClick(object sender, EventArgs e)
    {
        if (this.exportDateInput.Text == "")
        {
            return;
        }
        else
        {
            this.ExportSessionCompany();
        }
    }

    private void ExportSessionCompany()
    {
        SessionCompanyCollection sessionCompanyCollection = new SessionCompanyCollection();
        if (exportSessionCompanySelect.Value == "HOSE")
        {
            sessionCompanyCollection = SessionCompanyService.GetSessionCompanyHoseList(ApplicationHelper.ConvertStringToDate(this.exportDateInput.Text));            
        }
        else
        {
            sessionCompanyCollection = SessionCompanyService.GetSessionCompanyHastcList(ApplicationHelper.ConvertStringToDate(this.exportDateInput.Text));
        }
        ExportService.getMarket = exportSessionCompanySelect.Value;
        ExportService.TotalSymbol = sessionCompanyCollection.Count;

        byte[] data = ExportService.ExportSessionCompanyToExcel(sessionCompanyCollection, ApplicationHelper.GetFullPath(ApplicationHelper.ExportSessionCompany));
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=\"Thongtin3gia_ngay_" + this.exportDateInput.Text + ".xls\"");
        Response.AddHeader("Content-Length", data.Length.ToString());
        Response.OutputStream.Flush();
        Response.OutputStream.Write(data, 0, data.Length);
        Response.OutputStream.Flush();
    }
}
