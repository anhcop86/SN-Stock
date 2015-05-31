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

public partial class StatisticTransaction : System.Web.UI.Page
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
    }
    protected void exportButton_ServerClick(object sender, EventArgs e)
    {
        if (this.exportDateInput.Text == "")
        {
            return;
        }
        else
        {
            this.ExportStatisticTransaction();        
        }        
    }

    private void ExportStatisticTransaction()
    {
        int sequenceNo = 1;
        double SumBuyCount = 0;
        double SumBuyQuatity = 0;
        double SumSellCount = 0;
        double SumSellQuatity = 0;
        double SumVolume = 0;
        double SumTotalValue = 0;


        statisticTransactionCollection statisticTransactionCollection = new statisticTransactionCollection();
        statisticTransactionCollection = statisticTransactionService.ExportStatisticTransaction(ApplicationHelper.ConvertStringToDate(this.exportDateInput.Text), exportStatisticTranasctionSelect.Value);

        
        foreach (statisticTransaction statisticTransaction in statisticTransactionCollection)
        {
            statisticTransaction.No = sequenceNo;
            sequenceNo++;
            SumBuyCount = SumBuyCount + statisticTransaction.BuyCount;
            SumBuyQuatity = SumBuyQuatity + statisticTransaction.BuyQuantity;
            SumSellCount = SumSellCount + statisticTransaction.SellCount;
            SumSellQuatity = SumSellQuatity + statisticTransaction.SellQuantity;
            SumVolume = SumVolume + statisticTransaction.Volume;
            SumTotalValue = SumTotalValue + statisticTransaction.TotalValue;
        }

        Vfs.WebCrawler.Destination.Business.ExportService.SumBuyCount = SumBuyCount;
        Vfs.WebCrawler.Destination.Business.ExportService.SumBuyQuatity = SumBuyQuatity;
        Vfs.WebCrawler.Destination.Business.ExportService.SumSellCount = SumSellCount;
        Vfs.WebCrawler.Destination.Business.ExportService.SumSellQuatity = SumSellQuatity;
        Vfs.WebCrawler.Destination.Business.ExportService.SumChange = SumBuyQuatity - SumSellQuatity;
        if (SumBuyQuatity == 0 || SumBuyCount == 0)
        {
            Vfs.WebCrawler.Destination.Business.ExportService.SumDVDMTrungBinhTrenLenh = 0;
        }
        else
        {
            Vfs.WebCrawler.Destination.Business.ExportService.SumDVDMTrungBinhTrenLenh = SumBuyQuatity / SumBuyCount;
        }
        if (SumSellQuatity == 0 || SumSellCount == 0)
        {
            Vfs.WebCrawler.Destination.Business.ExportService.SumDVDBTrungBinhTrenLenh = 0;
        }
        else
        {
            Vfs.WebCrawler.Destination.Business.ExportService.SumDVDBTrungBinhTrenLenh = SumSellQuatity / SumSellCount;
        }         

        Vfs.WebCrawler.Destination.Business.ExportService.SumVolume = SumVolume;
        Vfs.WebCrawler.Destination.Business.ExportService.SumTotalValue = SumTotalValue;

        Vfs.WebCrawler.Destination.Business.ExportService.getDate = exportDateInput.Text;
        Vfs.WebCrawler.Destination.Business.ExportService.getMarket = exportStatisticTranasctionSelect.Value;
        
        byte[] data = Vfs.WebCrawler.Destination.Business.ExportService.ExportStatisticTransactionToExcel(statisticTransactionCollection, ApplicationHelper.GetFullPath(ApplicationHelper.ExportStatisticTransaction));
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=\"ThongKeGiaoDich.xls\"");
        Response.AddHeader("Content-Length", data.Length.ToString());
        Response.OutputStream.Flush();
        Response.OutputStream.Write(data, 0, data.Length);
        Response.OutputStream.Flush();
    }
}
