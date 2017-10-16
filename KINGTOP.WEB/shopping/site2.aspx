<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site2.aspx.cs" Inherits="KingTop.WEB.shopping.site2" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport"
          content="width=device-width, initial-scale=1,user-scalable=no, minimum-scale=1, maximum-scale=1">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta content="telephone=no" name="format-detection">
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta content="no-cache" http-equiv="pragma">
    <meta content="0" http-equiv="expires">
    <meta content="telephone=no, address=no" name="format-detection">
    <meta name="revised" content="Monday, April 25th, 2016, 15:00 pm">
    <meta name="author" content="xiaoxiao"/>
    <title>万菱汇——oneLinkWalk</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css"/>
    <link rel="stylesheet" type="text/css" href="../css/jquery.mCustomScrollbar.css"/>
</head>
<body style="min-width:100%">
<%--<div class="bl_txt">

     <%=this.str %>

</div>--%>

    <div class="bl_txt">
    <%=this.Stereogram %>

    <div class="subLTxt">
        <%=this.ShopLogo %>
               <span>
                <i>店铺名称：<%=this.TitleStr %></i>
                <i>位置：<%=this.ShopNo %> <em class="btnSite" id="btnSiteId">地图</em></i>
                <i>电话：<%=this.TelPhone %></i>
                <i>销售产品：<%=this.SalesPro%></i>
                <i>网址：<a href="http://<%=this.SiteURL %>" target="_blank"><%=this.SiteURL %></a></i>
               </span>
        <p><%=this. IntroDetail%></p>
        <a  target="_blank" href="/shopping/detail2.aspx?nid=<%=this.idStr %>&id=<%=this.typeStr %>" id="bl_txt_a" target="_parent">了解详情</a>
    </div>
</div>



<script src="/js/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    $(".btnSite").click(function () { $('.overbg,.lMap', window.parent.document).fadeIn(); });
</script>

</body>
</html>