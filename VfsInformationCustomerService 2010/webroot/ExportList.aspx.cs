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

using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Utility;


public partial class ExportList : System.Web.UI.Page
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
        this.exportButton.Value = Resources.UIResource.export;
        this.ErrorDateInput.ErrorMessage = Resources.UIResource.NotDate;
    }
    protected void exportButton_ServerClick(object sender, EventArgs e)
    {
        if (exportDateInput.Text == "")
        {
            return;
        }
        else
        {            
            this.Export_symbolPermLong();
        }
    }
    private void Export_symbolPermLong()
    {
        stock_SymbolPermLongExtensionCollection stock_SymbolPermLongExtensionCollection = new stock_SymbolPermLongExtensionCollection();
        stock_SymbolPermLongExtensionCollection = stock_SymbolPermLongService.Export_SymbolPermLongList(ApplicationHelper.ConvertStringToDate(exportDateInput.Text), exportMarketInput.Value);

        byte[] data = Vfs.WebCrawler.Destination.Business.ExportService.ExportStock_SymbolPermLongToExcel(stock_SymbolPermLongExtensionCollection, ApplicationHelper.GetFullPath(ApplicationHelper.ExportSymbolPermLong));
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=\"InfoMartketReview.xls\"");
        Response.AddHeader("Content-Length", data.Length.ToString());
        Response.OutputStream.Flush();
        Response.OutputStream.Write(data, 0, data.Length);
        Response.OutputStream.Flush();
    }
}
