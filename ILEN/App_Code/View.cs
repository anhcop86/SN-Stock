using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ILEN
{
    static public class View
    {
        static public void Load2DDL(IList<DonVi> lst, DropDownList ddl, bool coMucTatCa = false)
        {
            if (coMucTatCa)
            {
                lst.Insert(0, new DonVi()
                {
                    MaDV = 0,
                    TenDV = "--Tất cả--",
                    TenDVEN = "--All--"
                });
            }
            ddl.DataSource = lst;
            ddl.DataValueField = "MaDV";
            ddl.DataTextField = "TenDV";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<BoPhan> lst, DropDownList ddl, bool coMucTatCa = false)
        {
            if (coMucTatCa)
            {
                lst.Insert(0, new BoPhan()
                {
                    MaBP = 0,
                    TenBP = "--Tất cả--",
                    TenBPEN = "--All--"
                });
            }
            ddl.DataSource = lst;
            ddl.DataValueField = "MaBP";
            ddl.DataTextField = "TenBP";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<DonVi> lst, DropDownList ddl)
        {
            ddl.DataSource = lst;
            ddl.DataTextField = "TenDV";
            ddl.DataValueField = "MaDV";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<VaiTro> lst, DropDownList ddl)
        {
            ddl.DataSource = lst;
            ddl.DataTextField = "TenVT";
            ddl.DataValueField = "MaVT";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<ChuyenMon> lst, DropDownList ddl)
        {
            ddl.DataSource = lst;
            ddl.DataTextField = "TenCM";
            ddl.DataValueField = "MaCM";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<ViTriCongViec> lst, DropDownList ddl, bool coMucTatCa = false)
        {
            if (coMucTatCa)
            {
                lst.Insert(0, new ViTriCongViec()
                {
                    MaVTCV = 0,
                    TenVTCV = "--Tất cả--",
                    TenVTCVEN = "--All--"
                });
            }
            ddl.DataSource = lst;
            ddl.DataValueField = "MaVTCV";
            ddl.DataTextField = MyUtility.LaySession("en") == null ? "TenVTCV" : "TenVTCVEN";
            ddl.DataBind();
        }
        static public void Load2GV(IList<VaiTro> lst, GridView grv)
        {
            grv.DataSource = lst;
            grv.DataBind();
        }
        static public void Load2GV(IList<NhanVien> lst, GridView grv)
        {
            grv.DataSource = lst;
            grv.DataBind();
        }
        static public void Load2LV(IList<DonVi> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void Load2LV(IList<NhanVien> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void Load2LV(IList<CapDoTieuChi> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void Load2LV(IList<BoPhan> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void Load2LV(IList<ViTriCongViec> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void Load2LV(IList<ChuKyDanhGia> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }

        static public void LoadTrangThai(RadioButtonList rbl, bool en = false)
        {
            List<ListItem> lst = new List<ListItem>();
            if (en)
            {
                lst.Add(new ListItem()
                {
                    Text = "Active",
                    Value = "1",
                });
                lst.Add(new ListItem()
                {
                    Text = "Inactive",
                    Value = "0"
                });
            }
            else
            {
                lst.Add(new ListItem()
                {
                    Text = "Kích hoạt",
                    Value = "1",
                });
                lst.Add(new ListItem()
                {
                    Text = "Không kích hoạt",
                    Value = "0"
                });
            }
            rbl.RepeatDirection = RepeatDirection.Vertical;
            rbl.DataSource = lst;
            rbl.SelectedValue = "1";
            rbl.DataTextField = "Text";
            rbl.DataValueField = "Value";
            rbl.DataBind();
        }
        static public void LoadGioiTinh(RadioButtonList rbl, bool en = false)
        {
            List<ListItem> lst = new List<ListItem>();
            if (en)
            {
                lst.Add(new ListItem()
                {
                    Text = "Female",
                    Value = "0"
                });
                lst.Add(new ListItem()
                {
                    Text = "Male",
                    Value = "1",
                });
            }
            else
            {
                lst.Add(new ListItem()
                {
                    Text = "Nữ",
                    Value = "0"
                });
                lst.Add(new ListItem()
                {
                    Text = "Nam",
                    Value = "1",
                });
            }
            rbl.RepeatDirection = RepeatDirection.Vertical;
            rbl.DataSource = lst;
            rbl.SelectedValue = "1";
            rbl.DataTextField = "Text";
            rbl.DataValueField = "Value";
            rbl.DataBind();
        }

        static public void Load2GV(IList<BieuMauDanhGia> lst, ListView grv)
        {
            grv.DataSource = lst;
            grv.DataBind();
        }
        static public void LoadTo(IList<TieuChi> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void LoadTo(IList<NhomTieuChi> lst, ListView lv)
        {
            lv.DataSource = lst;
            lv.DataBind();
        }
        static public void Load2DDL(IList<ViTriCongViec> lst, DropDownList ddl)
        {
            ddl.DataSource = lst;
            ddl.DataValueField = "MaVTCV";
            ddl.DataTextField = "TenVTCV";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<ChuKyDanhGia> lst, DropDownList ddl)
        {
            ddl.DataSource = lst;
            ddl.DataValueField = "MaCK";
            ddl.DataTextField = MyUtility.TiengViet ? "TenCK" : "TenCKEN";
            ddl.DataBind();
        }
        static public void Load2DDL(IList<NhomTieuChi> lst, DropDownList ddl, bool chonTatCa = false)
        {
            List<ListItem> temp = lst.Select(l => new ListItem()
            {
                Text = l.TenNTC,
                Value = l.MaNTC.ToString()
            }).ToList();
            if (chonTatCa)
                temp.Insert(0, new ListItem()
                {
                    Text = "--Tat ca--",
                    Value = "0"//do ma NTC sinh tu dong nen gia tri 0 ko bi dung hang.
                    //sau nay neu select duoc 0 thi chinh la load tat ca.
                });
            ddl.DataSource = temp;
            ddl.DataValueField = "Value";
            ddl.DataTextField = "Text";
            ddl.DataBind();
        }
    }
}