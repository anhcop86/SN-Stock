using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditNhomTC : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var paramId = MyUtility.LayQueryString("id");
        if (paramId != null)
        {
            int maNTC = int.Parse(paramId);
            var nhomTieuChi = kho.TimNhomTieuChi(maNTC);
            txtTenNTC.Text = nhomTieuChi.TenNTC;
        }
    }
    protected void btnLuuNTC_Click(object sender, EventArgs e)
    {
        string tenVN = string.Empty;
        int ts = 0;
        int.TryParse(txtTrongSo.Text, out ts);
        if (!MyUtility.TextBoxHopLe(txtTenNTC, out tenVN))
        {
            lblLoiLuu.Text = "Tên không được bỏ trống!";
            return;
        }
        else if (ts <= 0)
        {
            lblLoiLuu.Text = "Trọng số không hợp lệ!";
            return;
        }
        else
        {
            lblLoiLuu.Text = "";
        }
        NhomTieuChi nhomTC = new NhomTieuChi
        {
            MaDV = 1,
            TenNTC = tenVN,
            TrongSo = ts,
            MoTa = txtMoTa.Text
        };
        kho.ThemNhomTieuChi(nhomTC);
        kho.Luu();
        Response.Redirect(@"~/Admin/QLTieuChi.aspx?id=");
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 

}