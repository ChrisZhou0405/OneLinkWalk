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
<ul id="tags"><li class="selectTag"><a href="javascript:;">������Ϣ</a></li><li><a href="javascript:;">�߼�����</a></li></ul><div id="panel"><fieldset><dl><dt>���⣺</dt><dd id="K_U_product_Title"><%=hsFieldValue["Title"]%></dd></dl><dl><dt>ѡ���ƷӦ�����</dt><dd id="K_U_product_productuse"><%=hsFieldValue["productuse"]%></dd></dl><dl><dt>Сͼ��</dt><dd id="K_U_product_SmallImg"><%=hsFieldValue["SmallImg"]%></dd></dl><dl><dt>��ͼ��</dt><dd id="K_U_product_BigImg"><%=hsFieldValue["BigImg"]%></dd></dl><dl><dt>������</dt><dd id="K_U_product_Attach"><%=hsFieldValue["Attach"]%></dd></dl><dl><dt>�Ƽ�����ҳ��</dt><dd id="K_U_product_tuij"><%=ctrManageView.ParseFieldValueToText("100000008367951",hsFieldValue["tuij"])%></dd></dl><dl><dt>����ʱ�䣺</dt><dd id="K_U_product_PubDate"><%=hsFieldValue["PubDate"]%></dd></dl><dl><dt>��飺</dt><dd id="K_U_product_Intro"><%=hsFieldValue["Intro"]%></dd></dl><dl><dt>���ݣ�</dt><dd id="K_U_product_Content"><%=hsFieldValue["Content"]%></dd></dl><div style="clear:left"></div></fieldset><fieldset style="display:none;"><dl><dt>���������</dt><dd id="K_U_product_jjfa"><%=hsFieldValue["jjfa"]%></dd></dl><dl><dt>��Ʒ������</dt><dd id="K_U_product_cpal"><%=hsFieldValue["cpal"]%></dd></dl><dl><dt>�����ؼ��֣�</dt><dd id="K_U_product_MetaKeyword"><%=hsFieldValue["MetaKeyword"]%></dd></dl><dl><dt>ҳ��������</dt><dd id="K_U_product_MetaDescript"><%=hsFieldValue["MetaDescript"]%></dd></dl><dl><dt>��᣺</dt><dd id="K_U_product_xc"><%=hsFieldValue["xc"]%></dd></dl><dl><dt>��ͼ���ԣ�</dt><dd id="K_U_product_dtcs"><%=hsFieldValue["dtcs"]%></dd></dl><div style="clear:left"></div></fieldset></div>
<asp:HiddenField Value="K_U_product" ID="hdnTableName" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172307" runat="server" />
</div>
<center><input type="button" value="����" onclick="history.back();"  class="AddBtn"/></center>
</form>
</body>
</html>
