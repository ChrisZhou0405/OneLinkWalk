﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
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
<div id="panel"><dl><dt>店铺名称</dt><dd><input  class="validate[required]"  type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width:300px;"  maxlength="512" name="Title" id="Title" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl><dl><dt>店铺位置</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["lcnum"].ToString())%>" style="width:300px;"  maxlength="300" name="lcnum" id="lcnum" /></dd></dl><dl><dt>楼层对应地图</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["lcoverimgurl"].ToString())%>" style="width:680px;"  maxlength="3000" name="lcoverimgurl" id="lcoverimgurl" /></dd></dl><dl><dt>楼层X坐标</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["lcx"].ToString())%>" style="width:100px;"  maxlength="30" name="lcx" id="lcx" /></dd></dl><dl><dt>楼层Y坐标</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["lcy"].ToString())%>" style="width:100px;"  maxlength="30" name="lcy" id="lcy" /></dd></dl><dl><dt>楼层链接</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["lclink"].ToString())%>" style="width:680px;"  maxlength="10000" name="lclink" id="lclink" /></dd></dl><dl><dt>楼层坐标值</dt><dd><textarea style="height:50px;width:680px;"   class="validate[optional]"  id="lccoords"  name="lccoords"><%=FormatInputValue(hsFieldValue["lccoords"].ToString())%></textarea></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit" CssClass="subButton"  runat="server" />
 <input type="button" value="返回" Class="subButton" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_Floorguide" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,lcnum||1,lcoverimgurl||1,lcx||1,lcy||1,lclink||1,lccoords||2," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000011154190" runat="server" /><input type="hidden" id="hdnModelAlias" value="楼层指引" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>