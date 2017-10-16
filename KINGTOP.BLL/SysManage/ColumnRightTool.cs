using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Reflection;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月24日
    功能描述： 栏目权限操作类
    ===============================================================*/
#endregion

namespace KingTop.BLL.SysManage
{
    public class ColumnRightTool
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.SysManage.IRightTool dal = (IDAL.SysManage.IRightTool)Assembly.Load(path).CreateInstance(path + ".SysManage.RightTool");

        /// <summary>
        /// 返回当前站点当前栏目下某用户组(用户组的角色)的所有权限
        /// </summary>
        /// <param name="GetType">"UserGroup","Role"</param>
        /// <param name="strUserGroupCode">用户组编码</param>
        /// <param name="NodeID">节点编码，如果是新增为""</param>
        /// <param name="SiteID">站点ID</param>
        public ArrayList GetUserGroupRightList(string GetType,string strUserGroupCode,string NodeID)
        {
            ArrayList arrReturn = new ArrayList();
            if (NodeID != "")
            {
                arrReturn = dal.GetUserGroupRightList(GetType, strUserGroupCode, NodeID);
            }
            return arrReturn;
        }

        //栏目管理权限
        public void SaveData(string strUserGropCode, ArrayList arrOperCode, string strNodeID)
        {
            dal.SaveColumnRight(strUserGropCode, arrOperCode, strNodeID);
        }

    }
}
