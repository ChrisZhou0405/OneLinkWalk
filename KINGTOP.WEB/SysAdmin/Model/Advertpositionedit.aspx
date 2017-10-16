﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false" Inherits="KingTop.Web.Admin.ControlManageEdit" %>

<%@ Register Src="../Controls/Editor.ascx" TagName="Editor" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <asp:HiddenField ID="hdnNodeID" runat="server" />
        <asp:HiddenField ID="hdnSiteID" runat="server" />
        <asp:HiddenField ID="hdnActionType" runat="server" />
        <asp:HiddenField ID="hdnBackUrlParam" runat="server" />
        <asp:HiddenField ID="hdnHtmlField" Value="" runat="server" />
        <input type="hidden" id="hdnCreateHtmlRighte" value="<%=isHasCreateHtmlRight %>" />
        <div class="container">
            <h4>位置：  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4>
            <div id="panel">
                <dl>
                    <dt>名称</dt>
                    <dd>
                        <input class="validate[required]" type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width: 300px;" maxlength="512" name="Title" id="Title" /><span style="color: #ff0000; font-size: 14px; font-weight: bold;">*</span></dd>
                </dl>
                <dl>
                    <dt>广告图</dt>
                    <dd>
                        <input type="text" value="<%=hsFieldValue["SmallImg"]%>" style="width: 300px;" maxlength="" name="SmallImg" id="SmallImg" />
                        <input type='button' onclick="InputImages('theForm', 'SmallImg', 1, '', 204800, '',125,125,0,'0','0')" value='上传图片' />
                        <input type='button' onclick="ShowImages('SmallImg', '<%=GetUploadImgUrl()%>    ','image')" value='预览图片' />
                        <input type='button' onclick="if($('#SmallImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'SmallImg', '<%=GetUploadImgUrl()%>        ') } });}" value='删除' /></dd>
                </dl>
                <dl>
                    <dt>链接地址</dt>
                    <dd>
                        <input class="validate[optional]" type="text" value="<%=FormatInputValue(hsFieldValue["IP"].ToString())%>" style="width: 680px;" maxlength="3000" name="IP" id="IP" /></dd>
                </dl>
                <dl>
                    <dt>广告位置</dt>
                    <dd>
                        <select id="Location" name="Location">
                            <option <%if (hsFieldValue["Location"] != null && hsFieldValue["Location"].ToString().Contains("1")) { Response.Write("selected='selected'"); }%> value="1">广告位置A</option>
                            <option <%if (hsFieldValue["Location"] != null && hsFieldValue["Location"].ToString().Contains("2")) { Response.Write("selected='selected'"); }%> value="2">广告位置B</option>
                            <option <%if (hsFieldValue["Location"] != null && hsFieldValue["Location"].ToString().Contains("3")) { Response.Write("selected='selected'"); }%> value="3">广告位置C</option>
                            <option <%if (hsFieldValue["Location"] != null && hsFieldValue["Location"].ToString().Contains("4")) { Response.Write("selected='selected'"); }%> value="4">广告位置D</option>
                            <option <%if (hsFieldValue["Location"] != null && hsFieldValue["Location"].ToString().Contains("5")) { Response.Write("selected='selected'"); }%> value="5">广告位置E</option>
                        </select>+
                         <a href="../images/HomeLocation.jpg" target="_blank">查看对应的广告位置</a>
                    </dd>
                </dl>
            </div>
            <div class="Submit" id="HQB_Operation_Button">
                <asp:Button ID="btnModelManageEdit" OnClick="Edit" CssClass="subButton" runat="server" />
                <input type="button" value="返回" class="subButton" onclick="back()" />
            </div>
        </div>
        <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" />
        <asp:HiddenField ID="hdnNodeCode" runat="server" />
        <asp:HiddenField Value="K_U_Advertposition" ID="hdnTableName" runat="server" />
        <asp:HiddenField Value="Title||1,SmallImg||11,IP||1,Location|0|6," ID="hdnFieldValue" runat="server" />
        <asp:HiddenField ID="hdnModelID" Value="100000004857821" runat="server" />
        <input type="hidden" id="hdnModelAlias" value="首页广告位" name="hdnModelAlias" />
        <script type="text/javascript"><%=jsMessage %></script>
    </form>
</body>
</html>
