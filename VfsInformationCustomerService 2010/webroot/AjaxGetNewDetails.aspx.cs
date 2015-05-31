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
using System.IO;
using System.Text;

using Vfs.WebCrawler.Business;
using Vfs.WebCrawler.Data;
using Vfs.WebCrawler.Entities;
using Vfs.WebCrawler.Utility;

public partial class AjaxGetNewDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        Response.Clear();
        Response.ContentType = "text/html";
        Response.Charset = "UTF-8";
        Response.Write(this.GetNewDetail());
        Response.End();
    }

    protected string GetNewDetail()
    {
        Int32 newId;
        StringBuilder returnValue = new StringBuilder();
        StockNew stockNew = null;
        newId = AppConstants.GetInt32(AppConstants.QS_NEW_ID);
        stockNew = StockNewService.GetStockNew(newId);
        try
        {
            if (stockNew == null)
                return String.Format("{1}#{2}", " ", " ");
            //returnValue.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            //returnValue.Append("<news>");
            //returnValue.Append("<newsTitle>");
            returnValue.Append(StockNewService.GetStockNew(newId).NewsTitle.Replace("#",""));
            //returnValue.Append("</newsTitle>");
            //returnValue.Append("<newsContent>");
            returnValue.Append("#");
            returnValue.Append(StockNewService.GetStockNew(newId).NewsContent);
            //returnValue.Append("</newsContent>");        
            //returnValue.Append("</news>");
        }
        catch(Exception ex)
        {
            return String.Format("{1}#{2}", " ", " ");
        }
        return returnValue.ToString();
    }
}
