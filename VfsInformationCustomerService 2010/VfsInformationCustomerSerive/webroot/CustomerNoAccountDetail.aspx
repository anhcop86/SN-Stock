<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CustomerNoAccountDetail.aspx.cs" Inherits="CustomerNoAccountDetail" Title="Untitled Page" %>
<%@ Register Assembly="jQueryDatePicker" Namespace="Westwind.Web.Controls" TagPrefix="ww" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<h1 runat="server" id="header1"></h1>    
<div>
     <input class="button" id="ButtonSave" runat="server" type="button" value="Lưu" onserverclick="buttonSave_ServerClick" />
     <input class="button" id="ButtonDelete" runat="server" type="button" onclick="confirmAction(event,'Bạn muốn xóa khách hàng này ra khỏi hệ thống ?');" onserverclick="ButtonDelete_onserverclick" />
     <input class="button" id="ButtonCallBack" runat="server" type="button"  onserverclick="ButtonCallBack_onserverclick" />
</div>  
    <table >      
        <tr>
            <td><%=Resources.UIResource.CustomerId%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputCustomerId" maxlength="10" style="width:100px"/><label id="labelExistsCustomer" runat="server" visible="true" class="inforError"></label>
                <label id="ErrorInfo" runat="server" visible="true" class="inforError"></label>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.CustomerName%></td>
            <td><input runat="server" id="InputCustomerName" maxlength="100" style="width:200px" />
                <label id="ErrorInfoCustomerName" runat="server" visible="true" class="inforError"></label>
            </td>            
        </tr>
        <tr>
            <td><%=Resources.UIResource.CustomerNameViet%><span class="span">(*)</span></td>
            <td><input runat="server" id="InputCustomerNameViet" maxlength="100" style="width:200px" />
            <label id="ErrorInfoName" runat="server" visible="true" class="inforError"></label></td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.DomesticForeign%></td>
            <td>
                <select runat="server" id="SelectDomesticForeign">
                    <option value="D" selected="selected">Trong nước</option>
                    <option value="F">Nước ngoài</option>
                </select>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.DobDetail%></td>
            <td><ww:jquerydatepicker id="Selectdob" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                    width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                </ww:jquerydatepicker>
                <label id="ErroInfoDob" runat="server" visible="true" class="inforError"></label>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.SexDetail%></td>
            <td>
                <select runat="server" id="SelectSex">
                    <option value="M">Nam</option>
                    <option value="F">Nữ</option>                    
                </select>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.OpendateDetail%></td>
             <td><ww:jquerydatepicker id="SelectOpendate" runat="server" dateformat="dd/MM/yyyy" displaymode="ImageButton"
                    width="93px" MaxDate="2025-12-31" MaxLength="10" MinDate="1990-01-01">
                    </ww:jquerydatepicker>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.CardIdentityDetail%></td>
            <td><input runat="server" id="InputCardIdentityDetail" maxlength="20" />
                <label id="ErrorInfoCardIdentityDetail" runat="server" visible="true" class="inforError"></label>
            </td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.AddressDetail%></td>
            <td><textarea runat="server" rows="2" cols="40" id="InputAddress" style="font-family:Arial"></textarea></td>
            <%--<td><input runat="server" id="InputAddress" maxlength="200" style="width:400px" /></td>--%>
        </tr>
        <tr>
            <td><%=Resources.UIResource.TelDetail%></td>
            <td><input runat="server" id="inputTel" maxlength="20" /></td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.FaxDetail%></td>
            <td><input runat="server" id="InputFax" maxlength="20" /></td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.Mobile1Detail%></td>
            <td><input runat="server" id="InputMobile1" /></td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.Mobile2Detail%></td>
            <td><input runat="server" id="InputModile2" /></td>
        </tr>
        <tr>
            <td><%=Resources.UIResource.SearchEmail%></td>
            <td><input runat="server" id="inputEmail" style="width:250px" /></td>
        </tr>
    </table>    
</asp:Content>

