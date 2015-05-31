<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessengerSending.aspx.cs" Inherits="MessengerSending" Title="Tin dang goi" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1>Tin đang gởi</h1>    
    <div><asp:Button CssClass="button" ID="InsertButton" runat="server" OnClick="InsertButton_Click" Text="Thêm" /> </div>
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
                                <td style="width:300px"><b><%=Resources.UIResource.BodyServiceType%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.BodyTemplateType%></b></td>                                    
                                    <td style="width:300px"><b><%=Resources.UIResource.SenderForContentTemplate%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.ReceiverForContentTemplate%></b></td>                                   
                                    <td style="width:300px"><b><%=Resources.UIResource.StatusMessage%></b></td>                                    
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><a href="catalogservicedetail.aspx?action=modify&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#GetNameServiceType(Eval("ServiceTypeID"))%> </a></td>                                                                        
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#GetNameContentTemplate(Eval("ContentTemplateID"))%></a></td>
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>
                                    <td><%#GetStatus(Eval("Status"))%></td>
                                    <td><a href="MessengerSendingDetail.aspx?action=modify&messagecontentid=<%#Eval("MessageContentID") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" /></a></td>
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("MessageContentID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                     <td><a href="catalogservicedetail.aspx?action=modify&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#GetNameServiceType(Eval("ServiceTypeID"))%> </a></td>                                                                        
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#GetNameContentTemplate(Eval("ContentTemplateID"))%></a></td>
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>
                                    <td><%#GetStatus(Eval("Status"))%></td>
                                    <td><a href="MessengerSendingDetail.aspx?action=modify&messagecontentid=<%#Eval("MessageContentID") %>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" /></a></td>
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("MessageContentID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
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

