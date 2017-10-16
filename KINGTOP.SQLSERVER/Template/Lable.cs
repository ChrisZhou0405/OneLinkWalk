using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using KingTop.Common;
using KingTop.IDAL.Template;
/*================================================================
    Copyright (C) 2010 华强北在线
 
// 更新日期        更新人      更新原因/内容
//2010-9-14        胡志瑶      IsExistLableName应同时判断K_T_LableFree、K_T_Lable这两个表
 * 2010-09-20      胡志瑶       GetDbTable，按表名进行排序
--===============================================================*/
namespace KingTop.SQLServer.Template
{
    public class Lable : ILable
    {
        /// <summary>
        /// 返回字符串类型
        /// </summary>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public string GetObject(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@I1",modelPrams.I1),
                new SqlParameter("@I2",modelPrams.I2),             
                new SqlParameter("@I3",modelPrams.I3),
                new SqlParameter("@S1",modelPrams.S1),
                new SqlParameter("@S2",modelPrams.S2),
                new SqlParameter("@S3",modelPrams.S3), 
                new SqlParameter("@S4",modelPrams.S4),
            };
            try
            {
                return SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Sel", prams).ToString();
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 获取标签数据
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataSet GetLable(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@I1",modelPrams.I1),
                new SqlParameter("@I2",modelPrams.I2),             
                new SqlParameter("@I3",modelPrams.I3),
                new SqlParameter("@S1",modelPrams.S1),
                new SqlParameter("@S2",modelPrams.S2),
                new SqlParameter("@S3",modelPrams.S3), 
                new SqlParameter("@S4",modelPrams.S4),
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Sel", prams);
            return ds;
        }

        /// <summary>
        /// 删除标签分类
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public int DeleteLableClass(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@I1", modelPrams.I1),
                new SqlParameter("@I2", modelPrams.I2),
                new SqlParameter("@I3", modelPrams.I3),
                new SqlParameter("@S1", modelPrams.S1),
                new SqlParameter("@S2", modelPrams.S2),
                new SqlParameter("@S3", modelPrams.S3),
                new SqlParameter("@S4", modelPrams.S4),
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Del", prams);
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public int DeleteLable(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@I1", modelPrams.I1),
                new SqlParameter("@I2", modelPrams.I2),
                new SqlParameter("@I3", modelPrams.I3),
                new SqlParameter("@S1", modelPrams.S1),
                new SqlParameter("@S2", modelPrams.S2),
                new SqlParameter("@S3", modelPrams.S3),
                new SqlParameter("@S4", modelPrams.S4),
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Del", prams);

        }

        /// <summary>
        /// 新增/修改标签分类
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLableClass(string tranType, KingTop.Model.Template.LableClassInfo model)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@SiteID",model.SiteID),
                new SqlParameter("@ClassID",model.ClassID),             
                new SqlParameter("@ClassName",model.ClassName),
                new SqlParameter("@Description",model.Description),
                new SqlParameter("@IsSystem",model.IsSystem),
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Save", prams);
        }

        /// <summary>
        /// 新增/修改标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLable(string tranType, KingTop.Model.Template.LableInfo model)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@ClassID",model.ClassID),             
                new SqlParameter("@LableID",model.LableID),
                new SqlParameter("@Title",model.Title),         
                new SqlParameter("@LableName",model.LableName),             
                new SqlParameter("@LableContent",model.LableContent),
                new SqlParameter("@Description",model.Description),
                new SqlParameter("@IsShare",model.IsShare),
                new SqlParameter("@IsSystem",model.IsSystem),
                new SqlParameter("@SiteID",model.SiteID),
                new SqlParameter("@TempPrjID",model.TempPrjID),
                new SqlParameter("@Identification",model.Identification),
                new SqlParameter("@Sequence",model.Sequence)

            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Save", prams);
        }

        /// <summary>
        /// 获取标签数据
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataSet GetLabelContentBySiteId(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@I1",modelPrams.I1),
                new SqlParameter("@I2",modelPrams.I2),             
                new SqlParameter("@I3",modelPrams.I3),
                new SqlParameter("@S1",modelPrams.S1),
                new SqlParameter("@S2",modelPrams.S2),
                new SqlParameter("@S3",modelPrams.S3), 
                new SqlParameter("@S4",modelPrams.S4),
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_T_Lable_Sel", prams);
            return ds;
        }

