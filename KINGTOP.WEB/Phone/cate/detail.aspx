<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="KingTop.WEB.Phone.cate.detail" %>

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
     <div class="curr"><h3>美食荟萃</h3></div>
     
        <div class="navinput subnav subnav2">
           <span class="jiao"></span><p class="set"><%=typeStr %></p>
             
             <ul class="sublist new">
               <li><a href="/Phone/cate/index.aspx"><img src="/Phone/images/cicon1.png" width="28"> 全部美食</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000000144283"><img src="/Phone/images/cicon2.png" width="28"> 亚洲美食</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000000825845"><img src="/Phone/images/cicon3.png" width="28"> 中式佳肴</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000001534222"><img src="/Phone/images/cicon4.png" width="28"> 西方美馔</a></li>
               <li><a href="/Phone/cate/index.aspx?id=100000002274564"><img src="/Phone/images/cicon5.png" width="28"> 轻便美食/甜点</a></li>
              </ul>
    </div>
     
    <div class="actdt">
       <div class="act-tit"><span class="sharebtn" id="sharebtn">分享</span><h2><%=this.shopName %></h2></div> 
       <div id="focus1" class="focus focus2" style=" margin:10px 0;">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
         <ul>
           <%=this.showImg %>
          </ul>
        </div>
        <span class="prev"></span>
		<span class="next"></span>
     </div>
     <div class="shop_tit"><h3>店铺信息</h3></div>
     <div class="shop">
         <div class="pic"><img src="<%=this.ShopLogo %>"></div>
         <div class="txt">
            <p>店铺名称：<%=this.shopName %></p>
            <p>位置：<%=postion %></p>
            <p>电话：<%=this.tel %></p>
            <p>销售产品：<%=this.sale %></p>
            <p>网址：<%=this.urlStr %></p>
        </div>
     </div>
     <p><%=this.detailStr %></p>
     
   </div> 
   
   <div class="like_tit"><span>你可能还会喜欢</span></div>
   
   <div id="focus2" class="focus focus2">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
         <ul>
           <%=this.likeStr %>          
        </ul>
        </div>
        <span class="prev"></span>
		<span class="next"></span>
     </div>     
  </div>
 </section>

    <uc1:footer runat="server" ID="footer" />


<div class="bglayout" id="bg"></div>
<div class="share" id="sharebg">
    <a id="aqq"><img src="/Phone/images/fxicon1.png"></a>
    <a id="awx" ><img src="/Phone/images/fxicon2.png"></a>
    <a id="wb" ><img src="/Phone/images/fxicon3.png"></a>

</div>
<div id="shareweixin" style="border: 1px solid rgba(0, 0, 0, 0.298039); box-shadow: rgba(0, 0, 0, 0.298039) 0px 3px 7px; margin: -200px 0px 0px -200px; font-size: 14px; z-index: 2222; width: 250px; left: 50%; position: fixed; overflow: hidden; top: 50%; height: 260px; background-color: rgb(255, 255, 255); background-clip: padding-box; display:none;">
<div style="padding:9px 15px;border-bottom:1px solid #eeeeee;text-align:left"><a target="_self" onclick="sharetoweixin_cancel()" id="jiathis_weixin_close" class="jiathis_weixin_close" style="text-decoration:none;  margin-top: 2px; color: #000000; float: right;  font-size: 20px;  font-weight: bold; cursor:pointer;line-height: 20px; opacity: 0.2; text-shadow: 0 1px 0 #FFFFFF;">×</a><h3 style="line-height:30px;font-weight:normal">分享到微信朋友圈</h3></div>
<div style="text-align:center;height:118px;"><div style="width:220px;height:220px;margin-top:15px;width:100%;"><p id="qrcode" title="http://www.mstar.cn/"><canvas width="220" height="220" style="display: none;"></canvas><img alt="Scan me!" style="padding-left:10px;width:110px;height:110px;" src="/uploadfiles/images/1476839221.png"></p></div></div>
<div style="padding:11px 10px 0;"><div style="font-size:12px;margin:0;padding:0px;text-align:left;">打开微信，点击底部的“发现”，使用 “扫一扫” 即可将网页分享到我的朋友圈。</div></div>
</div>

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

    TouchSlide({
        slideCell: "#focus2",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: false,//自动播放
        autoPage: true //自动分页
    });

</script>
    
 </body>
</html>

<script src="../../js/share.js"></script>
