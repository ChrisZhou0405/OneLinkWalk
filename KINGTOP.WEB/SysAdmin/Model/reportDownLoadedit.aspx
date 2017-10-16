<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>模型编辑页</title>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-validationEngine-cn.js"></script>
    <script src="../Calendar/WdatePicker.js" type="text/javascript"></script>
    
    
    
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
<div id="panel"><dl><dt>标题</dt><dd><input  class="validate[required]"  type="text" value="<%=hsFieldValue["Title"]%>" style="width:300px;"  maxlength="512" name="Title" id="Title" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl><dl><dt>发布时间</dt><dd><input id="PubDate"  value="<%if(hsFieldValue["PubDate"].ToString().Equals("DateTime.Now.ToString('yyyy-MM-dd HH:mm:ss')")){Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));}else{Response.Write(string.Format("{0:yyyy-MM-dd HH:mm:ss}",hsFieldValue["PubDate"]));}%>"   name="PubDate" style="width:200px;"  class="Wdate" type="text" onFocus="WdatePicker({lang:'<%=Resources.Model.DateLang%>',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})"/></dd></dl><dl><dt>附件</dt><dd><input   type="text" value="<%=hsFieldValue["Attach"]%>" style="width:300px;"  maxlength="" name="Attach" id="Attach" /> <input type='button' onclick="InputFile('theForm','Attach','file',1,'pdf|rar|zip',10240,'','','',0,'','')" value = '上传文件' /> <input type='button' value = '下载' onclick="ShowImages('Attach', '<%=GetUploadFileUrl()%>','file')"/> <input type='button' onclick="if($('#Attach').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'Attach', '<%=GetUploadFileUrl()%>') } });}" value = '删除'/><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit"  runat="server" />
 <input type="button" value="返回" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_reportDownLoad" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,PubDate|DateTime.Now.ToString('yyyy-MM-dd HH:mm:ss')|10,Attach||12," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172295" runat="server" /><input type="hidden" id="hdnModelAlias" value="定期报告" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>