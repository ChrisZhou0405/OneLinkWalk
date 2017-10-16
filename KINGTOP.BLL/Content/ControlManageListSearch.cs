#region 程序集引用using System;
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
// 作者：吴岸标// 创建日期：2010-03-26
// 功能描述：处理解析后的模型列表 -- 搜索功能
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.BLL.Content
{
    public partial class ControlManageList
    {
        #region 绑定自定义字段值        /// <summary>
        /// 绑定自定义字段值        /// </summary>
        /// <param name="tableName">引用表名</param>
        /// <param name="txtColumn">显示文本引用列</param>
        /// <param name="valueColumn">值引用列</param>
        /// <param name="sqlWhere">引用条件</param>
        /// <returns></returns>
        public Hashtable InitSearchField(string tableName, string txtColumn, string valueColumn, string sqlWhere)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return new Hashtable();
            }

            return dal.GetHashTable(tableName, txtColumn, valueColumn, sqlWhere);
        }
        #endregion

        #region 搜索处理
        // Request.Keys集合有几种来源        // 1、字段名，如普通的文本框        // 2、用于保存参数的控件名 hdn + 字段名        // 3、 搜索界面类型 ：分范围 第一个文本框为字段名，第二个为字段名加"0"；列表加文本框  列表字段名加"Compare" 文本框为字段名        // 4、 单选、复选  枚举型值，是手工输入而非来自于数据库 字段名 + Type 
        /// <summary>
        /// 搜索处理
        /// </summary>
        /// <returns></returns>
        public string Search()
        {
            StringBuilder sbSearch;     // 保存解析后的搜索条件
            List<string> listKey;       // 键集合            string checkKey;            // 用于进行检测键对            string joinTag;             // 将键数组联接成字符串的连接字符串
            string paramKey;            // 参数键变量            string keyValue;            // 键值            string paramValue;          // 参数值
            sbSearch = new StringBuilder();

            if (HttpContext.Current.Request.Form["action"] == null)   // 搜索按钮提交
            {
                joinTag = "$,";
                checkKey = string.Join(joinTag, HttpContext.Current.Request.Form.AllKeys) + joinTag;
                listKey = KeyFilter();

                foreach (string key in listKey)
                {
                    keyValue = HttpContext.Current.Request.Form[key]; // 键值
                    keyValue = Utils.cutBadStr(keyValue);             // 过滤非法字符

                    if (string.IsNullOrEmpty(keyValue))               // 搜索值为空时,忽略
                    {
                        paramKey = key + "{0}";                       // 分范围 为数字、货币、日期类型

                        if (checkKey.Contains(paramKey + joinTag))
                        {
                            paramValue = HttpContext.Current.Request[paramKey];  // 范围的最大值

                            if (!string.IsNullOrEmpty(paramValue))
                            {
                                dal.SearchRange(ref sbSearch, key, "", paramValue);
                            }
                        }

                        continue;
                    }
                    else
                    {
                        paramKey = "{hdn}" + key;   // 引用外表的参数

                        if (checkKey.Contains(paramKey + joinTag))
                        {
                            string[] arrParam;      // 引用外表时所需参数 [0] 控件类型 [1] 引用表名 [2] 当前表搜索字段 [3] 引用字段(文本引用字段) [4] 外键(值引用字段)
                            string[] arrField;      // 引用的外表字段
                            paramValue = HttpContext.Current.Request[paramKey];
                            arrParam = paramValue.Split(new char[] { ',' });

                            if (arrParam.Length > 4)
                            {
                                arrParam[2] = this.tableName + "." + arrParam[2];
                                arrField = arrParam[3].Split(new char[] { '|' });
                                dal.SearchForignTable(ref sbSearch, paramValue, keyValue, arrParam, arrField);
                            }

                            continue;
                        }

                        paramKey = key + "{0}";     // 分范围 为数字、货币、日期类型

                        if (checkKey.Contains(paramKey + joinTag))
                        {
                            paramValue = HttpContext.Current.Request[paramKey];     // 范围的最大值
                            dal.SearchRange(ref sbSearch, key, keyValue, paramValue);
                            continue;
                        }

                        paramKey = key + "{Compare}";   // 列表加文本框的形式,列表指定操作类型

                        if (checkKey.Contains(paramKey + joinTag))
                        {
                            paramValue = HttpContext.Current.Request[paramKey];      // 操作符
                            dal.SearchCompare(ref sbSearch, key, paramValue, keyValue);
                            continue;
                        }

                        paramKey = key + "{Type}";      // 手工输入的枚举型，如单选

                        if (checkKey.Contains(paramKey + joinTag))
                        {
                            paramValue = HttpContext.Current.Request[paramKey];
                            dal.SearchBasic(ref sbSearch, this.tableName + "." + key, keyValue, paramValue);
                            continue;
                        }

                        if (sbSearch.Length > 0)        // 缺省无参数的键
                        {
                            sbSearch.Append(" and ");
                        }

                        sbSearch.Append(" " + key + " like '%" + keyValue + "%'");
                        sbSearch.Append(" ");
                    }
                }
            }

            return sbSearch.ToString();
        }
        #endregion

        #region 对键集合进行筛选,将参数键过滤掉        private List<string> KeyFilter()
        {
            List<string> listKey ;          // 过滤后的键集
            string[] arrKey;            // 原始的键集            string checkKey;            // 用于进行检测键对            string joinTag;             // 将键数组联接成字符串的连接字符串

            listKey = new List<string>();
            joinTag = "$,";
            arrKey = HttpContext.Current.Request.Form.AllKeys;
            checkKey = string.Join(joinTag, arrKey) + joinTag;

            foreach (string key in arrKey)
            {
                if (checkKey.Contains("{hdn}" + key + joinTag) || checkKey.Contains(key + "{0}" + joinTag) || checkKey.Contains(key + "{Compare}" + joinTag) || checkKey.Contains(key + "{Type}" + joinTag)) // 有参数的键
                {
                    listKey.Add(key);
                }                 else if (!(key.Contains("{hdn}") || key.Contains("{0}") || key.Contains("{Compare}") || key.Contains("{Type}")))    // 无参数的键
                {
                    listKey.Add(key);
                }
            }

            return listKey;
        }
        #endregion
    }
}
