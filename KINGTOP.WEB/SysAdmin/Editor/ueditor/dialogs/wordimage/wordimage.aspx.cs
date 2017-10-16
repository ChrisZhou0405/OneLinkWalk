using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using KingTop.Common;
using KingTop.Config;
using System.Text;

namespace KingTop.WEB.SysAdmin.Editor.ueditor.dialogs.wordimage
{
    public partial class wordimage : KingTop.Web.Admin.AdminPage
    {
        #region 变量
        #region 上传时需要的变量
        public int MaxSize = 10240;      //2M
        UploadConfig uploadobj;  //上传设置信息
        public string AllowExt = ""; //上传文件类型
        public string AllowExtMemo = string.Empty;
        public string sitedir = string.Empty;
        public string paramKey = string.Empty;
        public string paramValue = string.Empty;
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 文件上传
            uploadobj = Upload.GetConfig(GetUploadImgPath);   //上传设置信息
            if (uploadobj.IsEnableUpload != "1")  // 判断是否允许上传
            {
                Response.Write("已经关闭上传功能，请联系管理员");
                Response.End();
            }
            MaxSize = int.Parse(uploadobj.UploadImageSize) / 1024;
            AllowExt = uploadobj.UploadImageType;
            AllowExt = "*." + AllowExt.Replace("|", ";*.");
            string imgSet = "0";
            string[] arr = AllowExt.Split(';');
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == 9)
                    AllowExtMemo += "<br>";
                if (i > 0)
                    AllowExtMemo += ";";
                AllowExtMemo += arr[i];
            }

            sitedir = SiteDir;

            if (uploadobj.IsEnableWatermark == "1")
            {
                imgSet = "1";
                chkWater.Checked = true;
                if (uploadobj.WatermarkType == "0")
                {
                    WaterDivId.InnerHtml = "<br>水印文字为:<br><font color=red>" + uploadobj.WatermarkText + "</font>";
                }
                else
                {
                    if (System.IO.File.Exists(Server.MapPath("/" + uploadobj.WatermarkPic)))
                        WaterDivId.InnerHtml = "水印图片:<br><img src='/" + uploadobj.WatermarkPic + "' style='width:100px;border:1px solid #CCCCCC' alt='水印图片' title='水印图片'>";
                    else
                        WaterDivId.InnerHtml = "<font color=red>注，请上传水印图片，图片位置为：<br>/" + uploadobj.WatermarkPic + "</font>";
                }
            }
            paramKey = DateTime.Now.ToString("yyyyMMddhhmmsfff");
            imgSet = "0,0," + imgSet;
            paramValue = imgSet;
            #endregion

        }
    }
}
