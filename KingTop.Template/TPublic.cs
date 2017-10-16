#region 引用程序集
using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.XPath;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web;
using System.Net;
using System.IO;

using KingTop.Template.ParamType;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-09
// 功能描述：内容发布 -- 公用方法
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    public class TPublic
    {
        #region 变量
        /// <summary>
        /// 首页默认文件名
        /// </summary>
        public const string defaultFileName = "default";
        /// <summary>
        /// 列表默认文件名
        /// </summary>
        public const string listFileName = "index";
        /// <summary>
        /// 栏目列表
        /// </summary>
        public DataTable dtMenuList;
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        private static string[] zeroArray = new string[] { "", "0", "00", "000", "0000", "00000", "000000", "0000000", "00000000", "000000000", "0000000000", "0000000000" };
        #endregion

        #region 处理栏目列表 添加栏目路径列
        /// <summary>
        /// 处理栏目列表 添加栏目路径列
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <param name="siteUrl">当前站点URL或配置的子域名</param>
        /// <param name="rootTemplate">当前站点模板根目录</param>
        /// <returns></returns>
        public DataTable DealMenuList(int siteID, string siteUrl, string rootTemplate, string fileType)
        {
            string nodeCode;                 // 临时变量,栏目NodeCode
            DataRow[] arrDr;
            int orders;
            bool hasPrevSinglePage;
            FirstMenuDirParam param;

            orders = 1;
            dtMenuList = dal.GetMenuList(siteID);

            if (dtMenuList != null)
            {
                dtMenuList.Columns.Add(new DataColumn("RootDirPath"));
                dtMenuList.Columns.Add(new DataColumn("HasTemplateRecently"));
                dtMenuList.Columns.Add(new DataColumn("MenuUrl"));

                foreach (DataRow dr in dtMenuList.Rows)
                {
                    dr["RootDirPath"] = GetRootDirPath(dr, "");
                    dr["NodelOrder"] = orders;
                    orders++;
                }

                if (!string.IsNullOrEmpty(rootTemplate))
                {
                    foreach (DataRow dr in dtMenuList.Rows)
                    {
                        if (dr["TableName"].ToString().Trim() == "K_SinglePage")
                        {
                            if (dr["DefaultTemplate"].ToString().Trim() == "")
                            {
                                dr["DefaultTemplate"] = GetDefaultTemplate(dr["NodeCode"].ToString(), rootTemplate, TemplateType.Single);
                            }
                        }
                        else
                        {
                            if (dr["DefaultTemplate"].ToString().Trim() == "")
                            {
                                dr["DefaultTemplate"] = GetDefaultTemplate(dr["NodeCode"].ToString(), rootTemplate, TemplateType.Index);
                            }

                            if (dr["ListPageTemplate"].ToString().Trim() == "" && dr["NodeType"].ToString().Trim() == "0")
                            {
                                dr["ListPageTemplate"] = GetDefaultTemplate(dr["NodeCode"].ToString(), rootTemplate, TemplateType.List);
                            }

                            if (dr["ContentTemplate"].ToString().Trim() == "")
                            {
                                dr["ContentTemplate"] = GetDefaultTemplate(dr["NodeCode"].ToString(), rootTemplate, TemplateType.Content);
                            }
                        }
                    }
                }

                foreach (DataRow dr in dtMenuList.Rows)
                {
                    dr["MenuUrl"] = dr["RootDirPath"];

                    if (dr["DefaultTemplate"].ToString().Trim() == "" && dr["ListPageTemplate"].ToString().Trim() == "")    // 当前栏目没有模板需查找最近的有模板的节点
                    {
                        nodeCode = GetHasTemplateRecentlyNode(dr["NodeCode"].ToString());
                        dr["HasTemplateRecently"] = nodeCode;
                    }

                    if (dr["TableName"].ToString().Trim() == "K_SinglePage")
                    {
                        hasPrevSinglePage = HasPrevSingleMenu(dr["NodeCode"].ToString());
                        param = GetFirstMenuDir(dr["NodeCode"].ToString());                                           //  一级栏目目录

                        if (dr["NodeCode"].ToString().Length <= 6 || param.HasDefaultTemplate || hasPrevSinglePage)   // 一级栏目存在首页模板会生成首页 或 当前栏目之前存在单页栏目
                        {
                            dr["MenuUrl"] = param.MenuDir + dr["NodelEngDesc"].ToString() + "." + fileType;
                        }
                        else  // 生成栏目首页
                        {
                            dr["MenuUrl"] = param.MenuDir + defaultFileName + "." + fileType;
                        }
                    }
                }

                foreach (DataRow dr in dtMenuList.Rows)
                {
                    if (dr["HasTemplateRecently"].ToString().Trim() != "")
                    {
                        arrDr = this.dtMenuList.Select("NodeCode='" + dr["HasTemplateRecently"].ToString().Trim() + "'");

                        if (arrDr != null && arrDr.Length > 0)
                        {
                            dr["MenuUrl"] = arrDr[0]["MenuUrl"];
                        }
                    }

                    SetMenuIsShow(dr["NodeCode"].ToString(), dr["NodeCode"].ToString(), "IsTopMenuShow");
                    SetMenuIsShow(dr["NodeCode"].ToString(), dr["NodeCode"].ToString(), "IsLeftMenuShow");
                }
            }

            return dtMenuList;
        }
        #endregion

        #region 递归设置顶级栏目及子栏目的显示与否
        private void SetMenuIsShow(string nodeCode,string parentNode,string fieldName)
        {
            DataRow[] parentDR;
            DataRow[] cuurnetDR;

            parentDR = this.dtMenuList.Select("NodeCode='"+ parentNode +"'");

            if (parentNode.Length >= 6 && parentDR != null && parentDR.Length >0)
            {
                if (!Common.Utils.ParseBool(parentDR[0][fieldName]))
                {
                    cuurnetDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");

                    if (cuurnetDR != null && cuurnetDR.Length > 0)
                    {
                        cuurnetDR[0][fieldName] = false;
                    }
                }
                else
                {
                    SetMenuIsShow(nodeCode, parentDR[0]["ParentNode"].ToString(), fieldName);
                }
            }
        }
        #endregion 

        #region 递归获取栏目最近的有模板的节点
        private string GetHasTemplateRecentlyNode(string nodeCode)
        {
            string menuNodeCode;
            DataRow[] arrMenu;

            menuNodeCode = string.Empty;
            arrMenu = this.dtMenuList.Select("ParentNode='" + nodeCode + "'");

            foreach (DataRow menu in arrMenu)
            {
                if (menu["DefaultTemplate"].ToString().Trim() != "" || menu["ListPageTemplate"].ToString().Trim() != "")
                {
                    menuNodeCode = menu["NodeCode"].ToString();
                    break;
                }

                menuNodeCode = GetHasTemplateRecentlyNode(menu["NodeCode"].ToString());
            }

            return menuNodeCode;
        }
        #endregion

        #region  递归获取栏目根路径
        private string GetRootDirPath(DataRow dr, string dirPath)
        {
            string rootDirPath;
            DataRow[] parentNode;

            rootDirPath = string.Empty;

            if (dr != null)
            {
                rootDirPath = dr["NodeDir"].ToString();

                if (rootDirPath.Trim() != "")
                {
                    if (dirPath.Trim() != "")
                    {
                        rootDirPath = rootDirPath + "/" + dirPath;
                    }
                    else
                    {
                        rootDirPath = rootDirPath + "/";
                    }
                }
                else
                {
                    rootDirPath = dirPath;
                }

                parentNode = this.dtMenuList.Select("NodeCode='" + dr["ParentNode"].ToString() + "'");

                if (parentNode != null && parentNode.Length > 0)
                {
                    rootDirPath = GetRootDirPath(parentNode[0], rootDirPath);
                }
            }

            return rootDirPath;
        }
        #endregion

        #region 格式化输出字段内容
        /// <summary>
        /// 格式化输出字段内容
        /// </summary>
        /// <param name="fieldContent">字段内容</param>
        /// <param name="outType">输出类型</param>
        /// <param name="outParam">输出类型参数</param>
        /// <returns></returns>
        public string FormatFieldContent(string fieldContent, string outType, string[] outParam)
        {
            string oldValue;    // 原内容
            string newValue;    // 格式化后的内容

            oldValue = fieldContent;
            newValue = fieldContent;

            switch (outType)
            {
                case "1":           // 文本类型
                    if (outParam.Length > 1)
                    {
                        int strLength;

                        strLength = Common.Utils.ParseInt(outParam[0], 0);

                        if (strLength > 0)  // 截取字符串
                        {
                            oldValue = Regex.Replace(oldValue, @"\<[^>]+\>|\</[^>]+|>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            if (oldValue.Length > strLength)
                            {
                                newValue = oldValue.Substring(0, strLength);
                            }
                            else
                            {
                                newValue = oldValue;
                            }

                            if (outParam[1] == "1")
                            {
                                newValue += "...";
                            }
                        }

                    }
                    break;
                case "2":           // 数字类型
                    float oldNumValue;

                    try
                    {
                        oldNumValue = float.Parse(oldValue);
                    }
                    catch
                    {
                        break;
                    }

                    switch (outParam[0])
                    {
                        case "1":       // 整数
                            try
                            {
                                if (outParam.Length > 1 && outParam[1].Trim() != "")
                                {
                                    newValue = string.Format("{0:0." + zeroArray[Common.Utils.ParseInt(outParam[1], 0)] + "}", oldNumValue);
                                }
                                else
                                {
                                    newValue = ((int)oldNumValue).ToString();
                                }
                            }
                            catch { }

                            break;
                        case "2":       // 小数
                            if (outParam.Length > 1 && outParam[1].Trim() != "")
                            {
                                newValue = string.Format("{0:0." + zeroArray[Common.Utils.ParseInt(outParam[1], 0)] + "}", oldNumValue);
                            }
                            break;
                        case "3":       // 百分数
                            newValue = string.Format("{0:0%}", oldNumValue);
                            break;
                    }

                    break;
                case "3":           // 日期类型
                    try
                    {
                        newValue = string.Format("{0:" + outParam[0] + "}", DateTime.Parse(oldValue));
                    }
                    catch { }
                    break;
                case "4":           // 是否（布尔）型
                    if (outParam.Length > 1)
                    {
                        if (Common.Utils.ParseBool(oldValue))
                        {
                            newValue = outParam[0];
                        }
                        else
                        {
                            newValue = outParam[1];
                        }
                    }
                    break;
            }

            return newValue;
        }
        #endregion

        #region 解析字段参数
        /// <summary>
        /// 解析字段参数
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="param">输出样式参数</param>
        /// <returns></returns>
        public Field GetFieldParam(string fieldName, string fieldType, string param)
        {
            Field fieldParam;             // 字段参数
            string[] outParam;                          // 输出格式参数
            string[] arrParam;                          // 临时变量,保存截取的参数
            fieldParam = new Field();
            fieldParam.Name = fieldName.Trim();
            fieldParam.OutType = fieldType;

            if (param.Trim() != null)
            {
                switch (fieldType)
                {
                    case "1":       // 文本类型 {$Field(1,字段名称,截取长度,截断处理)}
                        arrParam = param.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        if (arrParam.Length > 1)
                        {
                            outParam = arrParam;
                        }
                        else
                        {
                            outParam = new string[] { "0", "0" };
                        }

                        break;
                    case "2":       // 数字类型 {$Field(2,字段名称,数字类型,参数)}   数字类型 1 整数 2 小数  3 百分数   参数：为小数时位数
                        arrParam = param.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        if (arrParam.Length > 0 && !string.IsNullOrEmpty(arrParam[0]))
                        {
                            outParam = arrParam;
                        }
                        else
                        {
                            outParam = new string[] { "1" };
                        }

                        break;
                    case "3":       // 日期类型 {$Field(3,字段名称,格式)} 格式 yyyy-MM-dd
                        outParam = new string[] { param };
                        break;
                    case "4":       // 是否（布尔）型 {$Field(4,字段名称,为真输出，为假输出)}
                        arrParam = param.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        if (arrParam.Length > 1 && !string.IsNullOrEmpty(arrParam[1]))
                        {
                            outParam = arrParam;
                        }
                        else
                        {
                            outParam = new string[] { "是", "否" };
                        }

                        break;
                    default:
                        outParam = null;
                        break;
                }
            }
            else
            {
                outParam = null;
            }

            fieldParam.OutParam = outParam;
            return fieldParam;
        }
        #endregion

        #region 获取内容目录URL
        /// <summary>
        /// 获取内容目录URL
        /// </summary>
        /// <param name="siteUrl">站点URL</param>
        /// <param name="menuDirUrlOrNodeCode">栏目相对URL或NodeCode</param>
        /// <param name="cSaveType">内容页面保存方式</param>
        /// <param name="addDate">记录添加日期</param>
        /// <returns></returns>
        public string GetContentDirUrl(string siteUrl, string menuDirUrlOrNodeCode, ContentPageSaveType cSaveType, string addDate)
        {
            string cDirUrl;
            string menuDirUrl;
            string topMenuDirName;
            DataRow[] drMenu;

            if (Regex.IsMatch(menuDirUrlOrNodeCode, @"\d+"))    // menuDirUrlOrNodeCode 为NodeCode
            {
                drMenu = this.dtMenuList.Select("NodeCode='" + menuDirUrlOrNodeCode + "'");

                if (drMenu != null && drMenu.Length > 0)
                {
                    menuDirUrl = drMenu[0]["RootDirPath"].ToString();     // 当前发布栏目URL
                }
                else
                {
                    menuDirUrl = "";
                }
            }
            else   // 栏目相对URL
            {
                menuDirUrl = menuDirUrlOrNodeCode;
            }

            switch (cSaveType)
            {
                case ContentPageSaveType.Content:
                    cDirUrl = siteUrl + "c/";
                    break;
                case ContentPageSaveType.ContentAndDate:
                    cDirUrl = siteUrl + "c/" + string.Format("{0:yyMMdd}", DateTime.Parse(addDate)) + "/";
                    break;
                case ContentPageSaveType.Menu:
                    if (menuDirUrl.Contains("/"))
                    {
                        topMenuDirName = menuDirUrl.Split(new char[] { '/' })[0];
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = siteUrl + topMenuDirName + "/";
                    break;
                case ContentPageSaveType.MenuAndContent:
                    if (menuDirUrl.Contains("/"))
                    {
                        topMenuDirName = menuDirUrl.Split(new char[] { '/' })[0];
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = siteUrl + topMenuDirName + "/c/";
                    break;
                case ContentPageSaveType.MenuAndDate:
                    if (menuDirUrl.Contains("/"))
                    {
                        topMenuDirName = menuDirUrl.Split(new char[] { '/' })[0];
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = siteUrl + topMenuDirName + "/" + string.Format("{0:yyMM}", DateTime.Parse(addDate)) + "/";
                    break;
                case ContentPageSaveType.MenuContentAndDate:
                    if (menuDirUrl.Contains("/"))
                    {
                        topMenuDirName = menuDirUrl.Split(new char[] { '/' })[0];
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = siteUrl + topMenuDirName + "/c/" + string.Format("{0:yyMM}", DateTime.Parse(addDate)) + "/";
                    break;
                default:
                    cDirUrl = siteUrl;
                    break;
            }

            return cDirUrl;
        }
        #endregion

        #region 获取内容目录Path
        /// <summary>
        /// 获取内容目录Path
        /// </summary>
        /// <param name="siteUrl">站点Path</param>
        /// <param name="menuDirURL">栏目相对站点的路径</param>
        /// <param name="cSaveType">内容页面保存方式</param>
        /// <param name="addDate">记录添加日期</param>
        /// <returns></returns>
        public string GetContentDirPath(string sitePath, string menuDirUrl, ContentPageSaveType cSaveType, string addDate)
        {
            string cDirUrl;
            string topMenuDirName;
            string[] arrMenuName;

            switch (cSaveType)
            {
                case ContentPageSaveType.Content:
                    cDirUrl = sitePath + "c\\";
                    break;
                case ContentPageSaveType.ContentAndDate:
                    cDirUrl = sitePath + "c\\" + string.Format("{0:yyMMdd}", DateTime.Parse(addDate)) + "\\";
                    break;
                case ContentPageSaveType.Menu:
                    if (menuDirUrl.Contains("/"))
                    {
                        arrMenuName =  menuDirUrl.Split(new char[] { '/' });
                        topMenuDirName = arrMenuName[0];

                        if (topMenuDirName.Trim() == "" && arrMenuName.Length > 1)
                        {
                            topMenuDirName = arrMenuName[1];
                        }
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = sitePath + topMenuDirName + "\\";
                    break;
                case ContentPageSaveType.MenuAndContent:
                    if (menuDirUrl.Contains("/"))
                    {
                        arrMenuName = menuDirUrl.Split(new char[] { '/' });
                        topMenuDirName = arrMenuName[0];

                        if (topMenuDirName.Trim() == "" && arrMenuName.Length > 1)
                        {
                            topMenuDirName = arrMenuName[1];
                        }
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = sitePath + topMenuDirName + "\\c\\";
                    break;
                case ContentPageSaveType.MenuAndDate:
                    if (menuDirUrl.Contains("/"))
                    {
                        arrMenuName = menuDirUrl.Split(new char[] { '/' });
                        topMenuDirName = arrMenuName[0];

                        if (topMenuDirName.Trim() == "" && arrMenuName.Length > 1)
                        {
                            topMenuDirName = arrMenuName[1];
                        }
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = sitePath + topMenuDirName + "\\" + string.Format("{0:yyMM}", DateTime.Parse(addDate)) + "\\";
                    break;
                case ContentPageSaveType.MenuContentAndDate:
                    if (menuDirUrl.Contains("/"))
                    {
                        arrMenuName = menuDirUrl.Split(new char[] { '/' });
                        topMenuDirName = arrMenuName[0];

                        if (topMenuDirName.Trim() == "" && arrMenuName.Length > 1)
                        {
                            topMenuDirName = arrMenuName[1];
                        }
                    }
                    else
                    {
                        topMenuDirName = menuDirUrl;
                    }

                    cDirUrl = sitePath + topMenuDirName + "\\c\\" + string.Format("{0:yyMM}", DateTime.Parse(addDate)) + "\\";
                    break;
                default:
                    cDirUrl = sitePath;
                    break;
            }

            return cDirUrl;
        }
        #endregion

        #region 路径替换
        /// <summary>
        /// 路径替换
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="siteUrl">站点URL</param>
        public void FormatUrl(ref string content, string siteUrl)
        {
            content = Regex.Replace(content, @"(?<C>[""']\s*|\(\s*|\(')((\.\./)+|/)skins/", "${C}" + siteUrl + "skins/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<1>[^/]{1})(?<2>skins/)", "${1}" + siteUrl + "${2}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<C>[""']\s*|\(\s*|\(')((\.\./)+|/)IncludeFile/", "${C}" + siteUrl + "IncludeFile/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<1>[^/]{1})(?<2>IncludeFile/)", "${1}" + siteUrl + "${2}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<1>[^/]{1})(?<2>Plus/Form/)", "${1}" + siteUrl + "${2}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<1>[^/]{1})(?<2>Plus/Common/)", "${1}" + siteUrl + "${2}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<1>[^/]{1})(?<2>plus/comment/)", "${1}" + siteUrl + "${2}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(?<1>\<!--#include\s+file\s*=\s*[""'])(?<2>http://[^/""']+)(?<3>[^""']+[""']\s*--\>)", "${1}${3}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
        #endregion

        #region 获取内容保存方式
        /// <summary>
        /// 获取内容保存方式
        /// </summary>
        /// <param name="contentPageHtmlRule">内容页保存规则</param>
        /// <returns></returns>
        public ContentPageSaveType GetContentPageSaveType(string contentPageHtmlRule)
        {
            ContentPageSaveType cSaveType;

            switch (contentPageHtmlRule)  // 内容保存方式
            {
                case "2":
                    cSaveType = ContentPageSaveType.Content;
                    break;
                case "3":
                    cSaveType = ContentPageSaveType.Menu;
                    break;
                case "4":
                    cSaveType = ContentPageSaveType.MenuAndDate;
                    break;
                case "5":
                    cSaveType = ContentPageSaveType.MenuContentAndDate;
                    break;
                case "6":
                    cSaveType = ContentPageSaveType.MenuAndContent;
                    break;
                default:
                    cSaveType = ContentPageSaveType.ContentAndDate;
                    break;
            }
            return cSaveType;
        }
        #endregion

        #region 获取节点的一级栏目目录
        /// <summary>
        /// 获取节点的一级栏目目录
        /// </summary>
        /// <param name="nodeCode">栏目NodeCode</param>
        /// <returns></returns>
        public FirstMenuDirParam GetFirstMenuDir(string nodeCode)
        {
            FirstMenuDirParam param;  // 一级栏目目录参数

            param = new FirstMenuDirParam();
            GetMenuDir(nodeCode, ref param);
            return param;
        }

        private void GetMenuDir(string nodeCode, ref FirstMenuDirParam param)
        {
            DataRow[] arrDr;          // 当前栏目
            DataRow[] drParent;       // 父栏目
            string parentNodeCode;    // 当前栏目父级NODECODE


            arrDr = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");

            if (arrDr != null && arrDr.Length > 0)
            {
                parentNodeCode = arrDr[0]["ParentNode"].ToString();
                drParent = this.dtMenuList.Select("NodeCode='" + parentNodeCode + "'");

                if (drParent != null && drParent.Length > 0)  // 存在父栏目
                {
                    GetMenuDir(parentNodeCode, ref param);
                }
                else
                {
                    param.MenuDir = arrDr[0]["RootDirPath"].ToString();

                    if (arrDr[0]["DefaultTemplate"].ToString().Trim() != "")
                    {
                        param.HasDefaultTemplate = true;
                    }
                    else
                    {
                        param.HasDefaultTemplate = false;
                    }
                }
            }
        }
        #endregion

        #region 判断当前栏目之前有没有单页栏目
        public bool HasPrevSingleMenu(string nodeCode)
        {
            bool isTrue;
            DataRow[] arrDR;
            string parentNode;
            string order;

            isTrue = false;
            arrDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");

            if (arrDR.Length > 0 && nodeCode.Trim().Length > 6)
            {
                parentNode = arrDR[0]["ParentNode"].ToString();
                order = arrDR[0]["NodelOrder"].ToString();
                arrDR = this.dtMenuList.Select("ParentNode='" + parentNode + "' and NodelOrder < " + order + " and TableName='K_SinglePage'");

                if (arrDR.Length > 0)
                {
                    isTrue = true;
                }
                else
                {
                    arrDR = this.dtMenuList.Select("ParentNode='" + parentNode + "' and NodelOrder < " + order);

                    if (arrDR != null && arrDR.Length > 0)
                    {
                        foreach (DataRow dr in arrDR)
                        {
                            if (!isTrue)
                            {
                                CheckParentNodeHasSingleMenu(ref isTrue, dr["ParentNode"].ToString());
                            }
                        }
                    }
                    else if (parentNode.Length > 6)
                    {
                        isTrue = HasPrevSingleMenu(parentNode);
                    }
                }
            }

            return isTrue;
        }

        private void CheckParentNodeHasSingleMenu(ref bool isTrue, string pNodeCode)
        {
            DataRow[] arrDR;
            DataRow[] pDR;

            pDR = this.dtMenuList.Select("NodeCode='" + pNodeCode + "'");

            if (pDR.Length > 0)
            {
                arrDR = this.dtMenuList.Select("ParentNode='" + pDR[0]["ParentNode"].ToString() + "' and NodelOrder < " + pDR[0]["NodelOrder"].ToString() + " and TableName='K_SinglePage'");

                if (arrDR.Length > 0)
                {
                    isTrue = true;
                }
                else
                {
                    arrDR = this.dtMenuList.Select("ParentNode='" + pDR[0]["ParentNode"].ToString() + "' and NodelOrder < " + pDR[0]["NodelOrder"].ToString());

                    if (arrDR.Length > 0)
                    {
                        foreach (DataRow dr in arrDR)
                        {
                            CheckParentNodeHasSingleMenu(ref isTrue, dr["ParentNode"].ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #region 绑定列表单记录
        public string SingleRecordBind(DataRow dr, List<Field> lstField, string itemTemplate, string menuDir, string fileType, string siteUrl, ContentPageSaveType cSaveType)
        {
            string itemContent;
            string fieldValue;
            string cDirUrl;

            itemContent = itemTemplate;

            if (cSaveType == 0)
            {
                cSaveType = ContentPageSaveType.ContentAndDate;
            }

            try
            {
                cDirUrl = this.GetContentDirUrl(siteUrl, menuDir, cSaveType, dr["AddDate"].ToString());

                try
                {
                    itemContent = itemContent.Replace("{$LoopPageName}", cDirUrl + dr["ID"].ToString() + "." + fileType);
                }
                catch
                {
                    itemContent = itemContent.Replace("{$LoopPageName}", "标签数据源中不存在ID字段");
                }

                itemContent = itemContent.Replace("{$AbsoulteMenuUrl}", cDirUrl);
                itemContent = itemContent.Replace("{$RelativeMenuUrl}", cDirUrl.Replace(siteUrl, ""));
            }
            catch
            {
                itemContent = itemContent.Replace("{$LoopPageName}", "标签数据源中不存在AddDate字段");
            }

            foreach (Field field in lstField)    // 绑定字段值
            {
                if (dr[field.Name] != null)
                {
                    fieldValue = dr[field.Name].ToString();

                    if (field.OutParam != null && field.OutParam.Length > 0)
                    {
                        fieldValue = this.FormatFieldContent(fieldValue, field.OutType, field.OutParam);
                    }

                    itemContent = itemContent.Replace("{[#" + field.Name + "#]}", fieldValue);
                }
            }

            return itemContent;
        }
        #endregion

        #region 主模型绑定列表单记录
        public string SingleRecordBind(DataRow dr, List<Field> lstField, string itemTemplate, string menuDir, string fileType, string siteUrl, ContentPageSaveType cSaveType, List<SubModelParam> lstSubModel)
        {
            string itemContent;
            string fieldValue;
            string cDirUrl;

            itemContent = itemTemplate;

            try
            {
                cDirUrl = this.GetContentDirUrl(siteUrl, menuDir, cSaveType, dr["AddDate"].ToString());

                try
                {
                    itemContent = itemContent.Replace("{$LoopPageName}", cDirUrl + dr["ID"].ToString() + "." + fileType);
                }
                catch
                {
                    itemContent = itemContent.Replace("{$LoopPageName}", "标签数据源中不存在ID字段");
                }

                itemContent = itemContent.Replace("{$AbsoulteMenuUrl}", cDirUrl);
                itemContent = itemContent.Replace("{$RelativeMenuUrl}", cDirUrl.Replace(siteUrl, ""));

                foreach (SubModelParam subModel in lstSubModel)
                {
                    itemContent = itemContent.Replace(subModel.LabelName, siteUrl + "List/" + dr["ID"].ToString() + subModel.FieldName + "." + fileType);
                }

            }
            catch
            {
                itemContent = itemContent.Replace("{$LoopPageName}", "标签数据源中不存在AddDate字段");
            }

            foreach (Field field in lstField)    // 绑定字段值
            {
                if (dr[field.Name] != null)
                {
                    fieldValue = dr[field.Name].ToString();

                    if (field.OutParam != null && field.OutParam.Length > 0)
                    {
                        fieldValue = this.FormatFieldContent(fieldValue, field.OutType, field.OutParam);
                    }

                    itemContent = itemContent.Replace("{[#" + field.Name + "#]}", fieldValue);
                }
            }

            return itemContent;
        }
        #endregion

        #region 获取自由标签解析后的参数
        /// <summary>
        /// 获取自由标签解析后的参数
        /// </summary>
        /// <param name="labelContent">标签内容</param>
        /// <param name="itemContentTag">ItemTemplate内容替换标签</param>
        /// <returns></returns>
        public LoopLabelParseParam GetLoopLabelParam(string labelContent, string itemContentTag)
        {
            LoopLabelParseParam resultParam;            // 解析后的参数
            Regex loopReg;                              // 匹配循环标签
            Regex fieldReg;                             // 匹配字段
            Match loopMatch;
            MatchCollection fieldMatch;                 // 字段匹配内容
            string itemTemplateContent;                 // 循环体内容
            List<Field> lstFieldParam;    // 所有字段参数
            Field fieldParam;             // 字段参数

            loopReg = new Regex(@"\[HQB\.Loop\](?<1>.*)\[/HQB\.Loop\]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            fieldReg = new Regex(@"\{\$Field\((?<1>\d),(?<2>[^,]+),(?<3>[^)]*)\)\}|\{\$Field\((?<1>\d),(?<2>[^,]+)\)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            resultParam = new LoopLabelParseParam();
            lstFieldParam = new List<Field>();

            loopMatch = loopReg.Match(labelContent);

            if (loopMatch.Success)  // 循环标签
            {
                resultParam.IsLoop = true;
                resultParam.LabelTemplate = labelContent.Replace(loopMatch.Value, itemContentTag);
                itemTemplateContent = loopMatch.Groups[1].Value;

            }
            else
            {
                resultParam.IsLoop = false;
                resultParam.LabelTemplate = itemContentTag;
                itemTemplateContent = labelContent;
            }

            fieldMatch = fieldReg.Matches(itemTemplateContent);     // 匹配循环体中的所有字段

            foreach (Match matchItem in fieldMatch)
            {
                fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                itemTemplateContent = itemTemplateContent.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                lstFieldParam.Add(fieldParam);
            }

            resultParam.LstField = lstFieldParam;
            resultParam.ItemContent = itemTemplateContent;

            return resultParam;
        }
        #endregion

        #region 页面中添加JS，CSS文件引用
        public void AddFileRef(string insertCode, ref string htmlContent)
        {
            Regex reg;
            Match match;

            reg = new Regex(@"\<head(\s*[\w-]+\s*=\s*[""'][^""']+[""']\s*)*\>(?<1>.*?)\</head\>");
            match = reg.Match(htmlContent);

            if (match.Success)
            {
                htmlContent = htmlContent.Replace(match.Groups[1].Value, match.Groups[1].Value + insertCode);
            }
        }
        #endregion

        #region 查找默认模板
        public string GetDefaultTemplate(string nodeCode, string rootTemplate, TemplateType tempType)
        {
            string templatePath;   // 找到的模板路径

            templatePath = GetDefaultMenuTemplate(nodeCode, rootTemplate, tempType);
            return templatePath;
        }

        private string GetDefaultMenuTemplate(string nodeCode, string rootTemplate, TemplateType tempType)
        {
            DataRow[] drMenu;
            string templatePath;   // 找到的模板路径
            string tempVar;        // 临时变量

            templatePath = string.Empty;
            tempVar = string.Empty;
            drMenu = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");

            if (drMenu != null && drMenu.Length > 0)
            {
                switch (tempType)
                {
                    case TemplateType.Index:
                        tempVar = rootTemplate + drMenu[0]["RootDirPath"].ToString().Replace("/", "\\") + "Index.html";
                        break;
                    case TemplateType.Content:
                        tempVar = rootTemplate + drMenu[0]["RootDirPath"].ToString().Replace("/", "\\") + "Content.html";
                        break;
                    case TemplateType.List:
                        tempVar = rootTemplate + drMenu[0]["RootDirPath"].ToString().Replace("/", "\\") + "List.html";
                        break;
                    case TemplateType.Single:
                        tempVar = rootTemplate + drMenu[0]["RootDirPath"].ToString().Replace("/", "\\") + drMenu[0]["NodelEngDesc"].ToString() + "\\Single.html";

                        if (!File.Exists(tempVar))
                        {
                            tempVar = rootTemplate + drMenu[0]["RootDirPath"].ToString().Replace("/", "\\") + "\\Single.html";
                        }
                        break;
                }

                if (File.Exists(tempVar))
                {
                    templatePath = tempVar;
                }
                else
                {
                    drMenu = this.dtMenuList.Select("NodeCode='" + drMenu[0]["ParentNode"].ToString() + "'");

                    if (drMenu != null && drMenu.Length > 0)
                    {
                        templatePath = GetDefaultMenuTemplate(drMenu[0]["NodeCode"].ToString(), rootTemplate, tempType);
                    }
                }
            }

            templatePath = templatePath.Replace(rootTemplate, "");
            return templatePath;
        }
        #endregion

    }
}