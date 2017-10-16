using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KingTop.Common;
using KingTop.Config;
using KingTop.Web.Admin;
using System.Collections.Generic;

namespace KingTop.WEB.SysAdmin.Category
{
    public partial class List : AdminPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx?Action=NEW&NodeCode=" + KingTop.Common.Utils.CheckSql(Request["NodeCode"]));
        }
        
    }
}
