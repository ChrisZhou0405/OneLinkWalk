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
using System.Text;

// 更新日期        更新人      更新原因/内容
// 2010-9-7        胡志瑶      修改属性Width，由int型改为string型
namespace KingTop.Web.Admin.Controls
{
    public partial class Editor : UserControl
    {
        KingTop.Config.SiteParamConfig pci;
        KingTop.Web.Admin.AdminPage ap = new AdminPage();
        private string _width;
        private int _height;
        //private string _content;
        public string strEditor = string.Empty;
        public int _editorType = 0;
        public bool _isFirstEditor = true;

        public bool IsFirstEditor
        {
            set { _isFirstEditor = value; }
            get { return _isFirstEditor; }
        }
        public string width
        {
            set { _width = value; }
            get { return _width; }
        }
        public int height
        {
            set { _height = value; }
            get { return _height; }
        }
        public string Content
        {
            set { txtEditorContent.Text = value; }
            get
            {
                string reValue = txtEditorContent.Text;
                return reValue;
            }
        }

        public int EditorType
        {
            set { _editorType = value; }
            get { return _editorType; }
        }

        string _widthtype;
        public string WidthType
        {
            set { _widthtype = value; }
            get { return _widthtype; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            pci = KingTop.Config.SiteParam.GetConfig(ap.GetSiteParamPath);
            switch (pci.EditControl)
            {
                case "eWebEditor":
                    txtEditorContent.Attributes.Add("style", "display:none");
                    
                    break;
                case "ueditor":
                    if (EditorType == 0)
                    {
                        if (KingTop.Common.Utils.ParseInt(width, 0) < 610)
                            width = "610";
                        if (height < 180)
                            height = 180;
                    }
                    else
                    {
                        if (KingTop.Common.Utils.ParseInt(width, 0) < 710 && WidthType != "1")
                            width = "710";
                        if (height < 120)
                            height = 120;
                    }
                    txtEditorContent.Attributes.Add("style", "width:" + width + "px;height:" + height + "px;");
                    break;
            }

            btnImport.Attributes.Add("onclick", "DocImport('editor_" + this.ID + "')");
        }

        public string GetEditor()
        {
            StringBuilder sbEditor = new StringBuilder("");
            KingTop.Config.UploadConfig objUpload = KingTop.Config.Upload.GetConfig(ap.GetUploadImgPath);
            switch (pci.EditControl)
            {
                case "CKEditor":
                    if (IsFirstEditor)
                    {
                        sbEditor.Append("<script type=\"text/javascript\" src=\"../Editor/ckeditor/ckeditor.js\"></script><link href=\"../Editor/ckeditor/content.css\" rel=\"stylesheet\" type=\"text/css\" />");
                    }
                    if (EditorType == 0)
                    {
                        sbEditor.Append("<script type=\"text/javascript\">var " + this.ID + "$txtEditorContent");
                        sbEditor.Append("$$ckeditor$$obj = CKEDITOR.replace('" + this.ID + "$txtEditorContent', { linkUploadAllowedExtensions: '" + objUpload.UploadFilesType + "'");
                        sbEditor.Append(", nodeId: 1,language:'zh-cn', watermark: false, height: '" + height + "px', toolbar: 'ContentFull', modelId: 1, ");
                        sbEditor.Append("flashUploadAllowedExtensions: '" + objUpload.UploadMediaType + "', width: '" + width + "', imageUploadAllowedExtensions: '" + objUpload.UploadImageType + "'");
                        sbEditor.Append("  , skin: 'blue', thumbnail: false, fileRecord: true, fieldName: 'Content', wordPic: false, flashUpload: true, imageUpload: true, ");
                        sbEditor.Append("linkUpload: true, foreground: false, moduleName: '' }); </script>");
                    }
                    else
                    {
                        sbEditor.Append("<script type=\"text/javascript\">    var " + this.ID + "$txtEditorContent$$ckeditor$$obj = CKEDITOR.replace('" + this.ID + "$txtEditorContent', { language: 'zh-cn', height: '" + height + "px',width:'" + width + "' }); </script>");
                    }
                    btnImport.Attributes.Add("style", "display:none");

                    break;

                case "eWebEditor":
                    if (EditorType == 0)
                    {
                        sbEditor.Append("<iframe id=\"eWebEditor" + this.ID + "\" src=\"../Editor/eWebEditor/ewebeditor.htm?id=" + this.ID + "$txtEditorContent&amp;style=standard650&amp;skin=blue2\" width=\"" + width + "\" frameborder=\"0\" height=\"" + height + "\" scrolling=\"no\"></iframe>");
                    }
                    else
                    {
                        sbEditor.Append("<iframe id=\"eWebEditor" + this.ID + "\" src=\"../Editor/eWebEditor/ewebeditor.htm?id=" + this.ID + "$txtEditorContent&amp;style=mini500&amp;skin=blue2\" width=\"" + width + "\" frameborder=\"0\" height=\"" + height + "\" scrolling=\"no\"></iframe>");
                    }
                    btnImport.Attributes.Add("style", "display:none");
                    break;
                case "ueditor":
                    if (IsFirstEditor)
                    {
                        sbEditor.Append("<script type=\"text/javascript\" charset=\"utf-8\" src=\"../Editor/ueditor/editor_all_min.js\"></script><script type=\"text/javascript\" charset=\"utf-8\" src=\"../Editor/ueditor/editor_config.js\"></script><link rel=\"stylesheet\" type=\"text/css\" href=\"../Editor/ueditor/themes/default/ueditor.css\"/>");
                        sbEditor.Append(@"<script>function DocImport(editid) {
    openframe({ title: '文档导入', url: '../editor/ueditor/import.aspx?editid=' + editid, width: 400, height: 230 });
}</script>");
                    }
                    if (EditorType == 0)
                    {
                        sbEditor.Append("<script type=\"text/javascript\">var editor_" + this.ID + " = new baidu.editor.ui.Editor({minFrameHeight:" + (height - 110) + "});editor_" + this.ID + ".render('" + this.ID + "_txtEditorContent');</script>");
                    }
                    else
                    {
                        sbEditor.Append("<script type=\"text/javascript\">var editor_" + this.ID + " = new baidu.editor.ui.Editor({");
                        //                        sbEditor.Append(@"toolbars: [[ 'source',
                        //                                'bold', 'italic', 'underline', 'autotypeset', '|', 'pasteplain',  'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                        //                               'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                        //                                'link', 'unlink', 'insertimage', 'attachment', '|',
                        //                                'snapscreen', 'wordimage'
                        //                               ]],minFrameHeight:"+(height-50));

                        sbEditor.Append(@"toolbars: [[ 'source',
                                                        'bold', 'italic', 'underline',  '|', 'pasteplain',  'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                                                       'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                                                        'link', 'unlink', 'insertimage', 'insertvideo', 'attachment'
                                                       ]],minFrameHeight:" + (height - 50));
                        sbEditor.Append("});");
                        sbEditor.Append("editor_" + this.ID + ".render('" + this.ID + "_txtEditorContent');");
                        sbEditor.Append("</script>");
                    }
                    break;
                case "ueditormini":
                    if (IsFirstEditor)
                    {
                        sbEditor.Append("<script type=\"text/javascript\" charset=\"utf-8\" src=\"../Editor/ueditor/editor_all_min.js\"></script><script type=\"text/javascript\" charset=\"utf-8\" src=\"../Editor/ueditor/editor_config.js\"></script><link rel=\"stylesheet\" type=\"text/css\" href=\"../Editor/ueditor/themes/default/ueditor.css\"/>");
                        sbEditor.Append(@"<script>function DocImport(editid) {
    openframe({ title: '文档导入', url: '../editor/ueditor/import.aspx?editid=' + editid, width: 400, height: 230 });
}</script>");
                    }
                    sbEditor.Append("<script type=\"text/javascript\">var editor_" + this.ID + " = new baidu.editor.ui.Editor({");

                    sbEditor.Append(@"toolbars: [[ 'source',
                                                        'bold', 'italic', 'underline',  '|', 'pasteplain',  'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                                                       'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                                                        'link', 'unlink', 'insertimage', 'insertvideo', 'attachment'
                                                       ]],minFrameHeight:" + (height - 50));
                    sbEditor.Append("});");
                    sbEditor.Append("editor_" + this.ID + ".render('" + this.ID + "_txtEditorContent');");
                    sbEditor.Append("</script>");
                    break;
            }

            return sbEditor.ToString();
        }
    }
}