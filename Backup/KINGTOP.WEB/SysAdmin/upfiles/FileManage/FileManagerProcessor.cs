using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace CMSFileManage
{
    /// <summary>
    /// 文件管理处理
    /// </summary>
    public class FileManagerProcessor
    {
        private string _Value;
        private bool _Access;
        private string _FolderPath;     // 当前目录 绝对路径
        private int _FolderNum = 0;
        private int _FileNum = 0;

        /// <summary>
        /// 处理结果
        /// </summary>
        public string Value
        {
            get { return _Value; }
        }

        /// <summary>
        /// 权限是否足够
        /// </summary>
        public bool Access
        {
            get { return _Access; }
        }

        /// <summary>
        /// 目录数
        /// </summary>
        public int FolderNum
        {
            get { return _FolderNum; }
        }

        /// <summary>
        /// 文件数
        /// </summary>
        public int FileNum
        {
            get { return _FileNum; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public FileManagerProcessor()
        {
            HttpRequest Request = HttpContext.Current.Request;
            this._FolderPath = Request.QueryString["path"];
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="p_act">处理选项</param>
        public FileManagerProcessor(string p_act)
        {
            HttpRequest Request = HttpContext.Current.Request;
            this._FolderPath = Request.QueryString["path"];

            switch (p_act)
            {
                case "create":
                    CreateFolder();
                    break;
                case "rename":
                    RenameFileFolder();
                    break;
                case "del":
                    DeleteFileFolder();
                    break;
                case "upload":
                    UploadFile();
                    break;
                case "compress":
                    CompressFolder();
                    break;
                case "unpack":
                    UnpackFile();
                    break;
            }
        }

        /// <summary>
        /// 取得目录下所有文件
        /// </summary>
        /// <param name="p_folderPath">目录路径</param>
        /// <param name="p_read">是否操作成功</param>
        /// <returns>List FileInfo</returns>
        public List<FileFolderInfo> GetDirectories(string p_folderPath,string proName)
        {
            HttpServerUtility Server = HttpContext.Current.Server;
            HttpRequest Request = HttpContext.Current.Request;
            string imgExt = ".jpg,.bmp,.jpeg,.gif,.png";
            string imgHeight = string.Empty;
            string imgWidth = string.Empty;
            string order = Request.QueryString["order"];
            string urlString;
            List<FileFolderInfo> files = new List<FileFolderInfo>();
            DirectoryInfo di;
            FileInfo fi;
            string keyWord = Request.QueryString["keyword"];
            if (string.IsNullOrEmpty(keyWord))
            {
                keyWord = " ";
            }
            
            if (string.IsNullOrEmpty(proName))
            {
                proName = "default.aspx";
            }

            DirectoryInfo dir = new DirectoryInfo(p_folderPath);
            try
            {
                foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
                {
                    if (fsi is DirectoryInfo)
                    {
                        di = fsi as DirectoryInfo;
                        if ((di.Name.ToLower()+" ").Contains(keyWord.ToLower ()))
                        {
                            urlString = "<a href=\"" + proName + "?path=" + Server.UrlEncode(di.FullName) + "\">" +
                                "<img src=\"style/en/images/IcoFolder.gif\" alt=\"文件夹\" align=\"absmiddle\" /> " + di.Name + "</a>" +
                                " <a href=\"" + proName + "?act=compress&amp;path=" + Server.UrlEncode(this._FolderPath) + "&amp;objfolder=" + Server.UrlEncode(di.FullName) + "\" onclick=\"javascript:compressMsg();\">" +
                                "<img src=\"style/en/images/IcoPackage.gif\" alt=\"压缩\" align=\"absmiddle\" /></a>";

                            FileFolderInfo objFolder = new FileFolderInfo(di.Name, Server.UrlEncode(di.FullName), urlString, "", 0, "folder", di.LastWriteTime,"","");
                            files.Add(objFolder);

                            this._FolderNum++;
                        }
                    }
                    else
                    {
                        fi = fsi as FileInfo;
                        if ((fi.Name.ToLower() + " ").Contains(keyWord.ToLower()))
                        {
                            urlString = "<a href=\"file.axd?file=" + Server.UrlEncode(fi.FullName) + "\" target=\"_new\">" +
                                    "<img src=\"style/en/images/ext/" + fi.Extension.ToLower().Replace(".", "") + ".gif\" onerror=\"this.src='style/en/images/IcoOtherFile.gif'\" alt=\"文件\" align=\"absmiddle\" /> " + fi.Name + "</a>";

                            if (fi.Extension.ToLower() == ".zip")
                            {
                                urlString += "&nbsp;<a href=\"" + proName + "?act=unpack&amp;path=" + Server.UrlEncode(this._FolderPath) + "&amp;objfile=" + Server.UrlEncode(fi.FullName) + "\" onclick=\"javascript:unpackMsg();\">" +
                                    "<img src=\"style/en/images/IcoZip.gif\" alt=\"解压\" align=\"absmiddle\" /></a>";
                            }
                            else if (fi.Length < 50000 && (CheckExtEdit(fi.Extension.ToLower())))
                            {
                                urlString += "&nbsp;<a class=\"submodal-700-400\" href=\"Notepad.aspx?objfile=" + Server.UrlEncode(fi.FullName) + "\">" +
                                    "<img src=\"style/en/images/IcoNotepad.gif\" alt=\"编辑\" align=\"absmiddle\" /></a>";

                                if (CheckExtHighlighter(fi.Extension.ToLower()))
                                {
                                    urlString += "&nbsp;<a href=\"ViewCode.aspx?objfile=" + Server.UrlEncode(fi.FullName) + "\" target=\"_new\"><img src=\"style/en/images/IcoHighlighter.gif\" alt=\"查看代码\" align=\"absmiddle\" /></a>";
                                }
                            }
                            imgHeight = "";
                            imgWidth = "";
                            if (imgExt.IndexOf(fi.Extension.ToLower()) != -1)
                            {
                                //此段代码加上后效率很低，故去掉，获取图片尺寸到绑定数据中处理
                                //System.Drawing.Image image = System.Drawing.Image.FromFile(fi.FullName);
                                //imgHeight = image.Height.ToString ();
                                //imgWidth = image.Width.ToString ();
                            }

                            FileFolderInfo objFile = new FileFolderInfo(fi.Name, Server.UrlEncode(fi.FullName), urlString, fi.Extension, fi.Length, "file", fi.LastWriteTime,imgHeight,imgWidth);
                            files.Add(objFile);

                            this._FileNum++;
                        }
                    }
                }
                this._Access = true;
            }
            catch
            {
                this._Access = false;
                return files;
            }

            if (!string.IsNullOrEmpty(order))
                files.Sort(new FilesComparer(order));
            else
                files.Sort(new FilesComparer("name"));

            return files;
        }

        /// <summary>
        /// 检查文件扩展名, 是否可以编辑该文件
        /// </summary>
        /// <param name="p_ext">扩展名</param>
        /// <returns>true</returns>
        private bool CheckExtEdit(string p_ext)
        {
            string allowExt = ".ini|.txt|.log|.asp|.asa|.inc|.ascx|.config|.c|.cpp|.cs|.css|.java|.jsp|.js|.php|.sql|.vb|.xml|.htm|.html|.aspx|";

            if (allowExt.IndexOf(p_ext + "|") != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查文件扩展名, 是否支持代码高亮
        /// </summary>
        /// <param name="p_ext">扩展名</param>
        /// <returns>true</returns>
        private bool CheckExtHighlighter(string p_ext)
        {
            string allowExt = ".c|.cpp|.cs|.css|.java|.jsp|.js|.php|.sql|.vb|.xml|.htm|.html|.aspx|";

            if (allowExt.IndexOf(p_ext + "|") != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        private void CreateFolder()
        {
            HttpRequest Request = HttpContext.Current.Request;

            string folderName = Request.Form["txtFolderName"];
            DirectoryInfo dir = new DirectoryInfo(this._FolderPath + "\\" + folderName);
            if (dir.Exists)
            {
                this._Value = "目录名已存在";
            }
            else
            {
                try
                {
                    dir.Create();
                    this._Value = "创建目录成功, 目录名称为: " + folderName;
                }
                catch
                {
                    this._Value = "创建目录失败, 权限不足";
                }
            }
        }

        /// <summary>
        /// 重命名 移动 文件, 目录
        /// </summary>
        private void RenameFileFolder()
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpServerUtility Server = HttpContext.Current.Server;

            string newName = Request.Form["txtFolderName"];
            string oldName = Server.UrlDecode(Request.Form["txtOldName"]);

            if (File.Exists(oldName))
            {
                try
                {
                    if (File.Exists(Path.GetDirectoryName(oldName) + "\\" + newName))
                    {
                        this._Value = "文件已存在时, 无法重命名或移动该文件 ";
                    }
                    else
                    {
                        File.Move(oldName, Path.GetDirectoryName(oldName) + "\\" + newName);
                        this._Value = "重命名或移动文件成功, 新的文件名为: " + newName;
                    }
                }
                catch
                {
                    this._Value = "重命名或移动文件失败, 权限不足";
                }
            }
            else
            {
                try
                {
                    if (Directory.Exists(Path.GetDirectoryName(oldName) + "\\" + newName))
                    {
                        this._Value = "目录已存在时, 无法重命名或移动该目录 ";
                    }
                    else
                    {
                        Directory.Move(oldName, Path.GetDirectoryName(oldName) + "\\" + newName);
                        this._Value = "重命名或移动目录成功, 新的目录名为: " + newName;
                    }
                }
                catch (Exception ex)
                {
                    // this._Value = "重命名或移动目录失败, 权限不足";
                    this._Value = ex.Message;
                }
            }
        }

        /// <summary>
        /// 删除文件, 目录
        /// </summary>
        private void DeleteFileFolder()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string delName = Request.QueryString["file"];
            string delType = Request.QueryString["filetype"];

            if (delType == "file")
            {
                if (File.Exists(delName))
                {
                    try
                    {
                        File.Delete(delName);
                        this._Value = "删除文件成功, 被删除的文件为: " + Path.GetFileName(delName);
                    }
                    catch
                    {
                        this._Value = "删除文件失败, 权限不足";
                    }
                }
                else
                {
                    this._Value = "要删除的文件不存在";
                }
            }
            else if (delType == "folder")
            {
                if (Directory.Exists(delName))
                {
                    try
                    {
                        Directory.Delete(delName, true);
                        this._Value = "删除目录成功, 被删除的目录为: " + Path.GetFileName(delName);
                    }
                    catch
                    {
                        this._Value = "删除目录失败, 权限不足";
                    }
                }
                else
                {
                    this._Value = "要删除的目录不存在";
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadFile()
        {
            HttpRequest Request = HttpContext.Current.Request;
            HttpPostedFile fileUpload = Request.Files["fileUpload"];

            if (fileUpload.ContentLength == 0)
            {
                this._Value = "请先选择文件";
            }
            else
            {
                // HttpContext.Current.Server.ScriptTimeout = 600;
                string fileName = Path.GetFileName(fileUpload.FileName);

                if (File.Exists(this._FolderPath + "\\" + fileName))
                {
                    Random rnd = new Random();
                    string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + rnd.Next(1, 1000) + Path.GetExtension(fileName);

                    try
                    {
                        fileUpload.SaveAs(this._FolderPath + "\\" + newFileName);
                        this._Value = "上传的文件名已存在, 自动重命名为: " + newFileName;
                    }
                    catch
                    {
                        this._Value = "写入文件失败, 权限不足";
                    }
                }
                else
                {
                    try
                    {
                        fileUpload.SaveAs(this._FolderPath + "\\" + fileName);
                        this._Value = "上传文件完毕, 文件名为: " + fileName;
                    }
                    catch
                    {
                        this._Value = "写入文件失败, 权限不足";
                    }
                }
            }
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        private void CompressFolder()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string objFolder = Request.QueryString["objfolder"];

            FastZip fz = new FastZip();
            fz.CreateEmptyDirectories = true;
            try
            {
                // objFolder 要压缩的文件夹 (绝对路径)
                if (File.Exists(this._FolderPath + "\\" + Path.GetFileName(objFolder) + ".zip"))
                {
                    Random rnd = new Random();
                    string newFileName = Path.GetFileNameWithoutExtension(objFolder) + "_" + rnd.Next(1, 1000) + ".zip";
                    fz.CreateZip(this._FolderPath + "\\" + newFileName, objFolder, true, "");
                    this._Value = "已成功压缩文件夹, 文件名已存在, 自动重命名为: " + newFileName;
                }
                else
                {
                    fz.CreateZip(this._FolderPath + "\\" + Path.GetFileName(objFolder) + ".zip", objFolder, true, "");
                    this._Value = "已成功压缩文件夹, 文件名为: " + Path.GetFileName(objFolder) + ".zip";
                }
            }
            catch (Exception ex)
            {
                this._Value = ex.Message;
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        private void UnpackFile()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string objFile = Request.QueryString["objfile"];

            FastZip fz = new FastZip();
            try
            {
                // objFile 要解压的文件 (绝对路径)
                if (Directory.Exists(this._FolderPath + "\\" + Path.GetFileNameWithoutExtension(objFile)))
                {
                    Random rnd = new Random();
                    string newFolderName = this._FolderPath + "\\" + Path.GetFileNameWithoutExtension(objFile) + "_" + rnd.Next(1, 1000);

                    fz.ExtractZip(objFile, newFolderName, "");
                    this._Value = "解压完毕, 目录名称已存在, 自动重命名为: " + Path.GetFileNameWithoutExtension(newFolderName);
                }
                else
                {
                    fz.ExtractZip(objFile, this._FolderPath + "\\" + Path.GetFileNameWithoutExtension(objFile), "");
                    this._Value = "解压完毕, 存放在: " + Path.GetFileNameWithoutExtension(objFile);
                }
            }
            catch (Exception ex)
            {
                this._Value = ex.Message;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="p_filePath">文件路径</param>
        /// <param name="p_fileContent">文件内容</param>
        /// <param name="p_fileEncode">文件编码</param>
        public void ReadTextFile(string p_filePath, out string p_fileContent, out string p_fileEncode)
        {
            FileStream fs = new FileStream(p_filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, GetEncoding(fs, out p_fileEncode));

            p_fileContent = sr.ReadToEnd();

            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="p_filePath">文件路径</param>
        /// <param name="p_fileContent">文件内容</param>
        /// <param name="p_fileEncode">文件编码</param>
        public string SaveTextFile(string p_filePath, string p_fileContent, string p_fileEncode)
        {
            string result;
            System.Text.Encoding setEncoding = System.Text.Encoding.Default;

            if (p_fileEncode == "UTF-8")
            {
                setEncoding = System.Text.Encoding.UTF8;
            }
            else if (p_fileEncode == "Unicode")
            {
                setEncoding = System.Text.Encoding.Unicode;
            }
            else if (p_fileEncode == "Unicode big endian")
            {
                setEncoding = System.Text.Encoding.BigEndianUnicode;
            }

            FileStream fs = new FileStream(p_filePath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, setEncoding);

            try
            {
                sw.Write(p_fileContent);
                sw.Flush();

                result = "<script type=\"text/javascript\">alert(\"保存文件成功\");</script>";
            }
            catch (Exception ex)
            {
                result = "<script type=\"text/javascript\">alert(\"" + ex.Message + "\");</script>";
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
            return result;
        }

        /// <summary>
        /// 取得文件编码
        /// </summary>
        private System.Text.Encoding GetEncoding(FileStream p_fs, out string p_fileEncode)
        {
            System.Text.Encoding targetEncoding = System.Text.Encoding.Default;
            p_fileEncode = "ANSI";

            if (p_fs != null && p_fs.Length >= 1)
            {
                string byte1 = "";
                string byte2 = "";
                string byte3 = "";

                p_fs.Seek(0, SeekOrigin.Begin);
                byte1 = Convert.ToByte(p_fs.ReadByte()).ToString("X");

                if (p_fs.Length >= 2)
                    byte2 = Convert.ToByte(p_fs.ReadByte()).ToString("X");

                if (p_fs.Length >= 3)
                    byte3 = Convert.ToByte(p_fs.ReadByte()).ToString("X");

                // UTF8 EF, BB, BF
                // Unicode FF, FE
                // BE-Unicode FE, FF

                if (byte1 == "EF" && byte2 == "BB" && byte3 == "BF")
                {
                    targetEncoding = System.Text.Encoding.UTF8;
                    p_fileEncode = "UTF-8";
                }
                else if (byte1 == "FF" && byte2 == "FE")
                {
                    targetEncoding = System.Text.Encoding.Unicode;
                    p_fileEncode = "Unicode";
                }
                else if (byte1 == "FE" && byte2 == "FF")
                {
                    targetEncoding = System.Text.Encoding.BigEndianUnicode;
                    p_fileEncode = "Unicode big endian";
                }

                p_fs.Seek(0, SeekOrigin.Begin);
            }
            return targetEncoding;
        }
    }

    /// <summary>
    /// 自定义排序
    /// </summary>
    public class FilesComparer : IComparer<FileFolderInfo>
    {
        private string _sortColumn;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="sortExpression">要排序的字段</param>
        public FilesComparer(string sortExpression)
        {
            this._sortColumn = sortExpression;
        }

        /// <summary>
        /// 实现接口定义的 Compare 方法，比较两个 FileFolderInfo 对象实例
        /// </summary>
        /// <param name="a">实体类</param>
        /// <param name="b"></param>
        /// <returns>实体类</returns>
        public int Compare(FileFolderInfo a, FileFolderInfo b)
        {
            int retVal = 0;
            switch (_sortColumn.ToLower())
            {
                case "name":
                    //retVal = String.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);  //ASC
                    retVal = String.Compare(b.Name, a.Name, StringComparison.InvariantCultureIgnoreCase);    //DESC
                    break;
                case "ext":
                    retVal = String.Compare(a.Ext, b.Ext, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case "size":
                    retVal = String.Compare(a.FormatSize, b.FormatSize, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case "modifydate":
                    retVal = DateTime.Compare(DateTime.Parse(a.FormatModifyDate), DateTime.Parse(b.FormatModifyDate));
                    break;
                default:
                    retVal = String.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);
                    break;
            }
            return (retVal);
        }
    }
}
