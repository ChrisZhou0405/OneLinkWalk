<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.Phone.index" %>

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
<link rel="stylesheet" type="text/css" href="css/style.css">
<title></title>

</head>
<body>
    <uc1:navbox runat="server" ID="navbox" />

<section>
  <div class="ban">
    <div id="focus" class="focus">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
            <ul>
            <%=this.bd %>
            </ul>
        </div>
  </div>
  </div>
</section>

<section>

  <div class="main">
  <ul class="indexnavlist">
        <li><a href="/Phone/shopping/index.aspx"><img src="/Phone/images/icon1.png"><b>购物指南</b></a></li>
       <li><a href="/Phone/cate/index.aspx"><img src="/Phone/images/icon2.png"><b>美食荟萃</b></a></li>
       <li><a href="/Phone/activity/index.aspx"><img src="/Phone/images/icon3.png"><b>最新活动</b></a></li>
       <li><a href="/Phone/vip/vip.aspx"><img src="/Phone/images/icon4.png"><b>会员专区</b></a></li>
  </ul>
  
  
  <div id="focus1" class="focus focus2">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
            <ul>
            <%=this.focus1 %>
            </ul>
        </div>
        <span class="prev"></span>
		<span class="next"></span>
  </div>
  
  
  
  <div id="focus2" class="focus focus2">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
            <ul>
             <%=this.focus2 %>
            </ul>
        </div>
        <span class="prev"></span>
		<span class="next"></span>
  </div>
  
  </div>
   
  
</section>

    <uc1:footer runat="server" ID="footer" />


<script src="js/jquery.js"></script>
<script src="js/touch.min.js"></script>
<script src="js/jqnav.js"></script>
<script src="js/TouchSlide.1.0.js"></script>
<script>
    TouchSlide({
        slideCell: "#focus",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: true,//自动播放
        autoPage: true, //自动分页
        interTime:5000
    });
    TouchSlide({
        slideCell: "#focus1",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: true,//自动播放
        autoPage: true, //自动分页
        interTime:5000
    });
    TouchSlide({
        slideCell: "#focus2",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: true,//自动播放
        autoPage: true, //自动分页
        interTime: 5000
    });
</script>
 </body>
</html>

