using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.aid
{
    public partial class search : System.Web.UI.Page
    {
        public int sum = 0;
        public string key = string.Empty;
        public string lis = string.Empty;
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params.AllKeys.Contains("key"))
            {
                key = Request.QueryString["key"];
            }

            if (!this.IsPostBack)
            {
                BindMenu(key);
            }
        }
        #endregion

        #region 绑定菜单
        /// <summary>
        /// 绑定菜单
        /// </summary>
        protected void BindMenu(string key)
        {
            string shwhere = " and 1=1 ";
            shwhere += " and Title like '%" + key + "%'";
          
            DataTable dt = SQLHelper.GetDataSet("  select * from (select ID,nodecode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_News union all select ID,nodecode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_CategoryGuide union all select ID,NodeCode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_FoodGuide) as T where T.IsDel = 0 and T.FlowState =99  and 1=1  and NodeCode in('101004002','101002002','101003002') and Title like '%" + key + "%'");

            if (dt != null && dt.Rows.Count > 0)
            {
               
                sum = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    lis += "  <li><a href=\"" + GetLink(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["NodeCode"].ToString()) + "\">" + GetString(dt.Rows[i]["Title"].ToString(), key) + "</a></li>";
                }
            }
        }
        public string GetString(string str, string keys)
        {
            string result = string.Empty;
            if (str.Trim() != "" && keys.Trim() != "")
            {
                result = str.Replace(keys, "<span>" + keys + "</span>");
            }

            return result;
        }
        public string GetLink(string id, string node)
        {
            string result = string.Empty;
            string nodecode1 = KingTop.Common.Utils.GetSubString(node, 6, "");
            string nodecode2 = KingTop.Common.Utils.GetSubString(node, 9, "");
            string nodecode3 = KingTop.Common.Utils.GetSubString(node, 12, "");
            if (id != "" && node != "")
            {
                if (nodecode2 == "101002002")//产品
                {
                    result = "/Phone/shopping/detail.aspx?id=" + id;
                }
                if (nodecode2 == "101003002")//美食
                {
                    result = "/Phone/cate/detail.aspx?id=" + id;
                }
                if (nodecode3 == "101004002")//活动
                {
                    result = "/Phone/activity/activity.aspx?id=" + id;
                }
            }
            return result;
        }
        #endregion
    }
}