using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Web.Admin;
using KingTop.BLL.Template;
using KingTop.Common;

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class AspxToHtml : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KingTop.BLL.Template.Publish publish;

            if (!Page.IsPostBack)
            {
                publish = new Publish();

                publish.DropDownDataBind(lstbMenu, 1, SiteID);
            }
        }

        #region 发布
        protected void btnCreatePub_Click(object sender, EventArgs e)
        {
            KingTop.WEB.SysAdmin.SysManage.AspxToHtml_Publish publish;
            List<string> lstMenu;                           // 要发布的栏目，Count=0时全部发布
            string[] typeParam;
            string menuNodeCode;                           // 要发布的栏目ID，多个用逗号隔开
            string[] arrMenuNodeCode;
            lstMenu = new List<string>();
            publish = new AspxToHtml_Publish();

            typeParam = null;
            menuNodeCode = Request.Form["lstbMenu"];
            publish.IsIndex = cbtnIndex.Checked;           // 站点首页
            publish.IsMenuIndex = cbtnClassIndex.Checked;      // 栏目首页
            publish.IsMenuList = cbtnClassList.Checked;        // 栏目列表
            publish.IsContent = cbtnClassDetail.Checked;       // 内容页
            publish.UnPublished = chkUnPublished.Checked;      // 只生成未生成页面
            publish.siteID = SiteID;
            publish.siteDir = SiteDir;
            publish.NodeCode = NodeCode;

            if (rbtnCreate2.Checked)    //  按ID生成
            {
                publish.PublishType = 1;
                typeParam = new string[] { txtDetailId.Text.Trim() }; //内容ID 多个ID可由 , 分隔
            }
            else if (rbtnCreate4.Checked) //按更新时间段生成
            {
                typeParam = new string[2];
                publish.PublishType = 2;
                typeParam[0] = txtStartTime.Text.Trim();  // 起始时间
                typeParam[1] = txtEndTime.Text.Trim();    // 结束时间
            }

            if (!string.IsNullOrEmpty(menuNodeCode) && menuNodeCode.Trim() != "")  //如果选择了栏目
            {
                arrMenuNodeCode = menuNodeCode.Split(new char[] { ',' });

                foreach (string nodeCode in arrMenuNodeCode)
                {
                    if (nodeCode.Trim() != "")
                    {
                        lstMenu.Add(nodeCode);
                    }
                }
            }
            else  //没有选择，则全部发布
            {
                for(int k=0;k<lstbMenu.Items.Count ;k++)
                {
                    if (!string.IsNullOrEmpty(lstbMenu.Items[k].Value))
                    {
                        lstMenu.Add(lstbMenu.Items[k].Value);
                    }
                }
            }

            publish.TypeParam = typeParam;
            publish.ListMenu = lstMenu;

            try
            {
                mainid.Visible = false;
                publish.Execute();      // 执行发布
            }
            catch {
                mainid.Visible = true;
            }
        }
        #endregion
    }
}
