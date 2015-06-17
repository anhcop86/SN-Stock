<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master"
    EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="EditTieuChi.aspx.cs" Inherits="Admin_EditTieuChi" %>

<asp:Content ID="Content5" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        .an {
            display: none;
        }

        .test {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenBP">Quản lý tiêu chí</h2>
    <p class="text-center">Chỉnh sửa hay thêm mới tiêu chí</p>
    <!--chủ động đưa các thông tin cần thiết vào đây-->
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Chi Tiết Tiêu Chí</div>

            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="nhomtieuchi">Chọn Nhóm Tiêu chí: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList runat="server" class="form-control" ID="ddlNTC"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="tentieuchi">Tên Tiêu chí: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTenTC" placeholder="Nhập tên Tiêu chí" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="trongso">Trọng số: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtTrongSo" placeholder="Nhập Trọng số" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="mota">Mô tả: </label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" class="form-control" ID="txtMoTa" placeholder="Nhập mô tả" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:HiddenField runat="server" ID="hdfmaTC"
                        Value='0' />
                    <asp:Label runat="server" ID="lblKQ" class="control-lable col-sm-2"></asp:Label>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <br />
                        <asp:Button runat="server" ID="btnLuuTC" Text="Lưu" class="btn btn-info" OnClick="btnLuuTC_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnClearTC" Text="Nhập mới dữ liệu" class="btn btn-info"></asp:Button>
                        <asp:Button runat="server" ID="btnQuayLai" Text="Quay lại" OnClick="btnQuayLai_Click" class="btn btn-info"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="text-center">Câp độ đánh giá</h3>
        </div>
        <div class="panel-body">
            <asp:ListView runat="server" ID="lvDSCDTheoTC" class="list-group">
                <ItemTemplate>
                    <div class="list-group-item">
                        <span class="badge" runat="server" id="spnCapDo">
                            <div class="test">Cấp Độ: </div>
                            <asp:Panel runat="server" ID="pnlPro" CssClass="test" DefaultButton="btnSave">
                                <asp:TextBox runat="server" CssClass="txtTrongSo" ID="txtCapDo"
                                    Text='<%#Eval("GiaTri") %>' /><%#Eval("DonViTinh") %>
                                <asp:Button runat="server" CommandArgument='<%#Eval("MaCD") %>'
                                    OnClick="btnSave_Click"
                                    ID="btnSave" CssClass="an" />
                            </asp:Panel>
                        </span>
                        <asp:LinkButton class="badge" runat="server" ID="lbtnActive"
                            OnClick="lbtnActive_Click"
                            CommandArgument='<%#Eval("MaCD") %>'>
                            <%#(bool.Parse(Eval("Chon").ToString()))?"Active":"Passive" %>
                        </asp:LinkButton>
                        <h5 class="list-group-item-heading" runat="server" id="h5enTC"><%#Eval("Ten") %></h5>
                    </div>
                </ItemTemplate>
            </asp:ListView>

        </div>
    </div>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="text-center">Thêm Cấp độ Đánh giá vào Tiêu chí</h3>
        </div>
        <div class="panel-body">
            <div class="form-group" style="margin-top: 40px">
                <asp:ListView runat="server" ID="lvDSCDTC" class="list-group">
                    <ItemTemplate>
                        <div class="list-group-item">

                            <span style="float: right; margin-left: 10px">
                                <asp:HiddenField runat="server" ID="hdfMaCD" Value='<%#Eval("MaCD") %>' />
                                <asp:CheckBox runat="server"
                                    ID="chbCapDo" Checked="false"></asp:CheckBox>
                            </span>
                            <span class="badge" runat="server" id="spnTrongSo">Cấp độ: 
                                 <asp:TextBox runat="server" CssClass="txtTrongSo" ID="txtCapDo" Text='<%#Eval("GiaTri") %>' /><%#Eval("DonViTinh") %>
                            </span>
                            <h5 class="list-group-item-heading" runat="server" id="h5TenCD"><%#Eval("Ten") %></h5>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <asp:Label runat="server" ID="lblLoiLuu"></asp:Label>
            </div>

            <div class="col-sm-offset-5 col-sm-7">
                <asp:Button OnClick="btnLuuCDTTC_Click" runat="server" ID="btnLuuCDTheoTC"
                    class="btn btn-info" Text="Lưu"></asp:Button>
            </div>
        </div>
    </div>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="text-center">Nhập Cấp độ đánh giá của Tiêu chí</h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <label class="control-lable col-sm-2" for="tencapdo">Tên cấp độ đánh giá: </label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" class="form-control" ID="txtTenCapDo" placeholder="Nhập tên Cấp độ" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-lable col-sm-2" for="giatricapdo">Giá trị: </label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" class="form-control" ID="txtGiaTri" placeholder="Nhập Giá trị" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-lable col-sm-2" for="donvitinh">Đơn vị tính: </label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" class="form-control" ID="txtDonViTinh" Text="%" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-lable col-sm-2" for="mota">Mô tả: </label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" class="form-control" ID="txtMoTaCapDo" placeholder="Mô tả" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-lable col-sm-2" for="mota"></label>
                <div class="col-sm-10">
                    <asp:Label runat="server" ID="Labluucapdo" class="control-lable col-sm-2"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <br />
                    <asp:Button runat="server" ID="btnLuuCapDoTieuChi" Text="Lưu" class="btn btn-info" OnClick="btnLuuCDTC_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

