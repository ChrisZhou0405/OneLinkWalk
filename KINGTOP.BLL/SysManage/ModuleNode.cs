using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using KingTop.IDAL;
using System.Data;
using System.Configuration;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：肖丹
// 创建日期：2010-03-22
// 功能描述：对K_SysModuleNode表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/


namespace KingTop.BLL.SysManage
{
    public class ModuleNode
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IModuleNode dal = (IDAL.SysManage.IModuleNode)Assembly.Load(path).CreateInstance(path + ".SysManage.ModuleNode");

        #region 根据传入的参数查询K_SysModuleNode,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysModuleNode,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        //从缓存中获取K_SysModuleNode表
        public DataTable GetModeNodeFromCache()
        {
            if (AppCache.IsExist("ModeNodeAndModuleCache"))
            {
                return (DataTable)AppCache.Get("ModeNodeAndModuleCache");
            }
            else
            {
                DataTable dt = GetList("Cache", Utils.getOneParams(""));
                AppCache.AddCache("ModeNodeAndModuleCache", dt, 1440);
                return dt;
            }
        }

        #region 增、改K_SysModuleNode表
        /// <summary>
        /// 增、改K_SysModuleNode表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="AutModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.SysManage.ModuleNode ModuleNodeModel)
        {
             string re=dal.Save(trantype, ModuleNodeModel);

            //更新缓存
            AppCache.Remove("ModeNodeAndModuleCache");
            GetModeNodeFromCache();

            AppCache.Remove("PublishNodeCache");
            AppCache.Remove("SiteMenuNodeCache");
            Publish_GetNodeFromCache();

            return re;
        }
        #endregion

        #region 设置或者删除K_SysModuleNode记录
        /// <summary>
        /// 设置或者删除K_SysModuleNode记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModuleNodeSet(string tranType, string setValue, string IDList)
        {
            string re=dal.ModuleNodeSet(tranType, setValue, IDList);
            //更新缓存
            AppCache.Remove("ModeNodeAndModuleCache");
            GetModeNodeFromCache();

            AppCache.Remove("PublishNodeCache");
            AppCache.Remove("SiteMenuNodeCache");
            Publish_GetNodeFromCache();

            return re;
        }
        #endregion

        #region 栏目移动
        public string MenuMove(string fromMenu, string toMenu)
        {
            string re = dal.MenuMove(fromMenu, toMenu);
            //更新缓存
            AppCache.Remove("ModeNodeAndModuleCache");
            GetModeNodeFromCache();

            AppCache.Remove("PublishNodeCache");
            AppCache.Remove("SiteMenuNodeCache");
            Publish_GetNodeFromCache();

            return re;
        }
        #endregion 

        #region 栏目复制
        public string MenuCopy(string fromMenu, string toMenu,bool isDataCopy)
        {
            string re = dal.MenuCopy(fromMenu, toMenu, isDataCopy);
            //更新缓存
            AppCache.Remove("ModeNodeAndModuleCache");
            GetModeNodeFromCache();

            AppCache.Remove("PublishNodeCache");
            AppCache.Remove("SiteMenuNodeCache");
            Publish_GetNodeFromCache();

            return re;
        }
        #endregion 

        #region 栏目合并
        public string MenuUnite(string fromMenu, string toMenu)
        {
            string re = dal.MenuUnite(fromMenu, toMenu);
            //更新缓存
            AppCache.Remove("ModeNodeAndModuleCache");
            GetModeNodeFromCache();

            AppCache.Remove("PublishNodeCache");
            AppCache.Remove("SiteMenuNodeCache");
            Publish_GetNodeFromCache();

            return re;
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

        /// <summary>
        /// 从缓存读取栏目信息,如果缓存没有相关信息，则从数据库读取，然后添加到缓存
        /// </summary>
        /// <param name="nodeCode"></param>
        /// <returns></returns>
        public DataTable Publish_GetNodeFromCache()
        {
            if (KingTop.Common.AppCache.IsExist("PublishNodeCache"))
            {
                return (DataTable)KingTop.Common.AppCache.Get("PublishNodeCache");
            }
            else
            {
                DataTable dt = GetList("GETNODEID", Utils.getOneParams(""));
                AppCache.AddCache("PublishNodeCache", dt, 60);
                return dt;
            }
        }

        /// <summary>
        /// 缓存前台栏目
        /// </summary>
        /// <param name="nodeCode"></param>
        /// <returns></returns>
        public DataTable SiteMenu_GetNodeFromCache(int siteid)
        {
            if (KingTop.Common.AppCache.IsExist("SiteMenuNodeCache"))
            {
                return (DataTable)KingTop.Common.AppCache.Get("SiteMenuNodeCache");
            }
            else
            {
                DataTable dt = GetList("LISTBYSITE", Utils.getOneParams(siteid.ToString ()));
                AppCache.AddCache("SiteMenuNodeCache", dt, 60);
                return dt;
            }
        }
    }
}
