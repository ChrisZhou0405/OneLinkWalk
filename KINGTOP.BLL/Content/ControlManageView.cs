#region 程序集引用
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL.Content;

using KingTop.Common;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-04-27
// 功能描述：处理解析后的模型浏览页

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public class ControlManageView
    {
        #region 变量成员
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Content.IControlManageView dal = (IDAL.Content.IControlManageView)Assembly.Load(path).CreateInstance(path + ".Content.ControlManageView");
        /// <summary>
        /// 表名
        /// </summary>
        private string tableName;
        /// <summary>
        /// 所属模型
        /// </summary>
        private string modelId;
        /// <summary>
        /// 各字段更新值对,以字段名称为键名
        /// </summary>
        private Hashtable hsEditField;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="mID">模型ID</param>
        public ControlManageView(string tbName, string mID)
        { 
            this.modelId = mID;
            this.tableName = tbName;
            this.hsEditField = new Hashtable();
        }
        #endregion

        #region 加载字段值
        /// <summary>
        /// 加载字段值
        /// </summary>
        /// <returns></returns>
        public Hashtable FillField()
        {
            Hashtable hsInitField;      // 各字段初始值,以字段名称为键名
            string id;             // 记录ID

            id = HttpContext.Current.Request.QueryString["ID"];
            if (!string.IsNullOrEmpty(id))
            {
                hsInitField = dal.GetHashTableByID(this.tableName, id);
                LoadSpecial(id, ref hsInitField);
            }
            else
            {
                hsInitField = null;
            }

            return hsInitField;
        }
        #endregion

        #region 加载所属专题栏目
        private void LoadSpecial(string specialInfoID, ref Hashtable hsInitField)
        {
            DataTable dt;
            StringBuilder specialMenuTitle;        // 记录所附加的栏目（名称)

            specialMenuTitle = new StringBuilder();

            if (!string.IsNullOrEmpty(specialInfoID))
            {

                dt = dal.LoadSpecial(specialInfoID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        specialMenuTitle.Append(dr["SpecialName"]);
                        specialMenuTitle.Append(">>");
                        specialMenuTitle.Append(dr["SpecialMenuName"]);
                        specialMenuTitle.Append(",");
                    }
                    specialMenuTitle.Remove(specialMenuTitle.Length - 1, 1);
                }
            }

            hsInitField.Add("SpecialID", specialMenuTitle);
        }
        #endregion

        #region  解析模型基本字段中 单选、多选、列表的文本输入方式
        /// <summary>
        /// 解析模型基本字段中 单选、多选、列表的文本输入方式
        /// </summary>
        /// <param name="id">要解析的基本字段ID</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public string ParseFieldValueToText(string id, object value)
        {
            string text;
            string cacheName;

            // 保存当前字段值的Cache名称
            cacheName = "HQB_CONTENT_MODEL_MANAGELIST" + id;
            text = null;

            // 当前字段值为空时忽略
            if (value != null && value.ToString().Trim() != "")
            {
                if (HttpContext.Current.Cache[cacheName] == null)
                {
                    string optionsValue;
                    string[] optionItem;
                    string[] itemValue;
                    Hashtable hsItem;

                    hsItem = new Hashtable();
                    optionsValue = dal.GetFieldValue("K_ModelField", "OptionsValue", id);

                    // 字段配置表中没有设置选项数据，当前字段值为非法数据
                    if (!string.IsNullOrEmpty(optionsValue))
                    {
                        optionItem = optionsValue.Split(new char[] { ',' });

                        foreach (string item in optionItem)
                        {
                            itemValue = item.Split(new char[] { '|' });

                            if (itemValue.Length > 1)
                            {
                                hsItem.Add(itemValue[1], itemValue[0]);
                            }
                        }

                        HttpContext.Current.Cache[cacheName] = hsItem;

                    }
                }

                try
                {
                    if (value.ToString().Contains(","))
                    {
                        string[] arrValue;

                        arrValue = value.ToString().Split(new char[] { ',' });
                        foreach (string item in arrValue)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                text = text + ((Hashtable)HttpContext.Current.Cache[cacheName])[item].ToString() + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(text))
                        {
                            text = text.Remove(text.Length - 1, 1);
                        }
                    }
                    else
                    {
                        text = ((Hashtable)HttpContext.Current.Cache[cacheName])[value.ToString()].ToString();
                    }
                }
                catch
                {
                    text = "";
                }
            }
            return text;
        }
        #endregion
    }
}
