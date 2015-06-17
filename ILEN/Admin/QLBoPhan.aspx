<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLBoPhan.aspx.cs" Inherits="Admin_QLBoPhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
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
        <h3 class="text-center">Danh Sách Bộ phận trực thuộc</h3>
        <center>
        <span runat="server" id="spnSoBP" class="badge"></span>
            </center>
    </div>
    <div class="panel-body">
        <div class="list-group">
            <asp:ListView runat="server" ID="lvDSBP">
                <ItemTemplate>
                    <a href="./EditBoPhan.aspx?id=<%#Eval("MaBP") %>" class="list-group-item">
                        <h4 class="list-group-item-heading">
                            <asp:Label runat="server" ID="lblTenBP" Text='<%#MyUtility.TiengViet?Eval("TenBP"):Eval("TenBPEN") %>'></asp:Label>
                        </h4>
                        <span runat="server" id="spnTrangThai" class="badge">
                            <%#(bool)Eval("TrangThai")? (MyUtility.TiengViet?"Kích hoạt":"Active"):(MyUtility.TiengViet?"Không kích hoạt":"Inactive") %>
                        </span>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col-sm-offset-5 col-sm-7">
            <asp:Button runat="server" ID="btnCreate"
                OnClick="btnCreate_Click"
                Text="Button"
                class="btn btn-info" />
        </div>
    </div>
</asp:Content>

