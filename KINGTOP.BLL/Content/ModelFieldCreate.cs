using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Common;
using System.Reflection;
using System.Resources;

namespace KingTop.BLL.Content
{
    public class ModelFieldCreate
    {
        /// <summary>
        /// 模板缓存表
        /// </summary>
        public DataTable DtModelFiled { get; set; }

        /// <summary>
        /// 当前数据行
        /// </summary>
        public DataRow DrModelTableRow { get; set; }

        public string strModelTableName { get; set; }

        /// <summary>
        /// 页面生成函数
        /// </summary>
        /// <param name="strModelId"></param>
        /// <returns></returns>
        public bool CreateModelFiledEidt(string strModelId)
        {
            StringBuilder sbStr = new StringBuilder(400);
            FileReadOrWrite file = new FileReadOrWrite();
            sbStr.Append(file.FileRead("ControlManageEditTemplate.aspx"));
            sbStr.Replace("{GetControlHtml}", GetControlHtml(strModelId));
           return file.FileWrite(sbStr.ToString(), strModelTableName.Replace("K_U_", "").Replace("K_F_","") + "EDIT.aspx");
        }

        public string GetControlHtml(string strModelId)
        {           
            // StrModelId = "5230a9b-2d6f-49";
            ModelField bllModelField = new ModelField();
            ModelManage bllManage = new ModelManage();
            DataTable dtModelField = bllModelField.GetList("ALLModelId", Utils.getTwoParams(strModelId,"1"));
            DataTable dtModelManage = bllManage.GetList("ONE", Utils.getOneParams(strModelId));
            if (dtModelField.Rows.Count > 0 && dtModelManage.Rows.Count > 0)
            {               
                DtModelFiled = dtModelField;
                strModelTableName = dtModelManage.Rows[0]["tablename"].ToString();              
                dtModelField.Dispose();
                dtModelManage.Dispose();
                return Create(strModelId);
            }
            else
            {
                dtModelField.Dispose();
                dtModelManage.Dispose();
                return "";
            }

        }

        private string br = "\n\r"; //换行符;
        /// <summary>
        /// 开始创建页面
        /// </summary>
        public string Create(string strModelId)
        {
            if (DtModelFiled == null)  //如果当前表没有值,则直接退出
            {                     
                return "";              
            }       
            StringBuilder sb = new StringBuilder(400);

            //声明
            sb.Append("<asp:HiddenField ID='hidModelId' runat='server' Value='" + strModelId + "' />"+br);
            sb.Append("<%KingTop.BLL.Content.ModelField  bllModel = new KingTop.BLL.Content.ModelField();" + br);
            sb.Append("DataSet ds = null;" + br);
            sb.Append("int i = 0;" + br);
            sb.Append("int iCount = 0;" + br);
            sb.Append("string strChecked =\"\"; %>" + br);
            sb.Append("<table>"+ br);


            foreach (DataRow drModelFiled in DtModelFiled.Rows)
            {
                if (drModelFiled["name"].ToString().ToLower() != "id") //如果当前的是id 则界面不显示
                {
                    if (drModelFiled["SystemFirerdHtml"].ToString().IndexOf("{}") != -1)  //如果是多字段系统字字段,则只显示第一个字段的htmlcode 其它的则退出
                    {
                        break;
                    }
                    sb.Append("<tr class=\"btd2\"><td width=\"4%\" align=\"right\">");
                    sb.Append(drModelFiled["FieldAlias"].ToString() + "</td><td width=\"13%\">");
                    if (Utils.ParseBool(drModelFiled["IsSystemFiierd"].ToString())) //如果是系统字段,则暂时不处理
                    {
                        sb.Append(GetControlsSystemHtml(drModelFiled));
                    }
                    else
                    {
                        sb.Append(GetControlsHtml(drModelFiled,true));
                    }
                    sb.Append("</td></tr>" + br);
                }
            }
         //   sb.Append("<tr class=\"btd2\"><td  width=\"4%\" align=\"right\"></td><td width=\"13%\"></td></tr>");       
            sb.Append("</table>"+br);
            sb.Append("<% if(ds!=null)"+br+"{ds.Dispose();} %>");         
            return sb.ToString();
        }

