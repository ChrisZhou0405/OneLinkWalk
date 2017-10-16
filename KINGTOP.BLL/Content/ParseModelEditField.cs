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
using System.Xml;
using System.Xml.XPath;

using KingTop.Common;
using KingTop.Model;
using KingTop.IDAL;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-04-9
// 功能描述：解析模型字段 -- 模型编辑页 -- 基本字段解析
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region  编辑页解析 -- 基本字段
        // 编辑页解析
        public bool IsUeditorFirst = true;
        private void EditBasicField(ref StringBuilder sbFields, DataRowView dr)
        {
            //基本字段解析
            if (!string.IsNullOrEmpty(dr["BasicField"].ToString()))
            {
                string basicFieldType;  // 字段类型    

                basicFieldType = dr["BasicField"].ToString();
                switch (basicFieldType)
                {
                    case "1":           // 单行文本
                    case "8":           // 数字
                    case "9":           // 货币
                        ParseText(dr, ref sbFields, controlTextBox, false, false);
                        break;
                    case "10":          // 日期
                        ParseText(dr, ref sbFields, controlDate, true, false);
                        break;
                    case "2":           // 多行文本（文本域）
                        ParseText(dr, ref sbFields, controlTextArea, false, false);
                        break;
                    case "3":           // 多行文本（编辑器）
                        switch (dr["EditorType"].ToString())
                        {
                            case "1":   // CKEditor编辑器
                                if (!sbFields.ToString().Contains("<%KingTop.Config.UploadConfig uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath); %>"))
                                {
                                    sbFields.Append("<%KingTop.Config.UploadConfig uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath); %>");
                                }
                                switch (dr["EditorStyle"].ToString())
                                {
                                    case "1":   // 标准型
                                        ParseText(dr, ref sbFields, controlCKEditor, false, false);
                                        break;
                                    case "2":   // Mini型
                                        ParseText(dr, ref sbFields, controlCKEditroMini, false, false);
                                        break;
                                    default:
                                        ParseText(dr, ref sbFields, controlCKEditor, false, false);
                                        break;
                                }
                                break;
                            case "2":   // eWebEditor编辑器
                                ParseText(dr, ref sbFields, controlEWebEditor.Replace("{#EditorStyle#}", dr["EditorStyle"].ToString()), false, true);
                                break;
                            case "3":   //ueditor
                                string firstEditor = controlUEditorFirst;
                                if (!IsUeditorFirst)
                                {
                                    firstEditor = controlUEditor;
                                }
                                ParseText(dr, ref sbFields, firstEditor, false, false);

                                IsUeditorFirst = false;
                                break;
                            default:
                                if (!sbFields.ToString().Contains("<%KingTop.Config.UploadConfig uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath); %>"))
                                {
                                    sbFields.Append("<%KingTop.Config.UploadConfig uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath); %>");
                                }
                                ParseText(dr, ref sbFields, controlCKEditor, false, false);
                                break;
                        }
                        break;
                    case "4":           // 单选（单选按钮）
                        ParseItems(dr, ref sbFields, null, controlRadio, "checked='checked'");
                        break;
                    case "5":           // 多选（复选框）
                        ParseItems(dr, ref sbFields, null, controlCheckBox, "checked='checked'");
                        break;
                    case "6":           // 下拉列表（单选）
                        ParseItems(dr, ref sbFields, controlSelect, controlSelectItem, "selected='selected'");
                        break;
                    case "7":           // 下拉列表（多选）
                        ParseItems(dr, ref sbFields, controlMutiSelect, controlSelectItem, "selected='selected'");
                        break;
                    case "11":          // 图片
                    case "12":          // 文件
                        ParseUpload(dr, ref sbFields, basicFieldType);
                        break;
                    case "13":
                        ParseText(dr, ref sbFields, controlHidden, false, false);
                        break;
                    case "14":          // 子模型
                        ParseSubModelField(dr, ref sbFields);
                        break;
                    case "15":           // 密码
                        ParseText(dr, ref sbFields, controlTextPossWord, false, false);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 解析子模型
        private void ParseSubModelField(DataRowView dr, ref StringBuilder sbFields)
        {
            string controlHtml;
            string controlItem;

            SetFieldParam(dr["Name"].ToString(), "", "14");
            controlHtml = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());
            controlItem = "<select name=\"" + dr["Name"].ToString() + "\" id=\"" + dr["Name"].ToString() + "\" onchange=\"LoadSubModelField('" + dr["Name"].ToString() + "','<%=Request.QueryString[\"ID\"]%>')\"></select>";
            controlItem = containerItemTemplate.Replace(containerContent, controlItem);
            controlHtml = controlHtml + controlItem;
            controlHtml = containerTemplate.Replace(containerContent, controlHtml);
            controlHtml = controlHtml + "<div id=\"SubModel" + dr["Name"].ToString() + "\"></div>";
            controlHtml += "<script type=\"text/javascript\">LoadSubModel(\"" + dr["Name"].ToString() + "\",\"" + dr["SubModelGroupID"].ToString() + "\",\"<%=hsFieldValue[\"" + dr["name"] + "\"]%>\",\"<%=Request.QueryString[\"ID\"]%>\")</script>";

            sbFields.Append(controlHtml);
        }
        #endregion

        #region 解析选项类字段 -- 单选、多选、列表
        // template 为父标签模板如<select>标签模板
        // itemTemplate 为子标签模块如 <option>
        private void ParseItems(DataRowView dr, ref StringBuilder sbFields, string template, string itemTemplate, string selected)
        {
            string itemHtml;                                           // 临时变量，选项控件HTML代码
            string controlHtml;                                        // 保存当前字段的所有HTML代码

            SetFieldParam(dr);                                         // 保存字段参数

            if (string.Equals(dr["DropDownDataType"], "1"))             // 手工输入
            {
                string[] arrItemList;   // 值对字符串数组
                string[] arrItem;       // 保存按钮的值与文本
                string itemTemp;      // 临时变量,保存含有判断是否选中条件的代码

                controlHtml = null;
                arrItemList = dr["OptionsValue"].ToString().Split(new char[] { ',' });

                foreach (string strItem in arrItemList)
                {
                    if (!string.IsNullOrEmpty(strItem.Trim()))
                    {
                        arrItem = strItem.Split(new char[] { '|' });
                        if (arrItem.Length > 0)
                        {
                            itemHtml = itemTemplate;
                            itemHtml = itemHtml.Replace(controlText, arrItem[0]);
                            itemHtml = itemHtml.Replace(controlValue, arrItem[1]);
                            itemHtml = itemHtml.Replace(controlName, dr["name"].ToString());

                            switch (this.modelType)
                            {
                                case ParserType.Content:
                                    itemTemp = "<%if(hsFieldValue[\"" + dr["name"] + "\"] != null && hsFieldValue[\"" + dr["name"] + "\"].ToString().Contains(\"" + arrItem[1] + "\")) {Response.Write(\"" + selected + "\");}%>"; // 加载选项初始值
                                    //修改Contains的方法，包含会有ID值重复，例如：1、11、111   2014年6月25日 By BiLY
                                    //itemTemp = "<%if(hsFieldValue[\"" + dr["name"] + "\"] != null && hsFieldValue[\"" + dr["name"] + "\"].ToString()==\"" + arrItem[1] + "\") {Response.Write(\"" + selected + "\");}%>"; // 加载选项初始值
                                    break;
                                default:
                                    itemTemp = string.Empty;
                                    break;
                            }

                            itemTemp = itemHtml.Replace(controlSelected, itemTemp);
                            controlHtml = controlHtml + itemTemp;
                        }
                    }
                }

                // 选项是否为空
                if (!string.IsNullOrEmpty(controlHtml))
                {
                    // 有父标签模板如<select>则加父标签
                    if (!string.IsNullOrEmpty(template))
                    {
                        controlHtml = template.Replace(containerContent, controlHtml);
                        controlHtml = controlHtml.Replace(controlName, dr["name"].ToString());
                        controlHtml = controlHtml.Replace(controlID, dr["Name"].ToString());
                    }

                    if (Utils.ParseBool(dr["IsRequired"].ToString()))                                                  // 是否必填项
                    {
                        controlHtml = controlHtml + isRequiredTag;
                    }

                    controlHtml += dr["Message"].ToString();                                                           // 附加提示信息
                    controlHtml = containerItemTemplate.Replace(containerContent, controlHtml);                        // 添加容器如<li>
                    itemHtml = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());     // 添加标题
                    controlHtml = itemHtml + controlHtml;
                }

                controlHtml = containerTemplate.Replace(containerContent, controlHtml);
                sbFields.Append(controlHtml);
            }
            else // 来源于数据库
            {
                string hsObj;                                                                            //  控件选项数据集      
                string itemTemp;                                                                         // 临时变量,保存含有判断是否选中条件的代码

                hsObj = "ctrManageEdit.FieldBind(\""
                            + dr["DropDownTable"].ToString() + "\", \""                                  // 引用表名
                            + dr["DropDownTextColumn"].ToString() + "\", \""                             // 控件text绑定字段名
                            + dr["DropDownValueColumn"].ToString() + "\", \""                            // 控件value绑定字段名
                            + dr["DropDownSqlWhere"].ToString() + "\")";                                 // 选择条件  
                //控件选项
                itemHtml = itemTemplate.Replace(controlName, dr["name"].ToString());
                itemHtml = itemHtml.Replace(controlValue, "<%= de.Key%>");                        // 加载值 
                itemHtml = itemHtml.Replace(controlText, "<%=de.Value%>");          // 加载显示文本 
                itemTemp = "<%if(hsFieldValue[\"" + dr["name"].ToString() + "\"] != null && hsFieldValue[\"" + dr["name"].ToString() + "\"].ToString().Contains(de.Key.ToString())) {Response.Write(\"" + selected + "\");}%>";   // 是否选中
                //修改Contains的方法，包含会有ID值重复，例如：1、11、111   2014年6月25日 By BiLY
                //itemTemp = "<%if(hsFieldValue[\"" + dr["name"].ToString() + "\"] != null &&hsFieldValue[\"" + dr["name"].ToString() + "\"].ToString().Contains(de.Key.ToString())) {Response.Write(\"" + selected + "\");}%>";   // 是否选中，修改Contains的方法，包含会有ID值重复，例如：1、11、111   2014年6月25日
                controlHtml = "<%foreach (DictionaryEntry de in " + hsObj + "){%>";
                controlHtml = controlHtml + itemHtml.Replace(controlSelected, itemTemp);
                controlHtml = controlHtml + " <% } %> ";

                // 若设置了父标签模板则添加如<select>
                if (!string.IsNullOrEmpty(template))
                {
                    controlHtml = template.Replace(containerContent, controlHtml);
                    controlHtml = controlHtml.Replace(controlName, dr["name"].ToString());
                }

                if (Utils.ParseBool(dr["IsRequired"].ToString()))                                             // 是否必填项
                {
                    controlHtml = controlHtml + isRequiredTag;
                }

                controlHtml += dr["Message"].ToString();                                                       // 附加提示信息
                controlHtml = containerItemTemplate.Replace(containerContent, controlHtml);                    // 添加容器标签如<li>
                itemHtml = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString()); // 添加搜索项标题
                controlHtml = itemHtml + controlHtml;
                controlHtml = containerTemplate.Replace(containerContent, controlHtml);                         //添加容器标签如<ul>
                sbFields.Append(controlHtml);
            }
        }
        #endregion

        #region 文本框、文本域
        // textTemplate 文本框或文本域HTML模板
        // isDate 表示是否是日期类型
        private void ParseText(DataRowView dr, ref StringBuilder sbFields, string textTemplate, bool isDate, bool IsEwebeditor)
        {
            string itemHtml;        // 临时变量
            string controlHtml;     // 保存当前字段的所有HTML代码
            string fieldValue;      // 文本框（域）的值
            if (IsEwebeditor)
            {
                fieldValue = "<%=System.Web.HttpContext.Current.Server.HtmlEncode(hsFieldValue[\"" + dr["name"] + "\"].ToString())%>";                                   // hsFieldValue为HashTable类型
            }
            else
            {
                fieldValue = "<%=FormatInputValue(hsFieldValue[\"" + dr["name"] + "\"].ToString())%>";                                   // hsFieldValue为HashTable类型
            }



            controlHtml = textTemplate.Replace(controlName, dr["Name"].ToString());                     // 文本(域)框name属性
            controlHtml = controlHtml.Replace(controlID, dr["Name"].ToString());                        // 文本(域)框id属性

            #region ueditor
            int txtBoxWidth = KingTop.Common.Utils.ParseInt(dr["TextBoxWidth"].ToString(), 0);
            int txtBoxHeight = KingTop.Common.Utils.ParseInt(dr["TextBoxHieght"].ToString(), 0);
            string minFrameHeight = "";
            //判断是否为ueditor编辑器
            if (textTemplate.Contains("baidu.editor.ui.Editor({toolbars"))  //mini编辑器
            {
                if (txtBoxWidth < 650)
                    txtBoxWidth = 650;
                if (txtBoxHeight < 120)
                    txtBoxHeight = 120;

                //需重新计算编辑器高度，否则会显示错乱
                minFrameHeight = (txtBoxHeight - 50).ToString();
                controlHtml = controlHtml.Replace(controlWidth, txtBoxWidth.ToString());             // 文本(域)框宽度width属性
                controlHtml = controlHtml.Replace(controlHeight, txtBoxHeight.ToString());
                controlHtml = controlHtml.Replace("{#minFrameHeight#}", minFrameHeight);
            }
            else if (textTemplate.Contains("baidu.editor.ui.Editor"))  //ueditor编辑器
            {
                if (txtBoxWidth < 610)
                    txtBoxWidth = 610;
                if (txtBoxHeight < 180)
                    txtBoxHeight = 180;

                //需重新计算编辑器高度，否则会显示错乱
                minFrameHeight = (txtBoxHeight - 110).ToString();
                controlHtml = controlHtml.Replace(controlWidth, txtBoxWidth.ToString());             // 文本(域)框宽度width属性
                controlHtml = controlHtml.Replace(controlHeight, txtBoxHeight.ToString());
                controlHtml = controlHtml.Replace("{#minFrameHeight#}", minFrameHeight);
            }
            #endregion

            controlHtml = controlHtml.Replace(controlWidth, dr["TextBoxWidth"].ToString());             // 文本(域)框宽度width属性
            controlHtml = controlHtml.Replace(controlHeight, dr["TextBoxHieght"].ToString());

            if (!string.IsNullOrEmpty(dr["TextBoxMaxLength"].ToString()) && Utils.ParseInt(dr["TextBoxMaxLength"].ToString(), 0) > 0)  // 最大输入字符数有限制
            {
                controlHtml = controlHtml.Replace(controlMaxLength, dr["TextBoxMaxLength"].ToString());     // 文本(域)框最大输入字符数
            }
            else // 无限制
            {
                controlHtml = controlHtml.Replace(controlMaxLength, "10000");
            }

            if (isDate)                                                                                  // 日期类型处理
            {
                string dateFormat;   // 日期格式
                string defaultType;  // 日期缺省值类型
                string defaultValue; // 日期缺省值

                defaultType = dr["DateDefaultOption"].ToString();
                dateFormat = GetDateFormat(dr["DateFormatOption"].ToString());
                controlHtml = controlHtml.Replace(controlDateFormat, dateFormat);
                defaultValue = "none";

                switch (this.modelType)
                {
                    case ParserType.Content:
                        switch (defaultType)
                        {
                            case "2":      // 当前日期
                                defaultValue = "DateTime.Now.ToString('" + dateFormat + "')";
                                SetFieldParam(dr["Name"].ToString(), defaultValue, dr["BasicField"].ToString());
                                controlHtml = controlHtml.Replace(controlValue, "<%if(hsFieldValue[\"" + dr["name"] + "\"].ToString().Equals(\"" + defaultValue + "\")){Response.Write(" + defaultValue.Replace("'", "\"") + ");}else{Response.Write(string.Format(\"{0:" + dateFormat + "}\",hsFieldValue[\"" + dr["name"] + "\"]));}%>");
                                break;
                            case "3":     // 使用输入值
                                defaultValue = "string.Format('{0:" + dateFormat + "}','" + dr["DefaultValue"].ToString() + "')";
                                SetFieldParam(dr["Name"].ToString(), defaultValue, dr["BasicField"].ToString());
                                controlHtml = controlHtml.Replace(controlValue, "<%if(hsFieldValue[\"" + dr["name"] + "\"].ToString().Equals(\"" + defaultValue + "\")){Response.Write(" + defaultValue.Replace("'", "\"") + ");}else{Response.Write(string.Format(\"{0:" + dateFormat + "}\",hsFieldValue[\"" + dr["name"] + "\"]));}%>");
                                break;
                            default:
                                SetFieldParam(dr["Name"].ToString(), defaultValue, dr["BasicField"].ToString());
                                controlHtml = controlHtml.Replace(controlValue, "<%if(hsFieldValue[\"" + dr["name"] + "\"].ToString().Equals(\"" + defaultValue + "\")){Response.Write(\"\");}else{Response.Write(string.Format(\"{0:" + dateFormat + "}\",hsFieldValue[\"" + dr["name"] + "\"]));}%>");
                                break;
                        }

                        controlHtml = controlHtml.Replace(controlValidate, "");                                    // 无验证
                        break;
                    default:
                        controlHtml = controlHtml.Replace(controlValidate, "");                                    // 无验证
                        controlHtml = controlHtml.Replace(controlValue, "");                                       // 初始值
                        break;
                }
            }
            else
            {
                SetFieldParam(dr);                                                                         // 保存字段参数

                switch (this.modelType)
                {
                    case ParserType.Content:
                        controlHtml = controlHtml.Replace(controlValue, fieldValue);                       // 文本(域)框值
                        break;
                    case ParserType.Form:
                        controlHtml = controlHtml.Replace(controlValue, string.Empty);                    // 文本(域)框值
                        break;
                    default:
                        controlHtml = controlHtml.Replace(controlValue, string.Empty);                     // 文本(域)框值
                        break;
                }
                controlHtml = controlHtml.Replace(controlValidate, AddControlValidate(dr));                // 添加验证
            }

            if (Utils.ParseBool(dr["IsRequired"].ToString()))                                              // 是否必填项
            {
                controlHtml = controlHtml + isRequiredTag;
            }

            controlHtml += dr["Message"].ToString();                                                       // 附加提示信息
            controlHtml = containerItemTemplate.Replace(containerContent, controlHtml);                    // 加父容器如<Li>
            itemHtml = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString()); // 添加标题
            controlHtml = itemHtml + controlHtml;
            controlHtml = containerTemplate.Replace(containerContent, controlHtml);                        // 添加容器如<ul>
            sbFields.Append(controlHtml);
        }
        #endregion

        #region 解析日期格式
        private string GetDateFormat(string dateFormat)
        {
            string ctrDateFormat;

            switch (dateFormat)
            {
                case "1":
                    ctrDateFormat = "yyyy-MM-dd";
                    break;
                case "2":
                    ctrDateFormat = "H:mm:ss";
                    break;
                case "3":
                    ctrDateFormat = "yyyy-MM-dd HH:mm:ss";
                    break;
                default:
                    ctrDateFormat = "yyyy-MM-dd HH:mm:ss";
                    break;
            }

            return ctrDateFormat;
        }
        #endregion

        #region 上传
        private void ParseUpload(DataRowView dr, ref StringBuilder sbFields, string fieldType)
        {
            bool isUpload;          // 是否启用上传
            bool isMultiFile;       // 是否启用多文件
            bool isAlbums;          // 是否相册
            string itemHtml;        // 临时变量
            string controlHtml;     // 保存当前字段的所有HTML代码
            string fieldValue;      // 文本框（域）的值
            string disabledValue;   // 判断按钮是否可用

            SetFieldParam(dr);                                                                   // 保存字段参数
            isUpload = Utils.ParseBool(dr["IsUpload"].ToString());
            isMultiFile = Utils.ParseBool(dr["IsMultiFile"].ToString());
            isAlbums = Utils.ParseBool(dr["IsAlbums"].ToString());
            fieldValue = "<%=hsFieldValue[\"" + dr["name"] + "\"]%>";                            // hsFieldValue为HashTable类型
            disabledValue = "hsFieldValue[\"" + dr["name"] + "\"]";

            controlHtml = null;

            if (isMultiFile) // 多文件
            {
                if (fieldType == "11")  // 多图片
                {
                    if (isAlbums) // 相册
                    {
                        if (Common.Utils.ParseBool(dr["IsAlbumsUploadThumb"].ToString()))
                        {
                            controlHtml = controlHasThumbAlbums.Replace(controlName, dr["name"].ToString());
                            controlHtml = controlHtml.Replace("{#IsTrue#}", "true");
                        }
                        else
                        {
                            controlHtml = controlAlbums.Replace(controlName, dr["name"].ToString());
                            controlHtml = controlHtml.Replace("{#IsTrue#}", "false");
                        }

                        if (Common.Utils.ParseBool(dr["IsAlbumsDesc"].ToString()))
                        {
                            controlHtml = controlHtml.Replace("{#DESC#}", "");
                        }
                        else
                        {
                            controlHtml = controlHtml.Replace("{#DESC#}", " style=\"display:none;\" ");
                        }
                    }
                    else
                    {
                        controlHtml = controlMultiImage.Replace(controlName, dr["name"].ToString());         // 控件name属性
                    }
                }
                else  // 多文件
                {
                    controlHtml = controlMultiFile.Replace(controlName, dr["name"].ToString());         // 控件name属性
                }

                controlHtml = controlHtml.Replace("{#ControlGetSize#}", "");                            // 不保存文件大小

            }
            else
            {
                if (fieldType == "11") // 图片
                {
                    controlHtml = controlSingleImage.Replace(controlName, dr["name"].ToString());        // 控件name属性
                }
                else  // 单个文件
                {
                    controlHtml = controlSingleFile.Replace(controlName, dr["name"].ToString());        // 控件name属性
                }

                if (Utils.ParseBool(dr["IsSaveFileSize"].ToString()))                               // 是否保存文件大小
                {
                    itemHtml = controlHidden.Replace(controlName, dr["SaveFileName"].ToString());
                    itemHtml = itemHtml.Replace("<input", "<input id=\"" + dr["SaveFileName"].ToString() + "\" ");
                    itemHtml = itemHtml.Replace(controlValue, "<%=hsFieldValue[\"" + dr["SaveFileName"].ToString() + "\"]%>");
                    SetFieldParam(dr["SaveFileName"].ToString(), "0", "8");                      //  保存文件大小字段
                    controlHtml = controlHtml.Replace("{#ControlGetSize#}", dr["SaveFileName"].ToString());
                    controlHtml = itemHtml + controlHtml;
                }
                else
                {
                    controlHtml = controlHtml.Replace("{#ControlGetSize#}", "");
                }
            }

            controlHtml = controlHtml.Replace(controlName, dr["name"].ToString());
            controlHtml = controlHtml.Replace(controlID, dr["name"].ToString());                       // 控件id属性
            controlHtml = controlHtml.Replace(controlWidth, dr["TextBoxWidth"].ToString());            // 控件宽度width属性
            controlHtml = controlHtml.Replace(controlHeight, dr["TextBoxHieght"].ToString());          // 控件height属性值,只有为多文件时才起作用
            controlHtml = controlHtml.Replace(controlMaxLength, dr["TextBoxMaxLength"].ToString());    // 文本框最大输入字符数
            controlHtml = controlHtml.Replace("{#IsDisabledValue#}", disabledValue);
            controlHtml = controlHtml.Replace("{#ImageBestWidth#}", dr["ImageBestWidth"].ToString());
            controlHtml = controlHtml.Replace("{#ImageBestHeight#}", dr["ImageBestHeight"].ToString());
            switch (this.modelType)
            {
                case ParserType.Content:
                    controlHtml = controlHtml.Replace(controlValue, fieldValue);                        // 文本框值
                    break;
                case ParserType.Form:
                    controlHtml = controlHtml.Replace(controlValue, string.Empty);                      // 文本框值
                    break;
                default:
                    controlHtml = controlHtml.Replace(controlValue, string.Empty);                      // 文本框值
                    break;
            }

            controlHtml = controlHtml.Replace(controlValidate, AddControlValidate(dr));                // 添加验证
            controlHtml = controlHtml.Replace("{#FileSuffix#}", dr["ImageType"].ToString());           // 允许的文件类型
            controlHtml = controlHtml.Replace("{#FileMaxSize#}", dr["MaxSize"].ToString());            // 允许上传文件的最大大小(KB)

            if (fieldType == "11")       // 图片
            {
                if (!string.IsNullOrEmpty(dr["ThumbWidth"].ToString()))
                {
                    controlHtml = controlHtml.Replace("{#ThumbWidth#}", dr["ThumbWidth"].ToString());
                }
                else
                {
                    controlHtml = controlHtml.Replace("{#ThumbWidth#}", "0");
                }

                if (!string.IsNullOrEmpty(dr["ThumbHeight"].ToString()))
                {
                    controlHtml = controlHtml.Replace("{#ThumbHeight#}", dr["ThumbHeight"].ToString());
                }
                else
                {
                    controlHtml = controlHtml.Replace("{#ThumbHeight#}", "0");
                }
            }
            else if (fieldType == "12")   // 文件
            {
                controlHtml = controlHtml.Replace("{#FileType#}", "file");
            }

            if (isUpload)   // 启用上传
            {
                controlHtml = controlHtml.Replace(controlDisplay, "block");
            }
            else
            {
                controlHtml = controlHtml.Replace(controlDisplay, "none");
            }

            if (Utils.ParseBool(dr["IsRequired"].ToString()))                                             // 是否必填项
            {
                controlHtml = controlHtml + isRequiredTag;
            }

            //if (!isAlbums)
            //{
            controlHtml += dr["Message"].ToString();                                                      // 附加提示信息
            controlHtml = containerItemTemplate.Replace(containerContent, controlHtml);                   // 添加容器标签如<li>
            itemHtml = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());// 标题
            controlHtml = itemHtml + controlHtml;
            controlHtml = containerTemplate.Replace(containerContent, controlHtml);                       // 添加容器标签如<ul>
            //}

            sbFields.Append(controlHtml);
        }
        #endregion

        #region 设置字段参数，用于编辑页中操作
        // dr 当前字段记录
        private void SetFieldParam(DataRowView dr)
        {
            string fieldParam;

            fieldParam = dr["name"] + "|" + dr["DefaultValue"] + "|" + dr["BasicField"] + ",";

            if (!this.sbFieldParam.ToString().Contains(fieldParam))
            {
                this.sbFieldParam.Append(fieldParam);
            }
            if (Utils.ParseInt(dr["BasicField"].ToString(), 0) == 3 && !(htmlFieldParam + ",").Contains("_" + dr["name"].ToString() + ","))
            {
                if (!string.IsNullOrEmpty(htmlFieldParam))
                {
                    htmlFieldParam += ",";
                }
                htmlFieldParam += "editor_" + dr["name"];
            }
        }

        private void SetFieldParam(string fieldName, string defaultValue, string basicType)
        {
            string fieldParam;

            fieldParam = fieldName + "|" + defaultValue + "|" + basicType + ",";

            if (!this.sbFieldParam.ToString().Contains(fieldParam))
            {
                this.sbFieldParam.Append(fieldParam);
            }

            if (Utils.ParseInt(basicType, 0) == 3 && !(htmlFieldParam + ",").Contains("_" + fieldName + ","))
            {
                if (!string.IsNullOrEmpty(htmlFieldParam))
                {
                    htmlFieldParam += ",";
                }
                htmlFieldParam += "editor_" + fieldName;
            }
        }
        #endregion

        #region 为控件添加验证
        private string AddControlValidate(DataRowView dr)
        {
            bool isRequired;            // 当前字段是否必填
            string validateCode;        // 验证代码

            isRequired = Utils.ParseBool(dr["IsRequired"].ToString());
            validateCode = null;

            switch (dr["BasicField"].ToString())
            {
                case "1":   // 单文本
                case "2":   // 多文本
                case "3":
                    if (!string.Equals(dr["ValidationType"].ToString(), "-1") && !string.IsNullOrEmpty(dr["ValidationType"].ToString()))    // 自定义验证
                    {
                        if (isRequired)
                        {
                            validateCode = " class=\"validate[required,regex[" + dr["TextBoxValidation"] + "," + dr["ValidationMessage"] + "]]\"";
                        }
                        else
                        {
                            validateCode = " class=\"validate[optional,regex[" + dr["TextBoxValidation"] + "," + dr["ValidationMessage"] + "]]\"";
                        }
                    }
                    else   // 是否必填验证
                    {
                        if (isRequired)
                        {
                            validateCode = " class=\"validate[required]\"";
                        }
                        else
                        {
                            validateCode = " class=\"validate[optional]\"";
                        }
                    }
                    break;
                case "8":   // 数字
                    string numberCount;

                    numberCount = dr["NumberCount"].ToString(); //小数位数

                    if (numberCount == "0")  //只输入整数
                    {
                        if (isRequired)
                        {
                            validateCode = " class=\"validate[required],custom[onlyNumber],numberMinMax[" + dr["MinValue"].ToString() + "," + dr["MaxValue"].ToString() + "]\"";
                        }
                        else
                        {
                            validateCode = " class=\"validate[optional],custom[onlyNumber],numberMinMax[" + dr["MinValue"].ToString() + "," + dr["MaxValue"].ToString() + "]\"";
                        }
                    }
                    else
                    {
                        if (isRequired)
                        {
                            validateCode = " class=\"validate[required],numberMinMax[" + dr["MinValue"].ToString() + "," + dr["MaxValue"].ToString() + "],regex[^\\d+(\\.\\d{1," + numberCount + "})?$,小数位数为 1 至 " + numberCount + " 之间]\"";
                        }
                        else
                        {
                            validateCode = " class=\"validate[optional],numberMinMax[" + dr["MinValue"].ToString() + "," + dr["MaxValue"].ToString() + "],regex[^\\d+(\\.\\d{1," + numberCount + "})?$,小数位数为 1 至 " + numberCount + " 之间]\"";
                        }
                    }
                    break;
                case "9":   // 货币
                    if (isRequired)
                    {
                        validateCode = " class=\"validate[required],numberMinMax[" + dr["MinValue"].ToString() + "," + dr["MaxValue"].ToString() + "],regex[^\\d+(\\.\\d{1,10})?$,小数位数为 1 至 10 之间]";
                    }
                    else
                    {
                        validateCode = " class=\"validate[optional],numberMinMax[" + dr["MinValue"].ToString() + "," + dr["MaxValue"].ToString() + "],regex[^\\d+(\\.\\d{1,10})?$,小数位数为 1 至 10 之间]";
                    }
                    break;
                default:
                    validateCode = "";
                    break;
            }

            return validateCode;
        }
        #endregion
    }
}