        /// <summary>
        /// 数据源的数据表
        /// </summary>
        /// <returns></returns>
        public IList<Model.SysManage.TableInfo> GetDbTable()
        {
            IList<Model.SysManage.TableInfo> ltb = new List<Model.SysManage.TableInfo>();
            SqlConnection sc = new SqlConnection(SQLHelper.ConnectionStringLocalTransaction);
            sc.Open();
            DataTable dt = sc.GetSchema("Tables");
            DataView dv = new DataView(dt);
            dv.Sort = "TABLE_NAME";  //按表名排序
            foreach (DataRow dr in dv.ToTable().Rows)
            {
                if (dr.ItemArray[3].ToString() == "BASE TABLE")
                {
                    Model.SysManage.TableInfo model = new KingTop.Model.SysManage.TableInfo();
                    model.TableName = dr[2].ToString();
                    model.TableDescription = dr[2].ToString();
                    model.TableType = "";
                    ltb.Add(model);
                }
            }
            return ltb;
        }

        /// <summary>
        /// 自由标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataSet GetLableFreeList(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("tranType", tranType),
                new SqlParameter("I1", modelPrams.I1),
                new SqlParameter("I2", modelPrams.I2),
                new SqlParameter("I3", modelPrams.I3),
                new SqlParameter("S1", modelPrams.S1),
                new SqlParameter("S2", modelPrams.S2),
                new SqlParameter("S3", modelPrams.S3),
                new SqlParameter("S4", modelPrams.S4),
            };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_U_LableFree_Sel", prams);
        }

        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IList<KingTop.Model.SysManage.TableInfo> GetFields(string tableName)
        {
            IList<KingTop.Model.SysManage.TableInfo> Fields = new List<KingTop.Model.SysManage.TableInfo>();
            string Sql = "select top 1 * from " + tableName + " where 1=0";
            IDataReader rd = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, Sql, null);
            for (int i = 0; i < rd.FieldCount; i++)
            {
                string fdnm = rd.GetName(i);
                KingTop.Model.SysManage.TableInfo model = new KingTop.Model.SysManage.TableInfo();
                model.TableName = fdnm;
                model.TableDescription = fdnm;
                model.TableType = rd.GetDataTypeName(i);
                Fields.Add(model);
            }
            if (rd.IsClosed == false)
                rd.Close();
            return Fields;
        }

        /// <summary>
        /// 保存自由标签相关信息
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLableFree(string tranType, KingTop.Model.Template.LableFreeInfo model)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("TranType", tranType),
                new SqlParameter("LableID", model.LableID),
                new SqlParameter("SiteID", model.SiteID), 
                new SqlParameter("LableContent", model.LableContent),
                new SqlParameter("LableSQL", model.LabelSQL),
                new SqlParameter("@Title",model.Title),         
                new SqlParameter("LableName", model.LableName),
                new SqlParameter("Description", model.Description),
                new SqlParameter("@TempPrjID",model.TempPrjID),
                new SqlParameter("@IsShare",model.IsShare),
                new SqlParameter("@ClassId",model.ClassId),
                new SqlParameter("@PageSize",model.PageSize),
                new SqlParameter("@Identification",model.Identification),
                new SqlParameter("@Sequence",model.Sequence)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_U_LableFree_Save", prams);
        }



        /// <summary>
        /// 删除自由标签
        /// </summary>
        /// <param name="lableId"></param>
        /// <returns></returns>
        public int DeleteLableFree(string lableId)
        {
            string sql = "delete from K_T_LableFree Where lableId in(" + lableId + ")";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecSqlString(string sql)
        {
            DataTable dt = null;
            try
            {
                dt = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null).Tables[0];
            }
            catch (Exception ex)
            { }
            return dt;
        }

        /// <summary>
        /// 检查是否存在着相同名称的标签
        /// </summary>
        /// <param name="lableName"></param>
        /// <returns></returns>
        public bool IsExistLableName(string title, int siteID, int lableID)
        {
            StringBuilder strSql = new StringBuilder();
            string rowFilter = "";
            if (lableID != 0)
            {
                rowFilter = " and lableID!=" + lableID;
            }
            strSql.Append("select sum(con) From (");
            strSql.Append("select COUNT(*) con From K_T_LableFree Where Title='" + title + "' and (SiteID=" + siteID + "or IsShare=1)" + rowFilter + "");
            strSql.Append(" union ");
            strSql.Append(" select COUNT(*) con From K_T_Lable Where Title='" + title + "' and (SiteID=" + siteID + "or IsShare=1)" + rowFilter + ")g");


            object obj = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
            return Convert.ToInt32(obj) > 0;
        }

        public string LableEnable(int isEnable, int lableid, int tableType)
        {
            try
            {
                string sqlStr = string.Empty;
                if (tableType == 10)
                {

                    sqlStr = "update K_T_LableFree set IsEnable=" + isEnable + " Where LableID=" + lableid;
                }
                else
                {
                    sqlStr = "update K_T_Lable set IsEnable=" + isEnable + " Where LableID=" + lableid;
                }
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
