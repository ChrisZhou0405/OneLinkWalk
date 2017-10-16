<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ways.aspx.cs" Inherits="KingTop.WEB.Phone.about.ways" %>

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
     <div class="curr"><h3>到达方式</h3></div>     
     <div class="contact ways">
         <ul class="ways-list" id="waysList">
            <li id="1"><span><img src="/Phone/images/c-icon1.png"></span>地铁</li>
            <li id="2"><span><img src="/Phone/images/c-icon2.png"></span>公交</li>
            <li  id="3"><span><img src="/Phone/images/c-icon3.png"></span>自驾</li>
            <li id="4"><span><img src="/Phone/images/c-icon4.png"></span>BRT</li>
         </ul>
         
         <div class="map" id="map">
           <span id="spanId"><img src="/images/ddfs1.gif"></span>
           <p>点击放大</p>
         </div>
         <div class="con">
         <p class="f15 red">「万菱汇交通」</p>
         <%=this.translate %>
         
       </div>
     </div>
       
  </div> 
  

</section>


    <uc1:footer runat="server" ID="footer" />



<div class="page" style="display:none;">
   <span class="closed"><img src="/Phone/images/close.png"></span>
   <div class="pinch-zoom"><img src="/Phone/images/map1.jpg"></div>
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
        $("#waysList li").click(function () {
            var id = $(this).attr('id');
            $("#spanId").html("");
            $("#spanId").html('<img src="/images/ddfs'+id+'.gif">');
        });
    })


</script>

</body>
</html>
