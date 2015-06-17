<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLChuKyDG.aspx.cs" Inherits="Admin_QLChuKyDG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Quản lý chu kỳ đánh giá</title>
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

<asp:Content ID="Content4" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="panel-heading">
        <h3 class="text-center">Danh Sách Chu Kỳ Đánh Giá</h3>
        <center>
        <span runat="server" id="spnTongSo"
            class="badge"></span>
            </center>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:ListView runat="server" ID="lvDSCK">
                <ItemTemplate>
                    <a href="./EditChuKyDG.aspx?id=<%#Eval("MaCK") %>" class="list-group-item">
                        <h4 class="list-group-item-heading">
                            <asp:Label runat="server" ID="lblTenCK" Text='<%#MyUtility.TiengViet? Eval("TenCK"):Eval("TenCKEN")%>'></asp:Label>
                        </h4>
                        <p class="list-group-item-text" runat="server" id="pBenBP"><%#(this.TiengViet? "Từ: ":"From: ")+((DateTime)Eval("BatDau")).ToString("dd/MM/yyyy")  %></p>
                        <p class="list-group-item-text" runat="server" id="p1"><%#(this.TiengViet? "Đến: ":"To: ")+((DateTime)Eval("KetThuc")).ToString("dd/MM/yyyy")  %></p>
                        <span runat="server" id="spnTrangThai" class="badge">
                            
                        </span>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server" ID="btnCreate" Text="Thêm chu kỳ mới"
                OnClick="btnCreate_Click"
                class="btn btn-info" />
        </div>
    </div>
</asp:Content>

