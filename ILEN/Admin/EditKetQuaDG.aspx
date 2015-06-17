<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditKetQuaDG.aspx.cs" Inherits="Admin_EditKetQuaDG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
    <div class="col-sm-9">
        <ul class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#home">Profile</a></li>
            <li><a data-toggle="tab" href="#menu1">More Information</a></li>
        </ul>
        <div class="tab-content">
            <div id="home" class="tab-pane fade in active">
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Họ và Tên:</th>
                                <th>
                                    <asp:Label runat="server" ID="lblHoTen"></asp:Label>

                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th>Mã Nhân Viên:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblMaNV"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <th>Chức Danh Vị Trí Công Việc:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblVTCT"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Bộ Phận Trực Thuộc:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblBoPhan"></asp:Label>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="menu1" class="tab-pane fade">
                <div class="table-responsive">
                    <table class="table table-hover">
                        <tbody>
                            <tr>
                                <th>Sex:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblGioiTinh"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Date of Birth:</th>
                                <td>
                                    <asp:Label runat="server" ID="lblNgaySinh"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2HoTen">Nguyễn Văn B</h2>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Kết quả đánh giá</div>
            <div class="panel-body">
                <h1>dua Biểu Đồ vao day</h1>
                <asp:Button runat="server" ID="btnExport" Text="Export" class="btn btn-success" />
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Trang thai người được đánh giá</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="status">Tình Trạng: </label>
                    <div class="col-sm-10">
                        <asp:RadioButtonList runat="server" ID="rblTrangThai"></asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" ID="btnLuu" Text="Save" class="btn btn-success" />
                        <asp:Button runat="server" ID="btnXoa" Text="Xóa Người Được Đánh Giá" class="btn btn-danger" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">Thêm người đánh giá</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="jobposition">Người Đánh Giá: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" ID="ddlDS"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button runat="server" ID="btnThem" Text="Thêm" class="btn btn-success" />
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="text-center">Danh Sách Người Đánh Giá</h3>
            <span class="badge">04</span>
        </div>
        <div class="panel-body">
            <asp:ListView runat="server" ID="lvDS" class="list-group">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblVTCV"></asp:Label>
                    <asp:Label runat="server" ID="lblHoTen"></asp:Label>
                    <asp:CheckBox runat="server" ID="chkChon"></asp:CheckBox>
                </ItemTemplate>
            </asp:ListView>
            <div class="col-sm-offset-5 col-sm-7">
                <asp:Button runat="server" ID="btnXoaTat" class="btn btn-danger" Text="Xóa tất cả" />
                <asp:Button runat="server" ID="btnXoaNhieu" class="btn btn-danger" Text="Xóa" />
            </div>
        </div>
    </div>
</asp:Content>

