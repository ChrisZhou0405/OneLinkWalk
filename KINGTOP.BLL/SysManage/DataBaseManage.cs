using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年7月14日
    功能描述： 数据库管理操作类
// 更新日期        更新人      更新原因/内容
//2010-9-14        胡志瑶      GetUserTableInfo(),用户表数据不包括系统表
    ===============================================================*/
#endregion

namespace KingTop.BLL.SysManage
{
    public class DataBaseManage
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.SysManage.IDataBaseManage dal = (IDAL.SysManage.IDataBaseManage)Assembly.Load(path).CreateInstance(path + ".SysManage.DataBaseManage");

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
 
        #region 得到用户表数据
        /// <summary>
        /// 得到用户表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserTableInfo()
        {
            DataTable dt=dal.GetUserTableInfo();
            DataView dv =new DataView(dt);
            dv.RowFilter="name not like 'sys%'";   //去掉系统表
            return dv.ToTable();
        }
        #endregion    
 
        #region 直接执行sql语句
        /// <summary>
        /// 直接执行sql语句
        /// </summary>
        /// <returns>是否成功</returns>
        public bool ExecSql(string sql)
        {
            return dal.ExecSql(sql);
        }
        #endregion

        #region 执行多条select语句返回DataSet的第一个Table
        /// <summary>
        /// 执行多条select语句
        /// </summary>
        /// <returns>返回DataSet的第一个Table</returns>
        public DataTable GetTableExecSql(string sql)
        {
            return dal.GetTableExecSql(sql);
        }
        #endregion
    }
}
