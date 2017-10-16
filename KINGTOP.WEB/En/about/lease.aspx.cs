using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.about
{
    public partial class lease : System.Web.UI.Page
    {
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "104007004";
            if (!Page.IsPostBack)
            {
                //绑定场地与商铺租赁
                BindProject();
            }
        }
        #endregion

        #region 2.0绑定场地与商铺租赁
        /// <summary>
        /// 绑定场地与商铺租赁
        /// </summary>
        private void BindProject()
        {
            string sql = string.Empty;
            sql = "select Title,RetalIntro,BigImg,listimage from K_U_Rental where NodeCode='104007004' and IsDel = 0 and FlowState = 99 order by orders asc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                rptmonth.DataSource = dt;
                rptmonth.DataBind();
                rptontent.DataSource = dt;
                rptontent.DataBind();
            }
        }
        #endregion

        #region 3.0绑定多图
        /// <summary>
        /// 图片列表数据
        /// </summary>
        /// <param name="NodeCode"></param>
        public string GetListIMG(string listimg)
        {
            string result = string.Empty;
            string stylePd = string.Empty;
            string[] list = listimg.Replace("$$$", ",").Split(',');
            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    if (i == 2)
                    {
                        stylePd = "class=\"normg\"";
                    }
                    result += "<li " + stylePd + "><img src='/uploadfiles/images/" + list[i] + "' width='386' height='277'/></li>";
                }
            }
            return result;
        }
        #endregion
    }
}