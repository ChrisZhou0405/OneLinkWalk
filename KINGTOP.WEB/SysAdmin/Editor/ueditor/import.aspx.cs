using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Drawing;
using Aspose.Words;
using Aspose.Cells;
using KingTop.Common;
using Aspose.Pdf.Devices;



namespace KingTop.WEB.SysAdmin.Editor.ueditor
{
    public partial class import : System.Web.UI.Page
    {
        private string docPath = string.Empty;  //文档上传后的路径
        private string htmlPath = string.Empty; //转成html后的路径
        private string urlPath;  //页面内URL连接
        private string dirPath = string.Empty;//导入时上传的文件所在的文件夹路径
        private string fileName = string.Empty;//文件名

        private string allowedExt = ".doc.docx.xls.xlsx.ppt.pptx.pdf";  //允许导入的文档格式
        private int maxLengthDoc = 4 * 1024 * 1024;  //4M

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region 导入操作
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (UploadFile())
            {
                string con = string.Empty;
                if (docPath.ToLower().IndexOf(".doc") > 0)
                {
                    WordSaveAsHTML();
                    con = GetHtml(); 
                }
                else if (docPath.ToLower().IndexOf(".xls") >0)
                {
                    ExcelSaveAsHTML();
                    con = GetHtml(); 
                }
                else if (docPath.ToLower().IndexOf(".ppt") > 0)
                {
                    con = PPTSaveAsHTML();
                }
                else if (docPath.ToLower().IndexOf(".pdf") > 0)
                {
                    con = PDFSaveAsHTML(docPath);
                }

                con = con.Replace(fileName + "_files/", "/uploadfiles/import/" + fileName + "/" + fileName + "_files/");
                con = con.Replace(fileName + ".", "/uploadfiles/import/" + fileName + "/" + fileName + ".");
                txtArea.Value = con;
                OnDel(dirPath);
            }
        }
        #endregion 

        #region 上传到临时目录
        private bool UploadFile()
        {
            bool re = true;
            HttpPostedFile postedFile = docfile.PostedFile;

            string oldFileName = postedFile.FileName;
            string fileExt = Path.GetExtension(oldFileName);
            if (allowedExt.IndexOf(fileExt.ToLower()) == -1)
            {
                re = false;
                Utils.RunJavaScript(this, "alert({msg:'文档类型不正确，只能导入:"+allowedExt+"！',title:'提示信息'})");
            }
            if (postedFile.ContentLength > 0)
            {
                if (postedFile.ContentLength > maxLengthDoc)
                {
                    re = false;
                    Utils.RunJavaScript(this, "alert({msg:'文档内容过大，只能导入" + maxLengthDoc / 1048576 + "M以内的文档！',title:'提示信息'})");
                }
                else
                {
                    fileName = DateTime.Now.ToString("yyyyMMddhhmmsfff");
                    dirPath = Server.MapPath("/uploadfiles/import/" + fileName);
                    urlPath = "/uploadfiles/import/" + fileName + "/" + fileName + ".html";
                    docPath = dirPath + "\\" + fileName + fileExt;
                    htmlPath = Server.MapPath(urlPath);

                    try
                    {
                        Directory.CreateDirectory(dirPath);
                        postedFile.SaveAs(docPath);
                    }
                    catch (Exception e)
                    {
                        re = false;
                        Utils.RunJavaScript(this, "alert({msg:'导入失败：" + e.Message.Replace("'", "\\'").Replace("\r\n", "<br>") + "',title:'提示信息'})");
                    }
                }
            }
            
            return re;
        }
        #endregion

        #region 获取上传文件的html
        private string GetHtml()
        {
            string content = string.Empty;
            StringWriter sw = new StringWriter();
            try
            {
                HttpContext.Current.Server.Execute(urlPath, sw);
                content = sw.ToString();

            }
            catch(Exception e)
            {
                content = "";
                Utils.RunJavaScript(this, "alert({msg:'导入失败：" + e.Message.Replace("'", "\\'").Replace("\r\n", "<br>") + "',title:'提示信息'})");
            }

            if (Request.Form["chkClearHtml"] == "1")
            {
                content = ClearWord(content);
            }
            return content;
        }
        #endregion

