<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Commonedit.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Common.Commonedit" %>
<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
        <input type="hidden" id="hdnRSID" value="<%=Request.QueryString["ID"]%>"/>
        <asp:HiddenField ID="hdNewID" runat="server" />
        <asp:HiddenField ID="hdnNodeID" runat="server" />
        <asp:HiddenField ID="hdnSiteID" runat="server" />
        <asp:HiddenField ID="hdnActionType" runat="server" />
        <asp:HiddenField ID="hdnBackUrlParam" runat="server" />
        <asp:HiddenField ID="hdnHtmlField" Value="editor_detail" runat="server" />
        <input type="hidden" id="hdnCreateHtmlRighte" value="<%=isHasCreateHtmlRight %>" />
        <div class="container">
            <h4>位置：  <%GetPageNav(NodeCode);%> > <span id="CurrentOpNavName"></span></h4>
            <div id="panel">
                <dl>
                    <dt>标题</dt>
                    <dd>
                        <input class="validate[required]" type="text" value="<%=FormatInputValue(hsFieldValue["Title"].ToString())%>" style="width: 300px;" maxlength="512" name="Title" id="Title" /><span style="color: #ff0000; font-size: 14px; font-weight: bold;">*</span></dd>
                </dl>
                <dl>
                    <dt>用户名</dt>
                    <dd>
                        <input class="validate[optional]" type="text" value="<%=FormatInputValue(hsFieldValue["UserName"].ToString())%>" style="width: 300px;" maxlength="30" name="UserName" id="UserName" /></dd>
                </dl>
                <dl>
                    <dt>密码</dt>
                    <dd>
                        <input type="password" value="<%=FormatInputValue(hsFieldValue["Password"].ToString())%>" style="width: 300px;" maxlength="100" name="Password" id="Password" /></dd>
                </dl>
                <dl>
                    <dt>性别</dt>
                    <dd>
                        <input type="radio" <%if (hsFieldValue["Sex"] != null && hsFieldValue["Sex"].ToString().Contains("男")) { Response.Write("checked='checked'"); }%> name="Sex" value="男" />男<input type="radio" <%if (hsFieldValue["Sex"] != null && hsFieldValue["Sex"].ToString().Contains("女")) { Response.Write("checked='checked'"); }%> name="Sex" value="女" />女</dd>
                </dl>
                <dl>
                    <dt>发布时间</dt>
                    <dd>
                        <input id="PubDate" value="<%if (hsFieldValue["PubDate"].ToString().Equals("DateTime.Now.ToString('yyyy-MM-dd HH:mm:ss')")) { Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); } else { Response.Write(string.Format("{0:yyyy-MM-dd HH:mm:ss}", hsFieldValue["PubDate"])); }%>" name="PubDate" style="width: 200px;" class="Wdate" type="text" onfocus="WdatePicker({lang:'<%=Resources.Model.DateLang%>',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" /></dd>
                </dl>
                <dl>
                    <dt>简介</dt>
                    <dd>
                        <textarea style="height: 80px; width: 500px;" class="validate[optional]" id="Intro" name="Intro"><%=FormatInputValue(hsFieldValue["Intro"].ToString())%></textarea>500字符以内，注：1个汉字等于2个字符</dd>
                </dl>
                <dl>
                    <dt>大图</dt>
                    <dd>
                        <input type="text" value="<%=hsFieldValue["BigImg"]%>" style="width: 300px;" maxlength="" name="BigImg" id="BigImg" />
                        <input type='button' onclick="InputImages('theForm', 'BigImg', 1, '', 4096, '',125,125,0,'','')" value='上传图片' />
                        <input type='button' onclick="ShowImages('BigImg','<%=GetUploadImgUrl()%>','image')" value='预览图片' />
                        <input type='button' onclick="if($('#BigImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'BigImg', '<%=GetUploadImgUrl()%>') } });}" value='删除' />图片最佳宽度为：XXXpx；高度为：XXXpx</dd>
                </dl>
                <dl>
                    <dt>小图</dt>
                    <dd>
                        <input type="text" value="<%=hsFieldValue["SmallImg"]%>" style="width: 300px;" maxlength="" name="SmallImg" id="SmallImg" />
                        <input type='button' onclick="InputImages('theForm', 'SmallImg', 1, '', 4096, '',125,125,0,'','')" value='上传图片' />
                        <input type='button' onclick="ShowImages('SmallImg', '<%=GetUploadImgUrl()%>','image')" value='预览图片' />
                        <input type='button' onclick="if($('#SmallImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'SmallImg', '<%=GetUploadImgUrl()%>') } });}" value='删除' />图片最佳宽度为：XXXpx；高度为：XXXpx</dd>
                </dl>
                <dl>
                    <dt>多图</dt>
                    <dd>
                        <style type="text/css">
                            .HQB_MultiFile_ButtonList input {
                                margin-bottom: 5px;
                            }
                        </style>
                        <table class="HQB_MultiFile_ButtonList">
                            <tr>
                                <td valign="top">
                                    <ul style="list-style: none; padding:0; margin: 0;">
                                        <li style="margin: 0; padding: 0; margin-bottom: 5px;">
                                            <input type="hidden" id="listimage" value="<%=hsFieldValue["listimage"]%>" name="listimage" />
                                            <select style="width: 300px; height: 200px;" name="HQB_MultiFile_listimage" multiple="multiple" id="HQB_MultiFile_listimage" ondblclick="return MultiFileItemModify('HQB_MultiFile_listimage','listimage')" onchange="MultiFileSynchronousHideValue('HQB_MultiFile_listimage','listimage')"></select></li>
                                        <li style="margin: 0; padding: 0; display: block;">
                                            <input type='button' onclick="InputImages('theForm', 'HQB_MultiFile_listimage', 2, '', 4096, 'listimage',125,125,1,'','')" value='上传图片' />
                                            <input type='button' onclick="ShowImages('listimage', '<%=GetUploadImgUrl()%>','image',2)" value='预览图片' /></li>
                                    </ul>
                                </td>
                                <td valign="top">
                                    <input type="button" onclick="MultiFileItemAdd('HQB_MultiFile_listimage','listimage')" value="添加外部地址" /><br />
                                    <input type="button" value="修改选中" onclick="MultiFileItemModify('HQB_MultiFile_listimage','listimage')" /><br />
                                    <input type="button" value="删除选中" onclick="MultiFileItemDel('HQB_MultiFile_listimage','listimage')" /><br />
                                    <input type="button" value="向上移动" onclick="UpOrder('HQB_MultiFile_listimage')" /><br />
                                    <input type="button" value="向下移动" onclick="DownOrder('HQB_MultiFile_listimage')" /></td>
                            </tr>
                        </table>
                        <script type="text/javascript">MultiFileInit('HQB_MultiFile_listimage','listimage')</script>
                        图片最佳宽度为：XXXpx；高度为：XXXpx</dd>
                </dl>
                <dl>
                    <dt>多图相册</dt>
                    <dd>
                        <section class="xccontainer Albums_Del"><ul id="PhotoAlbumAlbumsContainer" class="AlbumsList ui-sortable"></ul></section>
                        <div style="clear: left; padding-left: 8px">
                            <input type="hidden" value="<%=hsFieldValue["PhotoAlbum"]%>" name="PhotoAlbum" id="PhotoAlbum" />
                            <input type="text" id="PhotoAlbumAlbumsNewTitle" style="width: 180px; display: none;">
                            <input type="text" id="PhotoAlbumAlbumsNewURL" readonly="readonly" style="width: 220px; display: none">
                            <input type="button" value="上传图片" id="PhotoAlbumAlbumsNewBtn">
                            <script type="text/javascript">$(function() { $('#PhotoAlbumAlbumsContainer').sortable({ start: function(event, ui) { ui.item.addClass('active');},stop: function(event, ui) {ui.item.removeClass('active').effect('highlight',{ color: '#000' }, 1000, function() {});}});});	var PhotoAlbumUploadParam = {FormName:"theForm", ElementName:"PhotoAlbumAlbumsNewURL", ControlType:3, ExtType:"", MaxSize:4096, GetSizeControl:"", ThumbWidth:125, ThumbHeight:125, IsMult: 1, BestWidth: "0", BestHeight: "0"};	var albumsPhotoAlbum = new Albums("PhotoAlbum",PhotoAlbumUploadParam,false);albumsPhotoAlbum.init("PhotoAlbum");	</script>
                        </div>
                    </dd>
                </dl>
                <dl>
                    <dt>附件</dt>
                    <dd>
                        <input type="text" value="<%=hsFieldValue["Attach"]%>" style="width: 300px;" maxlength="" name="Attach" id="Attach" />
                        <input type='button' onclick="InputFile('theForm','Attach','file',1,'',10000,'','','',0,'','')" value='上传文件' />
                        <input type='button' value='下载' onclick="ShowImages('Attach', '<%=GetUploadFileUrl()%>    ','file')" />
                        <input type='button' onclick="if($('#Attach').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'Attach', '<%=GetUploadFileUrl()%>        ') } });}" value='删除' /></dd>
                </dl>
                <dl>
                    <dt>多附件</dt>
                    <dd>
                        <style type="text/css">
                            .HQB_MultiFile_ButtonList input {
                                margin-bottom: 5px;
                            }
                        </style>
                        <table class="HQB_MultiFile_ButtonList">
                            <tr>
                                <td valign="top">
                                    <ul style="list-style: none; padding: :0; margin: 0;">
                                        <li style="margin: 0; padding: 0; margin-bottom: 5px;">
                                            <input type="hidden" id="AttachList" value="<%=hsFieldValue["AttachList"]%>" name="AttachList" /><select style="width: 300px; height: 100px;" name="HQB_MultiFile_AttachList" multiple="multiple" id="HQB_MultiFile_AttachList" ondblclick="return MultiFileItemModify('HQB_MultiFile_AttachList','AttachList')" onchange="MultiFileSynchronousHideValue('HQB_MultiFile_AttachList','AttachList')"></select></li>
                                        <li style="margin: 0; padding: 0; display: block;">
                                            <input type='button' onclick="InputFile('theForm','HQB_MultiFile_AttachList','file',2,'',4096,'AttachList','','',1,'','')" value='上传文件...' /></li>
                                    </ul>
                                </td>
                                <td valign="top">
                                    <input type="button" onclick="MultiFileItemAdd('HQB_MultiFile_AttachList','AttachList')" value="添加外部地址" /><br />
                                    <input type="button" value="修改选中" onclick="MultiFileItemModify('HQB_MultiFile_AttachList','AttachList')" /><br />
                                    <input type="button" value="删除选中" onclick="MultiFileItemDel('HQB_MultiFile_AttachList','AttachList')" /><br />
                                    <input type="button" value="向上移动" onclick="UpOrder('HQB_MultiFile_AttachList')" /><br />
                                    <input type="button" value="向下移动" onclick="DownOrder('HQB_MultiFile_AttachList')" /></td>
                            </tr>
                        </table>
                        <script type="text/javascript">MultiFileInit('HQB_MultiFile_AttachList','AttachList')</script>
                    </dd>
                </dl>
                <dl>
                    <dt>兴趣</dt>
                    <dd>
                        <select id="Interest" multiple="multiple" name="Interest">
                            <option <%if (hsFieldValue["Interest"] != null && hsFieldValue["Interest"].ToString().Contains("篮球")) { Response.Write("selected='selected'"); }%> value="篮球">篮球</option>
                            <option <%if (hsFieldValue["Interest"] != null && hsFieldValue["Interest"].ToString().Contains("足球")) { Response.Write("selected='selected'"); }%> value="足球">足球</option>
                            <option <%if (hsFieldValue["Interest"] != null && hsFieldValue["Interest"].ToString().Contains("羽毛球")) { Response.Write("selected='selected'"); }%> value="羽毛球">羽毛球</option>
                            <option <%if (hsFieldValue["Interest"] != null && hsFieldValue["Interest"].ToString().Contains("桌球")) { Response.Write("selected='selected'"); }%> value="桌球">桌球</option>
                        </select></dd>
                </dl>
                <dl>
                    <dt>详细内容</dt>
                    <dd>
                        <uc1:Editor ID="editor_detail" runat="server" width="950" height="500" IsFirstEditor="true" />
                    </dd>
                </dl>
            </div>
            <div class="Submit" id="HQB_Operation_Button">
                <asp:Button ID="btnModelManageEdit" OnClick="btnModelManageEdit_Click" CssClass="subButton" runat="server" />
                <input type="button" value="返回" class="subButton" onclick="javascript:window.location.href='Commonlist.aspx?NodeCode=<%=NodeCode %>&IsFirst=1'" />
            </div>
        </div>
        <asp:HiddenField ID="hdnFieldFromUrlParamValue" runat="server" Value="" />
        <asp:HiddenField ID="hdnNodeCode" runat="server" />
        <asp:HiddenField Value="K_U_Common" ID="hdnTableName" runat="server" />
        <asp:HiddenField Value="Title||1,UserName||1,Password||15,Sex||4,PubDate|DateTime.Now.ToString('yyyy-MM-dd HH:mm:ss')|10,Intro||2,BigImg||11,SmallImg||11,listimage||11,PhotoAlbum||11,Attach||12,AttachList||12,Interest||7,detail||3," ID="hdnFieldValue" runat="server" />
        <asp:HiddenField ID="hdnModelID" Value="100000003414185" runat="server" />
        <input type="hidden" id="hdnModelAlias" value="通用功能模型" name="hdnModelAlias" />
        <script type="text/javascript"><%=jsMessage %></script>
    </form>

    
</body>
</html>
