using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditChuKyDG : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.KhoiTao();
            this.LoadTTCKDG();

        }
    }
    ChuKyDanhGia LayCKDGTheoQS()
    {
        int maCK = 0;
        int.TryParse(MyUtility.LayQS("id"), out maCK);
        return kho.TimChuKyDanhGia(maCK);
    }
    void KhoiTao()
    {
        
    }
    private void LoadTTCKDG()
    {
        var ck = LayCKDGTheoQS();
        if (ck != default(ChuKyDanhGia))
        {
            h2TenCK.InnerText = TiengViet ? ck.TenCK : ck.TenCKEN;
            rblTT.SelectedValue = ck.TrangThai.ToString();
            txtTenCK.Text = ck.TenCK;
            txtTenCKEN.Text = ck.TenCKEN;
            txtNgayBD.Text = ck.BatDau.ToString("dd/MM/yyyy");
            txtNgayKT.Text = ck.KetThuc.ToString("dd/MM/yyyy");
            
        }
        else
        {
            txtNgayBD.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtNgayKT.Text = DateTime.Today.AddMonths(1).ToString("dd/MM/yyyy");
        }
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        var ck = LayCKDGTheoQS();
        if (ck != default(ChuKyDanhGia))
        {//cap nhat
            string tenCK = string.Empty;
            if (!MyUtility.TextBoxHopLe(txtTenCK, out tenCK))
            {
                lblTB.Text = ThongBao.BatBuoc;
                return;
            }
            DateTime bd = new DateTime();
            if (!MyUtility.NgayHopLe(txtNgayBD.Text, out bd))
            {
                lblTB.Text = ThongBao.NgayKhongDung;
                return;
            }
            DateTime kt = new DateTime();
            if (!MyUtility.NgayHopLe(txtNgayKT.Text, out kt))
            {
                lblTB.Text = ThongBao.NgayKhongDung;
                return;
            }
            ck = LayCKDGTheoQS();
            if (ck != default(ChuKyDanhGia))
            {
                ck.TenCK = tenCK;
                ck.TenCKEN = txtTenCKEN.Text;
                ck.BatDau = bd;
                ck.KetThuc = kt;
                ck.TrangThai = int.Parse(rblTT.SelectedValue);
                ck.MaDV = this.MaDV;

                if (!kho.SuaChuKyDanhGia(ck) || !kho.Luu())
                {
                    lblTB.Text = ThongBao.SuaKhongThanhCong;
                    return;
                }
                lblTB.Text = ThongBao.ThanhCong;
                this.LoadTTCKDG();
            }
        }
        else
        {//them moi
            string tenCK = string.Empty;
            if (!MyUtility.TextBoxHopLe(txtTenCK, out tenCK))
            {
                lblTB.Text = ThongBao.BatBuoc;
                return;
            }
            DateTime bd = new DateTime();
            if (!MyUtility.NgayHopLe(txtNgayBD.Text, out bd))
            {
                lblTB.Text = ThongBao.NgayKhongDung;
                return;
            }
            DateTime kt = new DateTime();
            if (!MyUtility.NgayHopLe(txtNgayKT.Text, out kt))
            {
                lblTB.Text = ThongBao.NgayKhongDung;
                return;
            }
            ck = new ChuKyDanhGia()
            {
                TenCK = tenCK,
                TenCKEN = txtTenCKEN.Text,
                BatDau = bd,
                KetThuc = kt,
                TrangThai = int.Parse(rblTT.SelectedValue),
                MaDV = this.MaDV,
            };
            if (!kho.ThemChuKyDanhGia(ck) || !kho.Luu())
            {
                lblTB.Text = ThongBao.ThemKhongThanhCong;
                return;
            }
            lblTB.Text = ThongBao.ThanhCong;
            this.LoadTTCKDG();
        }
    }
    protected void btnXoaCK_Click(object sender, EventArgs e)
    {
        var ck = LayCKDGTheoQS();
        if (ck != default(ChuKyDanhGia))
        {
            if (ck.TrangThai > 0)//khong cho xoa
            {
                lblTB.Text = ThongBao.DangSuDung;
                return;
            }
            //xoa:
            if (!kho.XoaChuKyDanhGia(ck) || !kho.Luu())
            {
                lblTB.Text = ThongBao.XoaKhongThanhCong;
                return;
            }
            Response.Redirect("~/Admin/QLChuKyDG.aspx");
        }
    }
    protected void ddlBM_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BieuMauDanhGia bm = kho.TimBieuMau(int.Parse(ddlBM.SelectedValue));
        ////ten cua chu ky danh gia se duoc recommended = ten bieu mau + ten vi tri cong viec
        //txtTenCK.Text = bm.TenBM + " - " + bm.ViTriCongViec.TenVTCV;
        //txtTenCKEN.Text = bm.TenBMEN + " - " + bm.ViTriCongViec.TenVTCVEN;
    }
}