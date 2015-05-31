<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestTool.aspx.cs" Inherits="TestTool" Title="Cập nhật giá theo ngày" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1> Cập nhật giá theo ngày </h1>
     <div>        
                <table width="100%">                    
                    <tr>
                        <td><b><%=Resources.UIResource.FromDate%></b></td>
                        <td><ww:jquerydatepicker id="FromDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                            width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                            </ww:jquerydatepicker>
                            
                        </td>
                        <td><asp:RequiredFieldValidator ID="FromDateValidator" runat="server" ControlToValidate="FromDate"
                                ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                        <td><b><%=Resources.UIResource.ToDate%></b></td>
                        <td><ww:jquerydatepicker id="ToDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                            width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                            </ww:jquerydatepicker>
                            
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="ToDateValidator" runat="server" ControlToValidate="ToDate"
                                    ErrorMessage="RequiredFieldValidator" EnableClientScript="false"></asp:RequiredFieldValidator>
                                    </td>
                    </tr>                                        
                    <tr>
                        <td><asp:Button class="button" id="SearchInput" runat="server" OnClick="SearchInput_Click" /></td>
                        <td></td>
                        <td><b style="color:Purple">KL trung bình phiên:</b></td>
                        <td><input type="text" runat="server" id="countAVGInput" style="width:24px" maxlength="3" /></td>
                    </tr>                   
                </table>
       
    </div> 
     <div style="margin-left:100px" runat="server">       
    <div>
        <table style="border:solid black 1px">
        <tr>
            <td><b>Ngày:</b></td>
            <td><%=FromDate.Text%></td>
            <asp:Repeater ID="IndexToRepeater" runat="server">
                <ItemTemplate>
                    <td id="Symbol"><b><asp:literal ID="Literal2" runat="server" Text=<%#Eval("Symbol") %>></asp:literal></b></td>
                    <td><asp:literal ID="Literal6" runat="server" Text=<%#Eval("IndexSymbol") %>></asp:literal></td>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <td id="Symbol"><b><asp:literal ID="Literal2" runat="server" Text=<%#Eval("Symbol") %>></asp:literal></b></td>
                    <td><asp:literal ID="Literal6" runat="server" Text=<%#Eval("IndexSymbol") %>></asp:literal></td>
                </AlternatingItemTemplate>
            </asp:Repeater>
                
        </tr>
        <tr>
            <td><b>Ngày:</b></td>
            <td><%=ToDate.Text%></td>
            <asp:Repeater ID="IndexFromRepeater" runat="server">
                <ItemTemplate>
                    <td id="Symbol"><b><asp:literal ID="Literal2" runat="server" Text=<%#Eval("Symbol") %>></asp:literal></b></td>
                    <td><asp:literal ID="Literal6" runat="server" Text=<%#Eval("IndexSymbol") %>></asp:literal></td>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <td id="Symbol"><b><asp:literal ID="Literal2" runat="server" Text=<%#Eval("Symbol") %>></asp:literal></b></td>
                    <td><asp:literal ID="Literal6" runat="server" Text=<%#Eval("IndexSymbol") %>></asp:literal></td>
                </AlternatingItemTemplate>
            </asp:Repeater>
                
        </tr>
            <tr>
                <td>Mã CP</td>
                <td>Giá Thấp</td>
                <td >Giá Đóng</td>
                <td style="border-left: solid black 1px">Mã CP</td>
                <td>Giá Cao</td>
                <td>Giá Đóng</td>
                <td style="margin:10px;color:Purple"><b>KL Trung Bình</b></td>
                       
            </tr>
            <asp:Repeater ID="symbolPermLongRepeater" runat="server">
					        <ItemTemplate>
					            <tr>
					                <td><b><asp:literal ID="Literal2" runat="server" Text=<%#Eval("SymbolFrom") %>></asp:literal></b></td>
						            <td ><asp:literal ID="Literal4" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceLowFrom")) %>></asp:literal></td>
						            <td><asp:literal ID="Literal5" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceCloseFrom")) %>></asp:literal></td>
					                <td><b><asp:literal ID="Literal1" runat="server" Text=<%#Eval("SymbolTo") %>></asp:literal></b></td>
						            <td><asp:literal ID="Literal3" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceHighTo")) %>></asp:literal></td>
						            <td><asp:literal ID="nameLiteral" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceCloseTo")) %>></asp:literal></td>
						            <td><asp:literal ID="Literal7" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("AVGVolume")) %>></asp:literal></td>
					            </tr>
					        </ItemTemplate>
					        <AlternatingItemTemplate>
					           <tr>					        
						            <td><b><asp:literal ID="Literal2" runat="server" Text=<%#Eval("SymbolFrom") %>></asp:literal></b></td>
						            <td><asp:literal ID="Literal4" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceLowFrom")) %>></asp:literal></td>
						            <td><asp:literal ID="Literal5" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceCloseFrom")) %>></asp:literal></td>
					                <td><b><asp:literal ID="Literal1" runat="server" Text=<%#Eval("SymbolTo") %>></asp:literal></b></td>
						            <td><asp:literal ID="Literal3" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceHighTo")) %>></asp:literal></td>
						            <td><asp:literal ID="nameLiteral" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("PriceCloseTo")) %>></asp:literal></td>	
						            <td><asp:literal ID="Literal7" runat="server" Text=<%#string.Format("{0,0:N0}",Eval("AVGVolume")) %>></asp:literal></td>					            
					            </tr>
					        </AlternatingItemTemplate>					        
			</asp:Repeater>
		</table>
    </div> 
  
   </div>
</asp:Content>