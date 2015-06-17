using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditVTCV : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.Load2DDL(this.DSBP, ddlBP);
            ILEN.View.LoadTrangThai(rblTT, !MyUtility.TiengViet);
            this.LoadVTCV();
        }
    }
    ViTriCongViec LayVTCVTheoQS()
    {
        int maVTCV = 0;
        int.TryParse(MyUtility.LayQS("id"), out maVTCV);
        return this.kho.TimViTriCongViec(maVTCV);
    }
    void LoadVTCV()
    {

        ViTriCongViec vt = LayVTCVTheoQS();
        if (vt != default(ViTriCongViec))
        {
            txtTenVT.Text = vt.TenVTCV;
            txtTenVTEN.Text = vt.TenVTCVEN;
            h2TenVTCV.InnerText = MyUtility.TiengViet ? vt.TenVTCV : vt.TenVTCVEN;
            ddlBP.SelectedValue = vt.MaBP.ToString();
            rblTT.SelectedValue = (bool)vt.TrangThai ? "1" : "0";
        }
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        ViTriCongViec vt = LayVTCVTheoQS();
        if (vt == default(ViTriCongViec))
        {//them moi
            vt = new ViTriCongViec()
            {
                TenVTCV = txtTenVT.Text,
                TenVTCVEN = txtTenVTEN.Text,
                MaBP = int.Parse(ddlBP.SelectedValue),
                TrangThai = rblTT.SelectedValue == "1" ? true : false
            };
            if (!kho.ThemViTriCongViec(vt) || !kho.Luu())
            {
                lblTB.Text = ThongBao.ThemKhongThanhCong;
            }
            else
            {
                lblTB.Text = ThongBao.ThanhCong;
            }
        }
        else
        {//cap nhat
            vt.TenVTCV = txtTenVT.Text;
            vt.TenVTCVEN = txtTenVTEN.Text;
            vt.MaBP = int.Parse(ddlBP.SelectedValue);
            vt.TrangThai = rblTT.SelectedValue == "1" ? true : false;
            if (!kho.SuaViTriCongViec(vt) || !kho.Luu())
            {
                lblTB.Text = ThongBao.SuaKhongThanhCong;
            }
            else
            {
                lblTB.Text = ThongBao.ThanhCong;
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ViTriCongViec vt = LayVTCVTheoQS();
        if (vt != default(ViTriCongViec))
        {
            if (!kho.XoaViTriCongViec(vt) || !kho.Luu())
            {
                lblTB.Text = ThongBao.XoaKhongThanhCong;
            }
            else
            {
                lblTB.Text = ThongBao.ThanhCong;
            }
        }
    }
}