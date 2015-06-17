<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLVTCV.aspx.cs" Inherits="Admin_QLVTCV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Quản lý vị trí công việc
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
    <ul class="nav nav-tabs">
        <li class="active"><a data-toggle="tab" href="#home">Profile</a></li>
    </ul>
    <div class="tab-content">
        <div id="home" class="tab-pane fade in active">
            <div class="table-responsive">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th>Họ và Tên:

                            </th>
                            <td>
                                <asp:Label runat="server" ID="lblHoTen"></asp:Label></td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>Mã Nhân Viên:</th>
                            <td>
                                <asp:Label runat="server" ID="lblMaNV"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>Chức Danh Vị Trí Công Việc:</th>
                            <td>
                                <asp:Label runat="server" ID="lblVTCV"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>Bộ Phận Trực Thuộc:</th>
                            <td>
                                <asp:Label runat="server" ID="lblBoPhan"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="panel-heading">
        <h3 class="text-center">Danh Sách Chức Danh Vị Trí Công Việc</h3>
        <center><span class="badge" runat="server" id="spnSoLuong"></span></center>
    </div>
    <div class="panel-body">
        <asp:ListView runat="server" ID="lvVTCV" class="list-group">
            <ItemTemplate>
                <a href="./EditVTCV.aspx?id=<%#Eval("MaVTCV") %>" class="list-group-item">
                    <span runat="server" id="spnTrangThai" class="badge">
                        <%#(bool)Eval("TrangThai")? (MyUtility.TiengViet?"Kích hoạt":"Active"):(MyUtility.TiengViet?"Không kích hoạt":"Inactive") %>
                    </span>
                    <h4 class="list-group-item-heading" runat="server" id="h4TenVTCV"><%#MyUtility.TiengViet? Eval("TenVTCV"):Eval("TenVTCVEN") %></h4>
                    <p class="list-group-item-text" runat="server" id="pBenBP"><%#MyUtility.TiengViet? Eval("BoPhan.TenBP"):Eval("BoPhan.TenBPEN") %></p>
                </a>
            </ItemTemplate>
        </asp:ListView>

        <div class="col-sm-offset-5 col-sm-7">
            <br />
            <asp:Button runat="server" ID="btnThem"
                OnClick="btnThem_Click"
                class="btn btn-info" Text="Create New Job Position" />
        </div>
    </div>
</asp:Content>

