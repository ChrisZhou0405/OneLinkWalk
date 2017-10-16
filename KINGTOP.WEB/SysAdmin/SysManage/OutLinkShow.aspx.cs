using System;
using System.Data;
using System.Web.UI;

namespace KingTop.Web.Admin
{
    public partial class OutLinkShow : AdminPage
    {
        public string OutUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                KingTop.BLL.SysManage.ModuleNode nodeObj = new KingTop.BLL.SysManage.ModuleNode();
                DataTable dt = nodeObj.GetList("LISTBYNODECODE", KingTop.Common.Utils.getOneParams(NodeCode));
                if (dt.Rows.Count > 0)
                {
                    OutUrl = dt.Rows[0]["LinkUrl"].ToString();
                }
            }
        }
    }
}
