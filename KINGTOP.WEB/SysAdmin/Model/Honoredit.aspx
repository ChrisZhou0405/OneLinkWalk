<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>ģ�ͱ༭ҳ</title>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-validationEngine-cn.js"></script>
    
    
    
    
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script src="../JS/publicform.js" type="text/javascript"></script>
    <script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/ControlManageEdit.js" type="text/javascript"></script>
    
    
    <script type="text/javascript">$(document).ready(function() { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });</script>
    
    
    <link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/public.css" rel="stylesheet" type="text/css" />
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    
      
 </head>
 <body>
 <form id="theForm" runat="server">
<input type="hidden" id="hdnUploadImgUrl" value="<%=GetUploadImgUrl() %>" />
<input type="hidden" id="hdnUploadImgPath" value="<%=GetUploadImgSetParam()[1] %>" />
<input type="hidden" id="hdnRSID" value="<%=Request.QueryString["ID"]%>" />
 <asp:hiddenfield ID="hdnNodeID"  runat="server"/>
<asp:hiddenfield ID="hdnSiteID"  runat="server"/>
<asp:HiddenField ID="hdnActionType" runat="server" />
<asp:hiddenfield ID="hdnBackUrlParam"  runat="server"/>
<input type="hidden" id="hdnCreateHtmlRighte" value="<%=isHasCreateHtmlRight %>" />
<div class="container">
 <h4>λ�ã�  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4> 
<div id="panel"><dl><dt>����</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["Title"]%>" style="width:180px;"  maxlength="512" name="Title" id="Title" /></dd></dl><dl><dt>��ͼ</dt><dd><input   type="text" value="<%=hsFieldValue["BigImg"]%>" style="width:300px;"  maxlength="" name="BigImg" id="BigImg" /> <input type='button' onclick="InputImages('theForm', 'BigImg', 1, '', 4096, '',125,125,0,'0','0')" value = '�ϴ�ͼƬ' /> <input type='button' onclick="ShowImages('BigImg', '<%=GetUploadImgUrl()%>','image')" value = 'Ԥ��ͼƬ'/> <input type='button' onclick="if($('#BigImg').val()!=''){selfconfirm({ msg: 'ȷ��Ҫִ��ɾ��������', fn: function(data) { FilesDel(data,'BigImg', '<%=GetUploadImgUrl()%>') } });}"  value = 'ɾ��' /></dd></dl><dl><dt>Сͼ</dt><dd><input   type="text" value="<%=hsFieldValue["SmallImg"]%>" style="width:300px;"  maxlength="" name="SmallImg" id="SmallImg" /> <input type='button' onclick="InputImages('theForm', 'SmallImg', 1, '', 4096, '',125,125,0,'0','0')" value = '�ϴ�ͼƬ' /> <input type='button' onclick="ShowImages('SmallImg', '<%=GetUploadImgUrl()%>','image')" value = 'Ԥ��ͼƬ'/> <input type='button' onclick="if($('#SmallImg').val()!=''){selfconfirm({ msg: 'ȷ��Ҫִ��ɾ��������', fn: function(data) { FilesDel(data,'SmallImg', '<%=GetUploadImgUrl()%>') } });}"  value = 'ɾ��' /></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit"  runat="server" />
 <input type="button" value="����" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_Honor" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,BigImg||11,SmallImg||11," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172301" runat="server" /><input type="hidden" id="hdnModelAlias" value="��������" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>