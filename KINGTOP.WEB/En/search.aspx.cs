using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En
{
    public partial class search : System.Web.UI.Page
    {
        public int sum = 0;
        public string key = string.Empty;
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            key = Request.QueryString["key"];
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
            //DataTable dt = SQLHelper.GetDataSet("select * from (select ID,nodecode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_News union all select ID,nodecode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_CategoryGuide union all select ID,NodeCode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_FoodGuide) as T where T.IsDel = 0 and T.FlowState =99 " + shwhere + "");
            DataTable dt = SQLHelper.GetDataSet("  select * from (select ID,nodecode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_News union all select ID,nodecode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_CategoryGuide union all select ID,NodeCode,Title,AddDate,AddMan as type,IsDel,FlowState,orders from K_U_FoodGuide) as T where T.IsDel = 0 and T.FlowState =99  and 1=1  and NodeCode in('104004002','104002002','104003002') and Title like '%"+key+"%'");
            if (dt != null && dt.Rows.Count > 0)
            {
                rptlist.DataSource = dt;
                rptlist.DataBind();
                sum = dt.Rows.Count;
            }
        }
        public string GetString(string str, string keys)
        {
            string result = string.Empty;
            if (str.Trim() != "" && keys.Trim() != "")
            {
                result = str.Replace(keys, "<i>" + keys + "</i>");
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
                if (nodecode2 == "104002002")//产品
                {
                    result = "/En/shopping/detail.aspx?nid=" + id;
                }
                if (nodecode2 == "104003002")//美食
                {
                    result = "/En/cate/detail.aspx?nid=" + id;
                }
                if (nodecode3 == "104004002")//活动
                {
                    result = "/En/activity/detail.aspx?nid=" + id;
                }
            }
            return result;
        }
        #endregion
    }
}