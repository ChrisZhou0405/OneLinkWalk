<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.Phone.activity.index" %>

<%@ Register Src="~/Phone/controls/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Phone/controls/navbox.ascx" TagPrefix="uc1" TagName="navbox" %>



ds<!DOCTYPE html>
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
     
    <div class="curr"><h3>活动推介</h3></div>  
        
    <ul class="actnavlist">
        <li class="hover"><a href="/Phone/activity/index.aspx">最新活动</a></li>
        <li><a href="/Phone/activity/activity.aspx">活动日志</a></li>
     </ul>
     
   
     <div id="focus1" class="focus focus2">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
            <ul id="bdul">
            <%=this.lisStr %>
            </ul>
        </div>
        <span class="prev"></span>
		<span class="next"></span>
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
        autoPlay: true,//自动播放
        autoPage: true //自动分页
    });

</script>


 </body>
</html>

<script type="text/javascript">
    $("#bdul li").click(function () {
        var id = $(this).attr('id');
        window.location = "/Phone/activity/activity.aspx?id="+id;
      
    });

</script>