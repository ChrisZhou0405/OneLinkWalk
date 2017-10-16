using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Config;
using System.Text;

namespace KingTop.WEB.SysAdmin.upfiles.uploadify
{
    public partial class editupload : KingTop.Web.Admin.AdminPage
    {
        #region 变量
        private string _upType = string.Empty;
        private string _extType = string.Empty;
        private string _controlMaxSize = string.Empty;
        public int MaxSize = 2097152;      //2M 1024*1024*2 允许上传最大文件大小

        UploadConfig uploadobj;  //上传设置信息
        public string AllowExt = ""; //上传文件类型
        protected string jsExe = string.Empty;  //输出字符串

        protected int thumbWidth, thumbHeight; // 缩咯图的宽与高 修改人：吴岸标
        public string IsMulti = "true";
        public string urlParam = string.Empty;
        public string divHeight = "140px";
        public string URL = string.Empty;
        string SavePath = ""; // 保存路径;
        #endregion

        #region 属性

        public string UpType
        {
            get { return _upType; }
            set { _upType = value; }
        }

        private string ExtType
        {
            get { return _extType; }
            set { _extType = value; }
        }

        private string ControlMaxSize
        {
            get { return _controlMaxSize; }
            set { _controlMaxSize = value; }
        }
        #endregion
        #region 得到上传文件限制大小
        private int GetMaxSize(int uploadSetSize)
        {
            int reSize = uploadSetSize;
            if (!Utils.StrIsNullOrEmpty(this.ControlMaxSize))
            {
                reSize = Utils.ParseInt(this.ControlMaxSize, uploadSetSize);
            }

            return reSize * 1024;
        }
        #endregion

        #region 得到上传文件限制类型
        private string GetExtType(string uploadSetExt)
        {
            string reExt = uploadSetExt;
            if (!Utils.StrIsNullOrEmpty(this.ExtType))
            {
                reExt = this.ExtType;
            }
            reExt = reExt.ToLower();
            reExt = reExt.Replace(",", "|");
            reExt = reExt.Replace(";", "|");
            reExt = reExt.Replace(".", "");
            reExt = reExt.Replace("*", "");
            if (reExt.IndexOf("|") > 0)
            {
                string[] arr = reExt.Split('|');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (SystemConst.NOT_ALLOWED_UPLOAD_TYPE.IndexOf(arr[i]) != -1)
                    {
                        reExt = reExt.Replace("|" + arr[i], "");
                        reExt = reExt.Replace(arr[i] + "|", "");
                    }
                }
            }
            return reExt;
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
            uploadobj = Upload.GetConfig(GetUploadImgPath);   //上传设置信息
            this.UpType = Request.QueryString["UpType"].ToLower();  //上传类型
            this.ExtType = Request.QueryString["ExtType"];            //用户设置允许上传文件类型
            this.ControlMaxSize = Request.QueryString["MaxSize"];      //用户设置允许最大上传文件大小
            string ThumbWidth = Request.QueryString["ThumbWidth"];
            string ThumbHeight = Request.QueryString["ThumbHeight"];
            string saveType = Request.QueryString["SaveType"];
            if (Request.QueryString["IsMulti"] != "1")
            {
                IsMulti = "false";
                divHeight = "60px";
            }
            if (uploadobj.IsEnableUpload != "1")  // 判断是否允许上传
            {
                Response.Write("已经关闭上传功能，请联系管理员");
                Response.End();
            }
            if (uploadobj.IsEnableWatermark == "1")
            {
                chkWater.Checked = true;
                if (uploadobj.WatermarkType == "0")
                {
                    WaterDivId.InnerHtml = "<br>水印文字为:<br><font color=red>" + uploadobj.WatermarkText + "</font>";
                }
                else
                {
                    if(System.IO .File .Exists(Server.MapPath("/"+uploadobj.WatermarkPic)))
                        WaterDivId.InnerHtml = "水印图片:<br><img src='/" + uploadobj.WatermarkPic + "' style='width:100px;border:1px solid #CCCCCC' alt='水印图片' title='水印图片'>";
                    else
                        WaterDivId.InnerHtml = "<font color=red>注，请上传水印图片，图片位置为：<br>/" + uploadobj.WatermarkPic + "</font>";
                }
            }

