
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@360hqb.com
// 创建日期：2010-03-10
// 功能描述：通用数据库操作（增、删、改、查）类

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL
{
    public class Public
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private static IDAL.IPublic dal = (IDAL.IPublic)Assembly.Load(path).CreateInstance(path + ".Public");

        #region 得到表排序号
        /// <summary>
        /// 根据传入的参数查询K_Source,返回查询结果
        /// </summary>
        /// <param name="tblName">表名称</param>
        /// <returns>返回排序序号</returns>
        public static int Orders(string tblName)
        {
            return dal.Orders(tblName);
        }
        #endregion

        #region 生成表主键 ycl@360hqb.com
        /// <summary>
        /// 生成表主键（ID），未知表的最大排序号时使用此方法,返回数组，GetTableID[0]=ID，GetTableID[1]=排序号
        /// </summary>
        /// <param name="isTop">是否置顶，0=否，1=是</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static string[] GetTableID(string isTop, string tableName)
        {
            string[] strNo = { "0", "0" };  //序列号
            string maxOrders = "";  //最大排序号码
            string strRandom = "";  //5位随机数 

            if (isTop != "1" && isTop != "0") //判断isTop是否是0和1，如果两者都不是将isTop置为0
            {
                isTop = "0";
            }

            maxOrders = Orders(tableName).ToString();

           // System.Random a = new Random(int.Parse(maxOrders));
            //strRandom = a.Next(10000, 100000).ToString();
            Random rdm1 = new Random(unchecked((int)DateTime.Now.Ticks));
            strRandom = (DateTime.Now.ToString("fffff").Replace("0", "") + rdm1.Next(10000, 99999).ToString()).Substring(0, 5);
            strNo[0] = (int.Parse (isTop)+1).ToString () + maxOrders.ToString().PadLeft(9, '0') + strRandom;
            strNo[1] = maxOrders;
            return strNo;
        }

        /// <summary>
        /// 生成表主键（ID），已知最大排序号
        /// </summary>
        /// <param name="isTop">是否置顶，0=否，1=是</param>
        /// <param name="maxOrders">排序号</param>
        /// <returns></returns>
        public static string GetTableID(string isTop, int maxOrders)
        {
            string strNo = "";  //序列号
            string strRandom = "";  //5位随机数 

            if (isTop != "1" && isTop != "0") //判断isTop是否是0和1，如果两者都不是将isTop置为0
            {
                isTop = "0";
            }

            //System.Random a = new Random(maxOrders);
            //strRandom = a.Next(10000, 100000).ToString();
            Random rdm1 = new Random(unchecked((int)DateTime.Now.Ticks));
            strRandom = (DateTime.Now.ToString("fffff").Replace("0", "") + rdm1.Next(10000, 99999).ToString()).Substring(0, 5);
            strNo = (int.Parse(isTop) + 1).ToString() + maxOrders.ToString().PadLeft(9, '0') + strRandom;
            return strNo;
        }
        #endregion


    }
}
