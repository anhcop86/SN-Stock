using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MyUtility
/// </summary>
static public class MyUtility
{
    public static bool TextBoxHopLe(TextBox txt, out string resultText)
    {
        if (!string.IsNullOrEmpty(txt.Text.Trim()) && !string.IsNullOrWhiteSpace(txt.Text.Trim()))
        {
            resultText = txt.Text.Trim();
            return true;
        }
        else
        {
            resultText = string.Empty;
            return false;
        }
    }
    public static bool TextBoxHopLe(TextBox txt)
    {
        if (!string.IsNullOrEmpty(txt.Text) && !string.IsNullOrWhiteSpace(txt.Text))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    static public bool ChuoiHopLe(string chuoi)
    {
        if (chuoi == null) return false;
        return !string.IsNullOrEmpty(chuoi.Trim()) && !string.IsNullOrWhiteSpace(chuoi.Trim());
    }
    static public void LamRongControls(Control container)
    {
        foreach (Control c in container.Controls)
        {
            if (c is TextBox) (c as TextBox).Text = string.Empty;
            if (c is Image) (c as Image).ImageUrl = string.Empty;
            if (c is Label) (c as Label).Text = string.Empty;

        }
    }
    static public void LamRongTextBox(Control container)
    {
        foreach (Control c in container.Controls)
        {
            if (c is TextBox) (c as TextBox).Text = string.Empty;
        }
    }
    static public GridViewRow TimDongGV(GridView grv, object ID, string controlID)
    {
        foreach (GridViewRow dong in grv.Rows)
        {
            Control ctr = dong.FindControl(controlID);
            if (ctr != null)
                if ((ctr as LinkButton).Text.Equals(ID.ToString()))
                    return dong;
        }
        return null;
    }
    public static bool FileHinhHopLe(FileUpload fulHinh, out string exten)
    {
        if (string.IsNullOrEmpty(fulHinh.FileName) || string.IsNullOrWhiteSpace(fulHinh.FileName))
        {
            exten = string.Empty;
            return false;
        }
        int dot = fulHinh.FileName.LastIndexOf('.');
        exten = fulHinh.FileName.Substring(dot + 1);
        string[] mang = { "jpg", "png", "bmp", "jpeg" };
        return mang.Contains(exten);
    }
    public static bool CoFile(string virPath)
    {
        return System.IO.File.Exists(HttpContext.Current.Server.MapPath(virPath));
    }

    public static string ToProper(this string value)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
    }
    public static string GetNameOnly(this string fullname)
    {
        int space = fullname.IndexOf(' ');
        return fullname.Substring(space + 1);
    }

    //doc so tien:
    public static string DocSoTien(this float _number)
    {
        if (_number == 0)
            return "Không đồng.";

        string _source = String.Format("{0:0,0}", _number);

        string[] _arrsource = _source.Split(',');

        string _letter = "";

        int _numunit = _arrsource.Length;
        foreach (string _str in _arrsource)
        {
            if (ThreeNumber2Letter(_str).Length != 0)
                _letter += String.Format("{0} {1} ", ThreeNumber2Letter(_str), NumUnit(_numunit));
            _numunit--;
        }

        if (_letter.StartsWith("không trăm"))
            _letter = _letter.Substring(11, _letter.Length - 12);
        if (_letter.StartsWith("lẻ"))
            _letter = _letter.Substring(3, _letter.Length - 3);

        return String.Format("{0}{1} đồng.", _letter.Substring(0, 1).ToUpper(), _letter.Substring(1, _letter.Length - 1).Trim());
    }

    private static string ThreeNumber2Letter(string _number)
    {
        int _hunit = 0, _tunit = 0, _nunit = 0;
        if (_number.Length == 3)
        {
            _hunit = int.Parse(_number.Substring(0, 1));
            _tunit = int.Parse(_number.Substring(1, 1));
            _nunit = int.Parse(_number.Substring(2, 1));
        }
        else if (_number.Length == 2)
        {
            _tunit = int.Parse(_number.Substring(0, 1));
            _nunit = int.Parse(_number.Substring(1, 1));
        }
        else if (_number.Length == 1)
            _nunit = int.Parse(_number.Substring(0, 1));

        if (_hunit == 0 && _tunit == 0 && _nunit == 0)
            return "";

        switch (_tunit)
        {
            case 0:
                if (_nunit == 0)
                    return String.Format("{0} trăm", OneNumber2Letter(_hunit));
                else
                    return String.Format("{0} trăm lẻ {1}", OneNumber2Letter(_hunit), OneNumber2Letter(_nunit));
            case 1:
                if (_nunit == 0)
                    return String.Format("{0} trăm mười", OneNumber2Letter(_hunit));
                else if (_nunit == 5)
                    return String.Format("{0} trăm mười lăm", OneNumber2Letter(_hunit), OneNumber2Letter(_nunit));
                else
                    return String.Format("{0} trăm mười {1}", OneNumber2Letter(_hunit), OneNumber2Letter(_nunit));
            default:
                if (_nunit == 0)
                    return String.Format("{0} trăm {1} mươi", OneNumber2Letter(_hunit), OneNumber2Letter(_tunit));
                else if (_nunit == 1)
                    return String.Format("{0} trăm {1} mươi mốt", OneNumber2Letter(_hunit), OneNumber2Letter(_tunit));
                else if (_nunit == 4)
                    return String.Format("{0} trăm {1} mươi tư", OneNumber2Letter(_hunit), OneNumber2Letter(_tunit));
                else if (_nunit == 5)
                    return String.Format("{0} trăm {1} mươi lăm", OneNumber2Letter(_hunit), OneNumber2Letter(_tunit));
                else
                    return String.Format("{0} trăm {1} mươi {2}", OneNumber2Letter(_hunit), OneNumber2Letter(_tunit), OneNumber2Letter(_nunit));
        }
    }

