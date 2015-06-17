using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;



public interface IKho
{

    bool Luu();

    #region don vi
    IList<DonVi> DanhSachDonVi { get; }
    DonVi TimDonVi(int ma);
    bool ThemDonVi(DonVi dv);
    bool SuaDonVi(DonVi dv);
    bool XoaDonVi(DonVi dv);
    #endregion

    #region nhan vien
    IList<NhanVien> DanhSachNhanVien { get; }
    IList<NhanVien> LayDanhSachNhanVienTheoBoPhan(int maBP);
    IList<NhanVien> LayDanhSachNhanVienTheoDonVi(int maDV);
    IList<ListItem> LayDanhSachNhanVienTheoDonViKemBoPhan(int maDV);
    IList<NhanVien> LayDanhSachNhanVien(string maNV, string hoTen, int maDV, int maBP);
    NhanVien TimNhanVien(string ma);
    bool ThemNhanVien(NhanVien nv);
    bool SuaNhanVien(NhanVien nv);
    bool XoaNhanVien(NhanVien nv);
    string SinhMaNhanVien();
    string SinhMatKhauMacDinhChoNV();
    #endregion

    #region bo phan
    IList<BoPhan> DanhSachBoPhan { get; }
    IList<BoPhan> LayDanhSachBoPhanTheoDonVi(int maDV);
    IList<BoPhan> LayDanhSachBoPhanTheoDonVi(int maDV, string tenBP);
    BoPhan TimBoPhan(int ma);
    bool ThemBoPhan(BoPhan bp);
    bool SuaBoPhan(BoPhan bp);
    bool XoaBoPhan(BoPhan bp);
    #endregion

    #region Cap Do
    IList<CapDoTieuChi> LayDanhSachCapDo(int giaTri);
    IList<CapDoTieuChi> DsCapDoTieuChi { get; }
    CapDoTieuChi TimCapDo(int ma);
    bool ThemCapDo(CapDoTieuChi d);
    bool SuaCapDo(CapDoTieuChi d);
    bool XoaCapDo(CapDoTieuChi d);
    IList<CapDoTieuChi> TimCapDoTheoTC(int maTC);

    #endregion

    #region tieu chi


    IList<TieuChi> DanhSachTieuChi { get; }
    TieuChi TimTieuChi(int ma);
    IList<TieuChi> DanhSachTCTheoDV(int maDV);
    IList<TieuChi> DanhSachTCTheoBM(int maBM);
    IList<TieuChi> DanhSachTCTheoNTC(int maNTC);
    bool ThemTieuChi(TieuChi tc);
    bool SuaTieuChi(TieuChi tc);
    bool XoaTieuChi(TieuChi tc);
    #endregion

    #region Cap Do theo tieu chi
    IList<CapDoTheoTieuChi> LayDSCapDoTheoTieuChiCuaNhungTieuChiKhongCoCon(int maTC);
    IList<CapDoTheoTieuChi> LayDSCapDoTheoTieuChi(int maTC);
    IList<CapDoTheoTieuChi> DanhSachCapDoTheoTieuChi { get; }
    CapDoTheoTieuChi TimCapDoTheoTieuChi(int ma);
    CapDoTheoTieuChi TimCapDoTheoTieuChi1(int maTC, int maCD);
    bool ThemCapDoTheoTieuChi(CapDoTheoTieuChi dv);
    bool ThemDSCDTheoTC(List<CapDoTheoTieuChi> dscdttc);
    bool SuaCapDoTheoTieuChi(CapDoTheoTieuChi dv);
    bool XoaCapDoTheoTieuChi(CapDoTheoTieuChi dv);
    IList<CapDoTieuChi> DsCapDoTieuChiTheoMaDV(int maDV);
    #endregion

    #region nhom tieu chi
    IList<NhomTieuChi> LayDanhSachNTC(string ten);
    IList<NhomTieuChi> DanhSachNhomTieuChi { get; }
    NhomTieuChi TimNhomTieuChi(int ma);
    IList<NhomTieuChi> TimNhomTieuChiTheoMaDV(int maDV);
    IList<NhomTieuChi> LayNhomTCTheoMaBM(int maBM);
    bool ThemNhomTieuChi(NhomTieuChi dv);
    bool SuaNhomTieuChi(NhomTieuChi dv);
    bool XoaNhomTieuChi(NhomTieuChi dv);
    #endregion

    #region ket qua danh gia
    IList<KetQuaDanhGia> DanhSachKetQuaDanhGia { get; }
    KetQuaDanhGia TimKetQuaDanhGia(int ma);
    bool ThemKetQuaDanhGia(KetQuaDanhGia dv);
    bool SuaKetQuaDanhGia(KetQuaDanhGia dv);
    bool XoaKetQuaDanhGia(KetQuaDanhGia dv);
    #endregion

    #region chi tiet danh gia
    IList<ChiTietDanhGia> DanhSachChiTietDanhGia { get; }
    ChiTietDanhGia TimChiTietDanhGia(int ma);
    bool ThemChiTietDanhGia(ChiTietDanhGia dv);
    bool SuaChiTietDanhGia(ChiTietDanhGia dv);
    bool XoaChiTietDanhGia(ChiTietDanhGia dv);
    #endregion

    #region chuyen mon
    IList<ChuyenMon> LayDanhSachChuyenMon(string ten);
    IList<ChuyenMon> LayDanhSachChuyenMon(int maDV);
    IList<ChuyenMon> DanhSachChuyenMon { get; }
    ChuyenMon TimChuyenMon(int ma);
    bool ThemChuyenMon(ChuyenMon dv);
    bool SuaChuyenMon(ChuyenMon dv);
    bool XoaChuyenMon(ChuyenMon dv);
    #endregion

