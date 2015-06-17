using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditTieuChi : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            var paramId = MyUtility.LayQueryString("id");
            if (paramId != null)
            {
                int maTC = int.Parse(paramId);
                TieuChi tieuChi = kho.TimTieuChi(maTC);
                hdfmaTC.Value = maTC.ToString();
                txtTenTC.Text = tieuChi.TenTC;
                txtTrongSo.Text = tieuChi.TrongSo.ToString();
                txtMoTa.Text = tieuChi.MoTa;
                ddlNTC.SelectedValue = tieuChi.MaNTC.ToString();

                this.loadCapDo2LV(maTC);

                ILEN.View.Load2LV(kho.TimCapDoTheoTC(maTC), lvDSCDTheoTC);
            }
            ILEN.View.Load2DDL(kho.TimNhomTieuChiTheoMaDV(1), ddlNTC, true);
        }
    }
    protected void loadCapDo2LV(int maTC)
    {
        IList<CapDoTieuChi> capDoTheoTieuChi = kho.TimCapDoTheoTC(maTC);
        IList<CapDoTieuChi> capDoTieuChi = kho.DsCapDoTieuChiTheoMaDV(1);
        List<CapDoTieuChi> cDTCThem = new List<CapDoTieuChi>();

        //foreach (var i in capDoTieuChi)
        //{
        //    bool flag = true;
        //    foreach(var j in capDoTheoTieuChi)
        //    {
        //        if(!i.MaCD.Equals(j.MaCD))
        //        {
        //            cDTCThem.Add(i);
        //        }
        //    }

        //}
        lvDSCDTC.DataSource = capDoTieuChi;
        lvDSCDTC.DataBind();
    }
    protected void btnLuuTC_Click(object sender, EventArgs e)
    {
        string tenVN = string.Empty;
        string tenEN = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtTenTC, out tenVN))
        {
            lblKQ.Text = "Tên không được bỏ trống!";
            return;
        }
        double ts = 0;
        double.TryParse(txtTrongSo.Text, out ts);
        if (ts <= 0)
        {
            lblKQ.Text = "Trọng số không hợp lệ!";
            return;
        }
        var maNTC = int.Parse(ddlNTC.SelectedValue);
        if (maNTC == 0)
        {
            lblKQ.Text = "Nhóm tiêu chí không hợp lệ!";
            return;
        }
        var maTC = int.Parse(hdfmaTC.Value);
        if (maTC == 0)
        {
            TieuChi tc = new TieuChi()
            {
                MaNTC = maNTC,
                TenTC = tenVN,
                TenTCEN = tenEN,
                TrongSo = ts,
                MoTa = txtMoTa.Text
            };
            if (!kho.ThemTieuChi(tc))
            {
                lblKQ.Text = "Lỗi lưu dữ liệu11!";
                return;
            }
        }
        else
        {
            TieuChi tc = new TieuChi()
            {
                MaTC = maTC,
                MaNTC = maNTC,
                TenTC = tenVN,
                TenTCEN = tenEN,
                TrongSo = ts,
                MoTa = txtMoTa.Text
            };
            if (!kho.SuaTieuChi(tc)) { 
                lblKQ.Text = "Lỗi lưu dữ liệu22!";
                return;
            }
        }
        if (!kho.Luu())
        {
            lblKQ.Text = "Lỗi lưu dữ liệu!";
            return;
        }
        //reload:
        Response.Redirect("~/Admin/QLTieuChi.aspx");
    }
    protected void btnLuuCDTTC_Click(object sender, EventArgs e)
    {
        try
        {
            int maTC = int.Parse(hdfmaTC.Value);
            List<CapDoTheoTieuChi> lsCapDoTheoTieuChi = new List<CapDoTheoTieuChi>();
            foreach (ListViewDataItem i in lvDSCDTC.Items)
            {
                HiddenField hdf = i.FindControl("hdfMaCD") as HiddenField;
                CheckBox chk = i.FindControl("chbCapDo") as CheckBox;
                TextBox capDo = i.FindControl("txtCapDo") as TextBox;
                if (chk.Checked)
                {
                    CapDoTheoTieuChi capDoTheoTieuChi = new CapDoTheoTieuChi();
                    capDoTheoTieuChi.MaCD = int.Parse(hdf.Value);
                    capDoTheoTieuChi.MaTC = maTC;
                    capDoTheoTieuChi.GiaTri = int.Parse(capDo.Text);
                    lsCapDoTheoTieuChi.Add(capDoTheoTieuChi);
                }
            }
            kho.ThemDSCDTheoTC(lsCapDoTheoTieuChi);
            kho.Luu();
            this.loadCapDo2LV(maTC);
            ILEN.View.Load2LV(kho.TimCapDoTheoTC(maTC), lvDSCDTheoTC);
        }
        catch
        {
            lblLoiLuu.Text = "Lỗi lưu Tiêu chí";
        }
    }
    protected void btnLuuCDTC_Click(object sender, EventArgs e)
    {
        string tenVN = string.Empty;
        int gt = 0;
        int.TryParse(txtGiaTri.Text, out gt);
        if (!MyUtility.TextBoxHopLe(txtTenCapDo, out tenVN))
        {
            Labluucapdo.Text = "Tên không được bỏ trống!";
            return;
        }
        else if (gt <= 0)
        {
            Labluucapdo.Text = "Giá trị không hợp lệ!";
            return;
        }
        else 
        {
            Labluucapdo.Text = "";
        }
        CapDoTieuChi capDoTieuChi = new CapDoTieuChi
        {
            Ten = tenVN,
            GiaTri = gt,
            DonViTinh = txtDonViTinh.Text,
            MoTa = txtMoTaCapDo.Text,
            MaDV = 1
        };
        kho.ThemCapDo(capDoTieuChi);
        kho.Luu();
        int maTC = int.Parse(hdfmaTC.Value);
        this.loadCapDo2LV(maTC);
    }
    protected void btnQuayLai_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/QLTieuChi.aspx");
    }

    protected void btnQLTC_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/QLTieuChi.aspx");

    }

    protected void lvDSNTC_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int maTC = int.Parse(hdfmaTC.Value);
        Button btn = sender as Button;
        int maCD = int.Parse(btn.CommandArgument);
        var pnl = btn.Parent;
        TextBox txtCapDo = pnl.FindControl("txtCapDo") as TextBox;
        int cd = 0;
        if (!int.TryParse(txtCapDo.Text, out cd))
        {
            txtCapDo.Text = "0";
            return;
        }
        var kq = kho.TimCapDoTheoTieuChi1(maTC, maCD);
        if (kq != default(CapDoTheoTieuChi))
        {
            kq.GiaTri = cd;
            kho.Luu();
        }
        
    }
    protected void lbtnActive_Click(object sender, EventArgs e)
    {
        int maTC = int.Parse(hdfmaTC.Value);
        int maCD = int.Parse((sender as LinkButton).CommandArgument);
        var kq = kho.TimCapDoTheoTieuChi1(maTC, maCD);
        if (kq != default(CapDoTheoTieuChi))
        {
            kq.Chon = !kq.Chon;
            kho.Luu();
        }
        Response.Redirect(@"~/Admin/EditTieuChi.aspx?id=" + maTC);
    }
}