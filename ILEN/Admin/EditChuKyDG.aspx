<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditChuKyDG.aspx.cs" Inherits="Admin_EditChuKyDG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenCK"></h2>
    <p class="text-center">Quản Lý Chu Kỳ Đánh Giá</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Thông tin chu kỳ đánh giá</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="tenchukydanhgia">Chọn biểu mẫu: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" class="form-control" ID="ddlBM"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBM_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <label class="control-lable col-sm-2" for="tenchukydanhgia">Chọn thang điểm: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" class="form-control" ID="ddlTD"></asp:DropDownList>
                    </div>
                    <label class="control-lable col-sm-2" for="tenchukydanhgia">Tên chu kỳ đánh giá: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenCK" placeholder="Vui lòng nhập tên chu kỳ đánh giá"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="tenchukydanhgia">The name of ...: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenCKEN" placeholder="Vui lòng nhập tên chu kỳ đánh giá"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2">Ngày bắt đầu: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            placeholder="dd/mm/yyyy"
                            class="form-control" ID="txtNgayBD"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="ngayketthuc">Ngày kết thúc: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server"
                            placeholder="dd/mm/yyyy"
                            class="form-control" ID="txtNgayKT"></asp:TextBox>
                    </div>
                    <label class="control-lable col-sm-2" for="status">Tình Trạng: </label>
                    <div class="col-sm-10">
                        <div class="radio">
                            <asp:RadioButtonList ID="rblTT" runat="server"></asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuu" class="btn btn-info"
                            OnClick="btnLuu_Click"
                            Text="Save"></asp:Button>
                        <input type="reset" value="Clear all" class="btn btn-info" />
                        <asp:Button runat="server" ID="btnXemKQ" class="btn btn-success" Text="Xem Kết Quả Đánh Giá"></asp:Button>
                        <asp:Button runat="server" ID="btnXoaCK" class="btn btn-danger"
                            OnClick="btnXoaCK_Click"
                            Text="Xóa Chu Kỳ Đánh Giá"></asp:Button>
                        <br />
                        <asp:Label runat="server" ID="lblTB"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

