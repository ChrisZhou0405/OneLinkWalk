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
<div id="panel"><dl><dt>对应类别</dt><dd><select  id="type" name="type"><%foreach (DictionaryEntry de in ctrManageEdit.FieldBind("K_U_FoodCategory", "Title", "ID", "NodeCode='101003001' and isDel=0 order by Orders Desc")){%><option  <%if(hsFieldValue["type"] != null && hsFieldValue["type"].ToString().Contains(de.Key.ToString())) {Response.Write("selected='selected'");}%> value="<%= de.Key%>"><%=de.Value%></option> <% } %> </select></dd></dl><dl><dt>店铺名称</dt><dd><input  class="validate[required]"  type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width:300px;"  maxlength="512" name="Title" id="Title" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl><dl><dt>店铺LOGO</dt><dd><input id=""  type="hidden" value="<%=hsFieldValue[""]%>" id="ShopLogo"  name="" /><input   type="text" value="<%=hsFieldValue["ShopLogo"]%>" style="width:300px;"  maxlength="" name="ShopLogo" id="ShopLogo" /> <input type='button' onclick="InputImages('theForm', 'ShopLogo', 1, '', 20480, '',125,125,0,'150','150')" value = '上传图片' /> <input type='button' onclick="ShowImages('ShopLogo', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#ShopLogo').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'ShopLogo', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片最佳宽度为：200px；高度为：200px</dd></dl><dl><dt>你可能还会喜欢列表图</dt><dd><input id=""  type="hidden" value="<%=hsFieldValue[""]%>" id="LikeImg"  name="" /><input   type="text" value="<%=hsFieldValue["LikeImg"]%>" style="width:300px;"  maxlength="" name="LikeImg" id="LikeImg" /> <input type='button' onclick="InputImages('theForm', 'LikeImg', 1, '', 10240, '',125,125,0,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('LikeImg', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#LikeImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'LikeImg', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片最佳宽度为：300px；高度为：300px</dd></dl><dl><dt>店铺实体图</dt><dd><input id=""  type="hidden" value="<%=hsFieldValue[""]%>" id="Stereogram"  name="" /><input   type="text" value="<%=hsFieldValue["Stereogram"]%>" style="width:300px;"  maxlength="" name="Stereogram" id="Stereogram" /> <input type='button' onclick="InputImages('theForm', 'Stereogram', 1, '', 20480, '',125,125,0,'15','150')" value = '上传图片' /> <input type='button' onclick="ShowImages('Stereogram', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#Stereogram').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'Stereogram', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片最佳宽度为：404px；高度为：172px</dd></dl><dl><dt>位置</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["ShopNo"].ToString())%>" style="width:300px;"  maxlength="300" name="ShopNo" id="ShopNo" /></dd></dl><dl><dt>按楼层</dt><dd><select  id="Floor" name="Floor"><option  <%if(hsFieldValue["Floor"] != null && hsFieldValue["Floor"].ToString().Contains("0")) {Response.Write("selected='selected'");}%> value="0">B1</option><option  <%if(hsFieldValue["Floor"] != null && hsFieldValue["Floor"].ToString().Contains("1")) {Response.Write("selected='selected'");}%> value="1">L1</option><option  <%if(hsFieldValue["Floor"] != null && hsFieldValue["Floor"].ToString().Contains("2")) {Response.Write("selected='selected'");}%> value="2">L2</option><option  <%if(hsFieldValue["Floor"] != null && hsFieldValue["Floor"].ToString().Contains("3")) {Response.Write("selected='selected'");}%> value="3">L3</option><option  <%if(hsFieldValue["Floor"] != null && hsFieldValue["Floor"].ToString().Contains("4")) {Response.Write("selected='selected'");}%> value="4">L4</option><option  <%if(hsFieldValue["Floor"] != null && hsFieldValue["Floor"].ToString().Contains("5")) {Response.Write("selected='selected'");}%> value="5">L5</option></select></dd></dl><dl><dt>位置地图</dt><dd><input id=""  type="hidden" value="<%=hsFieldValue[""]%>" id="LocationImg"  name="" /><input   type="text" value="<%=hsFieldValue["LocationImg"]%>" style="width:300px;"  maxlength="" name="LocationImg" id="LocationImg" /> <input type='button' onclick="InputImages('theForm', 'LocationImg', 1, '', 20480, '',125,125,0,'150','150')" value = '上传图片' /> <input type='button' onclick="ShowImages('LocationImg', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#LocationImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'LocationImg', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' />图片最佳宽度为：1166px；高度为：460px</dd></dl><dl><dt>电话</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["TelPhone"].ToString())%>" style="width:300px;"  maxlength="300" name="TelPhone" id="TelPhone" /></dd></dl><dl><dt>销售产品</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["SalesPro"].ToString())%>" style="width:300px;"  maxlength="300" name="SalesPro" id="SalesPro" /></dd></dl><dl><dt>网址</dt><dd><input  class="validate[optional]"  type="text" value="<%=FormatInputValue(hsFieldValue["SiteURL"].ToString())%>" style="width:300px;"  maxlength="300" name="SiteURL" id="SiteURL" /></dd></dl><dl><dt>简介</dt><dd><textarea style="height:200px;width:680px;"   class="validate[optional]"  id="IntroDetail"  name="IntroDetail"><%=FormatInputValue(hsFieldValue["IntroDetail"].ToString())%></textarea></dd></dl><dl><dt>店铺展示</dt><dd><style  type="text/css">.HQB_MultiFile_ButtonList input{margin-bottom:5px;}</style><table  class="HQB_MultiFile_ButtonList"><tr><td valign="top"><ul style="list-style:none; padding::0; margin:0; "><li style="margin:0; padding:0;margin-bottom:5px;"><input type="hidden" id="Shopshow" value="<%=hsFieldValue["Shopshow"]%>" name="Shopshow" /><select style=" width:300px; height:200px;" name="HQB_MultiFile_Shopshow" multiple="multiple" id="HQB_MultiFile_Shopshow" ondblclick="return MultiFileItemModify('HQB_MultiFile_Shopshow','Shopshow')" onchange="MultiFileSynchronousHideValue('HQB_MultiFile_Shopshow','Shopshow')"></select></li><li style="margin:0; padding:0; display:block;"><input type='button' onclick="InputImages('theForm', 'HQB_MultiFile_Shopshow', 2, '', 204800, 'Shopshow',125,125,1,'150','150')" value = '上传图片' /> <input type='button' onclick="ShowImages('Shopshow', '<%=GetUploadImgUrl()%>','image',2)" value = '预览图片'/></li></ul></td><td valign="top"><input type="button" onclick="MultiFileItemAdd('HQB_MultiFile_Shopshow','Shopshow')" value="添加外部地址" /><br /><input type="button" value="修改选中" onclick="MultiFileItemModify('HQB_MultiFile_Shopshow','Shopshow')" /><br /><input type="button" value="删除选中" onclick="MultiFileItemDel('HQB_MultiFile_Shopshow','Shopshow')"/><br /><input type="button" value="向上移动" onclick="UpOrder('HQB_MultiFile_Shopshow')" /><br /><input type="button" value="向下移动" onclick="DownOrder('HQB_MultiFile_Shopshow')" /></td></tr></table><script type="text/javascript">MultiFileInit('HQB_MultiFile_Shopshow','Shopshow')</script>图片最佳宽度为：600px；高度为：440px</dd></dl><dl><dt>关键字</dt><dd><textarea style="height:100px;width:680px;"   class="validate[optional]"  id="MetaKeyword"  name="MetaKeyword"><%=FormatInputValue(hsFieldValue["MetaKeyword"].ToString())%></textarea></dd></dl><dl><dt>页面描述</dt><dd><textarea style="height:100px;width:680px;"   class="validate[optional]"  id="MetaDescript"  name="MetaDescript"><%=FormatInputValue(hsFieldValue["MetaDescript"].ToString())%></textarea></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit" CssClass="subButton"  runat="server" />
 <input type="button" value="返回" Class="subButton" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_FoodGuide" ID="hdnTableName" runat="server" /><asp:HiddenField Value="type||6,Title||1,ShopLogo||11,|0|8,LikeImg||11,Stereogram||11,ShopNo||1,Floor|0|6,LocationImg||11,TelPhone||1,SalesPro||1,SiteURL||1,IntroDetail||2,Shopshow||11,MetaKeyword||2,MetaDescript||2," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000007655794" runat="server" /><input type="hidden" id="hdnModelAlias" value="美食荟萃" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>