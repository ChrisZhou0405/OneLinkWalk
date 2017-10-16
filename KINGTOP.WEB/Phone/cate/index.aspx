<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.Phone.cate.index" %>

<%@ Register Src="~/Phone/controls/navbox.ascx" TagPrefix="uc1" TagName="navbox" %>
<%@ Register Src="~/Phone/controls/footer.ascx" TagPrefix="uc1" TagName="footer" %>



<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="320">
<meta name="format-detection" content="telephone=no" />
<link rel="stylesheet" type="text/css" href="/Phone/css/style.css">
<title></title>
</head>
<body>

    <uc1:navbox runat="server" ID="navbox" />


<section>
  <div class="mmain bgwhite">
     <div class="curr"><h3>美食荟萃</h3></div>
     
        <div class="navinput subnav subnav2">
           <span class="jiao"></span>
            <p class="set"><%=KingTop.WEB.Phone.cate.common.currentStr() %></p>
              <ul class="sublist new">
               <li><a href="/Phone/cate/index.aspx"><img src="/Phone/images/cicon1.png" width="28"> 全部美食</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000000144283"><img src="/Phone/images/cicon2.png" width="28"> 亚洲美食</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000000825845"><img src="/Phone/images/cicon3.png" width="28"> 中式佳肴</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000001534222"><img src="/Phone/images/cicon4.png" width="28"> 西方美馔</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000002274564"><img src="/Phone/images/cicon5.png" width="28"> 轻便美食/甜点</a></li>
              </ul>
    </div>
     
    <%=this.contStr %>
     
     
     
  </div>
 </section>

    <uc1:footer runat="server" ID="footer" />


<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>

 </body>
</html>
