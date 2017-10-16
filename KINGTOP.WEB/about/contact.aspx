<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="KingTop.WEB.about.contact" %>

<%@ Register Src="~/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" ID="Meta" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- backTop start -->
        <div class="backTop"><a href="javascript:;" class="btnTop"></a></div>
        <!-- backTop end -->
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->

        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <div class="title">联系我们 </div>
                <div class="itmeTxt ml_0 m0">
                    <%=KingTop.Common.Tools.GetSinglePageContent("101007003001")%>
                </div>
                <ul class="tabList list2">
                    <li>
                        <input name="txtTitle" id="txtTitle" type="text" placeholder="您的姓名" />
                    </li>
                    <li>
                        <input name="txtEmail" id="txtEmail" type="text" placeholder="您的邮箱" />
                    </li>
                    <li>
                        <textarea id="txtMessage" name="txtMessage" placeholder="请填写您的留言..."></textarea>
                    </li>
                    <li>
                        <asp:Button ID="btnTijiao" runat="server" Text="提交" OnClientClick="return checkform();" OnClick="btnTijiao_Click" />
                    </li>
                </ul>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            function checkform() {
                var fTitle = document.getElementById("txtTitle").value;
                if (fTitle.length == 0) {
                    alert("请输入您的姓名！");
                    document.getElementById("txtTitle").focus();
                    return false;
                }
                var fEmail = document.getElementById("txtEmail").value;
                if (fEmail != "" && fEmail.length > 0 && isEmail(fEmail)) {
                } else {
                    alert("请输入您的姓名！");
                    document.getElementById("txtEmail").focus();
                    return false;
                }
                var fmessage = document.getElementById("txtMessage").value;
                if (fmessage.length == 0) {
                    alert("请填写您的留言......！");
                    document.getElementById("txtMessage").focus();
                    return false;
                }
            }
            function isEmail(str) {
                var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
                return reg.test(str);
            }
        </script>
    </form>
</body>
</html>
