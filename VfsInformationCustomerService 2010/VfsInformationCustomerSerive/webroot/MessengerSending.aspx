<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MessengerSending.aspx.cs" Inherits="MessengerSending" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h1>
        <%=Resources.UIResource.MessageSendingTitle%>
    </h1>
    <div>
        <input type="button" class="button" id="InsertButton" runat="server" onserverclick="InsertButton_Click" />
        <input class="button" runat="server" id="ButtonDeleteSelect" onclick="confirmAction(event,'Bạn muốn xóa những tin đang gởi ?');"
            type="button" onserverclick="ButtonDeleteSelect_Click" />
        <input class="button" runat="server" id="ButtonDeleteAllYear" onclick="confirmAction(event,'Bạn muốn xóa tất cả tin đang gởi ?');"
            type="button" onserverclick="ButtonDeleteAllYear_Click" />
        <input type="button" class="button" id="ResentMessengerButton" runat="server" onserverclick="ResentMessengerButton_Click" />
    </div>
    <br />
    <div>
        <fieldset border="1">
            <legend>Tìm kiếm theo:</legend>
            <table>
                <tr>
                    <td>
                        <b>
                            <%=Resources.UIResource.ServiceType%>
                        </b>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="FilterServiceTypeDropdownlis" AutoPostBack="true"
                            OnSelectedIndexChanged="FilterServiceTypeDropdownlis_OnSelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <%=Resources.UIResource.SearchDate%>
                        </b>
                    </td>
                    <td>
                        <ww:jQueryDatePicker ID="FromDate" runat="server" DateFormat="dd/MM/yyyy" DisplayMode="ImageButton"
                            Width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                        </ww:jQueryDatePicker>
                    </td>
                    <td>
                        <b>
                            <%=Resources.UIResource.ToDate%>
                        </b>
                    </td>
                    <td>
                        <ww:jQueryDatePicker ID="ToDate" runat="server" DateFormat="dd/MM/yyyy" DisplayMode="ImageButton"
                            Width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                        </ww:jQueryDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <%=Resources.UIResource.SenderForContentTemplate%>
                        </b>
                    </td>
                    <td>
                        <input id="SearchSenderInput" runat="server" /></td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <%=Resources.UIResource.ReceiverForContentTemplate%>
                        </b>
                    </td>
                    <td>
                        <input id="SearchReceiverInput" runat="server" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button class="button" ID="SearchInput" runat="server" OnClick="SearchInput_Click" /></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <hr class="widthForTable" />
    <cc1:Pager OnCommand="topPaging_Command" ShowFirstLast="true" ID="topPaging" runat="server">
    </cc1:Pager>
    <div>
        <table class="widthForTable">
            <tr>
                <td>
                    <table>
                        <asp:Repeater ID="RepeaterData" runat="server" OnItemCommand="RepeaterData_ItemCommand"
                            OnItemDataBound="RepeaterData_OnItemDataBound">
                            <HeaderTemplate>
                                <tr>
                                    <td>
                                        <input id="checkAll" type="checkbox" onclick="CheckAll(this);" /></td>
                                    <td style="width:250px">
                                        <b>
                                            <%=Resources.UIResource.BodyTemplateTypeList%>
                                        </b>
                                    </td>
                                    <td style="width: 500px">
                                        <b>
                                            <%=Resources.UIResource.TemplateTypeList%>
                                        </b>
                                    </td>
                                    <td style="width: 300px">
                                        <b>
                                            <%=Resources.UIResource.SenderList%>
                                        </b>
                                    </td>
                                    <td style="width: 300px">
                                        <b>
                                            <%=Resources.UIResource.RecieveList%>
                                        </b>
                                    </td>
                                    <td style="width: 300px">
                                        <b>
                                            <%=Resources.UIResource.StatusMessage%>
                                        </b>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input id="CheckBoxDelete" name="CheckBoxDelete" type="checkbox" value='<%#Eval("MessageContentID") %>'
                                            <%#returnDisable(Eval("MessageContentID")) %> onclick="SingleCheck(this)" /></td>
                                    <td>
                                        <a href="catalogservicedetail.aspx?action=modify&callback=messagesending&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>">
                                            <%#GetNameServiceType(Eval("ServiceTypeID"))%>
                                        </a>
                                    </td>
                                    <td>
                                        <a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>">
                                            <%#GetNameContentTemplate(Eval("ContentTemplateID"))%>
                                        </a>
                                    </td>
                                    <td>
                                        <%#Eval("Sender")%>
                                    </td>
                                    <td>
                                        <%#Eval("Receiver")%>
                                    </td>
                                    <td>
                                        <%#GetStatus(Eval("Status"))%>
                                    </td>
                                    <td>
                                        <a href="MessengerSendingDetail.aspx?action=modify&messagecontentid=<%#Eval("MessageContentID") %>">
                                            <img src="_assets/img/edit-icon.gif" alt="Edit" class="borderForEditIcon" /></a></td>
                                    <td>
                                        <asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument='<%#Eval("MessageContentID") %>'
                                            ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td>
                                        <input id="CheckBoxDelete" name="CheckBoxDelete" type="checkbox" value='<%#Eval("MessageContentID") %>'
                                            <%#returnDisable(Eval("MessageContentID")) %> onclick="SingleCheck(this)" /></td>
                                    <td>
                                        <a href="catalogservicedetail.aspx?action=modify&callback=messagesending&clicked=messagercontent&serviceTypeId=<%#Eval("ServiceTypeId") %>">
                                            <%#GetNameServiceType(Eval("ServiceTypeID"))%>
                                        </a>
                                    </td>
                                    <td>
                                        <a href="ContentTeplateDetail.aspx?action=modify&clicked=messagercontent&contentTemplateID=<%#Eval("ContentTemplateID") %>">
                                            <%#GetNameContentTemplate(Eval("ContentTemplateID"))%>
                                        </a>
                                    </td>
                                    <td>
                                        <%#Eval("Sender")%>
                                    </td>
                                    <td>
                                        <%#Eval("Receiver")%>
                                    </td>
                                    <td>
                                        <%#GetStatus(Eval("Status"))%>
                                    </td>
                                    <td>
                                        <a href="MessengerSendingDetail.aspx?action=modify&messagecontentid=<%#Eval("MessageContentID") %>">
                                            <img src="_assets/img/edit-icon.gif" alt="Edit" class="borderForEditIcon" /></a></td>
                                    <td>
                                        <asp:ImageButton ID="deleteImage" runat="server" CommandName="delete" CommandArgument='<%#Eval("MessageContentID") %>'
                                            ImageUrl="~/_assets/img/deleteButon.gif" /></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <cc1:Pager OnCommand="bottomPaging_Command" ShowFirstLast="true" ID="bottomPaging"
        runat="server"></cc1:Pager>
    <hr class="widthForTable" />
</asp:Content>
