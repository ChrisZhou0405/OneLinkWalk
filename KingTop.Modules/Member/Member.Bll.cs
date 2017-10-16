using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.Common;
using System.Data.SqlClient;


/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：严辉 2010-04-28
// 功能描述：对K_Member表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.Modules.BLL
{
    public class Member
    {
        private Modules.SqlServer.Member dal = new Modules.SqlServer.Member();
        #region 根据传入的参数查询K_Member,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_Member,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Modules.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 登录操作
        /// <summary>
        /// 作者：涂芳 2010-10-19
        /// 功能：传入参数进行查询比较，返回查询结果
        /// </summary>
        /// <param name="usn">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool GetUsnAndPwd(KingTop.Modules.Entity.Member member)
        {
            return dal.GetUsnAndPwd(member);
        }
        #endregion

        #region 添加参数
        /// <summary>
        /// 作者：涂芳 2010-10-19
        /// 功能：添加参数
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool RegisterUser(KingTop.Modules.Entity.Member member)
        {
            if (dal.RegisterUser(member) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 查找该用户是否存在
        /// <summary>
        /// 作者：涂芳 2010-10-20
        /// 功能：查找该用户是否存在
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public KingTop.Modules.Entity.Member GetEmailAndUsnByMID(string usn)
        {
            return dal.GetEmailAndUsnByMID(usn);
        }
        #endregion

        #region 查找邮箱是否已注册
        /// <summary>
        /// 作者：涂芳 2010-10-20
        /// 功能：查找邮箱是否已注册
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool GetEmAndUsnByMID(string email)
        {
            return dal.GetEmAndUsnByMID(email);
        }

        #endregion

        #region 根据用户名和邮箱找回会员ID
        public bool GetMemberIDByEmail(string email, string UserName)
        {
            return dal.GetMemberIDByEmail(email, UserName);
        }
        #endregion

        #region 根据ID获取会员

        public bool GetUsnByMID(string usn)
        {
            return dal.GetUsnByMID(usn);
        }
        #endregion

        #region 增、改K_Member表

        /// <summary>
        /// 增、改K_Member表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="MemModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Modules.Entity.Member memModel)
        {
            return dal.Save(trantype, memModel);
        }
        #endregion

        #region 设置或者删除K_Member记录
        /// <summary>
        /// 设置或者删除K_Member记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string MemberSet(string tranType, string setValue, string IDList)
        {
            return dal.MemberSet(tranType, setValue, IDList);
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Modules.Pager pager)
        {
            dal.PageData(pager);
        }
        #endregion

        #region 前台会员操作
        /// <summary>
        /// 会员可见选项（仅好友/所有人）
        /// </summary>
        /// <returns></returns>
        public DataTable GetMeberVisibleOptions()
        {
            DataTable dtVisible = KingTop.Common.Utils.GetXmlDataSet("../config/Member/MemberVisible.xml").Tables[0];

            return dtVisible;
        }

        /// <summary>
        /// 根据ID生成实体类
        /// </summary>
        /// <param name="strMember"></param>
        /// <returns></returns>
        public KingTop.Modules.Entity.Member GetMember(string strMember)
        {
            return GetMember(GetList("one", KingTop.Modules.Tools.getOneParams(strMember)));
        }

        /// <summary>
        /// 根据ID生成实体类
        /// </summary>
        /// <param name="strMember"></param>
        /// <returns></returns>
        public KingTop.Modules.Entity.Member GetMember(DataTable dt)
        {
            KingTop.Modules.Entity.Member member = new KingTop.Modules.Entity.Member();
            if (dt.Rows.Count > 0)
            {
                member.MemberID = dt.Rows[0]["MemberID"].ToString();
                member.UserName = dt.Rows[0]["username"].ToString();
                member.Password = dt.Rows[0]["password"].ToString();
                member.HeadImg = dt.Rows[0]["HeadImg"].ToString();
                member.Email = dt.Rows[0]["Email"].ToString();
                //member.Gender = KingTop.Common.Utils.ParseInt(dt.Rows[0]["Gender"], 1);
                member.Gender = dt.Rows[0]["Gender"].ToString ();
                return member;
            }
            return null;
        }

        /// <summary>
        /// 获取会员主题  ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public string GetThemeID(string memberID)
        {
            string themeId = string.Empty;
            DataTable dt = GetList("THEME", Tools.getOneParams(memberID));
            if (dt != null && dt.Rows.Count > 0)
            {
                themeId = dt.Rows[0][0].ToString();
            }
            return themeId;
        }

        /// <summary>
        /// 获取会员头像 url
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public string GetHeadImgUrl(string memberID)
        {
            string imgUrl = string.Empty;
            DataTable dt = GetList("headImg", Tools.getOneParams(memberID));
            if (dt != null && dt.Rows.Count > 0)
            {
                imgUrl = dt.Rows[0][0].ToString();
            }
            return imgUrl;
        }

        /// <summary>
        /// 根据用户名取ID
        /// </summary>
        /// <param name="MemberName"></param>
        /// <returns></returns>
        public string GetMemberID(string MemberName)
        {

            return Convert.ToString(ExecuteScalar("GETID", Tools.getOneParams(MemberName)));
        }

        /// <summary>
        /// 验证用户名或邮箱是否已存在
        /// </summary>
        /// <param name="tranType">ISUSERNAME/用户名,ISEMAIL/邮箱</param>
        /// <param name="emailOrUserName"></param>
        /// <returns></returns>
        public string IsExisUserNameOrEamil(string tranType, string emailOrUserName)
        {
            return ExecuteScalar(tranType, Tools.getOneParams(emailOrUserName)).ToString();
        }

        /// <summary>
        /// 会员登录(0/帐号不存在，1/登录成功，2/用户名或密码错误)
        /// </summary>
        /// <param name="emailOrUserName">用户名或邮箱名</param>
        /// <param name="strPwd">密码(未加密)</param>
        /// <param name="isRember">是否记住登录状态</param>
        /// <returns>0/用户名或邮箱不存在，1/登录成功，2/用户名或密码错误</returns>
        //public string MemberLogin(string emailOrUserName, string strPwd, bool isRember)
        //{
        //    //登录的结果
        //    string result = string.Empty;

        //    //密码加密
        //    strPwd = Utils.getMD5(strPwd.ToLower());

        //    DataTable dt = GetList("EMAILOGIN", Utils.getTwoParams(emailOrUserName, strPwd));
        //    if (dt.Columns.Count == 1)
        //    {
        //        result = dt.Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        System.Web.HttpContext.Current.Session[SystemConst.HQB360] = GetMember(dt);
        //        Utils.WriteCookie(SystemConst.COOKIE_PAGE, SystemConst.HQB360, dt.Rows[0]["memberid"].ToString());  //写入前台会员cookie
        //        Business.BusinessInfo bllBusiness = new KingTop.BLL.Business.BusinessInfo();
        //        string memberID = dt.Rows[0]["MemberID"].ToString();
        //        Model.Business.BusinessInfo businessInfo = bllBusiness.GetModel(memberID);
        //        if (businessInfo != null)  //如果存在店铺,则保存Session
        //        {
        //            System.Web.HttpContext.Current.Session[SystemConst.BUSINESSINFO] = businessInfo;
        //            // Utils.WriteCookie(SystemConst.COOKIE_PAGE, SystemConst.BUSINESSINFO, businessInfo.ID);  //写入前台商家cookie
        //        }
        //        if (isRember)
        //        {
        //            System.Web.HttpContext.Current.Session.Timeout = DateTime.MaxValue.Minute;
        //        }
        //        else
        //        {
        //            System.Web.HttpContext.Current.Session.Timeout = 20;
        //        }
        //        UpdateLoginIPAndLoginTime(memberID);

        //        result = "1";
        //    }

        //    return result;
        //}

        /// <summary>
        /// 会员登录(0/帐号不存在，1/登录成功，2/用户名或密码错误)  by 何伟 2010-10-15
        /// </summary>
        /// <param name="emailOrUserName">用户名或邮箱名</param>
        /// <param name="strPwd">密码(未加密)</param>        
        /// <returns>0/用户名或邮箱不存在，1/登录成功，2/用户名或密码错误</returns>
        public string MemberLogin(string userName, string strPwd)
        {
            //密码加密
            strPwd = Utils.getMD5(strPwd.ToLower());
            string result = string.Empty;  //返回的结果
            string memberId = "";    //会员ID

            DataTable dt = GetList("USERNAMELOGIN", Tools.getTwoParams(userName, strPwd));
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["RESULT"].ToString();
                switch (result)
                {
                    case "0":
                    case "2":
                        break;
                    case "1":
                        memberId = dt.Rows[0]["MemberID"].ToString();
                        Utils.WriteCookie(SystemConst.COOKIE_USERNAME, userName);          //写入前台用户名
                        UpdateLoginIPAndLoginTime(dt.Rows[0]["MemberID"].ToString());      //记录登录时间
                        break;
                }
            }
            else
            {
                result = "0";
            }
            return result + "_" + memberId;
        }

        //更新登录IP 时间等
        private void UpdateLoginIPAndLoginTime(string memberId)
        {
            string ip = Utils.GetIP();
            MemberSet("LOGIN", ip, memberId);
        }

        /// <summary>
        /// 随机产生一个不是自己的会员ID
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public string GetRandomMid(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
            {
                return "";
            }
            return ExecuteScalar("RANDOM", Tools.getOneParams(memberId)).ToString();
        }

        private object ExecuteScalar(string tranType, KingTop.Modules.SelectParams paramsModel)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tranType",tranType),
                    new SqlParameter("I1",paramsModel.I1),
                    new SqlParameter("I2",paramsModel.I2),
                    new SqlParameter("S1",paramsModel.S1),
                    new SqlParameter("S2",paramsModel.S2)
                };

            return SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_K_MemberSel", prams);
        }
        #endregion

        

        #region 根据token查询临时密码
        public KingTop.Modules.Entity.Member GetPwdByToken(string token)
        {
            return dal.GetPwdByToken(token);
        }
        #endregion


        #region 修改用户信息
        public string UpdateUserInfo(Entity.Member paramsModel)
        {
            string reMsg = string.Empty;
            try
            {
                string cmdText = "UPDATE K_Member SET RealName=@RealName,Gender=@Gender,Birthday=@Birthday,Marriage=@Marriage,Mobile=@Mobile,Headimg=@Headimg,Intro=@Intro Where UserName=@UserName";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@UserName",paramsModel.UserName),
                    new SqlParameter("@RealName",paramsModel.RealName ),
                    new SqlParameter("@Gender",paramsModel.Gender),
                    new SqlParameter("@Birthday",Convert.ToString(paramsModel.Birthday)),
                    new SqlParameter("@Marriage",paramsModel.Marriage),
                    new SqlParameter("@Mobile",paramsModel.Mobile ),
                    new SqlParameter("@Headimg",paramsModel.HeadImg ),
                    new SqlParameter("@Intro",paramsModel.Intro ),
                 };

                reMsg=SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, paras).ToString ();
            }
            catch (Exception ex)
            {
                reMsg = ex.Message;

            }

            return reMsg;
        }
        #endregion


        #region 查询token是否存在
        public bool GetMIDbyToken(string token)
        {
            return dal.GetMIDbyToken(token);
        }
        #endregion
    }
}
