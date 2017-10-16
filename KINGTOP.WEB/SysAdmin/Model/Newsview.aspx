﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageView.aspx.cs" Inherits="KingTop.Web.Admin.ControlManageView" %>

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
<div id="panel"><dl><dt>活动标题：</dt><dd id="K_U_News_Title"><%=hsFieldValue["Title"]%></dd></dl><dl><dt>活动时间：</dt><dd id="K_U_News_ActivityTime"><%=hsFieldValue["ActivityTime"]%></dd></dl><dl><dt>活动列表图：</dt><dd id="K_U_News_TitleImg"><%=hsFieldValue["TitleImg"]%></dd></dl><dl><dt>活动内容：</dt><dd id="K_U_News_Content"><%=hsFieldValue["Content"]%></dd></dl><dl><dt>关键字：</dt><dd id="K_U_News_MetaKeyword"><%=hsFieldValue["MetaKeyword"]%></dd></dl><dl><dt>页面描述：</dt><dd id="K_U_News_MetaDescript"><%=hsFieldValue["MetaDescript"]%></dd></dl><dl><dt>开始日期：</dt><dd id="K_U_News_startTime"><%=hsFieldValue["startTime"]%></dd></dl><dl><dt>结束日期：</dt><dd id="K_U_News_endTime"><%=hsFieldValue["endTime"]%></dd></dl></div>
<asp:HiddenField Value="K_U_News" ID="hdnTableName" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172319" runat="server" />
</div>
<center><input type="button" value="返回" onclick="history.back();"  class="AddBtn"/></center>
</form>
</body>
</html>
