using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RepositoryReport
/// </summary>
public class RepositoryReport
{
    public RepositoryReport()
    {
        //
        // TODO: Add constructor logic here
        //


    }
    static DBDataContext db;
    public static IList<TieuChiTongHopReport> GetTieuChiTongHop()
    {
        using (db = new DBDataContext(MyUtility.ChuoiKetNoi))
        {
            var result = (from dv in db.DonVis
                          select new TieuChiTongHopReport
                        {
                            MaDV = dv.MaDV,
                            TenDV = dv.TenDV
                        }).ToList();

            return result;
        }
    }

    public static List<ChuKyDanhGiaReport> GetChuKyDanhGiaReport()
    {
        using (db = new DBDataContext(MyUtility.ChuoiKetNoi))
        {
            //var result = (from ckdg in db.ChuKyDanhGias
            //              join td in db.ThangDos on ckdg.MaTD equals td.MaTD
            //              join bm in db.BieuMauDanhGias on ckdg.MaBM equals bm.MaBM
            //              join dv in db.DonVis on ckdg.MaDV equals dv.MaDV
            //              join bp in db.BoPhans on dv.MaDV equals bp.MaDV
            //              select new ChuKyDanhGiaReport
            //              {
            //                  FromDate = ckdg.BatDau,
            //                  ToDate = ckdg.KetThuc,
            //                  Name = ckdg.TenCK,
            //                  status = ckdg.TrangThai,
            //                  TenBieuMau = bm.TenBM,
            //                  TenBoPhan = bp.TenBP
            //              }).ToList();

            //return result;
            return null;
        }
    }

    public static List<NhanVienReport> GetThongTinNhanVien(int vaitroid, int bophanId)
    {
        using (db = new DBDataContext(MyUtility.ChuoiKetNoi))
        {
            var result = (from nv in db.NhanViens
                          from vt in db.VaiTros
                          from cm in db.ChuyenMons
                          from vtcv in db.ViTriCongViecs
                          from dv in db.DonVis
                          from bp in db.BoPhans
                          where nv.MaVT == vt.MaVT
                          && (vt.MaVT == vaitroid || vaitroid == -1) // filter by vaitro
                           && (bp.MaBP == bophanId || bophanId == -1) // filter by Bophan
                          && nv.MaCM == cm.MaCM
                          && nv.MaVTCV == vtcv.MaVTCV
                          && vt.MaDV == dv.MaDV
                          && vtcv.MaBP == bp.MaBP
                          select new NhanVienReport
                          {
                              MaNhanVien = nv.MaNV,
                              HoTen = nv.HoTen,
                              DienThoai = nv.DienThoai,
                              Email = nv.Email,
                              GioiTinh = nv.GioiTinh == true ? "Nam" : "Nữ",
                              NgaySinh = nv.NgaySinh,
                              VaiTro = vt.TenVT,
                              ChuyenMon = cm.TenCM,
                              ViTriCongViec = vtcv.TenVTCV,
                              DonVi = dv.TenDV,
                              BoPhan = bp.TenBP
                          }).ToList();

            return result;
        }
    }
}
public class TieuChiTongHopReport
{
    public int MaDV { get; set; }

    public string TenDV { get; set; }
}

public class ChuKyDanhGiaReport
{
    public string Name { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string TenBoPhan { get; set; }

    public string TenBieuMau { get; set; }

    public int status { get; set; }

}

public class NhanVienReport
{
    public string MaNhanVien { get; set; }

    public string HoTen { get; set; }

    public string GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string DienThoai { get; set; }

    public string Email { get; set; }

    public string VaiTro { get; set; }

    public string ChuyenMon { get; set; }

    public string DonVi { get; set; }

    public string ViTriCongViec { get; set; }

    public string BoPhan { get; set; }


}