    private static string NumUnit(int _unit)
    {
        switch (_unit)
        {
            case 0:
            case 1:
                return "";
            case 2:
                return "nghìn";
            case 3:
                return "triệu";
            case 4:
                return "tỷ";
            default:
                return String.Format("{0} {1}", NumUnit(_unit - 3), NumUnit(4));
        }
    }

    private static string OneNumber2Letter(int _number)
    {
        switch (_number)
        {
            case 0:
                return "không";
            case 1:
                return "một";
            case 2:
                return "hai";
            case 3:
                return "ba";
            case 4:
                return "bốn";
            case 5:
                return "năm";
            case 6:
                return "sáu";
            case 7:
                return "bảy";
            case 8:
                return "tám";
            case 9:
                return "chín";
            default:
                return "";
        }
    }
    public static string MyReverse(this string str)
    {
        string result = string.Empty;
        for (int i = 0; i < str.Length; i++)
            result = str[i] + result;
        return result;
    }
    public static string Currency2String(this double curr)
    {
        if (curr == default(double)) return string.Empty;
        curr = Math.Ceiling(curr / 1000) * 1000;
        string strCurr = curr.ToString().MyReverse();
        string result = string.Empty;
        while (strCurr.Length > 3)
        {
            result += strCurr.Substring(0, 3) + ".";
            strCurr = strCurr.Remove(0, 3);
        }
        result += strCurr;
        return result.MyReverse();
    }

    static public bool TryToConnectDatabase(string connectionString)
    {
        SqlConnection ketNoi = new SqlConnection(connectionString);
        try
        {
            ketNoi.Open();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            ketNoi.Close();
        }
    }
    public static string ToMD5(this string str)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        StringBuilder sbHash = new StringBuilder();
        foreach (byte b in bHash)
            sbHash.Append(String.Format("{0:x2}", b));
        return sbHash.ToString();
    }

    static public string ChuoiKetNoi
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["ILENDBConnectionString"].ConnectionString;
        }
    }

    static public bool ValidateEmail(string email)
    {
        string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
        Regex re = new Regex(emailRegex);
        return re.IsMatch(email);
    }
    static public bool Send(string to, string sub, string body, string attFile)
    {
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("customeritccorp@gmail.com");
        msg.To.Add(to);
        msg.Subject = sub;
        msg.Body = body;
        if (!string.IsNullOrEmpty(attFile) && File.Exists(attFile))
        {
            Attachment att = new Attachment(attFile);
            msg.Attachments.Add(att);
        }
        //msg.Priority = MailPriority.High;

        using (SmtpClient client = new SmtpClient())
        {
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("customeritccorp@gmail.com", "itccustomer");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    static public bool LuuHinh(FileUpload ful, string virPath)
    {
        try
        {
            var phyPath = HttpContext.Current.Server.MapPath(virPath);
            if (File.Exists(phyPath))
                File.Delete(phyPath);//xoa hinh cu neu co.
            ful.SaveAs(phyPath);
            return true;
        }
        catch
        {
            return false;
        }
    }
    static public bool XoaHinh(string virPath)
    {
        try
        {
            var phyPath = HttpContext.Current.Server.MapPath(virPath);
            File.Delete(phyPath);
            return true;
        }
        catch
        {
            return false;
        }
    }
    static public void XoaThongBao(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is Label)
            {
                ((Label)c).Text = string.Empty;
            }
            XoaThongBao(c);
        }
    }
    static public string LayQS(string queryName)
    {
        return HttpContext.Current.Request.QueryString[queryName];
    }
    static public object LaySession(string sessionName)
    {
        return HttpContext.Current.Session[sessionName];
    }
    static public bool MatKhauTrungKhop(string mk, string mkRetype)
    {
        return string.Compare(mk, mkRetype) == 0;
    }
   
    static public string LayQueryString(string queryName)
    {
        return HttpContext.Current.Request.QueryString[queryName];
    }
    static public bool TiengViet
    {
        get
        {
            return HttpContext.Current.Session["en"] == null;
        }
    }
    static public bool NgayHopLe(string dateString, out DateTime result)
    {
        result = new DateTime();
        if (!ChuoiHopLe(dateString)) return false;
        if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            return true;
        }
        return false;
    }
}