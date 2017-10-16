<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="change.aspx.cs" Inherits="KingTop.WEB.Phone.vip.change" %>

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
     <div class="curr"><h3>会员专区</h3></div>
     <div class="navinput subnav">
           <span class="jiao"></span><p class="set">换领礼品</p>
          <ul class="sublist new">
           <li><a href="integral.html">会员积分</a></li>
           <li><a href="change.html">换领礼品</a></li>
           <li><a href="rights.html">精选优品·会员权益</a></li>
           <li><a href="order.html">条款及细则</a></li></ul>
           </div>
        <p><img src="../images/vpic4.jpg"></p>
        <div class="vip">
           <div class="jifen">我的积分:<em>2</em> <em>2</em> <em>8</em> <em>0</em>分</div>
           <div class="choice">
               <h2>礼品筛选 (共筛选到 26 款礼品)</h2>
               <div class="items it-border">
                 <h3>礼品分类 :</h3>
                 <a href="#" class="hover">不限</a>
                 <a href="#">护肤品</a>
                 <a href="#">小家电</a>
                 <a href="#">优惠券</a>
                 <a href="#">记事本</a>
                 <a href="#">餐饮</a>
                 <a href="#">电影票</a>
                 <a href="#">现金券</a>
               </div>
               
               <div class="items">
                 <h3>积分数量 :</h3>
                 <a href="#" class="hover">不限</a>
                 <a href="#">0 - 1000</a>
                 <a href="#">1000 - 5000</a>
                 <a href="#">5000 - 10000</a>
                 <a href="#">5000 - 10000</a>
                 <a href="#">10000 - 20000</a>
                 <a href="#">20000 - 30000</a>
                 <a href="#">30000 - 40000</a>
               </div>
           </div>

        </div>
        
        <ul class="choicelist">
            <li><a href="#"><img src="../images/chpic1.jpg"><h3>OCE 200元现金 Coupon</h3><p>积分：<em>2500</em></p><p>兑换地点：<em>小中庭客服中心</em></p></a></li>
            <li><a href="#"><img src="../images/chpic1.jpg"><h3>OCE 200元现金 Coupon</h3><p>积分：<em>2500</em></p><p>兑换地点：<em>小中庭客服中心</em></p></a></li>
            <li><a href="#"><img src="../images/chpic1.jpg"><h3>OCE 200元现金 Coupon</h3><p>积分：<em>2500</em></p><p>兑换地点：<em>小中庭客服中心</em></p></a></li>
            <li><a href="#"><img src="../images/chpic1.jpg"><h3>OCE 200元现金 Coupon</h3><p>积分：<em>2500</em></p><p>兑换地点：<em>小中庭客服中心</em></p></a></li>
            <li><a href="#"><img src="../images/chpic1.jpg"><h3>OCE 200元现金 Coupon</h3><p>积分：<em>2500</em></p><p>兑换地点：<em>小中庭客服中心</em></p></a></li>
            <li><a href="#"><img src="../images/chpic1.jpg"><h3>OCE 200元现金 Coupon</h3><p>积分：<em>2500</em></p><p>兑换地点：<em>小中庭客服中心</em></p></a></li>
        </ul>
        
        <div class="pages">
         <a href="#">< 上一页</a>
         <span><b>6</b> / 9</span>
         <a href="#">下一页 ></a>
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

