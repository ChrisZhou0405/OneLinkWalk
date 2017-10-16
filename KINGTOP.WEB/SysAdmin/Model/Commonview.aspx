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
<div id="panel"><dl><dt>标题：</dt><dd id="K_U_Common_Title"><%=hsFieldValue["Title"]%></dd></dl><dl><dt>用户名：</dt><dd id="K_U_Common_UserName"><%=hsFieldValue["UserName"]%></dd></dl><dl><dt>密码：</dt><dd id="K_U_Common_Password"><%=hsFieldValue["Password"]%></dd></dl><dl><dt>性别：</dt><dd id="K_U_Common_Sex"><%=ctrManageView.ParseFieldValueToText("100011428849162",hsFieldValue["Sex"])%></dd></dl><dl><dt>发布时间：</dt><dd id="K_U_Common_PubDate"><%=hsFieldValue["PubDate"]%></dd></dl><dl><dt>简介：</dt><dd id="K_U_Common_Intro"><%=hsFieldValue["Intro"]%></dd></dl><dl><dt>大图：</dt><dd id="K_U_Common_BigImg"><%=hsFieldValue["BigImg"]%></dd></dl><dl><dt>小图：</dt><dd id="K_U_Common_SmallImg"><%=hsFieldValue["SmallImg"]%></dd></dl><dl><dt>多图：</dt><dd id="K_U_Common_listimage"><%=hsFieldValue["listimage"]%></dd></dl><dl><dt>多图相册：</dt><dd id="K_U_Common_PhotoAlbum"><%=hsFieldValue["PhotoAlbum"]%></dd></dl><dl><dt>附件：</dt><dd id="K_U_Common_Attach"><%=hsFieldValue["Attach"]%></dd></dl><dl><dt>多附件：</dt><dd id="K_U_Common_AttachList"><%=hsFieldValue["AttachList"]%></dd></dl><dl><dt>兴趣：</dt><dd id="K_U_Common_Interest"><%=ctrManageView.ParseFieldValueToText("100011427425825",hsFieldValue["Interest"])%></dd></dl><dl><dt>详细内容：</dt><dd id="K_U_Common_detail"><%=hsFieldValue["detail"]%></dd></dl></div>
<asp:HiddenField Value="K_U_Common" ID="hdnTableName" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000003414185" runat="server" />
</div>
<center><input type="button" value="返回" onclick="history.back();"  class="AddBtn"/></center>
</form>
</body>
</html>