        #region 删除上传的所有文件
        private void OnDel(string dir)
        {
            if (File.Exists(htmlPath))
            {
                File.Delete(htmlPath); //直接删除其中的文件   
            }
            if (File.Exists(docPath))
            {
                File.Delete (docPath);
            }
            return;

            //以下是删除所以上传文件
            if (Directory.Exists(dir))
            {
                try
                {
                    foreach (string d in Directory.GetFileSystemEntries(dir))
                    {
                        if (File.Exists(d))
                            File.Delete(d); //直接删除其中的文件                           
                        else
                            OnDel(d); //递归删除子文件夹    
                    }

                    Directory.Delete(dir, true); //删除已空文件夹       
                }
                catch
                {

                }
            }
        }
        #endregion

        #region 文档转换
        private void WordSaveAsHTML()
        {
            Aspose.Words.Document doc = new Document(docPath);
            doc.Save(htmlPath, Aspose.Words.SaveFormat.Html);
        }

        private void ExcelSaveAsHTML()
        {
            Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook(docPath);
            wb.Save(htmlPath, Aspose.Cells.SaveFormat.Html);
        }


        private string PPTSaveAsHTML()
        {
            //当转换成html时会格式比较混乱
            //Aspose.Slides.Pptx.PresentationEx pe = new Aspose.Slides.Pptx.PresentationEx(docPath);
            //pe.Save(htmlPath, Aspose.Slides.Export.SaveFormat.Html);  


            Aspose.Slides.Presentation ppt = new Aspose.Slides.Presentation(docPath);
            string thePath = htmlPath.Replace(".html", ".pdf");
            ppt.Save(thePath, Aspose.Slides.Export.SaveFormat.Pdf);

            return PDFSaveAsHTML(thePath);
        }

        private string PDFSaveAsHTML(string thePath)
        {
            Aspose.Pdf.Document document = new Aspose.Pdf.Document(thePath);
            string con = string.Empty;
            var device = new Aspose.Pdf.Devices.JpegDevice();
            int quality = 80;

            //默认质量为100，设置质量的好坏与处理速度不成正比，甚至是设置的质量越低反而花的时间越长，怀疑处理过程是先生成高质量的再压缩
            device = new Aspose.Pdf.Devices.JpegDevice(quality);
            //遍历每一页转为jpg
            for (var i = 1; i <= document.Pages.Count; i++)
            {
                string filePathOutPut = Path.Combine(Server.MapPath("/uploadfiles/import/" + fileName + "/"), string.Format("img_{0}.jpg", i));
                FileStream fs = new FileStream(filePathOutPut, FileMode.OpenOrCreate);
                try
                {
                    device.Process(document.Pages[i], fs);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    fs.Close();
                    File.Delete(filePathOutPut);
                }

                con += "<br><img src='/uploadfiles/import/" + fileName + "/img_" + i + ".jpg'>";
            }

            return con;
        }
        #endregion

        #region 清除word的html
        private string ClearWord(string html)
        {
            System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
            // get rid of unnecessary tag spans (comments and title)
            sc.Add(@"<!--(\w|\W)+?-->");
            sc.Add(@"<title>(\w|\W)+?</title>");
            // Get rid of classes and styles
            sc.Add(@"\s?class=\w+");
            sc.Add(@"\s+style='[^']+'");
            // Get rid of unnecessary tags
            //sc.Add(@"<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|body|/?body|/?span|!\[)[^>]*?>");
            sc.Add(@"<(meta|link|/?o:|/?style|/?font|/?strong|/?st\d|/?head|/?html|body|/?body|/?span|!\[)[^>]*?>");
            // Get rid of empty paragraph tags
            sc.Add(@"(<[^>]+>)+ (</\w+>)+");
            // remove bizarre v: element attached to <img> tag
            sc.Add(@"\s+v:\w+=""[^""]+""");
            // remove extra lines
            sc.Add(@"(\n\r){2,}");
            foreach (string s in sc)
            {
                html = System.Text.RegularExpressions.Regex.Replace(html, s, "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }

            return html;
        }
        #endregion


    }
}