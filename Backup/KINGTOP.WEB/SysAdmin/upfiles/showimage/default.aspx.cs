using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

namespace KingTop.WEB.SysAdmin.upfiles.showimage
{
    public partial class default2 : System.Web.UI.Page
    {
        public string imgList = string.Empty;
        public string dt=string.Empty ;
        public string sltlist = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             <li class="current" url="images/photo01.jpg">
        <div class="img"><img src="images/thumb01.jpg" _fcksavedurl="images/thumb01.jpg" alt=""/></div>
        <div class="detail">
          <h2>Great Wall</h2>
          <div class="poster"><span>Photo by</span> CNwander</div>
          <div class="num">01</div>
        </div>
      </li>
             */


            string v = Request.QueryString["v"];
            string controlType = Request.QueryString["ControlType"];
            string UploadFileDir = Request.QueryString["UploadFileDir"];
            string num = string.Empty;
            string imgName = string.Empty;
            if (!string.IsNullOrEmpty(v))
            {
                string[] list = v.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    if (i < 10)
                        num = "0" + i.ToString();
                    else
                        num = i.ToString();

                    if (i == 0)
                    {
                        imgList += "<li class=\"current\" url=\"" + UploadFileDir + list[i] + "\">";
                        dt = "<img src=\"" + UploadFileDir + list[i] + "\"width=\"650\" border=\"0\" id=\"main_img\" rel=\"" + UploadFileDir + list[i] + "\"/>";
                    }
                    else
                        imgList += "<li url=\"" + UploadFileDir + list[i] + "\">";


                    if (list[i].IndexOf("/") == -1)
                    {
                        imgName = list[i];
                    }
                    else
                    {
                        string[] im = list[i].Split('/');
                        imgName = im[im.Length - 1];
                    }
                    if (imgName.Length > 14)
                    {
                        imgName = imgName.Substring(0, 14) + "<br>" + imgName.Substring(14);
                    }
                    else
                    {
                        imgName += "<br>&nbsp;";
                    }
                    imgList += "<div class=\"img\"><img src=\"" + UploadFileDir + list[i] + "\" _fcksavedurl=\"" + UploadFileDir + list[i] + "\" height=60 width=120 alt=\"\"/></div>";
                    imgList += "<div class=\"detail\">";
                    imgList += "<h2>" + imgName + "</h2>";
                    imgList += "<div class=\"num\">" + num + "</div>";
                    imgList += "</div>";
                    imgList += "</li>";

                    sltlist +="<img src=\"" + UploadFileDir + list[i] + "\" alt=\"" + imgName + "\" width=\"100\" height=\"80\" border=\"0\" class=\"thumb_img\" rel=\"" + UploadFileDir + list[i] + "\" />";

                }
            }
        }
    }
}
