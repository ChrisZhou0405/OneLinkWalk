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

namespace KingTop.WEB.SysAdmin.upfiles
{
    public partial class upload : KingTop.Web.Admin.AdminPage
    {
        #region 变量
        private string _upType = string.Empty;
        private string _extType = string.Empty;
        private string _controlMaxSize = string.Empty;
        private int MaxSize = 2097152;      //2M 1024*1024*2 允许上传最大文件大小
        UploadConfig uploadobj;  //上传设置信息
        string SavePath = ""; // 保存路径;
        string URL = "";      //上传文件域名
        string AllowExt = ""; //上传文件类型
        protected string jsExe = string.Empty;  //输出字符串
        protected  int thumbWidth, thumbHeight; // 缩咯图的宽与高 修改人：吴岸标
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

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            uploadobj = Upload.GetConfig(GetUploadImgPath);   //上传设置信息
            this.UpType = Request.QueryString["UpType"].ToLower();  //上传类型
            this.ExtType = Request.QueryString["ExtType"];            //用户设置允许上传文件类型
            this.ControlMaxSize = Request.QueryString["MaxSize"];      //用户设置允许最大上传文件大小

            tr3.Visible = false;
            if (uploadobj.IsEnableUpload == "0")  //判断是否关闭上传功能
            {
                span1.Visible = false;
                span2.Visible = true;
                return;
            }

            switch (this.UpType)
            {
                case "media"://视频，flash
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadMediaSize));
                    SavePath = uploadobj.MediaSavePath + "/" + SiteDir + "/Medias";
                    AllowExt = GetExtType(uploadobj.UploadMediaType);
                    //URL = GetUploadUrl(uploadobj.MediaUrl, "Medias", SavePath);
                    break;

