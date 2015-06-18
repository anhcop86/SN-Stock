using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;

public class Kho : IKho
{
    #region datacontext
    DBDataContext dc = new DBDataContext(MyUtility.ChuoiKetNoi);
    public bool Luu()
    {
        try
        {
            dc.SubmitChanges();
            return true;
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
            return false;
        }
    }
    #endregion

    #region don vi
    public IList<DonVi> DanhSachDonVi
    {
        get
        {
            return dc.DonVis.ToList();
        }
    }
    public DonVi TimDonVi(int ma)
    {
        return dc.DonVis.FirstOrDefault(d => d.MaDV.Equals(ma));
    }
    public bool ThemDonVi(DonVi dv)
    {
        try
        {
            var kq = TimDonVi(dv.MaDV);
            if (kq != default(DonVi)) return false;
            dc.DonVis.InsertOnSubmit(dv);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaDonVi(DonVi dv)
    {
        try
        {
            var kq = TimDonVi(dv.MaDV);
            if (kq == default(DonVi)) return false;
            kq.TenDV = dv.TenDV;
            kq.DiaChi = dv.DiaChi;
            kq.DienThoai = dv.DienThoai;
            kq.Fax = dv.Fax;
            kq.Email = dv.Email;
            kq.TenDVEN = dv.TenDVEN;
            kq.TrangThai = dv.TrangThai;
            kq.Website = dv.Website;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaDonVi(DonVi dv)
    {
        try
        {
            var kq = TimDonVi(dv.MaDV);
            if (kq == default(DonVi)) return false;
            dc.DonVis.DeleteOnSubmit(kq);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region nhan vien

    public IList<ListItem> LayDanhSachNhanVienTheoDonViKemBoPhan(int maDV)
    {
        List<ListItem> lst = new List<ListItem>();
        foreach (var i in dc.BoPhans.Where(b => b.MaDV.Equals(maDV)))
            foreach (ViTriCongViec v in i.ViTriCongViecs)
                foreach (NhanVien n in v.NhanViens)
                {
                    ListItem item = new ListItem(n.HoTen, n.MaNV);
                    item.Attributes["OptionGroup"] = MyUtility.TiengViet ? i.TenBP : i.TenBPEN;
                    lst.Add(item);
                }
        return lst;
    }

    public IList<NhanVien> LayDanhSachNhanVien(string maNV = null
        , string hoTen = null, int maDV = 0, int maBP = 0)
    {
        IList<NhanVien> kq = DanhSachNhanVien;
        if (maNV != null)
            kq = kq.Where(n => n.MaNV.ToLower().Contains(maNV.ToLower())).ToList();
        if (hoTen != null)
            kq = kq.Where(n => n.HoTen.ToLower().Contains(hoTen.ToLower())).ToList();

        return kq;
    }
    public string SinhMatKhauMacDinhChoNV()
    {
        return "123";
    }
    public string SinhMaNhanVien()
    {
        var ma = (DanhSachNhanVien.Max(nv => nv.STT) + 1).ToString();
        while (ma.Length < 5) ma = "0" + ma;
        return string.Format(@"NV{0}", ma);
    }
    public IList<NhanVien> DanhSachNhanVien
    {
        get
        {
            return dc.NhanViens.ToList();
        }
    }
    public IList<NhanVien> LayDanhSachNhanVienTheoDonVi(int maDV)
    {
        var lst = from vt in DanhSachViTriCongViec
                  join nv in DanhSachNhanVien
                  on vt.MaVTCV equals nv.MaVTCV
                  join bp in DanhSachBoPhan
                  on vt.MaBP equals bp.MaBP
                  join dv in DanhSachDonVi
                  on bp.MaDV equals dv.MaDV
                  where dv.MaDV.Equals(maDV)
                  select nv;
        return lst.ToList();
    }
    public IList<NhanVien> LayDanhSachNhanVienTheoBoPhan(int maBP)
    {
        return null;
    }

    public NhanVien TimNhanVien(string ma)
    {
        return dc.NhanViens.FirstOrDefault(n => n.MaNV.Equals(ma));
    }

    public bool ThemNhanVien(NhanVien nv)
    {
        try
        {
            var kq = TimNhanVien(nv.MaNV);
            if (kq != default(NhanVien)) return false;
            dc.NhanViens.InsertOnSubmit(nv);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaNhanVien(NhanVien nv)
    {
        try
        {
            var kq = TimNhanVien(nv.MaNV);
            if (kq == default(NhanVien)) return false;
            kq.HoTen = nv.HoTen;
            kq.GioiTinh = nv.GioiTinh;
            kq.NgaySinh = nv.NgaySinh;
            kq.DienThoai = nv.DienThoai;
            kq.Email = nv.Email;
            kq.Diachi = nv.Diachi;
            kq.NgayHD = nv.NgayHD;
            //kq.MaBP = nv.MaBP;
            kq.MaCM = nv.MaCM;
            kq.MaVT = nv.MaVT;

            if (!string.IsNullOrEmpty(nv.Hinh))
                kq.Hinh = nv.Hinh;

            if (!string.IsNullOrEmpty(nv.MatKhau) && !string.IsNullOrWhiteSpace(nv.MatKhau))
                kq.MatKhau = nv.MatKhau;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaNhanVien(NhanVien nv)
    {
        try
        {
            var kq = TimNhanVien(nv.MaNV);
            if (kq == default(NhanVien)) return false;
            dc.NhanViens.DeleteOnSubmit(nv);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region bo phan
    public IList<BoPhan> DanhSachBoPhan
    {
        get
        {
            return dc.BoPhans.ToList();
        }
    }

    public IList<BoPhan> LayDanhSachBoPhanTheoDonVi(int maDV)
    {
        return DanhSachBoPhan.Where(b => b.MaDV.Equals(maDV)).ToList();
    }
    public IList<BoPhan> LayDanhSachBoPhanTheoDonVi(int maDV, string tenBP)
    {
        return LayDanhSachBoPhanTheoDonVi(maDV).Where(b =>
            b.TenBPEN.ToLower().Contains(tenBP.ToLower())
            ||
            b.TenBP.ToLower().Contains(tenBP.ToLower())).ToList();
    }
    public BoPhan TimBoPhan(int ma)
    {
        return dc.BoPhans.FirstOrDefault(b => b.MaBP.Equals(ma));
    }

    public bool ThemBoPhan(BoPhan bp)
    {
        try
        {
            var kq = TimBoPhan(bp.MaBP);
            if (kq != default(BoPhan)) return false;
            dc.BoPhans.InsertOnSubmit(bp);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaBoPhan(BoPhan bp)
    {
        try
        {
            var kq = TimBoPhan(bp.MaBP);
            if (kq == default(BoPhan)) return false;
            kq.TenBP = bp.TenBP;
            kq.MaDV = bp.MaDV;
            kq.MoTa = bp.MoTa;
            kq.TenBPEN = bp.TenBPEN;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaBoPhan(BoPhan bp)
    {
        try
        {
            var kq = TimBoPhan(bp.MaBP);
            if (kq == default(BoPhan)) return false;
            if (bp.ViTriCongViecs.Count > 0) return false;
            dc.BoPhans.DeleteOnSubmit(kq);
            return true;
        }
        catch (Exception ex)
        {
            var temp = ex.Message;
            return false;
        }
    }
    #endregion

    #region tieu chi
    public IList<TieuChi> DanhSachTieuChi
    {
        get
        {
            return dc.TieuChis.ToList();
        }
    }
    public TieuChi TimTieuChi(int ma)
    {
        return dc.TieuChis.FirstOrDefault(t => t.MaTC.Equals(ma));
    }
    public bool ThemTieuChi(TieuChi tc)
    {
        try
        {
            //var kq = TimTieuChi(tc.MaTC);
            //if (kq != default(TieuChi)) return false;
            dc.TieuChis.InsertOnSubmit(tc);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaTieuChi(TieuChi tc)
    {
        try
        {
            var kq = TimTieuChi(tc.MaTC);
            if (kq == default(TieuChi)) return false;
            kq.MaNTC = tc.MaNTC;
            kq.TenTC = tc.TenTC;
            kq.TrongSo = tc.TrongSo;
            kq.MoTa = tc.MoTa;
            kq.MaTCCha = tc.MaTCCha;
            kq.TenTCEN = tc.TenTCEN;

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaTieuChi(TieuChi tc)
    {
        try
        {
            var kq = TimTieuChi(tc.MaTC);
            if (kq == default(TieuChi)) return false;

            //kiem tra rang buoc:
            if (tc.ChiTietDanhGias.Count > 0) throw new Exception();
            if (tc.CapDoTheoTieuChis.Count > 0) throw new Exception();

            dc.TieuChis.DeleteOnSubmit(tc);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public IList<TieuChi> DanhSachTCTheoDV(int maDV)
    {
        return dc.TieuChis.Where(n => n.NhomTieuChi.MaDV.Equals(maDV)).ToList();
    }
    public IList<TieuChi> DanhSachTCTheoNTC(int maNTC)
    {
        return dc.TieuChis.Where(n => n.NhomTieuChi.MaNTC.Equals(maNTC)).ToList();
    }
    public IList<TieuChi> DanhSachTCTheoBM(int maBM)
    {
        var lst = from tc in dc.TieuChiTheoBieuMaus.Where(x => x.MaBM.Equals(maBM)).ToList()
                  join t in dc.TieuChis
                  on tc.MaTC equals t.MaTC
                  select new TieuChi//luc nay ko new tieu chi duoc nua. ok?cho chut.ok tiep fan khac di.
                  {
                      MaTC = tc.MaTC,
                      MaTCCha = t.MaTCCha,
                      MaNTC = t.MaNTC,
                      TenTC = t.TenTC,
                      TenTCEN = t.TenTCEN,
                      Chon = t.Chon,
                      TrongSo = tc.TrongSo
                  };
        return lst.ToList();
    }
    #endregion

    #region CapDo theo tieu chi
    public IList<CapDoTheoTieuChi> DanhSachCapDoTheoTieuChi
    {
        get
        {
            return dc.CapDoTheoTieuChis.ToList();
        }
    }
    public CapDoTheoTieuChi TimCapDoTheoTieuChi(int ma)
    {
        return dc.CapDoTheoTieuChis.FirstOrDefault(d => d.Ma.Equals(ma));
    }
    public CapDoTheoTieuChi TimCapDoTheoTieuChi1(int maTC, int maCD)
    {
        return dc.CapDoTheoTieuChis.FirstOrDefault(d => d.MaTC.Equals(maTC) && d.MaCD.Equals(maCD));
    }
    public bool ThemCapDoTheoTieuChi(CapDoTheoTieuChi dttc)
    {
        try
        {
            dc.CapDoTheoTieuChis.InsertOnSubmit(dttc);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool ThemDSCDTheoTC(List<CapDoTheoTieuChi> dscdttc)
    {
        try
        {
            dc.CapDoTheoTieuChis.InsertAllOnSubmit(dscdttc);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaCapDoTheoTieuChi(CapDoTheoTieuChi dttc)
    {
        try
        {
            var kq = TimCapDoTheoTieuChi(dttc.Ma);
            if (kq == default(CapDoTheoTieuChi)) throw new Exception();
            //kq.Ten = dttc.Ten;
            kq.MaCD = dttc.MaCD;
            kq.MaTC = dttc.MaTC;
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaCapDoTheoTieuChi(CapDoTheoTieuChi d)
    {
        try
        {
            var kq = TimCapDoTheoTieuChi(d.Ma);
            if (kq == default(CapDoTheoTieuChi)) throw new Exception();
            dc.CapDoTheoTieuChis.DeleteOnSubmit(d);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region nhom tieu chi
    public IList<NhomTieuChi> LayNhomTCTheoMaBM(int maBM)
    {
        IList<TieuChi> result = this.DanhSachTCTheoBM(maBM).ToList();
        IList<NhomTieuChi> lstNhom = new List<NhomTieuChi>();
        foreach (var i in result)
        {
            var obj = dc.NhomTieuChis.FirstOrDefault(n => n.MaNTC.Equals(i.MaNTC));
            if (!lstNhom.Contains(obj))
                lstNhom.Add(obj);
        }
        return lstNhom;
    }
    public IList<NhomTieuChi> LayDanhSachNTC(string ten)
    {
        return DanhSachNhomTieuChi.Where(c => c.TenNTC.ToLower().Contains(ten.ToLower())
            || c.TenNTCEN.ToLower().Contains(ten.ToLower())).ToList();
    }
    public IList<NhomTieuChi> DanhSachNhomTieuChi
    {
        get
        {
            return dc.NhomTieuChis.ToList();
        }
    }
    public NhomTieuChi TimNhomTieuChi(int ma)
    {
        return dc.NhomTieuChis.FirstOrDefault(n => n.MaNTC.Equals(ma));
    }
    public bool ThemNhomTieuChi(NhomTieuChi ntc)
    {
        try
        {
            var kq = TimNhomTieuChi(ntc.MaNTC);
            if (kq != default(NhomTieuChi)) throw new Exception();
            dc.NhomTieuChis.InsertOnSubmit(ntc);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaNhomTieuChi(NhomTieuChi ntc)
    {
        try
        {
            var kq = TimNhomTieuChi(ntc.MaNTC);
            if (kq == default(NhomTieuChi)) throw new Exception();

            kq.TenNTC = ntc.TenNTC;
            kq.TrongSo = ntc.TrongSo;
            kq.MoTa = ntc.MoTa;
            kq.TenNTCEN = ntc.TenNTCEN;

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaNhomTieuChi(NhomTieuChi ntc)
    {
        try
        {
            var kq = TimNhomTieuChi(ntc.MaNTC);
            if (kq == default(NhomTieuChi)) throw new Exception();

            dc.NhomTieuChis.DeleteOnSubmit(ntc);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public IList<NhomTieuChi> TimNhomTieuChiTheoMaDV(int maDV)
    {
        return dc.NhomTieuChis.Where(n => n.MaDV.Equals(maDV)).ToList();
    }
    #endregion

    #region Bieu mau
    public IList<BieuMauDanhGia> DanhSachBieuMau
    {
        get
        {
            return dc.BieuMauDanhGias.ToList();
        }
    }
    public BieuMauDanhGia TimBieuMau(int maCKDG, int maVTCV)
    {
        return dc.BieuMauDanhGias.FirstOrDefault(b => b.MaCK.Equals(maCKDG)
            && b.MaVTCV.Equals(maVTCV));
    }
    public BieuMauDanhGia TimBieuMau(int maBM)
    {
        return dc.BieuMauDanhGias.FirstOrDefault(b => b.MaBM.Equals(maBM));
    }
    public IList<BieuMauDanhGia> TimBieuMauTheoDV(int maDV)
    {
        return dc.BieuMauDanhGias.Where(b => b.MaDV.Equals(maDV) && b.Chon).ToList();
    }
    public bool luuBMDG(BieuMauDanhGia bmdg)
    {
        try
        {
            dc.BieuMauDanhGias.InsertOnSubmit(bmdg);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region Vi Tri Cong Viec
    public IList<ViTriCongViec> DanhSachVTCV
    {
        get
        {
            return dc.ViTriCongViecs.ToList();
        }
    }
    public IList<ViTriCongViec> DanhSachVTCVTheoDV(int maDV)
    {
        return dc.ViTriCongViecs.Where(v => v.BoPhan.MaDV.Equals(maDV)).ToList();
    }

    #endregion

    #region ket qua danh gia
    public IList<KetQuaDanhGia> DanhSachKetQuaDanhGia
    {
        get
        {
            return dc.KetQuaDanhGias.ToList();
        }
    }
    public KetQuaDanhGia TimKetQuaDanhGia(int ma)
    {
        return dc.KetQuaDanhGias.FirstOrDefault(k => k.MaKQDG.Equals(ma));
    }
    public bool ThemKetQuaDanhGia(KetQuaDanhGia k)
    {
        try
        {
            var kq = TimKetQuaDanhGia(k.MaKQDG);
            if (kq != default(KetQuaDanhGia)) throw new Exception();
            dc.KetQuaDanhGias.InsertOnSubmit(k);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaKetQuaDanhGia(KetQuaDanhGia k)
    {
        try
        {
            var kq = TimKetQuaDanhGia(k.MaKQDG);
            if (kq == default(KetQuaDanhGia)) throw new Exception();

            kq.NgayDG = k.NgayDG;
            kq.NguoiDG = k.NguoiDG;
            kq.NguoiDuocDG = k.NguoiDuocDG;
            kq.CoHieuLuc = k.CoHieuLuc;
            kq.GhiChu = k.GhiChu;

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaKetQuaDanhGia(KetQuaDanhGia k)
    {
        try
        {
            var kq = TimKetQuaDanhGia(k.MaKQDG);
            if (kq == default(KetQuaDanhGia)) throw new Exception();

            dc.KetQuaDanhGias.DeleteOnSubmit(k);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region chi tiet danh gia
    public IList<ChiTietDanhGia> DanhSachChiTietDanhGia
    {
        get
        {
            return dc.ChiTietDanhGias.ToList();
        }
    }
    public ChiTietDanhGia TimChiTietDanhGia(int ma)
    {
        return dc.ChiTietDanhGias.FirstOrDefault(c => c.MaCTDG.Equals(ma));
    }
    public bool ThemChiTietDanhGia(ChiTietDanhGia ct)
    {
        try
        {
            var kq = TimChiTietDanhGia(ct.MaCTDG);
            if (kq != null) throw new Exception();
            dc.ChiTietDanhGias.InsertOnSubmit(ct);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaChiTietDanhGia(ChiTietDanhGia ct)
    {
        try
        {
            var kq = TimChiTietDanhGia(ct.MaCTDG);
            if (kq == null) throw new Exception();

            //kq.MaTC = ct.MaTC;
            kq.MaCD = ct.MaCD;
            kq.MaKQDG = ct.MaKQDG;

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaChiTietDanhGia(ChiTietDanhGia ct)
    {
        try
        {
            var kq = TimChiTietDanhGia(ct.MaCTDG);
            if (kq == null) throw new Exception();

            dc.ChiTietDanhGias.DeleteOnSubmit(ct);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region chuyen mon

    public IList<ChuyenMon> LayDanhSachChuyenMon(int maDV)
    {
        return dc.ChuyenMons.Where(c => c.MaDV.Equals(maDV)).ToList();
    }

    public IList<ChuyenMon> LayDanhSachChuyenMon(string ten)
    {
        return DanhSachChuyenMon.Where(c => c.TenCM.ToLower().Contains(ten.ToLower())
            || c.TenCMEN.ToLower().Contains(ten.ToLower())).ToList();
    }
    public IList<ChuyenMon> DanhSachChuyenMon
    {
        get
        {
            return dc.ChuyenMons.ToList();
        }
    }
    public ChuyenMon TimChuyenMon(int ma)
    {
        return dc.ChuyenMons.FirstOrDefault(c => c.MaCM.Equals(ma));
    }
    public bool ThemChuyenMon(ChuyenMon cm)
    {
        try
        {
            var kq = TimChuyenMon(cm.MaCM);
            if (kq != default(ChuyenMon)) throw new Exception();

            dc.ChuyenMons.InsertOnSubmit(cm);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaChuyenMon(ChuyenMon cm)
    {
        try
        {
            var kq = TimChuyenMon(cm.MaCM);
            if (kq == default(ChuyenMon)) throw new Exception();

            kq.TenCM = cm.TenCM;
            kq.MoTa = cm.MoTa;
            kq.TenCMEN = cm.TenCMEN;

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaChuyenMon(ChuyenMon cm)
    {
        try
        {
            var kq = TimChuyenMon(cm.MaCM);
            if (kq == default(ChuyenMon)) throw new Exception();

            dc.ChuyenMons.DeleteOnSubmit(cm);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region ngoai ngu
    public IList<NgoaiNgu> LayDanhSachNgoaiNgu(string ten)
    {
        return DanhSachNgoaiNgu.Where(n => n.TenNN.ToLower().Contains(ten.ToLower())
            || n.TenNNEN.ToLower().Contains(ten.ToLower())).ToList();
    }
    public IList<NgoaiNgu> DanhSachNgoaiNgu
    {
        get
        {
            return dc.NgoaiNgus.ToList();
        }
    }
    public NgoaiNgu TimNgoaiNgu(int ma)
    {
        return dc.NgoaiNgus.FirstOrDefault(nn => nn.MaNN.Equals(ma));
    }
    public bool ThemNgoaiNgu(NgoaiNgu nn)
    {
        try
        {
            var kq = TimNgoaiNgu(nn.MaNN);
            if (kq != default(NgoaiNgu)) throw new Exception();

            dc.NgoaiNgus.InsertOnSubmit(nn);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool SuaNgoaiNgu(NgoaiNgu nn)
    {
        try
        {
            var kq = TimNgoaiNgu(nn.MaNN);
            if (kq == default(NgoaiNgu)) throw new Exception();

            kq.TenNN = nn.TenNN;
            kq.MoTa = nn.MoTa;
            kq.TenNNEN = nn.TenNNEN;

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool XoaNgoaiNgu(NgoaiNgu nn)
    {
        try
        {
            var kq = TimNgoaiNgu(nn.MaNN);
            if (kq == default(NgoaiNgu)) throw new Exception();

            dc.NgoaiNgus.DeleteOnSubmit(nn);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region danh muc quan tri
    public string LayTenDanhMucQuanTri(string rawURL)
    {
        int viTri = rawURL.LastIndexOf('/');
        string site = rawURL.Substring(viTri + 1);
        DanhMucQuanTri dmqt = TimDanhMucQuanTri(site);
        if (dmqt != default(DanhMucQuanTri))
            return dmqt.TenDM;
        else return string.Empty;
    }
    public IList<DanhMucQuanTri> DanhSachDanhMucQuanTri
    {
        get
        {
            return dc.DanhMucQuanTris.ToList();
        }
    }

    public DanhMucQuanTri TimDanhMucQuanTri(string ma)
    {
        return dc.DanhMucQuanTris.FirstOrDefault(d => d.MaDM.Equals(ma));
    }

    public bool ThemDanhMucQuanTri(DanhMucQuanTri dm)
    {
        try
        {
            var kq = TimDanhMucQuanTri(dm.MaDM);
            if (kq != default(DanhMucQuanTri)) return false;

            dc.DanhMucQuanTris.InsertOnSubmit(dm);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaDanhMucQuanTri(DanhMucQuanTri dm)
    {
        try
        {
            var kq = TimDanhMucQuanTri(dm.MaDM);
            if (kq == default(DanhMucQuanTri)) return false;

            kq.TenDM = dm.TenDM;
            kq.MoTa = dm.MoTa;
            kq.TenDMEN = dm.TenDMEN;


            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaDanhMucQuanTri(DanhMucQuanTri dm)
    {
        try
        {
            var kq = TimDanhMucQuanTri(dm.MaDM);
            if (kq == default(DanhMucQuanTri)) return false;

            dc.DanhMucQuanTris.DeleteOnSubmit(dm);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public List<string> LayCacTrangAdmin()
    {
        List<string> kq = new List<string>();

        string virPath = @"~/Admin";
        string phyPath = HttpContext.Current.Server.MapPath(virPath);

        DirectoryInfo dir = new DirectoryInfo(phyPath);

        foreach (FileInfo f in dir.GetFiles())
            if (f.Extension.Equals(".cs"))
                kq.Add(f.Name);

        return kq;
    }

    public bool SuaDanhMucQuanTri(string maDM, string tenDM)
    {
        try
        {
            var kq = TimDanhMucQuanTri(maDM);
            if (kq == default(DanhMucQuanTri)) return false;
            kq.TenDM = tenDM;

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region vai tro

    public IList<VaiTro> LayDanhSachVaiTro(int maDV)
    {
        return dc.VaiTros.Where(v => v.MaDV.Equals(maDV)).ToList();
    }

    public IList<VaiTro> LayDanhSachVaiTro(string ten)
    {
        return DanhSachVaiTro.Where(c => c.TenVT.ToLower().Contains(ten.ToLower())
            || c.TenVTEN.ToLower().Contains(ten.ToLower())).ToList();
    }
    public IList<VaiTro> DanhSachVaiTro
    {
        get
        {
            return dc.VaiTros.ToList();
        }
    }

    public VaiTro TimVaiTro(int ma)
    {
        return dc.VaiTros.FirstOrDefault(v => v.MaVT.Equals(ma));
    }

    public bool ThemVaiTro(VaiTro vt)
    {
        try
        {
            var kq = TimVaiTro(vt.MaVT);
            if (kq != default(VaiTro)) return false;

            dc.VaiTros.InsertOnSubmit(vt);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaVaiTro(VaiTro vt)
    {
        try
        {
            var kq = TimVaiTro(vt.MaVT);
            if (kq == default(VaiTro)) return false;

            kq.TenVT = vt.TenVT;
            kq.MoTa = vt.MoTa;
            kq.TenVTEN = vt.TenVTEN;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaVaiTro(VaiTro vt)
    {
        try
        {
            var kq = TimVaiTro(vt.MaVT);
            if (kq == default(VaiTro)) return false;

            dc.VaiTros.DeleteOnSubmit(vt);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region quyen
    public IList<Quyen> DanhSachQuyen
    {
        get
        {
            return dc.Quyens.ToList();
        }
    }

    public Quyen LayQuyen(int maVT, string maDMQT)
    {
        return dc.Quyens.FirstOrDefault(q => q.MaVT.Equals(maVT) && q.MaDM.Equals(maDMQT));
    }

    public Quyen TimQuyen(int ma)
    {
        return dc.Quyens.FirstOrDefault(q => q.Ma.Equals(ma));
    }

    public bool ThemQuyen(Quyen q)
    {
        try
        {
            var kq = TimQuyen(q.Ma);
            if (kq != default(Quyen)) return false;

            dc.Quyens.InsertOnSubmit(q);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaQuyen(Quyen q)
    {
        try
        {
            var kq = TimQuyen(q.Ma);
            if (kq == default(Quyen)) return false;

            kq.MaVT = q.MaVT;
            kq.MaDM = q.MaDM;
            kq.Xem = q.Xem;
            kq.Them = q.Them;
            kq.Xoa = q.Xoa;
            kq.Sua = q.Sua;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaQuyen(Quyen q)
    {
        try
        {
            var kq = TimQuyen(q.Ma);
            if (kq == default(Quyen)) return false;

            dc.Quyens.DeleteOnSubmit(q);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region ngoai ngu cua nhan vien
    public IList<NgoaiNguCuaNhanVien> DanhSachNgoaiNguCuaNhanVien
    {
        get
        {
            return dc.NgoaiNguCuaNhanViens.ToList();
        }
    }

    public NgoaiNguCuaNhanVien TimNgoaiNguCuaNhanVien(int ma)
    {
        return dc.NgoaiNguCuaNhanViens.FirstOrDefault(n => n.Ma.Equals(ma));
    }

    public bool ThemNgoaiNguCuaNhanVien(NgoaiNguCuaNhanVien nn)
    {
        try
        {
            var kq = TimNgoaiNguCuaNhanVien(nn.Ma);
            if (kq != default(NgoaiNguCuaNhanVien)) return false;

            dc.NgoaiNguCuaNhanViens.InsertOnSubmit(nn);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaNgoaiNguCuaNhanVien(NgoaiNguCuaNhanVien nn)
    {
        try
        {
            var kq = TimNgoaiNguCuaNhanVien(nn.Ma);
            if (kq == default(NgoaiNguCuaNhanVien)) return false;

            kq.MaNV = nn.MaNV;
            kq.MaNN = nn.MaNN;
            kq.TrinhDo = nn.TrinhDo;


            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaNgoaiNguCuaNhanVien(NgoaiNguCuaNhanVien nn)
    {
        try
        {
            var kq = TimNgoaiNguCuaNhanVien(nn.Ma);
            if (kq == default(NgoaiNguCuaNhanVien)) return false;

            dc.NgoaiNguCuaNhanViens.DeleteOnSubmit(nn);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region danh muc hien thi
    public IList<DanhMucHienThi> DanhSachDanhMucHienThi
    {
        get
        {
            return dc.DanhMucHienThis.ToList();
        }
    }

    public DanhMucHienThi TimDanhMucHienThi(string ma)
    {
        return dc.DanhMucHienThis.FirstOrDefault(d => d.Ma.ToLower().Equals(ma.ToLower()));
    }

    public bool ThemDanhMucHienThi(DanhMucHienThi dm)
    {
        try
        {
            var kq = TimDanhMucHienThi(dm.Ma);
            if (kq != default(DanhMucHienThi)) return false;
            dc.DanhMucHienThis.InsertOnSubmit(dm);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaDanhMucHienThi(DanhMucHienThi dm)
    {
        try
        {
            var kq = TimDanhMucHienThi(dm.Ma);
            if (kq == default(DanhMucHienThi)) return false;

            kq.TenVN = dm.TenVN;
            kq.TenEN = dm.TenEN;
            kq.MoTa = dm.MoTa;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaDanhMucHienThi(DanhMucHienThi dm)
    {
        try
        {
            var kq = TimDanhMucHienThi(dm.Ma);
            if (kq == default(DanhMucHienThi)) return false;

            dc.DanhMucHienThis.DeleteOnSubmit(dm);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region admin don vi
    public IList<AdminDV> DanhSachAdminDV
    {
        get
        {
            return dc.AdminDVs.ToList();
        }
    }

    public AdminDV TimAdminDV(int ma)
    {
        return dc.AdminDVs.FirstOrDefault(v => v.MaDV.Equals(ma));
    }

    public bool ThemAdminDV(AdminDV ad)
    {
        try
        {
            var kq = TimAdminDV(ad.MaDV);
            if (kq != default(AdminDV)) return false;

            dc.AdminDVs.InsertOnSubmit(ad);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SuaAdminDV(AdminDV ad)
    {
        try
        {
            var kq = TimAdminDV(ad.MaDV);
            if (kq == default(AdminDV)) return false;
            kq.MaDV = ad.MaDV;
            kq.TenDN = ad.TenDN;
            kq.GioiTinh = ad.GioiTinh;
            kq.NgaySinh = ad.NgaySinh;
            if (MyUtility.ChuoiHopLe(ad.Hinh))
                kq.Hinh = ad.Hinh;
            kq.Email = ad.Email;
            kq.DienThoai = ad.DienThoai;
            kq.TrangThai = ad.TrangThai;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaAdminDV(AdminDV vt)
    {
        try
        {
            var kq = TimAdminDV(vt.MaDV);
            if (kq == default(AdminDV)) return false;

            dc.AdminDVs.DeleteOnSubmit(vt);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region vi tri cong viec
    public IList<ViTriCongViec> DanhSachViTriCongViec
    {
        get
        {
            return dc.ViTriCongViecs.ToList();
        }
    }

    public IList<ViTriCongViec> LayDanhSachViTriCongViecTheoDV(int maDV)
    {
        var list = from bp in this.LayDanhSachBoPhanTheoDonVi(maDV)
                   join vt in this.DanhSachViTriCongViec
                   on bp.MaBP equals vt.MaBP
                   select vt;
        return list.ToList();
    }

    public ViTriCongViec TimViTriCongViec(int ma)
    {
        return dc.ViTriCongViecs.FirstOrDefault(vt => vt.MaVTCV.Equals(ma));
    }

    public bool ThemViTriCongViec(ViTriCongViec vtcv)
    {
        try
        {
            var kq = TimViTriCongViec(vtcv.MaVTCV);
            if (kq != default(ViTriCongViec)) return false;
            dc.ViTriCongViecs.InsertOnSubmit(vtcv);
            return true;
        }
        catch (Exception ex)
        {
            var temp = ex.Message;
            return false;
        }
    }

    public bool SuaViTriCongViec(ViTriCongViec vtcv)
    {
        try
        {
            var kq = TimViTriCongViec(vtcv.MaVTCV);
            if (kq == default(ViTriCongViec)) return true;

            kq.MaBP = vtcv.MaBP;
            kq.TenVTCV = vtcv.TenVTCV;
            kq.TenVTCVEN = vtcv.TenVTCVEN;
            kq.TrangThai = vtcv.TrangThai;
            kq.MoTa = vtcv.MoTa;
            kq.MoTaEN = vtcv.MoTaEN;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaViTriCongViec(ViTriCongViec vtcv)
    {
        try
        {
            var kq = TimViTriCongViec(vtcv.MaVTCV);
            if (kq == default(ViTriCongViec)) return true;

            if (kq.NhanViens.Count > 0) return false;//co nhan vien

            dc.ViTriCongViecs.DeleteOnSubmit(kq);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public IList<ViTriCongViec> LayDanhSachViTriCongViecTheoBP(int maBP)
    {
        return dc.ViTriCongViecs.Where(v => v.MaBP.Equals(maBP)).ToList();
    }
    #endregion

    #region chu ky danh gia
    public IList<ChuKyDanhGia> DanhSachChuKy
    {
        get
        {
            return dc.ChuKyDanhGias.ToList();
        }
    }
    public IList<ChuKyDanhGia> LayDanhSachChuKyDanhGiaTheoDV(int maDV)
    {
        return dc.ChuKyDanhGias.Where(c => c.MaDV.Equals(maDV)).ToList();
    }

    public ChuKyDanhGia TimChuKyDanhGia(int maCK)
    {
        return dc.ChuKyDanhGias.FirstOrDefault(c => c.MaCK.Equals(maCK));
    }

    public bool ThemChuKyDanhGia(ChuKyDanhGia ck)
    {
        try
        {
            var kq = TimChuKyDanhGia(ck.MaCK);
            if (kq != default(ChuKyDanhGia)) return false;

            dc.ChuKyDanhGias.InsertOnSubmit(ck);

            return true;
        }

        catch
        {
            return false;
        }
    }

    public bool SuaChuKyDanhGia(ChuKyDanhGia ck)
    {
        try
        {
            var kq = TimChuKyDanhGia(ck.MaCK);
            if (kq == default(ChuKyDanhGia)) return true;

            kq.TenCK = ck.TenCK;
            kq.TenCKEN = ck.TenCKEN;
            kq.BatDau = ck.BatDau;
            kq.KetThuc = ck.KetThuc;
            kq.TrangThai = ck.TrangThai;
            kq.MoTa = ck.MoTa;
            kq.MoTaEN = ck.MoTaEN;
            kq.MaDV = ck.MaDV;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool XoaChuKyDanhGia(ChuKyDanhGia ck)
    {
        try
        {
            var kq = TimChuKyDanhGia(ck.MaCK);
            if (kq == default(ChuKyDanhGia)) return true;

            dc.ChuKyDanhGias.DeleteOnSubmit(kq);

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region Tieu Chi Theo Bieu mau

    public IList<TieuChi> LayTCTheoBMVaTCCha(int maBM, int maTCCha)
    {
        var lst = (from tc in dc.TieuChis.Where(x => x.MaTCCha.Equals(maTCCha))
                   join tbm in dc.TieuChiTheoBieuMaus.Where(x => x.MaBM.Equals(maBM))
                   on tc.MaTC equals tbm.MaTC
                   select tc).ToList();
        return lst;
    }

    public IList<TieuChi> LayTCTheoBMVaNTC(int maBM, int maNTC)
    {
        var lst = from tc in DanhSachTieuChi
                  join tcbm in DSTieuChiTheoBieuMau
                  on tc.MaTC equals tcbm.MaTC
                  where tcbm.MaBM.Equals(maBM)
                  where tc.MaNTC.Equals(maNTC)
                  select tc;
        var temp = lst.Count();
        return lst.ToList();
    }
    public TieuChiTheoBieuMau TimTCTBM(int maBM, int maTC)
    {
        return dc.TieuChiTheoBieuMaus.FirstOrDefault(t => t.MaBM.Equals(maBM) && t.MaTC.Equals(maTC));
    }
    public bool ThemTCTheoBM(TieuChiTheoBieuMau tctbm)
    {
        try
        {
            dc.TieuChiTheoBieuMaus.InsertOnSubmit(tctbm);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public IList<TieuChiTheoBieuMau> DSTieuChiTheoBieuMau
    {
        get
        {
            return dc.TieuChiTheoBieuMaus.ToList();
        }
    }
    public bool ThemDCTCTheoBM(List<TieuChiTheoBieuMau> dstctbm)
    {
        try
        {
            dc.TieuChiTheoBieuMaus.InsertAllOnSubmit(dstctbm);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region super admin

    public SuperAdmin TimSuperAdmin(string tenDN)
    {
        return dc.SuperAdmins.FirstOrDefault(s => s.TenDN.Equals(tenDN));
    }
    #endregion

    #region Cap Do

    public IList<CapDoTieuChi> TimCapDoTheoTC(int maTC)
    {
        var lst = from dttc in dc.CapDoTheoTieuChis.Where(t => t.MaTC.Equals(maTC)).ToList()
                  join d in dc.CapDoTieuChis
                  on dttc.MaCD equals d.MaCD
                  select new CapDoTieuChi
                  {
                      MaCD = d.MaCD,
                      Ten = d.Ten,
                      TenEN = d.TenEN,
                      GiaTri = dttc.GiaTri,
                      DonViTinh = d.DonViTinh,
                      MoTa = d.MoTa,
                      MaDV = d.MaDV,
                      Chon = dttc.Chon
                  };
        return lst.ToList();
    }

    public IList<CapDoTieuChi> LayDanhSachCapDo(int giaTri)
    {
        throw new NotImplementedException();
    }

    public IList<CapDoTieuChi> DsCapDoTieuChi
    {
        get { throw new NotImplementedException(); }
    }

    public CapDoTieuChi TimCapDo(int ma)
    {
        throw new NotImplementedException();
    }

    public bool ThemCapDo(CapDoTieuChi d)
    {
        throw new NotImplementedException();
    }

    public bool SuaCapDo(CapDoTieuChi d)
    {
        throw new NotImplementedException();
    }

    public bool XoaCapDo(CapDoTieuChi d)
    {
        throw new NotImplementedException();
    }

    public IList<CapDoTieuChi> DsCapDoTieuChiTheoMaDV(int maDV)
    {
        return dc.CapDoTieuChis.Where(c => c.MaDV.Equals(maDV)).ToList();
    }
    #endregion


    public IList<CapDoTheoTieuChi> LayDSCapDoTheoTieuChiCuaNhungTieuChiKhongCoCon(int maTC)
    {
        return dc.CapDoTheoTieuChis.Where(c => c.MaTC.Equals(maTC) 
             && c.TieuChi.MaTCCha == null).ToList();
    }


    public IList<CapDoTheoTieuChi> LayDSCapDoTheoTieuChi(int maTC)
    {
        return dc.CapDoTheoTieuChis.Where(c => c.MaTC.Equals(maTC)).ToList();
    }
}