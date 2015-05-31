<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CatalogService.aspx.cs" Inherits="CustomerService_CatalogService" Title="Danh muc dich vu" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Danh mục dịch vu</h1>    
    <div><asp:Button CssClass="button" ID="InserMessage" runat="server" OnClick="InsertMessageContent_Click" Text="Thêm" /></div>
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" PageSize="10" runat="server">
    </cc1:Pager>    
    
    <div>
        <table>            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterMessageContent" runat="server" OnItemCommand="RepeaterServiceType_ItemCommand" OnItemDataBound="RepeaterServiceType_OnItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td style="width:150px"><b><%=Resources.UIResource.serviceType %></b></td>                                    
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><a href="catalogservicedetail.aspx?action=modify&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#Eval("ServiceTypeDescription")%> </a></td>                                    
                                    <td><a href="catalogservicedetail.aspx?action=modify&serviceTypeId=<%#Eval("ServiceTypeId") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" /></a></td>
                                    <td></a><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("ServiceTypeId") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td><a href="catalogservicedetail.aspx?action=modify&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#Eval("ServiceTypeDescription")%> </a></td>                                    
                                    <td><a href="catalogservicedetail.aspx?action=modify&serviceTypeId=<%#Eval("ServiceTypeId") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" /></a></td>
                                    <td></a><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("ServiceTypeId") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>                
            </tr>
        </table>
    </div>
    <cc1:Pager OnCommand="bottomPaging_Command" ShowFirstLast="true" id="bottomPaging" PageSize="10" runat="server">
    </cc1:Pager>   
</asp:Content>

