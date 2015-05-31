<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CustomerAccount.aspx.cs" Inherits="CustomerAccount" Title="Khách hàng đã mở tài khoản" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1>Danh sách khách hàng đã mở tài khoản tại VFS</h1>            
<div>
    <input type="button" id="ButtonSynchronize" runat="server" onserverclick="ButtonSynchronize_Click"/>             
</div>
      <div>
      <br />
        <fieldset border="1">
            <legend>Tìm kiếm khách hàng:</legend>
                <br />
                <table>
                    <tr>
                        <td><b><%=Resources.UIResource.CustomerId%></b></td>
                        <td><input id="InputSearchCustomerid" runat="server" maxlength="10"/></td>
                    </tr>                
                    <tr>
                        <td><b><%=Resources.UIResource.CustomerName%></b></td>
                        <td><input id="InputSearchCustomerName" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                        <td><b><%=Resources.UIResource.CustomerNameViet%></b></td>
                        <td><input id="InputSearchCustomerNameViet" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                        <td><b><%=Resources.UIResource.SearchEmail%></b></td>
                        <td><input id="InputSearchEmail" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                        <td><b><%=Resources.UIResource.TelDetail%></b></td>
                        <td><input id="InputTel" runat="server" maxlength="20" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button class="button" id="SearchInput" runat="server" OnClick="SearchInput_Click" /></td>                        
                    </tr>
                </table>
        </fieldset>
    </div>    
    <br />
    <div class="createmesseng_div">
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" runat="server">
    </cc1:Pager>    
    <div>   
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand" OnItemDataBound="RepeaterData_OnItemDataBound">
                            <HeaderTemplate>
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">         
                                    <td style="width:100px"><a id="A1" runat="server" onserverclick="SortCustomerId" href="#" ><b><%=Resources.UIResource.CustomerIdList%></b></a><a><%#GetOrderDirectionIndicator("CustomerId")%></a></td>                                    
                                    <td style="width:250px"><a id="A2" runat="server" onserverclick="SortCustomerName" href="#"><b><%=Resources.UIResource.CustomerNameList%></b></a><a><%#GetOrderDirectionIndicator("CustomerNameViet")%></a></td>
                                    <td style="width:100px"><a id="A3" runat="server" onserverclick="SortDOB" href="#"><b><%=Resources.UIResource.DobList%></b></a><a><%#GetOrderDirectionIndicator("Dob")%></a></td>                                   
                                    <td style="width:100px"><a id="A4" runat="server" onserverclick="SortMobile" href="#"><b><%=Resources.UIResource.Mobile1List%></b></a><a><%#GetOrderDirectionIndicator("Mobile")%></a></td>                                   
                                    <td style="width:210px"><a id="A5" runat="server" onserverclick="SortEmail" href="#"><b><%=Resources.UIResource.EmailList%></b></a><a><%#GetOrderDirectionIndicator("Email")%></a></td>                                    
                                </tr>
                            </HeaderTemplate>   
                            <ItemTemplate>
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                    <td><a href="CustomerAccountDetail.aspx?action=modify&customerid=<%#Eval("CustomerId") %>"><%#Eval("CustomerId")%></a></td>
                                    <td><%#Eval("CustomerNameViet")%> </a></td>
                                    <td><%#ConverFormat(Eval("Dob"))%></td>
                                    <td><%#Eval("Mobile") %></td>
                                    <td><%#Eval("Email")%></td>                                                                                                           
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate> 
                                <tr onmouseover="this.className='highlight'" onmouseout="this.className='normal'">
                                   <td><a href="CustomerAccountDetail.aspx?action=modify&customerid=<%#Eval("CustomerId") %>"><%#Eval("CustomerId")%></a></td>
                                    <td><%#Eval("CustomerNameViet")%> </a></td>
                                    <td><%#ConverFormat(Eval("Dob"))%></td>
                                    <td><%#Eval("Mobile") %></td>
                                    <td><%#Eval("Email")%></td>                                                                                                     
                                </tr>
                            </AlternatingItemTemplate>                    
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div>
    <cc1:Pager OnCommand="bottomPaging_Command" ShowFirstLast="true" id="bottomPaging" runat="server">
    </cc1:Pager>   
    </div>
    
</asp:Content>

