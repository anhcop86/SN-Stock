using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperAdmin_QLAdminDV : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.KhoiTao();
            this.LoadTTAdmin();
            this.LoadTTDV();
            MyUtility.XoaThongBao(divParent);
        }
    }

    void LoadTTDV()
    {
        DonVi dv = LayDonViTuQS();
        if (dv != default(DonVi))
        {
            rblTT.SelectedValue = (bool)dv.TrangThai ? "1" : "0";
            txtSoLuongAccount.Text = dv.TongSoTK.ToString();
            lblDonVi.Text = Session["en"] == null ? dv.TenDV : dv.TenDVEN;
        }
    }
    void LoadTTAdmin()
    {
        if (Request.QueryString["dv"] != null)
        {
            var maDV = 0;
            int.TryParse(Request.QueryString["dv"], out maDV);
            AdminDV ad = kho.TimAdminDV(maDV);
            if (ad != default(AdminDV))
            {
                txtAdDienThoai.Text = ad.DienThoai;
                txtAdEmail.Text = ad.Email;
                txtTenAdmin.Text = ad.HoTen;
                txtTenDN.Text = ad.TenDN;
                rblGT.SelectedValue = (bool)ad.GioiTinh ? "1" : "0";
            }
        }
    }
    private DonVi LayDonViTuQS()
    {
        if (Request.QueryString["dv"] != null)
        {
            var maDV = Request.QueryString["dv"];
            return kho.TimDonVi(int.Parse(maDV));
        }
        return null;
    }

    private void KhoiTao()
    {
        ILEN.View.LoadTrangThai(rblTT);
        ILEN.View.LoadGioiTinh(rblGT);

        divProfile.InnerText = "Profile";
    }

    protected void btnCapNhatAdmin_Click(object sender, EventArgs e)
    {
        //validation:
        string hoTen = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtTenAdmin, out hoTen))
        {
            lblKQAdmin.Text = ThongBao.BatBuoc;
            return;
        }
        string tenDN = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtTenDN, out tenDN))
        {
            lblKQAdmin.Text = ThongBao.BatBuoc;
            return;
        }
        DonVi dv = LayDonViTuQS();

        if (dv != null)
        {
            AdminDV ad = new AdminDV()
            {
                MaDV = dv.MaDV,
                HoTen = hoTen,
                TenDN = tenDN,
                GioiTinh = rblGT.SelectedValue == "1" ? true : false,
                Email = txtAdEmail.Text,
                DienThoai = txtAdDienThoai.Text,
                MatKhauMD = "123"
            };

            if (dv.AdminDV == null)
            {//them moi:
                dv.AdminDV = ad;
                if (!kho.ThemAdminDV(ad) || !kho.Luu())
                {
                    lblKQAdmin.Text = ThongBao.ThemKhongThanhCong;
                    return;
                }
            }
            else
            {//cap nhat:
                if (!kho.SuaAdminDV(ad) || !kho.Luu())
                {
                    lblKQAdmin.Text = ThongBao.SuaKhongThanhCong;
                    return;
                }
            }
            lblKQAdmin.Text = ThongBao.ThanhCong;

        }
    }
    protected void btnUpdateAcc_Click(object sender, EventArgs e)
    {
        DonVi dv = LayDonViTuQS();
        if (dv != default(DonVi) && dv.AdminDV != default(AdminDV))
        {
            dv.AdminDV.TrangThai = rblTT.SelectedValue == "1" ? true : false;
            if (!kho.Luu())
            {
                lblKQStatus.Text = ThongBao.SuaKhongThanhCong;
            }
            else
            {
                lblKQStatus.Text = ThongBao.ThanhCong;
            }
        }
    }
    protected void btnUpdatePass_Click(object sender, EventArgs e)
    {
        //cap nhat mat khau:
        DonVi dv = this.LayDonViTuQS();
        if (dv != default(DonVi) && dv.AdminDV != default(AdminDV))
        {
            if (!txtPass.Text.Equals(txtRePass.Text))
            {
                lblMatKhau.Text = ThongBao.MatKhauKhongTrung;
                return;
            };
            dv.AdminDV.MatKhauMD = txtPass.Text;
            if (!kho.Luu())
            {
                lblMatKhau.Text = ThongBao.SuaKhongThanhCong;
            }
            else
            {
                lblMatKhau.Text = ThongBao.ThanhCong;
            }
        }
    }

    protected void btnXoaAdmin_Click(object sender, EventArgs e)
    {
        //xoa tai khoan admin:
        DonVi dv = this.LayDonViTuQS();
        if (dv != default(DonVi) && dv.AdminDV != default(AdminDV))
        {
            if (!kho.XoaAdminDV(dv.AdminDV) || !kho.Luu())
            {
                lblXoaAdmin.Text = ThongBao.XoaKhongThanhCong;
            }
            else
            {
                lblXoaAdmin.Text = ThongBao.ThanhCong;
            }
        }
    }
}