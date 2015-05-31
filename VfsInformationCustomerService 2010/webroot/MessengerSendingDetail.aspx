<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessengerSendingDetail.aspx.cs" Inherits="MessageContentDetail" Title="Chi tiet tin dang goi" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<div><input class="button" id="ButtonSave" runat="server" type="button" value="Lưu" onserverclick="buttonSave_ServerClick" />
         <input class="button" id="ButtonDelete" runat="server" type="button" value="Xóa" onclick="confirmAction(event,'Bạn muốn xóa loại dịch vụ này ra khỏi hệ thống ?');" onserverclick="ButtonDelete_onserverclick" />
    </div>  
    <table>      
            <tr>
                <td><%=Resources.UIResource.ChooseServiceType%><span class="inforError">(*)</span></td>
                <td style="width: 158px"><asp:DropDownList Width="150px" ID="LoadServiceType" runat="server"></asp:DropDownList></td>
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
                <td><select runat="server" id="InputBodyContentType">
                        <option value="1" selected="selected">HTML</option>
                        <option value="2">Plan text</option>
                    </select></td>                
            </tr>            
            <tr>
                <td><%=Resources.UIResource.BodyEncoding%></td>
                <td><select runat="server" id="InputBodyEncoding">
                        <option value="1" selected="selected">UTF-8</option>
                        <option value="2">Unicode</option>
                    </select>
                </td>                
            </tr>
            <tr>
                <td><%=Resources.UIResource.BodyMessager%></td>                
                <td style="width: 158px"><textarea id="InputBodyMessager" rows="10" runat="server" style="width: 268px"></textarea></td>
            </tr>            
    </table>
</asp:Content>

