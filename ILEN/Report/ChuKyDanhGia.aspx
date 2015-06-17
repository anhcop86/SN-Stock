<%@ Page Title="" Language="C#" MasterPageFile="~/Report/ReportMasterPage.master" AutoEventWireup="true" CodeFile="ChuKyDanhGia.aspx.cs" Inherits="Report_ChuKyDanhGia" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerHolderId" Runat="Server">
    <title>Chu kỳ đánh giá:</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolderId" Runat="Server">
    <div>
            <h3>Đơn vị báo cáo: Bệnh viện gia định</h3>
     </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="700px" Width="1200px">
        <LocalReport ReportPath="Report\ChuKyDanhGia.rdlc">
        </LocalReport>
</rsweb:ReportViewer>
</asp:Content>

