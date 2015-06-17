<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdCommon.master" AutoEventWireup="true" CodeFile="DanhGia.aspx.cs"
    EnableEventValidation="false"
    Inherits="Admin_DanhGia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <title>Thực hiện đánh giá</title>
    <style type="text/css">
        .thutdong {
            padding-left: 15px;
        }

        .thutdong1 {
            padding-left: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <h2 class="text-center" runat="server" id="h2TenNV"></h2>
    <p class="text-center">Thực Hiện Quá Trình Đánh Giá Nhân Sự</p>

    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Bộ chọn</div>
            <div class="panel-body">
                <div class="form-group">
                    <label class="control-lable col-sm-2" for="employeeid">Chọn nhân sự: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList
                            class="form-control"
                            runat="server" ID="ddlNhanSu">
                        </asp:DropDownList>
                    </div>
                    <label class="control-lable col-sm-2" for="employeeid">Chọn chu kỳ đánh giá: </label>
                    <div class="col-sm-10">
                        <asp:DropDownList
                            class="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlChuKyDG_SelectedIndexChanged"
                            runat="server" ID="ddlChuKyDG">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading" runat="server" id="divTenBM">Biểu mẫu đánh giá</div>
            <asp:GridView runat="server" ID="grv"></asp:GridView>
            <div class="panel-body">
                <div class="form-group">

                    <asp:ListView runat="server" ID="lvNTC">
                        <ItemTemplate>
                            <h5 class="list-group-item-heading">
                                <asp:Label runat="server" ID="lblTenNTC" Text='<%#MyUtility.TiengViet?Eval("TenNTC"):Eval("TenNTCEN") %>'></asp:Label>
                            </h5>
                            <asp:HiddenField runat="server" ID="hdfMaNTC" Value='<%#Eval("MaNTC") %>' />
                            <ul>
                                <asp:ListView runat="server" ID="lvTC">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdfMaTC" Value='<%#Eval("MaTC") %>' />
                                        <li>
                                            <asp:Label runat="server" ID="lblTenTC" Text='<%#MyUtility.TiengViet?Eval("TenTC"):Eval("TenTCEN") %>'></asp:Label>

                                            <asp:RadioButtonList runat="server" ID="rblMucTC" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator runat="server"
                                                ID="rfvMucTC"
                                                ErrorMessage="*"
                                                ForeColor="red"
                                                ControlToValidate="rblMucTC"></asp:RequiredFieldValidator>
                                        </li>
                                        <ul>
                                            <asp:ListView runat="server" ID="lvTCCon">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hdfMaTC" Value='<%#Eval("MaTC") %>' />
                                                    <li>
                                                        <asp:Label runat="server" ID="lblTenTCCon" Text='<%#MyUtility.TiengViet?Eval("TenTC"):Eval("TenTCEN") %>'></asp:Label>

                                                        <asp:RadioButtonList runat="server" ID="rblMucTC" RepeatDirection="Horizontal">
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator runat="server"
                                                            ID="rfvMucTC"
                                                            ErrorMessage="*"
                                                            ForeColor="red"
                                                            ControlToValidate="rblMucTC"></asp:RequiredFieldValidator>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>

    <asp:LinkButton runat="server" ID="lbtnLuu"
        OnClick="btnLuu_Click"
        CssClass="btn btn-info"><%=this.TiengViet?"Lưu kết quả":"Save" %></asp:LinkButton>
    <br />
    <asp:Label runat="server" ID="lblThongBao"></asp:Label>
</asp:Content>

