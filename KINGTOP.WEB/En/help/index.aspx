<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.En.help.index" %>

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
                <div class="title">Sitemap </div>
                <ul class="siteList">
                    <li>
                        <h2><a href="/En/shopping/index.aspx">Shopping</a></h2>
                        <asp:Repeater ID="RptRelated" runat="server">
                            <ItemTemplate>
                                <a href="/En/shopping/index.aspx?id=<%#Eval("ID") %>"><%#Eval("Title")%></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </li>
                    <li>
                        <h2><a href="/En/cate/index.aspx">Dining</a></h2>
                        <a href="/En/cate/index.aspx">All Delights</a>
                        <a href="/En/cate/index.aspx?sx=1">Asian Delights</a>
                        <a href="/En/cate/index.aspx?sx=2">Chinese Cuisine</a>
                        <a href="/En/cate/index.aspx?sx=3">Western Delicacies</a>
                        <a href="/En/cate/index.aspx?sx=4">Deli / Dessert</a>
                    </li>
                    <li>
                        <h2><a href="/En/activity/index.aspx">ActivitiesPromotion</a></h2>
                        <a href="/En/activity/index.aspx?sx=0">EventsPromotions</a>
                        <a href="/En/activity/index.aspx?sx=1">EventCalendar</a>
                    </li>
                    <li>
                        <h2><a href="/En/vip/index.aspx">Members Only</a></h2>
              <%--          <a href="../vip/index.html">会员招募</a>
                        <a href="../vip/index.html">会员专属优惠</a>
                        <a href="../vip/index.html">积分换礼</a>--%>
                    </li>
                    <li>
                        <h2><a href="/En/serve/index.aspx">ServicesFacilities</a></h2>
                        <a href="/En/serve/index.aspx?sx=0">Information</a>
                        <a href="/En/serve/index.aspx?sx=1">Parking</a>
                        <a href="/En/serve/index.aspx?sx=2">ATM</a>
                        <a href="/En/serve/index.aspx?sx=3">ElectricCarCharging</a>
                        <a href="/En/serve/index.aspx?sx=4">Infant Room</a>
                    </li>
                    <li>
                        <h2><a href="/En/about/index.aspx">About Us</a></h2>
                        <a href="/En/about/index.aspx">About OneLink Walk</a>
                        <a href="/En/about/traffic.aspx">How to Get There</a>
                        <a href="/En/about/contact.aspx">Contact Us</a>
                        <a href="/En/about/lease.aspx">Leasing</a>
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
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
            jQuery(".picScroll5").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 4, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
    </form>
</body>
</html>
