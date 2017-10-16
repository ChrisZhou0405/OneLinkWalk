
﻿#region 程序集引用
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

using KingTop.IDAL.Content;
using KingTop.Common;
#endregion

#region 版权注释
/*----------------------------------------------------------------------------------------
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-03-23
// 功能描述：模型生成列表操作 -- 搜索操作

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public partial class ControlManageList : IControlManageList
    {

        #region 搜索 -- 外表的参数类型
        /// <summary>
        ///  搜索 -- 外表的参数类型 解析
        /// </summary>
        /// <param name="sbSearch"></param>
        /// <param name="paramValue">外表参数</param>
        /// <param name="keyValue">搜索值</param>
        /// <param name="arrParam"></param>
        /// <param name="arrField"></param>
        public void SearchForignTable(ref StringBuilder sbSearch, string paramValue, string keyValue,string[] arrParam,string[] arrField)
        {
            StringBuilder sqlWhere; // 外表搜索查询条件

            sqlWhere = new StringBuilder();

            // 没有引用外表字段
            if (arrField.Length > 0)
            {

                if (sbSearch.Length > 0)
                {
                    sbSearch.Append(" and ");
                }

                sbSearch.Append("(");

                switch (arrParam[0])
                {
                    case "4":   // 单选
                        //  数字类不加 ''
                        if (IsNumber(keyValue))
                        {
                            sqlWhere.Append(" " + arrParam[2] + " = " + keyValue);
                        }
                        else //字符串类 加 ''
                        {
                            sqlWhere.Append(" " + arrParam[2] + " = '" + keyValue + "' ");
                        }
                        break;
                    case "5":   // 多选
                        string[] itemValue;     // 选项值
                        string thisSqlWhere;    // 选项对应的SQL条件

                        itemValue = keyValue.Split(new char[] { ',' });
                        thisSqlWhere = null;
                        // 遍历每个选项值生成条件
                        foreach (string item in itemValue)
                        {
                            if(!string.IsNullOrEmpty(item))
                            {
                                if (!string.IsNullOrEmpty(thisSqlWhere))
                                {
                                    thisSqlWhere = thisSqlWhere + "and";
                                }

                                thisSqlWhere = thisSqlWhere + " Patindex('%" + item + "%'," + arrParam[2] + ") > 0 ";
                            }
                        }

                        sqlWhere.Append(thisSqlWhere);
                        break;
                    default:    // 文本框
                        sbSearch.Append(" " + arrParam[2] + " ");   // 当前表搜索字段
                        sbSearch.Append(" in(select ");
                        sbSearch.Append(arrParam[4]);               // 外键(引用表中字段)，当前表搜索字段
                        sbSearch.Append(" from ");
                        sbSearch.Append("["+arrParam[1]+"]");               // 引用的外表
                        sbSearch.Append(" where ");
                        // 外表查询条件
                        foreach (string fieldName in arrField)
                        {
                            if (sqlWhere.Length > 0)
                            {
                                sqlWhere.Append(" or ");
                            }

                            sqlWhere.Append(" " + fieldName + " like '%" + keyValue + "%' ");
                        }
                        sqlWhere.Append(")");
                        break;
                }

                sbSearch.Append(sqlWhere);
                sbSearch.Append(") ");
            }
        }
        #endregion

        #region 搜索 -- 分范围 解析
        /// <summary>
        /// 搜索 -- 分范围 解析
        /// 值范围是从 keyValue  至 paramValue
        /// </summary>
        /// <param name="sbSearch"></param>
        /// <param name="fieldName">字段名</param>
        /// <param name="keyValue">范围开始值</param>
        /// <param name="paramValue">范围结束值</param>
        public void SearchRange(ref StringBuilder sbSearch, string fieldName, string keyValue, string paramValue)
        {
            string patternStr;  // 匹配日期
            string patternNum;  // 匹配数字

            patternStr = @"^(\d{2}-\d{2}-\d{2})|(\d{4}-\d{2}-\d{2})|(\d{2}-\d{2}-\d{2}\s*\d{2}:\d{2}:\d{2})|(\d{4}-\d{2}-\d{2}\s*\d{2}:\d{2}:\d{2}|(\d{2}:\d{2}:\d{2}))$";
            patternNum = @"\d*";
            paramValue = paramValue.Trim();

            if (!string.IsNullOrEmpty(keyValue) || !string.IsNullOrEmpty(paramValue))  // 输入了搜索值            {
                // 日期
                if (Regex.IsMatch(keyValue, patternStr) || Regex.IsMatch(paramValue, patternStr))
                {
                    if (sbSearch.Length > 0)
                    {
                        sbSearch.Append(" and  ");
                    }

                    sbSearch.Append("(");
                    // 开始日期
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        sbSearch.Append(fieldName + " > cast('" + keyValue + "' as datetime)");
                    }
                    // 结束日期
                    if (!string.IsNullOrEmpty(paramValue))
                    {
                        if (!string.IsNullOrEmpty(keyValue))
                        {
                            sbSearch.Append(" and ");
                            sbSearch.Append(fieldName + " < cast('" + paramValue + "' as datetime)");
                        }
                        else
                        {
                            sbSearch.Append(fieldName + " < cast('" + paramValue + "' as datetime)");
                        }
                    }
                    sbSearch.Append(")");
                }
                else if(Regex.IsMatch(keyValue,patternNum) || Regex.IsMatch(paramValue,patternNum)) // 数字、货币
                {
                    if (sbSearch.Length > 0)
                    {
                        sbSearch.Append(" and  ");
                    }

                    sbSearch.Append("(");
                    // 起始数字
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        sbSearch.Append(fieldName + " > " + keyValue);
                    }
                    // 结束数字
                    if (!string.IsNullOrEmpty(paramValue))
                    {
                        if (!string.IsNullOrEmpty(keyValue))
                        {
                            sbSearch.Append(" and ");
                            sbSearch.Append(fieldName + " < " + paramValue);
                        }
                        else
                        {
                            sbSearch.Append(fieldName + " < " + paramValue);
                        }
                    }
                    sbSearch.Append(")");
                }
            }
        }
        #endregion

        #region 搜索 -- 列表加文本框
        /// <summary>
        /// 搜索 -- 列表加文本框 解析
        /// 列表值指定操作文本框值的操作类型
        /// </summary>
        /// <param name="sbSearch"></param>
        /// <param name="fieldName">要搜索的字段名称</param>
        /// <param name="paramValue">操作符</param>
        /// <param name="keyValue">搜索值</param>
        public void SearchCompare(ref StringBuilder sbSearch, string fieldName, string paramValue, string keyValue)
        {
            string patternStr;  // 匹配日期
            string patternNum;  // 匹配数字

            patternStr = @"((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9]))|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29))|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29) ))\s((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";
            patternNum = @"\d*";

            if (sbSearch.Length > 0)
            {
                sbSearch.Append(" and  ");
            }

            // 日期
            if (Regex.IsMatch(keyValue, patternStr) || Regex.IsMatch(paramValue, patternStr))
            {
                sbSearch.Append("(");
                sbSearch.Append(fieldName + paramValue + "cast('" + keyValue + "' as datetime)");
                sbSearch.Append(")");
            }
            else if (Regex.IsMatch(keyValue, patternNum) || Regex.IsMatch(paramValue, patternNum)) // 数字、货币
            {
                sbSearch.Append("(");
                sbSearch.Append(fieldName + paramValue + keyValue);
                sbSearch.Append(")");
            }
        }
        #endregion

        #region 搜索 -- 基本类型手工输入值
        /// <summary>
        ///  搜索 -- 基本类型手工输入值
        /// </summary>
        /// <param name="sbSearch"></param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="keyValue">字段值</param>
        /// <param name="type">基本字段类型</param>
        public void SearchBasic(ref StringBuilder sbSearch, string fieldName, string keyValue, string type)
        {
            switch (type)
            {
                case "4":   // 单选按钮
                case "6":   // 单选列表
                    if (sbSearch.Length > 0)
                    {
                        sbSearch.Append(" and ");
                    }
                    sbSearch.Append("(");
                    sbSearch.Append(fieldName);
                    sbSearch.Append("=");
                    // 值为数字类型
                    if (IsNumber(keyValue))
                    {
                        sbSearch.Append(keyValue);
                    }
                    else // 值为非数字类型
                    {
                        sbSearch.Append("'" + keyValue + "'");
                    }
                    sbSearch.Append(")");
                    break;
                case "5":   // 复选框
                case "7":   // 多选下拉列表
                    if (sbSearch.Length > 0)
                    {
                        sbSearch.Append(" and ");
                    }
                    sbSearch.Append("(");
                    sbSearch.Append(fieldName);
                    sbSearch.Append(" in(");
                    // 值为数字类型
                    if (IsNumber(keyValue.Split(new char[] { ',' })[0]))
                    {
                        sbSearch.Append(keyValue);
                    }
                    else // 值为非数字类型
                    {
                        keyValue = keyValue.Replace(",", "','");
                        keyValue = "'" + keyValue + "'";
                        sbSearch.Append(keyValue);
                    }
                    sbSearch.Append(")");
                    sbSearch.Append(")");
                    break;
                default:
                    if (sbSearch.Length > 0)
                    {
                        sbSearch.Append(" and ");
                    }
                    sbSearch.Append("(");
                    sbSearch.Append(fieldName);
                    sbSearch.Append(" in(");
                    sbSearch.Append(keyValue);
                    sbSearch.Append(")");
                    sbSearch.Append(")");
                    break;
            }
        }
        #endregion
    }
}