    #region ngoai ngu
    IList<NgoaiNgu> DanhSachNgoaiNgu { get; }
    IList<NgoaiNgu> LayDanhSachNgoaiNgu(string ten);
    NgoaiNgu TimNgoaiNgu(int ma);
    bool ThemNgoaiNgu(NgoaiNgu dv);
    bool SuaNgoaiNgu(NgoaiNgu dv);
    bool XoaNgoaiNgu(NgoaiNgu dv);
    #endregion

    #region ngoai ngu cua nhan vien
    IList<NgoaiNguCuaNhanVien> DanhSachNgoaiNguCuaNhanVien { get; }
    NgoaiNguCuaNhanVien TimNgoaiNguCuaNhanVien(int ma);
    bool ThemNgoaiNguCuaNhanVien(NgoaiNguCuaNhanVien dv);
    bool SuaNgoaiNguCuaNhanVien(NgoaiNguCuaNhanVien dv);
    bool XoaNgoaiNguCuaNhanVien(NgoaiNguCuaNhanVien dv);
    #endregion

    #region danh muc quan tri
    IList<DanhMucQuanTri> DanhSachDanhMucQuanTri { get; }
    DanhMucQuanTri TimDanhMucQuanTri(string ma);
    bool ThemDanhMucQuanTri(DanhMucQuanTri dm);
    bool SuaDanhMucQuanTri(DanhMucQuanTri dm);
    bool XoaDanhMucQuanTri(DanhMucQuanTri dm);
    List<string> LayCacTrangAdmin();
    bool SuaDanhMucQuanTri(string maDM, string tenDM);
    #endregion

    #region vai tro
    string LayTenDanhMucQuanTri(string rawURL);

    IList<VaiTro> LayDanhSachVaiTro(string ten);
    IList<VaiTro> LayDanhSachVaiTro(int maDV);
    IList<VaiTro> DanhSachVaiTro { get; }
    VaiTro TimVaiTro(int ma);
    bool ThemVaiTro(VaiTro vt);
    bool SuaVaiTro(VaiTro vt);
    bool XoaVaiTro(VaiTro vt);
    #endregion

    #region quyen
    IList<Quyen> DanhSachQuyen { get; }
    Quyen LayQuyen(int maVT, string maDMQT);
    Quyen TimQuyen(int ma);
    bool ThemQuyen(Quyen q);
    bool SuaQuyen(Quyen q);
    bool XoaQuyen(Quyen q);
    #endregion

    #region danh muc hien thi
    IList<DanhMucHienThi> DanhSachDanhMucHienThi { get; }

    DanhMucHienThi TimDanhMucHienThi(string ma);
    bool ThemDanhMucHienThi(DanhMucHienThi dm);
    bool SuaDanhMucHienThi(DanhMucHienThi dm);
    bool XoaDanhMucHienThi(DanhMucHienThi dm);
    #endregion

    #region admin don vi
    IList<AdminDV> DanhSachAdminDV { get; }
    AdminDV TimAdminDV(int ma);
    bool ThemAdminDV(AdminDV ad);
    bool SuaAdminDV(AdminDV ad);
    bool XoaAdminDV(AdminDV ad);
    #endregion

    #region vi tri cong viec
    IList<ViTriCongViec> DanhSachViTriCongViec { get; }
    IList<ViTriCongViec> LayDanhSachViTriCongViecTheoBP(int maBP);
    IList<ViTriCongViec> LayDanhSachViTriCongViecTheoDV(int maDV);

    ViTriCongViec TimViTriCongViec(int ma);
    bool ThemViTriCongViec(ViTriCongViec vtcv);
    bool SuaViTriCongViec(ViTriCongViec vtcv);
    bool XoaViTriCongViec(ViTriCongViec vtcv);
    #endregion

    #region chu ky danh gia
    IList<ChuKyDanhGia> LayDanhSachChuKyDanhGiaTheoDV(int maDV);

    ChuKyDanhGia TimChuKyDanhGia(int maCK);
    bool ThemChuKyDanhGia(ChuKyDanhGia ck);
    bool SuaChuKyDanhGia(ChuKyDanhGia ck);
    bool XoaChuKyDanhGia(ChuKyDanhGia ck);
    #endregion

    #region Bieu mau

    IList<BieuMauDanhGia> DanhSachBieuMau { get; }
    BieuMauDanhGia TimBieuMau(int maCK, int maVTCV);
    BieuMauDanhGia TimBieuMau(int maBM);
    IList<BieuMauDanhGia> TimBieuMauTheoDV(int maDV);
    bool luuBMDG(BieuMauDanhGia bmdg);
    #endregion

    #region Vi Tri Cong Viec
    IList<ViTriCongViec> DanhSachVTCV { get; }
    IList<ViTriCongViec> DanhSachVTCVTheoDV(int maDV);
    #endregion

    #region Tieu Chi Theo Bieu mau

    IList<TieuChi> LayTCTheoBMVaTCCha(int maBM, int maTCCha);

    bool ThemTCTheoBM(TieuChiTheoBieuMau tctbm);
    bool ThemDCTCTheoBM(List<TieuChiTheoBieuMau> dstctbm);
    IList<TieuChiTheoBieuMau> DSTieuChiTheoBieuMau { get; }
    IList<TieuChi> LayTCTheoBMVaNTC(int maBM, int maNTC);

    TieuChiTheoBieuMau TimTCTBM(int maBM, int maTC);
    #endregion

    #region super admin
    SuperAdmin TimSuperAdmin(string tenDN);
    #endregion

}