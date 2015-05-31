using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Drawing;
using System.Drawing.Drawing2D;
using VfsCustomerService.Utility;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace VfsCustomerService.Utility
{
    enum FileFormat
    {
        MP3Format = 1,
        FLVFormat = 2,
        DocFormat = 3,
        ExcelFormat = 4,
        TxtFormat = 5,
        CsvFormat = 6
    }

    public class UploadService
    {
        private static void CheckFLVFile(Stream stream)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(stream);
                char[] buff = new char[3];
                sr.Read(buff, 0, 3);
                if (buff[0] != 'F' || buff[1] != 'L' || buff[2] != 'V')
                {
                    throw new InvalidFLVFile();
                }
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
                throw new InvalidFLVFile();
            }
        }

        private static void CheckMP3File(Stream stream)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(stream);
                char[] buff = new char[3];
                sr.Read(buff, 0, 3);
                if (buff[0] != 'I' || buff[1] != 'D' || buff[2] != '3')
                {
                    throw new InvalidMP3File();
                }
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
                throw new InvalidMP3File();
            }
        }

        public static string UploadDocument(Stream stream, string filePath, string fileName)
        {
            int pos = fileName.LastIndexOf('.');
            string extFile = string.Empty;
            extFile = fileName.Substring(pos);
            string uploadFilename = Guid.NewGuid().ToString();
            try
            {
                stream.Seek(0, SeekOrigin.Begin);

                using (FileStream fileStream = new FileStream(filePath + uploadFilename + extFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[Constants.BUFFER_SIZE];
                    int bufferSize = 0;
                    do
                    {
                        bufferSize = stream.Read(buffer, 0, buffer.Length);
                        fileStream.Write(buffer, 0, bufferSize);
                    } while (bufferSize > 0);
                    stream.Close();
                    fileStream.Close();
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
            }
            return uploadFilename + extFile;
        }

        public static string UploadDocument(Stream stream, string filePath, string fileName, bool keepOriginalFileName)
        {
            int pos = fileName.LastIndexOf('.');
            string extFile = string.Empty;
            extFile = fileName.Substring(pos);

            string uploadFilename = Guid.NewGuid().ToString();

            if (keepOriginalFileName == true)
            {
                uploadFilename = fileName;
            }
            else
            {
                uploadFilename = fileName + extFile;
            }

            try
            {
                stream.Seek(0, SeekOrigin.Begin);

                using (FileStream fileStream = new FileStream(filePath + uploadFilename, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[Constants.BUFFER_SIZE];
                    int bufferSize = 0;
                    do
                    {
                        bufferSize = stream.Read(buffer, 0, buffer.Length);
                        fileStream.Write(buffer, 0, bufferSize);
                    } while (bufferSize > 0);
                    stream.Close();
                    fileStream.Close();
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
            }
            return uploadFilename;
        }

        public static ImageFormat GetImageFormat(ImageFormat rawFormat)
        {
            string serial = rawFormat.Guid.ToString();
            ImageFormat result = null;

            switch (serial.ToUpper())
            {
                case "B96B3CAB-0728-11D3-9D7B-0000F81EF32E":
                    result = ImageFormat.Bmp;
                    break;
                case "B96B3CB0-0728-11D3-9D7B-0000F81EF32E":
                    result = ImageFormat.Gif;
                    break;
                case "B96B3CAF-0728-11D3-9D7B-0000F81EF32E":
                    result = ImageFormat.Png;
                    break;
                case "B96B3CB1-0728-11D3-9D7B-0000F81EF32E":
                    result = ImageFormat.Tiff;
                    break;
                case "B96B3CAE-0728-11D3-9D7B-0000F81EF32E":
                    result = ImageFormat.Jpeg;
                    break;
                default:
                    throw new InvalidImageTypeFile();
            }
            return result;
        }

        public static string GetImageExtention(ImageFormat imageFormat)
        {
            string result = "";
            switch (imageFormat.Guid.ToString().ToUpper())
            {
                case "B96B3CAB-0728-11D3-9D7B-0000F81EF32E":
                    result = "bmp";
                    break;
                case "B96B3CB0-0728-11D3-9D7B-0000F81EF32E":
                    result = "gif";
                    break;
                case "B96B3CAF-0728-11D3-9D7B-0000F81EF32E":
                    result = "png";
                    break;
                case "B96B3CB1-0728-11D3-9D7B-0000F81EF32E":
                    result = "tiff";
                    break;
                case "B96B3CAE-0728-11D3-9D7B-0000F81EF32E":
                    result = "jpg";
                    break;
                default:
                    throw new InvalidImageTypeFile();
            }
            return result;
        }

        public static string UploadImage(string filePath, System.IO.Stream imageStream, bool resize, int fixedWidth)
        {
            Image fullSizeImg = null;
            Bitmap bitmap = null;
            Graphics g = null;
            string filename = "";
            try
            {
                /*throw incase of invalid image type file*/
                fullSizeImg = Image.FromStream(imageStream);

                filename = Guid.NewGuid().ToString() + "." + GetImageExtention(fullSizeImg.RawFormat);

                filePath = filePath + filename;

                /*catch ivalid image type*/
                bitmap = new Bitmap(fullSizeImg.Width, fullSizeImg.Height);
                g = Graphics.FromImage(bitmap);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, fullSizeImg.Width, fullSizeImg.Height);
                Rectangle srcRect = new Rectangle(0, 0, fullSizeImg.Width, fullSizeImg.Height);
                Rectangle desRect = new Rectangle(0, 0, fullSizeImg.Width, fullSizeImg.Height);
                g.DrawImage(fullSizeImg, desRect, srcRect, GraphicsUnit.Pixel);
                bitmap.Save(filePath, GetImageFormat(fullSizeImg.RawFormat));
                //resize image
                /* if (resize)
                 {
                     string newSize = CalculateImageSize(fullSizeImg.Width, fullSizeImg.Height, fixedWidth);
                     ImageResize resizingImage = new ImageResize();
                     resizingImage.PreserveAspectRatio = false;
                     resizingImage.Width = Convert.ToInt32(newSize.Split(',')[0]);
                     resizingImage.Height = Convert.ToInt32(newSize.Split(',')[1]);
                     resizingImage.File = filePath;
                     fullSizeImg = resizingImage.GetThumbnail();
                     fullSizeImg.Save(filePath);
                 }*/
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidImageTypeFile();
            }
            finally
            {
                if (fullSizeImg != null) { fullSizeImg.Dispose(); }
                if (bitmap != null) { bitmap.Dispose(); }
                if (g != null) { g.Dispose(); }
            }
            return filename;
        }

        public static void Delete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                log4net.Util.LogLog.Error(ex.Message, ex);
            }
        }

        private static string CalculateImageSize(int imgWidth, int imgHeight, int originalFixedWidth)
        {
            //default width =152px
            /*int width = imgWidth;
            int height = imgHeight;*/
            decimal newheight;

            if (imgWidth <= 152)
            {
                return (imgWidth.ToString() + "," + imgHeight.ToString());
            }
            else
            {

                newheight = ((decimal)imgHeight / (decimal)imgWidth) * originalFixedWidth;
                return (originalFixedWidth.ToString() + "," + Convert.ToInt32(newheight).ToString());
            }

        }

        public static System.Drawing.Image Resize(Image srcImage, int width, int height)
        {
            Image fullSizeImg = srcImage;

            Bitmap bmPhoto = new Bitmap(width, height);
            /*bmPhoto.SetResolution(fullSizeImg.HorizontalResolution,
                srcImage.VerticalResolution);*/
            bmPhoto.SetResolution(width, height);

            int sourceWidth = width;
            int sourceHeight = height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;

            grPhoto.DrawImage(fullSizeImg,
                new Rectangle(destX, destY, width, height),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return fullSizeImg;
        }
    }

    public class ImageResize
    {
        private double m_width, m_height;
        private bool m_use_aspect = true;
        private bool m_use_percentage = false;
        private string m_image_path;
        private Image m_src_image, m_dst_image;
        private ImageResize m_cache;
        private Graphics m_graphics;

        public string File
        {
            get { return m_image_path; }
            set { m_image_path = value; }
        }

        public Image Image
        {
            get { return m_src_image; }
            set { m_src_image = value; }
        }

        public bool PreserveAspectRatio
        {
            get { return m_use_aspect; }
            set { m_use_aspect = value; }
        }

        public bool UsePercentages
        {
            get { return m_use_percentage; }
            set { m_use_percentage = value; }
        }

        public double Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        public double Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        protected virtual bool IsSameSrcImage(ImageResize other)
        {
            if (other != null)
            {
                return (File == other.File
                    && Image == other.Image);
            }

            return false;
        }

        protected virtual bool IsSameDstImage(ImageResize other)
        {
            if (other != null)
            {
                return (Width == other.Width
                    && Height == other.Height
                    && UsePercentages == other.UsePercentages
                    && PreserveAspectRatio == other.PreserveAspectRatio);
            }

            return false;
        }

        public virtual Image GetThumbnail()
        {
            // Flag whether a new image is required
            bool recalculate = false;
            double new_width = Width;
            double new_height = Height;

            // Is there a cached source image available? If not,
            // load the image if a filename was specified; otherwise
            // use the image in the Image property
            if (!IsSameSrcImage(m_cache))
            {
                if (m_image_path.Length > 0)
                {
                    // Load via stream rather than Image.FromFile to release the file
                    // handle immediately
                    if (m_src_image != null)
                        m_src_image.Dispose();

                    // Wrap the FileStream in a "using" directive, to ensure the handle
                    // gets closed when the object goes out of scope
                    using (Stream stream = new FileStream(m_image_path, FileMode.Open))
                        m_src_image = Image.FromStream(stream);

                    recalculate = true;
                }
            }

            // Check whether the required destination image properties have
            // changed
            if (!IsSameDstImage(m_cache))
            {
                // Yes, so we need to recalculate.
                // If you opted to specify width and height as percentages of the original
                // image's width and height, compute these now
                if (UsePercentages)
                {
                    if (Width != 0)
                    {
                        new_width = (double)m_src_image.Width * Width / 100;

                        if (PreserveAspectRatio)
                        {
                            new_height = new_width * m_src_image.Height / (double)m_src_image.Width;
                        }
                    }

                    if (Height != 0)
                    {
                        new_height = (double)m_src_image.Height * Height / 100;

                        if (PreserveAspectRatio)
                        {
                            new_width = new_height * m_src_image.Width / (double)m_src_image.Height;
                        }
                    }
                }
                else
                {
                    // If you specified an aspect ratio and absolute width or height, then calculate this 
                    // now; if you accidentally specified both a width and height, ignore the 
                    // PreserveAspectRatio flag
                    if (PreserveAspectRatio)
                    {
                        if (Width != 0 && Height == 0)
                        {
                            new_height = (Width / (double)m_src_image.Width) * m_src_image.Height;
                        }
                        else if (Height != 0 && Width == 0)
                        {
                            new_width = (Height / (double)m_src_image.Height) * m_src_image.Width;
                        }
                    }
                }

                recalculate = true;
            }

            if (recalculate)
            {
                // Calculate the new image
                if (m_dst_image != null)
                {
                    m_dst_image.Dispose();
                    m_graphics.Dispose();
                }

                Bitmap bitmap = new Bitmap((int)new_width, (int)new_height, m_src_image.PixelFormat);
                m_graphics = Graphics.FromImage(bitmap);
                m_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                m_graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                m_graphics.DrawImage(m_src_image, 0, 0, bitmap.Width, bitmap.Height);
                m_dst_image = bitmap;

                // Cache the image and its associated settings
                m_cache = this.MemberwiseClone() as ImageResize;
            }

            return m_dst_image;
        }

        ~ImageResize()
        {
            // Free resources
            if (m_dst_image != null)
            {
                m_dst_image.Dispose();
                m_graphics.Dispose();
            }

            if (m_src_image != null)
                m_src_image.Dispose();
        }
    }



/// <summary>
/// Methods to remove HTML from strings.
/// </summary>
    public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove White space which occur consecutively more than once.
        /// </summary>
        public static string NormalizeWhitespace(String InputStr)
        {
            Regex NormRx = new Regex("\\s\\s+");
            return NormRx.Replace(InputStr, " ");
        }
        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
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
    }

    public static class Encyption
    {
        public static string DoMD5(string SData)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
            byte[] result1 = md5.ComputeHash(encode.GetBytes(SData));
            string sResult2 = ToHexa(result1);
            return sResult2;
        }

        public static string ToHexa(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.AppendFormat("{0:X2}", data[i]);
            return sb.ToString();
        }
    }
}
