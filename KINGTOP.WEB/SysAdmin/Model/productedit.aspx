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
    <script src="../JS/ModelFieldAlbums.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.14.custom.min.js" type="text/javascript"></script>
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
<asp:HiddenField ID="hdnHtmlField" Value="editor_Content,editor_jjfa,editor_cpal" runat="server" />
<input type="hidden" id="hdnCreateHtmlRighte" value="<%=isHasCreateHtmlRight %>" />
<div class="container">
 <h4>位置：  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4> 
<ul id="tags"><li class="selectTag"><a href="javascript:;">基本信息</a> </li><li><a href="javascript:;">高级设置</a> </li></ul><div id="panel"><fieldset><dl><dt>标题</dt><dd><input  class="validate[required]"  type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width:300px;"  maxlength="512" name="Title" id="Title" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl><dl><dt>选择产品应用类别</dt><dd><select  id="productuse" name="productuse"><%foreach (DictionaryEntry de in ctrManageEdit.FieldBind("K_Types", "TypeName", "TypeId", "menuid='101029003'  AND Ispub=1")){%><option  <%if(hsFieldValue["productuse"] != null &&hsFieldValue["productuse"].ToString().Contains(",de.Key.ToString(),")) {Response.Write("selected='selected'");}%> value=",<%= de.Key%>,"><%=de.Value%></option> <% } %> </select></dd></dl><dl><dt>小图</dt><dd><input   type="text" value="<%=hsFieldValue["SmallImg"]%>" style="width:300px;"  maxlength="" name="SmallImg" id="SmallImg" /> <input type='button' onclick="InputImages('theForm', 'SmallImg', 1, '', 4096, '',0,0,0,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('SmallImg', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#SmallImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'SmallImg', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' /></dd></dl><dl><dt>大图</dt><dd><input   type="text" value="<%=hsFieldValue["BigImg"]%>" style="width:300px;"  maxlength="" name="BigImg" id="BigImg" /> <input type='button' onclick="InputImages('theForm', 'BigImg', 1, '', 4096, '',125,125,0,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('BigImg', '<%=GetUploadImgUrl()%>','image')" value = '预览图片'/> <input type='button' onclick="if($('#BigImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'BigImg', '<%=GetUploadImgUrl()%>') } });}"  value = '删除' /></dd></dl><dl><dt>附件</dt><dd><input id=""  type="hidden" value="<%=hsFieldValue[""]%>" id="Attach"  name="" /><input   type="text" value="<%=hsFieldValue["Attach"]%>" style="width:300px;"  maxlength="" name="Attach" id="Attach" /> <input type='button' onclick="InputFile('theForm','Attach','file',1,'pdf|zip|rar',40960,'','','',0,'','')" value = '上传文件' /> <input type='button' value = '下载' onclick="ShowImages('Attach', '<%=GetUploadFileUrl()%>','file')"/> <input type='button' onclick="if($('#Attach').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'Attach', '<%=GetUploadFileUrl()%>') } });}" value = '删除'/></dd></dl><dl><dt>推荐到首页</dt><dd><input  type="radio" <%if(hsFieldValue["tuij"] != null && hsFieldValue["tuij"].ToString().Contains("是")) {Response.Write("checked='checked'");}%>  name="tuij" value="是" />是<input  type="radio" <%if(hsFieldValue["tuij"] != null && hsFieldValue["tuij"].ToString().Contains("否")) {Response.Write("checked='checked'");}%>  name="tuij" value="否" />否<br> 勾选“是”后会在首页显示</dd></dl><dl><dt>发布时间</dt><dd><input id="PubDate"  value="<%if(hsFieldValue["PubDate"].ToString().Equals("DateTime.Now.ToString('yyyy-MM-dd HH:mm:ss')")){Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));}else{Response.Write(string.Format("{0:yyyy-MM-dd HH:mm:ss}",hsFieldValue["PubDate"]));}%>"   name="PubDate" style="width:200px;"  class="Wdate" type="text" onFocus="WdatePicker({lang:'<%=Resources.Model.DateLang%>',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})"/></dd></dl><dl><dt>简介</dt><dd><textarea style="height:80px;width:500px;"   class="validate[optional]"  id="Intro"  name="Intro"><%=FormatInputValue(hsFieldValue["Intro"].ToString())%></textarea>500字符以内，注：1个汉字等于2个字符</dd></dl><dl><dt>内容</dt><dd><uc1:Editor ID="editor_Content" runat="server" width="680" height="500" IsFirstEditor="true"/></dd></dl><div style="clear:left"></div></fieldset><fieldset style="display:none;"><dl><dt>解决方案</dt><dd><uc1:Editor ID="editor_jjfa" runat="server" width="650" height="300" IsFirstEditor="false"/></dd></dl><dl><dt>产品案例</dt><dd><uc1:Editor ID="editor_cpal" runat="server" width="650" height="300" IsFirstEditor="false"/></dd></dl><dl><dt>搜索关键字</dt><dd><textarea style="height:40px;width:500px;"   class="validate[required]"  id="MetaKeyword"  name="MetaKeyword"><%=FormatInputValue(hsFieldValue["MetaKeyword"].ToString())%></textarea><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span>用于搜索</dd></dl><dl><dt>页面描述</dt><dd><textarea style="height:40px;width:500px;"   class="validate[optional]"  id="MetaDescript"  name="MetaDescript"><%=FormatInputValue(hsFieldValue["MetaDescript"].ToString())%></textarea>页面描述字段</dd></dl><dl><dt>相册</dt><dd><section class="xccontainer Albums_Del"><ul id="xcAlbumsContainer" class="AlbumsList ui-sortable"></ul></section><div style="clear:left;padding-left:8px">	<input type="hidden" value="<%=hsFieldValue["xc"]%>" name="xc" id="xc" />	<input type="text" id="xcAlbumsNewTitle"  style="width: 180px;display:none;"> <input type="text" id="xcAlbumsNewURL"   readonly="readonly"  style="width: 220px;display:none">    <input type="button" value="上传图片" id="xcAlbumsNewBtn">	<script type="text/javascript">$(function() { $('#xcAlbumsContainer').sortable({ start: function(event, ui) { ui.item.addClass('active');},stop: function(event, ui) {ui.item.removeClass('active').effect('highlight',{ color: '#000' }, 1000, function() {});}});});	var xcUploadParam = {FormName:"theForm", ElementName:"xcAlbumsNewURL", ControlType:3, ExtType:"", MaxSize:4096, GetSizeControl:"", ThumbWidth:125, ThumbHeight:125, IsMult: 1, BestWidth: "0", BestHeight: "0"};	var albumsxc = new Albums("xc",xcUploadParam,false);albumsxc.init("xc");	</script></div></dd></dl><dl><dt>多图测试</dt><dd><style  type="text/css">.HQB_MultiFile_ButtonList input{margin-bottom:5px;}</style><table  class="HQB_MultiFile_ButtonList"><tr><td valign="top"><ul style="list-style:none; padding::0; margin:0; "><li style="margin:0; padding:0;margin-bottom:5px;"><input type="hidden" id="dtcs" value="<%=hsFieldValue["dtcs"]%>" name="dtcs" /><select style=" width:100px; height:30px;" name="HQB_MultiFile_dtcs" multiple="multiple" id="HQB_MultiFile_dtcs" ondblclick="return MultiFileItemModify('HQB_MultiFile_dtcs','dtcs')" onchange="MultiFileSynchronousHideValue('HQB_MultiFile_dtcs','dtcs')"></select></li><li style="margin:0; padding:0; display:block;"><input type='button' onclick="InputImages('theForm', 'HQB_MultiFile_dtcs', 2, '', 4096, 'dtcs',125,125,1,'0','0')" value = '上传图片' /> <input type='button' onclick="ShowImages('dtcs', '<%=GetUploadImgUrl()%>','image',2)" value = '预览图片'/></li></ul></td><td valign="top"><input type="button" onclick="MultiFileItemAdd('HQB_MultiFile_dtcs','dtcs')" value="添加外部地址" /><br /><input type="button" value="修改选中" onclick="MultiFileItemModify('HQB_MultiFile_dtcs','dtcs')" /><br /><input type="button" value="删除选中" onclick="MultiFileItemDel('HQB_MultiFile_dtcs','dtcs')"/><br /><input type="button" value="向上移动" onclick="UpOrder('HQB_MultiFile_dtcs')" /><br /><input type="button" value="向下移动" onclick="DownOrder('HQB_MultiFile_dtcs')" /></td></tr></table><script type="text/javascript">MultiFileInit('HQB_MultiFile_dtcs','dtcs')</script></dd></dl><div style="clear:left"></div></fieldset></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit" CssClass="subButton"  runat="server" />
 <input type="button" value="返回" Class="subButton" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_product" ID="hdnTableName" runat="server" /><asp:HiddenField Value="Title||1,productuse||6,SmallImg||11,BigImg||11,Attach||12,|0|8,tuij|否|4,PubDate|DateTime.Now.ToString('yyyy-MM-dd HH:mm:ss')|10,Intro||2,Content||3,jjfa||3,cpal||3,MetaKeyword|射频贴片电感、贴片磁珠、磁珠排、LC滤波器、介质天线、压敏电阻、功率电感、各类插装式，贴片式绕线电感|2,MetaDescript|致力于成为亚洲地区顶尖的被动电子元器件制造商。主要研发和制造各类射频贴片电感、贴片磁珠、磁珠排、LC滤波器、介质天线、压敏电阻、功率电感、各类插装式和贴片式绕线电感等系列产品|2,xc||11,dtcs||11," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000000172307" runat="server" /><input type="hidden" id="hdnModelAlias" value="产品" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>