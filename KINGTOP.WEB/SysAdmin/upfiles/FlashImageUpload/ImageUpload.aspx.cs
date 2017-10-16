using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Drawing;
using KingTop.Config;
using KingTop.Common;

public partial class ImageUpload : System.Web.UI.Page
{
    #region 变量

    UploadConfig uploadobj;  //上传设置信息
    string SavePath = ""; // 保存路径;
    string URL = "";      //上传文件域名
    string strParam = string.Empty;

    //protected string jsExe = string.Empty;  //输出字符串
    protected int thumbWidth, thumbHeight, IsWater; // 缩咯图的宽与高 修改人：吴岸标
    private string SiteDir;  //站点目录
    private string SaveFileKey;  //上传后文件名保存到缓存中，SaveFileKey为缓存的Key值
    private string[] initArr;
    #endregion

    #region 属性

    public string GetUploadImgPath
    {
        get
        {
            return System.Web.HttpContext.Current.Server.MapPath("~/" + SiteDir + "/config/Upload.config");
        }
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
    private string GetUrlParam(string param, string[] arr)
    {
        string re = "";
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].IndexOf(param + "=") != -1)
            {
                return arr[i].Replace(param + "=", "");
            }
        }

        return re;
    }

    #endregion

    #region 得到上传文件URL
    private string GetUploadUrl(string strUrl, string fileDir, string setUploadPath)
    {
        string reUrl;
        if (string.IsNullOrEmpty(strUrl) && setUploadPath.IndexOf(":") == -1)
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

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {

        
        HttpFileCollection files = Request.Files;

        if (files.Count == 0)
        {
            Response.Write("请勿直接访问本文件");
            Response.End();
        }

        uploadobj = Upload.GetConfig(GetUploadImgPath);   //上传设置信息

        if (uploadobj.IsEnableUpload != "1")  // 判断是否允许上传
        {
            Response.Write("已经关闭上传功能，请联系管理员");
            Response.End();
        }

        On_Upload(files[0]);     // 只取第 1 个文件

    }
    #endregion

    #region 文件上传
    private void On_Upload(HttpPostedFile file)
    {
        if (file != null && file.ContentLength > 0)
        {

            #region 变量

            strParam = Request.QueryString["InitParam"];
            initArr = Utils.strSplit(strParam, "_tp_");
            SiteDir = GetUrlParam("SiteDir", initArr);
            SaveFileKey = GetUrlParam("CacheKey", initArr);

            string ImgSet = GetUrlParam("ImgSet", initArr);
            string[] arrImgSet = ImgSet.Split(',');
            thumbWidth = Utils.ParseInt(arrImgSet[0], 0);
            thumbHeight = Utils.ParseInt(arrImgSet[1], 0);
            IsWater = Utils.ParseInt(arrImgSet[2], 0);
            string UPType = GetUrlParam("UpType", initArr);
            if (string.IsNullOrEmpty(UPType))
            {
                UPType = "image";
            }
            
            string saveType = GetUrlParam("SaveType", initArr);              // 保存文件名类型，1=用上传文件名保存，否则用系统时间重命名保存

            string noExt = SystemConst.NOT_ALLOWED_UPLOAD_TYPE;         // 不允许上传类型

            string fileName = string.Empty;                             // 文件名

            string saveName = DateTime.Now.ToString("yyyyMMddHHmmss");  // 上传文件名

            string fileExt = string.Empty;                              // 上传文件扩展名

            fileName = Path.GetFileName(file.FileName);
            fileExt = Path.GetExtension(fileName).ToLower();  //上传文件扩展名
            #endregion
            if (noExt.IndexOf(fileExt) != -1)
            {
                System.Web.HttpContext.Current.Response.Write("改文件类型不允许上传！");
                System.Web.HttpContext.Current.Response.End();
            }

            SavePath = string.IsNullOrEmpty(SiteDir) == true ? uploadobj.ImageSavePath + "/" + UPType + "s" : uploadobj.ImageSavePath + "/" + SiteDir + "/" + UPType + "s";
            URL = GetUploadUrl(uploadobj.ImageUrl, UPType + "s", SavePath);
            string reFilePath = URL;  // 返回文件路径,如果保存路径填写的是
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
            URL = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveName + fileExt;
            if (File.Exists(fileName) && saveType == "1")
            {
                File.Delete(fileName);
            }

            try
            {
                file.SaveAs(fileName); //保存至服务器

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

                if (IsWater  == 1) //如果上传图片，则判断是否需要打水印
                {
                    KingTop.WEB.SysAdmin.UploadBase.ImageWater(uploadobj, fileName);
                }
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            
            //缓存返回值
            string returnStr;
            if (AppCache.IsExist(SaveFileKey))
            {
                returnStr = AppCache.Get(SaveFileKey) + "," + URL;
                AppCache.Remove(SaveFileKey);
            }
            else
            {
                returnStr = URL ;
            }

            AppCache.AddCache(SaveFileKey, returnStr, 1200);
        }
    }
    #endregion

}
