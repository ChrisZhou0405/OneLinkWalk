using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Config;

#region 说明
//注意：不能控制上传文件的大小
#endregion
namespace KingTop.WEB.SysAdmin.upfiles.FlashImageUpload
{
    public partial class upimage : KingTop.Web.Admin.AdminPage
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

        #endregion

        #region 属性

        private string UpType
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
        protected void Page_Load(object sender, EventArgs e)
        {
            //pFile=&FormName=theForm&ElementName=HQB_MultiFile_img1&ControlType=2&UpType=image&ExtType=&MaxSize=4096&GetSizeControl=img1&ThumbWidth=125&ThumbHeight=125

            uploadobj = Upload.GetConfig(GetUploadImgPath);   //上传设置信息
            this.UpType = Request.QueryString["UpType"].ToLower();  //上传类型
            this.ExtType = Request.QueryString["ExtType"];            //用户设置允许上传文件类型
            this.ControlMaxSize = Request.QueryString["MaxSize"];      //用户设置允许最大上传文件大小
            string ThumbWidth = Request.QueryString["ThumbWidth"];
            string ThumbHeight = Request.QueryString["ThumbHeight"];
            string saveType = Request.QueryString["SaveType"];
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
                    WaterDivId.InnerHtml = "<br>水印图片:<br><img src='/" + uploadobj.WatermarkPic + "' style='width:70px;height:70px;border:1px solid #CCCCCC' alt='水印图片' title='水印图片'>";
                }
            }

            txtWidth.Value = string.IsNullOrEmpty(ThumbWidth) == true ? uploadobj.ThumbnailWidth : ThumbWidth;
            txtHeight.Value = string.IsNullOrEmpty(ThumbHeight) == true ? uploadobj.ThumbnailHeight : ThumbHeight;
            divID.Visible = false;

            switch (this.UpType)
            {
                case "media"://视频，flash
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadMediaSize));
                    AllowExt = GetExtType(uploadobj.UploadMediaType);
                    break;

                case "file"://文件
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadFilesSize));
                    AllowExt = GetExtType(uploadobj.UploadFilesType);
                    break;

                case "image"://图片
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadImageSize));
                    AllowExt = GetExtType(uploadobj.UploadImageType);
                    divID.Visible = true;
                    break;

                default:
                    break;
            }

            MaxSize = MaxSize / 1024;

            AllowExt = GetExtType(uploadobj.UploadImageType);
            AllowExt = "*." + AllowExt.Replace("|", ";*.");
            this.divAllowType.InnerHtml = "允许上传的文件类型：" + AllowExt;
            string imgset = txtWidth.Value + "," + txtHeight.Value + "," + uploadobj.IsEnableWatermark;
            string Keys = DateTime.Now.ToString("yyyyMMddHHmmss");
            string urlParam = "SiteDir=" + SiteDir + "_tp_UpType=" + this.UpType + "_tp_SaveType=" + saveType + "_tp_CacheKey=" + Keys + "_tp_ImgSet=" + imgset;
            InitParam.Value = urlParam;
            cacheKey.Value = Keys;
        }
    }
}
