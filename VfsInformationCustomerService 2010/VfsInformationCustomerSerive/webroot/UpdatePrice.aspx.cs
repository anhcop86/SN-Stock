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
using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Utility;

public partial class UpdatePrice : System.Web.UI.Page
{
    stock_SymbolPermLong stock_SymbolPermLong;
    int symbolId;
    string permDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.loadStockSymbol();
        }
    }

    private void paramater()
    {
        this.symbolId = Convert.ToInt32(this.DropdownlistSelectSymbol.Value);
        this.permDate = this.SelectPermDate.Text;
    }

    private void loadStockSymbol()
    {
        this.DropdownlistSelectSymbol.DataSource = stock_SymbolService.Getstock_SymbolList(stock_SymbolColumns.Symbol, "ASC");
        this.DropdownlistSelectSymbol.DataTextField = "Symbol";
        this.DropdownlistSelectSymbol.DataValueField = "SymbolId";
        this.DataBind();
    }

    protected void buttonSave_ServerClick(object sender, EventArgs e)
    {
        this.saveSymbol();
    }

    private void saveSymbol()
    {
        this.paramater();
        this.label1.InnerText = "";
        this.label2.InnerText = "";
        this.label3.InnerText = "";
        this.label4.InnerText = "";
        this.label5.InnerText = "";
        this.label6.InnerText = "";
        this.label7.InnerText = "";


        if (this.SelectPermDate.Text == string.Empty)
        {
            this.label1.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }
        if (this.InputPriceOpen.Value == string.Empty)
        {
            this.label2.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }
        if (this.InputPriceClose.Value == string.Empty)
        {
            this.label3.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }
        if (this.InputPriceHight.Value == string.Empty)
        {
            this.label4.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }
        if (this.InputPriceLow.Value == string.Empty)
        {
            this.label5.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }
        if (this.InputVolume.Value == string.Empty)
        {
            this.label6.InnerText = Resources.UIResource.ErrorInfo;
            return;
        }
        if (this.IsNumber(this.InputPriceOpen.Value) == false||
            this.IsNumber(this.InputPriceClose.Value) == false||
            this.IsNumber(this.InputPriceHight.Value) == false||
            this.IsNumber(this.InputPriceLow.Value) == false||                       
            this.IsNumber(this.InputVolume.Value) == false)
        {
            this.label7.InnerText = Resources.UIResource.TestInfo;
            this.label7.Attributes["class"] = "inforError";
            return;
        }
        
        stock_SymbolPermLong = stock_SymbolPermLongService.Getstock_SymbolPermLong(symbolId, ApplicationHelper.ConvertStringToDate(permDate));
        
        if (stock_SymbolPermLong == null)
        {
            stock_SymbolPermLong = new stock_SymbolPermLong();
            stock_SymbolPermLong = this.getValueSymobl(stock_SymbolPermLong);
           stock_SymbolPermLongService.Createstock_SymbolPermLong(stock_SymbolPermLong);
        }
        else
        {
            stock_SymbolPermLong = this.getValueSymobl(stock_SymbolPermLong);
            stock_SymbolPermLongService.Updatestock_SymbolPermLong(stock_SymbolPermLong);
        }
        this.label7.InnerText = Resources.UIResource.UpdateSeccess;
        this.label7.Attributes["class"] = "inforSeccessful";
    }

    private stock_SymbolPermLong getValueSymobl(stock_SymbolPermLong sk_SymbolPermLong)
    {
        sk_SymbolPermLong.SymbolID = Convert.ToInt32(this.DropdownlistSelectSymbol.Value);
        sk_SymbolPermLong.PermDate = ApplicationHelper.ConvertStringToDate(this.SelectPermDate.Text);
        sk_SymbolPermLong.PriceOpen= Convert.ToInt64(this.InputPriceOpen.Value);
        sk_SymbolPermLong.PriceClose = Convert.ToInt64(this.InputPriceClose.Value);
        sk_SymbolPermLong.PriceHigh = Convert.ToInt64(this.InputPriceHight.Value);
        sk_SymbolPermLong.PriceLow = Convert.ToInt64(this.InputPriceLow.Value);
        sk_SymbolPermLong.PriceAverage = this.InputPriceAverage.Value == string.Empty ? 0 : Convert.ToInt64(this.InputPriceAverage.Value);
        sk_SymbolPermLong.PricePreviousClose = this.InputPricePreviousClose.Value == string.Empty ? 0 : Convert.ToInt64(this.InputPricePreviousClose.Value);
        sk_SymbolPermLong.Volume = Convert.ToInt64(this.InputVolume.Value);
        sk_SymbolPermLong.TotalTrade = this.InputTotalTrade.Value == string.Empty ? 0 : Convert.ToInt64(this.InputTotalTrade.Value);
        sk_SymbolPermLong.TotalValue = this.InputTotalValue.Value == string.Empty ? 0 : Convert.ToInt64(this.InputTotalValue.Value);
        sk_SymbolPermLong.AdjRatio = this.InputAdjRatio.Value == string.Empty ? 0 : Convert.ToDouble(this.InputAdjRatio.Value);
        sk_SymbolPermLong.LastUpdated = ApplicationHelper.ConvertStringToDate(this.SelectPermDate.Text);
        //sk_SymbolPermLong.AdjRatio = this.InputAdjRatio.Value == string.Empty ? 0 : Convert.ToInt64(this.InputAdjRatio.Value);
        sk_SymbolPermLong.CurrentForeignRoom = this.InputCurrentForeignRoom.Value == string.Empty ? 0 : Convert.ToInt64(this.InputCurrentForeignRoom.Value);
        sk_SymbolPermLong.BuyCount = this.InputBuyCount.Value == string.Empty ? 0 : Convert.ToInt64(this.InputBuyCount.Value);
        sk_SymbolPermLong.BuyQuantity = this.InputBuyQuantity.Value == string.Empty ? 0 : Convert.ToInt64(this.InputBuyQuantity.Value);
        sk_SymbolPermLong.SellCount = this.InputSellCount.Value == string.Empty ? 0 : Convert.ToInt64(this.InputSellCount.Value);
        sk_SymbolPermLong.SellQuantity = this.InputSellQuantity.Value == string.Empty ? 0 : Convert.ToInt64(this.InputSellQuantity.Value);
        sk_SymbolPermLong.BuyForeignCount = this.InputBuyForeignCount.Value == string.Empty ? 0 : Convert.ToInt64(this.InputBuyForeignCount.Value);
        sk_SymbolPermLong.BuyForeignQuantity = this.InputBuyForeignQuantity.Value == string.Empty ? 0 : Convert.ToInt64(this.InputBuyForeignQuantity.Value);
        sk_SymbolPermLong.BuyForeignValue = this.InputBuyForeignValue.Value == string.Empty ? 0 : Convert.ToInt64(this.InputBuyForeignValue.Value);
        sk_SymbolPermLong.SellForeignCount = this.InputSellForeignCount.Value == string.Empty ? 0 : Convert.ToInt64(this.InputSellForeignCount.Value);
        sk_SymbolPermLong.SellForeignQuantity = this.InputSellForeignQuantity.Value == string.Empty ? 0 : Convert.ToInt64(this.InputSellForeignQuantity.Value);
        sk_SymbolPermLong.SellForeignValue = this.InputSellForeignValue.Value == string.Empty ? 0 : Convert.ToInt64(this.InputSellForeignValue.Value);

        return sk_SymbolPermLong;
    }
    private bool IsNumber(string s)
    {
        try
        {
            Int32.Parse(s);
        }
        catch
        {
            return false;
        }
        return true;
    }
    
}
