using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace CMSFileManage
{
    public partial class filesmanage : KingTop.Web.Admin.AdminPage
    {
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
        public string FileUrl = string .Empty ;
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
            // 操作处理
            FileManagerProcessor fileManage = new FileManagerProcessor(Request.QueryString["act"]);
            builder.Append(fileManage.Value);

            string upFileType = Request.QueryString["type"];
            string CKEditor = Request.QueryString["CKEditor"];
            string CKEditorFuncNum = Request.QueryString["CKEditorFuncNum"];
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
                currPath.AppendFormat("<a href=\"filesmanage.aspx?path={1}&type={2}&CKEditor={3}&CKEditorFuncNum={4}\">{0}</a>", q + "\\", comePath, upFileType, CKEditor, CKEditorFuncNum);
                comePath += "\\";
            }

            // 返回上级
            //if (new DirectoryInfo(folderPath).Root.ToString().Replace("\\", "") != folderPath.ToUpper())
            if(RootUploadFiles.ToLower ().IndexOf (folderPath.ToLower ())==-1)
            {
                string previousFolder = folderPath.Substring(0, folderPath.LastIndexOf("\\"));
                backHtml = "<a href=\"filesmanage.aspx?path=" + Server.UrlEncode(previousFolder) + "&type=" + upFileType + "&CKEditor=" + CKEditor + "&CKEditorFuncNum=" + CKEditorFuncNum + "\"><img src=\"style/Images/IcoLeft.gif\" alt=\"返回上级\" align=\"absmiddle\" /> 返回上级</a>";
            }
            else
            {
                folderPath += "\\";
            }
            folderPath = folderPath.Replace("\\\\", "\\");
            // 绑定数据
            fileManage = new FileManagerProcessor();
            List<FileFolderInfo> files = fileManage.GetDirectories(folderPath,"filesmanage.aspx");

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
        }

        private void BindData(List<FileFolderInfo> files, FileManagerProcessor fileManage)
        {
            if (files == null)
                return;

            folderNum = fileManage.FolderNum;
            fileNum = fileManage.FileNum;

            List<FileFolderInfo> Result = new List<FileFolderInfo>();

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
            int resultInt=0;
            for (int i = startIndex; i < startIndex + pageSize && i < files.Count; i++)
            {
                Result.Add(files[i]);
                imgWidth = "";
                imgHeight = "";
                if (imgExt.IndexOf(files[i].Ext.ToLower()) != -1 && files[i].Type=="file")
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
        public string GetFiles(string FormatName, string fileName, string path, string ext,string type)
        {
            string re = FormatName;
            string upFileType = Request.QueryString["type"];
            string CKEditor = Request.QueryString["CKEditor"];
            string CKEditorFuncNum = Request.QueryString["CKEditorFuncNum"];
            string imgExt = ".jpg,.bmp,.jpeg,.gif,.png";
            string url=string.Empty ;
            FileUrl = "";
            URLStr = "";
            if (imgExt.IndexOf(ext) != -1 && type == "file")
            {
                url = path.ToLower().Replace(Request.PhysicalApplicationPath.ToLower(), "");
                URLStr = "<a href=\"#\" onclick=\"OnCKEditor('/" + (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "');return false;\">";
                re = URLStr + "<img src='/" + (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "'  class='imgCon' border=0></a>";
                FileUrl = (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/");
                
            }
            else if (type == "folder")
            {
                url = path+"\\" + fileName;
                re = "<a href='?path=" + url + "&type=" + upFileType + "&CKEditor=" + CKEditor + "&CKEditorFuncNum=" + CKEditorFuncNum + "'><img src='style/images/ext2/folder.jpg' class='imgCon'></a>";
                URLStr = "<a href='?path=" + url + "&type=" + upFileType + "&CKEditor=" + CKEditor + "&CKEditorFuncNum=" + CKEditorFuncNum + "'>";
            }
            else
            {
                url = path.ToLower().Replace(Request.PhysicalApplicationPath.ToLower(), "");
                URLStr = "<a href=\"#\" onclick=\"OnCKEditor('/" + (url.Replace("\\", "/") + "/" + fileName).Replace("//", "/") + "');return false;\">";
                re = URLStr+"<img src='style/images/ext2/" + ext.Replace(".", "") + ".jpg' class='imgCon' onerror=\"this.src='style/images/ext2/other.jpg'\" border=0></a>";
            }

            return re;
        }
    }
}
