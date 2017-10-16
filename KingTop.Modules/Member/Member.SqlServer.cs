

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.Common;
using System.Text.RegularExpressions;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：严辉 2010-04-28
// 功能描述：对K_Member表的的操作


// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Modules.SqlServer
{
    public class Member 
    {
        #region 根据传入的参数查询K_Member,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_Member,返回查询结果
        /// </summary>
        /// <param Name="tranType">操作类型</param>
        /// <param Name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Modules.SelectParams paramsModel)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tranType",tranType),
                    new SqlParameter("I1",paramsModel.I1),
                    new SqlParameter("I2",paramsModel.I2),
                    new SqlParameter("S1",paramsModel.S1),
                    new SqlParameter("S2",paramsModel.S2)
                };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_K_MemberSel", prams).Tables[0];
        }
        #endregion

        #region 登录操作
        /// <summary>
        /// 作者：涂芳 2010-10-19
        /// 功能：传入参数进行查询比较，返回查询结果
        /// </summary>
        /// <param name="usn">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>member</returns>
        public bool GetUsnAndPwd(KingTop.Modules.Entity.Member member) 
        {
         //   KingTop.Model.Member.Member member = null;
            bool flag=false;
            string sql = "select memberid,username,password,siteID from k_member where UserName=@username and Password=@password";
            SqlParameter[] para = new SqlParameter[]{
                 new SqlParameter("@username", member.UserName),
                 new SqlParameter("@password",member.Password)
                };
            SqlDataReader reader = SQLHelper.GetReader(sql,para);
            if (reader.Read())
            {
                flag = true;
            }
            else {
                flag = false;
            }
          
             return flag;
        }
        #endregion

        #region 添加参数
        /// <summary>
        /// 作者：涂芳 2010-10-19
        /// 功能：添加参数
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int RegisterUser(KingTop.Modules.Entity.Member member)
        {
            //RegexOptions options = (RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            int flag = 0;
            Regex r = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"); //emial
            //Regex strName = new Regex("^[a-zA-Z]{1}([a-zA-Z0-9]|[-_]){3,15}$");   //只能输入4-16个以字母开头、可带数字、“_”、“-”的字串
            //Regex strPwd = new Regex("^(\\w){6,16}$");         //长度在6-16位
            
            if (r.IsMatch(member.Email))
            {
                string sql = "proc_K_MemberAdd";
                SqlParameter[] para = new SqlParameter[] 
            {
                new SqlParameter("@MemberID",member.MemberID),
                new SqlParameter("@UserName",member.UserName),
                new SqlParameter("@Password",member.Password),
                new SqlParameter("@Email",member.Email)
            };
                flag = SQLHelper.ExecuteCommand(sql, CommandType.StoredProcedure, para);

            }
           else
            {
                flag = 0;

            }
            return flag;

        }
        #endregion

        #region 查询注册时 用户名是否已存在
        /// <summary>
        /// 作者：涂芳 2010-10-20
        /// 功能：根据用户名查找ID进行客户端的判断 
        /// </summary>
        /// <param name="usn"></param>
        /// <returns>对象</returns>
        public KingTop.Modules.Entity.Member GetEmailAndUsnByMID(string usn) 
        {
            KingTop.Modules.Entity.Member member = new KingTop.Modules.Entity.Member();
            string sql = "select MemberID,UserName from K_Member where UserName='"+usn+"'";
            DataTable table = SQLHelper.GetDataSet(sql);
             foreach (DataRow row in table.Rows)
             {
              
                 member.MemberID = new Guid(row["MemberID"].ToString()).ToString();
                 member.UserName = (string)row["UserName"];
             }
             return member;

        }
        #endregion

        #region 根据用户名查找ID进行客户端的判断   返回bool类型
        /// <summary>
        ///作者：涂芳 2010-10-22
        /// 功能：根据用户名查找ID进行客户端的判断   返回bool类型
        /// </summary>
        /// <param name="usn"></param>
        /// <returns></returns>
        public bool GetUsnByMID(string usn)
        {
            string sql = "select MemberID,UserName from K_Member where UserName=@usn";
            SqlParameter para = new SqlParameter("@usn", usn);
            SqlDataReader reader = SQLHelper.GetReader(sql, para);
            bool isExists = false;
            if (reader.Read())
            {
                isExists = true;
            }
            reader.Close();

            return isExists;
        }
        #endregion

        #region 查询激活码是否存在
        /// <summary>
        /// 查询激活码是否存在
        /// 作者：涂芳 2010-11-05
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool GetMIDbyToken(string token)
        {
            string sql = "select MemberID from K_Member where Token=@token";
            SqlParameter para = new SqlParameter("@token", token);
            SqlDataReader reader = SQLHelper.GetReader(sql, para);
            bool isExists = false;
            if (reader.Read())
            {
                isExists = true;
            }
            reader.Close();

            return isExists;
        }
        #endregion

        #region 查询注册时 Email是否已经被注册
        /// <summary>
        /// 作者：涂芳 2010-10-20
        /// 功能：根据Email查找ID进行客户端的判断
        /// </summary>
        /// <param name="usn"></param>
        /// <returns></returns>
        public bool GetEmAndUsnByMID(string email)
        {
            string sql = "select MemberID,Email from K_Member where Email=@email";
            SqlParameter para = new SqlParameter("@email", email);
            SqlDataReader reader = SQLHelper.GetReader(sql, para);
            bool isExists = false;
            if (reader.Read())
            {
                isExists = true;
            }
            reader.Close();

            return isExists;
        }
        #endregion

        #region 根据激活码查询临时密码
        public KingTop.Modules.Entity.Member GetPwdByToken(string token)
        {
            KingTop.Modules.Entity.Member member = new KingTop.Modules.Entity.Member();
            string sql="select PwdVe from K_Member where Token='"+token+"'";
             SqlDataReader reader=SQLHelper.GetReader(sql);
            if(reader.Read())
            {
                if (reader["Pwdve"] != DBNull.Value)
                {
                    member.Pwdve = (string)reader["Pwdve"];
                }
            }
            return member;
        }
        #endregion 

        #region 根据邮箱和用户名查找ID
        /// <summary>
        /// 根据邮箱和用户名查找ID
        /// 作者： 涂芳 2010-11-02
        /// </summary>
        /// <param name="email"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool GetMemberIDByEmail(string email,string UserName)
        {
            string sql = "select MemberID from K_Member where Email=@email and UserName=@username";
            SqlParameter para = new SqlParameter("@email", email);
            SqlParameter para1 = new SqlParameter("@username", UserName);
            SqlDataReader reader = SQLHelper.GetReader(sql,para,para1);
            bool isExists = false;
            if (reader.Read())
            {
                isExists = true;
            }
            reader.Close();

            return isExists;
        }
        #endregion

        #region 设置或者删除K_Member记录
        /// <summary>
        /// 设置或者删除K_Member记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string MemberSet(string tranType, string setValue, string IDList)
        {

            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("trantype",tranType),
                    new SqlParameter("SetValue",setValue),
                    new SqlParameter("IDList",IDList)};


            return SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction,
                           CommandType.StoredProcedure, "proc_K_MemberSet", prams).ToString();

        }
        #endregion

        #region 增、改K_Member表

        /// <summary>
        /// 增、改K_Member表

        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Modules.Entity.Member paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_MemberSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("MemberID",paramsModel.MemberID),
                    new SqlParameter("UserName",paramsModel.UserName),
                    new SqlParameter("Password",paramsModel.Password),
                    new SqlParameter("Gender",paramsModel.Gender),
                    new SqlParameter("GroupID",paramsModel.GroupID),
                    new SqlParameter("ModleID",paramsModel.ModleID),
                    new SqlParameter("Email",paramsModel.Email),
                    new SqlParameter("Funds",paramsModel.Funds),
                    new SqlParameter("Point",paramsModel.Point),
                    new SqlParameter("Integral",paramsModel.Integral),
                    new SqlParameter("StateID",paramsModel.StateID),
                    new SqlParameter("IsCheck",paramsModel.IsCheck),                     
                    new SqlParameter("RegitIP",paramsModel.RegitIP),  
                    new SqlParameter("NodeCode",paramsModel.NodeCode),                     
                    new SqlParameter("SiteID",paramsModel.SiteID),  
                    new SqlParameter("token",paramsModel.Token ),  

                    returnValue
                 };

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                isOk = returnValue.Value.ToString();
            }
            catch (Exception ex)
            {
                isOk = ex.Message;

            }

            return isOk;
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
                new SqlParameter("@Id","MemberID"),
                new SqlParameter("@Table","K_Member"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*,(SELECT GroupName FROM K_MemberGroup WHERE ID=GroupID) AS GroupName"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","RegitDate DESC"),                
                new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        //得到查询条件
        string GetWhere(Dictionary<string, string> DicWhere)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(" 1=1 ");
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "UserName")
                    {
                        sbSql.Append(" and " + kvp.Key + " LIKE '%" + kvp.Value + "%'");
                    }
                    else
                    {
                        sbSql.Append(" and " + kvp.Key + " = '" + kvp.Value + "'");
                    }

                }
            }

            return sbSql.ToString();
        }
        #endregion
    }
}
