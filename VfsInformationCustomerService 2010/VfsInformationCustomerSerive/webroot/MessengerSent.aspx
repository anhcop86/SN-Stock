<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessengerSent.aspx.cs" Inherits="CustomerService_MessengerSent"%>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1>Tin đã gởi</h1>    
  
    <div>
        <fieldset border="1">
            <legend>Tìm kiếm theo:</legend>
                <table>
                    <tr>
                        <td><b><%=Resources.UIResource.ServiceType%></b></td>
                        <td><asp:DropDownList runat="server" ID="FilterServiceTypeDropdownlis" AutoPostBack="true" OnSelectedIndexChanged="FilterServiceTypeDropdownlis_OnSelectedIndexChanged"></asp:DropDownList></td>
                    </tr>                
                    <tr>
                        <td><b><%=Resources.UIResource.SearchDate%></b></td>
                        <td><ww:jquerydatepicker id="FromDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                            width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                            </ww:jquerydatepicker>
                        </td>
                        <td><b><%=Resources.UIResource.ToDate%></b></td>
                        <td><ww:jquerydatepicker id="ToDate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                            width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                            </ww:jquerydatepicker>
                        </td>
                    </tr>
                    <tr>
                        <td><b><%=Resources.UIResource.SenderForContentTemplate%></b></td>
                        <td><input id="SearchSenderInput" runat="server"/></td>
                    </tr>
                    <tr>
                        <td><b><%=Resources.UIResource.ReceiverForContentTemplate%></b></td>
                        <td><input id="SearchReceiverInput" runat="server"/></td>
                    </tr>
                    <tr>
                        <td><asp:Button class="button" id="SearchInput" runat="server" OnClick="SearchInput_Click" /></td>                        
                    </tr>
                </table>
        </fieldset>
    </div>    
    <hr class="widthForTable" />
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" id="topPaging" runat="server">
    </cc1:Pager>    
    <div>   
        <table class="widthForTable">            
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand" OnItemDataBound="RepeaterData_OnItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td><input id="checkAll" type="checkbox" onclick="CheckAll(this);" /></td>
                                    <td style="width:400px"><b><%=Resources.UIResource.TypeTemplate%></b></td>                                    
                                    <td style="width:300px"><b><%=Resources.UIResource.ServiceTypeList%></b></td>
                                    <td style="width:300px"><b><%=Resources.UIResource.SenderList%></b></td>                                   
                                    <td style="width:300px"><b><%=Resources.UIResource.RecieveList%></b></td>                                    
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>                                
                                    <td><input type="checkbox"  id="CheckBoxDelete" name="CheckBoxDelete" value=<%#Eval("MessageContentSentID")%> onclick="SingleCheck(this)"  /></td>
                                    <td><a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#GetNameContentTemplate(Eval("ContentTemplateID"))%></a></td>
                                    <td><a href="catalogservicedetail.aspx?action=modify&callback=messagersent&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#GetNameServiceType(Eval("ServiceTypeID"))%> </a></td>                                                                                                            
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>                                                                        
                                   <td><a href="MessengerSentDetail.aspx?action=modify&messagecontentsentid=<%#Eval("MessageContentSentID")%>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" class="borderForEditIcon" /></a></td>         
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("MessageContentSentID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                   <td><input type="checkbox"  id="CheckBoxDelete" name="CheckBoxDelete" value=<%#Eval("MessageContentSentID")%> onclick="SingleCheck(this)" /></td>                                    
                                   <td><a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>"><%#GetNameContentTemplate(Eval("ContentTemplateID"))%></a></td>
                                    <td><a href="catalogservicedetail.aspx?action=modify&callback=messagersent&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>"><%#GetNameServiceType(Eval("ServiceTypeID"))%> </a></td>                                                                                                            
                                    <td><%#Eval("Sender")%></td>
                                    <td><%#Eval("Receiver")%></td>                                                                        
                                    <td><a href="MessengerSentDetail.aspx?action=modify&messagecontentsentid=<%#Eval("MessageContentSentID")%>" ><img  src="_assets/img/edit-icon.gif" alt="Edit" class="borderForEditIcon" /></a></td>         
                                    <td><asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument=<%#Eval("MessageContentSentID") %> ImageUrl="~/_assets/img/deleteButon.gif" /></td>
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
    <hr class="widthForTable" />
</asp:Content>

