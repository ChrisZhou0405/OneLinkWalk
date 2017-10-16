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
	public class HRJob
	{
		#region GetPostData
		public  Entity.HRJob GetPostData
		{
			get {
					Entity.HRJob model = new Entity.HRJob();
					model.AddDate = Utils.StrToDate(Utils.RequestStr("AddDate"));
					model.AddMan = Utils.RequestStr("AddMan");
                    model.Age = Utils.ParseInt(Utils.RequestStr("txtAge"), 0);
					model.DelTime = Utils.StrToDate(Utils.RequestStr("DelTime"));
                    model.Degree = Utils.RequestStr("ddlDegreeFrom");
                    model.EMail = Utils.RequestStr("txtEMail");
                    model.EndDate = Utils.StrToDate(Utils.RequestStr("txtEndDate"));
                    model.Experience = Utils.RequestStr("txtExperience");
					model.FlowState = Utils.ParseInt(Utils.RequestStr("FlowState"),3);
					model.IsDel = Utils.ParseInt(Utils.RequestStr("IsDel"),0);
					model.IsHtml = Utils.RequestStr(("IsHtml"),false);
                    model.JobType = Utils.RequestStr("ddlJobType");
                    model.MetaDescript = Utils.RequestStr("txtMetaDesc");
                    model.MetaKeyword = Utils.RequestStr("txtKeyWords");
					model.NodeCode = Utils.RequestStr("NodeCode");
                    model.Number = Utils.RequestStr("txtNumber");
					model.Orders = Utils.ParseInt(Utils.RequestStr("Orders"),0);
                    model.PageTitle = Utils.RequestStr("txtPageTitle");
                    model.PublishDate = Utils.StrToDate(Utils.RequestStr("txtPublishDate"));
					model.Qualifications = Utils.RequestStr("Qualifications");
                    model.Salary = Utils.RequestStr("txtSalary");
					model.SiteID = Utils.ParseInt(Utils.RequestStr("SiteID"),0);
					model.Title = Utils.RequestStr("txtTitle");
					model.Welfare = Utils.RequestStr("Welfare");
					model.WorkDuty = Utils.RequestStr("WorkDuty");
                    model.WorkPlace = Utils.RequestStr("ddlWorkPlace");
                    model.WorkUnit = Utils.RequestStr("ddlWorkUnit");
					return model;
				}
			}
			#endregion

		#region 添加
		public string[] CreateItem(Entity.HRJob model)
		{
				Dictionary<string, string> dic = new Dictionary<string, string>();
                string orders = Tools.Orders("K_HRJob").ToString ();
				dic.Add("AddDate",DateTime .Now .ToString ());
				dic.Add("AddMan",model.AddMan.ToString());
				dic.Add("Age",model.Age.ToString());
                dic.Add("Degree", model.Degree.ToString());
				dic.Add("EMail",model.EMail.ToString());
				dic.Add("EndDate",model.EndDate.ToString());
				dic.Add("Experience",model.Experience.ToString());
				dic.Add("FlowState",model.FlowState.ToString());
				dic.Add("IsDel","0");
				dic.Add("IsHtml","0");
				dic.Add("JobType",model.JobType.ToString());
				dic.Add("MetaDescript",model.MetaDescript.ToString());
				dic.Add("MetaKeyword",model.MetaKeyword.ToString());
				dic.Add("NodeCode",model.NodeCode.ToString());
				dic.Add("Number",model.Number.ToString());
                dic.Add("Orders", orders);
				dic.Add("PageTitle",model.PageTitle.ToString());
				dic.Add("PublishDate",model.PublishDate.ToString());
				dic.Add("Qualifications",model.Qualifications.ToString());
				dic.Add("Salary",model.Salary.ToString());
				dic.Add("SiteID",model.SiteID.ToString());
				dic.Add("Title",model.Title.ToString());
				dic.Add("Welfare",model.Welfare.ToString());
				dic.Add("WorkDuty",model.WorkDuty.ToString());
				dic.Add("WorkPlace",model.WorkPlace.ToString());
				dic.Add("WorkUnit",model.WorkUnit.ToString());
                return InfoHelper.Add("K_HRJob", dic);
		}
		#endregion

        #region 修改
        public string Edit(Modules.Entity.HRJob mode)
        {
            string reMsg = string.Empty;
            string sql = @"UPDATE K_HRJob Set 
                Title=@Title,
                JobType=@JobType,
                WorkUnit=@WorkUnit,
                WorkPlace=@WorkPlace,
                Salary=@Salary,
                Degree=@Degree,
                Age=@Age,
                Experience=@Experience,
                Number=@Number,
                EMail=@EMail,
                PublishDate=@PublishDate,
                EndDate=@EndDate,
                WorkDuty=@WorkDuty,
                Qualifications=@Qualifications,
                Welfare=@Welfare,
                PageTitle=@PageTitle,
                MetaKeyword=@MetaKeyword,
                MetaDescript=@MetaDescript
                WHERE ID=@ID";
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@Title",mode.Title),
            new SqlParameter("@JobType",mode.JobType),
            new SqlParameter("@WorkUnit",mode.WorkUnit),
            new SqlParameter("@WorkPlace",mode.WorkPlace),

            new SqlParameter("@Salary",mode.Salary),
            new SqlParameter("@Degree",mode.Degree),
            new SqlParameter("@Age",mode.Age),
            new SqlParameter("@Experience",mode.Experience),

            new SqlParameter("@Number",mode.Number),
            new SqlParameter("@EMail",mode.EMail),
            new SqlParameter("@PublishDate",mode.PublishDate),
            new SqlParameter("@EndDate",mode.EndDate),

            new SqlParameter("@WorkDuty",mode.WorkDuty),
            new SqlParameter("@Qualifications",mode.Qualifications),
            new SqlParameter("@Welfare",mode.Welfare),
            new SqlParameter("@PageTitle",mode.PageTitle),

            new SqlParameter("@MetaKeyword",mode.MetaKeyword),
            new SqlParameter("@MetaDescript",mode.MetaDescript),
            new SqlParameter("@ID",mode.ID),
            };
            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
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
            if(string.IsNullOrEmpty (idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                int id=Utils.ParseInt (ArrID[i],0);
                sql+="Update K_HRJob set IsDel=1,DelTime=getdate() where id=" + id+";";
            }

            try
            {
                reMsg=SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).ToString ();
            }
            catch(Exception e)
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
        public string Check(string idList,int CheckValue)
        {
            if (string.IsNullOrEmpty(idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                int id = Utils.ParseInt(ArrID[i], 0);
                sql += "Update K_HRJob set FlowState="+CheckValue +" where id=" + id + ";";
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

        #region 根据ID获取单条数据
        public Entity.HRJob GetItemByID(int ID)
		{
				string sSql = "select AddDate,AddMan,Age,DelTime,Degree,EMail,EndDate,Experience,FlowState,ID,IsDel,IsHtml,JobType,MetaDescript,MetaKeyword,NodeCode,Number,Orders,PageTitle,PublishDate,Qualifications,Salary,SiteID,Title,Welfare,WorkDuty,WorkPlace,WorkUnit from K_HRJob where ID=" + ID;
				Entity.HRJob model = new Entity.HRJob();
				using (DbDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sSql, null))
				{
					while (dr.Read())
					{
					model.AddDate = Utils.StrToDate(dr["AddDate"].ToString());
					model.AddMan = dr["AddMan"].ToString();
					model.Age = Utils.ParseInt(dr["Age"].ToString(),0);
					model.DelTime = Utils.StrToDate(dr["DelTime"].ToString());
                    model.Degree = dr["Degree"].ToString();
					model.EMail = dr["EMail"].ToString();
					model.EndDate = Utils.StrToDate(dr["EndDate"].ToString());
					model.Experience = dr["Experience"].ToString();
					model.FlowState = Utils.ParseInt(dr["FlowState"].ToString(),0);
					model.IsDel = Utils.ParseInt(dr["IsDel"].ToString(),0);
					model.IsHtml = Utils.ParseBool(dr["IsHtml"].ToString());
					model.JobType = dr["JobType"].ToString();
					model.MetaDescript = dr["MetaDescript"].ToString();
					model.MetaKeyword = dr["MetaKeyword"].ToString();
					model.NodeCode = dr["NodeCode"].ToString();
					model.Number = dr["Number"].ToString();
					model.Orders = Utils.ParseInt(dr["Orders"].ToString(),0);
					model.PageTitle = dr["PageTitle"].ToString();
					model.PublishDate = Utils.StrToDate(dr["PublishDate"].ToString());
					model.Qualifications = dr["Qualifications"].ToString();
					model.Salary = dr["Salary"].ToString();
					model.SiteID = Utils.ParseInt(dr["SiteID"].ToString(),0);
					model.Title = dr["Title"].ToString();
					model.Welfare = dr["Welfare"].ToString();
					model.WorkDuty = dr["WorkDuty"].ToString();
					model.WorkPlace = dr["WorkPlace"].ToString();
					model.WorkUnit = dr["WorkUnit"].ToString();
					}
				}
				return model;
		}
		#endregion

        #region 获得所有记录
        public DataTable dt(int SiteID)
        {
            string sql = "SELECT ID,Title From K_HRJob WHERE IsDel=0 AND SiteID=" + SiteID + " Order by Orders Desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            return dt;
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
                new SqlParameter("@Table","K_HRJob"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","Orders Desc"),                
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
                    else if (kvp.Key == "Title")
                    {
                        sqlWhere += " AND " + kvp.Key + " like '%" + kvp.Value + "%'";
                    }
                    else
                    {
                        sqlWhere += " AND " + kvp.Key + " = '" + kvp.Value + "'";
                    }
                }
            }

            return sqlWhere;
        }
        #endregion

        #region 修改最后预览简历时间
        public void EditLastViewResume(string jobid)
        {
            string sql = "UPDATE K_HRJob SET LastViewResume=getdate() WHERE ID=" + Utils.ParseInt(jobid, 0);
            SQLHelper.ExcuteCommand(sql);
        }
        #endregion
    }
}