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
<div id="panel"><dl><dt>标题</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["Title"]%>" style="width:180px;"  maxlength="512" name="Title" id="Title" /></dd></dl><dl><dt>描述</dt><dd><textarea style="height:120px;width:320px;"   class="validate[optional]"  id="Desc"  name="Desc"><%=hsFieldValue["Desc"]%></textarea></dd></dl><dl><dt>资料文件</dt><dd><input id="Files_Size"  type="hidden" value="<%=hsFieldValue["Files_Size"]%>" id="Files"  name="Files_Size" /><input   type="text" value="<%=hsFieldValue["Files"]%>" style="width:150px;"  maxlength="" name="Files" id="Files" /> <input type='button' onclick="InputFile('theForm','Files','file',1,'',40960,'Files_Size','','',0,'','')" value = '上传文件' /> <input type='button' value = '下载' onclick="ShowImages('Files', '<%=GetUploadFileUrl()%>','file')"/> <input type='button' onclick="if($('#Files').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'Files', '<%=GetUploadFileUrl()%>') } });}" value = '删除'/></dd></dl><dl><dt>下载类型</dt><dd><select  id="DType" name="DType"><%foreach (DictionaryEntry de in ctrManageEdit.FieldBind("K_U_DownloadType", "Title", "ID", "isdel=0")){%><option  <%if(hsFieldValue["DType"] != null && hsFieldValue["DType"].ToString().Contains(de.Key.ToString())) {Response.Write("selected='selected'");}%> value="<%= de.Key%>"><%=de.Value%></option> <% } %> </select></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit"  runat="server" />
 <input type="button" value="返回" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_Download" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,Desc||2,Files||12,Files_Size|0|8,DType||6," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000003575738" runat="server" /><input type="hidden" id="hdnModelAlias" value="资料下载" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>