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
using Vfs.WebCrawler.Destination.Business;
using Vfs.WebCrawler.Utility;
using System.IO;
using System.Text;

public partial class ExportDataForMetaStox : System.Web.UI.Page
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
        Page.Title = Resources.UIResource.ExportStox;
    }
    protected void exportButton_ServerClick(object sender, EventArgs e)
    {
        if (exportDateInput.Text == "")
        {
            return;
        }
        else
        {
            this.ExportDataStox();
        }
    }


    private void ExportDataStox()
    {
        ExportDataForMetaStoxCollection ExDataForMetaCollection = new ExportDataForMetaStoxCollection();
        if (exportMarketInput.Value == "1")
        {
            ExDataForMetaCollection = ExportDataForMetaStoxService.GetExportIndexForMetaStoxList(ApplicationHelper.ConvertStringToDate(exportDateInput.Text), Convert.ToInt32(exportMarketInput.Value));
            foreach (Vfs.WebCrawler.Destination.Entities.ExportDataForMetaStox item in ExDataForMetaCollection)
            {
                if (item.Symbol == "HOSE")
                {
                    item.Symbol = "VNINDEX";
                }
                else
                {
                    item.Symbol = "HNXINDEX";
                }
            }
        }
        else
        {
            ExDataForMetaCollection = ExportDataForMetaStoxService.GetExportDataForMetaStoxList(ApplicationHelper.ConvertStringToDate(exportDateInput.Text), Convert.ToInt32(exportMarketInput.Value));
        }
        StringBuilder str = new StringBuilder();
        str.AppendLine( "<Ticker>,<DTYYYYMMDD>,<Open>,<High>,<Low>,<Close>,<Volume>");
        foreach (Vfs.WebCrawler.Destination.Entities.ExportDataForMetaStox item in ExDataForMetaCollection)
        {            
            str.AppendLine(item.Symbol + "," + item.PermDate + "," + item.PriceOpen + "," + item.PriceHigh + "," + item.PriceLow + "," + item.PriceClose + "," + item.Volume);
        }
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + (exportMarketInput.Value == "6" ? "HOSE" : exportMarketInput.Value == "4" ? "HNX" : "IndexCCQ") + exportDateInput.Text.Replace("/", ".") + ".txt\"");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.text";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        Response.Write(str.ToString());
        Response.End();
    }
}
