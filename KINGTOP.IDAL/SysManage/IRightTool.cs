using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Xml;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月24日
    功能描述： 权限菜单操作接口
    ===============================================================*/
#endregion

namespace KingTop.IDAL.SysManage
{  

    public interface IRightTool
    {
        bool IsHaveRightByOperCode(string NodeID, string OperType, string accountId);

        DataTable GetRightData(string accountId, string NodeID);

        ArrayList GetModuleRightList(string strUserGropCode,string RoleCode ,string strMdlCode);

        ArrayList GetModuleRightList2(string strRoleCode, string strMdlCode);

        ArrayList GetModuleRightList3(string strUserGropCode, string strRoleCode, string strAccountID, string strMdlCode);

        ArrayList GetTemplateRightList(int TemplateID, string strMdlCode);

        //DataTable GetTreeData(string strParCode, int SiteID);

        DataTable GetTemplateTreeData(string strParCode, int TemplateID);

        //DataTable GetTreeLeafData(int SiteID,string strMdlCode);

        DataTable GetTemplateTreeLeafData(int TemplateID, string strMdlCode);

        //DataTable GetParentGropNodeID(string ParentGropID);

        DataTable GetParentGropPer(string ParentGropID);

        void SaveData(string strUserGropCode, XmlNode objNodes, string strSubRigth);

        void SaveData2(string strRoleCode, XmlNode objNodes);

        void SaveData3(string strAccountCode, XmlNode objNodes);

        void SaveDataTemplatePer(int TemplateID, XmlNode objNodes);

        ArrayList GetUserGroupRightList(string GetType, string strUserGroupCode, string NodeID);

        void SaveColumnRight(string strUserGropCode, ArrayList arrOperCode, string strNodeID);

        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);
    }
}
