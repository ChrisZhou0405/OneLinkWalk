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
// 作者：吴岸标 周武
// 创建日期：2010-05-28
// 功能描述：解析后的模型编辑页操作调用接口

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ControlManageEdit
    {
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="isAdd">是否是添加操作</param>
        /// <param name="id">修改时记录ID</param>
        /// <returns>影响行数</returns>
        public int Edit(bool isAdd, string id, Dictionary<string, string> dcField)
        {
            string strID = "";
            return Edit(isAdd, id, dcField, ref strID); 
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="isAdd">是否是添加操作</param>
        /// <param name="id">修改时记录ID</param>
        /// <returns>影响行数</returns>
        public int Edit(bool isAdd, string id, Dictionary<string, string> dcField, ref string strID)
        {
            int returnValue;                                  // 操作影响行数
            Hashtable hsFieldParam;                           // 字段缺省值

            hsFieldParam = ParseFieldParam();

            if (hsFieldParam == null)                         // 字段参数有误
            {
                returnValue = 0;
            }
            else
            {
                foreach (string fieldName in hsFieldParam.Keys)      // 遍历获取字段值
                {
                    string fieldValue = null;                               // 当前字段值
                    if (dcField.ContainsKey(fieldName))
                    {
                        fieldValue = dcField[fieldName];
                    }
                    else
                    {
                        fieldValue = System.Web.HttpContext.Current.Request.Form[fieldName];
                    }
                    if (fieldValue != null)
                    {
                        hsEditField.Add(fieldName, fieldValue);
                    }
                }
                hsEditField.Add("NodeCode", this.nodeCode);
                hsEditField.Add("SiteID", this.siteID);

                if (isAdd)                 // 添加
                {
                    string[] tableID;

                    tableID = Public.GetTableID("0", this.tableName);
                    strID = tableID[0];
                    hsEditField.Add("ReferenceID", tableID[0]);

                    returnValue = Utils.ParseInt(dal.Add(this.tableName, tableID[0], Utils.ParseInt(tableID[1], 0), hsEditField,null),0);

                    if (this._isEnableNode && returnValue > 0)   // 若记录添加成功且启用了附加节点的功能则附加至节点
                    {
                        AppendTolNode(tableID[0]);
                    }

                    if (this._isEnableSpecial && returnValue > 0)         // 若记录添加成功且启用了附加至专题的功能则附加至专题信息表
                    {
                        AppendToSpecial(tableID[0]);
                    }
                }
                else                       //修改
                {
                    

                    
                    returnValue = Utils.ParseInt(dal.Update(this.tableName, id, hsEditField,null),0);

                    if (this._isEnableSpecial)  // 启用了附加至专题的功能
                    {
                        EditSpecialInfo(id);
                        LoadSpecial(id, ref hsEditField);
                    }

                    if (this._isEnableNode)     // 启用了附加至节点的功能 
                    {
                        LoadNode(ref hsEditField);
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="isAdd">是否是添加操作</param>
        /// <param name="id">修改时记录ID</param>
        /// <returns>影响行数</returns>
        public int Edit(bool isAdd, string id, ref string strID)
        {
            int returnValue;                                  // 操作影响行数
            Hashtable hsFieldParam;                           // 字段缺省值

            hsFieldParam = ParseFieldParam();

            if (hsFieldParam == null)                         // 字段参数有误
            {
                returnValue = 0;
            }
            else
            {
                foreach (string fieldName in hsFieldParam.Keys)      // 遍历获取字段值
                {
                    string fieldValue = null;                               // 当前字段值
                    fieldValue = System.Web.HttpContext.Current.Request.Form[fieldName];

                    if (fieldValue != null)
                    {
                        hsEditField.Add(fieldName, fieldValue);
                    }
                }
                hsEditField.Add("NodeCode", this.nodeCode);
                hsEditField.Add("SiteID", this.siteID);

                if (isAdd)                 // 添加
                {
                    string[] tableID;

                    tableID = Public.GetTableID("0", this.tableName);
                    strID = tableID[0];
                    hsEditField.Add("ReferenceID", tableID[0]);

                    returnValue = Utils.ParseInt(dal.Add(this.tableName, tableID[0], Utils.ParseInt(tableID[1], 0), hsEditField,null),0);

                    if (this._isEnableNode && returnValue > 0)   // 若记录添加成功且启用了附加节点的功能则附加至节点
                    {
                        AppendTolNode(tableID[0]);
                    }

                    if (this._isEnableSpecial && returnValue > 0)         // 若记录添加成功且启用了附加至专题的功能则附加至专题信息表
                    {
                        AppendToSpecial(tableID[0]);
                    }
                }
                else                       //修改
                {



                    returnValue = Utils.ParseInt(dal.Update(this.tableName, id, hsEditField,null),0);

                    if (this._isEnableSpecial)  // 启用了附加至专题的功能
                    {
                        EditSpecialInfo(id);
                        LoadSpecial(id, ref hsEditField);
                    }

                    if (this._isEnableNode)     // 启用了附加至节点的功能 
                    {
                        LoadNode(ref hsEditField);
                    }
                }
            }
            return returnValue;
        }
        #endregion

        #region 加载字段默认/初始值
        /// <summary>
        /// 加载字段默认/初始值
        /// </summary>
        /// <returns></returns>
        public Hashtable FillField(bool IsLoad)
        {
            Hashtable hsInitField;      // 各字段初始值,以字段名称为键名

            if (IsLoad && HttpContext.Current.Request["action"] != null && HttpContext.Current.Request["action"].ToString() == "edit")  // 编辑操作
            {
                string id;             // 记录ID

                id = HttpContext.Current.Request.QueryString["ID"];
                if (!string.IsNullOrEmpty(id))
                {
                    hsInitField = dal.GetHashTableByID(this.tableName, id);
                    LoadSpecial(id, ref hsInitField);
                    LoadNode(ref hsInitField);
                }
                else
                {
                    hsInitField = null;
                }
            }
            else // 添加操作
            {
                hsInitField = ParseFieldParam();
                LoadSpecial(null, ref hsInitField);
                LoadNode(ref hsInitField);
            }

            return hsInitField;
        }
        #endregion

        #region 添加登录用户信息
        //public void WriteLoginInfo(ref Hashtable hsFiled)
        //{
        //    Model.Member.Member modelMember = System.Web.HttpContext.Current.Session[KingTop.Common.SystemConst.HQB360] as Model.Member.Member;
        //    if (modelMember != null)
        //    {
        //        hsFiled["ContactMan"] = modelMember.UserName;
        //        hsFiled["Email"] = modelMember.Email;
        //    }
        //}
        #endregion

    }
}
