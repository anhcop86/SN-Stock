<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImportListUpdateForeign.aspx.cs" Inherits="ImportListUpdateForeign" Title="Untitled Page" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1> Cập nhật thông tin lịch sử giá nhà đầu tư nước ngoài </h1>
<div><input type="button"  id="importUpdateButton" runat="server" onserverclick="importUpdateButton_Click"  />
            <input type="button"  id="ShowInforMarket" runat="server" onserverclick="ShowInforMarketUpdateButton_Click"  />
            <input type="button"  id="DeleteVolumn" runat="server" onserverclick="DeleteVolumn_Click"  />
    </div>
    <div><label id="importDateLable" for="importDateInput"><%= Resources.UIResource.Date %></label>
    </div>
    <div><ww:jquerydatepicker id="importDateInput" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                        width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01"></ww:jquerydatepicker>
        <asp:RequiredFieldValidator ID="importDateRequiredFieldValidator" runat="server" ControlToValidate="importDateInput"
            ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator></div>
    <div><label><%= Resources.UIResource.SelectFile%></label>
    </div>
    <div>
        <asp:FileUpload id="uploadFile" runat="server"/>
        <asp:RequiredFieldValidator ID="uploadFileRequiredFieldValidator" runat="server" ControlToValidate="uploadFile"
        ErrorMessage="uploadFileRequired" EnableClientScript="false"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Literal runat="server" ID="successMessage"></asp:Literal>
    </div>    
    <div>
        <table border="1">
            <tr>
                <td>Mã CP</td>
                <td>Lệnh đặt mua</td>
                <td>Khối lượng mua</td>
                <td>Lệnh đặt bán</td>
                <td>Khối lượng bán</td>                
            </tr>
            <asp:Repeater ID="symbolPermLongRepeater" runat="server">
					        <ItemTemplate>
					            <tr>
					                <td><asp:literal ID="Literal1" runat="server" Text=<%#GetStockSymbolById(Eval("SymbolID")) %>></asp:literal></td>
						            <td><asp:literal ID="Literal3" runat="server" Text=<%#Eval("BuyForeignCount") %>></asp:literal></td>						        
						            <td><asp:literal ID="nameLiteral" runat="server" Text=<%#Eval("BuyForeignQuantity") %>></asp:literal></td>						        
						            <td><asp:literal ID="martketLiteral" runat="server" Text=<%#Eval("SellForeignCount") %>></asp:literal></td>
						            <td><asp:literal ID="totalShareLiteral" runat="server" Text=<%#Eval("SellForeignQuantity") %>></asp:literal></td>						        
					            </tr>
					        </ItemTemplate>
					        <AlternatingItemTemplate>
					           <tr class="bg">					        
						            <td><asp:literal ID="Literal1" runat="server" Text=<%#GetStockSymbolById(Eval("SymbolID")) %>></asp:literal></td>						        						        
						            <td><asp:literal ID="Literal3" runat="server" Text=<%#Eval("BuyForeignCount") %>></asp:literal></td>						        
						            <td><asp:literal ID="nameLiteral" runat="server" Text=<%#Eval("BuyForeignQuantity") %>></asp:literal></td>						        
						            <td><asp:literal ID="martketLiteral" runat="server" Text=<%#Eval("SellForeignCount") %>></asp:literal></td>
						            <td><asp:literal ID="totalShareLiteral" runat="server" Text=<%#Eval("SellForeignQuantity") %>></asp:literal></td>
					            </tr>
					        </AlternatingItemTemplate>					        
					    </asp:Repeater>
		</table>
    </div>
</asp:Content>