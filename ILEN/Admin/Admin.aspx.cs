using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DonVi dv = kho.TimDonVi(1);//test
        btnCreate.Enabled = !dv.HetTaiKhoan;
        if (!this.IsPostBack)
        {
            var lst = kho.LayDanhSachNhanVienTheoDonVi(1);
            ILEN.View.Load2LV(lst, lvDSTK);
            this.KhoiTao();
        }
    }

    void KhoiTao()
    {
        DonVi dv = kho.TimDonVi(1);
        if (dv != default(DonVi))
        {
            spnSoTK.InnerText = this.SoTKHienTai + "/" + this.TongSoTK;
            //load thong tin admin:
            var ad = dv.AdminDV;
            lblHoTen.Text = ad.HoTen;
            lblMaNV.Text = ad.TenDN;
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ThemTKNV.aspx");
    }
}
