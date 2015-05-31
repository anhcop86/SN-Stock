<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessengerSent.aspx.cs" Inherits="CustomerService_MessengerSent" Title="Tin da goi" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1>Tin đã gởi</h1>    
    <div><asp:Button CssClass="button" ID="InsertButton" runat="server" OnClick="InsertButton_Click" Text="Thêm" /></div>
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
                                    <td style="width:300px"><b>Loại nội dung mẫu</b></td>                                    
                                    <td style="width:300px"><b>Loại dịch vụ</b></td>
                                    <td style="width:300px"><b>Người gởi</b></td>                                   
                                    <td style="width:300px"><b>Người nhận</b></td>
                                    <td style="width:300px"><b>Định dạng gởi</b></td>
                                    <td style="width:300px"><b>Kiểu mã</b></td>
                                    <td style="width:300px"><b>Nội dung</b></td>                                                                        
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>                                    
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#GetNameContentTemplate(Eval("ContentTemplateID"))%></a></td>
                                    <td><a href="catalogservicedetail.aspx?action=modify&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#GetNameServiceType(Eval("ServiceTypeID"))%> </a></td>                                                                                                            
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>                                                                        
                                    <td><%#GetNameBodyContentType(Eval("BodyContentType"))%></td>
                                    <td><%#GetNameBodyEncoding(Eval("BodyEncoding"))%></td>
                                    <td><%#Eval("BodyMessage")%></td>                                   
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("MessageContentSentID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                   <td><a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#GetNameContentTemplate(Eval("ContentTemplateID"))%></a></td>
                                    <td><a href="catalogservicedetail.aspx?action=modify&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#GetNameServiceType(Eval("ServiceTypeID"))%> </a></td>                                                                                                            
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>                                                                        
                                    <td><%#GetNameBodyContentType(Eval("BodyContentType"))%></td>
                                    <td><%#GetNameBodyEncoding(Eval("BodyEncoding"))%></td>
                                    <td><%#Eval("BodyMessage")%></td>                                   
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("MessageContentSentID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
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

