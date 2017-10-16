using System;
using System.Data;

namespace KingTop.WEB
{
    public partial class errorpage : System.Web.UI.Page
    {
        private DataTable dt2;
        protected void Page_Load(object sender, EventArgs e)
        {

            string publishtype = KingTop.WEB.BasePage.PublishType;

            DataTable dt = new DataTable();
            if (KingTop.Common.AppCache.IsExist("PublishNodeCache"))
            {
                dt = (DataTable)KingTop.Common.AppCache.Get("PublishNodeCache");
            }
            else
            {
                KingTop.BLL.SysManage.ModuleNode bll = new KingTop.BLL.SysManage.ModuleNode();
                dt = bll.Publish_GetNodeFromCache();
            }

            dt2 = dt.Copy();  //将缓存的table复制到新table中，否则以下合并列时会影响缓存table

            if (publishtype == "0")  //如果是动态，则将datatable中的subdomain复制到linkUrl
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string url = dt.Rows[i]["subdomain"].ToString();
                    if (url.IndexOf("?") == -1)
                    {
                        url = url + "?nc=" + dt.Rows[i]["NodeCode"].ToString();
                    }
                    else if (url.IndexOf("nc=") == -1)
                    {
                        url = url + "&nc=" + dt.Rows[i]["NodeCode"].ToString();
                    }
                    dt2.Rows[i]["linkUrl"] = url;
                }
            }

            DataRow[] dr = dt2.Select("ParentNode='101'", " NodelOrder ASC");
            Repeater1.DataSource = dr;
            Repeater1.DataBind();
        }
    }
}
