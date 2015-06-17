using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditBoPhan : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.LoadTrangThai(rblTrangThai, !MyUtility.TiengViet);
            this.LoadTTBP();
        }
    }
    BoPhan LayBoPhanTheoQS()
    {
        if (MyUtility.LayQS("id") != null)
        {
            var ma = int.Parse(MyUtility.LayQS("id"));
            return kho.TimBoPhan(ma);
        }
        return default(BoPhan);
    }
    void LoadTTBP()
    {
        BoPhan bp = LayBoPhanTheoQS();
        if (bp != default(BoPhan))
        {
            txtTenBP.Text = bp.TenBP;
            txtTenBPEN.Text = bp.TenBPEN;
            h2TenBP.InnerText = txtTenBP.Text;
            rblTrangThai.SelectedValue = bp.TrangThai ? "1" : "0";
        }
    }
    protected void btnLuuBP_Click(object sender, EventArgs e)
    {
        string tenBP = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtTenBP, out tenBP))
        {
            lblThongBao.Text = ThongBao.BatBuoc;
            return;
        }
        BoPhan bp = LayBoPhanTheoQS();
        if (bp == default(BoPhan))
        {//them moi
            bp = new BoPhan()
            {
                MaDV = this.MaDV,
                TenBP = txtTenBP.Text,
                TenBPEN = txtTenBPEN.Text,
                TrangThai = rblTrangThai.SelectedValue == "1" ? true : false
            };
            if (!kho.ThemBoPhan(bp) || !kho.Luu())
            {
                lblThongBao.Text = ThongBao.ThemKhongThanhCong;
                return;
            }
            lblThongBao.Text = ThongBao.ThanhCong;
            this.LoadTTBP();
        }
        else
        {//cap nhat
            bp.MaDV = this.MaDV;
            bp.TenBP = txtTenBP.Text;
            bp.TenBPEN = txtTenBPEN.Text;
            bp.TrangThai = rblTrangThai.SelectedValue == "1" ? true : false;
            if (!kho.SuaBoPhan(bp) || !kho.Luu())
            {
                lblThongBao.Text = ThongBao.SuaKhongThanhCong;
                return;
            }
            lblThongBao.Text = ThongBao.ThanhCong;
            this.LoadTTBP();
        }
    }
    protected void btnXoaTT_Click(object sender, EventArgs e)
    {
        BoPhan bp = LayBoPhanTheoQS();
        if (bp != default(BoPhan))
        {
            if (!kho.XoaBoPhan(bp) || !kho.Luu())
            {
                lblThongBao.Text = ThongBao.XoaKhongThanhCong;
                return;
            }
            lblThongBao.Text = ThongBao.ThanhCong;
            this.LoadTTBP();
        }
    }
}