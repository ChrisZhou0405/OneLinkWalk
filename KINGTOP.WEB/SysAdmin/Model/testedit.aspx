<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageEdit.aspx.cs" ValidateRequest="false"  Inherits="KingTop.Web.Admin.ControlManageEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>ģ�ͱ༭ҳ</title>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-validationEngine-cn.js"></script>
    
    <script type="text/javascript" src="../Editor/ckeditor/ckeditor.js"></script>
    
    
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script src="../JS/publicform.js" type="text/javascript"></script>
    <script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/ControlManageEdit.js" type="text/javascript"></script>
    
    
    <script type="text/javascript">$(document).ready(function() { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });</script>
    <link href="../Editor/ckeditor/content.css" rel="stylesheet" type="text/css" />
    
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
<div id="panel"><dl><dt>�Ƽ�</dt><dd><input  type="radio" <%if(hsFieldValue["IsCommend"] != null && hsFieldValue["IsCommend"].ToString().Contains("1")) {Response.Write("checked='checked'");}%>  name="IsCommend" value="1" />��<input  type="radio" <%if(hsFieldValue["IsCommend"] != null && hsFieldValue["IsCommend"].ToString().Contains("0")) {Response.Write("checked='checked'");}%>  name="IsCommend" value="0" />��</dd></dl><dl><dt>�ö�</dt><dd><input  type="radio" <%if(hsFieldValue["IsTop"] != null && hsFieldValue["IsTop"].ToString().Contains("1")) {Response.Write("checked='checked'");}%>  name="IsTop" value="1" />��<input  type="radio" <%if(hsFieldValue["IsTop"] != null && hsFieldValue["IsTop"].ToString().Contains("0")) {Response.Write("checked='checked'");}%>  name="IsTop" value="0" />��</dd></dl><dl><dt>ҳ������</dt><dd><textarea style="height:40px;width:500px;"   class="validate[optional]"  id="MetaDescript"  name="MetaDescript"><%=hsFieldValue["MetaDescript"]%></textarea>ҳ�������ֶ�</dd></dl><dl><dt>����</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["Title"]%>" style="width:180px;"  maxlength="512" name="Title" id="Title" /></dd></dl><dl><dt>�Ա�Ҫ��</dt><dd><input  type="radio" <%if(hsFieldValue["Sex"] != null && hsFieldValue["Sex"].ToString().Contains("����")) {Response.Write("checked='checked'");}%>  name="Sex" value="����" />����<input  type="radio" <%if(hsFieldValue["Sex"] != null && hsFieldValue["Sex"].ToString().Contains("��")) {Response.Write("checked='checked'");}%>  name="Sex" value="��" />��<input  type="radio" <%if(hsFieldValue["Sex"] != null && hsFieldValue["Sex"].ToString().Contains("Ů")) {Response.Write("checked='checked'");}%>  name="Sex" value="Ů" />Ů</dd></dl><dl><dt>����</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["Author"]%>" style="width:300px;"  maxlength="30" name="Author" id="Author" /></dd></dl><dl><dt>test1</dt><dd><input  class="validate[optional]"  type="text" value="<%=hsFieldValue["test"]%>" style="width:200px;"  maxlength="30" name="test" id="test" />��˹�ٷ�3</dd></dl><dl><dt>�������</dt><dd><select  id="SoftLang" name="SoftLang"><option  <%if(hsFieldValue["SoftLang"] != null && hsFieldValue["SoftLang"].ToString().Contains("Ӣ��")) {Response.Write("selected='selected'");}%> value="Ӣ��">Ӣ��</option><option  <%if(hsFieldValue["SoftLang"] != null && hsFieldValue["SoftLang"].ToString().Contains("��������")) {Response.Write("selected='selected'");}%> value="��������">��������</option><option  <%if(hsFieldValue["SoftLang"] != null && hsFieldValue["SoftLang"].ToString().Contains("��������")) {Response.Write("selected='selected'");}%> value="��������">��������</option><option  <%if(hsFieldValue["SoftLang"] != null && hsFieldValue["SoftLang"].ToString().Contains("������")) {Response.Write("selected='selected'");}%> value="������">������</option><option  <%if(hsFieldValue["SoftLang"] != null && hsFieldValue["SoftLang"].ToString().Contains("�������")) {Response.Write("selected='selected'");}%> value="�������">�������</option><option  <%if(hsFieldValue["SoftLang"] != null && hsFieldValue["SoftLang"].ToString().Contains("��������")) {Response.Write("selected='selected'");}%> value="��������">��������</option></select></dd></dl><dl><dt>���</dt><dd><textarea style="height:80px;width:500px;"   class="validate[optional]"  id="Intro"  name="Intro"><%=hsFieldValue["Intro"]%></textarea>500�ַ����ڣ�ע��1�����ֵ���2���ַ�</dd></dl><%KingTop.Config.UploadConfig uploadobj = KingTop.Config.Upload.GetConfig(GetUploadImgPath); %><dl><dt>����</dt><dd><textarea  id="Content" name="Content"><%=hsFieldValue["Content"]%></textarea><script type="text/javascript">var Content$$ckeditor$$obj = CKEDITOR.replace('Content', { linkUploadAllowedExtensions: '<%=uploadobj.UploadFilesType %>', nodeId: 1,language:'<%=Resources.Model.DateLang%>', watermark: false, height: '500px', toolbar: 'ContentFull', modelId: 1, flashUploadAllowedExtensions: '<%=uploadobj.UploadMediaType %>', width: '680px', imageUploadAllowedExtensions: '<%=uploadobj.UploadImageType %>', skin: 'blue', thumbnail: false, fileRecord: true, fieldName: 'Content', wordPic: false, flashUpload: true, imageUpload: true, linkUpload: true, foreground: false, moduleName: '' }); </script></dd></dl><dl><dt>email</dt><dd><input  class="validate[required,regex[\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+,���email�������]]"  type="text" value="<%=hsFieldValue["email"]%>" style="width:100px;"  maxlength="30" name="email" id="email" /><span style="color:#ff0000; font-size:14px; font-weight:bold;">*</span></dd></dl></div>
<div class="Submit" id="HQB_Operation_Button">
 <asp:Button ID="btnModelManageEdit" OnClick="Edit"  runat="server" />
 <input type="button" value="����" onclick="back()" />
 </div>
</div>
 <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" /><asp:hiddenfield ID="hdnNodeCode" runat="server"/><asp:HiddenField Value="K_U_test" ID="hdnTableName" runat="server" /><asp:HiddenField Value="IsCommend||4,IsTop||4,MetaDescript||2,Title||1,Sex|����|4,Author||1,test||1,SoftLang|��������|6,Intro||2,Content||3,email||1," ID="hdnFieldValue" runat="server" /><asp:HiddenField ID="hdnModelID" Value="100000001438546" runat="server" /><input type="hidden" id="hdnModelAlias" value="test" name="hdnModelAlias"/> 
<script type="text/javascript"><%=jsMessage %></script>
 </form>
 </body>
 </html>