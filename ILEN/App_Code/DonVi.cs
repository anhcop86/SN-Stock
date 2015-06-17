using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DonVi
/// </summary>
public partial class DonVi
{
    public bool HetTaiKhoan
    {
        get
        {
            int limit = new Kho().LayDanhSachNhanVienTheoDonVi(this.MaDV).Count;
            return this.TongSoTK <= limit;
        }
    }
}