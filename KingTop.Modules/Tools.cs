using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.Common;

namespace KingTop.Modules
{
    public static class Tools
    {
        #region 添加参数
        /// <summary>
        /// 添加单个字符型参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static SelectParams getOneParams(string strValue)
        {
            SelectParams param = new SelectParams();
            param.S1 = strValue;
            return param;
        }

        /// <summary>
        /// 添加单个数字型参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static SelectParams getOneNumParams(int num)
        {
            SelectParams param = new SelectParams();
            param.I1 = num;
            return param;
        }

        /// <summary>
        /// 添加两个参数
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static SelectParams getTwoParams(string strOneValue, string strTwoValue)
        {
            SelectParams param = new SelectParams();
            param.S1 = strOneValue;
            param.S2 = strTwoValue;
            return param;
        }
        #endregion

        #region 得到表排序号
        /// <summary>
        /// 根据传入的参数查询K_Source,返回查询结果
        /// </summary>
        /// <param Name="tblName">表名称</param>
        /// <returns>返回排序序号</returns>
        public static int Orders(string tblName)
        {
            int orders = 1;
            string strSql = "SELECT MAX(Orders) FROM " + tblName;
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            if (dr.Read())
            {
                if (dr[0].ToString() == "")
                {
                    orders = 1;
                }
                else
                {
                    orders = int.Parse(dr[0].ToString()) + 7;
                }
            }
            dr.Close();
            dr.Dispose();
            return orders;
        }
        #endregion
    }
}
