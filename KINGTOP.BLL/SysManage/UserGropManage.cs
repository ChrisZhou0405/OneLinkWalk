using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Reflection;
using KingTop.IDAL;

namespace KingTop.BLL.SysManage
{

    #region 版权注释
    /*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月29日
    功能描述： 用户组管理类
    ===============================================================*/
    #endregion

    public class UserGropManage
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IUserGropManage dal = (IDAL.SysManage.IUserGropManage)Assembly.Load(path).CreateInstance(path + ".SysManage.UserGropManage");

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
        public void PageData(KingTop.Model.Pager pager,int strNumber)
        {
            dal.PageData(pager,strNumber);
        }
        #endregion
    }
}
