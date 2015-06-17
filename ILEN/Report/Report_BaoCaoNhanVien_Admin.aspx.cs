using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Report_Report_BaoCaoNhanVien_Admin : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoadListVaiTro();
            LoadListBoPhan();
            ReportRender(-1,-1); // -1 là tất cả cho dễ nhớ
            
        }
    }

    protected void ReportRender(int vaitroid, int bophanId)
    {
        ReportDataSource ds = new ReportDataSource()
        {
            Name = "ThongTinNhanVien",
            Value = RepositoryReport.GetThongTinNhanVien(vaitroid, bophanId)
        };
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(ds);
        ReportViewer1.LocalReport.Refresh();
    }

    protected void LoadListVaiTro()
    {
        var listVaiTro = kho.DanhSachVaiTro;
        listVaiTro.Insert(0, new VaiTro()
        {
            MaVT = -1,
            TenVT = "--Tất cả--",
            TenVTEN = "--All--"
        });
        ListVaiTroSelect.DataSource = listVaiTro;
        ListVaiTroSelect.DataValueField = "MaVT";
        ListVaiTroSelect.DataTextField = "TenVT";
        ListVaiTroSelect.DataBind();
    }

    protected void LoadListBoPhan()
    {
        var listBP = kho.DanhSachBoPhan;
        listBP.Insert(0, new BoPhan()
        {
            MaBP = -1,
            TenBP = "--Tất cả--",
            TenBPEN = "--All--"
        });
        ListBoPhanSelect.DataSource = listBP;
        ListBoPhanSelect.DataValueField = "MaBP";
        ListBoPhanSelect.DataTextField = "TenBP";
        ListBoPhanSelect.DataBind();
    }


    protected void ListVaiTroSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReportRender(int.Parse(ListVaiTroSelect.SelectedValue), int.Parse(ListBoPhanSelect.SelectedValue));
    }
    protected void ListBoPhanSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReportRender(int.Parse(ListVaiTroSelect.SelectedValue), int.Parse(ListBoPhanSelect.SelectedValue));
    }
}