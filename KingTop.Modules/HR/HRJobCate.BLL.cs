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
	public class HRJobCate
	{
		#region GetPostData
        public Entity.HRJobCate GetPostData
        {
            get
            {
                Entity.HRJobCate model = new Entity.HRJobCate();
                model.AddDate = DateTime.Now ;
                model.CateType = Utils.RequestStr("CateType");
                model.DelTime = Utils.StrToDate(Utils.RequestStr("DelTime"));
                model.FlowState = Utils.ParseInt(Utils.RequestStr("FlowState"), 3);
                model.Intro = Utils.RequestStr("Intro");
                model.IsDel = Utils.ParseInt(Utils.RequestStr("IsDel"), 0);
                model.ParentID = Utils.RequestStr("ParentID");
                model.Title = Utils.RequestStr("txtTitle");
                model.Orders = Utils.RequestStr("Orders",0);
                return model;
            }
        }
			#endregion

		#region 添加
        public string[] CreateItem(Entity.HRJobCate model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string sql = "select max(ID) from K_HRJobCate Where ParentID=@ParentID AND CateType=@CateType";
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@ParentID",model.ParentID),
                    new SqlParameter("@CateType",model.CateType),
                };
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, prams);
            string id = "100";
            if (dr.Read())
            {
                string id1 = dr[0].ToString();
                if (string.IsNullOrEmpty(id1) && model.ParentID != "0")
                {
                    id = model.ParentID + "001";
                }
                else if(!string.IsNullOrEmpty(id1))
                {
                    int idLen = id1.Length;
                    string id2 = "00" + (Utils.ParseInt(id1.Substring(idLen - 3), 0) + 1).ToString();
                    id = id1.Substring(0, idLen - 3) + id2.Substring(id2.Length - 3);
                }
            }
            else
            {
                if (model.ParentID != "0")
                {
                    id = model.ParentID + "001";
                }
            }
            dr.Close();
            dr.Dispose();

            dic.Add("ID", id);
            dic.Add("AddDate", model.AddDate.ToString());
            dic.Add("CateType", model.CateType);
            dic.Add("DelTime", model.DelTime.ToString());
            dic.Add("FlowState", model.FlowState.ToString());
            dic.Add("Intro", model.Intro.ToString());
            dic.Add("IsDel", model.IsDel.ToString());
            dic.Add("NodeCode", model.NodeCode.ToString());
            dic.Add("ParentID", model.ParentID);
            dic.Add("SiteID", model.SiteID.ToString());
            dic.Add("Title", model.Title.ToString());
            dic.Add("Orders", model.Orders.ToString());
            return InfoHelper.Add("K_HRJobCate", dic);
        }
		#endregion

        #region 修改
        public string Edit(Modules.Entity.HRJobCate mode)
        {
            string reMsg = string.Empty;
            string sql = "UPDATE K_HRJobCate Set Title=@Title,Intro=@Intro,Orders=@Orders WHERE ID=@ID";
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@Title",mode.Title),
            new SqlParameter("@Intro",mode.Intro),
            new SqlParameter("@Orders",mode.Orders),
            new SqlParameter("@ID",mode.ID),
            };
            try
            {
                reMsg=SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param).ToString ();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion

        #region 根据ID获取单条数据
        public Entity.HRJobCate GetItemByID(string ID)
        {
            string sSql = "select AddDate,CateType,DelTime,FlowState,ID,Intro,IsDel,NodeCode,ParentID,SiteID,Title,Orders from K_HRJobCate where ID=@ID";
            SqlParameter param = new SqlParameter("@ID",ID);
            Entity.HRJobCate model = new Entity.HRJobCate();
            using (DbDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sSql, param))
            {
                while (dr.Read())
                {
                    model.AddDate = Utils.StrToDate(dr["AddDate"].ToString());
                    model.CateType = dr["CateType"].ToString();
                    model.DelTime = Utils.StrToDate(dr["DelTime"].ToString());
                    model.FlowState = Utils.ParseInt(dr["FlowState"].ToString(), 0);
                    model.ID = dr["ID"].ToString();
                    model.Intro = dr["Intro"].ToString();
                    model.IsDel = Utils.ParseInt(dr["IsDel"].ToString(), 0);
                    model.NodeCode = dr["NodeCode"].ToString();
                    model.ParentID = dr["ParentID"].ToString();
                    model.SiteID = Utils.ParseInt(dr["SiteID"].ToString(), 0);
                    model.Title = dr["Title"].ToString();
                    model.Orders = Utils.ParseInt(dr["Orders"].ToString(), 0);
                }
            }
            return model;
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
                new SqlParameter("@Table","K_HRJobCate"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","ID Asc,Orders Asc"),                
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
                    else if (kvp.Key == "CateType")
                    {
                        sqlWhere += " AND " + kvp.Key + " = '" + kvp.Value+"'";
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

        #region 根据CateType、SiteID查询所有记录
        public DataTable dt(string cateType,int SiteID)
        {
            string sql = "select ID,ParentID,Title,FlowState,AddDate,Orders from K_HRJobCate Where isDel=0 and CateType=@CateType AND SiteID=@SiteID Order By ID";
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@CateType",cateType),
                    new SqlParameter("@SiteID",SiteID)
                };
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, prams).Tables[0];
        }
        #endregion

        #region 根据名称查询节点及其父节点
        public DataTable dt(string cateType, int SiteID, string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return dt(cateType, SiteID);
            }

            string sql = @"
                with GetCateParent as  
                (  
                select * from K_HRJobCate where Title like '%'+@keyWord+'%' AND isDel=0 and CateType=@CateType AND SiteID=@SiteID
                UNION ALL  
                 (SELECT a.* from K_HRJobCate as a inner join  
                  GetCateParent as b on a.ID=b.ParentID  
                 )  
                )  
                SELECT distinct * FROM GetCateParent order by ID
            ";
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@CateType",cateType),
                    new SqlParameter("@SiteID",SiteID),
                    new SqlParameter("@keyWord",keyWord)
                };
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, prams).Tables[0];
        }
        #endregion

        #region 根据ParentID查询子节点
        public DataTable GetSubDt(string ParentId, string cateType, int SiteID)
        {
            string sql = "select ID,ParentID,Title,FlowState,AddDate,Orders from K_HRJobCate Where ParentID=@ParentID AND isDel=0 AND SiteID=@SiteID and CateType=@CateType Order By Orders,Order ID";
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("@CateType",cateType),
                    new SqlParameter("@ParentID",ParentId),
                    new SqlParameter("@SiteID",SiteID)
                };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, prams).Tables[0];
        }
        #endregion

        #region 删除 将删除字段isdel值修改为1
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">删除多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string Delete(string idList)
        {
            if (string.IsNullOrEmpty(idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Update K_HRJobCate set IsDel=1,DelTime=getdate() where Left(id,"+id.Length+")='" + id + "';";  //删除自己和子节点记录
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

        #region 审核
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">删除多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string Check(string idList, int CheckValue)
        {
            if (string.IsNullOrEmpty(idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Update K_HRJobCate set FlowState=" + CheckValue + " where id='" + id + "';";
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