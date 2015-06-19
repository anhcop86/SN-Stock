using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_ChuKyDanhGia : TrangKho
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LoadListChuKy();
            ReportRender(-1);
        }
    }
    //static DBDataContext db;
    protected void LoadListChuKy()
    {
        
            var listVaiTro = kho.DanhSachChuKy;
            listVaiTro.Insert(0, new ChuKyDanhGia()
            {
                MaCK = -1,
                TenCK = "--Tất cả--",
            });
            ListDanhMucCKSelect.DataSource = listVaiTro;
            ListDanhMucCKSelect.DataValueField = "MaCK";
            ListDanhMucCKSelect.DataTextField = "TenCK";
            ListDanhMucCKSelect.DataBind();
        
    }
    public void ReportRender(int ckid)
    {
        ReportDataSource ds = new ReportDataSource()
        {
            Name = "ChuKyDanhGia",
            Value = RepositoryReport.GetChuKyDanhGiaReport(ckid)
        };
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(ds);
        ReportViewer1.LocalReport.Refresh();
    }

    protected void ListDanhMucCKSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReportRender(int.Parse(ListDanhMucCKSelect.SelectedValue));

    }
}