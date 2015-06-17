<%@ Page Title="EditBieuMau" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="EditBieuMau.aspx.cs" Inherits="Admin_EditBieuMau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        .an
        {
            display: none;
        }
        .test
        {
            float:left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenBM">Nhập thông tin Biểu mẫu</h2>
    <p class="text-center">Quản Lý Biểu Mẫu</p>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Thông tin biểu mẫu</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="tenbieumau">Tên Biểu Mẫu: </label>
                    <div class="col-sm-10">
                        <asp:HiddenField runat="server" ID="hdfMaBM" />
                        <asp:TextBox runat="server" class="form-control" ID="txtTenBM" placeholder="Vui lòng nhập tên biểu mẫu" />
                    </div>

                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="jobposition">Biểu Mẫu Dành Cho: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" ID="ddlVTCV" CssClass="form-control"></asp:DropDownList>
                        <asp:Label runat="server" ID="lblThongBao"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button
                            runat="server" ID="btnLuuBM"
                            class="btn btn-info" Text="Save" OnClick="btnLuuBM_Click"></asp:Button>
                        <asp:Button
                            runat="server" ID="btnClearBM"
                            class="btn btn-info" Text="Clear"></asp:Button>
                        <asp:Button
                            runat="server" ID="Button1"
                            class="btn btn-danger" Text="Xóa biểu mẫu"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="text-center">Danh Sách Tiêu Chí</h3>
            <center><span class="badge" runat="server" id="spnSoLuong">Dua so luong vao day</span></center>
        </div>
        <div class="panel-body">
            <!--co fai yeu cau la nhom tc roi tung nhom se view tc?-->
            <asp:ListView runat="server" ID="lvDSNTC" class="list-group" OnSelectedIndexChanged="lvDSNTC_SelectedIndexChanged">
                <ItemTemplate>
                    <div class="list-group-item">
                        <span class="badge" runat="server" id="spnTrongSo">Tỉ trọng: 
                            <asp:TextBox runat="server" CssClass="txtTrongSo" ID="txtTrongSoNTC" Text='<%#Eval("TrongSo") %>' />%
                        </span>
                        <asp:LinkButton runat="server" class="badge" ID="lbtnTTTC"
                            OnClick="lbtnEditTTNTC_Click"
                            CommandArgument='<%#Eval("MaNTC") %>'>
                                    <%#(bool.Parse(Eval("Chon").ToString()))?"Active":"Passive" %>
                        </asp:LinkButton>
                        <h4 class="list-group-item-heading" runat="server" id="h4TenTC"><%#Eval("TenNTC") %></h4>
                    </div>
                    <asp:HiddenField runat="server" ID="hdfNTC"
                        Value='<%#Eval("MaNTC") %>' />
                    <asp:ListView runat="server" ID="lvDSTC" class="list-group">
                        <ItemTemplate>
                            <div class="list-group-item">
                                <span class="badge" runat="server" id="spnTrongSo">
                                    <div class="test">Tỉ trọng: </div>
                                    <asp:Panel runat="server" ID="pnlPro" CssClass="test" DefaultButton="btnSave">
                                        <asp:TextBox runat="server" CssClass="txtTrongSo" ID="txtTrongSoTC"
                                            Text='<%#Eval("TrongSo") %>' />%
                                        <asp:Button runat="server" CommandArgument='<%#Eval("MaTC") %>'
                                            OnClick="btnSave_Click"
                                            ID="btnSave" CssClass="an" />
                                    </asp:Panel>
                                </span>
                                <asp:LinkButton class="badge" runat="server" ID="lbtnTTTC"
                                    OnClick="lbtnEditTTTC_Click"
                                    CommandArgument='<%#Eval("MaTC") %>'>
                                    <%#(bool.Parse(Eval("Chon").ToString()))?"Active":"Passive" %>
                                </asp:LinkButton>
                                <h5 class="list-group-item-heading" runat="server" id="h4TenTC"><%#Eval("TenTC") %></h5>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </ItemTemplate>
            </asp:ListView>

        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="text-center">Thêm Tiêu chí vào Biểu mẫu</h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <label class="control-lable col-sm-2" for="jobposition">Chọn nhóm Tiêu Chí: </label>
                <div class="col-sm-10">
                    <asp:DropDownList runat="server" ID="ddlNTC"
                        OnSelectedIndexChanged="onSelectedNTC_Click"
                        CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="form-group" style="margin-top: 40px">
                <asp:ListView runat="server" ID="lvDSTCTheoNTC" class="list-group">
                    <ItemTemplate>
                        <div class="list-group-item">

                            <span style="float: right; margin-left: 10px">
                                <asp:HiddenField runat="server" ID="hdfMaTC" Value='<%#Eval("MaTC") %>' />
                                <asp:CheckBox runat="server"
                                    ID="chbTieuChi" Checked="false"></asp:CheckBox>
                            </span>
                            <span class="badge" runat="server" id="spnTrongSo">Tỉ trọng: 
                                    <asp:TextBox runat="server" CssClass="txtTrongSo" ID="txtTrongSo" Text='<%#Eval("TrongSo") %>' />%
                            </span>
                            <h5 class="list-group-item-heading" runat="server" id="h5TenTC"><%#Eval("TenTC") %></h5>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Label runat="server" ID="lblLoiLuuTieuChi"></asp:Label>
            </div>

            <div class="col-sm-offset-5 col-sm-7">
                <asp:Button OnClick="btnThemTC_Click" runat="server" ID="Button2"
                    class="btn btn-info" Text="Lưu"></asp:Button>
                <asp:Button OnClick="btnQLTC_Click" runat="server" ID="Button3"
                    class="btn btn-info" Text="Quản lý Tiêu chí"></asp:Button>
            </div>

        </div>
    </div>
</asp:Content>

