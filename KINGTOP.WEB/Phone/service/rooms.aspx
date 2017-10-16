<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rooms.aspx.cs" Inherits="KingTop.WEB.Phone.service.rooms" %>

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
     
    <ul class="sernavlist">
            <li><a href="/Phone/service/index.aspx"><span><img src="/Phone/images/aicon1.jpg"></span>咨询中心</a></li>
             <li><a href="/Phone/service/parking.aspx"><span><img src="/Phone/images/aicon2.jpg"></span>停车服务</a></li>
             <li><a href="/Phone/service/ATM.aspx"><span><img src="/Phone/images/aicon3.jpg"></span>ATM设施</a></li>
             <li><a href="/Phone/service/EVC.aspx"><span><img src="/Phone/images/aicon4.jpg"></span>电动汽车充电</a></li>
             <li  class="hover"><a href="/Phone/service/rooms.aspx"><span><img src="/Phone/images/aicon5.jpg"></span>母婴室</a></li>
         </ul>
        
     <div class="sitemaps">
        <%=KingTop.Common.Tools.GetSinglePageContent("101009006005")%>
      </div>
    
  </div> 
  

</section>
    <uc1:footer runat="server" ID="footer" />



<div class="page" style="display:none;">
   <span class="closed"><img src="../images/close.png"></span>
   <div class="pinch-zoom"></div>
</div>

<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>
<script src="/Phone/js/pinchzoom.js"></script>
<script type="text/javascript">
    $(function () {
        $('div.pinch-zoom').each(function () {
            new RTP.PinchZoom($(this), {});
        });
        var url = $("#map span img").attr('src');
        $(".pinch-zoom").html("<img src='" + url + "'>");
    })
</script>

 </body>
</html>

