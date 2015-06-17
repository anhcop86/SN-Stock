using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperAdmin_EditDonVi : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["dv"] != null)
            {
                var maDV = int.Parse(Request.QueryString["dv"]);
                CheDo(true);
                this.LoadTTDV(maDV);
            }
            else
            {
                CheDo(false);
            }
        }
    }

    private void CheDo(bool capNhat)
    {
        btnCapNhat.Enabled = capNhat;
        btnSave.Enabled = !capNhat;
    }
    void LoadTTDV(int maDV)
    {
        DonVi dv = this.kho.TimDonVi(maDV);
        if (dv != default(DonVi))
        {
            //header:
            lblMaDV.Text = dv.MaDV.ToString();
            lblDiaChiDV.Text = dv.DiaChi;
            lblFaxDV.Text = dv.Fax;
            lblEmailDV.Text = dv.Email;
            lblDienThoaiDV.Text = dv.DienThoai;
            lblTenDV.Text = dv.TenDV;
            lblWebsite.Text = dv.Website;
            //thong tin don vi:
            txtTenDV.Text = dv.TenDV;
            txtWebsite.Text = dv.Website;
            txtFax.Text = dv.Fax;
            txtEmail.Text = dv.Email;
            txtDC.Text = dv.DiaChi;
            txtDT.Text = dv.DienThoai;
            txtWebsite.Text = dv.Website;
            if (MyUtility.ChuoiHopLe(dv.Logo))
                imgLogo.ImageUrl = string.Format(@"~/images/LogoDV/{0}", dv.Logo);
            //load thong tin admin don vi:
            if (dv.AdminDV != null)
            {
                lblHoTenAdmin.Text = dv.AdminDV.HoTen;
                lblEmailAdmin.Text = dv.AdminDV.Email;
                lblTenDN.Text = dv.AdminDV.TenDN;
                lblDienThoaiAdmin.Text = dv.AdminDV.DienThoai;
                lblGioiTinh.Text = (bool)dv.AdminDV.GioiTinh ? "Nam" : "Nữ";
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!MyUtility.ChuoiHopLe(txtTenDV.Text))
        {
            lblKQDV.Text = ThongBao.BatBuoc;
            return;
        }

        DonVi dv = new DonVi()
        {
            TenDV = txtTenDV.Text,
            TenDVEN = string.Empty,
            DienThoai = txtDT.Text,
            DiaChi = txtDC.Text,
            Email = txtEmail.Text,
            Fax = txtFax.Text,
            TrangThai = true,
            Logo = string.Empty,
            Website = txtWebsite.Text
        };
        //neu co logo:
        if (fulLogo.HasFile)
        {
            string exten;
            if (!MyUtility.FileHinhHopLe(fulLogo, out exten))
            {
                lblKQDV.Text = ThongBao.HinhKhongHoLe;
                return;
            }
            var virPath = @"~/images/LogoDV/" + dv.MaDV + "." + exten;
            if (MyUtility.LuuHinh(fulLogo, virPath))
                dv.Logo = dv.MaDV + "." + exten;
        }
        if (!kho.ThemDonVi(dv))
        {
            lblKQDV.Text = ThongBao.ThemKhongThanhCong;
            return;
        }
        if (!kho.Luu())
        {
            lblKQDV.Text = ThongBao.ThemKhongThanhCong;
            return;
        }
        else
        {
            //neu co logo:
            if (fulLogo.HasFile)
            {
                string exten;
                if (!MyUtility.FileHinhHopLe(fulLogo, out exten))
                {
                    lblKQDV.Text = ThongBao.HinhKhongHoLe;
                    return;
                }
                var virPath = @"~/images/LogoDV/" + dv.MaDV + "." + exten;
                if (MyUtility.LuuHinh(fulLogo, virPath))
                {
                    dv.Logo = dv.MaDV + "." + exten;
                    if (!kho.Luu())
                    {
                        lblKQDV.Text = ThongBao.ThemKhongThanhCong;
                        return;
                    }
                }
            }
            //reload:
            Response.Redirect("~/SuperAdmin/QLDonVi.aspx");
        }
    }

    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (!MyUtility.ChuoiHopLe(txtTenDV.Text))
        {
            lblKQDV.Text = ThongBao.BatBuoc;
            return;
        }
        if (Request.QueryString["dv"] != null)
        {//cap nhat:
            int maDV = int.Parse(Request.QueryString["dv"]);
            DonVi dv = kho.TimDonVi(maDV);
            if (dv == default(DonVi)) return;

            dv.TenDV = txtTenDV.Text;
            dv.TenDVEN = string.Empty;
            dv.DienThoai = txtDT.Text;
            dv.DiaChi = txtDC.Text;
            dv.Email = txtEmail.Text;
            dv.Fax = txtFax.Text;
            dv.TrangThai = true;
            dv.Logo = string.Empty;
            dv.Website = txtWebsite.Text;

            //neu co logo:
            if (fulLogo.HasFile)
            {
                string exten;
                if (!MyUtility.FileHinhHopLe(fulLogo, out exten))
                {
                    lblKQDV.Text = ThongBao.HinhKhongHoLe;
                    return;
                }
                var virPath = @"~/images/LogoDV/" + dv.MaDV + "." + exten;
                if (MyUtility.LuuHinh(fulLogo, virPath))
                    dv.Logo = dv.MaDV + "." + exten;
            }

            if (!kho.SuaDonVi(dv) || !kho.Luu())
            {
                lblKQDV.Text = ThongBao.SuaKhongThanhCong;
                return;
            }
            Response.Redirect("~/SuperAdmin/QLDonVi.aspx");
        }
    }
    protected void btnSaveAdmin_Click(object sender, EventArgs e)
    {

    }
    protected void btnCapNhatAdmin_Click(object sender, EventArgs e)
    {

    }
    protected void btnQLAdminDV_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["dv"] != null)
        {
            var maDV = int.Parse(Request.QueryString["dv"]);
            Response.Redirect("~/SuperAdmin/QLAdminDV.aspx?dv=" + maDV);
        }
        else
            Response.Redirect("~/SuperAdmin/QLAdminDV.aspx");
    }
}