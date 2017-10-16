using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

using KingTop.Config;
using KingTop.Common;

namespace KingTop.WEB.SysAdmin
{
    public static class UploadBase
    {
        #region 生成水印，后台上传图片用到 gavin 2012-10-19
        public static  void ImageWater(UploadConfig uploadobj,string imgFilePath)
        {
            ImageWatermark iwobj = new ImageWatermark(0, 0, imgFilePath);
            if (uploadobj.WatermarkType == "0") //水印为文字类型
            {
                iwobj.Quality = 100;
                iwobj.Title = uploadobj.WatermarkText;
                iwobj.FontSize = int.Parse(uploadobj.WatermarkFontSize);
                switch (uploadobj.WatermarkFontStyle)
                {
                    case "1":
                        iwobj.StrStyle = FontStyle.Bold;
                        break;
                    case "2":
                        iwobj.StrStyle = FontStyle.Italic;
                        break;
                    case "4":
                        iwobj.StrStyle = FontStyle.Underline;
                        break;
                    case "8":
                        iwobj.StrStyle = FontStyle.Strikeout;
                        break;

                }
                iwobj.FontColor = ColorTranslator.FromHtml("#" + uploadobj.WatermarkFontColor);
                iwobj.BackGroudColor = Color.White;
                iwobj.FontFamilyName = uploadobj.WatermarkFontType;
                iwobj.Waterpos = uploadobj.WatermarkLocation;
                iwobj.FontBorderColor = ColorTranslator.FromHtml("#" + uploadobj.WatermarkFontBorderColor);
                iwobj.FontBorderSize = int.Parse(uploadobj.WatermarkFontBorder);

                iwobj.Watermark();
            }
            else
            {
                iwobj.Waterpos = uploadobj.WatermarkLocation;
                iwobj.WaterPath = System.Web.HttpContext .Current .Server.MapPath("~/" + uploadobj.WatermarkPic);
                iwobj.WaterPicturemark();
                
            }
        }
        #endregion

        #region 生成缩略图

        /// <summary>
        /// 生成缩略图

        /// </summary>
        /// <param Name="originalImagePath">源图路径（物理路径）</param>
        /// <param Name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param Name="width">缩略图宽度</param>
        /// <param Name="height">缩略图高度</param>
        /// <param Name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }//新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板



            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充



            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                //throw e;

            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                // File.Delete(originalImagePath);//删除原图
            }
        }
        #endregion
    }

    
}
