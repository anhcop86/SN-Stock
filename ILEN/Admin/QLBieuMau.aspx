<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="QLBieuMau.aspx.cs" Inherits="Admin_QLBieuMau" %>

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
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">
      	    <h3 class="text-center">Danh Sách Biểu Mẫu Đánh Giá</h3>
      	    <center><span class="badge">04</span></center>
        </div>
        <div class="panel-body">
            <asp:ListView runat="server" ID="lvDSBM" class="list-group">
                <ItemTemplate>
                    <div class="list-group-item">
                        <asp:LinkButton runat="server" class="badge" ID="lbtnTTBM"
                            OnClick="lbtnEditTTBM_Click"
                            CommandArgument='<%#Eval("MaBM") %>'>
                            <%#(bool.Parse(Eval("Chon").ToString()))?"Active":"Passive" %>
                        </asp:LinkButton>
                        <h5 class="list-group-item-heading" runat="server" id="h5TenTC">
                                <asp:LinkButton runat="server" ID="lbtnTenBM"
                                OnClick="lbtnEditBM_Click"
                                CommandArgument='<%#Eval("MaBM") %>'
                                Text='<%#Eval("TenBM") %>'></asp:LinkButton>
                        </h5>
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <div class="col-sm-offset-5 col-sm-7">
                <br />
                <asp:Button runat="server" ID="btnThem"
                    OnClick="btnThem_Click"
                    class="btn btn-info" Text="Tạo thêm biểu mẫu" />
            </div>
        </div>
    </div>
</asp:Content>

