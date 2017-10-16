﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vip.aspx.cs" Inherits="KingTop.WEB.Phone.vip.vip" %>

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
       <ul class="actnavlist">
        <li class="hover"><a href="/Phone/vip/vip.aspx">会员卡</a></li>
        <li><a href="/Phone/vip/card.aspx">尊享卡</a></li>
     </ul>
     
        <div class="vip">
        
       <div class="vipcard">
           <div id="focus1" class="focus focus2" style="margin:0">
            <div class="hd">
                <ul></ul>
            </div>
            <div class="bd">
                <ul>
                <%=this.lisStr %>
                </ul>
            </div>
            <span class="prev"></span>
            <span class="next"></span>
          </div>
      
           <div class="vip-card-con">
                <%=KingTop.Common.Tools.GetSinglePageContent("101005001001002")%>
           </div>
              
        </div>  
        
        <div class="user-tit2"><h3>条款及细则</h3></div>    
           
        <div class="navinput navinput1 subnav">
           <span class="jiao"></span><p class="set">使用细则</p>
          <ul class="sublist new">
           <li>使用细则</li>
           <li>积分规则</li>
           <li>会员短信服务</li>
           <li>会员卡有效期</li>
           <li>续卡条件</li>
           </ul>
           </div>
           
          <div class="case-about uservip" id="case-about">
        <div class="tab">
              <%=KingTop.Common.Tools.GetSinglePageContent("101005003004001")%>  
        </div> 
        
        
         <div class="tab" style="display:none;">
              <%=KingTop.Common.Tools.GetSinglePageContent("101005003004002")%>
         </div>
         <div class="tab" style="display:none;">
               <%=KingTop.Common.Tools.GetSinglePageContent("101005003004003")%>
         </div>
         <div class="tab" style="display:none;">
              <%=KingTop.Common.Tools.GetSinglePageContent("101005003004004")%>
         </div>
         <div class="tab" style="display:none;">
               <%=KingTop.Common.Tools.GetSinglePageContent("101005003004005")%>
         </div>
        
        </div>
        
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
