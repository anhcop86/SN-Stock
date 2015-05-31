<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdatePrice.aspx.cs" Inherits="UpdatePrice" Title="Update Price" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1><%=Resources.UIResource.UpdatePriceHeader%></h1>
    <label runat="server" id="label7" class=""></label>
<hr class="widthForTable" />
    <table >      
        <tr>
            <td><%=Resources.UIResource.SymbolUpdateList%><span class="span">(*)</span></td>            
            <td><select runat="server" id="DropdownlistSelectSymbol"></select></td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.SearchDate%><span class="span">(*)</span></td>
            <td><ww:jquerydatepicker id="SelectPermDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                    width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                    
                </ww:jquerydatepicker>
                <label runat="server" id="label1" class="inforError"></label>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.PriceOpen%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputPriceOpen"/><label runat="server" id="label2" class="inforError"></label></td>            
        </tr>        
        <tr>
            <td><%=Resources.UIResource.PriceClose%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputPriceClose"/><label runat="server" id="label3" class="inforError"></label></td>            
        </tr>        
        <tr>
            <td><%=Resources.UIResource.PriceHight%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputPriceHight"/><label runat="server" id="label4" class="inforError"></label></td>            
        </tr>        
        <tr>
            <td><%=Resources.UIResource.PriceLow%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputPriceLow"/><label runat="server" id="label5" class="inforError"></label></td>            
        </tr>        
        <tr>
            <td><%=Resources.UIResource.PriceAverage%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputPriceAverage"/></td>            
        </tr> 
        <tr>
            <td style="width:200px"><%=Resources.UIResource.PricePreviousClose%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputPricePreviousClose"/></td>            
        </tr>        
        
        <tr>
            <td><%=Resources.UIResource.VolumeUpdatePrice%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputVolume"/><label runat="server" id="label6" class="inforError"></label></td>            
        </tr>        
        <tr>
            <td><%=Resources.UIResource.TotalTrade%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputTotalTrade"/></td>            
        </tr>        
        <tr>
            <td><%=Resources.UIResource.TotalValue%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputTotalValue"/></td>            
        </tr>
        
          <tr>
            <td><%=Resources.UIResource.AdjRatio%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputAdjRatio"/></td>            
        </tr>                       
        <tr>
            <td><%=Resources.UIResource.BuyCount%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputBuyCount"/></td>            
        </tr>
        <tr>
            <td><%=Resources.UIResource.BuyQuantity%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputBuyQuantity"/></td>            
        </tr>
         <tr>
            <td><%=Resources.UIResource.SellCount%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputSellCount"/></td>            
        </tr>
         <tr>
            <td><%=Resources.UIResource.SellQuantity%><span class="spanNoNeed">(*)</span></td>
            <td><input runat="server" id="InputSellQuantity"/></td>            
         </tr>         
         
         
    </table>    
    <br />    
    <hr class="widthForTable" />
    <h5>Nhà đầu tư nước ngoài</h5>
    <table>
        <tr>
            <td style="width:200px"><%=Resources.UIResource.CurrentForeignRoom%></td>
            <td><input runat="server" id="InputCurrentForeignRoom"/></td>            
        </tr>
        <tr>
            <td><%=Resources.UIResource.BuyCount%></td>
            <td><input runat="server" id="InputBuyForeignCount"/></td>            
         </tr> 
         <tr>
            <td><%=Resources.UIResource.BuyQuantity%></td>
            <td><input runat="server" id="InputBuyForeignQuantity"/></td>            
         </tr> 
         <tr>
            <td><%=Resources.UIResource.BuyForeignValue%></td>
            <td><input runat="server" id="InputBuyForeignValue"/></td>            
         </tr> 
         <tr>
            <td><%=Resources.UIResource.SellCount%></td>
            <td><input runat="server" id="InputSellForeignCount"/></td>            
         </tr> 
         <tr>
            <td><%=Resources.UIResource.SellQuantity%></td>
            <td><input runat="server" id="InputSellForeignQuantity"/></td>            
         </tr> 
         <tr>
            <td><%=Resources.UIResource.SellForeignValue%></td>
            <td><input runat="server" id="InputSellForeignValue"/></td>            
         </tr> 
    </table>
    
    <hr class="widthForTable" />
    <div>
        <input id="ButtonSave" runat="server" type="button" value="Cập nhật" onserverclick="buttonSave_ServerClick" />
    </div>  
</asp:Content>

