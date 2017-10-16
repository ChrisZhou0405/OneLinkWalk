
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：何伟 2010-09-01
// 功能描述：对K_TemplateSkin表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL
{
    public class TemplateSkin
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.ITemplateSkin dal = (IDAL.ITemplateSkin)Assembly.Load(path).CreateInstance(path + ".TemplateSkin");

        #region 根据传入的参数查询K_TemplateSkin,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_TemplateSkin,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 增、改K_TemplateSkin表
        /// <summary>
        /// 增、改K_TemplateSkin表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="TemModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.TemplateSkin temModel)
        {
            return dal.Save(trantype, temModel);
        }
        #endregion

        #region 设置或者删除K_TemplateSkin记录
        /// <summary>
        /// 设置或者删除K_TemplateSkin记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string TemplateSkinSet(string tranType, string setValue, string IDList)
        {
            return dal.TemplateSkinSet(tranType, setValue, IDList);
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
    }
}
