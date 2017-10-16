#region 引用程序集
using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.IO;

using KingTop.Model;
using KingTop.IDAL;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-18
// 功能描述：模型解析// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 私有变量
        /// <summary>
        /// 生成的模型页文件名
        /// </summary>
        private string _pageName;
        /// <summary>
        /// 当前模型主键
        /// </summary>
        private string modelID;
        /// <summary>
        /// 站点根目录URL
        /// </summary>
        private string rootUrl;
        /// <summary>
        /// 保存引用表相关的字符串
        /// </summary>
        private string tableReferenceInfo;
        /// <summary>
        /// 显示列的基本字段中，数据来自数据库的相关参数  格式：引用表名|表名.字段名称|Text引用列名|value引用列名 多个用 ","隔开
        /// </summary>
        private string listForignCol;
        /// <summary>
        /// 列表项高
        /// </summary>
        private string listHeight;
        /// <summary>
        /// 当前模型显示的列
        /// </summary>
        private string showColumn;
        /// <summary>
        /// 模型字段
        /// </summary>
        private DataTable dtField;
        /// <summary>
        /// 保存模型属性
        /// </summary>
        private Hashtable hsModel;
        /// <summary>
        /// 字段参数，字段名|字段缺省值
        /// </summary>
        private StringBuilder sbFieldParam;
        /// <summary>
        /// 要传递的Url参数
        /// </summary>
        private string keepUrlParam;
        /// <summary>
        /// 编辑页必项提示符
        /// </summary>
        /// <summary>
        /// 字段参数，字段名|字段缺省值
        /// </summary>
        private string htmlFieldParam;

        private string isRequiredTag = "<span style=\"color:#ff0000; font-size:14px; font-weight:bold;\">*</span>";
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Content.IParseModel dal = (IDAL.Content.IParseModel)Assembly.Load(path).CreateInstance(path + ".Content.ParseModel");
        /// <summary>
        /// 模型类型
        /// </summary>
        private ParserType modelType;
        #endregion

        #region 控件HTML代码常量
        /// <summary>
        /// UEditor编辑器标准型标签
        /// <uc1:Editor ID="Editor1" runat="server" width=650 height="450" EditorType=0/>
        /// </summary>
        //private static string controlUEditorFirst="<script type=\"text/javascript\" charset=\"utf-8\" src=\"../Editor/ueditor/editor_all_min.js\"></script><script type=\"text/javascript\" charset=\"utf-8\" src=\"../Editor/ueditor/editor_config.js\"></script><link rel=\"stylesheet\" type=\"text/css\" href=\"../Editor/ueditor/themes/default/ueditor.css\"/>";
        private static string controlUEditorFirst = "<uc1:Editor ID=\"editor_{#ID#}\" runat=\"server\" width=\"{#Width#}\" height=\"{#Height#}\" IsFirstEditor=\"true\"/>";
        //private static string controlUEditor = "<textarea  id=\"{#ID#}\" name=\"{#ID#}\" style=\"height:{#Height#}px;width:{#Width#}px\">{#Value#}</textarea><script type=\"text/javascript\">var editor_{#ID#} = new baidu.editor.ui.Editor({minFrameHeight:{#minFrameHeight#}});editor_{#ID#}.render('{#ID#}');</script>";
        private static string controlUEditor = "<uc1:Editor ID=\"editor_{#ID#}\" runat=\"server\" width=\"{#Width#}\" height=\"{#Height#}\" IsFirstEditor=\"false\"/>";
        /// <summary>
        /// UEditor编辑器Mini型标签
        /// </summary>
        //private static string controlUEditroMini = "<textarea  id=\"{#ID#}\" name=\"{#ID#}\" style=\"height:{#Height#}px;width:{#Width#}px\">{#Value#}</textarea><script type=\"text/javascript\">var editor_{#ID#} = new baidu.editor.ui.Editor({toolbars: [[ 'source', 'bold', 'italic', 'underline', '|', 'pasteplain',  'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|','lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|','link', 'unlink', 'insertimage', 'attachment', 'wordimage']],minFrameHeight:{#minFrameHeight#}});editor_{#ID#}.render('{#ID#}');</script>";
        
        /// <summary>
        /// CKEditor编辑器标准型标签
        /// </summary>
        private static string controlCKEditor = "<textarea  id=\"{#ID#}\" name=\"{#ID#}\">{#Value#}</textarea><script type=\"text/javascript\">var {#ID#}$$ckeditor$$obj = CKEDITOR.replace('{#ID#}', { linkUploadAllowedExtensions: '<%=uploadobj.UploadFilesType %>', nodeId: 1,language:'<%=Resources.Model.DateLang%>', watermark: false, height: '{#Height#}px', toolbar: 'ContentFull', modelId: 1, flashUploadAllowedExtensions: '<%=uploadobj.UploadMediaType %>', width: '{#Width#}px', imageUploadAllowedExtensions: '<%=uploadobj.UploadImageType %>', skin: 'blue', thumbnail: false, fileRecord: true, fieldName: 'Content', wordPic: false, flashUpload: true, imageUpload: true, linkUpload: true, foreground: false, moduleName: '' }); </script>";
        /// <summary>
        /// CKEditor编辑器Mini型标签
        /// </summary>
        private static string controlCKEditroMini = "<textarea  id=\"{#ID#}\" name=\"{#ID#}\">{#Value#}</textarea><script type=\"text/javascript\">    var {#ID#}$$ckeditor$$obj = CKEDITOR.replace('{#ID#}', { language: '<%=Resources.Model.DateLang%>', height: '{#Height#}px',width:'{#Width#}px' }); </script>";
        /// <summary>
        /// eWebEditor编辑器
        /// </summary>
        private static string controlEWebEditor = "<input type=\"hidden\" name=\"{#ID#}\" value=\"{#Value#}\"><iframe id=\"eWebEditor{#ID#}\" src=\"../Editor/eWebEditor/ewebeditor.htm?id={#ID#}&style={#EditorStyle#}&skin=blue2\" frameborder=\"0\" scrolling=\"no\" width=\"{#Width#}\" height=\"{#Height#}\"></iframe>";
        /// <summary>
        /// 单文件（图片）
        /// </summary>
        private static string controlSingleFile = "<input   type=\"text\" value=\"{#Value#}\" style=\"width:{#Width#}px;\"  maxlength=\"{#MaxLength#}\" name=\"{#ID#}\" id=\"{#ID#}\" /> <input type='button' onclick=\"InputFile('theForm','{#ID#}','{#FileType#}',1,'{#FileSuffix#}',{#FileMaxSize#},'{#ControlGetSize#}','','',0,'','')\" value = '上传文件' /> <input type='button' value = '下载' onclick=\"ShowImages('{#ID#}', '<%=GetUploadFileUrl()%>','file')\"/> <input type='button' onclick=\"if($('#{#ID#}').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'{#ID#}', '<%=GetUploadFileUrl()%>') } });}\" value = '删除'/>";
        /// <summary>
        ///  单图片
        /// </summary>
        private static string controlSingleImage = "<input   type=\"text\" value=\"{#Value#}\" style=\"width:{#Width#}px;\"  maxlength=\"{#MaxLength#}\" name=\"{#ID#}\" id=\"{#ID#}\" /> <input type='button' onclick=\"InputImages('theForm', '{#ID#}', 1, '{#FileSuffix#}', {#FileMaxSize#}, '{#ControlGetSize#}',{#ThumbWidth#},{#ThumbHeight#},0,'{#ImageBestWidth#}','{#ImageBestHeight#}')\" value = '上传图片' /> <input type='button' onclick=\"ShowImages('{#ID#}', '<%=GetUploadImgUrl()%>','image')\" value = '预览图片'/> <input type='button' onclick=\"if($('#{#ID#}').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'{#ID#}', '<%=GetUploadImgUrl()%>') } });}\"  value = '删除' />";
        /// <summary>
        /// 多文件
        /// </summary>
        private static string controlMultiFile = "<style  type=\"text/css\">.HQB_MultiFile_ButtonList input{margin-bottom:5px;}</style><table  class=\"HQB_MultiFile_ButtonList\"><tr><td valign=\"top\"><ul style=\"list-style:none; padding::0; margin:0; \"><li style=\"margin:0; padding:0;margin-bottom:5px;\"><input type=\"hidden\" id=\"{#ID#}\" value=\"{#Value#}\" name=\"{#ID#}\" /><select style=\" width:{#Width#}px; height:{#Height#}px;\" name=\"HQB_MultiFile_{#ID#}\" multiple=\"multiple\" id=\"HQB_MultiFile_{#ID#}\" ondblclick=\"return MultiFileItemModify('HQB_MultiFile_{#ID#}','{#ID#}')\" onchange=\"MultiFileSynchronousHideValue('HQB_MultiFile_{#ID#}','{#ID#}')\"></select></li><li style=\"margin:0; padding:0; display:{#Display#};\"><input type='button' onclick=\"InputFile('theForm','HQB_MultiFile_{#ID#}','{#FileType#}',2,'{#FileSuffix#}',{#FileMaxSize#},'{#ID#}','','',1,'','')\" value = '上传文件...' /></li></ul></td><td valign=\"top\"><input type=\"button\" onclick=\"MultiFileItemAdd('HQB_MultiFile_{#ID#}','{#ID#}')\" value=\"添加外部地址\" /><br /><input type=\"button\" value=\"修改选中\" onclick=\"MultiFileItemModify('HQB_MultiFile_{#ID#}','{#ID#}')\" /><br /><input type=\"button\" value=\"删除选中\" onclick=\"MultiFileItemDel('HQB_MultiFile_{#ID#}','{#ID#}')\"/><br /><input type=\"button\" value=\"向上移动\" onclick=\"UpOrder('HQB_MultiFile_{#ID#}')\" /><br /><input type=\"button\" value=\"向下移动\" onclick=\"DownOrder('HQB_MultiFile_{#ID#}')\" /></td></tr></table><script type=\"text/javascript\">MultiFileInit('HQB_MultiFile_{#ID#}','{#ID#}')</script>";
        /// <summary>
        /// 多图片
        /// </summary>
        private static string controlMultiImage = "<style  type=\"text/css\">.HQB_MultiFile_ButtonList input{margin-bottom:5px;}</style><table  class=\"HQB_MultiFile_ButtonList\"><tr><td valign=\"top\"><ul style=\"list-style:none; padding::0; margin:0; \"><li style=\"margin:0; padding:0;margin-bottom:5px;\"><input type=\"hidden\" id=\"{#ID#}\" value=\"{#Value#}\" name=\"{#ID#}\" /><select style=\" width:{#Width#}px; height:{#Height#}px;\" name=\"HQB_MultiFile_{#ID#}\" multiple=\"multiple\" id=\"HQB_MultiFile_{#ID#}\" ondblclick=\"return MultiFileItemModify('HQB_MultiFile_{#ID#}','{#ID#}')\" onchange=\"MultiFileSynchronousHideValue('HQB_MultiFile_{#ID#}','{#ID#}')\"></select></li><li style=\"margin:0; padding:0; display:{#Display#};\"><input type='button' onclick=\"InputImages('theForm', 'HQB_MultiFile_{#ID#}', 2, '{#FileSuffix#}', {#FileMaxSize#}, '{#ID#}',{#ThumbWidth#},{#ThumbHeight#},1,'{#ImageBestWidth#}','{#ImageBestHeight#}')\" value = '上传图片' /> <input type='button' onclick=\"ShowImages('{#ID#}', '<%=GetUploadImgUrl()%>','image',2)\" value = '预览图片'/></li></ul></td><td valign=\"top\"><input type=\"button\" onclick=\"MultiFileItemAdd('HQB_MultiFile_{#ID#}','{#ID#}')\" value=\"添加外部地址\" /><br /><input type=\"button\" value=\"修改选中\" onclick=\"MultiFileItemModify('HQB_MultiFile_{#ID#}','{#ID#}')\" /><br /><input type=\"button\" value=\"删除选中\" onclick=\"MultiFileItemDel('HQB_MultiFile_{#ID#}','{#ID#}')\"/><br /><input type=\"button\" value=\"向上移动\" onclick=\"UpOrder('HQB_MultiFile_{#ID#}')\" /><br /><input type=\"button\" value=\"向下移动\" onclick=\"DownOrder('HQB_MultiFile_{#ID#}')\" /></td></tr></table><script type=\"text/javascript\">MultiFileInit('HQB_MultiFile_{#ID#}','{#ID#}')</script>";
        /// <summary>
        /// 相册
        /// </summary>
        //private static string controlAlbums = "<ul id=\"{#Name#}AlbumsContainer\" style=\"list-style:none; width:650px;\"></ul><div style=\"clear:both; margin-left:120px;\">	<input type=\"hidden\" value=\"{#Value#}\" name=\"{#Name#}\" id=\"{#Name#}\" />	<span {#DESC#}>描述：<input type=\"text\" id=\"{#Name#}AlbumsNewTitle\"  style=\"width: 180px;\"> </span><input type=\"text\" id=\"{#Name#}AlbumsNewURL\"   readonly=\"readonly\"  style=\"width: 220px;\">    <input type=\"button\" value=\"上传图片\" id=\"{#Name#}AlbumsNewBtn\">	<script type=\"text/javascript\">	var {#Name#}UploadParam = {FormName:\"theForm\", ElementName:\"{#Name#}AlbumsNewURL\", ControlType:1, ExtType:\"{#FileSuffix#}\", MaxSize:{#FileMaxSize#}, GetSizeControl:\"\", ThumbWidth:{#ThumbWidth#}, ThumbHeight:{#ThumbHeight#}};	var albums{#Name#} = new Albums(\"{#Name#}\",{#Name#}UploadParam,{#IsTrue#});albums{#Name#}.init();	</script></div>";

        private static string controlAlbums = "<section class=\"xccontainer Albums_Del\"><ul id=\"{#Name#}AlbumsContainer\" class=\"AlbumsList ui-sortable\"></ul></section><div style=\"clear:left;padding-left:8px\">	<input type=\"hidden\" value=\"{#Value#}\" name=\"{#Name#}\" id=\"{#Name#}\" />	<input type=\"text\" id=\"{#Name#}AlbumsNewTitle\"  style=\"width: 180px;display:none;\"> <input type=\"text\" id=\"{#Name#}AlbumsNewURL\"   readonly=\"readonly\"  style=\"width: 220px;display:none\">    <input type=\"button\" value=\"上传图片\" id=\"{#Name#}AlbumsNewBtn\">	<script type=\"text/javascript\">$(function() { $('#{#Name#}AlbumsContainer').sortable({ start: function(event, ui) { ui.item.addClass('active');},stop: function(event, ui) {ui.item.removeClass('active').effect('highlight',{ color: '#000' }, 1000, function() {});}});});	var {#Name#}UploadParam = {FormName:\"theForm\", ElementName:\"{#Name#}AlbumsNewURL\", ControlType:3, ExtType:\"{#FileSuffix#}\", MaxSize:{#FileMaxSize#}, GetSizeControl:\"\", ThumbWidth:{#ThumbWidth#}, ThumbHeight:{#ThumbHeight#}, IsMult: 1, BestWidth: \"{#ImageBestWidth#}\", BestHeight: \"{#ImageBestHeight#}\"};	var albums{#Name#} = new Albums(\"{#Name#}\",{#Name#}UploadParam,{#IsTrue#});albums{#Name#}.init(\"{#Name#}\");	</script></div>";
        /// <summary>
        /// 相册可上传小图
        /// </summary>
        private static string controlHasThumbAlbums = "<ul id=\"{#Name#}AlbumsContainer\" style=\"list-style:none; width:650px;\"></ul><div style=\"clear:both; margin-left:120px;\"><input type=\"hidden\" value=\"{#Value#}\" name=\"{#Name#}\" id=\"{#Name#}\" /><span {#DESC#}>描述：<input type=\"text\" id=\"{#Name#}AlbumsNewTitle\"  style=\"width: 180px;\"></span><input type=\"text\" id=\"{#Name#}ThumbAlbumsNewURL\"   readonly=\"readonly\"  style=\"width: 180px;\">  <input type=\"button\" value=\"上传小图\" id=\"{#Name#}ThumbAlbumsNewBtn\"><input type=\"text\" id=\"{#Name#}AlbumsNewURL\"   readonly=\"readonly\"  style=\"width: 180px;\">  <input type=\"button\" value=\"上传大图\" id=\"{#Name#}AlbumsNewBtn\">		<script type=\"text/javascript\">	var {#Name#}UploadParam = {FormName:\"theForm\", ElementName:\"{#Name#}AlbumsNewURL\", ThumbElementName:\"{#Name#}ThumbAlbumsNewURL\",ControlType:1, ExtType:\"{#FileSuffix#}\", MaxSize:{#FileMaxSize#}, GetSizeControl:\"\", ThumbWidth:{#ThumbWidth#}, ThumbHeight:{#ThumbHeight#}};	var albums{#Name#} = new Albums(\"{#Name#}\",{#Name#}UploadParam,{#IsTrue#});albums{#Name#}.init();</script></div>";
        /// <summary>
        ///  控件父容器
        /// </summary>
        private static string containerTemplate = "<dl>{#Content#}</dl>";
        /// <summary>
        ///  控件项标题容器
        /// </summary>
        private static string containerItemHeaderTemplate = "<dt>{#Content#}</dt>";
        /// <summary>
        ///  控件项内容容器
        /// </summary>
        private static string containerItemTemplate = "<dd>{#Content#}</dd>";
        /// <summary>
        ///  搜索控件父容器
        /// </summary>
        private static string searchContainerTemplate = "{#Content#}";
        /// <summary>
        ///  搜索控件项标题容器
        /// </summary>
        private static string searchContainerItemHeaderTemplate = "<li>{#Content#}</li>";
        /// <summary>
        ///  搜索控件项内容容器
        /// </summary>
        private static string searchContainerItemTemplate = "<li>{#Content#}</li>";
        /// <summary>
        ///  内容替换标签
        /// </summary>
        private static string containerContent = "{#Content#}";
        /// <summary>
        /// 日期控件
        /// </summary>
        private static string controlDate = "<input id=\"{#ID#}\"  value=\"{#Value#}\"   name=\"{#ID#}\" style=\"width:{#Width#}px;\"  class=\"Wdate\" type=\"text\" onFocus=\"WdatePicker({lang:'<%=Resources.Model.DateLang%>',skin:'whyGreen',dateFmt:'{#DateFormat#}'})\"/>";
        /// <summary>
        /// 日期控件的格式
        /// </summary>
        private static string controlDateFormat = "{#DateFormat#}";
        /// <summary>
        /// 文本框
        /// </summary>
        private static string controlTextBox = "<input {#Validate#}  type=\"text\" value=\"{#Value#}\" style=\"width:{#Width#}px;\"  maxlength=\"{#MaxLength#}\" name=\"{#Name#}\" id=\"{#ID#}\" />";

        /// <summary>
        /// 密码框
        /// </summary>
        private static string controlTextPossWord = "<input {#Validate#}  type=\"password\" value=\"{#Value#}\" style=\"width:{#Width#}px;\"  maxlength=\"{#MaxLength#}\" name=\"{#Name#}\" id=\"{#ID#}\" />";

        /// <summary>
        /// 文本域
        /// </summary>
        private static string controlTextArea = "<textarea style=\"height:{#Height#}px;width:{#Width#}px;\"  {#Validate#}  id=\"{#ID#}\"  name=\"{#Name#}\">{#Value#}</textarea>";
        /// <summary>
        /// 表单隐藏控件
        /// </summary>
        private static string controlHidden = "<input type=\"hidden\" value=\"{#Value#}\" id=\"{#ID#}\"  name=\"{#Name#}\" />";
        /// <summary>
        /// 单选按钮
        /// </summary>
        private static string controlRadio = "<input  type=\"radio\" {#Selected#}  name=\"{#Name#}\" value=\"{#Value#}\" />{#Text#}";
        /// 复选按钮
        /// </summary>
        private static string controlCheckBox = "<input {#Selected#} type=\"checkbox\" name=\"{#Name#}\" value=\"{#Value#}\" />{#Text#}";
        /// <summary>
        /// 下拉列表项
        /// </summary>
        private static string controlSelectItem = "<option  {#Selected#} value=\"{#Value#}\">{#Text#}</option>";
        /// <summary>
        /// 下拉单选列表
        /// </summary>
        //private static string controlSelect = "<select  id=\"{#ID#}\" name=\"{#Name#}\">{#Content#}</select>"; 原
        private static string controlSelect = "<select  id=\"{#Name#}\" name=\"{#Name#}\">{#Content#}</select>";
        /// <summary>
        /// 下拉多选列表
        /// </summary>
        //private static string controlMutiSelect = "<select  id=\"{#ID#}\"  multiple=\"multiple\" name=\"{#Name#}\">{#Content#}</select>";
        private static string controlMutiSelect = "<select  id=\"{#Name#}\"  multiple=\"multiple\" name=\"{#Name#}\">{#Content#}</select>";
        /// <summary>
        ///  input控件value
        /// </summary>
        private static string controlValue = "{#Value#}";
        /// <summary>
        ///  input 选项是否选中 1 选中 0 不选中
        /// </summary>
        private static string controlSelected = "{#Selected#}";
        /// <summary>
        ///  input控件name
        /// </summary>
        private static string controlName = "{#Name#}";
        /// <summary>
        ///  input控件ID
        /// </summary>
        private static string controlID = "{#ID#}";
        /// <summary>
        /// input控件text,显示文本
        /// </summary>
        private static string controlText = "{#Text#}";
        /// <summary>
        /// input控件width 宽度
        /// </summary>
        private static string controlWidth = "{#Width#}";
        /// <summary>
        /// input控件height 高度
        /// </summary>
        private static string controlHeight = "{#Height#}";
        /// <summary>
        /// input控件maxlength 最大字符数
        /// </summary>
        private static string controlMaxLength = "{#MaxLength#}";
        /// <summary>
        /// 控件验证代码
        /// </summary>
        private static string controlValidate = "{#Validate#}";
        /// <summary>
        /// 控件显示状态
        /// </summary>
        private static string controlDisplay = "{#Display#}";
        #endregion

        #region 构造函数
        public ParseModel(string modelID,ParserType type)
        {
            this.hsModel = new Hashtable();
            this.sbFieldParam = new StringBuilder();

            this.modelID = modelID;
            this.Init();
            this.tableReferenceInfo = this.hsModel["CustomCol"].ToString();
            this.modelType = type;
            this._pageName = CutTableNamePreFix(this.hsModel["TableName"].ToString());
        }
        #endregion

        #region 属性
        /// <summary>
        /// 生成的模型页文件名
        /// </summary>
        public string PageName
        {
            get { return this._pageName; }
        }
        #endregion

        #region 初始数据加载
        // 初始数据
        private void Init()
        {
            ModelManage model;
            SelectParams selParams;
            DataTable modelTB;

            model = new ModelManage();
            selParams = new SelectParams();

            selParams.S1 = this.modelID;
            selParams.I1 = 1;
            modelTB = model.GetList("ONE", selParams);
            this.dtField = dal.GetField(this.modelID);
            this.rootUrl = GetRootUrl();

            if (modelTB.Rows.Count > 0)
            {
                foreach (DataColumn item in modelTB.Columns)
                {
                    this.hsModel.Add(item.ColumnName, modelTB.Rows[0][item.ColumnName]);
                }
            }

            if (this.hsModel.Count > 0)
            {
                this.hsModel["ListLink"] = this.hsModel["ListLink"].ToString().Replace("{WebSite}", this.rootUrl);
                this.hsModel["ListButton"] = this.hsModel["ListButton"].ToString().Replace("{WebSite}", this.rootUrl);
            }
            this.showColumn = "," + this.hsModel["TableName"].ToString() + "." + "ID," + this.hsModel["TableName"].ToString() + ".FlowState,";
            SetKeepUrlParam();  // 设置要传递的URL参数
        }
        #endregion

        #region 设置要传递的URL参数
        private void SetKeepUrlParam()
        {
            string itemUrlParm;

            itemUrlParm = "";
            this.keepUrlParam = "";

            if (!string.IsNullOrEmpty(this.hsModel["DeliverUrlParam"].ToString()) || !string.IsNullOrEmpty(this.hsModel["DeliverAndSearchUrlParam"].ToString())) // 添加需要传递的URL参数
            {
                string[] arrUrlParam;
                arrUrlParam = this.hsModel["DeliverAndSearchUrlParam"].ToString().Split(new char[] { ',' });    // 需传递且参与查询的参数字段


                foreach (string param in arrUrlParam)
                {
                    if (!string.IsNullOrEmpty(param.Trim()))
                    {
                        itemUrlParm = param + "=<%=ctrManageList.KeepParamValue[\"" + param + "\"]%>";
                        if (!keepUrlParam.Contains(itemUrlParm))
                        {
                            keepUrlParam += "&" + itemUrlParm;
                        }
                    }
                }

                arrUrlParam = this.hsModel["DeliverUrlParam"].ToString().Split(new char[] { ',' });             // 需传递的参数

                foreach (string param in arrUrlParam)
                {
                    if (!string.IsNullOrEmpty(param))
                    {
                        itemUrlParm = param + "=<%=ctrManageList.KeepParamValue[\"" + param + "\"]%>";
                        if (!keepUrlParam.Contains(itemUrlParm))
                        {
                            keepUrlParam += "&" + itemUrlParm;
                        }
                    }
                }
            }
        }
        #endregion

        #region 创建模型
        /// <summary>
        /// 创建模型
        /// </summary>
        public void Create()
        {
            CreateList();   // 创建列表页
            CreateEdit();   // 创建编辑页
            CreateView();   // 创建浏览页
        }
        #endregion

        #region 除去表名前缀
        /// <summary>
        /// 除去表名前缀
        /// </summary>
        /// <param name="strContent">原操作内容</param>
        /// <returns></returns>
        public string CutTableNamePreFix(string strContent)
        {
            strContent = strContent.Replace("K_U_", "");
            strContent = strContent.Replace("K_F_", "");
            strContent = strContent.Replace("K_G_", "");

            return strContent;
        }
        #endregion
    }

    #region 类型

    #region 解析类型
    public enum ParserType
    {
        /// <summary>
        /// 内容模型
        /// </summary>
        Content = 1,
        /// <summary>
        /// 自定义表单
        /// </summary>
        Form,
        /// <summary>
        /// 子模型
        /// </summary>
        SubModel
    }
    #endregion

    #endregion
}