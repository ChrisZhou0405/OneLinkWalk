<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.help.index" %>

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
                <div class="title">网站地图 </div>
                <ul class="siteList">
                    <li>
                        <h2><a href="/shopping/index.aspx">购物指南</a></h2>
                        <asp:Repeater ID="RptRelated" runat="server">
                            <ItemTemplate>
                                <a href="/shopping/index.aspx?id=<%#Eval("ID") %>"><%#Eval("Title")%></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </li>
                    <li>
                        <h2><a href="/cate/index.aspx">美食荟萃</a></h2>
                        <a href="/cate/index.aspx">全部美食</a>
                        <a href="/cate/index.aspx?sx=1">亚洲美食</a>
                        <a href="/cate/index.aspx?sx=2">中式佳肴</a>
                        <a href="/cate/index.aspx?sx=3">西方美馔</a>
                        <a href="/cate/index.aspx?sx=4">轻便美食/甜点</a>
                    </li>
                    <li>
                        <h2><a href="/activity/index.aspx">活动推介</a></h2>
                        <a href="/activity/index.aspx?sx=0">最新活动</a>
                        <a href="/activity/index.aspx?sx=1">活动日志</a>
                    </li>
                    <li>
                        <h2><a href="/vip/index.aspx">会员专区</a></h2>
              <%--          <a href="../vip/index.html">会员招募</a>
                        <a href="../vip/index.html">会员专属优惠</a>
                        <a href="../vip/index.html">积分换礼</a>--%>
                    </li>
                    <li>
                        <h2><a href="/serve/index.aspx">服务与设施</a></h2>
                        <a href="/serve/index.aspx?sx=0">咨询中心</a>
                        <a href="/serve/index.aspx?sx=1">停车服务</a>
                        <a href="/serve/index.aspx?sx=2">ATM设施</a>
                        <a href="/serve/index.aspx?sx=3">电动汽车充电</a>
                        <a href="/serve/index.aspx?sx=4">母婴室</a>
                    </li>
                    <li>
                        <h2><a href="/about/index.aspx">关于我们</a></h2>
                        <a href="/about/index.aspx">项目概述</a>
                        <a href="/about/traffic.aspx">到达方式</a>
                        <a href="/about/contact.aspx">联系我们</a>
                        <a href="/about/lease.aspx">场地与商铺租赁</a>
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
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
            jQuery(".picScroll5").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 4, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
    </form>
</body>
</html>
