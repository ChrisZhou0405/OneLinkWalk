using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Config;
using System.Text;

namespace KingTop.WEB.SysAdmin.upfiles
{
    public partial class _Default : KingTop.Web.Admin.AdminPage
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
        public string divHeight = "170px";


        #region 选择文件时需要的变量
        protected string backHtml;
        protected string folderPath = HttpContext.Current.Request.QueryString["path"];
        protected System.Text.StringBuilder currPath = new System.Text.StringBuilder("");
        protected int folderNum = 0;
        protected int fileNum = 0;
        public string PageStr = string.Empty;
        public int iLoop = 0;
        public string URLStr = string.Empty;
        protected System.Text.StringBuilder builder = new System.Text.StringBuilder("");
        string SavePath = ""; // 保存路径;
        public string FileUrl = string.Empty;
        string RootUploadFiles = string.Empty;
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

        protected void Page_Load(object sender, EventArgs e)
        {

            #region 上传文件
            //pFile=&FormName=theForm&ElementName=HQB_MultiFile_img1&ControlType=2&UpType=image&ExtType=&MaxSize=4096&GetSizeControl=img1&ThumbWidth=125&ThumbHeight=125

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
                    WaterDivId.Visible = true;
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
                this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "；<br>图片最佳";
                if (bestWidth>0)
                {
                    this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "宽度为：" + bestWidth+"px；";
                }
                if (bestHeight>0)
                {
                    this.divAllowType.InnerHtml = this.divAllowType.InnerHtml + "高度为：" + bestHeight + "px";
                }
            }
            urlParam = "'ExtType':'" + this.ExtType + "','MaxSize':'" + MaxSize / 1024 + "','SiteDir':'" + SiteDir + "','UpType':'" + this.UpType + "','SaveType':'" + saveType+"'";
            #endregion

            #region 选择文件
            // 操作处理
            CMSFileManage.FileManagerProcessor fileManage = new CMSFileManage.FileManagerProcessor(Request.QueryString["act"]);
            builder.Append(fileManage.Value);

