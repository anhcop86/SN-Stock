<%@ Page Title="" Language="C#" MasterPageFile="~/Report/ReportMasterPage.master" AutoEventWireup="true" CodeFile="Report_BaoCaoNhanVien_Admin.aspx.cs" Inherits="Report_Report_BaoCaoNhanVien_Admin" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerHolderId" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolderId" Runat="Server">
    <div class="contentCenter">   
        <table class="tableFilter">
            <tr>
                <td>Vai trò:</td>
                <td>
                    <asp:DropDownList runat="server" ID ="ListVaiTroSelect" AutoPostBack="true" OnSelectedIndexChanged="ListVaiTroSelect_SelectedIndexChanged">                   
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>Bộ phận:</td>
                <td>
                    <asp:DropDownList runat="server" ID ="ListBoPhanSelect" AutoPostBack="true" OnSelectedIndexChanged="ListBoPhanSelect_SelectedIndexChanged" >                   
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="9pt" Height="700px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1200px" Font-Strikeout="False">
            <LocalReport ReportPath="Report\Report_BaoCaoNhanVien_Admin.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>

