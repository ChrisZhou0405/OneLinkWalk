
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL.Content;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武 2010-08-02
// 功能描述：对K_RecyclingManage表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.Content
{
    public class RecyclingManage
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.Content.IRecyclingManage dal = (IDAL.Content.IRecyclingManage)Assembly.Load(path).CreateInstance(path + ".Content.RecyclingManage");

        #region 根据传入的参数查询K_RecyclingManage,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_RecyclingManage,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 增、改K_RecyclingManage表
        /// <summary>
        /// 增、改K_RecyclingManage表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="RecModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.Content.RecyclingManage recModel)
        {
            return dal.Save(trantype, recModel);
        }
        #endregion

        #region 设置或者删除K_RecyclingManage记录
        /// <summary>
        /// 设置或者删除K_RecyclingManage记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string RecyclingManageSet(string tranType, string setValue, string IDList)
        {
            return dal.RecyclingManageSet(tranType, setValue, IDList);
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

        /// <summary>
        /// 创建视图
        /// </summary>
        public void CreateView()
        {
            dal.CreateView();
        }

        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageNewsData(KingTop.Model.Pager pager)
        {
            dal.PageNewsData(pager);
        }

           /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        public void Del(string Id)
        {
            dal.Del(Id);
        }
        #endregion
    }
}
