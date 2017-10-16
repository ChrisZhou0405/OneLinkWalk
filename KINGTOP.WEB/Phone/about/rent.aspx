<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rent.aspx.cs" Inherits="KingTop.WEB.Phone.about.rent" %>

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
     <div class="curr"><h3>场地与商铺租赁</h3></div>
     <div class="navinput navinput1 subnav">
       <span class="jiao"></span><p class="set"><%=this.titleStr %></p>
        <ul class="sublist new" id="ulId">
          <%=this.lis %>
        </ul>
     </div>
     
     <div class="case-about">
       <div class="tab s">
        <%=this.tabStr %>
       </div>           
    </div>

  </div> 
  
<div id="focus1" class="focus focus3">
        <div class="hd">
            <ul></ul>
        </div>
        <div class="bd">
            <ul >
            <%=this.lisImgStr %>
            </ul>
        </div>
        <span class="prev"></span>
		<span class="next"></span>
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

<script type="text/javascript">
    $(function () {
        $("#ulId li").click(function () {
            var id = $(this).attr('id');
         
            window.location = "/Phone/about/rent.aspx?id=" + id;
            
        });
    });

</script>

<link rel="stylesheet" type="text/css" href="/Phone/css/style.css">
