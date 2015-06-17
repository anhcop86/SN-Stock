using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QLNTC : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.LoadTo(kho.DanhSachNhomTieuChi.Where(n => n.MaDV.Equals(1)).ToList(), lvDSNTC);
            
            foreach (ListViewDataItem item in lvDSNTC.Items)
            {
                ListView child = item.FindControl("lvDSTC") as ListView;
                HiddenField hdf = item.FindControl("hdfNTC") as HiddenField;
                int maNhomTC = int.Parse(hdf.Value);
                child.DataSource = kho.DanhSachTieuChi
                    .Where(t => t.MaNTC.Equals(maNhomTC)
                    && t.MaTCCha == null).ToList();
                child.DataBind();
               
            }
            
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Admin/EditTieuChi.aspx");

    }
    protected void btnCreateNTC_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Admin/EditNhomTC.aspx");
    }
    protected void lbtnEditNTC_Click(object sender, EventArgs e)
    {
        var maNTC = (sender as LinkButton).CommandArgument;
        Response.Redirect(@"~/Admin/EditNhomTC.aspx?id="+maNTC);
    }
    protected void lbtnEditTC_Click(object sender, EventArgs e)
    {
        var maTC = (sender as LinkButton).CommandArgument;
        Response.Redirect(@"~/Admin/EditTieuChi.aspx?id=" + maTC);
    }
    protected void lbtnEditTTTC_Click(object sender, EventArgs e)
    {
        var maTC = (sender as LinkButton).CommandArgument;
        var kq = kho.TimTieuChi(int.Parse(maTC));
        if (kq != default(TieuChi)) {
            kq.Chon = !kq.Chon;
            kho.Luu();
        }
        Response.Redirect(@"~/Admin/QLTieuChi.aspx");
    }
    protected void lbtnEditTTNTC_Click(object sender, EventArgs e)
    {
        var maNTC = (sender as LinkButton).CommandArgument;
        var kq = kho.TimNhomTieuChi(int.Parse(maNTC));
        if (kq != default(NhomTieuChi))
        {
            IList<TieuChi> dsTieuChiTheoNTC = kho.DanhSachTCTheoNTC(int.Parse(maNTC));
            foreach (var i in dsTieuChiTheoNTC) {
                i.Chon = !kq.Chon;
                kho.Luu();
            }
            kq.Chon = !kq.Chon;
            kho.Luu();
        }
        Response.Redirect(@"~/Admin/QLTieuChi.aspx");
    }
}