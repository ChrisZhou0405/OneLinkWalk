using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Reflection;
using KingTop.Common;

namespace KingTop.Modules.BLL
{
    public class Categorys
    {


        public DataTable SetCategoryCache()
        {
            DataTable dt = null;
            AppCache.Remove("CATEGORYCACHE");
            string sSql = string.Format("select * from K_Category where IsDel=0");
            dt = InfoHelper.ExecuteSQL(sSql);
            if (dt != null && dt.Rows.Count > 0)
                AppCache.Add("CATEGORYCACHE", dt);
            return dt;
        }

        public DataTable GetCategoryCache()
        {
            DataTable dt = null;
            if (KingTop.Common.AppCache.IsExist("CATEGORYCACHE"))
            {
                dt = (DataTable)KingTop.Common.AppCache.Get("CATEGORYCACHE");
            }
            if (dt == null || dt.Rows.Count == 0)
                dt = SetCategoryCache();
            return dt;
        }


        Dictionary<string, string> dic = new Dictionary<string, string>();


        /// <summary>
        /// 根椐父ID以及NodeCode 取得所有子分类
        /// </summary>
        /// <param name="sParentID"></param>
        /// <param name="NodeCode"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetHashTable(string sParentID, string NodeCode)
        {


            DataTable dt = new DataTable();
            dt = GetCategoryCache();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("ParentID=" + sParentID + " and NodeCode='" + NodeCode + "'", " ParentID asc");
                for (int i = 0; i < dr.Length; i++)
                {
                    dic.Add(dr[i]["ID"].ToString(), GetDepthStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) + dr[i]["Name"].ToString());
                    GetHashTable(dr[i]["ID"].ToString(), NodeCode);
                }
            }
            return dic;
        }





        protected string GetDepthStr(int depth)
        {
            string sStr = string.Empty;
            for (int i = 0; i < depth; i++)
            {
                if (i == 0)
                    sStr = "┣";
                else
                    sStr += " . ┅ ";
            }
            return sStr;
        }


    }
}
