<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="integral.aspx.cs" Inherits="KingTop.WEB.Phone.vip.integral" %>

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
  <div class="mmain">
     <div class="curr"><h3>会员专区</h3></div>
     <div class="navinput subnav">
           <span class="jiao"></span><p class="set">会员积分</p>
          <ul class="sublist new">
           <li><a href="integral.html">会员积分</a></li>
           <li><a href="change.html">换领礼品</a></li>
           <li><a href="rights.html">精选优品·会员权益</a></li>
           <li><a href="order.html">条款及细则</a></li></ul>
           </div>
        <div class="vip">
            <p><img src="../images/vpic3.png"></p> 
          <div class="infor_tit"><h2>我的信息</h2></div>
            <ul class="inforlist">
                  <li><span class="repair">修改</span><b class="abicon"><img src="../images/abicon7.png"></b>姓名：<input name="" type="text" value="万菱汇会员" disabled></li>
                  <li><span class="repair">修改</span><b class="abicon"><img src="../images/adicon2.png"></b>电话：<input name="" type="text" value="18888888888" disabled></li>
                  <li><span class="repair">修改</span><b class="abicon"><img src="../images/adicon5.png"></b>邮箱：<input name="" type="text" value="Onelinkwalk@mail.com" disabled></li>
                  <li><b class="abicon"><img src="../images/abicon8.png"></b>卡号：100646</li>
                  <li><b class="abicon"><img src="../images/abicon9.png"></b>我的积分:<em>2</em> <em>2</em> <em>8</em> <em>0</em> 分</li>
            </ul>         
        </div>  
     </div>
</section>

    <uc1:footer runat="server" ID="footer" />


<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>
<script src="/Phone/js/TouchSlide.1.0.js"></script>
<script>
    TouchSlide({
        slideCell: "#focus1",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: false,//自动播放
        autoPage: true //自动分页
    });

</script>

 </body>
</html>

