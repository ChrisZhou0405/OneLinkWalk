using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Reflection;
using KingTop.Common;
using KingTop.Modules.Entity;
using System.Data.SqlClient;

namespace KingTop.Modules.BLL
{
    public class HRApplyJob
    {
        #region 添加
        public int CreateItem(Entity.HRApplyJob model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("JobID", model.JobID.ToString());
            dic.Add("ResumeID", model.ResumeID.ToString());
            dic.Add("Status", model.Status.ToString());
            dic.Add("MemberName", model.MemberName.ToString());
            return InfoHelper.Add1("K_HRApplyJob", dic);
        }
        #endregion


        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Modules.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","K_HRResume"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","ID Desc"),                
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
            string sqlWhere = "IsDel=0";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "SiteID")
                    {
                        sqlWhere += " AND " + kvp.Key + " = " + kvp.Value;
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

        #region 设置
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">设置多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string SET(string idList,int statusValue)
        {
            if (string.IsNullOrEmpty(idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Update K_HRApplyJob set Status=" + statusValue + " where id=" + id + ";"; 
            }

            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion
    }
}
