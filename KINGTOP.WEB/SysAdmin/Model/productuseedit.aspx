<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>模型编辑页</title>
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
 <h4>位置：  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4> 
<div id="panel"><dl><dt>中文名称</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["Title"]%>" style="width:200px;"  maxlength="512" name="Title" id="Title" /></dd></dl><dl><dt>英文名称</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["Company"]%>" style="width:200px;"  maxlength="128" name="Company" id="Company" /></dd></dl><dl><dt>类别图片</dt><dd><input   type="text" value="<%=hsFieldValue["img2"]%>" style="width:200px;"  maxlength="" name="img2" id="img2" /> <input type='button' onclick="InputImages('theForm', 'img2', 1, '', 4096, '',125,125,0,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('img2', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#img2').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'img2', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片长宽是：201px*120px</dd></dl><dl><dt>密码测试</dt><dd><input   type="password" value="<%=hsFieldValue["mm1"]%>" style="width:100px;"  maxlength="30" name="mm1" id="mm1" /></dd></dl><dl><dt>密码2</dt><dd><input   type="password" value="<%=hsFieldValue["mm2"]%>" style="width:100px;"  maxlength="30" name="mm2" id="mm2" /></dd></dl><dl><dt>密码3</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["妈妈"]%>" style="width:100px;"  maxlength="30" name="妈妈" id="妈妈" /></dd></dl><dl><dt>密码4</dt><dd><input   type="password" value="<%=hsFieldValue["mm4"]%>" style="width:100px;"  maxlength="30" name="mm4" id="mm4" /></dd></dl><dl><dt>密码4</dt><dd><input   type="password" value="<%=hsFieldValue["mm4"]%>" style="width:100px;"  maxlength="30" name="mm4" id="mm4" /></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit"  runat="server" />
 <input type="button" value="返回" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_productuse" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,Company||1,img2||11,mm1||15,mm2||15,妈妈||1,mm4||15," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172277" runat="server" /><input type="hidden" id="hdnModelAlias" value="产品应用" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>