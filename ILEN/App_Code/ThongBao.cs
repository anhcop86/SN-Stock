using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class ThongBao
{
    static public bool VN
    {
        get
        {
            return HttpContext.Current.Session["en"] == null;
        }
    }
    static public string BatBuoc
    {
        get
        {
            return VN ? "Không được bỏ trống" : "Must not be empty";
        }
    }
    static public string NgayKhongDung
    {
        get
        {
            return VN ? "Ngày không đúng định dạng" : "Date is invalid format";
        }
    }
    static public string ThemKhongThanhCong
    {
        get
        {
            return VN ? "Thêm mới không thành công" : "Insert failed";
        }
    }
    static public string SuaKhongThanhCong
    {
        get
        {
            return VN ? "Sửa không thành công" : "Update failed";
        }
    }
    static public string XoaKhongThanhCong
    {
        get
        {
            return VN ? "Xóa không thành công" : "Delete failed";
        }
    }
    static public string KhongTonTai
    {
        get
        {
            return VN ? "Không tồn tại đối tượng này" : "Not exists";
        }
    }
    static public string KhongHopLe
    {
        get
        {
            return VN ? "Thông tin không hợp lệ" : "Invalid information";
        }
    }
    static public string ChonDonVi
    {
        get
        {
            return VN ? "Phải chọn đơn vị" : "Please choose company";
        }
    }
    static public string Chon
    {
        get
        {
            return VN ? "Hãy chọn đối tượng" : "Please select object";
        }
    }
    static public string ChonDoiTuongCapNhat
    {
        get
        {
            return VN ? "Chưa chọn đối tượng cập nhật" : "Please select object to update";
        }
    }
    static public string ChonFileHinh
    {
        get
        {
            return VN ? "Hãy chọn loại file hình ảnh" : "Please select image";
        }
    }
    static public string LoiLuuHinh
    {
        get
        {
            return VN ? "Lỗi lưu file hình ảnh" : "Error when save image file";
        }
    }
    static public string HinhKhongHoLe
    {
        get
        {
            return VN ? "File logo không hơp lệ" : "You must upload image file.";
        }
    }
    static public string ThanhCong
    {
        get
        {
            return VN ? "Thao tác thành công" : "Successfully.";
        }
    }
    static public string MatKhauKhongTrung
    {
        get
        {
            return VN ? "Mật khẩu không trùng khớp" : "Password is not match.";
        }
    }
    static public string HetSoLuong
    {
        get
        {
            return VN ? "Hết số tài khoản" : "Run out of account number.";
        }
    }
    static public string DangSuDung
    {
        get
        {
            return VN ? "Đối tượng đang được sử dụng, không thể xóa." : "In using, cannot delete.";
        }
    }
    static public string SaiTenDangNhap
    {
        get
        {
            return VN ? "Sai tên đăng nhập." : "Invalid username.";
        }
    }
    static public string SaiTenMatKhau
    {
        get
        {
            return VN ? "Sai mật khẩu." : "Invalid password.";
        }
    }
}