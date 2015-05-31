<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessengerSentDetail.aspx.cs" Inherits="MessengerSentDetail"%>
<%@ Register Assembly="Jiffycms.Net.Toolkit" Namespace="Jiffycms.Net.Toolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div>
        <input class="button" id="ButtonDelete" runat="server" type="button" onclick="confirmAction(event,'Bạn muốn xóa loại dịch vụ này ra khỏi hệ thống ?');" onserverclick="ButtonDelete_onserverclick" />         <input class="button" id="ButtonCallBack" runat="server" type="button" value="Cancel" onserverclick="ButtonCallBack_onserverclick" />
    </div>  
    <table class="widthForTable">      
            <tr>
                <td class="widthtd"><%=Resources.UIResource.ChooseServiceType%><span class="inforError">(*)</span></td>
                <td><asp:DropDownList Width="150px" ID="LoadServiceType" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><%=Resources.UIResource.BodyContentType%><span class="inforError">(*)</span></td>
                <td style="width: 158px"><asp:DropDownList Width="150px" ID="LoadTemplateType" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><%=Resources.UIResource.SenderForContentTemplate%></td>
                <td style="width: 158px"><input runat="server" id="InputSender" maxlength="100" style="width: 269px" /></td>
            </tr>
            <tr>
                <td><%=Resources.UIResource.ReceiverForContentTemplate%></td>
                <td style="width: 158px"><input runat="server" id="InputReceiver" maxlength="100" style="width: 269px" /></td>
            </tr>
            <tr>
                <td><%=Resources.UIResource.SubjectForContentTemplate%></td>
                <td style="width: 158px"><input runat="server" id="inputSuject" maxlength="50" style="width: 269px" /></td>
            </tr>
              <tr>
                    <td><%=Resources.UIResource.BodyContentType%></td>
                    <td><asp:DropDownList ID="InputBodyContentType" runat="server" AutoPostBack="true">
                            <asp:ListItem Selected="True" Text="HTML" Value ="HTML"></asp:ListItem>
                            <asp:ListItem  Text="Plain text" Value ="PlainText"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                
                </tr>            
                <tr>
                    <td><%=Resources.UIResource.BodyEncoding%></td>
                    <td><select runat="server" id="InputBodyEncoding" disabled="disabled">
                            <option value="UTF-8" selected="selected">UTF-8</option>
                            <option value="Unicode">Unicode</option>
                        </select>
                    </td>                
                </tr>
            <tr>
                <td><%=Resources.UIResource.BodyMessager%></td>     
                <td><cc1:Editor runat="server" ID="InputBodyMessager" style="width: 268px"></cc1:Editor></td>
            </tr>            
    </table>
</asp:Content>

