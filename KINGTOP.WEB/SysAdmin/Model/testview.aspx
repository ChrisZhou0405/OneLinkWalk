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
<h4>λ�ã� <a href="#">���ݹ���</a><a> ></a> <a href="#">���²鿴</a></span></h4> 
<div id="panel"><dl><dt>�Ƽ���</dt><dd id="K_U_test_IsCommend"><%=ctrManageView.ParseFieldValueToText("100011413043917",hsFieldValue["IsCommend"])%></dd></dl><dl><dt>�ö���</dt><dd id="K_U_test_IsTop"><%=ctrManageView.ParseFieldValueToText("100011413144158",hsFieldValue["IsTop"])%></dd></dl><dl><dt>��Դ��</dt><dd id="K_U_test_Source"><%=hsFieldValue["Source"]%></dd></dl><dl><dt>ҳ��������</dt><dd id="K_U_test_MetaDescript"><%=hsFieldValue["MetaDescript"]%></dd></dl><dl><dt>���⣺</dt><dd id="K_U_test_Title"><%=hsFieldValue["Title"]%></dd></dl><dl><dt>�Ա�Ҫ��</dt><dd id="K_U_test_Sex"><%=ctrManageView.ParseFieldValueToText("100011414251828",hsFieldValue["Sex"])%></dd></dl><dl><dt>���ߣ�</dt><dd id="K_U_test_Author"><%=hsFieldValue["Author"]%></dd></dl><dl><dt>test1��</dt><dd id="K_U_test_test"><%=hsFieldValue["test"]%></dd></dl><dl><dt>������ԣ�</dt><dd id="K_U_test_SoftLang"><%=ctrManageView.ParseFieldValueToText("100011414552414",hsFieldValue["SoftLang"])%></dd></dl><dl><dt>��飺</dt><dd id="K_U_test_Intro"><%=hsFieldValue["Intro"]%></dd></dl><dl><dt>���ݣ�</dt><dd id="K_U_test_Content"><%=hsFieldValue["Content"]%></dd></dl><dl><dt>email��</dt><dd id="K_U_test_email"><%=hsFieldValue["email"]%></dd></dl></div>
<asp:HiddenField Value="K_U_test" ID="hdnTableName" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000001438546" runat="server" />
</div>
<center><input type="button" value="����" onclick="history.back();"  class="AddBtn"/></center>
</form>
</body>
</html>
