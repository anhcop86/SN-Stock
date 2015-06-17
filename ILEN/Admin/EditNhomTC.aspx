<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditNhomTC.aspx.cs" Inherits="Admin_EditNhomTC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenBP">Nhập thông tin Nhóm tiêu chí</h2>
    <p class="text-center">Quản lý Nhóm tiêu chí</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Tên Nhóm tiêu chí</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="labNhomTC">Tên Nhóm tiêu chí: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenNTC" placeholder="Nhập tên Nhóm tiêu chí" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="labTrongSo">Trọng số: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTrongSo" placeholder="Nhập Trọng số" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="labMoTa">Mô tả: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtMoTa" placeholder="Mô tả" />
                    </div>
                </div>
                <asp:Label runat="server" ID="lblLoiLuu"></asp:Label>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuuNTC" Text="Lưu" OnClick="btnLuuNTC_Click" class="btn btn-info"></asp:Button>
                        <asp:Button runat="server" ID="btnClearNTC" Text="Xóa" class="btn btn-info"></asp:Button>
                        <asp:Button runat="server" ID="btnBoQua" Text="Bỏ qua" class="btn btn-info"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
       
    </div>
</asp:Content>

