using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EditBieuMau : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.Load2DDL(kho.DanhSachVTCVTheoDV(1), ddlVTCV);
            var paramId = MyUtility.LayQueryString("id");
            if (paramId != NULL)
            {
                int maBM = int.Parse(paramId);
                var bieuMau = kho.TimBieuMau(maBM);
                txtTenBM.Text = bieuMau.TenBM;
                hdfMaBM.Value = bieuMau.MaBM.ToString();

                ddlVTCV.SelectedValue = bieuMau.MaVTCV.ToString();

                //lay nhom tieu chi theo bieu mau.
                ILEN.View.Load2DDL(kho.TimNhomTieuChiTheoMaDV(1), ddlNTC, true);

                //load vao tieu chi vao list view.
                this.loadTieuChiToLV(maBM);

            }
        }
    }

    protected void loadTieuChiToLV(int maBM)
    {

        IList<TieuChi> dsTCTheoBM = kho.DanhSachTCTheoBM(maBM);
        IList<NhomTieuChi> dsNTCTheoBM = kho.LayNhomTCTheoMaBM(maBM);
        lvDSNTC.DataSource = dsNTCTheoBM;
        lvDSNTC.DataBind();
        foreach (ListViewDataItem item in lvDSNTC.Items)
        {
            ListView child = item.FindControl("lvDSTC") as ListView;
            HiddenField hdf = item.FindControl("hdfNTC") as HiddenField;
            int maNhomTC = int.Parse(hdf.Value);
            List<TieuChi> temp = new List<TieuChi>();
            foreach (var tieuChi in dsTCTheoBM)
            {
                if (tieuChi.MaNTC == maNhomTC)
                {
                    temp.Add(tieuChi);
                }
            }
            child.DataSource = temp;
            child.DataBind();
        }
    }

    protected void btnLuuBM_Click(object sender, EventArgs e)
    {

        string tenVN = string.Empty;
        string tenEN = string.Empty;
        if (!MyUtility.TextBoxHopLe(txtTenBM, out tenVN))
        {
            lblThongBao.Text = "Tên không được bỏ trống!";
            return;
        }
        BieuMauDanhGia bmdg = new BieuMauDanhGia()
        {
            //MaBM = 5,
            TenBM = tenVN,
            TenBMEN = tenEN,
            MaVTCV = int.Parse(ddlVTCV.SelectedValue),
            MaDV = 1
        };
        try
        {
            bmdg.MaBM = int.Parse(hdfMaBM.Value);
            if (!kho.luuBMDG(bmdg) || !kho.Luu())
            {
                lblThongBao.Text = "Lỗi thêm dữ liệu!";
                return;
            }
        }
        catch
        {
            if (!kho.luuBMDG(bmdg) || !kho.Luu())
            {
                lblThongBao.Text = "Lỗi thêm dữ liệu!";
                return;
            }
        }


        Response.Redirect("~/Admin/QLBieuMau.aspx");
    }

    protected void onSelectedNTC_Click(object sender, EventArgs e)
    {
        this.loadNTC();
    }

    protected void loadNTC()
    {
        var maNTC = int.Parse(ddlNTC.SelectedValue);
        int maBM = int.Parse(hdfMaBM.Value);
        IList<TieuChi> dsTCTheoBM = kho.LayTCTheoBMVaNTC(maBM, maNTC);
        IList<TieuChi> dsTCTheoNTC = kho.DanhSachTCTheoNTC(maNTC);
        List<TieuChi> dsTCThem = new List<TieuChi>();
        foreach (var tieuChi in dsTCTheoNTC)
        {
            if (!dsTCTheoBM.Contains(tieuChi))
            {
                dsTCThem.Add(tieuChi);
            }
        }
        lvDSTCTheoNTC.DataSource = dsTCThem;
        lvDSTCTheoNTC.DataBind();
    }

    protected void btnThemTC_Click(object sender, EventArgs e)
    {
        try
        {
            int maBM = int.Parse(hdfMaBM.Value);
            List<TieuChiTheoBieuMau> lsTieuChiTheoBM = new List<TieuChiTheoBieuMau>();
            foreach (ListViewDataItem i in lvDSTCTheoNTC.Items)
            {
                HiddenField hdf = i.FindControl("hdfMaTC") as HiddenField;
                CheckBox chk = i.FindControl("chbTieuChi") as CheckBox;
                TextBox trongSo = i.FindControl("txtTrongSo") as TextBox;
                if (chk.Checked)
                {
                    TieuChiTheoBieuMau tieuChiTheoBM = new TieuChiTheoBieuMau();
                    int maTC = int.Parse(hdf.Value);
                    tieuChiTheoBM.MaTC = maTC;
                    tieuChiTheoBM.MaBM = maBM;
                    tieuChiTheoBM.TrongSo = float.Parse(trongSo.Text);
                    lsTieuChiTheoBM.Add(tieuChiTheoBM);
                }
            }
            kho.ThemDCTCTheoBM(lsTieuChiTheoBM);
            kho.Luu();
            this.loadTieuChiToLV(maBM);
            this.loadNTC();
        }
        catch
        {
            lblLoiLuuTieuChi.Text = "Lỗi lưu Tiêu chí";
        }
    }
    protected void btnQLTC_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/QLTieuChi.aspx");

    }

    protected void lbtnEditTTTC_Click(object sender, EventArgs e)
    {
        var maTC = (sender as LinkButton).CommandArgument;
        var kq = kho.TimTieuChi(int.Parse(maTC));
        if (kq != default(TieuChi))
        {
            kq.Chon = !kq.Chon;
            kho.Luu();
        }
        var maBM = hdfMaBM.Value;
        Response.Redirect(@"~/Admin/EditBieuMau.aspx?id=" + maBM);
    }
    protected void lbtnEditTTNTC_Click(object sender, EventArgs e)
    {
        var maNTC = (sender as LinkButton).CommandArgument;
        var kq = kho.TimNhomTieuChi(int.Parse(maNTC));
        if (kq != default(NhomTieuChi))
        {
            IList<TieuChi> dsTieuChiTheoNTC = kho.DanhSachTCTheoNTC(int.Parse(maNTC));
            foreach (var i in dsTieuChiTheoNTC)
            {
                i.Chon = !kq.Chon;
                kho.Luu();
            }
            kq.Chon = !kq.Chon;
            kho.Luu();
        }
        var maBM = hdfMaBM.Value;
        Response.Redirect(@"~/Admin/EditBieuMau.aspx?id=" + maBM);
    }

    protected void editTrongSo(object sender, EventArgs e)
    {
        var trongSo = (sender as TextBox).Text;

        Response.Redirect(@"~/Admin/QLBieuMau.aspx?id=");
    }

    public string NULL { get; set; }
    protected void lvDSNTC_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        int maTC = int.Parse(btn.CommandArgument);
        int maBM = int.Parse(hdfMaBM.Value);
        var pnl = btn.Parent;
        TextBox txtTS = pnl.FindControl("txtTrongSoTC") as TextBox;
        float ts = 0;
        if (!float.TryParse(txtTS.Text, out ts))
        {
            txtTS.Text = "0";
            return;
        }
        var kq = kho.TimTCTBM(maBM, maTC);
        if (kq != default(TieuChiTheoBieuMau))
        {
            kq.TrongSo = ts;
            kho.Luu();
        }
    }
}