using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingTop.WEB.Phone.cate
{
   static public class common
    {
     public  static string currentStr()
       {
           if (HttpContext.Current.Request.Params.AllKeys.Contains("id"))
           {
             string id=  HttpContext.Current.Request.Params["id"].ToString();
              if(id=="")
              {
                  return "<img src=\"/Phone/images/cicon1.png\" width=\"28\">全部美食";
              }
              else if (id == "100000000144283")
              {
                  return "<img src=\"/Phone/images/cicon2.png\" width=\"28\"> 亚洲美食</a>";
              }
              else if (id == "100000000825845")
              {
                  return "<img src=\"/Phone/images/cicon3.png\" width=\"28\"> 中式佳肴</a>";
              }
              else if (id == "100000001534222")
              {
                  return "<img src=\"/Phone/images/cicon4.png\" width=\"28\">  西方美馔</a>";
              }
              else if (id == "100000002274564")
              {
                  return "<img src=\"/Phone/images/cicon5.png\" width=\"28\">  轻便美食/甜点</a>";
              }

           }
           return "<img src=\"/Phone/images/cicon1.png\" width=\"28\">全部美食";
       }
    }
}