using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_ChuKyDanhGia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ReportRender();
        }
    }

    public void ReportRender()
    {
        ReportDataSource ds = new ReportDataSource()
        {
            Name = "ChuKyDanhGia",
            Value = RepositoryReport.GetChuKyDanhGiaReport()
        };
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(ds);
        ReportViewer1.LocalReport.Refresh();
    }
}