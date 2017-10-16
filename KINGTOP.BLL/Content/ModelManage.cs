#region 引用程序集using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web.UI.WebControls;

using KingTop.IDAL;
using KingTop.Model;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-09
// 功能描述：对K_ModelManage表的操作
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ModelManage
    {
        #region 变量成员
        private static string path= ConfigurationManager.AppSettings["WebDAL"]; 
        private IDAL.Content.IModelManage dal = (IDAL.Content.IModelManage)Assembly.Load(path).CreateInstance(path + ".Content.ModelManage");
        #endregion

        #region 根据传入的参数查询K_ModelManage,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ModelManage,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 增、改K_ModelManage表        /// <summary>
        /// 增、改K_ModelManage表        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="ModModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.Content.ModelManage modModel, string ddlSQL, string dmlSQL)
        {
            return dal.Save(trantype, modModel, ddlSQL,dmlSQL,this.operationTitle,this.operationName,this.operationCount);
        }
        #endregion

        #region 设置或者删除K_ModelManage记录
        /// <summary>
        /// 设置或者删除K_ModelManage记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModelManageSet(string tranType, string setValue, string IDList)
        {
            return dal.ModelManageSet(tranType, setValue, IDList);
        }
        #endregion

        #region 创建表        /// <summary>
        /// 创建表        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string CreateTable(string tableName,bool isSub)
        {
            string createSQL;

            createSQL = dal.CreateTable(tableName,isSub);

            return createSQL;
        }
        #endregion

        #region 创建表时初始字段插入字段表中
        /// <summary>
        /// 创建表时初始字段插入字段表中
        /// </summary>
        /// <param name="modelID">所属模型ID</param>
        /// <returns></returns>
        public string InsertInitField(string modelID,bool isSub)
        {
            string dmlSQL;
            string[] id;
            int[] orders;

            id = new string[13];
            orders = new int[13];

            for (int i = 0; i < id.Length; i++)
            {
                this.order = this.order + 1;
                orders[i] = this.order;
                id[i] = Public.GetTableID("K_ModelField", orders[i]);
            }

            dmlSQL = dal.InsertInitField(id, orders, modelID,isSub);
            return dmlSQL;
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            dal.PageData(pager);
        }
        #endregion

        #region 绑定子模型分组
        public void SubModelGroupBind(DropDownList ddlSubModelGroup)
        {
            DataTable dt;

            dt = dal.GetSubModelGroupList();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSubModelGroup.DataTextField = "Name";
                ddlSubModelGroup.DataValueField = "ID";
                ddlSubModelGroup.DataSource = dt;
                ddlSubModelGroup.DataBind();
            }
        }
        #endregion
    }
}
