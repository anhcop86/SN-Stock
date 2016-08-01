using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;

namespace PhimHang.Models
{
    public static class AppHelper
    {
        private static testEntities db;
        private const string _imageURLAvataDefault = "/img/avatar2.jpg";
        private const string _imageURLAvata = "/images/avatar/";
        private const string _imageURLCoverDefault = "/img/cover_default.jpg";
        private const string _imageURLCover = "/images/cover/";
        /// <summary>
        /// Lọc từ khóa bậy được lưu trong db
        /// </summary>
        /// <param name="messege"></param>
        /// <returns></returns> String with clean messege
        public static string FilteringWord(string messege)
        {
            //get list keyword
            FilterKeyworkSingleton instance = FilterKeyworkSingleton.Instance;
            List<PosistionFilter> listPosistionFilter = new List<PosistionFilter>();
            var stringOriginal = new StringBuilder(messege);
            string messegeNonUnicode = ConvertToNonUnicode(messege).ToLower();
            var keywordList = instance.getListKeyWord();
            foreach (var item in keywordList)
            {
                int index = messegeNonUnicode.IndexOf(item, 0);
                while (index != -1)
                {
                    listPosistionFilter.Add(new PosistionFilter { Index = index, Posistion = item.Length });
                    index = messegeNonUnicode.IndexOf(item, index + item.Length);
                }
            }
            foreach (var item in listPosistionFilter)
            {
                stringOriginal.Remove(item.Index, item.Posistion);
                stringOriginal.Insert(item.Index, "*", item.Posistion);
            }

            return stringOriginal.ToString();
        }
        /// <summary>
        /// Convert a string Unicode to Non Unicode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToNonUnicode(string str)
        {
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;

        }
        private static readonly string[] VietNamChar = new string[]
        {
             "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũứừựửữư",
            "ÚÙỤỦŨỨỪỰỬỮƯ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        /// <summary>
        /// Get domain name from Web.config
        /// </summary>
        public static string AbsolutePathHostName
        {
            get
            {
                return ConfigurationManager.AppSettings["AbsolutePathHostName"].ToString();
            }
        }
        public static string TinyURL
        {
            get
            {
                return ConfigurationManager.AppSettings["TinyURL"].ToString();
            }
        }

        public static string GetDomain(string domainFull)
        {
            return new System.Uri(domainFull).Host;
        }

        public static List<string> GetListHotStock()
        {
            #region WebCache HotPrice
            List<string> result = WebCache.Get("HotPrice") as List<string>;
            if (result == null)
            {
                using (db = new testEntities())
                {
                    var resultCache = (from hoststock in db.TickerHots // danh sach co phieu nong trong db
                                       select hoststock.THName).ToListAsync();
                    result = resultCache.Result;
                    WebCache.Set("HotPrice", result, 30, true);
                }

            }
            return result;

            #endregion

        }

        /// <summary>
        /// relace a string HTML to non HTML
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        public static string Encode(string password)
        {
            return Crypto.HashPassword(password);
        }
        public static bool CheckTimeUpdatePrice()
        {
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 15 && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool sendEmail(string subject, String toEmail, string BodyContent)
        {
            MailMessage message = new MailMessage();
            MailAddress sender = new MailAddress(ApplicationEmailAddress, DisplayEmailReset);
            MailAddress receiver = new MailAddress(toEmail);
            message.From = sender;
            message.Sender = sender;
            message.To.Add(receiver);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = BodyContent;
            message.Priority = MailPriority.High;
            //config email
            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = SmtpPort;
            smtpClient.Host = SmtpServer;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = EnableSsl;

            smtpClient.Credentials = new NetworkCredential(ApplicationEmailAddress, ApplicationEmailPassword);
            //
            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static string GetContentTemplate(string fileName, string token, string userName)
        {
            NameValueCollection parameters = new NameValueCollection();
            StreamReader reader = null;

            reader = new StreamReader(fileName, System.Text.UTF8Encoding.UTF8);
            string bodyText = reader.ReadToEnd();
            reader.Close();

            parameters.Add("tokenReset", token);
            parameters.Add("username", userName);
            parameters.Add("AbsolutePathHostName", AppHelper.AbsolutePathHostName);

            for (int i = 0; i < parameters.Keys.Count; i++)
            {
                string replaceKey = "${" + parameters.GetKey(i) + "}$";
                bodyText = bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
            }
            return bodyText;

        }
        public static string GetContentTemplate(string fileName, string userName)
        {
            NameValueCollection parameters = new NameValueCollection();
            StreamReader reader = null;

            reader = new StreamReader(fileName, System.Text.UTF8Encoding.UTF8);
            string bodyText = reader.ReadToEnd();
            reader.Close();

            parameters.Add("username", userName);
            parameters.Add("AbsolutePathHostName", AppHelper.AbsolutePathHostName);

            for (int i = 0; i < parameters.Keys.Count; i++)
            {
                string replaceKey = "${" + parameters.GetKey(i) + "}$";
                bodyText = bodyText.Replace(replaceKey, String.Concat(parameters.GetValues(i)));
            }
            return bodyText;

        }
        public static string ResetPasswordEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["ResetPasswordEmailTemplatePath"];
            }
        }
        public static System.Drawing.Image CropImage(System.Drawing.Image Image, int Height, int Width, int StartAtX, int StartAtY)
        {
            Image outimage;
            MemoryStream mm = null;
            try
            {
                //check the image height against our desired image height
                if (Image.Height < Height)
                {
                    Height = Image.Height;
                }

                if (Image.Width < Width)
                {
                    Width = Image.Width;
                }

                //create a bitmap window for cropping
                Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(100, 100);

                //create a new graphics object from our image and set properties
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //now do the crop
                grPhoto.DrawImage(Image, new Rectangle(0, 0, Width, Height), StartAtX, StartAtY, Width, Height, GraphicsUnit.Pixel);

                // Save out to memory and get an image from it to send back out the method.
                mm = new MemoryStream();
                bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
                Image.Dispose();
                bmPhoto.Dispose();
                grPhoto.Dispose();
                outimage = Image.FromStream(mm);

                return outimage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error cropping image, the error was: " + ex.Message);
            }
        }
        /// <summary>
        /// Syn avata from facebook site to phochungkhoan site
        /// </summary>
        /// <param name="idFacebook"></param>
        /// <returns></returns>
        public async static Task<bool> AvatarSyn(string idFacebook)
        {
            using (db = new testEntities())
            {
                var getUserFacebook = await db.UserLogins.FirstOrDefaultAsync(u => u.IdFacebook == idFacebook);
                // facebok url to get  avartar of user 
                //https://graph.facebook.com/v2.3/1435015210144873/picture?type=large
                string urlFacebookAvatar = "https://graph.facebook.com/v2.3/" + idFacebook + "/picture?type=large";
                WebClient webClient = new WebClient();
                var uploadDir = "~/" + ImageURLAvata;
                #region delete old avata image

                string fullPath = HttpContext.Current.Server.MapPath(uploadDir) + getUserFacebook.AvataImage;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                #endregion
                string NameFiletimeupload = getUserFacebook.KeyLogin + DateTime.Now.ToString("HHmmss") + "_avata";

                //string JsonResult = webClient.DownloadString(urlFacebookAvatar);
                //JObject jsonUserInfo = JObject.Parse(JsonResult);
                //var urlData = jsonUserInfo.Value<dynamic>("data");
                //string urlAvarta = urlData.Value<string>("url");

                //Uri myURI4 = new Uri(urlAvarta);
                //var fi = new FileInfo(myURI4.AbsolutePath);
                //var ext = fi.Extension;

                var imagePath = Path.Combine(HttpContext.Current.Server.MapPath(uploadDir), NameFiletimeupload + ".jpg");


                try // download and save facebook image of user into database
                {
                    webClient.DownloadFile(urlFacebookAvatar, imagePath);
                    getUserFacebook.AvataImage = NameFiletimeupload + ".jpg";
                    db.Entry(getUserFacebook).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {


                }

                return true;
            }

        }
        /// <summary>
        /// set cookie when login by facebook
        /// </summary>
        public static void SetCookieOfFace()
        {
            var FacebookLogin = new HttpCookie("FacebookLogin", "F");
            FacebookLogin.Expires.AddDays(2);
            if (FacebookLogin == null)
            {
                HttpContext.Current.Response.Cookies.Add(FacebookLogin);
            }
            else
            {
                HttpContext.Current.Response.SetCookie(FacebookLogin);
            }

        }
        /// <summary>
        /// release cookie when logout facebook
        /// </summary>
        public static void ReleaseCookieOfFace()
        {
            if (HttpContext.Current.Request.Cookies["FacebookLogin"] != null)
            {
                var user = new HttpCookie("FacebookLogin")
                {
                    Expires = DateTime.Now.AddDays(-2),
                    Value = null
                };
                HttpContext.Current.Response.Cookies.Add(user);
            }
        }

        public static string ApplicationEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationEmailAddress"].ToString();
            }
        }
        public static string ApplicationEmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationEmailPassword"].ToString();
            }
        }

        public static string SmtpServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpServer"].ToString();
            }
        }
        public static int SmtpPort
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["SmtpPort"].ToString());
            }
        }
        public static bool EnableSsl
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["EnableSsl"].ToString());
            }
        }

        public static string DisplayEmailReset
        {
            get
            {
                return ConfigurationManager.AppSettings["DisplayEmailReset"].ToString();
            }
        }
        public static string RegisterEmailTemplatePath
        {
            get
            {
                string rootPath = HttpContext.Current.Server.MapPath("~");
                return rootPath.Substring(0, rootPath.LastIndexOf(@"\")) + ConfigurationManager.AppSettings["RegisterEmailTemplatePath"];
            }
        }

        /// <summary>
        /// ten avatar mac dinh
        /// </summary>
        public static string ImageURLAvataDefault
        {
            get
            {
                return _imageURLAvataDefault;
            }
        }

        /// <summary>
        /// URL Avata mac dinh
        /// </summary>
        public static string ImageURLAvata
        {
            get
            {
                return _imageURLAvata;
            }
        }
        /// <summary>
        /// URL Conver mac dinh
        /// </summary>
        public static string ImageURLCoverDefault
        {
            get
            {
                return _imageURLCoverDefault;
            }
        }
        /// <summary>
        /// Tên Cover mac dinh
        /// </summary>
        public static string ImageURLCover
        {
            get
            {
                return _imageURLCover;
            }
        }
        /// <summary>
        /// Check string into User list<string> with | tag
        /// </summary>
        /// <param name="manUserName"></param>
        /// <param name="userName"></param>
        /// <param name="idUserName"></param>
        public static string[] StringUserSlipt(string[] manUserName, string userName, string idUserName)
        {
            if (!manUserName[0].Contains(userName + "|"))
            {
                manUserName[0] += userName + "|"; // list user     
                manUserName[1] += idUserName + "|"; // list id                
            }
            return manUserName;
        }
        /// <summary>
        /// 
        /// </summary>
        public static string GetPriceAPIUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["GetPriceAPIUrl"].ToString();
            }
        }
    }
    public class PosistionFilter
    {
        public int Index { get; set; }
        public int Posistion { get; set; }
    }




}