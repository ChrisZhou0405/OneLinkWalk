<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
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
<asp:HiddenField ID="hdnHtmlField" Value="" runat="server" />
<input type="hidden" id="hdnCreateHtmlRighte" value="<%=isHasCreateHtmlRight %>" />
<div class="container">
 <h4>位置：  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4> 
<div id="panel"><dl><dt>到达方式</dt><dd><input  class="validate[required]"  type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width:300px;"  maxlength="512" name="Title" id="Title" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl><dl><dt>到达方式ICON</dt><dd><input   type="text" value="<%=hsFieldValue["BigImg"]%>" style="width:300px;"  maxlength="" name="BigImg" id="BigImg" /> <input type='button' onclick="InputImages('theForm', 'BigImg', 1, '', 4096, '',125,125,0,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('BigImg', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#BigImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'BigImg', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片最佳宽度为：100px以下；高度为：28px</dd></dl><dl><dt>到达方式介绍</dt><dd><textarea style="height:100px;width:600px;"   class="validate[optional]"  id="ReachIntro"  name="ReachIntro"><%=FormatInputValue(hsFieldValue["ReachIntro"].ToString())%></textarea>需要分行请在后面加<br/></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit" CssClass="subButton"  runat="server" />
 <input type="button" value="返回" Class="subButton" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_ModeArrival" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,BigImg||11,ReachIntro||2," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000010425995" runat="server" /><input type="hidden" id="hdnModelAlias" value="到达方式" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>