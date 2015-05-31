<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContentTemplate.aspx.cs" Inherits="CustomerService_ModelContent" Title="Noi Dung Mau" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Danh mục nội dung mẫu</h1>    
    <div><asp:Button CssClass="button" ID="InsertButton" runat="server" OnClick="InsertButton_Click" Text="Thêm" />                </div>
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" PageSize="10" runat="server">
    </cc1:Pager>    
    
    <div>
        <table>            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand" OnItemDataBound="RepeaterData_OnItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td style="width:300px"><b><%=Resources.UIResource.TypeTemplate%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.serviceType %></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.SenderForContentTemplate%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.ReceiverForContentTemplate%></b></td>                                   
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#Eval("Description")%> </a></td>
                                    <td><%#GetNameServiceType(Eval("ServiceTypeID"))%></td>
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" /></a></td>
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("ContentTemplateID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#Eval("Description")%> </a></td>
                                    <td><%#GetNameServiceType(Eval("ServiceTypeID"))%></td>
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&contentTemplateID=<%#Eval("ContentTemplateID") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" /></a></td>
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("ContentTemplateID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
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

