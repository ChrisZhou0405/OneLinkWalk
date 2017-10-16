<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Editor._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <title></title>
    <script type="text/javascript" charset="utf-8" src="editor_config.js"></script>
    <!--开发版-->
    <script type="text/javascript" charset="utf-8" src="editor_all_min.js"></script>
    <link rel="stylesheet" type="text/css" href="themes/default/ueditor.css"/>
</head>
<body>
<form id="form1" runat="server">
<p>
<h1>UEditor所有功能</h1>

<asp:TextBox ID="txtContent" runat="server" TextMode ="MultiLine" style="width:800px;height:300px"></asp:TextBox>

</p>
<p>
<h1>UEditor部分功能</h1>
<asp:TextBox ID="txtContent2" runat="server" TextMode ="MultiLine" style="width:800px;height:300px"></asp:TextBox>
<%--<script type="text/javascript" id="txtContent2"></script>--%>
<script type="text/javascript">
    var editor_a = new baidu.editor.ui.Editor();
    editor_a.render('txtContent');
    
    var editor_a1 = new baidu.editor.ui.Editor({
        toolbars: [[ 'source',
                'bold', 'italic', 'underline', 'autotypeset', '|', 'pasteplain',  'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
               'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                'link', 'unlink', 'insertimage', 'attachment', '|',
                'snapscreen', 'wordimage'
               ]]
    });
    editor_a1.render('txtContent2');
</script>
</p>

<textarea id="Editor1_txtEditorContent" cols="20" rows="2" name="Editor1$txtEditorContent"></textarea>
<script type="text/javascript">
var editor_Editor1 = new baidu.editor.ui.Editor({toolbars: [[ 'source',
 'bold', 'italic', 'underline', 'autotypeset', '|', 'pasteplain', 'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
 'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
 'link', 'unlink', 'insertimage', 'attachment', '|',
 'snapscreen', 'wordimage'
 ]] });editor_Editor1.render('Editor1_txtEditorContent');
</script>
<asp:Button ID="btnSubmit2" OnClientClick="on_submit()" OnClick="btn_Click" Text="确定" runat="server"/>
</form>
</body>


</html>

