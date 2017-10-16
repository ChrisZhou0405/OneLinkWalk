<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="KingTop.WEB.En.about.contact" %>

<%@ Register Src="~/En/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/En/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/En/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




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
                <div class="title">Contact us </div>
                <div class="itmeTxt ml_0 m0">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104007003001")%>
                </div>
                <ul class="tabList list2">
                    <li>
                        <input name="txtTitle" id="txtTitle" type="text" placeholder="Your Name" />
                    </li>
                    <li>
                        <input name="txtEmail" id="txtEmail" type="text" placeholder="Your Mailbox" />
                    </li>
                    <li>
                        <textarea id="txtMessage" name="txtMessage" placeholder="Please fill in your message..."></textarea>
                    </li>
                    <li>
                        <asp:Button ID="btnTijiao" runat="server" Text="Submit" OnClientClick="return checkform();" OnClick="btnTijiao_Click" />
                    </li>
                </ul>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="/En/js/jquery.js" type="text/javascript"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            function checkform() {
                var fTitle = document.getElementById("txtTitle").value;
                if (fTitle.length == 0) {
                    alert("Please enter your name！");
                    document.getElementById("txtTitle").focus();
                    return false;
                }
                var fEmail = document.getElementById("txtEmail").value;
                if (fEmail != "" && fEmail.length > 0 && isEmail(fEmail)) {
                } else {
                    alert("Please enter your name！");
                    document.getElementById("txtEmail").focus();
                    return false;
                }
                var fmessage = document.getElementById("txtMessage").value;
                if (fmessage.length == 0) {
                    alert("Please fill in your message......！");
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
