<%@ Page Title="" Language="C#" MasterPageFile="~/Report/ReportMasterPage.master" AutoEventWireup="true" CodeFile="ChiTietDanhGia.aspx.cs" Inherits="Report_ChuKyDanhGia" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerHolderId" Runat="Server">
    <title>Chu kỳ đánh giá:</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolderId" Runat="Server">
    <div class="contentCenter">   
        <table class="tableFilter">
            <tr>
                <td>Chọn chu kỳ đánh giá:</td>
                <td>
                    <asp:DropDownList runat="server" ID ="ListChukySelect" AutoPostBack="true" >                   
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>Chọn biểu mẫu đánh giá:</td>
                <td>
                    <asp:DropDownList runat="server" ID ="ListBieuMauSelect" AutoPostBack="true" >                   
                    </asp:DropDownList>
                </td>
            </tr>
            </table>    
        </div>
    
</asp:Content>

