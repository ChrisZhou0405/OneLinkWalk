<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendemail.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.sendemail" %>
<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>发送邮件</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
    <link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/<%=Resources.Common.formValidationLanguage %>"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });
    </script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript">
        function checkFrm() {
            var em = $.trim($("#txtEMail").val());
            var ti = $.trim($("#txtTitle").val());
            var co = $.trim($("#Editor1$txtEditorContent").val());
            if (em == "") {
                alert({msg:""})
            }
        }
    </script>
</head>
<body>
    <form id="theForm" name="theForm" runat="server">
    <input type="hidden" id="hidLogTitle" runat="server" />
    <input type="hidden" id="hidID" runat="server" />
    <div class="container">

        <table class="listInfo" bordercolor="#dbe5e7" border="1">
            <tbody>
            <tr class="listInfotr">
                <td width="100" align="right">邮件地址</td>
                <td><input type="text" id="txtEMail" runat="server" class="validate[required,custom[email]]" maxlength="128" style="width:300px"/> <font color=red>*</font></td>
            </tr>
            <tr class="listInfotr">
                <td align="right">标题</td>
                <td><input type="text" id="txtTitle" runat="server" class="validate[required]" maxlength="128" style="width:300px"/> <font color=red>*</font></td>
            </tr>
            <tr class="listInfotr">
                <td align="right">内容</td>
                <td height="315"><uc1:Editor ID="Editor1" runat="server" width="500" height="300" EditorType=1 WidthType="1"/></td>
            </tr>
            </tbody>
        </table>

        <div class="Submit">
            <asp:Button ID="BtnSave" runat="server" CssClass="subButton" Text="发送"
                OnClick="BtnSave_Click" />
                <input type="reset" id="reset1" name="reset1" class="subButton" value="重置" />
        </div>
    </div>
    </form>
</body>
</html>
