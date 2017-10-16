using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.SysAdmin.Editor
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Response.Write("txtContent1="+txtContent.Text);
            Response.Write("<br> <br> <br>txtContent1="+txtContent2.Text);
        }
    }
}
