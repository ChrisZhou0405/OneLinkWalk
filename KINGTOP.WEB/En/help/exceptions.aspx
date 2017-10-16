<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exceptions.aspx.cs" Inherits="KingTop.WEB.En.help.exceptions" %>

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
                <div class="title">免责条款 </div>
                <ul class="crList">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104008001")%>
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
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
            jQuery(".picScroll5").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 4, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
    </form>
</body>
</html>