        /// <summary>
        /// 生成表单字段
        /// </summary>
        /// <param name="drModelField"></param>
        /// <returns></returns>
        public string Create(DataRow drModelField)
        {
            if (Utils.ParseBool(drModelField["IsSystemFiierd"].ToString())) //如果是系统字段,则暂时不处理
            {
                return GetControlsSystemHtml(drModelField);
            }
            else
            {
                return GetControlsHtml(drModelField, true);
            }
        }

        public string GetControlsSystemHtml(DataRow drModelField) //获取系统字段html控件
        {
            StringBuilder sb = new StringBuilder(250);
            string strValue = "";
            strValue = "<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString():\"" + drModelField["DefaultValue"].ToString() + "\"%>";
            sb.Append(Utils.HtmlDecode(drModelField["SystemFirerdHtml"].ToString().Replace("{"+drModelField["name"]+"}",strValue)));
            return sb.ToString();
        }

        public string GetControlsHtml(DataRow drModelField,bool isAddMessage)  //获取控件html
        {
            StringBuilder sb = new StringBuilder(250);
            string strValue = "";
            strValue = "<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString():\"" + drModelField["DefaultValue"].ToString() + "\"%>";
  
            if (drModelField["Controls"].ToString() != "") //如果系统预定义控件,则暂时不处理
            {
                return "";
            }
            else
            {
                string  strBasicField = drModelField["BasicField"].ToString();
                switch (strBasicField)
                {
                    case "1":  //如果选择的是单行文本
                        sb.Append("<input type='text' id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"].ToString() + "\" width=" + drModelField["TextBoxWidth"] + " maxlength=" + drModelField["TextBoxMaxLength"] + " value='" + strValue + "'");
                        sb = IsRequiredHtml(drModelField["IsRequired"].ToString(), sb);
                        //if (Utils.ParseBool(drModelField["IsRequired"].ToString()))  //如果当前是必填
                        //{
                        //    sb.Append(" class='validate[required");
                        //}
                        //else
                        //{
                        //    sb.Append(" class='validate[optional");
                        //}
                        if (drModelField["ValidationType"].ToString() != "-1")  //如果当前验证不为无
                        {
                            if (drModelField["ValidationType"].ToString() != "0") //如果当前不是自定义验证
                            {
                                sb.Append(",regex[" + drModelField["TextBoxValidation"] + "," + drModelField["ValidationMessage"] + "]]'");
                            }
                            else //自定义暂时不处理
                            {
                                sb.Append("]'");
                            }
                        }
                        else
                        {
                            sb.Append("]'");
                        }
                        sb.Append(" />");
                        break;
                    case "2": //  多行文本（不支持HTML）
                        sb.Append("<textarea id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"].ToString() + "\" cols=\"" + drModelField["TextBoxWidth"] + "\" rows=\"" + drModelField["TextBoxHieght"] + "\"");
                        sb = IsRequiredHtml(drModelField["IsRequired"].ToString(), sb);
                        sb.Append("]'" + " />" + strValue + "</textarea>");
                        break;
                    case "3": //多行文本（支持HTML）
                        string strEditorType = drModelField["EditorType"].ToString();//编辑器类型
                        sb.Append("<input type='hidden' name=\"" + drModelField["name"].ToString() + "\" id=\"" + drModelField["name"].ToString() + "\"  value='" + strValue + "' />");
                        sb.Append("<input type='hidden' name=\"hid" + drModelField["name"].ToString() + "\" id=\"hid" + drModelField["name"].ToString() + "\" value='" + strEditorType + "' />");
                        switch (strEditorType)
                        {
                            case "1": //fck
                                sb.Append("<FCKeditorV2:FCKeditor id=\"" + drModelField["name"].ToString() + "\" runat=\"server\" Width='" + drModelField["TextBoxWidth"] + "' Height='" + drModelField["TextBoxHieght"] + "'");
                                //  sb = IsRequiredHtml(drModelField["IsRequired"].ToString(), sb);
                                //   sb.Append("]'" + " >" + strValue + "</FCKeditorV2:FCKeditor>");
                                sb.Append(" ></FCKeditorV2:FCKeditor>");
                                break;
                        }
                        break;                       
                    case "4":  //单选
                        if (drModelField["DropDownDataType"].ToString() == "1") //手动输入数据
                        {
                            sb = GetRadioInput(drModelField, sb);
                        }
                        else  //从数据库绑定
                        {
                            sb = GetRadioDataBase(drModelField, sb);
                        }
                        break;
                    case "5": //多选 
                        if (drModelField["DropDownDataType"].ToString() == "1") //手动输入数据
                        {
                            sb = GetCheckInput(drModelField, sb);
                        }
                        else  //从数据库绑定
                        {
                            sb = GetCheckDataBase(drModelField, sb);
                        }
                        break;
                    case "6": //下拉
                        if (drModelField["DropDownDataType"].ToString() == "1") //手动输入数据
                        {
                            sb = GetDropDownInput(drModelField, sb);
                        }
                        else  //从数据库绑定
                        {
                            sb = GetDropDownDataBase(drModelField, sb);
                        }
                        break;
                    case "7": //列表(可以多选)
                        if (drModelField["DropDownDataType"].ToString() == "1") //手动输入数据
                        {
                            sb = GetDropDownListInput(drModelField, sb);
                        }
                        else  //从数据库绑定
                        {
                            sb = GetDropDownDataBaseList(drModelField, sb);
                        }
                        break;
                    case "8": //数字
                    case "9"://货币
                        sb.Append("<input type='text'  id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"].ToString() + "\"");
                        sb = IsRequiredHtml(drModelField["IsRequired"].ToString(), sb);
                        if (strBasicField == "8")
                        {
                            string strNumberCount = drModelField["NumberCount"].ToString(); //小数位数
                            if (strNumberCount == "0")  //只输入整数
                            {
                                sb.Append(",custom[onlyNumber],numberMinMax[" + drModelField["MinValue"].ToString() + "," + drModelField["MaxValue"].ToString() + "]");
                            }
                            else
                            {
                                sb.Append(",numberMinMax[" + drModelField["MinValue"].ToString() + "," + drModelField["MaxValue"].ToString() + "],regex[^\\d+(\\.\\d{1," + strNumberCount + "})?$,小数位数为 1 至 " + strNumberCount + " 之间]");
                            }
                        }
                        else
                        {
                            sb.Append(",numberMinMax[" + drModelField["MinValue"].ToString() + "," + drModelField["MaxValue"].ToString() + "],regex[^\\d+(\\.\\d{1,10})?$,小数位数为 1 至 10 之间]");
                        }
                        sb.Append("]' value='" + strValue + "' />");
                        break;
                    case "10": //时间
                        sb.Append("<input type='hidden'  name=\"hid" + drModelField["name"].ToString() + "\" value = \"" + drModelField["DateFormatOption"].ToString() + "\" />");
                        strValue = drModelField["DateDefaultOption"].ToString();
                        string strDefaultTime = "";
                        string strDateFormat = drModelField["DateFormatOption"].ToString();
                        sb.Append("<input type='text'  id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"].ToString() + "\"");
                        sb = IsRequiredHtml(drModelField["IsRequired"].ToString(), sb);
                        if (strValue == "2")  //当前日期
                        {
                            switch (strDateFormat)
                            {
                                case "1":
                                    strDefaultTime = "KingTop.Common.Utils.GetDate()";
                                    sb.Append(",regex[^[1-2][0-9]{3}-[0-1][0-9]-[0-3][0-9]$,日期格式输入错误 请参照:2010-03-22]");
                                    break;
                                case "2":
                                    strDefaultTime = "KingTop.Common.Utils.GetTime()";
                                    sb.Append(",regex[^[0-5][0-9]:[0-5][0-9]:[0-5][0-9]$,日期格式输入错误 请参照:14:00:00]");
                                    break;
                                case "3":
                                    strDefaultTime = "KingTop.Common.Utils.GetDateTime()";
                                    sb.Append(",regex[^[1-2][0-9]{3}-[0-1][0-9]-[0-3][0-9]\\s[0-5][0-9]:[0-5][0-9]:[0-5][0-9]$,日期格式输入错误 请参照:2010-03-22 14:00:00]");
                                    break;
                            }
                        }
                        else //指定日期
                        {
                            switch (strDateFormat)
                            {
                                case "1":
                                    strDefaultTime = Utils.GetStandardDateTime(drModelField["defaultValue"].ToString(), "yyyy-MM-dd");
                                    sb.Append(",regex[^[1-2][0-9]{3}-[0-1][0-9]-[0-3][0-9]$,日期格式输入错误 请参照:2010-03-22]");
                                    break;
                                case "2":
                                    strDefaultTime = Utils.GetStandardDateTime(drModelField["defaultValue"].ToString(), "hh:mm:ss");
                                    sb.Append(",regex[^[0-5][0-9]:[0-5][0-9]:[0-5][0-9]$,日期格式输入错误 请参照:14:00:00]");
                                    break;
                                case "3":
                                    strDefaultTime = Utils.GetStandardDateTime(drModelField["defaultValue"].ToString(), "yyyy-MM-dd hh:mm:ss");
                                    sb.Append(",regex[^[1-2][0-9]{3}-[0-1][0-9]-[0-3][0-9]\\s[0-5][0-9]:[0-5][0-9]:[0-5][0-9]$,日期格式输入错误 请参照:2010-03-22 14:00:00]");
                                    break;
                            }
                        }
                        sb.Append("]'  value = \"<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString():\"" + strDefaultTime + "\"%>\" />");
                        break;
                    case "11": //图片
                    case "12": //文件
                        bool isUpload = Utils.ParseBool(drModelField["IsUpload"].ToString()); //是否启用上传     
                        sb.Append("<input type='text'  id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"].ToString() + "\" width='" + drModelField["TextBoxWidth"].ToString() + "'");
                        sb = IsRequiredHtml(drModelField["IsRequired"].ToString(), sb);
                        if( drModelField["DefaultValue"].ToString()=="")
                        {
                            sb.Append("]' value='<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString():\" \"%>' />");
                        }
                        else
                        {
                              sb.Append("]' value='" + strValue + "' />");
                        }
                        if (isUpload)
                        {
                            string strWhere =null;
                            if (strBasicField == "11") //图片
                            {
                                strWhere="InputFile('theForm','" + drModelField["name"] + "','image',1,'" + drModelField["ImageType"]+"'," + drModelField["MaxSize"];
                            }
                            else
                            {
                                strWhere = "InputFile('theForm','" + drModelField["name"] + "','file',1,'" + drModelField["ImageType"]+"'," + drModelField["MaxSize"] ; 
                            }
                            if (Utils.ParseBool(drModelField["IsSaveFileSize"].ToString()))  //是否保存文件大小
                            {
                                sb.Append("<input type='hidden' value='<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["SaveFileName"] + "\"].ToString():\"0\"%>'  name=\"hid" + drModelField["SaveFileName"].ToString() + "\" />");
                                strWhere += ",'hid" + drModelField["SaveFileName"] + "')";
                            }
                            else
                            {
                                strWhere += ",'')";
                            }                          
                            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' onclick=\""+strWhere+"\" value = '上传' />");
                        }
                        break;
                }
            }
            if (isAddMessage)  //是否要添加提示
            {
                sb.Append("&nbsp;&nbsp;" + drModelField["message"].ToString()); //提示文本
            }
            return sb.ToString();
        }

     

        /// <summary>
        /// 是否必填
        /// </summary>
        /// <param name="IsRequired"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public  StringBuilder IsRequiredHtml(string IsRequired,StringBuilder sb)
        {
            if (Utils.ParseBool(IsRequired))
            {
                sb.Append(" class='validate[required");
            }
            else
            {
                sb.Append(" class='validate[optional");
            }
            return sb;
        }

        /// <summary>
        /// 下拉列表(多选)[手动输入数据]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetDropDownListInput(DataRow drModelField, StringBuilder sb)
        {
            string[] strs = Utils.strSplit(drModelField["OptionsValue"].ToString(), ",");
            string[] strsValue = null;
            sb.Append("<select  multiple=\"multiple\"");
            if (Utils.ParseBool(drModelField["IsRequired"].ToString()))
            {
                sb.Append(" class='validate[required]' ");
            }
            sb.Append(" id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"] + "\">");
            foreach (string str in strs)
            {
                strsValue = Utils.strSplit(str, "|"); //再次分割,得到详细数据        
                sb.Append("<option value=\"" + strsValue[1] + "\" "+
                 "<%=DrModelTableRow!=null?KingTop.Common.Utils.InArray(\"" + strsValue[1] + "\",DrModelTableRow[\"" + drModelField["name"] + "\"].ToString())?\"selected\":\"\":\"\"%>"
                   +" />" + strsValue[0] + "</option>");
            }
            sb.Append("</select>");
            return sb;
        }

        /// <summary>
        /// 下拉列表(多选)[从数据库中读取]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetDropDownDataBaseList(DataRow drModelField, StringBuilder sb)
        {
            sb.Append("<% ds = bllModel.GetTable(\"" + drModelField["DropDownTable"] + "\",\"" + drModelField["DropDownTextColumn"] + "\",\"" + drModelField["DropDownValueColumn"] + "\",\"" + drModelField["DropDownSqlWhere"] + "\");" + br);
            sb.Append(" if (ds != null && ds.Tables.Count > 0){%>" + br);
            sb.Append("<select ");
            if (Utils.ParseBool(drModelField["IsRequired"].ToString()))
            {
                sb.Append(" class='validate[required]' ");
            }
            sb.Append("multiple=\"multiple\" id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"] + "\">");
            sb.Append("<%foreach (DataRow dr in ds.Tables[0].Rows){%>" + br);
            sb.Append("<option value=\"<%=dr[\"" + drModelField["DropDownValueColumn"] + "\"] %>\" <%=DrModelTableRow!=null?KingTop.Common.Utils.InArray(dr[\"" + drModelField["DropDownValueColumn"] + "\"].ToString(),DrModelTableRow[\"" + drModelField["name"] + "\"].ToString())?\"selected\":\"\":\"\"%> /><%=dr[\"" + drModelField["DropDownTextColumn"] + "\"]%></option>");
            sb.Append("<%}}%>" + br);
            sb.Append("</select>");
            return sb;
        }

        /// <summary>
        /// 下拉列表[手动输入数据]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetDropDownInput(DataRow drModelField, StringBuilder sb)
        {
            string[] strs = Utils.strSplit(drModelField["OptionsValue"].ToString(), ",");
            string[] strsValue = null;            
            sb.Append("<select  id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"] + "\">");
            foreach (string str in strs)
            {
                strsValue = Utils.strSplit(str, "|"); //再次分割,得到详细数据        
                sb.Append("<option value=\"" + strsValue[1] + "\"  <%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString()==\"" + strsValue[1] + "\"?\"selected\":\"\":\"\"%> />" + strsValue[0] +"</option>");              
            }
            sb.Append("</select>");
            return sb;
        }

        /// <summary>
        /// 下拉列表[从数据库中读取]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetDropDownDataBase(DataRow drModelField, StringBuilder sb)
        {
            sb.Append("<% ds = bllModel.GetTable(\"" + drModelField["DropDownTable"] + "\",\"" + drModelField["DropDownTextColumn"] + "\",\"" + drModelField["DropDownValueColumn"] + "\",\"" + drModelField["DropDownSqlWhere"] + "\");" + br);
            sb.Append(" if (ds != null && ds.Tables.Count > 0){%>" + br);
            sb.Append("<select  id=\"" + drModelField["name"].ToString() + "\" name=\"" + drModelField["name"] + "\">");
            sb.Append("<%foreach (DataRow dr in ds.Tables[0].Rows){%>" + br);
            sb.Append("<option value=\"<%=dr[\"" + drModelField["DropDownValueColumn"] + "\"] %>\" <%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString()==dr[\"" + drModelField["DropDownValueColumn"] + "\"].ToString()?\"selected\":\"\":\"\"%> /><%=dr[\"" + drModelField["DropDownTextColumn"] + "\"]%></option>");
            sb.Append("<%}}%>" + br);
            return sb;
        }


        
        /// <summary>
        /// 多选[手动输入数据]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetCheckInput(DataRow drModelField, StringBuilder sb)
        {
            string[] strs = Utils.strSplit(drModelField["OptionsValue"].ToString(), ",");
            string[] strsValue = null;
            int i = 0;
            int iCount = Utils.ParseInt(drModelField["OptionCount"], 5); //每页显示项数
            string strChecked = ""; //当前是否选中
            foreach (string str in strs)
            {
                strsValue = Utils.strSplit(str, "|"); //再次分割,得到详细数据         
                if (i == 0)
                {
                    strChecked = "checked";
                }
                sb.Append("<input");
                if (Utils.ParseBool(drModelField["IsRequired"].ToString()))
                {
                    sb.Append(" class='validate[minCheckbox[1]]' ");
                }
                sb.Append(" type=\"checkbox\" name=\"" + drModelField["name"] + "\" id=\"" + drModelField["name"] + "\" value=\"" + strsValue[1] + "\" " +
               "<%=DrModelTableRow!=null?KingTop.Common.Utils.InArray(\"" + strsValue[1] + "\",DrModelTableRow[\"" + drModelField["name"] + "\"].ToString())?\"checked\":\"\":\"" + strChecked + "\"%> />"
               + strsValue[0] + " &nbsp;");
                i += 1;
                if (iCount != 0 && (i % iCount == 0 && i != strs.Length))
                {
                    sb.Append("<br />");
                }
            }
            return sb;
        }

         /// <summary>
        /// 多选[从数据库中读取]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetCheckDataBase(DataRow drModelField, StringBuilder sb)
        {
            sb.Append("<% ds = bllModel.GetTable(\"" + drModelField["DropDownTable"] + "\",\"" + drModelField["DropDownTextColumn"] + "\",\"" + drModelField["DropDownValueColumn"] + "\",\"" + drModelField["DropDownSqlWhere"] + "\");" + br);
            sb.Append(" if (ds != null && ds.Tables.Count > 0){" + br);
            sb.Append(" i = 0;" + br);
            sb.Append(" iCount =KingTop.Common.Utils.ParseInt(" + drModelField["OptionCount"].ToString() + ", 5); //每页显示项数" + br);
            sb.Append(" strChecked =\"\";" + br);
            sb.Append("foreach (DataRow dr in ds.Tables[0].Rows){" + br);
            sb.Append("if (i == 0){strChecked = \"checked\"; }%>" + br);
            sb.Append("<input ");
            if(Utils.ParseBool(drModelField["IsRequired"].ToString()))
            {
                sb.Append(" class='validate[minCheckbox[1]]' ");
            }
            sb.Append(" type=\"checkbox\" name=\"" + drModelField["name"] + "\" id=\"" + drModelField["name"] + "\" value=\"<%=dr[\"" + drModelField["DropDownValueColumn"] + "\"] %>\" " +
              "<%=DrModelTableRow!=null?KingTop.Common.Utils.InArray(dr[\"" + drModelField["DropDownValueColumn"] + "\"].ToString(),DrModelTableRow[\"" + drModelField["name"] + "\"].ToString())?\"checked\":\"\":\" + strChecked + \"%> />"
              + "<%=dr[\"" + drModelField["DropDownTextColumn"] + "\"]%>" + " &nbsp;" + br);
            sb.Append("<%i += 1;" + br);
            sb.Append("if (iCount != 0 && (i % iCount == 0 && i != ds.Tables[0].Rows.Count)){" + br);
            sb.Append("Response.Write(\"<br />\");}}}%>" + br);
            return sb;
        }

        /// <summary>
        /// 单选[手动输入数据]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetRadioInput(DataRow drModelField,StringBuilder sb)
        {
            string[] strs = Utils.strSplit(drModelField["OptionsValue"].ToString(), ",");
            string[] strsValue = null;
            int i=0;
            int iCount = Utils.ParseInt(drModelField["OptionCount"],5); //每页显示项数
            string strChecked  = ""; //当前是否选中
            foreach(string str in strs)
            {
                strsValue = Utils.strSplit(str,"|"); //再次分割,得到详细数据         
                if(i==0)
                {
                    strChecked ="checked";
                }
                getRadioNext(strChecked, drModelField, strsValue[1], strsValue[0], ref sb);
                i += 1;
                if (iCount != 0 && (i % iCount == 0 && i != strs.Length))
                {
                    sb.Append("<br />");
                }              
            }
            return sb;
        }

        /// <summary>
        /// 单选[从数据库中读取]
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public StringBuilder GetRadioDataBase(DataRow drModelField, StringBuilder sb)
        {
            sb.Append("<% ds = bllModel.GetTable(\"" + drModelField["DropDownTable"] + "\",\"" + drModelField["DropDownTextColumn"] + "\",\"" + drModelField["DropDownValueColumn"] + "\",\"" + drModelField["DropDownSqlWhere"] + "\");" + br);
            sb.Append(" if (ds != null && ds.Tables.Count > 0){"+br);
            sb.Append("i = 0;"+br);
            sb.Append("iCount =KingTop.Common.Utils.ParseInt("+drModelField["OptionCount"].ToString()+", 5); //每页显示项数"+br);
            sb.Append("strChecked =\"\";" + br); 
            sb.Append("foreach (DataRow dr in ds.Tables[0].Rows){"+br);
            sb.Append("if (i == 0){strChecked = \"checked\"; }%>" + br);
             sb.Append("<input type=\"radio\" name=\"" + drModelField["name"] + "\" value=\"<%=dr[\""+drModelField["DropDownValueColumn"]+"\"] %>\" " +
               "<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString()==dr[\""+drModelField["DropDownValueColumn"]+"\"].ToString()?\"checked\":\"\":\" + strChecked + \"%> />"
               + "<%=dr[\""+drModelField["DropDownTextColumn"]+"\"]%>" + " &nbsp;"+br);
             sb.Append("<%i += 1;" + br);
            sb.Append("if (iCount != 0 && (i % iCount == 0 && i != ds.Tables[0].Rows.Count)){" + br);
            sb.Append("Response.Write(\"<br />\");}}}%>"+br);
            return sb;
        }
       

        /// <summary>
        /// 单选操作下一步
        /// </summary>
        /// <param name="strChecked"></param>
        /// <param name="drModelField"></param>
        /// <param name="strValue"></param>
        /// <param name="strText"></param>
        public void getRadioNext(string strChecked, DataRow drModelField, string strValue, string strText,ref StringBuilder sb)
        {
            sb.Append("<input type=\"radio\" name=\"" + drModelField["name"] + "\" value=\"" + strValue + "\" " +
               "<%=DrModelTableRow!=null?DrModelTableRow[\"" + drModelField["name"] + "\"].ToString()==\"" + strValue + "\"?\"checked\":\"\":\"" + strChecked + "\"%> />"
               + strText + " &nbsp;");
        }
    }
}
