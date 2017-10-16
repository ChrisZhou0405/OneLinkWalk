#region 程序集引用
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

using KingTop.Template.ParamType;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-08
// 功能描述：内容发布 -- 分页标签  各分页样式实现了解析接口
// 更新日期        更新人     更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    #region 分页操作类
    /// <summary>
    /// 分页操作类
    /// </summary>
    public class Split
    {
        #region 变量成员
        private ISplit _parser;
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        #endregion

        #region 属性成员
        /// <summary>
        /// 解析分页标签名册
        /// </summary>
        public ISplit Parser
        {
            set { this._parser = value; }
            get { return this._parser; }
        }
        #endregion

        #region 抓取所有分页标签
        /// <summary>
        /// 抓取所有分页标签
        /// </summary>
        /// <param name="templateContent">模板内容</param>
        /// <param name="splitLabelList">分页标签列表</param>
        /// <returns></returns>
        public List<SplitLabel> GetSplitLabel(string templateContent, Dictionary<string, string> splitLabelList)
        {
            Regex reg;
            Regex regParam;
            MatchCollection matchCollection;
            MatchCollection collectParam;
            List<SplitLabel> lstSplitLabel;
            SplitLabel splitLabel;
            string splitID;
            string splitType;
            string param;

            lstSplitLabel = new List<SplitLabel>();
            splitLabel = new SplitLabel();

            splitID = string.Empty;
            splitType = string.Empty;
            reg = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<2>[^}]*)LableType\s*=\s*[""']SPLIT[""'](?<3>[^}]*)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase);

            matchCollection = reg.Matches(templateContent);

            foreach (Match matchItem in matchCollection)
            {
                splitLabel.Config = matchItem.Value;
                splitLabel.Name = matchItem.Groups[1].Value;
                param = matchItem.Groups[2].Value + matchItem.Groups[3].Value;
                collectParam = regParam.Matches(param);

                foreach (Match itemParam in collectParam)
                {
                    switch (itemParam.Groups[1].Value.Trim().ToLower())
                    {
                        case "splittype":
                            splitType = itemParam.Groups[2].Value;
                            break;
                        case "id":
                            splitID = itemParam.Groups[2].Value;
                            break;
                    }
                }

                foreach (string key in splitLabelList.Keys)
                {
                    if (Regex.IsMatch(key, @"\{HQB_" + splitLabel.Name + @"_[^\s}]+"))
                    {
                        splitLabel.Content = splitLabelList[key];
                    }

                }

                if (string.IsNullOrEmpty(splitLabel.Content))
                {
                    break;
                }
                else
                {
                    splitLabel.SplitType = splitType;
                    splitLabel.ID = splitID;
                    lstSplitLabel.Add(splitLabel);
                }
            }

            return lstSplitLabel;
        }
        #endregion

        #region 加载分页标签
        /// <summary>
        /// 加载分页标签
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> LoadSplitLabel(int siteID)
        {
            Dictionary<string, string> dicSplitLabel;
            DataTable dtSplitLabel;

            dicSplitLabel = new Dictionary<string, string>();
            dtSplitLabel = dal.GetLabelList(siteID, 2);

            if (dtSplitLabel != null && dtSplitLabel.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSplitLabel.Rows)
                {
                    try
                    {
                        dicSplitLabel.Add(dr["LableName"].ToString(), dr["LableContent"].ToString());
                    }
                    catch { }
                }
            }

            return dicSplitLabel;
        }
        #endregion
    }
    #endregion

    #region 分页标签解析接口
    public interface ISplit
    {
        string LabelContent { set; get; }
        string PageName { set; get; }
        string PageType { set; get; }
        string RootUrl { set; get; }
        int PageSize { set; get; }
        int TotalPage { set; get; }
        int Count { set; get; }
        string GetHtmlCode(int pageIndex);
        string GetHtmlCode(string labelContent, string pageName, int pageSize, int totalPage, int pageIndex, int count);
    }
    #endregion

    #region 简单分页方式
    /// <summary>
    /// 简单分页方式
    /// </summary>
    public class SimpleSplit : ISplit
    {
        #region  变量成员
        private string _labelContent;
        private string _pageName;
        private string _pageType;
        private string _rootUrl;
        private int _pageSize;
        private int _totalPage;
        private int _count;
        #endregion

        #region 属性
        /// <summary>
        /// 分页标签内容
        /// </summary>
        public string LabelContent
        {
            set { this._labelContent = value; }
            get { return this._labelContent; }
        }

        /// <summary>
        /// 列表页名称 如 Default
        /// </summary>
        public string PageName
        {
            set { this._pageName = value; }
            get { return this._pageName; }
        }

        /// <summary>
        /// 页面类型如html
        /// </summary>
        public string PageType
        {
            set { this._pageType = value; }
            get { return this._pageType; }
        }

        /// <summary>
        /// 保存当前分页面的目录路径
        /// </summary>
        public string RootUrl
        {
            set { this._rootUrl = value; }
            get { return this._rootUrl; }
        }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            set { this._pageSize = value; }
            get { return this._pageSize; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            set { this._totalPage = value; }
            get { return this._totalPage; }
        }

        /// <summary>
        /// 当前分页模式不用此参数
        /// </summary>
        public int Count
        {
            set { this._count = value; }
            get { return this._count; }
        }
        #endregion

        #region 获取分页HTML代码
        /// <summary>
        ///  获取分页HTML代码
        /// </summary>
        /// <param name="pageIndex">当前分页页码</param>
        /// <returns></returns>
        public string GetHtmlCode(int pageIndex)
        {
            string itemTemp;            // 分页链接及样式
            string currentPageTemp;     // 当前分页链接及样式
            string splitHtmlCode;       // 分页代码
            StringBuilder sbItemHtmlCode; // 所有分页面码链接
            string tempLink;            // 临时变量，保存循环中的当前分页链接
            string[] arrTemp;           // 临时变量 
            Regex reg;
            Match match;
            bool isChecked;             // 验证是否配置了分页基本属性

            itemTemp = string.Empty;
            tempLink = string.Empty;
            currentPageTemp = string.Empty;
            sbItemHtmlCode = new StringBuilder();
            reg = new Regex(@"\[HQB\.Loop\s*(count\s*=\s*[""]\d+[""])?\s*\](?<1>.+?)\[/HQB\.Loop\]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            match = reg.Match(this.LabelContent);
            splitHtmlCode = this.LabelContent;
            isChecked = !(string.IsNullOrEmpty(this.LabelContent) || string.IsNullOrEmpty(this.PageName) || (this.PageSize == 0) || (this.TotalPage == 0) || string.IsNullOrEmpty(this.PageType));

            if (isChecked)
            {
                splitHtmlCode = SetLabeslValue(splitHtmlCode, pageIndex);

                if (match.Success)      // 分页循环标签
                {
                    itemTemp = match.Groups[1].Value;
                    splitHtmlCode = splitHtmlCode.Replace(match.Value, "{[#SplitItem#]}");

                    if (itemTemp.Contains("[$$$$]"))    // 存在当前分页链接样式
                    {
                        arrTemp = itemTemp.Split(new string[] { "[$$$$]" }, StringSplitOptions.None);

                        if (arrTemp.Length > 1)
                        {
                            itemTemp = arrTemp[0];
                            currentPageTemp = arrTemp[1];
                        }
                        else
                        {
                            itemTemp = arrTemp[0];
                            currentPageTemp = arrTemp[0];
                        }
                    }

                    for (int k = 1; k <= this.TotalPage; k++)
                    {
                        if (k != 1)
                        {
                            if (k != pageIndex)
                            {
                                tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "_" + k.ToString() + "." + this.PageType);
                            }
                            else // 当前页链接
                            {
                                tempLink = currentPageTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "_" + k.ToString() + "." + this.PageType);
                            }
                        }
                        else  // 分页起始页（第一页）
                        {
                            if (k != pageIndex)
                            {
                                tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
                            }
                            else // 当前页链接
                            {
                                tempLink = currentPageTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
                            }
                        }

                        tempLink = tempLink.Replace("{$PageIndex}", k.ToString());
                        sbItemHtmlCode.Append(tempLink);
                    }

                    splitHtmlCode = splitHtmlCode.Replace("{[#SplitItem#]}", sbItemHtmlCode.ToString());
                }
            }

            return splitHtmlCode;
        }
        #endregion

        #region 获取分页HTML代码
        /// <summary>
        /// 获取分页HTML代码
        /// </summary>
        /// <param name="labelContent">分页标签内容</param>
        /// <param name="pageName">页面名称</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="totalPage">分页总数</param>
        /// <param name="pageIndex">当前分页面码</param>
        /// <param name="count">传入0</param>
        /// <returns></returns>
        public string GetHtmlCode(string labelContent, string pageName, int pageSize, int totalPage, int pageIndex, int count)
        {
            string splitHtmlCode;

            this.LabelContent = labelContent;
            this.PageName = pageName;
            this.PageSize = pageSize;
            this.TotalPage = totalPage;

            splitHtmlCode = GetHtmlCode(pageIndex);
            return splitHtmlCode;
        }
        #endregion

        #region 替换分页标签中的变量
        /// <summary>
        /// 替换分页标签中的变量
        /// </summary>
        /// <param name="content">要处理的内容</param>
        /// <param name="currentPageIndex">当前页分页面码</param>
        /// <returns></returns>
        private string SetLabeslValue(string content, int currentPageIndex)
        {
            string resultContent;

            resultContent = content;
            resultContent = resultContent.Replace("{$TotalPage}", this.TotalPage.ToString());                               // 总页数
            resultContent = resultContent.Replace("{$FirstPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);   // 首页地址
            resultContent = resultContent.Replace("{$CurrentPageIndex}", currentPageIndex.ToString());                      // 当前页页码
            resultContent = resultContent.Replace("{$RSTotal}", (this.TotalPage * this.PageSize).ToString());               // 记录总数
            resultContent = resultContent.Replace("{$PageSize}", this.PageSize.ToString());                                  // 分页大小


            if (currentPageIndex > 1)   // 上一页地址
            {
                if (currentPageIndex - 1 == 1)
                {
                    resultContent = resultContent.Replace("{$PrevPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
                }
                else
                {
                    resultContent = resultContent.Replace("{$PrevPageUrl}", this.RootUrl + this.PageName + "_" + (currentPageIndex - 1).ToString() + "." + this.PageType);
                }
            }
            else
            {
                resultContent = resultContent.Replace("{$PrevPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
            }

            if (this.TotalPage - 1 > currentPageIndex)  // 下一页地址
            {
                resultContent = resultContent.Replace("{$NextPageUrl}", this.RootUrl + this.PageName + "_" + (currentPageIndex + 1).ToString() + "." + this.PageType);
            }
            else
            {
                if (this.TotalPage != 1)
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", this.RootUrl + this.PageName + "_" + this.TotalPage.ToString() + "." + this.PageType);
                }
                else
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
                }
            }

            if (this.TotalPage != 1) // 最后一页地址
            {
                resultContent = resultContent.Replace("{$LastPageUrl}", this.RootUrl + this.PageName + "_" + this.TotalPage.ToString() + "." + this.PageType);
            }
            else
            {
                resultContent = resultContent.Replace("{$LastPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
            }

            return resultContent;
        }
        #endregion
    }
    #endregion

    #region 前几页 中间几页 未尾几页的间断显示方式
    /// <summary>
    /// 前几页 中间几页 未尾几页的显示方式
    /// </summary>
    public class IntermittentSplit : ISplit
    {
        #region  变量成员
        private string _labelContent;
        private string _pageName;
        private string _pageType;
        private string _rootUrl;
        private int _pageSize;
        private int _totalPage;
        private int _count;
        #endregion

        #region 属性
        /// <summary>
        /// 分页标签内容
        /// </summary>
        public string LabelContent
        {
            set { this._labelContent = value; }
            get { return this._labelContent; }
        }

        /// <summary>
        /// 列表页名称 如 Default
        /// </summary>
        public string PageName
        {
            set { this._pageName = value; }
            get { return this._pageName; }
        }

        /// <summary>
        /// 页面类型如html
        /// </summary>
        public string PageType
        {
            set { this._pageType = value; }
            get { return this._pageType; }
        }

        /// <summary>
        /// 保存当前分页面的目录路径
        /// </summary>
        public string RootUrl
        {
            set { this._rootUrl = value; }
            get { return this._rootUrl; }
        }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            set { this._pageSize = value; }
            get { return this._pageSize; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            set { this._totalPage = value; }
            get { return this._totalPage; }
        }

        /// <summary>
        /// 中间显示的分页数
        /// </summary>
        public int Count
        {
            set { this._count = value; }
            get { return this._count; }
        }
        #endregion

        #region 获取分页HTML代码
        /// <summary>
        ///  获取分页HTML代码
        /// </summary>
        /// <param name="pageIndex">当前分页页码</param>
        /// <returns></returns>
        public string GetHtmlCode(int pageIndex)
        {
            string itemTemp;            // 分页链接及样式
            string currentPageTemp;     // 当前分页链接及样式
            string splitHtmlCode;       // 分页代码
            StringBuilder sbItemHtmlCode; // 所有分页面码链接
            string tempLink;            // 临时变量，保存循环中的当前分页链接
            string[] arrTemp;           // 临时变量 
            Regex reg;
            Match match;
            bool isChecked;             // 验证是否配置了分页基本属性

            itemTemp = string.Empty;
            tempLink = string.Empty;
            currentPageTemp = string.Empty;

            reg = new Regex(@"\[HQB\.Loop\s*(count\s*=\s*[""]\d+[""])?\s*\](?<1>.+?)\[/HQB\.Loop\]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            match = reg.Match(this.LabelContent);
            splitHtmlCode = this.LabelContent;
            isChecked = !(string.IsNullOrEmpty(this.LabelContent) || string.IsNullOrEmpty(this.PageName) || (this.PageSize == 0) || (this.TotalPage == 0) || string.IsNullOrEmpty(this.PageType));

            if (this.Count == 0)
            {
                this.Count = 8;
            }

            if (isChecked)
            {
                splitHtmlCode = SetLabeslValue(splitHtmlCode, pageIndex);

                if (match.Success)      // 分页循环标签
                {
                    itemTemp = match.Groups[1].Value;
                    splitHtmlCode = splitHtmlCode.Replace(match.Value, "{[#SplitItem#]}");

                    if (itemTemp.Contains("[$$$$]"))    // 存在当前分页链接样式
                    {
                        arrTemp = itemTemp.Split(new string[] { "[$$$$]" }, StringSplitOptions.None);

                        if (arrTemp.Length > 1)
                        {
                            itemTemp = arrTemp[0];
                            currentPageTemp = arrTemp[1];
                        }
                        else
                        {
                            itemTemp = arrTemp[0];
                            currentPageTemp = arrTemp[0];
                        }
                    }

                    if (this.TotalPage > this.Count + 6)
                    {
                        if (pageIndex > this.Count / 2 + 2)
                        {
                            if (pageIndex > (this.TotalPage - (this.Count / 2) - 2))
                            {
                                sbItemHtmlCode = GetIntermittentHtmlCode(itemTemp, currentPageTemp, pageIndex, this.TotalPage - this.Count, this.TotalPage, true, false, this.TotalPage);
                            }
                            else
                            {
                                sbItemHtmlCode = GetIntermittentHtmlCode(itemTemp, currentPageTemp, pageIndex, pageIndex - this.Count / 2, pageIndex + this.Count / 2, true, true, this.TotalPage);
                            }
                        }
                        else
                        {
                            sbItemHtmlCode = GetIntermittentHtmlCode(itemTemp, currentPageTemp, pageIndex, 3, this.Count + 2, false, true, this.TotalPage);
                        }

                    }
                    else
                    {
                        sbItemHtmlCode = GetIntermittentHtmlCode(itemTemp, currentPageTemp, pageIndex, 3, this.TotalPage, false, false, this.TotalPage);
                    }


                    splitHtmlCode = splitHtmlCode.Replace("{[#SplitItem#]}", sbItemHtmlCode.ToString());
                }
            }

            splitHtmlCode = splitHtmlCode.Replace("{$CurrentPageIndex}", pageIndex.ToString());



            return splitHtmlCode;
        }
        #endregion

        #region 中间分页链接
        private StringBuilder GetIntermittentHtmlCode(string itemTemp, string currentPageTemp, int currentPageIndex, int startIndex, int endIndex, bool isStart, bool isEnd, int pageCount)
        {
            StringBuilder sbItemHtmlCode;
            string tempLink;

            sbItemHtmlCode = new StringBuilder();

            if (currentPageIndex != 1)
            {
                tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "." + this.PageType);  // 第一页
                tempLink = tempLink.Replace("{$PageIndex}", "1");
            }
            else  // 当前页为第一页
            {
                tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);  // 第一页
                tempLink = tempLink.Replace("{$CurrentPageIndex}", "1");
            }

            sbItemHtmlCode.Append(tempLink);

            if (pageCount > 1)
            {
                if (currentPageIndex != 2)
                {
                    tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "_2." + this.PageType);  // 第二页
                    tempLink = tempLink.Replace("{$PageIndex}", "2");
                }
                else  // 当前页为第二页
                {
                    tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", this.RootUrl + this.PageName + "_2." + this.PageType);  // 第二页
                    tempLink = tempLink.Replace("{$CurrentPageIndex}", "2");
                }

                sbItemHtmlCode.Append(tempLink);
            }

            if (isStart)
            {
                sbItemHtmlCode.Append("...");
            }

            for (int k = startIndex; k <= endIndex; k++)
            {
                if (k == currentPageIndex)
                {
                    tempLink = currentPageTemp.Replace("{$CurrentPageUrl}", this.RootUrl + this.PageName + "_" + k.ToString() + "." + this.PageType);
                    tempLink = tempLink.Replace("{$CurrentPageIndex}", k.ToString());
                }
                else
                {
                    tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "_" + k.ToString() + "." + this.PageType);
                    tempLink = tempLink.Replace("{$PageIndex}", k.ToString());
                }

                sbItemHtmlCode.Append(tempLink);
            }

            if (isEnd)
            {
                sbItemHtmlCode.Append("...");
                tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "_" + (this.TotalPage - 1).ToString() + "." + this.PageType);  // 尾二页
                tempLink = tempLink.Replace("{$PageIndex}", (this.TotalPage - 1).ToString());
                sbItemHtmlCode.Append(tempLink);
                tempLink = itemTemp.Replace("{$PageUrl}", this.RootUrl + this.PageName + "_" + this.TotalPage.ToString() + "." + this.PageType);  // 尾页
                tempLink = tempLink.Replace("{$PageIndex}", this.TotalPage.ToString());
                sbItemHtmlCode.Append(tempLink);
            }

            return sbItemHtmlCode;
        }
        #endregion

        #region 获取分页HTML代码
        /// <summary>
        /// 获取分页HTML代码
        /// </summary>
        /// <param name="labelContent">分页标签内容</param>
        /// <param name="pageName">页面名称</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="totalPage">分页总数</param>
        /// <param name="pageIndex">当前分页面码</param>
        /// <param name="count">中间显示的分页数</param>
        /// <returns></returns>
        public string GetHtmlCode(string labelContent, string pageName, int pageSize, int totalPage, int pageIndex, int count)
        {
            string splitHtmlCode;

            this.LabelContent = labelContent;
            this.PageName = pageName;
            this.PageSize = pageSize;
            this.TotalPage = totalPage;

            splitHtmlCode = GetHtmlCode(pageIndex);
            return splitHtmlCode;
        }
        #endregion

        #region 替换分页标签中的变量
        /// <summary>
        /// 替换分页标签中的变量
        /// </summary>
        /// <param name="content">要处理的内容</param>
        /// <param name="currentPageIndex">当前页分页面码</param>
        /// <returns></returns>
        private string SetLabeslValue(string content, int currentPageIndex)
        {
            string resultContent;

            resultContent = content;
            resultContent = resultContent.Replace("{$TotalPage}", this.TotalPage.ToString());                               // 总页数
            resultContent = resultContent.Replace("{$FirstPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);   // 首页地址
            resultContent = resultContent.Replace("{$RSTotal}", (this.TotalPage * this.PageSize).ToString());               // 记录总数
            resultContent = resultContent.Replace("{$PageSize}", this.PageSize.ToString());                                  // 分页大小

            if (currentPageIndex > 2)   // 上一页地址
            {
                resultContent = resultContent.Replace("{$PrevPageUrl}", this.RootUrl + this.PageName + "_" + (currentPageIndex - 1).ToString() + "." + this.PageType);
            }
            else
            {
                resultContent = resultContent.Replace("{$PrevPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
            }

            if (this.TotalPage - 1 > currentPageIndex)  // 下一页地址
            {
                resultContent = resultContent.Replace("{$NextPageUrl}", this.RootUrl + this.PageName + "_" + (currentPageIndex + 1).ToString() + "." + this.PageType);
            }
            else
            {
                if (this.TotalPage != 1)
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", this.RootUrl + this.PageName + "_" + this.TotalPage.ToString() + "." + this.PageType);
                }
                else
                {
                    resultContent = resultContent.Replace("{$NextPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
                }
            }

            if (this.TotalPage != 1) // 最后一页地址
            {
                resultContent = resultContent.Replace("{$LastPageUrl}", this.RootUrl + this.PageName + "_" + this.TotalPage.ToString() + "." + this.PageType);
            }
            else
            {
                resultContent = resultContent.Replace("{$LastPageUrl}", this.RootUrl + this.PageName + "." + this.PageType);
            }

            return resultContent;
        }
        #endregion
    }
    #endregion
}
