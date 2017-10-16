using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using KingTop.IDAL.SysManage;
using KingTop.Common;

namespace KingTop.SQLServer.SysManage
{

    #region 版权注释
    /*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月29日
    功能描述： 用户组管理类
    ===============================================================*/
    #endregion

    public class UserGropManage : KingTop.IDAL.SysManage.IUserGropManage
    {
        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","UserGroupCode"),
                new SqlParameter("@Table","K_SysUserGroup"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","UserGroupCode DESC"),                
                 new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        string GetWhere(Dictionary<string, string> DicWhere)
        {
            string sqlWhere = "1=1";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "SiteID")
                    {
                        sqlWhere += " AND "+ kvp.Key + " = " + kvp.Value;
                    }
                    else if (kvp.Key == "NumCode")
                    {
                        sqlWhere += " AND Left(ParentNumCode, "+kvp.Value.Length +")= '" + kvp.Value+"'";
                    }
                    else
                    {
                        sqlWhere += " AND " + kvp.Key + " like '%" + kvp.Value + "%'";
                    }
                }
            }

            return sqlWhere;
        }
        #endregion

        #region 使用SQL语句获得分页数据
        /// <summary>
        /// 使用SQL语句获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager, int StrNumber)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id",""),
                new SqlParameter("@Table",""),             
                new SqlParameter("@Where",""),
                new SqlParameter("@Cou",""),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","Numcode asc"),                
                new  SqlParameter("@isSql",1),
                new  SqlParameter("@strSql",GetStrSqlByNumber(StrNumber,GetWhere(pager.DicWhere)))
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        //选择调用哪条SQL语句
        public string GetStrSqlByNumber(int StrNumber,string Condition)
        {
            string strResult = string.Empty;
            switch (StrNumber)
            {
                case 1:
                    if (Condition != "")
                    {
                        strResult = GetStrSql1() + " where " + Condition;
                    }
                    else
                    {
                        strResult= GetStrSql1(); //+ " order by UserGroupCode desc";
                    }
                    break;
                default:
                    strResult="";
                    break;
            }
            return strResult;
        }

        public string GetStrSql1()
        {
            string strsql = "select * from (select distinct A.UserGroupCode,UserGroupName,NumCode,ParentNumCode,SiteID from K_SysUserGroup as A left join  K_SysUserGroupRole as  B on A.UserGroupCode=B.UserGroupCode WHERE A.IsDel=0)T";
            return strsql;
        }
        #endregion


      
    }
}
