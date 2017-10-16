<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.En.serve.index" %>

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
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->

        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <ul class="serveList">
                    <li class="sOn">
                        <img src="/En/images/ico/navs-02.png" alt="" />Information <i></i></li>
                    <li class="nsbg">
                        <img src="/En/images/ico/navs-03.png" alt="" />Parking <i></i></li>
                    <li class="nsbg-02">
                        <img src="/En/images/ico/navs-05.png" alt="" />ATM <i></i></li>
                    <li class="nsbg-03">
                        <img src="/En/images/ico/navs-04.png" alt="" /><span>Electric Vehicle<br /> Charging Service</span>
                        <i></i></li>
                    <li class="nsbg-04">
                        <img src="/En/images/ico/navs.png" alt="" />Infant Room <i></i></li>
                </ul>
                <div class="tab_txt" style="display: block;">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104006001")%>
                </div>
                <!-- tab_txt 0 end  -->
                <div class="tab_txt">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104006002")%>
                </div>
                <!-- tab_txt 1 end  -->
                <div class="tab_txt">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104006003")%>
                </div>
                <!-- tab_txt 2 end  -->
                <div class="tab_txt">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104006004")%>
                </div>
                <!-- tab_txt 3 end  -->
                <div class="tab_txt">
                    <%=KingTop.Common.Tools.GetSinglePageContent("104006005")%>
                </div>
                <!-- tab_txt 3 end  -->
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="/En/js/jquery.js" type="text/javascript"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
    </form>
</body>
</html>
<script>
    var sx = "<%=sx%>";
    $(function () {
        if (sx != "") {
            $(".serveList").children("li").eq(sx).click();
        }
    })
</script>