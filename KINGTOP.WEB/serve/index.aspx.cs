﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.serve
{
    public partial class index : System.Web.UI.Page
    {
        public string sx = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "101006";
            sx=Request.QueryString["sx"];
        }
    }
}