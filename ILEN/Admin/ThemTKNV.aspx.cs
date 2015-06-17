using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ThemTKNV : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DonVi dv = kho.TimDonVi(1);//test
        btnCreate.Enabled = !dv.HetTaiKhoan;
        if (dv.HetTaiKhoan)
        {
            lblThongBao.Text = ThongBao.HetSoLuong;
            return;
        }
        if (!this.IsPostBack)
        {
            ILEN.View.LoadTrangThai(rblTT);
            ILEN.View.LoadGioiTinh(rblGT);
            ILEN.View.Load2DDL(kho.LayDanhSachBoPhanTheoDonVi(1), ddlBP);
            this.ddlBP_SelectedIndexChanged(null, null);
            ILEN.View.LoadGioiTinh(rblGT);
            ILEN.View.Load2DDL(kho.LayDanhSachVaiTro(1), ddlVaiTro);//maDV = 1 (demo)
            ILEN.View.Load2DDL(kho.LayDanhSachChuyenMon(1), ddlChuyenMon);//maDV = 1
            //sinh ma cho tu dong cho nhan vien:
            string maNV = kho.SinhMaNhanVien();
            txtMaNV.Text = maNV;
        }
    }
    private void LoadTTNV()
    {
        var nv = kho.TimNhanVien(txtMaNV.Text);
        if (nv != default(NhanVien))
        {
            h2TenNV.InnerText = nv.HoTen.ToProper();
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
    protected void ddlBP_SelectedIndexChanged(object sender, EventArgs e)
    {
        int maBP = int.Parse(ddlBP.SelectedValue);
        ILEN.View.Load2DDL(kho.LayDanhSachViTriCongViecTheoBP(maBP), ddlVTCV);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string maNV = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtMaNV, out maNV))
        {
            lblThongBao.Text = ThongBao.BatBuoc;
            return;
        }
        string hoTen = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtHoTen, out hoTen))
        {
            lblThongBao.Text = ThongBao.BatBuoc;
            return;
        }
        DateTime ns = new DateTime();
        if (!DateTime.TryParseExact(txtNgaySinh.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out ns))
        {
            lblThongBao.Text = ThongBao.NgayKhongDung;
            return;
        }
        if (!MyUtility.MatKhauTrungKhop(txtPass.Text, txtRetype.Text))
        {
            lblThongBao.Text = ThongBao.MatKhauKhongTrung;
            return;
        };
        //lay thong tin:
        NhanVien nv = new NhanVien()
        {
            MaNV = maNV,
            HoTen = hoTen.ToProper(),
            MaVTCV = int.Parse(ddlVTCV.SelectedValue),
            GioiTinh = rblGT.SelectedValue == "1" ? true : false,
            NgaySinh = ns,
            Email = txtEmail.Text,
            DienThoai = txtDT.Text,
            TrangThai = rblTT.SelectedValue == "1" ? true : false,
            MatKhau = txtPass.Text,
            MaVT = int.Parse(ddlVaiTro.SelectedValue),
            MaCM = int.Parse(ddlChuyenMon.SelectedValue)
        };
        //them nhan vien:
        //luu hinh nhan vien neu co:
        if (fulProfile.HasFile)
        {
            string extension = string.Empty;
            if (!MyUtility.FileHinhHopLe(fulProfile, out extension))
            {
                lblThongBao.Text = ThongBao.HinhKhongHoLe;
                return;
            }

            if (MyUtility.LuuHinh(fulProfile, string.Format(@"~/images/NhanVien/{0}.{1}", nv.MaNV, extension)))
            {
                nv.Hinh = string.Format("{0}.{1}", nv.MaNV, extension);
            }
        }
        if (!kho.ThemNhanVien(nv) || !kho.Luu())
        {
            lblThongBao.Text = ThongBao.ThemKhongThanhCong;
            return;
        }
        this.LoadTTNV();
        lblThongBao.Text = ThongBao.ThanhCong;
    }
}