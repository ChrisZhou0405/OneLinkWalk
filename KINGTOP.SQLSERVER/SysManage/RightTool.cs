using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using System.Configuration;
using System.Data;
using System.Collections;
using KingTop.IDAL;
using KingTop.Common;
using System.Xml;
using System.Data.SqlClient;
using KingTop.SQLServer;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月24日
    功能描述： 权限菜单操作类
    ===============================================================*/
#endregion

namespace KingTop.SQLServer.SysManage
{  
    public class RightTool : KingTop.IDAL.SysManage.IRightTool
    {
        ActionPermit objActPermit = new ActionPermit();
        ModuleNode objModNode = new ModuleNode();

        #region 根据操作节点ID，操作类型，用户登录ID判断当前用户是否有当前操作权限
        public bool IsHaveRightByOperCode(string NodeID,string OperType, string accountId)
        {            
            //先判断是不是超级用户
            if (accountId != null && accountId.Equals("0"))
            {
                return true;
            }
            else
            {
                KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
                //parms.I1 = 1;
                parms.I2 = Utils.ParseInt(accountId,1);
                parms.S1 = NodeID;
                parms.S2 = OperType;
                DataTable dt = objActPermit.GetList("ISHAVERIGHTBYOPERCODE", parms);                
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion


        #region 权限分配页面生成横排权限树
        /// <summary>
        /// 得到某用户组的所有权限
        /// </summary>
        /// <param Name="strUserID">用户组id</param>
        /// <param Name="strMdlCode">模块编码</param>
        /// <returns></returns>
        public ArrayList GetModuleRightList(string strUserGropCode,string strRoleCode, string strMdlCode)
        {
            ArrayList arrReturn;

            KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
            parms.S1 = strUserGropCode;
            parms.S2 = strRoleCode;
            parms.S3 = strMdlCode;
            DataTable dt = objActPermit.GetModuleRightList("GetModuleRightList1", parms);
            try
            {                
                arrReturn = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arrReturn.Add(dt.Rows[i][0].ToString().Trim());
                }
                dt.Dispose();
                dt = null;
            }
            catch
            {
                arrReturn = new ArrayList();
            }           
            return arrReturn;
        }

        /// <summary>
        /// 得到某角色的所有权限
        /// </summary>
        /// <param Name="strUserID">角色id</param>
        /// <param Name="strMdlCode">模块编码</param>
        /// <returns></returns>
        public ArrayList GetModuleRightList2(string strRoleCode, string strMdlCode)
        {
            ArrayList arrReturn=new ArrayList ();
            if (!string.IsNullOrEmpty(strRoleCode))
            {
                KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
                parms.S2 = strRoleCode;
                parms.S3 = strMdlCode;

                DataTable dt = objActPermit.GetModuleRightList("GetModuleRightList2", parms);
                try
                {
                    arrReturn = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        arrReturn.Add(dt.Rows[i][0].ToString().Trim());
                    }
                    dt.Dispose();
                    dt = null;
                }
                catch
                {
                    arrReturn = new ArrayList();
                }
            }
            return arrReturn;
        }

        /// <summary>
        /// 得到某用户组的所有权限
        /// </summary>
        /// <param Name="strUserID">用户组id</param>
        /// <param Name="strMdlCode">模块编码</param>
        /// <returns></returns>
        public ArrayList GetModuleRightList3(string strUserGropCode, string strRoleCode,string strAccountID, string strMdlCode)
        {
            ArrayList arrReturn;

            KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
            parms.S1 = strUserGropCode;
            parms.S2 = strRoleCode;
            parms.S3 = strMdlCode;
            parms.S4 = strAccountID;
            DataTable dt = objActPermit.GetModuleRightList("GetModuleRightList3", parms);
            try
            {
                arrReturn = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arrReturn.Add(dt.Rows[i][0].ToString().Trim());
                }
                dt.Dispose();
                dt = null;
            }
            catch
            {
                arrReturn = new ArrayList();
            }
            return arrReturn;
        }
        
