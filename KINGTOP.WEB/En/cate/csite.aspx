<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="csite.aspx.cs" Inherits="KingTop.WEB.En.cate.csite" %>

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
<div class="bl_txt">

     <%=this.str %>

</div>
<script src="/En/js/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    $(".btnSite").click(function () { $('.overbg,.lMap', window.parent.document).fadeIn(); });
</script>

</body>
</html>
