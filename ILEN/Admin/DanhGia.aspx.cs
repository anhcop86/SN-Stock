using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DanhGia : TrangKho
{
    #region ho tro sinh du lieu mau

    void TaoDuLieuTieuChiTheoBieuMau()
    {
        int maBM = 1;
        bool trangThai = true;

        using (DBDataContext dc = new DBDataContext(MyUtility.ChuoiKetNoi))
        {
            foreach (TieuChi tc in kho.DanhSachTieuChi)
            {
                TieuChiTheoBieuMau x = new TieuChiTheoBieuMau()
                {
                    MaBM = maBM,
                    TrongSo = tc.TrongSo,
                    Chon = trangThai,
                    MaTC = tc.MaTC
                };
                dc.TieuChiTheoBieuMaus.InsertOnSubmit(x);
            }
            dc.SubmitChanges();
        }
    }

    void TaoDuLieuCapDoTC()
    {
        using (DBDataContext dc = new DBDataContext(MyUtility.ChuoiKetNoi))
        {
            //chi add muc tieu chi cho nhung tieu chi con.
            var cap1 = new CapDoTieuChi()
            {
                MaDV = 1,
                GiaTri = 0.25,
                Ten = "muc 0",
                TenEN = "level 0",
                Chon = true,
            };
            var cap2 = new CapDoTieuChi()
            {
                MaDV = 1,
                GiaTri = 0.5,
                Ten = "muc 1",
                TenEN = "level 1",
                Chon = true,
            };

            var cap3 = new CapDoTieuChi()
            {
                MaDV = 1,
                GiaTri = 0.75,
                Ten = "muc 2",
                TenEN = "level 3",
                Chon = true,
            };

            var cap4 = new CapDoTieuChi()
            {
                MaDV = 1,
                GiaTri = 1,
                Ten = "muc 3",
                TenEN = "level 3",
                Chon = true,
            };


            dc.CapDoTieuChis.InsertOnSubmit(cap1);
            dc.CapDoTieuChis.InsertOnSubmit(cap2);
            dc.CapDoTieuChis.InsertOnSubmit(cap3);
            dc.CapDoTieuChis.InsertOnSubmit(cap4);
            dc.SubmitChanges();
        }
    }

    void TaoDuLieuCapDoTheoTC()
    {
        using (DBDataContext dc = new DBDataContext(MyUtility.ChuoiKetNoi))
        {
            List<CapDoTheoTieuChi> lst = new List<CapDoTheoTieuChi>();
            foreach (var i in dc.CapDoTieuChis)
            {
                foreach (var j in dc.TieuChis.Where(t => t.TieuChis.Count == 0))//ko co tieu chi con.
                {
                    CapDoTheoTieuChi x = new CapDoTheoTieuChi()
                    {
                        MaCD = i.MaCD,
                        MaTC = j.MaTC,
                        Chon = true,
                        GiaTri = i.GiaTri,
                    };
                    lst.Add(x);
                }
            }
            dc.CapDoTheoTieuChis.InsertAllOnSubmit(lst);
            dc.SubmitChanges();
        }
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.LoadNhanVien();
        #region enable linkbutton luu
        int maCK = 0;
        int.TryParse(ddlChuKyDG.SelectedValue, out maCK);
        BieuMauDanhGia bm = kho.TimBieuMau(maCK, this.NhanVien.MaVTCV);
        if (bm != default(BieuMauDanhGia))
        {//hay viet lai cho phu hop voi CSDL moi.
            //var kqDG = kho.TimKQDG(this.NhanVien.MaNV,
            //    ddlNhanSu.SelectedValue, bm.MaBM);
            //var status = (kqDG == default(KetQuaDanhGia));
            //lbtnLuu.Visible = status;
        }
        #endregion
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ILEN.View.Load2DDL(kho.LayDanhSachChuKyDanhGiaTheoDV(this.MaDV), ddlChuKyDG);
            int maCK = 0;
            int.TryParse(ddlChuKyDG.SelectedValue, out maCK);
            this.LoadBieuMau(this.NhanVien.MaNV, maCK);
        }
    }

    private void LoadBieuMau(string maNV, int maCK)
    {//load cac tieu chi cua bieu mau phu hop voi chu ky va nhan vien => listview:
        //load nhom tieu chi:
        lvNTC.DataSource = kho.TimNhomTieuChiTheoMaDV(this.MaDV);
        lvNTC.DataBind();

        BieuMauDanhGia bm = kho.TimBieuMau(maCK, this.NhanVien.MaVTCV);
        divTenBM.InnerHtml = "Biểu mẫu đánh giá: " + (bm == null ? string.Empty : bm.TenBM);

        if (bm != default(BieuMauDanhGia))
        {
            //load tieu chi tuong ung theo nhom:
            foreach (ListViewDataItem item in lvNTC.Items)
            {
                HiddenField hdf = item.FindControl("hdfMaNTC") as HiddenField;
                int maNTC = 0;
                int.TryParse(hdf.Value, out maNTC);

                ListView lvTC = item.FindControl("lvTC") as ListView;

                lvTC.DataSource = kho.LayTCTheoBMVaNTC(bm.MaBM, maNTC).Where(t => t.MaTCCha == null).ToList();
                lvTC.DataBind();

                foreach (ListViewDataItem i in lvTC.Items)//voi moi tieu chi cha.
                {
                    HiddenField hdfTC = i.FindControl("hdfMaTC") as HiddenField;
                    int maTC = int.Parse(hdfTC.Value);

                    //voi moi tieu chi, load danh sach cac muc tieu chi tuong ung:
                    RadioButtonList rbl = i.FindControl("rblMucTC") as RadioButtonList;

                    List<CapDoTheoTieuChi> dsCapDoTheoTC = kho.LayDSCapDoTheoTieuChi(maTC).ToList();
                    if (dsCapDoTheoTC.Count > 0)
                    {
                        rbl.DataSource = dsCapDoTheoTC;
                        rbl.DataTextField = MyUtility.TiengViet ? "Ten" : "TenEN";
                        rbl.DataValueField = "Ma";
                        rbl.DataBind();
                        //debug cho nhanh:
                        rbl.Items[0].Selected = true;
                    }
                    //voi moi tieu chi cha, load danh sach cac tieu chi con tuong ung:
                    ListView lvTCCon = i.FindControl("lvTCCon") as ListView;
                    lvTCCon.DataSource = kho.LayTCTheoBMVaTCCha(bm.MaBM, maTC);
                    lvTCCon.DataBind();
                    foreach (ListViewDataItem j in lvTCCon.Items)
                    {
                        //voi moi tieu chi con, load danh sach cac muc tieu chi tuong ung:
                        HiddenField subHDFTC = j.FindControl("hdfMaTC") as HiddenField;
                        int ma = int.Parse(subHDFTC.Value);
                        RadioButtonList rblCon = j.FindControl("rblMucTC") as RadioButtonList;
                        List<CapDoTheoTieuChi> lst = kho.LayDSCapDoTheoTieuChi(ma).ToList();
                        if (lst.Count > 0)
                        {
                            rblCon.DataSource = lst;
                            rblCon.DataTextField = MyUtility.TiengViet ? "Ten" : "TenEN";
                            rblCon.DataValueField = "Ma";
                            rblCon.DataBind();
                            //debug cho nhanh:
                            rblCon.Items[0].Selected = true;
                        }
                    }
                }
            }
        }
    }

    private void LoadNhanVien()
    {
        ddlNhanSu.Items.Clear();
        foreach (var i in kho.LayDanhSachNhanVienTheoDonViKemBoPhan(this.MaDV).Where(n => !n.Value.Equals(this.NhanVien.MaNV)))
            ddlNhanSu.Items.Add(i);
        if (ViewState["NhanVien"] != null)
            ddlNhanSu.SelectedValue = ViewState["NhanVien"].ToString();
    }

    protected void ddlChuKyDG_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["NhanVien"] = ddlNhanSu.SelectedValue;
        this.LoadBieuMau(this.NhanVien.MaNV, int.Parse(ddlChuKyDG.SelectedValue));
    }

    protected void btnLuu_Click(object sender, EventArgs e)
    {//cần kiểm tra lại cho phù hợp với CSDL mới.
        int maCK = int.Parse(ddlChuKyDG.SelectedValue);

        BieuMauDanhGia bm = kho.TimBieuMau(maCK, this.NhanVien.MaVTCV);
        if (bm == default(BieuMauDanhGia)) return;
        //luu ket qua danh gia:

        KetQuaDanhGia kq = new KetQuaDanhGia()
        {
            MaBM = bm.MaBM,
            NgayDG = DateTime.Today,
            NguoiDG = this.NhanVien.MaNV,
            NguoiDuocDG = ddlNhanSu.SelectedValue,
            HienThi = true,
            CoHieuLuc = true
        };
        if (!kho.ThemKetQuaDanhGia(kq) || !kho.Luu())
        {
            lblThongBao.Text = ThongBao.ThemKhongThanhCong;
            return;
        }

        //luu chi tiet ket qua danh gia:
        List<ChiTietDanhGia> lstCTDG = new List<ChiTietDanhGia>();
        #region lap theo nhom tieu chi
        //foreach (ListViewDataItem item in lvNTC.Items)
        //{
        //    ListView lvTCCha = item.FindControl("lvTC") as ListView;

        //    foreach (ListViewDataItem i in lvTCCha.Items)
        //    {
        //        RadioButtonList rbl = i.FindControl("rblMucTC") as RadioButtonList;
        //        HiddenField hdfCha = i.FindControl("hdfMaTC") as HiddenField;
        //        if (rbl.Items.Count > 0)
        //        {
        //            MucTrongTieuChi muc = kho.TimMucTrongTC(int.Parse(rbl.SelectedValue));
        //            TieuChiTheoBieuMau tctbm = kho.TimTCTBM(bm.MaBM, int.Parse(hdfCha.Value));
        //            ChiTietDanhGia ct = new ChiTietDanhGia()
        //            {
        //                MaKQDG = kq.MaKQDG,
        //                MaMucTC = muc.MaMuc,
        //                MaTCTheoBM = tctbm.Ma,
        //                TongDiem = muc.GiaTri * tctbm.TrongSo,
        //                MaTC = tctbm.MaTC,
        //                MaTCCha = tctbm.TieuChi.MaTCCha,
        //                MaNhomTC = tctbm.TieuChi.MaNTC
        //            };
        //            lstCTDG.Add(ct);
        //        }
        //        //tim listview tieu chi con:
        //        ListView lvTCCon = i.FindControl("lvTCCon") as ListView;
        //        foreach (ListViewDataItem j in lvTCCon.Items)
        //        {
        //            RadioButtonList rblCon = j.FindControl("rblMucTC") as RadioButtonList;
        //            HiddenField hdfCon = j.FindControl("hdfMaTC") as HiddenField;
        //            if (rblCon.Items.Count > 0)
        //            {
        //                MucTrongTieuChi muc = kho.TimMucTrongTC(int.Parse(rblCon.SelectedValue));
        //                TieuChiTheoBieuMau tctbm = kho.TimTCTBM(bm.MaBM, int.Parse(hdfCon.Value));
        //                ChiTietDanhGia ctCon = new ChiTietDanhGia()
        //                {
        //                    MaKQDG = kq.MaKQDG,
        //                    MaMucTC = muc.MaMuc,
        //                    MaTCTheoBM = tctbm.Ma,
        //                    TongDiem = muc.GiaTri * tctbm.TrongSo,
        //                    MaTC = tctbm.MaTC,
        //                    MaTCCha = tctbm.TieuChi.MaTCCha,
        //                    MaNhomTC = tctbm.TieuChi.MaNTC
        //                };
        //                lstCTDG.Add(ctCon);
        //            }
        //        }

        //    }

        //}
        #endregion

        //if (!kho.ThemChiTietDanhGia(lstCTDG) || !kho.Luu())
        //{
        //    lblThongBao.Text = ThongBao.ThemKhongThanhCong;
        //    return;
        //}
        ////voi moi tieu chi con trong ChiTietDanhGia, insert 1 record cho tieu chi cha:
        //lstCTDG = lstCTDG.Where(x => x.MaTCCha != null).ToList();
        //var temp = lstCTDG.GroupBy(x => new { x.MaKQDG, x.MaTCCha, x.MaNhomTC }).Select(x => new
        //{
        //    MaTC = x.Key.MaTCCha,
        //    MaKQDG = x.Key.MaKQDG,
        //    MaNhomTC = x.Key.MaNhomTC,
        //    TongDiem = x.Sum(t => t.TongDiem),
        //    MaTCTheoBM = kho.TimTCTBM(bm.MaBM, (int)(x.Key.MaTCCha)).Ma
        //});

        //List<ChiTietDanhGia> dsTCCha = new List<ChiTietDanhGia>();
        //foreach (var item in temp)
        //    dsTCCha.Add(new ChiTietDanhGia()
        //    {
        //        MaTCTheoBM = item.MaTCTheoBM,
        //        MaMucTC = null,
        //        MaKQDG = item.MaKQDG,
        //        TongDiem = item.TongDiem * kho.TimTieuChi((int)item.MaTC).TrongSo / 100,
        //        MaTC = (int)item.MaTC,
        //        MaTCCha = null,
        //        MaNhomTC = item.MaNhomTC
        //    });
        //if (!kho.ThemChiTietDanhGia(dsTCCha) || !kho.Luu())
        //{
        //    lblThongBao.Text = ThongBao.ThemKhongThanhCong;
        //    return;
        //}
        //lblThongBao.Text = ThongBao.ThanhCong;
    }
}