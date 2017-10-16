using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using KingTop.Common;
using KingTop.Config;

namespace UploadifyDemo.RemoteHandlers {

    public class Upload : IHttpHandler {
        #region 变量

        UploadConfig uploadobj;  //上传设置信息
        string SavePath = ""; // 保存路径;
        string URL = "";      //上传文件域名
        string strParam = string.Empty;

        //protected string jsExe = string.Empty;  //输出字符串
        protected int thumbWidth, thumbHeight, IsWater; // 缩咯图的宽与高 修改人：吴岸标
        private string SiteDir;  //站点目录
        #endregion

        #region 属性

        public string GetUploadImgPath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/" + SiteDir + "/config/Upload.config");
            }
        }

        private string GetUrlParam(string key)
        {
            return HttpContext.Current.Request.Form[key];
        }

        //获取虚拟路径
        public string GetVirtualPath
        {
            get
            {
                string virPath;
                virPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (virPath.Substring(virPath.Length - 1) != "/")
                {
                    virPath = virPath + "/";
                }
                return virPath;
            }
        }
        #endregion

        #region 得到上传文件URL
        private string GetUploadUrl(string strUrl, string fileDir, string setUploadPath)
        {
            string reUrl;
            if (string.IsNullOrEmpty(strUrl))
            {
                reUrl = GetVirtualPath + setUploadPath;
                reUrl = reUrl.Replace("//", "/");
            }
            else
            {
                reUrl = strUrl;
            }
            if (reUrl.Substring(reUrl.Length - 1) != "/")
            {
                reUrl = reUrl + "/";
            }
            return reUrl;
        }
        #endregion
        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try {

                HttpPostedFile postedFile = context.Request.Files["Filedata"];


                SiteDir = GetUrlParam("SiteDir");
                string saveType = GetUrlParam("SaveType");
                string ImgSet = GetUrlParam("ImgSet");
                if (string.IsNullOrEmpty(ImgSet))
                {
                    ImgSet = "0,0,0";
                }
                string[] arrImgSet = ImgSet.Split(',');
                thumbWidth = Utils.ParseInt(arrImgSet[0], 0);
                thumbHeight = Utils.ParseInt(arrImgSet[1], 0);
                IsWater = Utils.ParseInt(arrImgSet[2], 0);
                string UPType = GetUrlParam("UpType");
                string fileName = postedFile.FileName;
                string fileExt = Path.GetExtension(fileName).ToLower();
                string saveName = DateTime.Now.ToString("yyyyMMddhhmmsfff");  // 上传文件名

                if (string.IsNullOrEmpty(UPType))
                {
                    UPType = "image";
                }

                uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath);   //上传设置信息

                if (uploadobj.IsEnableUpload != "1")  // 判断是否允许上传
                {
                    System.Web.HttpContext.Current.Response.Write("已经关闭上传功能，请联系管理员");
                    System.Web.HttpContext.Current.Response.End();
                }

                if (SystemConst.NOT_ALLOWED_UPLOAD_TYPE.IndexOf(fileExt) != -1)
                {
                    System.Web.HttpContext.Current.Response.Write("该文件类型不允许上传！");
                    System.Web.HttpContext.Current.Response.End();
                }

                SavePath = "{0}";
                //文件保存在站点目录下，用下面代码,注释后将不分站点保存
                //if (!string.IsNullOrEmpty(SiteDir))
                //{
                //    SavePath += "/" + SiteDir;
                //}
                SavePath += "{1}";

                switch (UPType)
                {
                    case "media"://视频，flash
                        SavePath = string.Format(SavePath, uploadobj.MediaSavePath, "/Medias");
                        URL = GetUploadUrl(uploadobj.MediaUrl, "Medias", SavePath);
                        break;

                    case "file"://文件
                        SavePath = string.Format(SavePath, uploadobj.FileSavePath, "/Files");
                        URL = GetUploadUrl(uploadobj.FileUrl, "Files", SavePath);
                        break;

                    case "image"://图片
                        SavePath = string.Format(SavePath, uploadobj.ImageSavePath, "/Images");
                        URL = GetUploadUrl(uploadobj.ImageUrl, "Images", SavePath);
                        break;
                    case "flash"://flash
                        SavePath = string.Format(SavePath, uploadobj.MediaSavePath, "/Medias");
                        URL = GetUploadUrl(uploadobj.MediaUrl, "Medias", SavePath);
                        break;
                    default:
                        break;
                }

                string reFilePath = URL;  // 返回文件路径,如果保存路径填写的是绝对地址，则返回文件路径为域名+系统创建路径，如果为相对地址，则为：域名+相对路径+系统创建路径
                if (SavePath.IndexOf(":") == -1)  //判断输入的是虚拟路径
                {
                    SavePath = context.Server.MapPath(GetVirtualPath + SavePath);
                }
                SavePath = SavePath + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                if (!Directory.Exists(SavePath))
                {
                    Directory.CreateDirectory(SavePath);
                }

                if (saveType == "1")  //使用原文件名进行保存到服务器
                {
                    int fileNameLength = fileName.LastIndexOf(".");
                    saveName = fileName.Substring(0, fileNameLength);
                }

                fileName = SavePath + saveName + fileExt;
                URL = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveName + fileExt;
                if (File.Exists(fileName) && saveType == "1")
                {
                    File.Delete(fileName);
                }
                postedFile.SaveAs(fileName); //保存至服务器
                if (thumbWidth > 0 || thumbHeight > 0)  //判断是否上传的是否需生成缩略图
                {
                    string thumbMode = "HW";
                    if (thumbWidth > 0 && thumbHeight == 0)
                    {
                        thumbMode = "W";
                    }
                    else if (thumbWidth == 0 && thumbHeight > 0)
                    {
                        thumbMode = "H";
                    }
                    KingTop.WEB.SysAdmin.UploadBase.MakeThumbnail(fileName, fileName + ".gif", thumbWidth, thumbHeight, thumbMode);     // 生成缩略图方法
                }

                if (IsWater == 1) //如果上传图片，则判断是否需要打水印
                {
                    KingTop.WEB.SysAdmin.UploadBase.ImageWater(uploadobj, fileName);
                }
                
                context.Response.Write(URL);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex) {
                context.Response.Write("Error");
                context.Response.StatusCode = 500;
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}
