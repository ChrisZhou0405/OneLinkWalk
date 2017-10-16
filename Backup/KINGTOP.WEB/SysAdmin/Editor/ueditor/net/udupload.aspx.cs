using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Config;
using System.IO;

namespace KingTop.WEB.SysAdmin.Editor.ueditor.net
{
    public partial class udupload : System.Web.UI .Page 
    {
        #region 变量
        string state = "SUCCESS";
        UploadConfig uploadobj;  //上传设置信息
        string SavePath = ""; // 保存路径;
        string URL = "";      //上传文件域名
        string strParam = string.Empty;
        protected int thumbWidth, thumbHeight, IsWater; // 缩咯图的宽与高 修改人：吴岸标
        private string SiteDir;  //站点目录
        private string AllowExt;  //允许上传的文件类型
        private int MaxSize = 2;  //允许上传的文件最大大小 2M
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
            return HttpContext.Current.Request[key];
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

        

        protected void Page_Load(object sender, EventArgs e)
        {
            string fileExt = string.Empty;
            string fileName = string.Empty;
            string originalName = null;
            Response.ContentType = "text/plain";
            Response.Expires = -1;
            
            //如果设置了水印和缩略图，则先将设置保存到cache，在上传图片
            string paramValue = Request["paramValue"];
            string paramKey = Request["paramKey"];
            if (!string.IsNullOrEmpty(paramValue) && !string.IsNullOrEmpty(paramKey))
            {
                AppCache.AddCache(paramKey, paramValue, 60);
                Response.Write("OK");
                return;
            }

            //图片上传
            try
            {
                HttpPostedFile postedFile = Request.Files[0];



                SiteDir = GetUrlParam("SiteDir");
                string saveType = GetUrlParam("SaveType");
                string key = GetUrlParam("param1");
                string ImgSet = GetUrlParam("param2");
                if (!string.IsNullOrEmpty (key)&&AppCache.IsExist(key))
                {
                    ImgSet = AppCache.Get(key).ToString ();
                }
                if (string.IsNullOrEmpty(ImgSet))
                {
                    ImgSet = "0,0,0";  //
                }
                string[] arrImgSet = ImgSet.Split(',');
                thumbWidth = Utils.ParseInt(arrImgSet[0], 0);
                thumbHeight = Utils.ParseInt(arrImgSet[1], 0);
                IsWater = Utils.ParseInt(arrImgSet[2], 0);
                string UPType = GetUrlParam("UpType");
                fileName = postedFile.FileName;
                originalName = fileName;
                fileExt = Path.GetExtension(fileName).ToLower();
                string saveName = DateTime.Now.ToString("yyyyMMddhhmmsfff");  // 上传文件名

                if (string.IsNullOrEmpty(UPType))
                {
                    UPType = "image";
                }

                uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath);   //上传设置信息

                if (uploadobj.IsEnableUpload != "1")  // 判断是否允许上传
                {
                    state = "已经关闭上传功能，请联系管理员";
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
                        AllowExt = "*.wav;*.avi;*.mpg;*.mpeg;*.wma;*.flv;*.swf";
                        MaxSize = 100;
                        break;

                    case "file"://文件
                        SavePath = string.Format(SavePath, uploadobj.FileSavePath, "/Files");
                        URL = GetUploadUrl(uploadobj.FileUrl, "Files", SavePath);
                        AllowExt = uploadobj.UploadFilesType;
                        break;

                    case "image"://图片
                        SavePath = string.Format(SavePath, uploadobj.ImageSavePath, "/Images");
                        URL = GetUploadUrl(uploadobj.ImageUrl, "Images", SavePath);
                        AllowExt = uploadobj.UploadImageType;
                        break;
                    case "flash"://flash
                        SavePath = string.Format(SavePath, uploadobj.MediaSavePath, "/Medias");
                        URL = GetUploadUrl(uploadobj.MediaUrl, "Medias", SavePath);
                        AllowExt = uploadobj.UploadMediaType;
                        break;
                    default:
                        break;
                }

                if (AllowExt.IndexOf (fileExt.Replace (".","")) == -1)
                {
                    state = "文件类型不允许上传！";
                }
                if (SystemConst.NOT_ALLOWED_UPLOAD_TYPE.IndexOf(fileExt.Replace(".", "")) != -1)
                {
                    state = "文件类型不允许上传！";
                }
                if (postedFile.ContentLength > MaxSize * 1024 * 1024)
                {
                    state = "文件大小超出网站限制！";
                }

                string reFilePath = URL;  // 返回文件路径,如果保存路径填写的是绝对地址，则返回文件路径为域名+系统创建路径，如果为相对地址，则为：域名+相对路径+系统创建路径
                if (SavePath.IndexOf(":") == -1)  //判断输入的是虚拟路径
                {
                    SavePath = Server.MapPath(GetVirtualPath + SavePath);
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
                URL += DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveName + fileExt;
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
            }
            catch (Exception ex)
            {
                state = ex.Message;
            }
            if (Request["action"] == "tmpImg")
            {
                Response.Write("<script>alert('bbbb');parent.ue_callback('" + URL + "','" + state + "')</script>");//回调函数
            }
            else
            {
            Response.Write("{'state':'" + state + "','url':'" + URL + "','fileType':'" + fileExt + "','original':'" + originalName + "'}"); //向浏览器返回数据json数据
            }
        }
    }
}
