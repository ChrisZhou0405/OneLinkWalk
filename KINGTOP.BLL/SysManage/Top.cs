#region 引用程序集
using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.IO;

using KingTop.Model;
using KingTop.IDAL;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-06-18
// 功能描述：top.aspx中显示公告

// 更新日期        更新人      更新原因/内容
//201008027         胡志瑶       1.GetMessage方法加上一个长度参数 2.加GetDataBaseInfo方法
===========================================================================*/
#endregion

namespace KingTop.BLL.SysManage
{
        public class Top
        {
            private static string path = ConfigurationManager.AppSettings["WebDAL"];

            private IDAL.SysManage.ITop dal = (IDAL.SysManage.ITop)Assembly.Load(path).CreateInstance(path + ".SysManage.Top");


            public DataTable  GetMessage(int count,string siteID,string userName,string userid)
            {
                return dal.GetMessage(count,siteID, userName, userid);
            }
            public List<string> GetDataBaseInfo()
            {
                return dal.GetDataBaseInfo();
            }
            /// <summary>
            /// 获得我发表的信息
            /// </summary>
            /// <returns></returns>
            public DataTable GetMeInfo(string userNo,int siteID)
            {
                Module model = new Module();              
                DataTable newDt = new DataTable();  //分4列显示
                newDt.Columns.Add("ModuleName");
                newDt.Columns.Add("Count");
                newDt.Columns.Add("ModuleName2");
                newDt.Columns.Add("Count2");
                DataTable dt = model.GetList("GetFirstModule", KingTop.Common.Utils.getOneNumParams(siteID));  //模型列表          
                int m = 0;
                StringBuilder strSql = new StringBuilder();

                int rows = dt.Rows.Count / 2 + dt.Rows.Count % 2;
                for (int i = 0; i < rows; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataRow newRow = newDt.NewRow();
                    newRow["ModuleName"] = dr["ModuleName"].ToString().Replace("模型", "");

                    strSql.Append("select count(id) from " + dr["TableName"] + " where addman='" + userNo + "';");
                    newDt.Rows.Add(newRow);
                }
                for (int j = rows; j < dt.Rows.Count; j++)
                {
                    DataRow dr = dt.Rows[j];
                    newDt.Rows[m]["ModuleName2"] = dr["ModuleName"].ToString().Replace("模型", "");
                    strSql.Append("select count(id) from " + dr["TableName"] + " where addman='" + userNo + "';");
                    m++;
                   
                }
                
               
                DataSet ds = KingTop.Common.SQLHelper.ExecuteDataSet(KingTop.Common.SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null);
                for (int i = 0; i < rows; i++)
                {
                    newDt.Rows[i]["Count"] = ds.Tables[i].Rows[0][0];
                }
                m = 0;
                for (int j = rows; j < dt.Rows.Count; j++)
                {
                    newDt.Rows[m]["Count2"] = ds.Tables[j].Rows[0][0];
                    m++;
                }
                return newDt;
            }


            public DataTable GetPendingAudi(KingTop.Model.SelectParams modelPrams) //获得待审核的信息
            {
                DataTable dt = dal.GetPendingAudi(modelPrams);
                int m = 0;
                DataTable newDt = new DataTable();  //分4列显示
                newDt.Columns.Add("ModelName");
                newDt.Columns.Add("Counter");
                newDt.Columns.Add("ModelName2");
                newDt.Columns.Add("Counter2");
                DataRow[] dr1 = dt.Select("ModelName<>''");
                int rows = dr1.Length / 2 + dr1.Length % 2;
                for (int i = 0; i < rows; i++)
                {
                    DataRow newRow = newDt.NewRow();
                    newRow["ModelName"] = dr1[i]["ModelName"].ToString().Replace("模型", "");
                    newRow["Counter"] = dr1[i]["Counter"].ToString();
                    newDt.Rows.Add(newRow);
                }
                m = 0;
                for (int j = rows; j < dr1.Length ; j++)
                {
                    newDt.Rows[m]["ModelName2"] = dr1[j]["ModelName"].ToString().Replace("模型", "");
                    newDt.Rows[m]["Counter2"] = dr1[j]["Counter"].ToString();
                    m++;

                }

                return newDt;
            }
        }
}