                case "file"://文件
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadFilesSize));
                    SavePath = uploadobj.FileSavePath + "/" + SiteDir + "/Files";
                    AllowExt = GetExtType(uploadobj.UploadFilesType);
                    //URL = GetUploadUrl(uploadobj.FileUrl, "Files", SavePath);
                    break;

                case "image"://图片
                    MaxSize = GetMaxSize(int.Parse(uploadobj.UploadImageSize));
                    SavePath = uploadobj.ImageSavePath + "/" + SiteDir + "/Images";
                    AllowExt = GetExtType(uploadobj.UploadImageType);
                    //URL = GetUploadUrl(uploadobj.ImageUrl, "Images", SavePath);
                    SetThumbWH();
                    tr3.Visible = true;
                    break;

                default:
                    break;
            }
            SavePath = SavePath.Replace("//", "/");
            URL = URL.Replace("//", "/");
            if (!Page.IsPostBack)
            {
                spanSize.InnerHtml = (Math.Round(float.Parse(MaxSize.ToString()) / 1048576, 2)).ToString();
                this.lblMessage.Text = Utils.GetResourcesValue("Common", "UploadMsg") + AllowExt;
            }
        }
        #endregion

        #region 设置图片缩略图宽、高
        // 作者：吴岸标
        private void SetThumbWH()
        {
            thumbWidth = Utils.ParseInt(uploadobj.ThumbnailWidth, 125);
            thumbHeight = Utils.ParseInt(uploadobj.ThumbnailHeight, 125);

            if (Request.Cookies["HQB_UpFile_ThumbHW"] != null && Server.UrlDecode(Request.Cookies["HQB_UpFile_ThumbHW"].Value).Contains("$$")) // 上传窗口中设置
            {
                string[] thumbWH;

                thumbWH = Server.UrlDecode(Request.Cookies["HQB_UpFile_ThumbHW"].Value).Split(new string[] { "$$" }, StringSplitOptions.None);

                if (thumbWH.Length > 1 && !string.IsNullOrEmpty(thumbWH[0].Trim()) && !string.IsNullOrEmpty(thumbWH[1].Trim()))
                {
                    thumbWidth = Utils.ParseInt(thumbWH[0], thumbWidth);
                    thumbHeight = Utils.ParseInt(thumbWH[1], thumbHeight);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ThumbWidth"]) || !string.IsNullOrEmpty(Request.QueryString["TumbHeight"]))
                    {
                        if (!string.Equals(Request.QueryString["ThumbWidth"].Trim(), "0"))  // 宽不能为零
                        {
                            thumbWidth = Utils.ParseInt(Request.QueryString["ThumbWidth"], thumbWidth);
                        }

                        if (!string.Equals(Request.QueryString["ThumbHeight"].Trim(), "0")) // 高不能为零
                        {
                            thumbHeight = Utils.ParseInt(Request.QueryString["ThumbHeight"], thumbHeight);
                        }

                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ThumbWidth"]) || !string.IsNullOrEmpty(Request.QueryString["TumbHeight"]))
                {
                    if (!string.Equals(Request.QueryString["ThumbWidth"].Trim(), "0"))  // 宽不能为零
                    {
                        thumbWidth = Utils.ParseInt(Request.QueryString["ThumbWidth"], thumbWidth);
                    }

                    if (!string.Equals(Request.QueryString["ThumbHeight"].Trim(), "0")) // 高不能为零
                    {
                        thumbHeight = Utils.ParseInt(Request.QueryString["ThumbHeight"], thumbHeight);
                    }

                }
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

            return reExt.ToLower();
        }
        #endregion

        #region 文件上传
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (uploadobj.IsEnableUpload == "1")  // 判断是否允许上传
            {
                if (txtFileUpload.HasFile)
                {
                    #region 变量
                    string formName = Request.QueryString["FormName"];              // 表单名称
                    string elementName = Request.QueryString["ElementName"];        // 控件名称
                    string saveType = Request.QueryString["SaveType"];              // 保存文件名类型，1=用上传文件名保存，否则用系统时间重命名保存
                    string controlType = Request.QueryString["ControlType"];        // 控件类型，0=select,其他为input;
                    string getSizeControl = Request.QueryString["GetSizeControl"];  // 如果上传设置了需要保存文件大小，则填写保存文件大小控件名称，否则不用填写,返回字节数。例如：ImageSize

                    string noExt = SystemConst.NOT_ALLOWED_UPLOAD_TYPE;         // 不允许上传类型
                    string fileName = string.Empty;                             // 文件名
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmss");  // 上传文件名
                    string reFilePath = URL;                                    // 返回文件路径,如果保存路径填写的是绝对地址，则返回文件路径为域名+系统创建路径，如果为相对地址，则为：域名+相对路径+系统创建路径
                    string fileExt = string.Empty;                              // 上传文件扩展名

                    fileName = Path.GetFileName(this.txtFileUpload.FileName);
                    fileExt = Path.GetExtension(fileName).ToLower();  //上传文件扩展名
                    #endregion

                    if (txtFileUpload.FileContent.Length > MaxSize)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=\"javascript\">alert('" + Utils.GetResourcesValue("Common", "UploadErr1") + Math.Round(float.Parse(MaxSize.ToString()) / 1048576, 2) + "M!');</script>");
                        return;
                    }

                    if (SavePath.IndexOf(":") == -1)  //判断输入的是虚拟路径
                    {
                        SavePath = Server.MapPath(GetVirtualPath + SavePath);
                    }

                    if (AllowExt.IndexOf(fileExt.Replace(".", "")) == -1)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=\"javascript\">alert('" + Utils.GetResourcesValue("Common", "UploadExtErr") + "!');</script>");
                        return;
                    }

                    if (noExt.IndexOf(fileExt) != -1)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=\"javascript\">alert('" + Utils.GetResourcesValue("Common", "UploadExtErr1") + "!');</script>");
                        return;
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
                    //URL = URL + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveName + fileExt;
                    URL =  DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveName + fileExt;
                    if (File.Exists(fileName) && saveType == "1")
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=\"javascript\">alert('" + Utils.GetResourcesValue("Common", "UploadFileExistErr") + "!');</script>");
                        return;
                    }

                    try
                    {
                        txtFileUpload.SaveAs(fileName); //保存至服务器
                        DelFile();
                        if (this.UpType == "image" && uploadobj.IsEnableWatermark == "1") //如果上传图片，则判断是否需要打水印
                        {
                            KingTop.WEB.SysAdmin.UploadBase.ImageWater(uploadobj, fileName);
                        }
                        if (Request.Form["chkIshumbnail"] == "1")  //判断是否上传的是否需生成缩略图
                        {
                            KingTop.WEB.SysAdmin.UploadBase.MakeThumbnail(fileName, fileName + ".gif", thumbWidth, thumbHeight, "HW");     // 生成缩略图方法
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }


                    if (controlType == "0")         // 将上传文件名加到下拉列表控件中
                    {
                        jsExe = "<script type=\"text/javascript\">parent.document.getElementById(\"" + elementName + "\").options[parent.document.getElementById(\"" + elementName + "\").options.length] = new Option(\"" + URL + "\", \"" + URL + "\");parent.Closed();</script>";
                    }
                    else if (controlType == "2")    // 多文件自定义系统字段
                    {
                        jsExe = "<script type=\"text/javascript\">parent.AddItemToMultiFile(\""+ URL +"\", \""+ elementName +"\", \""+ getSizeControl +"\", true);parent.Closed();</script>";
                    }
                    else                            // 将上传文件地址附加至文本框
                    {
                        jsExe = "<script language='javascript'>parent.document.getElementById(\"" + elementName + "\").value=\"" + URL + "\";if(parent.document.getElementById(\"" + elementName + "_Del\") != null){parent.document.getElementById(\"" + elementName + "_Del\").disabled=false;};";

                        if (!string.IsNullOrEmpty(getSizeControl) && getSizeControl != "undefined")
                        {
                            jsExe += "parent.document.getElementById(\"" + getSizeControl + "\").value=\"" + txtFileUpload.FileContent.Length + "\";";
                        }
                        jsExe += "if(parent.document.getElementById(\"" + elementName + "_Look\") != null){parent.document.getElementById(\"" + elementName + "_Look\").disabled=false;}; parent.Closed();</script>";
                    }
                }
            }
        }
        #endregion

        #region 删除原文件
        void DelFile()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pFile"]))
            {
                string filePath = Server.MapPath(Request.QueryString["pFile"]);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                if (File.Exists(filePath + ".gif"))
                {
                    File.Delete(filePath + ".gif");
                }
            }
        }
        #endregion

        
    }
}
