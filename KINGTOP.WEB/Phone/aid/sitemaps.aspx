<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sitemaps.aspx.cs" Inherits="KingTop.WEB.Phone.aid.sitemaps" %>

<%@ Register Src="~/Phone/controls/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Phone/controls/navbox.ascx" TagPrefix="uc1" TagName="navbox" %>



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
     <div class="curr"><h3>网站地图</h3></div>    
     <div class="sitemaps">
         <ul class="sitemapslist">
             <li>
                <h3>购物指南</h3>
                <div class="site">
                    <a href="/Phone/shopping/index.aspx?id=100000000188449">服饰及鞋履</a>
                    <a href="/Phone/shopping/index.aspx?id=100000000896784">珠宝及配饰</a>
                    <a href="/Phone/shopping/index.aspx?id=100000001531614">生活及服务</a>
                    <a href="/Phone/shopping/index.aspx?id=100000002263254">美容及护理</a>
                    <a href="/Phone/shopping/index.aspx?id=100000002948178">童装玩具</a>
                </div>
             </li>
             <li>
                <h3>美食荟萃</h3>
                 <div class="site">
                    <a href="/Phone/cate/index.aspx?id=100000000144283">亚洲美食</a>
                    <a href="/Phone/cate/index.aspx?id=100000000825845">中式佳肴</a>
                    <a href="/Phone/cate/index.aspx?id=100000001534222">西方美馔</a>
                    <a href="/Phone/cate/index.aspx?id=100000002274564">轻便美食/甜点</a>
                </div>
             </li>
             <li>
                <h3>活动推介</h3>
                <div class="site">
                    <a href="/Phone/activity/index.aspx">最新活动</a>
                    <a href="/Phone/activity/activity.aspx">活动日志</a>

                </div>
             </li>
             <li>
                <h3>会员专区</h3>
                <div class="site">
                    <a href="/Phone/vip/vip.aspx">会员招募</a>
                   <%-- <a href="#">会员专属优惠</a>
                    <a href="#">积分换礼</a>--%>

                </div>
             </li>
             <li>
                <h3>服务与设施</h3>
                <div class="site">
                    <a href="/Phone/service/index.aspx">咨询中心</a>
                    <a href="/Phone/service/parking.aspx">停车服务</a>
                    <a href="/Phone/service/ATM.aspx">ATM设施</a>
                    <a href="/Phone/service/EVC.aspx">电动汽车充电</a>
                    <a href="/Phone/service/rooms.aspx">母婴室</a>

                </div>
             </li>
             <li>
                <h3>关于我们</h3>
                <div class="site">
                    <a href="/Phone/about/index.aspx">项目概述</a>
                    <a href="/Phone/about/ways.aspx">到达方式</a>
                    <a href="/Phone/about/contact.aspx">联系我们</a>
                    <a href="/Phone/about/rent.aspx">场地与商铺租赁</a>

                </div>
             </li>
         </ul>
     </div>
  </div> 
  

</section>
    <uc1:footer runat="server" ID="footer" />



<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>

 </body>
</html>

