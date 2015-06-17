using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TrangKho : System.Web.UI.Page
{
    protected IKho kho;
    public TrangKho()
    {
        this.kho = new Kho();
    }
    public int MaDV
    {
        get
        {
            return 1;
        }
    }
    public DonVi DonVi
    {
        get
        {
            return kho.TimDonVi(this.MaDV);
        }
    }

    public int TongSoTK
    {
        get
        {
            return (int)DonVi.TongSoTK;
        }
    }
    public int SoTKHienTai
    {
        get
        {
            return kho.LayDanhSachNhanVienTheoDonVi(this.MaDV).Count;
        }
    }
    public List<NhanVien> DSNV
    {
        get
        {
            return kho.LayDanhSachNhanVienTheoDonVi(this.MaDV).ToList();
        }
    }
    public List<BoPhan> DSBP
    {
        get
        {
            return kho.LayDanhSachBoPhanTheoDonVi(this.MaDV).ToList();
        }
    }
    public List<ViTriCongViec> DSVTCV
    {
        get
        {
            return this.kho.LayDanhSachViTriCongViecTheoDV(this.MaDV).ToList();
        }
    }
    public bool TiengViet
    {
        get
        {
            return MyUtility.TiengViet;
        }
    }

    public List<BieuMauDanhGia> BieuMau
    {
        get
        {
            return kho.TimBieuMauTheoDV(this.MaDV).ToList();
        }
    }
    public NhanVien NhanVien
    {
        get
        {
            return kho.DanhSachNhanVien.FirstOrDefault(n=>n.MaVTCV.Equals(1));
        }
    }
}