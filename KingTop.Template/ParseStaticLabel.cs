#region 引用程序集
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.IO;

using KingTop.Template.ParamType;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-02
// 功能描述：内容发布 -- 静态标签  静态标签分两种类型 一种就是直接
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    public class ParseStaticLabel
    {
        #region 变量成员
        /// <summary>
        /// 站点ID
        /// </summary>
        private int siteID;
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        private Dictionary<string, string> _dicStaticLabel;
        #endregion

        #region 构造函数
        /// <summary>
        /// 处理静态标签
        /// </summary>
        /// <param name="webSiteID">站点ID</param>
        public ParseStaticLabel(int webSiteID)
        {
            this.siteID = webSiteID;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 静态标签
        /// </summary>
        public Dictionary<string, string> StaticLabel
        {
            get
            {
                if (this._dicStaticLabel == null)
                {
                    this._dicStaticLabel = LoadStaticLabel();
                }

                return this._dicStaticLabel;
            }
        }
        #endregion

        #region  解析静态标签
        /// <summary>
        /// 解析静态标签
        /// </summary>
        /// <param name="labelName">标签名称</param>
        /// <returns></returns>
        public string ParseStatic(string labelName, string siteUrl,string uploadImg,DataTable menuList,string fileType)
        {
            string resultContent;
            List<SysLabelMenu> lstSysLabelMenu;          // 栏目类型系统标签
            ParseSystemLabel systemLabelParser;          // 系统标签解析器
            string parsedContent;                        // 标签解析返回内容

            systemLabelParser = new ParseSystemLabel(siteUrl);
            systemLabelParser.SiteID = this.siteID;
            systemLabelParser.UploadImgUrl = uploadImg;
            systemLabelParser.MenuList = menuList;
            systemLabelParser.FileType = fileType;


            try
            {
                resultContent = this.StaticLabel[labelName];
                lstSysLabelMenu = systemLabelParser.GetSysLabelMenu(resultContent, string.Empty);     // 抓取栏目类型系统标签

                foreach (SysLabelMenu label in lstSysLabelMenu)                // 解析模板中栏目类型的系统标签
                {
                    parsedContent = systemLabelParser.ParseMenu(label, siteUrl);
                    resultContent = resultContent.Replace(label.LabelName, parsedContent);
                }
            }
            catch
            {
                resultContent = string.Empty;
            }

            return resultContent;
        }
        #endregion

        #region 抓取所有静态标签
        /// <summary>
        /// 抓取所有静态标签
        /// </summary>
        /// <param name="templateContent">模板内容</param>
        /// <returns></returns>
        public List<string> GetStaticLabel(string templateContent)
        {
            List<string> lstStaticLabel;        // 模板内容中的静态标签
            Regex reg;
            MatchCollection matchCollection;

            reg = new Regex(@"\{HQB_L\d+_[^\s}]+\s*LableType\s*=[""']STATIC[""']\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            lstStaticLabel = new List<string>();

            matchCollection = reg.Matches(templateContent);

            foreach (Match matchItem in matchCollection)
            {
                lstStaticLabel.Add(matchItem.Value);
            }

            return lstStaticLabel;
        }
        #endregion

        #region 解析包含标签
        /// <summary>
        /// 解析包含标签
        /// </summary>
        /// <param name="label">包含标签</param>
        /// <param name="rootTemplate">当前站的模板方案根目录</param>
        /// <returns></returns>
        public string ParseInclude(IncludeLabel label, string rootTemplate)
        {
            string resultContent;       // 解析后的内容
            string filePath;

            filePath = rootTemplate + label.FilePath;
            resultContent = string.Empty;

            if (File.Exists(filePath))
            {
                resultContent = File.ReadAllText(filePath);
            }

            return resultContent;
        }
        #endregion

        #region 抓取所有包含标签
        /// <summary>
        /// 抓取所有包含标签
        /// </summary>
        /// <param name="templateContent">模板内容</param>
        /// <returns> 模板内容中的包含标签 key 标签内容 value   包含的文件路径</returns>
        public List<IncludeLabel> GetIncludeLabel(string templateContent)
        {
            List<IncludeLabel> lstIncludeLabel; // 模板内容中的包含标签 key 标签内容 value   包含的文件路径
            Regex reg;
            MatchCollection matchCollection;
            IncludeLabel includeLabel;

            includeLabel = new IncludeLabel();
            reg = new Regex(@"\{Include\s+FilePath\s*=\s*[""'](?<1>[^""']+)[""']\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            lstIncludeLabel = new List<IncludeLabel>();

            matchCollection = reg.Matches(templateContent);

            foreach (Match matchItem in matchCollection)
            {
                includeLabel.Content = matchItem.Value;
                includeLabel.FilePath = matchItem.Groups[1].Value;
                lstIncludeLabel.Add(includeLabel);
            }

            return lstIncludeLabel;
        }
        #endregion

        #region 加载静态标签
        /// <summary>
        /// 加载静态标签
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> LoadStaticLabel()
        {
            DataTable dtLabel;
            Dictionary<string, string> dicStaticLabel;

            dicStaticLabel = new Dictionary<string, string>();
            dtLabel = dal.GetLabelList(this.siteID, 0);

            if (dtLabel != null)
            {
                foreach (DataRow dr in dtLabel.Rows)
                {
                    dicStaticLabel.Add(dr["LableName"].ToString(), dr["LableContent"].ToString());
                }
            }

            return dicStaticLabel;
        }
        #endregion
    }
}
