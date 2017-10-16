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

namespace KingTop.WEB.SysAdmin.Editor.ueditor.dialogs.background
{
    public partial class background : KingTop.Web.Admin.AdminPage
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
            imgSet =  "0,0,"  + imgSet;
            paramValue = imgSet;
            #endregion

            #region 选择文件
            // 操作处理
            CMSFileManage.FileManagerProcessor fileManage = new CMSFileManage.FileManagerProcessor(Request.QueryString["act"]);
            builder.Append(fileManage.Value);

            string upFileType = Request.QueryString["type"];
            string RootUploadFiles = string.Empty;
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
                currPath.AppendFormat("<a href=\"background.aspx?path={1}&type={2}\">{0}</a>", q + "\\", comePath, upFileType);
                comePath += "\\";
            }

            // 返回上级
            //if (new DirectoryInfo(folderPath).Root.ToString().Replace("\\", "") != folderPath.ToUpper())
            if (RootUploadFiles.ToLower().IndexOf(folderPath.ToLower()) == -1)
            {
                string previousFolder = folderPath.Substring(0, folderPath.LastIndexOf("\\"));
                backHtml = "<a href=\"background.aspx?path=" + Server.UrlEncode(previousFolder) + "&type=" + upFileType + "\"><img src=\"../video/folderback.gif\" alt=\"返回上级\" align=\"absmiddle\"  border=0/> 返回上级</a>";
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
            int maxWidth = 130;
            int maxHeight = 130;
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
            int pageSize = 20;
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
            FileUrl = "";
            URLStr = "";
            if (imgExt.IndexOf(ext) != -1 && type == "file")
            {
                url = path.ToLower().Replace(Request.PhysicalApplicationPath.ToLower(), "");
                URLStr = "<a href=\"#\" ondblclick=\"InsertSelect('/" + (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "');return false;\" title='双击选择'>";
                re = URLStr + "<img src='/" + (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "' " + GetImgHW(imgWidth, imgHeight) + "  class='imgCon' border=0></a>";
                FileUrl = (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/");

            }
            else if (type == "folder")
            {
                url = path + "\\" + fileName;
                re = "<a href='?path=" + url + "&type=" + upFileType + "'><img src='../video/ext2/folder.jpg' class='imgCon' border=0></a>";
                URLStr = "<a href='?path=" + url + "&type=" + upFileType + "'>";
            }
            else
            {
                url = path.ToLower().Replace(Request.PhysicalApplicationPath.ToLower(), "");
                URLStr = "<a href=\"#\" ondblclick=\"InsertSelect('/" + (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "');return false;\" title='双击选择'>";
                re = URLStr + "<img src='../video/ext2/" + ext.Replace(".", "") + ".jpg' class='imgCon' onerror=\"this.src='../video/ext2/other.jpg'\" border=0></a>";
            }

            return re;
        }
        #endregion
    }
}
