using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditNhanVien : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DonVi dv = kho.TimDonVi(1);//test
        btnCreate.Enabled = !dv.HetTaiKhoan;
        if (!this.IsPostBack)
        {
            ILEN.View.Load2DDL(kho.LayDanhSachBoPhanTheoDonVi(1), ddlBP);
            ILEN.View.LoadTrangThai(rblTT);
            this.ddlBP_SelectedIndexChanged(null, null);
            ILEN.View.LoadGioiTinh(rblGT);
            this.LoadTTNV();
        }
    }

    private void LoadTTNV()
    {
        var nv = LayNVTheoQS();
        if (nv != default(NhanVien))
        {
            h2TenNV.InnerText = nv.HoTen;
            txtHoTen.Text = nv.HoTen;
            txtMaNV.Text = nv.MaNV;
            txtDT.Text = nv.DienThoai;
            txtEmail.Text = nv.Email;
            txtNgaySinh.Text = nv.NgaySinh == null ? string.Empty : ((DateTime)nv.NgaySinh).ToString("dd/MM/yyyy");
            ddlBP.SelectedValue = nv.ViTriCongViec.MaBP.ToString();
            ddlBP_SelectedIndexChanged(null, null);
            ddlVTCV.SelectedValue = nv.MaVTCV.ToString();
            imgNV.ImageUrl = @"~/images/NhanVien/" + nv.Hinh;
            rblTT.SelectedValue = nv.TrangThai ? "1" : "0";
        }
    }
    NhanVien LayNVTheoQS()
    {
        var maNV = MyUtility.LayQS("id");
        return kho.TimNhanVien(maNV);
    }

    protected void ddlBP_SelectedIndexChanged(object sender, EventArgs e)
    {
        int maBP = int.Parse(ddlBP.SelectedValue);
        ILEN.View.Load2DDL(kho.LayDanhSachViTriCongViecTheoBP(maBP), ddlVTCV);
    }

    //protected void btnSaveProfile_Click(object sender, EventArgs e)
    //{//cap nhat thong tin tai khoan admin
    //    string hoTen = string.Empty;
    //    if (!MyUtility.TextBoxHopLe(txtHoTen, out hoTen))
    //    {
    //        lblThongBaoProfile.Text = ThongBao.BatBuoc;
    //        return;
    //    }
    //    NhanVien nv = LayNVTheoQS();
    //    if (nv != default(NhanVien))
    //    {
    //        nv.HoTen = hoTen;
    //        nv.MaVTCV = int.Parse(ddlVTCV.SelectedValue);
    //        nv.GioiTinh = rblGT.SelectedValue == "1" ? true : false;
    //        nv.NgaySinh = // hix dang code 
    //    }
    //}
    //t dang lam trang nay em dung lam trang nay nua nhe. em lam trang nao fai fill vao schemale de moi nguoi biet.

    protected void btnSaveProfile_Click(object sender, EventArgs e)
    {//cap nhat thong tin tai khoan admin
        string hoTen = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtHoTen, out hoTen))
        {
            lblThongBaoProfile.Text = ThongBao.BatBuoc;
            return;
        }
        DateTime ns = new DateTime();
        if (!DateTime.TryParseExact(txtNgaySinh.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out ns))
        {
            lblThongBaoProfile.Text = ThongBao.NgayKhongDung;
            return;
        }
        NhanVien nv = LayNVTheoQS();
        if (nv != default(NhanVien))
        {
            nv.HoTen = hoTen;
            nv.MaVTCV = int.Parse(ddlVTCV.SelectedValue);
            nv.GioiTinh = rblGT.SelectedValue == "1" ? true : false;
            nv.NgaySinh = ns;
            nv.Email = txtEmail.Text;
            nv.TrangThai = rblTT.SelectedValue == "1" ? true : false;
        }
        //luu hinh nhan vien neu co:
        string fileHinhCu = nv.Hinh;
        if (fulProfile.HasFile)
        {
            string extension = string.Empty;
            if (!MyUtility.FileHinhHopLe(fulProfile, out extension))
            {
                lblThongBaoProfile.Text = ThongBao.HinhKhongHoLe;
                return;
            }

            if (MyUtility.LuuHinh(fulProfile, string.Format(@"~/images/NhanVien/{0}.{1}", nv.MaNV, extension)))
            {
                nv.Hinh = string.Format("{0}.{1}", nv.MaNV, extension);
                if (MyUtility.ChuoiHopLe(fileHinhCu) && !fileHinhCu.Equals(nv.Hinh))
                    //xoa hinh cu:
                    MyUtility.XoaHinh(string.Format(@"~/images/NhanVien/{0}", fileHinhCu));
            }
        }
        if (!kho.SuaNhanVien(nv) || !kho.Luu())
        {
            lblThongBaoProfile.Text = ThongBao.SuaKhongThanhCong;
            return;
        }
        this.LoadTTNV();
        lblThongBaoProfile.Text = ThongBao.ThanhCong;
    }

    protected void btnLuuPass_Click(object sender, EventArgs e)
    {//cap nhat lai mat khau:
        NhanVien nv = LayNVTheoQS();
        if (!MyUtility.MatKhauTrungKhop(txtPass.Text, txtRetype.Text))
        {
            lblTBPass.Text = ThongBao.MatKhauKhongTrung;
            return;
        };
        if (nv != default(NhanVien))
        {
            nv.MatKhau = txtPass.Text;
            if (!kho.SuaNhanVien(nv) || !kho.Luu())
            {
                lblThongBaoProfile.Text = ThongBao.SuaKhongThanhCong;
                return;
            }
            lblTBPass.Text = ThongBao.ThanhCong;
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ThemTKNV.aspx");
    }
}