            string upFileType = this.UpType;
            string thisSiteDir = "/" + SiteDir;
            //文件不分站点保存
            thisSiteDir = "";
            switch (upFileType)
            {
                case "image":
                    RootUploadFiles = Server.MapPath("/uploadfiles" + thisSiteDir + "/images/");
                    break;
                case "file":
                    RootUploadFiles = Server.MapPath("/uploadfiles" + thisSiteDir + "/files/");
                    break;
                case "media":
                    RootUploadFiles = Server.MapPath("/uploadfiles" + thisSiteDir + "/medias/");
                    break;
                default:
                    RootUploadFiles = Server.MapPath("/uploadfiles" + thisSiteDir + "/files/");
                    break;
            }

            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = RootUploadFiles;
            }
            else if (folderPath.IndexOf(":") == -1)
            {
                folderPath = Server.MapPath("/" + folderPath);
            }


            // 组合路径, 快速导航
            string comePath = "";
            foreach (string q in folderPath.Split('\\'))
            {
                comePath += q;
                currPath.AppendFormat("<a href=\"default.aspx?path={1}&pfile=" + Request.QueryString["pfile"] + "&FormName=" + Request.QueryString["FormName"] + "&ElementName=" + Request.QueryString["ElementName"] + "&ControlType=" + Request.QueryString["ControlType"] + "&UpType=" + Request.QueryString["UpType"] + "&ExtType=" + Request.QueryString["ExtType"] + "&MaxSize=" + Request.QueryString["MaxSize"] + "&GetSizeControl=" + Request.QueryString["GetSizeControl"] + "&ThumbWidth=" + Request.QueryString["ThumbWidth"] + "&ThumbHeight=" + Request.QueryString["ThumbHeight"] + "&BestWidth=" + Request.QueryString["BestWidth"] + "&BestHeight=" + Request.QueryString["BestHeight"] + "&IsMulti=" + Request.QueryString["IsMulti"] + "\">{0}</a>", q + "\\", comePath, upFileType);
                comePath += "\\";
            }

            // 返回上级
            //if (new DirectoryInfo(folderPath).Root.ToString().Replace("\\", "") != folderPath.ToUpper())
            if (RootUploadFiles.ToLower().IndexOf(folderPath.ToLower()) == -1)
            {
                string previousFolder = folderPath.Substring(0, folderPath.LastIndexOf("\\"));
                backHtml = "<a href=\"default.aspx?path=" + Server.UrlEncode(previousFolder) + "&pfile=" + Request.QueryString["pfile"] + "&FormName=" + Request.QueryString["FormName"] + "&ElementName=" + Request.QueryString["ElementName"] + "&ControlType=" + Request.QueryString["ControlType"] + "&UpType=" + Request.QueryString["UpType"] + "&ExtType=" + Request.QueryString["ExtType"] + "&MaxSize=" + Request.QueryString["MaxSize"] + "&GetSizeControl=" + Request.QueryString["GetSizeControl"] + "&ThumbWidth=" + Request.QueryString["ThumbWidth"] + "&ThumbHeight=" + Request.QueryString["ThumbHeight"] + "&BestWidth=" + Request.QueryString["BestWidth"] + "&BestHeight=" + Request.QueryString["BestHeight"] + "&IsMulti=" + Request.QueryString["IsMulti"] + "\"><img src=\"../../editor/ueditor/dialogs/video/folderback.gif\" alt=\"返回上级\" align=\"absmiddle\"  border=0/> 返回上级</a>";
            }
            else
            {
                folderPath += "\\";
            }
            folderPath = folderPath.Replace("\\\\", "\\");
            // 绑定数据
            fileManage = new CMSFileManage.FileManagerProcessor();
            List<CMSFileManage.FileFolderInfo> files = fileManage.GetDirectories(folderPath, "filesmanage.aspx");

            if (fileManage.Access)
            {
                BindData(files, fileManage);
            }
            else
            {
                builder.Append("无权限访问该目录. <a href='javascript:history.go(-1);' style='font-weight: normal'>后退</a>");
            }


            if (builder.ToString() != "")
            {
                string builderResult = builder.ToString();
                builder = new System.Text.StringBuilder("");
                builder.AppendFormat("<script type=\"text/javascript\">$(\"#tips\").show(); $(\"#tipsMsg\").html(\"{0}\"); </script>", builderResult.Replace(@"\", @"\\"));
            }
            #endregion
        }

        #region 选择文件用到的方法
        private string GetImgHW(string imgWidth, string imgHeight)
        {
            string re = string.Empty;
            int maxWidth = 115;
            int maxHeight = 115;
            int imgw = KingTop.Common.Utils.ParseInt(imgWidth, 0);
            int imgh = KingTop.Common.Utils.ParseInt(imgHeight, 0);
            if (imgw > maxWidth || imgh > maxHeight)
            {
                if (imgw >= imgh)
                    re = "style='width:" + maxWidth + "px'";
                else
                    re = "style='height:" + maxHeight + "px'";
            }
            return re;
        }

        private void BindData(List<CMSFileManage.FileFolderInfo> files, CMSFileManage.FileManagerProcessor fileManage)
        {
            if (files == null)
                return;

            folderNum = fileManage.FolderNum;
            fileNum = fileManage.FileNum;

            List<CMSFileManage.FileFolderInfo> Result = new List<CMSFileManage.FileFolderInfo>();

            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
            {
                pageIndex = int.Parse(Request.QueryString["pg"]);
            }
            if (pageIndex < 1) pageIndex = 1;
            int pageSize = 18;
            int startIndex = (pageIndex - 1) * pageSize;
            string imgExt = ".jpg,.bmp,.jpeg,.gif,.png";
            string imgWidth = string.Empty;
            string imgHeight = string.Empty;
            int resultInt = 0;
            for (int i = startIndex; i < startIndex + pageSize && i < files.Count; i++)
            {
                Result.Add(files[i]);
                imgWidth = "";
                imgHeight = "";
                if (imgExt.IndexOf(files[i].Ext.ToLower()) != -1 && files[i].Type == "file")
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(Server.UrlDecode(files[i].FullName));
                    imgWidth = image.Width.ToString();
                    imgHeight = image.Height.ToString();
                    Result[resultInt].ImgWidth = imgWidth;
                    Result[resultInt].ImgHeight = imgHeight;
                }
                resultInt++;
            }

            rptList.DataSource = Result;
            rptList.DataBind();
            KingTop.Common.Split split = new KingTop.Common.Split();
            string pageTemplate = "<li><a href=\"{$FirstPageUrl}\"  class=\"text\">首页</a></li><li><a href=\"{$PrevPageUrl}\" class=\"text\">上页</a></li>[HQB.Loop]<li><a href=\"{$PageUrl}\">{$PageIndex}</a></li><li>[$$$$]</li><li><a href=\"{$CurrentPageUrl}\" class=\"hover02\">{$CurrentPageIndex}</a></li>[/HQB.Loop]<li><a href=\"{$NextPageUrl}\" class=\"text\">下页</a></li><li><a href=\"{$LastPageUrl}\" class=\"text\">末页</a></li>";
            string url = "";
            PageStr = KingTop.Common.Split.GetHtmlCode(url, pageTemplate, 2, pageIndex, pageSize, files.Count, false);
        }

        //图片分布式现在程序还不支持，故可以按照以下方式返回图片路径，如果考虑分布式，则返回路径需要重新考虑
        public string GetFiles(string FormatName, string fileName, string path, string ext, string type, string imgWidth, string imgHeight)
        {
            string re = FormatName;
            string upFileType = Request.QueryString["type"];
            string CKEditor = Request.QueryString["CKEditor"];
            string CKEditorFuncNum = Request.QueryString["CKEditorFuncNum"];
            string imgExt = ".jpg,.bmp,.jpeg,.gif,.png";
            string url = string.Empty;
            string imgUrl = string.Empty;
            FileUrl = "";
            URLStr = "";
            if (imgExt.IndexOf(ext) != -1 && type == "file")
            {
                url = path.ToLower().Replace(RootUploadFiles.ToLower(), "");
                url = url.Replace(Request.PhysicalApplicationPath.ToLower(), "");
                if (!string.IsNullOrEmpty(url))
                {
                    url += "/";
                }

                imgUrl = path.ToLower().Replace(Request.PhysicalApplicationPath.ToLower(), "");
                URLStr = "<a href=\"javascript:;\" ondblclick=\"InputUrl('" + (url.Replace("\\", "/") + fileName).Replace("//", "/") + "','');return false;\" title='双击选择'>";
                re = URLStr + "<img src='/" + (imgUrl.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "' " + GetImgHW(imgWidth, imgHeight) + "  class='imgCon' border=0></a>";
                FileUrl = (imgUrl.Replace("\\", "/") + "/" + fileName).Replace("//", "/");

            }
            else if (type == "folder")
            {
                url = path + "\\" + fileName;
                re = "<a href='?path=" + url + "&pfile=" + Request.QueryString["pfile"] + "&FormName=" + Request.QueryString["FormName"] + "&ElementName=" + Request.QueryString["ElementName"] + "&ControlType=" + Request.QueryString["ControlType"] + "&UpType=" + Request.QueryString["UpType"] + "&ExtType=" + Request.QueryString["ExtType"] + "&MaxSize=" + Request.QueryString["MaxSize"] + "&GetSizeControl=" + Request.QueryString["GetSizeControl"] + "&ThumbWidth=" + Request.QueryString["ThumbWidth"] + "&ThumbHeight=" + Request.QueryString["ThumbHeight"] + "&BestWidth=" + Request.QueryString["BestWidth"] + "&BestHeight=" + Request.QueryString["BestHeight"] + "&IsMulti=" + Request.QueryString["IsMulti"] + "'><img src='../../editor/ueditor/dialogs/video/ext2/folder.jpg' class='imgCon' border=0></a>";
                URLStr = "<a href='?path=" + url + "&type=" + upFileType + "'>";
            }
            else
            {
                url = path.ToLower().Replace(RootUploadFiles.ToLower(), "");
                url = url.Replace(Request.PhysicalApplicationPath.ToLower(), "");
                if (!string.IsNullOrEmpty(url))
                {
                    url += "/";
                }

                URLStr = "<a href=\"javascript:;\" ondblclick=\"InputUrl('" + (url.Replace("\\", "/") + fileName).Replace("//", "/") + "','');return false;\" title='双击选择'>";
                re = URLStr + "<img src='../../editor/ueditor/dialogs/video/ext2/" + ext.Replace(".", "") + ".jpg' class='imgCon' onerror=\"this.src='../../editor/ueditor/dialogs/video/ext2/other.jpg'\" border=0></a>";
            }

            return re;
        }
        #endregion
    }
}

