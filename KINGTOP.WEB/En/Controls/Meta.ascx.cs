using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.Controls
{
    public partial class Meta : System.Web.UI.UserControl
    {
        public string NodeCode = string.Empty;
        public BasePage MyPage = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            if (!string.IsNullOrEmpty(NodeCode))
            {
                MyPage.GetPageHeadInfo(Utils.GetSubString(NodeCode, 9, ""));
                return;
            }
            NodeCode = KingTop.Common.Utils.CheckSql(Request.QueryString["Node"]);
            if (string.IsNullOrEmpty(NodeCode))
            {
                MyPage.GetPageHeadInfo();
            }
            else
            {
                MyPage.GetPageHeadInfo(Utils.GetSubString(NodeCode, 9, ""));
            }
        }
    }
}