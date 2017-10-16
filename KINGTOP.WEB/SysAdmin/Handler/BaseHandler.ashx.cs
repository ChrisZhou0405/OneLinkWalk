using KingTop.Common;
using KingTop.Web.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace KingTop.WEB.Admin
{
    /// <summary>
    /// BaseHandler 的摘要说明
    /// </summary>
    public abstract class BaseHandler : IHttpHandler, IRequiresSessionState 
    {
        //设置变量
        private string _nodeid;

        //节点编码
        public virtual string NodeCode
        {
            get
            {
                return HttpContext.Current.Request.QueryString["NodeCode"];
            }
        }

        public string NodeID
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Nodeid"]))
                {
                    this._nodeid = Utils.ReqUrlParameter("Nodeid").ToUpper();
                }
                else
                {
                    this._nodeid = GetNodeId();
                }
                return this._nodeid;
            }
        }

        public abstract void ProcessRequest(HttpContext context);

        #region 根据操作编码查找用户是否存在此权限
        public bool IsHaveRightByOperCode(string OperType)
        {
            KingTop.BLL.SysManage.RightTool RightToo = new KingTop.BLL.SysManage.RightTool();
            return RightToo.IsHaveRightByOperCode(NodeID, OperType, GetLoginAccountId());
        }
        #endregion

        #region  得到登录信息  @author 陈顺 by 2010-03-24
        /// <summary>
        /// 得到当前登录的用户账号的主键（表Account中的主键）
        /// </summary>
        /// <returns></returns>
        protected string GetLoginAccountId()
        {
            return LoginInfoHelper.GetLoginAccountId(HttpContext.Current.Session, HttpContext.Current.Response);
        }

        /// <summary>
        /// 得到当前登录的用户账号的账号名（表Account中的AccountName）
        /// </summary>
        /// <returns></returns>
        protected string GetLoginAccountName()
        {
            return LoginInfoHelper.GetLoginAccount(HttpContext.Current.Session, HttpContext.Current.Response).UserName;
        }

        /// <summary>
        /// 得到当前登录的用户账号的用户组（表Account中的UserGroupCode）
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUserGroupCode()
        {
            return LoginInfoHelper.GetLoginAccount(HttpContext.Current.Session, HttpContext.Current.Response).UserGroupCode;
        }

        #endregion

        //通过NodeCode和SiteID获取Nodeid
        private string GetNodeId()
        {
            string strResult = string.Empty;
            KingTop.BLL.SysManage.ModuleNode bll = new KingTop.BLL.SysManage.ModuleNode();
            DataTable dt = bll.GetModeNodeFromCache();
            DataRow[] dr = dt.Select("NodeCode='" + NodeCode + "' and webSiteID=" + SiteID);
            if (dr.Length > 0)
            {
                strResult = dr[0]["NodeID"].ToString();
            }
            return strResult;
        }

        //站点的ID
        public int SiteID
        {
            get
            {
                if (HttpContext.Current.Session["SiteID"] != null)
                {
                    return Utils.ParseInt(HttpContext.Current.Session["SiteID"].ToString(), 0);
                }
                else
                {
                    HttpContext.Current.Session.Abandon();
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["SiteID"] = value;

                if (System.Web.HttpContext.Current.Request.Cookies["AdminUserInfo"] != null)
                {
                    string cookieValue = System.Web.HttpContext.Current.Request.Cookies["AdminUserInfo"].Value;
                    string[] arr = cookieValue.Split('|');
                    System.Web.HttpContext.Current.Response.Cookies["AdminUserInfo"].Value = arr[0] + "|" + value;
                }
            }
        }

        #region 辅助方法
        //输出Json信息
        public void WritMesg(string msg)
        {
            HttpContext.Current.Response.Write(msg);
            HttpContext.Current.Response.End();
        }

        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}