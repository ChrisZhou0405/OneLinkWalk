<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site2.aspx.cs" Inherits="KingTop.WEB.En.shopping.site2" %>

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
    <link rel="stylesheet" type="text/css" href="/En/css/style.css"/>
    <link rel="stylesheet" type="text/css" href="/En/css/jquery.mCustomScrollbar.css"/>
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
                <i>Shop：<%=this.TitleStr %></i>
                <i>Location：<%=this.ShopNo %> <em class="btnSite" id="btnSiteId">Map</em></i>
                <i>Tel：<%=this.TelPhone %></i>
                <i>Product：<%=this.SalesPro%></i>
                <i>URL：<a href="http://<%=this.SiteURL %>" target="_blank"><%=this.SiteURL %></a></i>
               </span>
        <p><%=this. IntroDetail%></p>
        <a  target="_blank" href="/En/shopping/detail2.aspx?nid=<%=this.idStr %>" id="bl_txt_a" target="_parent">Details</a>
    </div>
</div>



<script src="/En/js/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    $(".btnSite").click(function () { $('.overbg,.lMap', window.parent.document).fadeIn(); });
</script>

</body>
</html>