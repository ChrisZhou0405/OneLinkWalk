#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Web.Hosting;

#endregion

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:     袁纯林 吴岸标 周武 严辉
    创建时间： 2010年3月10日

    功能描述： 通用方法集

 
// 更新日期        更新人      更新原因/内容
// 2010-3-11       周武        加入cookie操作
// 2010-5-12       严辉        替换 HTML 标签
// 2010-5-25       朱存群      加入前台服务端验证、字符过滤和MD5加密
// 2010-5-27       何建龙      加入读文件夹/文件列表的读取、删除方法

--===============================================================*/
#endregion


namespace KingTop.Common
{
    public static partial class Utils
    {
        #region 判断审核状态
        public static string GetFlowState(string para)
        {
            string result = string.Empty;
            if (para == "99")
            {
                result = "审核通过";
            }
            else if (para == "3")
            {
                result = "审核不通过";
            }
            else if (para == "0")
            {
                result = "待审核";
            }
            return result;
        }
        #endregion
        
        #region 判断DataSet是否有记录
        public static bool CheckDataSet(DataSet ds)
        {
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断datatable是否有记录
        /// <summary>
        /// 判断datatable是否有记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool CheckDataTable(DataTable dt)
        {
            if (dt == null)
            {
                return false;
            }
            else
            {

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region 获得当前页面客户端的IP
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
                return "127.0.0.1";

            return result;
        }
        #endregion

        #region 判断是否为ip
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region 将object类型值转换成long值，失败返回预设的缺省值
        /// <summary>
        /// 将object类型值转换成long值，失败返回预设的缺省值
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ParseLong(object originalValue, long defaultValue)
        {
            long targetValue;

            try
            {
                targetValue = long.Parse(originalValue.ToString().Trim());
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }
        #endregion

        #region 将object类型值转换成int值，失败返回预设的缺省值
        /// <summary>
        /// 将object类型值转换成long值，失败返回预设的缺省值
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ParseInt(object originalValue, int defaultValue)
        {
            int targetValue;

            try
            {
                targetValue = int.Parse(originalValue.ToString().Trim());
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }
        #endregion

        #region 将object类型值转换成float值，失败返回预设的缺省值
        /// <summary>
        /// 将object类型值转换成long值，失败返回预设的缺省值
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ParseFloat(object originalValue, float defaultValue)
        {
            float targetValue;

            try
            {
                targetValue = float.Parse(originalValue.ToString().Trim());
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }
        #endregion

        #region 将int转成bool 1转true 其它转false
        /// <summary>
        /// 将int转成bool 1转true 其它转false
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static bool ParseBool(int intValue)
        {
            bool targetValue;
            if (intValue == 1)
            {
                targetValue = true;
            }
            else
            {
                targetValue = false;
            }
            return targetValue;
        }

        /// <summary>
        /// 将int转成bool 1转true 或者"True"转True 其它转false
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns></returns>
        public static bool ParseBool(string intValue)
        {
            bool targetValue;
            if (intValue == "1" || intValue.ToLower () == "true")
            {
                targetValue = true;
            }
            else
            {
                targetValue = false;
            }
            return targetValue;
        }

        /// <summary>
        /// 将object转成bool 1转true 其它转false
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static bool ParseBool(object objValue)
        {
            bool targetValue;

            if (objValue.ToString() == "1" || objValue.ToString().ToLower() == "true")
            {
                targetValue = true;
            }
            else
            {
                targetValue = false;
            }

            return targetValue;
        }

        public static string BoolToIntString(bool targetValue)
        {
            return targetValue ? "1" : "0";
        }

        /// <summary>
        /// 将Bool转成int true转1 其它转0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int ParseBoolToInt(object objValue)
        {
            return ParseBoolToInt(objValue.ToString());
        }

        /// <summary>
        /// 将Bool转成int true转1 其它转0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int ParseBoolToInt(string objValue)
        {
            int targetValue;

            if (objValue == "True")
            {
                targetValue = 1;
            }
            else
            {
                targetValue = 0;
            }

            return targetValue;
        }



        #endregion

        #region  bool值转int  true 为1  false为0
        /// <summary>
        /// bool值转int  true 为1  false为0
        /// </summary>
        /// <param name="strValue">bool值</param>
        /// <returns>转变后的值</returns>
        public static int BoolToInt(string strValue)
        {
            return strValue == "True" ? 1 : 0;
        }

        /// <summary>
        /// bool值转int  true 为1  false为0
        /// </summary>
        /// <param name="strValue">bool值</param>
        /// <returns>转变后的值</returns>
        public static int BoolToInt(bool strValue)
        {
            return strValue ? 1 : 0;
        }
        #endregion

        #region 类型转换ToDecimal ToDouble StrToByte StrToInt64 StrToDate
        public static DateTime? StrToDate(string s, string format)
        {
            try {
                return DateTime.ParseExact(s, format, null); }
            catch { return null; }
        }
        public static DateTime? StrToDate(string s)
        {
            try { return DateTime.Parse(s); }
            catch { return null; }
        }
        public static Decimal ToDecimal(object o, Decimal defVal)
        {
            if (null == o) return defVal;
            return ToDecimal(o.ToString(), defVal);
        }
        /// <summary>
        /// SQL中的money字段用此类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(string s, Decimal defVal)
        {
            try
            {
                return Convert.ToDecimal(s);
            }
            catch { return defVal; }
        }
        public static Double ToDouble(object s, Double defVal)
        {
            if (null == s || string.IsNullOrEmpty(s.ToString())) return defVal;
            try
            {
                return Convert.ToDouble(s.ToString());
            }
            catch { return defVal; }
        }

        public static Byte StrToByte(string strValue, Byte defValue)
        {
            try
            {
                return Convert.ToByte(strValue);
            }
            catch { return defValue; }
        }
        public static Byte StrToByte(object objValue, Byte defValue)
        {
            if (null == objValue) return defValue;
            return StrToByte(objValue.ToString(), defValue);
        }

        public static long StrToInt64(object o, long defaultValue)
        {
            if (null == o) return defaultValue;
            return StrToInt64(o.ToString(), defaultValue);
        }
        public static long StrToInt64(string s, long defaultValue)
        {
            try { return Int64.Parse(s); }
            catch { return defaultValue; }
        }
        #endregion 

        #region xml操作类
        /// <summary>
        /// 读取xml 转DataSet
        /// </summary>
        /// <param name="strPath">xml路径</param>
        /// <returns></returns>
        public static DataSet GetXmlDataSet(string strPath)
        {
            DataSet dsXml = new DataSet();
            dsXml.ReadXml(GetPath(strPath));
            return dsXml;
        }

        /// <summary>
        /// 查询xml文件属性单个节点

        /// </summary>
        /// <param name="strPath">xml路径</param>
        /// <param name="strXPath">XPath字符串</param>
        /// <returns></returns>
        public static XmlNode XmlSelectSingleNode(string strPath, string strXPath)
        {
            XmlNode xmlnode = null;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(GetPath(strPath));
            xmlnode = xmldoc.SelectSingleNode(strXPath);
            return xmlnode;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        public static string XmlRead(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void XmlInsert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool XmlUpdate(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;

                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool XmlCDATAUpdate(string path, string node, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                xn.InnerText = "";
                xn.AppendChild(doc.CreateCDataSection(value));
                doc.Save(path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void XmlDelete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// 返回实际路径
        /// </summary>
        /// <param name="strPath">虚拟路径</param>
        /// <returns></returns>
        public static string GetPath(string strPath)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }
        #endregion

        #region 添加参数
        /// <summary>
        /// 添加单个字符型参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static KingTop.Model.SelectParams getOneParams(string strValue)
        {
            KingTop.Model.SelectParams param = new KingTop.Model.SelectParams();
            param.S1 = strValue;
            return param;
        }

        /// <summary>
        /// 添加单个数字型参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static KingTop.Model.SelectParams getOneNumParams(int num)
        {
            KingTop.Model.SelectParams param = new KingTop.Model.SelectParams();
            param.I1 = num;
            return param;
        }

        /// <summary>
        /// 添加两个参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static KingTop.Model.SelectParams getTwoParams(string strOneValue, string strTwoValue)
        {
            KingTop.Model.SelectParams param = new KingTop.Model.SelectParams();
            param.S1 = strOneValue;
            param.S2 = strTwoValue;
            return param;
        }
        #endregion

        #region 弹出对话框
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strMessage">要执行的javascript代码</param>
        /// <param name="alertTitle">弹出框标题</param>
        public static void AlertMessage(System.Web.UI.Page typeClass, string strMessage, string alertTitle)
        {
            RunJavaScript(typeClass, "alert({msg:'" + strMessage + "',title:'" + alertTitle + "'})");
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strMessage">要执行的javascript代码</param>
        public static void AlertMessage(System.Web.UI.Page typeClass, string strMessage)
        {
            RunJavaScript(typeClass, "alert({msg:'" + strMessage + "',title:'提示消息'})");
        }

        /// <summary>
        /// 提示框内容处理，将'替换成\',\r\n替换成<br>
        /// </summary>
        /// <param name="strMessage">提示框内容</param>
        public static string AlertMessage(string strMessage)
        {
            if (string.IsNullOrEmpty(strMessage))
                return "";

            return strMessage.Replace("\r\n", "<br>").Replace("'", "\\'");
        }

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strMessage">要执行的javascript代码</param>
        public static void AlertJavaScript(System.Web.UI.Page typeClass, string strMessage)
        {
            strMessage = strMessage.Replace("'", "\'");
            RunJavaScript(typeClass, "alert('" + strMessage + "');");
        }

        /// <summary>
        /// 执行客户端脚本
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要执行的javascript代码</param>
        public static void RunJavaScript(System.Web.UI.Page typeClass, string strMessage)
        {
            //typeClass.RegisterStartupScript("", "<script>$(function(){" + strMessage + "});</script>");
            string msg = "<script>";
            if (strMessage.IndexOf("nmsgtitle") != -1 && strMessage.IndexOf("alert")==-1)
            {
                msg += " var nmsgtitle='';var nmsgcontent='';";
            }
            msg+="$(function(){" + strMessage + "});";
            if (strMessage.IndexOf("nmsgtitle") != -1 && strMessage.IndexOf("alert") == -1)
            {
                msg += "$(function(){alert({msg:nmsgcontent,title:nmsgtitle})});";
            }
            msg += "</script>";

            typeClass.RegisterStartupScript("", msg);
        }
        #endregion

        #region cookie操作
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Path = "/";
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">key</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();

            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">key</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpContext.Current.Request.Cookies[strName][key].ToString();

            return "";
        }
        #endregion

        #region 获取资源文件内容
        /// <summary>
        /// 获取资源文件内容
        /// </summary>
        /// <param name="strFileName">资源文件名</param>
        /// <param name="strKey">要获取的字符名</param>
        /// <returns></returns>
        public static string GetResourcesValue(string strFileName, string strKey)
        {
            System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources." + strFileName, global::System.Reflection.Assembly.Load("App_GlobalResources"));
            return temp.GetString(strKey);
        }
        #endregion

        #region 获取参数
        /// <summary>
        /// 获取URL中参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReqUrlParameter(string key)
        {
            string urlParamValue = HttpContext.Current.Request.QueryString[key];

            if (urlParamValue == null)
            {
                urlParamValue = "";
            }

            return urlParamValue;
        }

        /// <summary>
        /// 获取from值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReqFromParameter(string key)
        {
            return ReqFromParameter(key, -1);
        }

        /// <summary>
        /// 获取指定name名的单个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public static string ReqFromParameter(string key, int intType)
        {
            string urlParamValue = null;
            if (intType == -1)
            {
                urlParamValue = HttpContext.Current.Request.Form[key];
            }
            else
            {
                urlParamValue = HttpContext.Current.Request.Form.GetValues(key)[intType];
            }

            if (urlParamValue == null)
            {
                urlParamValue = "";
            }

            return urlParamValue;
        }

        #endregion

        #region 验证输入类型
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        public static bool IsNumber(string inputData)
        {
            Regex RegNumber = new Regex("^[-]?[0-9]+$");
            try
            {
                Match m = RegNumber.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        public static bool IsHasCHZN(string inputData)
        {
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            try
            {
                Match m = RegCHZN.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是邮件地址
        /// </summary>
        public static bool IsEmail(string inputData)
        {
            Regex RegEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");        //email地址 
            try
            {
                Match m = RegEmail.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        public static bool IsDecimal(string inputData)
        {
            Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
            try
            {
                Match m = RegDecimal.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 是否是浮点数 可带正负号

        /// </summary>
        public static bool IsDecimalSign(string inputData)
        {
            Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
            try
            {
                Match m = RegDecimalSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///是否指定的合法字符（以字母开头，长度在6-18之间）只能输入由数字和 26 个英文字母组成的字符串："^[A-Za-z0-9]+$"
        /// </summary>
        public static bool IsLegitSign(string inputData)
        {
            Regex RegLegitSign = new Regex("^[a-zA-Z]w{5,17}$");
            try
            {
                Match m = RegLegitSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///是否只能输入由数字和 26 个英文字母组成的字符串： 
        /// </summary>
        public static bool IsM26MSign(string inputData)
        {
            Regex RegM26MSign = new Regex("^[A-Za-z0-9]+$");
            try
            {
                Match m = RegM26MSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是身份证
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsIDCard(string inputData)
        {
            Regex RegIDCardSign = new Regex("d{15}|d{}18$");
            try
            {
                Match m = RegIDCardSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是电话号码 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTelSign(string inputData)
        {
            Regex RegTelSign = new Regex("^((d{3,4})|(d{3,4}-)?d{7,8}$");
            try
            {
                Match m = RegTelSign.Match(inputData);
                return m.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string cutBadStr(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return "";

            //inputStr = inputStr.Replace(",", "");
            inputStr = inputStr.Replace("<", "");
            inputStr = inputStr.Replace(">", "");
            inputStr = inputStr.Replace("%", "");
            inputStr = inputStr.Replace("^", "");
            inputStr = inputStr.Replace("*", "");
            inputStr = inputStr.Replace("`", "");
            //inputStr = inputStr.Replace(" ", "");
            //inputStr = inputStr.Replace("~", "");
            inputStr = inputStr.Replace("'", "");
            return inputStr;
        }
        #endregion

        #region html/url 编码/解码

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        #endregion

        #region 字符串截取


        #region 截取允许的最大的字符串子串

        /// <summary>
        /// 说明：截取允许的最大的字符串子串      
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">保留的长度</param>
        /// <returns></returns>
        public static string MaxLengthSubString(string str, int len)
        {
            string temp = str;

            if (GetStringLength(str) <= len)
            {
                return str;
            }
            else
            {
                //先大胆截断一截

                if (str.Length > len)
                {
                    temp = str.Substring(0, len);
                }

                while (GetStringLength(temp) > len && temp.Length > 0)
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }

                return temp;
            }
        }
        #endregion

        #region 截取允许的最大的字符串子串

        /// <summary>
        /// 说明：截取允许的最大的字符串子串       
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">保留的长度</param>
        /// <param name="postStr">如果超过允许的长度，字符串添加的后缀</param>
        /// <returns></returns>
        public static string GetSubString(string str, int len, string postStr)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string subStr = MaxLengthSubString(str, len);
                int subStrLen = GetStringLength(subStr);
                int postStrLen = GetStringLength(postStr);
                if (subStr.Length == str.Length)
                {
                    return str;
                }
                else
                {
                    if (subStrLen > postStrLen)
                    {
                        return MaxLengthSubString(subStr, len - postStrLen) + postStr;
                    }
                    else
                    {
                        return subStr;
                    }
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 获取字符串长度

        /// <summary>
        /// 说明：获取字符串长度       
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return Regex.Replace(str, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region 去掉html内容中的html标签
        /// <summary>
        /// 去掉html内容中的html标签
        /// </summary>
        /// <param name="content">html内容</param>
        /// <returns>去掉标签的内容</returns>
        public static string DropHtmlTag(string content)
        {
            //去掉<tagname>和</tagname>
            // return DropIgnoreCase(content, "<[/]{0,1}" + tagName + "[^\\>]*\\>");
            return DropIgnoreCase(content, "<.+?>");

        }

        #region 删除字符串中指定的内容,不区分大小写
        /// <summary>
        /// 删除字符串中指定的内容,不区分大小写
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要删除的正则表达式模式</param>
        /// <returns>已删除指定内容的字符串</returns>
        public static string DropIgnoreCase(string src, string pattern)
        {
            return Replace(src, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
        }
        #endregion
        #endregion

        #region  正则替换字符串
        /// <summary>
        ///  正则替换字符串
        /// </summary>
        /// <param name="src">要修改的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">匹配模式</param>
        /// <returns>已修改的字符串</returns>
        public static string Replace(string src, string pattern, string replacement, RegexOptions options)
        {
            if (!string.IsNullOrEmpty(src))
            {
                Regex regex = new Regex(pattern, options | RegexOptions.Compiled);

                return regex.Replace(src, replacement);
            }
            return "";
        }
        #endregion

        #region 转换值，例如：1,2,3转换成'1','2','3'
        /// <summary>
        /// 转换值，例如：1,2,3转换成'1','2','3'
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string ConvertString(string ids)
        {
            StringBuilder str = new StringBuilder();
            foreach (string id in ids.Split(','))
            {
                str.Append("'" + id + "',");
            }

            return str.ToString().Length > 0 ? str.ToString().TrimEnd(',') : str.ToString();
        }
        #endregion

        #region 文件操作
        /// <summary>
        /// 显示文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string showFileContet(string path)
        {
            string str_content = "";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader Fso = new StreamReader(path);
                    str_content = Fso.ReadToEnd();
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
            }
            else
            {
                throw new Exception("找不到相应的文件!");
            }
            return str_content;
        }

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="path">写入文件的路径</param>
        /// <param name="content">写入文件的内容</param>
        /// <returns>返回一个Boolean值,表示文件是否写入成功</returns>
        public static Boolean WriteFile(string path, string content)
        {
            Encoding code = Encoding.GetEncoding("utf-8");//字符编码
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(path, false, code);
                writer.Write(content);

                writer.Flush();
                return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void FileDelete(string filePath)
        {
            if (filePath.IndexOf(":") == -1)
            {
                filePath = HttpContext.Current.Server.MapPath(filePath);
            }
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 删除文件夹及文件夹下面的目录、文件
        /// </summary>
        /// <param name="dirPath"></param>
        public static void DirectoryDelete(string dirPath)
        {
            if (dirPath.IndexOf(":") == -1)
            {
                dirPath = HttpContext.Current.Server.MapPath(dirPath);
            }
            if (Directory.Exists(dirPath))
            {
                try
                {
                    Directory.Delete(dirPath, true);
                }
                catch { }
            }
        }
        #endregion

        #region 设置访问过的分页列表
        /// <summary>
        /// 设置访问过的分页列表(有数量限制)
        /// </summary>
        /// <param name="strCookieName">cookie名称</param>
        /// <param name="strCookieKey">cookie key</param>
        /// <param name="strCookieValue">cookie 值</param>
        /// <param name="intMax">最大保存限制</param>
        public static void SetVisiteList(string strCookieName, string strCookieKey, string strCookieValue, int intMax)
        {
            string pageKey = System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToLower();
            int num = pageKey.LastIndexOf("list");
            if (num > 0)
            {
                string[] arrpage = pageKey.Substring(0, num).Split('/');
                pageKey = arrpage[arrpage.Length - 1];
            }
            else
            {
                pageKey = "";
            }

            strCookieKey = strCookieKey + Utils.ReqUrlParameter("NodeCode") + pageKey;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strCookieName];

            if (cookie != null)
            {
                if (cookie[strCookieKey] == null)  //如果当前cookie子集没有保存,则新增一个
                {
                    int iCount = cookie.Values.Count; //当前保存的cookie子集合数
                    if (iCount >= intMax) //如果子集合数超过最大保存限制,则删除最早添加的一个子集
                    {
                        cookie.Values.Remove(cookie.Values.GetKey(iCount - 1));
                    }
                    cookie.Values.Add(strCookieKey, strCookieValue);
                }
                else  //否则 直接改变当前子集的值
                {
                    cookie[strCookieKey] = strCookieValue;
                }
            }
            else
            {
                cookie = new HttpCookie(strCookieName);
                cookie.Values.Add(strCookieKey, strCookieValue);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        #region 获取url全部参数
        /// <summary>
        /// 获取url全部参数
        /// </summary>
        /// <returns></returns>
        public static string GetUrlParams()
        {
            string strUrl = HttpContext.Current.Request.Url.OriginalString;
            int index = strUrl.IndexOf("?");
            if (index != -1)
            {
                return strUrl.Substring(index + 1);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 分割字符串
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] strSplit(string strContent, string strSplit)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] strSplit(string strContent, string strValue, int count)
        {
            string[] result = new string[count];
            string[] splited = strSplit(strContent, strValue);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }
        #endregion

        #region sql条件拼接
        /// <summary>
        /// 过滤sql字段关键字,如果存在 则加上[]
        /// </summary>
        /// <param name="strColumnName">sql字段名</param>
        /// <returns></returns>
        public static string GetFilterKeyword(string strColumnName)
        {
            return "[" + strColumnName + "]";
        }

        /// <summary>
        /// sql条件拼接
        /// </summary>
        /// <param name="strKey">key</param>
        /// <param name="strValue">value</param>
        /// <param name="strType">连接符号 可为=,<>,>,<</param>
        /// <param name="strParamType">参数类型,可为 str,int等</param>
        /// <param name="sbSqlWhere">原始条件</param>
        public static void GetWhereAppend(string strKey, string strValue, string strValue2, string strType, KingTop.Model.SqlParamType strParamType, ref StringBuilder sbSqlWhere)
        {
            sbSqlWhere.Append(" AND [" + strKey + "] ");
            sbSqlWhere.Append(strType);
            if (strType.ToUpper() == "LIKE") //如果是like
            {
                sbSqlWhere.Append("'%" + strValue.Replace("'", "''") + "%'");
            }
            else if (strType.ToUpper() == "Between") //如果是Between
            {
                switch (strParamType) //参数类型
                {
                    case KingTop.Model.SqlParamType.Str:  //字符串则要加单引号

                        sbSqlWhere.Append("  between '" + strValue.Replace("'", "''") + "' and '" + strValue2.Replace("'", "''") + "'");
                        break;
                    case KingTop.Model.SqlParamType.Int:
                        sbSqlWhere.Append("  between " + strValue.Replace("'", "''") + " and " + strValue2.Replace("'", "''"));
                        break;
                    default:
                        sbSqlWhere.Append("  between " + strValue.Replace("'", "''") + " and " + strValue2.Replace("'", "''"));
                        break;
                }
            }
            else
            {
                switch (strParamType) //参数类型
                {
                    case KingTop.Model.SqlParamType.Str:  //字符串则要加单引号

                        sbSqlWhere.Append(" '" + strValue.Replace("'", "''") + "'");
                        break;
                    case KingTop.Model.SqlParamType.Int:
                        sbSqlWhere.Append(" " + strValue.Replace("'", "''"));
                        break;
                    default:
                        sbSqlWhere.Append(" " + strValue.Replace("'", "''"));
                        break;
                }
            }

        }

        /// <summary>
        /// sql条件拼接
        /// </summary>
        /// <param name="strKey">key</param>
        /// <param name="strValue">value</param>
        /// <param name="strType">连接符号 可为=,<>,>,<</param>
        /// <param name="strParamType">参数类型,可为 str,int等</param>
        /// <param name="sbSqlWhere">原始条件</param>
        public static void GetWhereAppend(string strKey, string strValue, string strType, KingTop.Model.SqlParamType strParamType, ref StringBuilder sbSqlWhere)
        {
            GetWhereAppend(strKey, strValue, "", strType, strParamType, ref sbSqlWhere);
        }
        #endregion

        #region 泛型集合Dictionary转SqlParams
        /// <summary>
        /// 泛型集合Dictionary转SqlParams
        /// </summary>
        /// <param name="dctWhere"></param>
        /// <returns></returns>
        public static System.Data.SqlClient.SqlParameter[] DictToSqlParams(Dictionary<string, string> dctWhere)
        {
            List<System.Data.SqlClient.SqlParameter> list = new List<System.Data.SqlClient.SqlParameter>();
            foreach (KeyValuePair<string, string> kvp in dctWhere)
            {
                list.Add(new System.Data.SqlClient.SqlParameter("@" + kvp.Key, kvp.Value));
            }
            return list.ToArray();
        }
        #endregion

        #region 日期操作
           /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
                return replacestr;

            if (datetimestr.Equals(""))
                return replacestr;

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }


        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            DateTime time = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out time))
                return time.ToString(formatStr);
            else
                return "";
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region 页面跳转
        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="strUrl">要跳转的地址</param>
        public static void UrlRedirect(string strUrl)
        {
            HttpContext.Current.Response.Redirect(strUrl);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要跳转的地址</param>
        /// <param name="strMessage">要弹出的消息</param>
        public static void UrlRedirect(System.Web.UI.Page typeClass, string strUrl, string strMessage)
        {
            UrlRedirect(typeClass, strUrl, strMessage, 2);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="typeClass">执行的类</param>
        /// <param name="strUrl">要跳转的地址</param>
        /// <param name="strMessage">要弹出的消息</param>
        /// <param name="strType">操作的类型 1为直接跳转 2为alert 3 为confrim 4为close</param>
        public static void UrlRedirect(System.Web.UI.Page typeClass, string strUrl, string strMessage, int iType)
        {
            StringBuilder sb = new StringBuilder(36);
            strMessage = strMessage.Replace("'", "''");
            sb.Append("<script>");
            switch (iType)
            {
                case 2:
                    sb.Append("alert('" + strMessage + "');");
                    break;
            }
            if (iType != 4 && iType != 5)
            {
                sb.Append("location.href='" + strUrl + "'");
            }
            sb.Append("</script>");
            typeClass.RegisterStartupScript("", sb.ToString());
        }
        #endregion

        #region 解析是或否

        public static string ParseIsOrNot(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = "是";
                    break;
                case "0":
                case "False":
                    parseValue = "否";
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 解析状态
        //状态图标
        public static string ParseState(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = GetResourcesValue("Common", "ON");
                    break;
                case "0":
                case "False":
                    parseValue = GetResourcesValue("Common", "OFF");
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        //状态名称 gavin by 2010-07-14
        public static string ParseStateTitle(object orginalValue, string defaultValue)
        {
            string parseValue;

            parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "1":
                case "True":
                    parseValue = GetResourcesValue("Common", "OnTitle");
                    break;
                case "0":
                case "False":
                    parseValue = GetResourcesValue("Common", "OffTitle");
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }

        public static string ParseModelFieldState(object orginalValue, string defaultValue)
        {
            string parseValue = "";

            switch (orginalValue.ToString().Trim())
            {
                case "True":
                case "1":
                    parseValue = "√";
                    break;
                case "False":
                case "0":
                    parseValue = "";
                    break;
                default:
                    parseValue = defaultValue;
                    break;
            }

            return parseValue;

        }
        #endregion

        #region 字段串是否为Null或为""(空)
        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }
        #endregion

        #region 清除查询字符串的危险字符
        /// <summary>
        /// 清除查询字符串的危险字符
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string CheckSql(string sql)
        {
            string reSql = "";
            if (sql == null)
            {
                return reSql;
            }
            else
            {
                reSql = sql;
                reSql = reSql.ToLower().Replace("\"", "&quot;");
                reSql = reSql.ToLower().Replace("<", "&lt;");
                reSql = reSql.ToLower().Replace(">", "&gt;");
                reSql = reSql.Replace("script", "&#115;cript");
                reSql = reSql.Replace("SCRIPT", "&#083;CRIPT");
                reSql = reSql.Replace("Script", "&#083;cript");
                reSql = reSql.Replace("script", "&#083;cript");
                reSql = reSql.Replace("object", "&#111;bject");
                reSql = reSql.Replace("OBJECT", "&#079;BJECT");
                reSql = reSql.Replace("Object", "&#079;bject");
                reSql = reSql.Replace("object", "&#079;bject");
                reSql = reSql.Replace("applet", "&#097;pplet");
                reSql = reSql.Replace("APPLET", "&#065;PPLET");
                reSql = reSql.Replace("Applet", "&#065;pplet");
                reSql = reSql.Replace("applet", "&#065;pplet");
                reSql = reSql.ToLower().Replace("[", "&#091;");
                reSql = reSql.ToLower().Replace("]", "&#093;");
                reSql = reSql.ToLower().Replace("=", "&#061;");
                reSql = reSql.ToLower().Replace("'", "''");
                reSql = reSql.ToLower().Replace("select", "select");
                reSql = reSql.ToLower().Replace("execute", "&#101xecute");
                reSql = reSql.ToLower().Replace("exec", "&#101xec");
                reSql = reSql.ToLower().Replace("join", "join");
                reSql = reSql.ToLower().Replace("union", "union");
                reSql = reSql.ToLower().Replace("where", "where");
                reSql = reSql.ToLower().Replace("insert", "insert");
                reSql = reSql.ToLower().Replace("delete", "delete");
                reSql = reSql.ToLower().Replace("update", "update");
                reSql = reSql.ToLower().Replace("like", "like");
                reSql = reSql.ToLower().Replace("drop", "drop");
                reSql = reSql.ToLower().Replace("create", "create");
                reSql = reSql.ToLower().Replace("rename", "rename");
                reSql = reSql.ToLower().Replace("count", "co&#117;nt");
                reSql = reSql.ToLower().Replace("chr", "c&#104;r");
                reSql = reSql.ToLower().Replace("mid", "m&#105;d");
                reSql = reSql.ToLower().Replace("truncate", "trunc&#097;te");
                reSql = reSql.ToLower().Replace("nchar", "nch&#097;r");
                reSql = reSql.ToLower().Replace("char", "ch&#097;r");
                reSql = reSql.ToLower().Replace("alter", "alter");
                reSql = reSql.ToLower().Replace("cast", "cast");
                reSql = reSql.ToLower().Replace("exists", "e&#120;ists");
                reSql = reSql.ToLower().Replace("\n", "<br>");
                return reSql;
            }
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// 获取真正的MD5加密
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string getMD5(string encypStr)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "");
            return retStr;
        }
        #endregion

        #region 接收表单或者参数值（Reqeust）
        public static string RequestStr(string queryName)
        {

            if (HttpContext.Current.Request[queryName] == null)
                return "";
            else
            {
                try
                {
                    return Convert.ToString(HttpContext.Current.Request[queryName]);
                }
                catch
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// HttpContext.Current.Request[key]
        /// </summary>
        /// <param name="queryName">参数名</param>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        public static bool RequestStr(string queryName, bool defaultStr)
        {

            if (HttpContext.Current.Request[queryName] == null)
                return defaultStr;
            else
            {
                try
                {
                    return ParseBool(HttpContext.Current.Request[queryName]);
                }
                catch
                {
                    return defaultStr;
                }
            }
        }

        public static int RequestStr(string queryName, int defaultStr)
        {

            if (HttpContext.Current.Request[queryName] == null)
                return defaultStr;
            else
            {
                try
                {
                    return ParseInt(Convert.ToString(HttpContext.Current.Request[queryName]), 0);
                }
                catch
                {
                    return defaultStr;
                }
            }
        }
        #endregion

        #region 邮件发送
        public static void SendMail(string M_from, string[] M_to, string M_UserName, string M_Password,string M_SMTP,int M_Port, string title, string msgContent)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress(M_from);

            foreach (string to_mail in M_to)
            {
                mailMessage.To.Add(to_mail);
            }

            mailMessage.Subject = title;
            mailMessage.Body = msgContent;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = System.Net.Mail.MailPriority.Normal;

            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = M_SMTP;
            mail.EnableSsl = true;
            mail.Port = M_Port;

            mail.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            mail.UseDefaultCredentials = false;
            mail.Credentials = new System.Net.NetworkCredential(M_UserName, M_Password);
            mail.Send(mailMessage);
        }

        public static void SendMail(string SiteDir, string[] ToMail, string Title, string msgContent)
        {
            string path = HttpContext.Current.Server .MapPath ("~/" + SiteDir + "/config/Post.config");
            string smtp = Utils.XmlRead(path, "PostConfig/SmtpServer", "");
            string fromEmail = Utils.XmlRead(path, "PostConfig/Email", "");
            string pwd = Utils.XmlRead(path, "PostConfig/Password", "");
            if (!string.IsNullOrEmpty(pwd))
            {
                pwd=DesSecurity.DesDecrypt(pwd, "emailpwd");
            }
            int port = Utils.ParseInt(Utils.XmlRead(path, "PostConfig/Port", ""),25);

            SendMail(fromEmail, ToMail, fromEmail, pwd, smtp, port, Title, msgContent);
        }
        #endregion

        #region 纯文本转换HTML格式
        public static string TxtTHtml(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("  ", "&nbsp;&nbsp;").Replace("\r\n", "<br>").Replace("\n", "<br>");
            }

            return s;
        }
        #endregion
    }
}
