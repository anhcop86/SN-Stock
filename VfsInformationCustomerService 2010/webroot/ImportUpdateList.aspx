<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImportUpdateList.aspx.cs" Inherits="ImportUpdateList" Title="Import Stock Information" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div><input type="button"  id="importUpdateButton" runat="server" onserverclick="importUpdateButton_Click"  />
            <input type="button"  id="ShowInforMarket" runat="server" onserverclick="ShowInforMarketUpdateButton_Click"  />
    </div>
    <div><label id="importDateLable" for="importDateInput"><%= Resources.UIResource.date %></label>
    </div>
    <div><ww:jquerydatepicker id="importDateInput" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                        width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01"></ww:jquerydatepicker>
        <asp:RequiredFieldValidator ID="importDateRequiredFieldValidator" runat="server" ControlToValidate="importDateInput"
            ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator></div>
    <div><label><%= Resources.UIResource.selectFile%></label>
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
						            <td><asp:literal ID="Literal3" runat="server" Text=<%#Eval("BuyCount") %>></asp:literal></td>						        
						            <td><asp:literal ID="nameLiteral" runat="server" Text=<%#Eval("BuyQuantity") %>></asp:literal></td>						        
						            <td><asp:literal ID="martketLiteral" runat="server" Text=<%#Eval("SellCount") %>></asp:literal></td>
						            <td><asp:literal ID="totalShareLiteral" runat="server" Text=<%#Eval("SellQuantity") %>></asp:literal></td>						        
					            </tr>
					        </ItemTemplate>
					        <AlternatingItemTemplate>
					           <tr class="bg">					        
						            <td><asp:literal ID="Literal1" runat="server" Text=<%#GetStockSymbolById(Eval("SymbolID")) %>></asp:literal></td>						        						        
						            <td><asp:literal ID="Literal3" runat="server" Text=<%#Eval("BuyCount") %>></asp:literal></td>						        
						            <td><asp:literal ID="nameLiteral" runat="server" Text=<%#Eval("BuyQuantity") %>></asp:literal></td>						        
						            <td><asp:literal ID="martketLiteral" runat="server" Text=<%#Eval("SellCount") %>></asp:literal></td>
						            <td><asp:literal ID="totalShareLiteral" runat="server" Text=<%#Eval("SellQuantity") %>></asp:literal></td>
					            </tr>
					        </AlternatingItemTemplate>					        
					    </asp:Repeater>
		</table>
    </div>
</asp:Content>

