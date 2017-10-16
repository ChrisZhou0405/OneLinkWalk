<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
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
<asp:HiddenField ID="hdnHtmlField" Value="editor_Content" runat="server" />
<input type="hidden" id="hdnCreateHtmlRighte" value="<%=isHasCreateHtmlRight %>" />
<div class="container">
 <h4>位置：  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4> 
<div id="panel"><dl><dt>活动标题</dt><dd><input  class="validate[required]"  type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width:300px;"  maxlength="512" name="Title" id="Title" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl><dl><dt>活动时间</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["ActivityTime"].ToString())%>" style="width:200px;"  maxlength="300" name="ActivityTime" id="ActivityTime" /></dd></dl><dl><dt>活动列表图</dt><dd><input   type="text" value="<%=hsFieldValue["TitleImg"]%>" style="width:300px;"  maxlength="" name="TitleImg" id="TitleImg" /> <input type='button' onclick="InputImages('theForm', 'TitleImg', 1, '', 20480, '',125,125,0,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('TitleImg', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#TitleImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'TitleImg', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片最佳宽度为：386px；高度为：312px</dd></dl><dl><dt>活动内容</dt><dd><uc1:Editor ID="editor_Content" runat="server" width="680" height="400" IsFirstEditor="true"/></dd></dl><dl><dt>关键字</dt><dd><textarea style="height:40px;width:500px;"   class="validate[optional]"  id="MetaKeyword"  name="MetaKeyword"><%=FormatInputValue(hsFieldValue["MetaKeyword"].ToString())%></textarea></dd></dl><dl><dt>页面描述</dt><dd><textarea style="height:40px;width:500px;"   class="validate[optional]"  id="MetaDescript"  name="MetaDescript"><%=FormatInputValue(hsFieldValue["MetaDescript"].ToString())%></textarea>页面描述字段</dd></dl><dl><dt>开始日期</dt><dd><input id="startTime"  value="<%if(hsFieldValue["startTime"].ToString().Equals("none")){Response.Write("");}else{Response.Write(string.Format("{0:yyyy-MM-dd}",hsFieldValue["startTime"]));}%>"   name="startTime" style="width:100px;"  class="Wdate" type="text" onFocus="WdatePicker({lang:'<%=Resources.Model.DateLang%>',skin:'whyGreen',dateFmt:'yyyy-MM-dd'})"/></dd></dl><dl><dt>结束日期</dt><dd><input id="endTime"  value="<%if(hsFieldValue["endTime"].ToString().Equals("none")){Response.Write("");}else{Response.Write(string.Format("{0:yyyy-MM-dd}",hsFieldValue["endTime"]));}%>"   name="endTime" style="width:100px;"  class="Wdate" type="text" onFocus="WdatePicker({lang:'<%=Resources.Model.DateLang%>',skin:'whyGreen',dateFmt:'yyyy-MM-dd'})"/></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit" CssClass="subButton"  runat="server" />
 <input type="button" value="返回" Class="subButton" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_News" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,ActivityTime||1,TitleImg||11,Content||3,MetaKeyword||2,MetaDescript||2,startTime|none|10,endTime|none|10," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172319" runat="server" /><input type="hidden" id="hdnModelAlias" value="活动日志" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>