            txtWidth.Value = string.IsNullOrEmpty(ThumbWidth) == true ? uploadobj.ThumbnailWidth : ThumbWidth;
            txtHeight.Value = string.IsNullOrEmpty(ThumbHeight) == true ? uploadobj.ThumbnailHeight : ThumbHeight;
            divID.Visible = false;
            SavePath = "{0}";
            if (!string.IsNullOrEmpty(SiteDir))
            {
                //此版本没有考虑按照站点目录存文件
                //SavePath += "/" + SiteDir;
            }
            SavePath += "{1}";

            switch (this.UpType)
            {
                case "media"://视频，flash
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadMediaSize));
                    AllowExt = GetExtType(uploadobj.UploadMediaType);
                    SavePath = string.Format(SavePath, uploadobj.MediaSavePath, "/Medias");
                    
                    URL = GetUploadUrl(uploadobj.MediaUrl, "Medias", SavePath);

                    divID.InnerHtml = @"<div style='width:390px;padding-left:5px'>如果网速不好，文件太大将会上传失败。可以用FTP将文件上传到服务器uploadfiles/medias/文件夹中，然后切换到“常规”在源文件中输入视频地址：/uploadfiles/medias/****.swf</div>
<div style='width:380px;padding-left:25px'>建议将视频转换为*.flv;*.swf格式，这样可以控制视频大小，又不会存在浏览器不兼容导致视频不能播放问题。<a href='../../editor/chs_bsvcsetup.rar' target='_blank' style='color:red'>下载视频转换器</a></div>";
                    divID.Visible =true;
                    divID.Attributes.Add("style", "width:420px");
                    break;

                case "file"://文件
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadFilesSize));
                    AllowExt = GetExtType(uploadobj.UploadFilesType);
                    SavePath = string.Format(SavePath, uploadobj.FileSavePath, "/Files");
                    URL = GetUploadUrl(uploadobj.FileUrl, "Files", SavePath);
                    break;

                case "image"://图片
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadImageSize));
                    AllowExt = GetExtType(uploadobj.UploadImageType);
                    SavePath = string.Format(SavePath, uploadobj.ImageSavePath, "/Images");
                    URL = GetUploadUrl(uploadobj.ImageUrl, "Images", SavePath);
                    divID.Visible = true;
                    WaterDivId.Visible = true;
                    break;
                case "flash"://flash
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadMediaSize));
                    SavePath = string.Format(SavePath, uploadobj.MediaSavePath, "/Medias");
                    AllowExt = "swf|fla";
                    URL = GetUploadUrl(uploadobj.MediaUrl, "Medias", SavePath);
                    break;
                default:
                    break;
            }

            AllowExt = "*." + AllowExt.Replace("|", ";*.");
            string[] extArr = AllowExt.Split(';'); //判断个数
            StringBuilder sb = new StringBuilder("允许上传的文件类型：");
            if (extArr.Length > 5)
            {
                for (int i = 0; i < extArr.Length; i++)
                {
                    if (i % 9 == 0 && i != 0)
                    {
                        sb.Append("<br>");
                    }
                    if (i != 0)
                    {
                        sb.Append(";");
                    }
                    sb.Append(extArr[i]);
                }
            }
            else
            {
                sb.Append(AllowExt);
            }
            this.divAllowType.InnerHtml = sb.ToString ();

            this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "<br>单个文件最大允许上传“"+ Math.Round((decimal)MaxSize/1048576,2)+"M”";
            int bestWidth = Utils.ParseInt(Request.QueryString["BestWidth"],0);
            int  bestHeight = Utils.ParseInt(Request.QueryString["BestHeight"],0);
            if (this.UpType == "image"&& (bestWidth>0|| bestHeight>0))
            {
                this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "；图片最佳";
                if (bestWidth>0)
                {
                    this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "宽度为：" + bestWidth+"px；";
                }
                if (bestHeight>0)
                {
                    this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "高度为：" + bestHeight + "px";
                }
            }
            urlParam = "'ExtType':'" + this.ExtType + "','MaxSize':'" + MaxSize / 1024 + "','SiteDir':'" + SiteDir + "','UpType':'" + this.UpType + "','SaveType':'" + saveType+"'" ;
        }
    }
}

  