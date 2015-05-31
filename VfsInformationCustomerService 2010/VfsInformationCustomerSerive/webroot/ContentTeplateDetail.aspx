<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContentTeplateDetail.aspx.cs" Inherits="ContentTeplateDetail" Title="Chi tiet noi dung mau" ValidateRequest="false" %>

<%@ Register Assembly="Jiffycms.Net.Toolkit" Namespace="Jiffycms.Net.Toolkit" TagPrefix="cc1" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div style="width:100%">    
        <div><input class="button" id="ButtonSave" runat="server" type="button" value="Lưu" onserverclick="buttonSave_ServerClick" />
             <input class="button" id="ButtonDelete" runat="server" type="button" value="Xóa" onclick="confirmAction(event,'Bạn muốn xóa loại dịch vụ này ra khỏi hệ thống ?');" onserverclick="ButtonDelete_onserverclick" />
             <input class="button" id="ButtonCallBack" runat="server" type="button" value="Cancel" onserverclick="ButtonCallBack_onserverclick" />
        </div>
        <table class="widthForTable">
                <tr>
                    <td></td>
                    <td><label class="inforError" visible="true" runat="server" id="errorCheckMaxleng"></label></td>
                </tr>        
                <tr>
                    <td style="width:130px"><%=Resources.UIResource.NameTemplate%><span class="inforError">(*)</span></td>
                    <td ><input runat="server" id="inputNameTemplate" maxlength="50" style="width: 267px" /><label class="inforError" visible="false" runat="server" id="lableInfor"></label></td>                    
                </tr>
                <tr>
                    <td><%=Resources.UIResource.ChooseServiceType%><span class="inforError">(*)</span></td>
                    <td ><asp:DropDownList Width="150px" ID="LoadServiceType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadServiceType_OnSelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><%=Resources.UIResource.SenderForContentTemplate%></td>
                    <td ><input runat="server" id="InputSender" maxlength="100" style="width: 269px" /></td>
                </tr>
                <tr>
                    <td><%=Resources.UIResource.ReceiverForContentTemplate%></td>
                    <td ><input runat="server" id="InputReceiver" maxlength="100" style="width: 269px" /></td>
                </tr>
                <tr>
                    <td ><%=Resources.UIResource.SubjectForContentTemplate%></td>
                    <td ><input runat="server" id="inputSuject" maxlength="50" style="width: 268px" /></td>
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
                    <td style="padding-bottom:300px"><%=Resources.UIResource.BodyMessager%></td>                
                    <td>
                        <cc1:Editor runat="server" ID="InputBodyMessager" style="width: 600px; height: 400px"></cc1:Editor>
                    </td>
                </tr>
                
                <tr>
                    <td><%=Resources.UIResource.ToBeAttachedDoc%></td>                
                    <td>
                        <asp:FileUpload runat="server" ID="attchementFile" ></asp:FileUpload>
                    </td>
                </tr>
                <tr>
                    <td><%=Resources.UIResource.Attachement%></td>                
                    <td><asp:ListBox runat="server" ID="contentTemplateAttchementDropDownList"></asp:ListBox><asp:Button runat="server" ID="deleteAttchementButton" OnClick="DeleteAttchement" />                    
                    </td>
                </tr>                        
        </table>
    </div>            
</asp:Content>