        //得到模板权限
        public ArrayList GetTemplateRightList(int TemplateID, string strMdlCode)
        {
            ArrayList arrReturn = new ArrayList();
            KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
            parms.S2 = TemplateID.ToString();
            parms.S3 = strMdlCode;

            DataTable dt = objActPermit.GetModuleRightList("GetTemplateRightList", parms);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        arrReturn.Add(dt.Rows[i][0].ToString().Trim());
                    }
                    dt.Dispose();
                    dt = null;
                }
                catch{}
            }
            return arrReturn;
        }


        ///// 得到文件夹节点数据
        //public DataTable GetTreeData(string strParCode, int SiteID)
        //{
        //    KingTop.Model.SelectParams parm = new KingTop.Model.SelectParams();
        //    parm.I1= SiteID;
        //    parm.I2 = 0;
        //    parm.S1 = "";
        //    parm.S2 = strParCode;
        //    DataTable dt = objModNode.GetList("GetTreeData", parm);
        //    return dt;
        //}

        /// 得到模板文件夹节点数据
        public DataTable GetTemplateTreeData(string strParCode, int TemplateID)
        {
            KingTop.Model.SelectParams parm = new KingTop.Model.SelectParams();
            parm.I1 = TemplateID;
            parm.I2 = 0;
            parm.S1 = "";
            parm.S2 = strParCode;
            DataTable dt = objModNode.GetList("GetTemplateTreeData", parm);
            return dt;
        }

        /// 得到具体操作叶子节点数据 
        //public DataTable GetTreeLeafData(int SiteID,string strMdlCode)
        //{
        //    KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
        //    parms.I1 = SiteID;
        //    parms.S3 = strMdlCode;

        //    DataTable dt = objActPermit.GetModuleRightList("GetTreeLeafData", parms);
        //    return dt;
        //}

        /// 得到模板操作叶子节点数据 
        public DataTable GetTemplateTreeLeafData(int TemplateID, string strMdlCode)
        {
            KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
            parms.I1 = TemplateID;
            parms.S3 = strMdlCode;

            DataTable dt = objActPermit.GetModuleRightList("GetTemplateTreeLeafData", parms);
            return dt;
        }

        /// 得到父用户组的所有所有节点编码
        //public DataTable GetParentGropNodeID(string ParentGropID)
        //{
        //    KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
        //    parms.S3 = ParentGropID;

        //    DataTable dt = objActPermit.GetModuleRightList("GetParentGropNodeID", parms);
        //    return dt;
        //}

        /// 得到父用户组的所有所有操作编码
        public DataTable GetParentGropPer(string ParentGropID)
        {
            KingTop.Model.SelectParams parms = new KingTop.Model.SelectParams();
            parms.S3 = ParentGropID;

            DataTable dt = objActPermit.GetModuleRightList("GetParentGropPer", parms);
            return dt;
        }

        ///保存修改(用户组)
        public void SaveData(string strUserGropCode, XmlNode objNodes, string strSubRigth)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@strUserGropCode",strUserGropCode)
                };
                string strSQL = "delete from K_SysUserGroupPermit where UserGroupCode=@strUserGropCode ";
                for (int i = 0; i < objNodes.ChildNodes.Count; i++)
                {                
                    string strOprCode = objNodes.ChildNodes[i].Attributes["BindOperCode"].Value;
                    string strNodeCode = objNodes.ChildNodes[i].Attributes["NodeCode"].Value;
                    strSQL += "insert into K_SysUserGroupPermit(UserGroupCode,PermitCode,NodeID) values(@strUserGropCode,'" + strOprCode + "','" + strNodeCode + "')";
                }
                if (strSubRigth.Length > 0)
                {
                    //strSQL += "delete from K_SysUserGroupPermit where UserGroupCode IN (select UserGroupCode from K_SysUserGroup,(select SiteID,NumCode from K_SysUserGroup where UserGroupCode='" + strUserGropCode + "') B where K_SysUserGroup.SiteID=B.SiteID AND K_SysUserGroup.ParentNumCode like '%'+B.NumCode+'%') and PermitCode IN (select ID from K_SysActionPermit where OperCode in(" + strSubRigth + "))";
                    strSQL += "delete from K_SysUserGroupPermit where UserGroupCode IN (select UserGroupCode from K_SysUserGroup,(select SiteID,NumCode from K_SysUserGroup where UserGroupCode='" + strUserGropCode + "') B where K_SysUserGroup.SiteID=B.SiteID AND K_SysUserGroup.ParentNumCode like '%'+B.NumCode+'%') and PermitCode IN ('" + strSubRigth.Replace (",","','") + "')";
                }
                try
                {
                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSQL,prams);
                }
                catch (System.Exception exp)
                {
                    throw new Exception(exp.Message);
                }
                return;           
        }

        ///保存修改(角色)
        public void SaveData2(string strRoleCode, XmlNode objNodes)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@strRoleCode",strRoleCode)
                };
            string strSQL = "delete from K_SysRolePermit where RoleCode=@strRoleCode ";
            for (int i = 0; i < objNodes.ChildNodes.Count; i++)
            {                
                string strOprCode = objNodes.ChildNodes[i].Attributes["BindOperCode"].Value;
                string strNodeCode = objNodes.ChildNodes[i].Attributes["NodeCode"].Value;
                strSQL += "insert into K_SysRolePermit(RoleCode,PermitCode,NodeID) values(@strRoleCode,'" + strOprCode + "','" + strNodeCode + "')";
            }
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSQL,prams);
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
            return;
        }

        ///保存修改(用户)
        public void SaveData3(string strAccountCode, XmlNode objNodes)
        {
            //删除用户本站点的权限
            string strSQL = "delete from K_SysAccountPermit where AccountID=@strAccountCode and NodeID in (select NodeID from K_SysModuleNode  where WebSiteID=(select WebSiteID from K_SysModuleNode where NodeID=@NodeID)); ";
            string strNodeCode = string.Empty;
            for (int i = 0; i < objNodes.ChildNodes.Count; i++)
            {
                string strOprCode = objNodes.ChildNodes[i].Attributes["BindOperCode"].Value;
                strNodeCode = objNodes.ChildNodes[i].Attributes["NodeCode"].Value;
                strSQL += "insert into K_SysAccountPermit(AccountID,PermitCode,NodeID) values(@strAccountCode,'" + strOprCode + "','" + strNodeCode + "');";
            }

            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@strAccountCode",strAccountCode),
                    new SqlParameter ("@NodeID",strNodeCode)
                };

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSQL, prams);
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
            return;
        }

        ///保存修改(模板权限)
        public void SaveDataTemplatePer(int TemplateID, XmlNode objNodes)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@TemplateID",TemplateID)
                };
            string strSQL = "delete from K_WebSiteTemplatePermit where TemplateID=@TemplateID ";
            for (int i = 0; i < objNodes.ChildNodes.Count; i++)
            {
                string strOprCode = objNodes.ChildNodes[i].Attributes["BindOperCode"].Value;
                string strNodeCode = objNodes.ChildNodes[i].Attributes["NodeCode"].Value;
                strSQL += "insert into K_WebSiteTemplatePermit(TemplateID,PermitCode,TemplateNodeID) values(@TemplateID,'" + strOprCode + "','" + strNodeCode + "')";
            }
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSQL, prams);
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
            return;
        }

        //栏目管理权限
        public void SaveColumnRight(string strUserGropCode, ArrayList arrOperCode, string strNodeID)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@strUserGropCode",strUserGropCode),
                    new SqlParameter("@strNodeID",strNodeID)
                };
            string strSQL = "delete from K_SysUserGroupPermit where UserGroupCode=@strUserGropCode and NodeID=@strNodeID";
            for (int i = 0; i < arrOperCode.Count; i++)
            {
                string strOprCode = arrOperCode[i].ToString();
                strSQL += " insert into K_SysUserGroupPermit(UserGroupCode,PermitCode,NodeID) values(@strUserGropCode,'" + strOprCode + "',@strNodeID)";
            }
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSQL, prams);
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
            return;
        }
        #endregion

        #region 查找当前用户当前节点的所有操作权限
        public DataTable GetRightData(string accountId, string NodeID)
        {
            ActionPermit objActionPer=new ActionPermit();           
            DataTable dt = objActionPer.GetList("SETRIGHT", Utils.getTwoParams(NodeID, accountId));
            return dt;
        }
        #endregion

        #region 栏目管理权限分配用到方法

        /// <summary>
        /// 返回当前站点当前栏目下某用户组(用户组的角色)的所有权限
        /// </summary>
        /// <param name="GetType">"UserGroup","Role"</param>
        /// <param name="strUserGroupCode">用户组编码</param>
        /// <param name="NodeID">节点编码，如果是新增为""</param>
        public ArrayList GetUserGroupRightList(string GetType, string strUserGroupCode, string NodeID)
        {
            ArrayList arrReturn = new ArrayList();

            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("TranType",GetType),
                    new SqlParameter("UserGroupCode",strUserGroupCode),
                    new SqlParameter("NodeID",NodeID)
                };
            DataTable dt = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_GetUserGroupRightList", prams).Tables[0];
            try
            {               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arrReturn.Add(dt.Rows[i][0].ToString().Trim());
                }
                dt.Dispose();
                dt = null;
            }
            catch{}
            return arrReturn;
        }

        #endregion

        #region 以下方法gavin添加 by 2010-07-12，tranType=USERGROUPPERMITAll或ROLEPERMITAll或USERPERMITAll
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tranType",tranType),
                    new SqlParameter("I1",paramsModel.I1),
                    new SqlParameter("I2",paramsModel.I2),
                    new SqlParameter("S1",paramsModel.S1),
                    new SqlParameter("S2",paramsModel.S2)
                };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_K_SysModuleNodeSel", prams).Tables[0];
        }
        #endregion
    }
}
