#region 引用程序集using System;
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
// 作者：吴岸标// 创建日期：2010-03-18
// 功能描述：模型列表解析类 -- 链接解析
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 链接解析
        private string ListLink(bool isButtonList)
        {
            string[] linkType;              // linkType[0] 字段型链接字符串 linkType[1]  自定义链接字符串
            string[] selfLinkList;          // 自定义链接            Queue<Link> linkList;           // 字段型链接            int count;                      // 字段型链接个数            StringBuilder linkHtml;
            string displayTemplate;         // 链接显示模板

            linkHtml = new StringBuilder();

            if (isButtonList)   // 功能按钮
            {
                displayTemplate = "<input type=\"submit\" value=\"{#Text#}\"  runat=\"server\" onclick=\"return setAction('{#Url#}');\" />";
                linkType = this.hsModel["ListButton"].ToString().Split(new char[] { '$' });
            }
            else  // 页面链接
            {
                displayTemplate = "<a href=\"{#Url#}\">{#Text#}</a>";
                linkType = this.hsModel["ListLink"].ToString().Split(new char[] { '$' });
            }

                       linkList = GetLink(linkType[0], isButtonList);  // 加载系统预定义链接
            count = linkList.Count;

            for (int i = 0; i < count; i++) //解析系统预定义链接
            {
                Link lnk = linkList.Dequeue();

                if (lnk.Value.ToLower().Contains("{a}"))    // 添加，转换至添加地址页面
                {
                    linkHtml.Append(lnk.Value.Replace("{a}", PageName + "edit.aspx?action=add"));
                }
                else
                {
                    if (isButtonList)
                    {
                        linkHtml.Append(lnk.Value);
                    }
                    else
                    {
                        linkHtml.Append(displayTemplate.Replace("{#Text#}", lnk.Text).Replace("{#Url#}", lnk.Value));
                    }
                }
            }

                       if (linkType[0].Trim() != "none")    // 加载自定义链接
            {
                selfLinkList = linkType[1].Split(new char[] { ',' });

                foreach (string linkItem in selfLinkList)
                {
                    string[] selfLink = linkItem.Split(new char[] { '|' });

                    if (selfLink.Length > 1 && selfLink[0].Trim() != "")
                    {
                        
                        if (selfLink[1].ToLower().Contains("action={a}"))   // 添加，转换至添加地址页面
                        {
                            StringBuilder sbAddUrl;

                            sbAddUrl = new StringBuilder();

                            sbAddUrl.Append(this.hsModel["TableName"].ToString().Replace("K_U_", "").Replace("K_F_",""));
                            sbAddUrl.Append("edit");
                            sbAddUrl.Append(".aspx?action=add" + this.keepUrlParam);
                            linkHtml.Append(displayTemplate.Replace("{#Text#}", selfLink[0]).Replace("{#Url#}", sbAddUrl.ToString()));
                        }
                        else
                        {
                            linkHtml.Append(displayTemplate.Replace("{#Text#}", selfLink[0]).Replace("{#Url#}", selfLink[1]));
                        }
                    }
                }
            }

            return linkHtml.ToString();
        }
        #endregion

        #region 链接结构类型
        private struct Link
        {
            private string _text;
            private string _value;

            /// <summary>
            /// 链接文本
            /// </summary>
            public string Text
            {
                set { this._text = value; }
                get { return this._text; }
            }

            /// <summary>
            /// 链接
            /// </summary>
            public string Value
            {
                set { this._value = value; }
                get { return this._value; }
            }

        }
        #endregion

        #region 通过以逗号隔开的ID字符串读取链接配置        private Queue<Link> GetLink(string strID, bool isButtonList)
        {
            Queue<Link> linkQueue;
            string[] arrID;                     // 保存配置文件中节点ID
            string configPath;                  // 链接配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;     // 链接/按钮配置根节点指针            XPathNodeIterator currentNav;       // 临时指针

            linkQueue = new Queue<Link>();

            arrID = strID.Split(new char[] { ',' });
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "ListPath");

            if (isButtonList)
            {
                xpath = "/config/listbutton";
            }
            else
            {
                xpath = "/config/listlink";
            }

            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();

            foreach (string strItem in arrID)
            {
                if (string.IsNullOrEmpty(strItem))
                {
                    continue;
                }

                currentNav = navNodeXPath.Current.Select("link[@id=" + strItem + "]");

                if (currentNav != null && currentNav.Count > 0)
                {
                    try
                    {
                        Link lnk = new Link();

                        currentNav.MoveNext();
                        lnk.Text = currentNav.Current.GetAttribute("text", "");
                        lnk.Value = currentNav.Current.SelectSingleNode("value").InnerXml;
                        lnk.Value = lnk.Value.Replace("{#EditUrlParam#}", this.keepUrlParam);
                        linkQueue.Enqueue(lnk);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return linkQueue;
        }
        #endregion
    }
}
