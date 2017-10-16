<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KingTop.Web.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>KingTop CMS -- 图派科技</title>
<link href="../sysadmin/css/login.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../sysadmin/js/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="../sysadmin/js/jquery.ui.form.select.js"></script>
<link href="../sysadmin/css/dialog.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../sysadmin/js/jquery.dialog.js"></script>
<script type="text/javascript" src="../sysadmin/js/win.js"></script>
<script type="text/javascript">
    function check(o) {
        var id = o.id;
        if (id == "username") {
            document.getElementById(id).value = "";
        }
        if (id == "password") {
            document.getElementById(id).value = "";
        }
        if (id == "showRegExp") {
            document.getElementById(id).value = "";
        }
    }
    function out(o) {
        var id = o.id;
        var val = document.getElementById(id).value;
        if (!val) {

            if (id == "username") {
                document.getElementById(id).value = "用户名";
            }
            if (id == "password") {
                document.getElementById(id).value = "密码";
            }
            if (id == "showRegExp") {
                document.getElementById(id).value = "验证码";
            }
        }
    }
</script>
<script type="text/javascript">
    if (top.location != self.location) {
        top.location = self.location;
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="slogoinContainer">
<div class="loginLayer">
<p>
<asp:dropdownlist id="DDLSite" runat="server" CssClass="carteselect"></asp:dropdownlist>
</p>
<p>用户名：<asp:textbox id="AccountName" CssClass="showInput" runat="server" tabindex="1" onfocus="check(this)" onblur="out(this)"></asp:textbox></p>
<p>密　码：<asp:textbox id="AccountPwd" CssClass="showInput" runat="server" textmode="Password" tabindex="2" onfocus="check(this)" onblur="out(this)"></asp:textbox></p>
<p id="przm" runat="server">认证码：<asp:textbox id="txtRZM" CssClass="showInput" runat="server" textmode="Password" tabindex="3" onfocus="check(this)" onblur="out(this)"></asp:textbox></p>
<p id="pyzm" runat="server"><dl><dt>验证码：<asp:textbox id="txtValidate" CssClass="showRegExp" runat="server" tabindex="4" onfocus="check(this)" onblur="out(this)"></asp:textbox></dt><dd>
    <img src="ValidateCode.aspx"  alt="看不清楚？点击刷新验证码！" 
        style="cursor:pointer; left: 0px;" 
        onclick="src='ValidateCode.aspx?s='+Math.random()" width="53" alt="图形验证码" /></dd></dl></p>
<p><asp:checkbox id="SaveLoginInfo" runat="server" tabindex="4" CssClass="check" Text="记住用户名" /><asp:ImageButton ID="btnLogin" ImageUrl="../sysadmin/images/loginsub.png" runat="server" CssClass="loginsubmit" tabindex="5" onclick="btnLogin_Click" /></p>
</div>

</div>
</form>
</body>
</html>
