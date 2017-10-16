<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageView.aspx.cs" Inherits="KingTop.Web.Admin.ControlManageView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=hsFieldValue["Title"]%></title>
        <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
        <script type="text/javascript" src="../js/publicform.js"></script>
        <script type="text/javascript" src="../js/public.js"></script>
        <script type="text/javascript" src="../js/regExp.js"></script>
        <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
        <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../js/jquery.dialog.js"></script>
        <script type="text/javascript" src="../js/win.js"></script>
    <script src="../JS/ControlManageView.js" type="text/javascript"></script>

    <link href="../css/public.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="theForm" runat="server">
<div class="container">
<h4>位置： <a href="#">内容管理</a><a> ></a> <a href="#">文章查看</a></span></h4> 
<div id="panel"><dl><dt>对应类别：</dt><dd id="K_U_FoodGuide_type"><%=hsFieldValue["type"]%></dd></dl><dl><dt>店铺名称：</dt><dd id="K_U_FoodGuide_Title"><%=hsFieldValue["Title"]%></dd></dl><dl><dt>店铺LOGO：</dt><dd id="K_U_FoodGuide_ShopLogo"><%=hsFieldValue["ShopLogo"]%></dd></dl><dl><dt>你可能还会喜欢列表图：</dt><dd id="K_U_FoodGuide_LikeImg"><%=hsFieldValue["LikeImg"]%></dd></dl><dl><dt>店铺实体图：</dt><dd id="K_U_FoodGuide_Stereogram"><%=hsFieldValue["Stereogram"]%></dd></dl><dl><dt>位置：</dt><dd id="K_U_FoodGuide_ShopNo"><%=hsFieldValue["ShopNo"]%></dd></dl><dl><dt>按楼层：</dt><dd id="K_U_FoodGuide_Floor"><%=ctrManageView.ParseFieldValueToText("100011474896672",hsFieldValue["Floor"])%></dd></dl><dl><dt>位置地图：</dt><dd id="K_U_FoodGuide_LocationImg"><%=hsFieldValue["LocationImg"]%></dd></dl><dl><dt>电话：</dt><dd id="K_U_FoodGuide_TelPhone"><%=hsFieldValue["TelPhone"]%></dd></dl><dl><dt>销售产品：</dt><dd id="K_U_FoodGuide_SalesPro"><%=hsFieldValue["SalesPro"]%></dd></dl><dl><dt>网址：</dt><dd id="K_U_FoodGuide_SiteURL"><%=hsFieldValue["SiteURL"]%></dd></dl><dl><dt>简介：</dt><dd id="K_U_FoodGuide_IntroDetail"><%=hsFieldValue["IntroDetail"]%></dd></dl><dl><dt>店铺展示：</dt><dd id="K_U_FoodGuide_Shopshow"><%=hsFieldValue["Shopshow"]%></dd></dl><dl><dt>关键字：</dt><dd id="K_U_FoodGuide_MetaKeyword"><%=hsFieldValue["MetaKeyword"]%></dd></dl><dl><dt>页面描述：</dt><dd id="K_U_FoodGuide_MetaDescript"><%=hsFieldValue["MetaDescript"]%></dd></dl></div>
<asp:HiddenField Value="K_U_FoodGuide" ID="hdnTableName" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000007655794" runat="server" />
</div>
<center><input type="button" value="返回" onclick="history.back();"  class="AddBtn"/></center>
</form>
</body>
</html>
