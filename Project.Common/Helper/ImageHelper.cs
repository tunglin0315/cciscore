using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.Globalization;
using System.IO;

namespace Project.Common.Helper
{
    /// <summary>
    /// 圖片共用方法
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 圖片壓縮、存檔
        /// </summary>
        /// <param name="stream">圖片資料</param>
        /// <param name="path">儲存路徑</param>
        /// <param name="maxWidth">限定最大寬度，預設1024</param>
        /// <returns>檔名</returns>
        public static string Image_Resize(Stream stream, string path, int maxWidth = 1024)
        {
            try
            {
                Bitmap img = new Bitmap(stream);

                int max_width = maxWidth;

                int resize_width = 0;//最後要縮圖的寬度
                int resize_height = 0;//最後要縮圖的高度
                double ratio = 0;//縮圖的比例   

                resize_width = img.Width;
                resize_height = img.Height;

                //計算最後要縮圖的寬度
                if (img.Width > max_width)
                {
                    ratio = (double)max_width / (double)img.Width;
                    resize_width = max_width;
                    resize_height = Convert.ToInt32(ratio * img.Height);
                }

                int _colorMaxCount = 1024;
                HashSet<Color> colors = new HashSet<Color>();
                if (img != null)
                {
                    for (int y = 0; y < img.Size.Height; y++)
                    {
                        for (int x = 0; x < img.Size.Width; x++)
                        {
                            colors.Add(img.GetPixel(x, y));
                            //假如色超過1024色
                            if (colors.Count >= _colorMaxCount)
                            {
                                break;
                            }
                        }
                        //假如色超過1024色
                        if (colors.Count >= _colorMaxCount)
                        {
                            break;
                        }
                    }
                }

                Bitmap img_bitmap = new Bitmap(resize_width, resize_height);
                Graphics gph = Graphics.FromImage(img_bitmap);
                Rectangle img_rec = new Rectangle(0, 0, resize_width, resize_height);

                //日期檔名 例:"20130719181411_7128.jpg"
                string _fn = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo);
                string newFileName = string.Empty;

                //是否大於1024色限定只輸出jpg 
                if (colors.Count >= _colorMaxCount)
                {
                    gph.CompositingQuality = System.DrawingCore.Drawing2D.CompositingQuality.HighQuality;
                    gph.SmoothingMode = System.DrawingCore.Drawing2D.SmoothingMode.HighQuality;
                    gph.InterpolationMode = System.DrawingCore.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gph.DrawImage(img, img_rec);

                    ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
                    EncoderParameters Params = new EncoderParameters(1);
                    Params.Param[0] = new EncoderParameter(System.DrawingCore.Imaging.Encoder.Quality, 95L);
                    newFileName = _fn + ".jpg";
                    img_bitmap.Save(path + "\\" + newFileName, Info[1], Params);
                }
                else
                {
                    //假如圖片色數小於1024    
                    gph.SmoothingMode = System.DrawingCore.Drawing2D.SmoothingMode.AntiAlias;
                    gph.InterpolationMode = System.DrawingCore.Drawing2D.InterpolationMode.HighQualityBicubic;
                    newFileName = _fn + ".png";
                    gph.DrawImage(img, img_rec);
                    img_bitmap.Save(path + "\\" + newFileName, ImageFormat.Png);
                }
                img.Dispose();
                img_bitmap.Dispose();
                gph.Dispose();
                return newFileName;
            }
            catch
            {
                return "";
            }
        }
    }
}