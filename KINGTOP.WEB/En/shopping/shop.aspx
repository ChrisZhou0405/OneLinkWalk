<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop.aspx.cs" Inherits="KingTop.WEB.En.shopping.shop" %>

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
        <!-- backTop end -->
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->
        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <div class="title">推荐店铺</div>
                <ul class="sList">
                    <asp:Repeater ID="RptIsRecommd" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="/En/shopping/detail.aspx?nid=<%#Eval("ID") %>">
                                    <img src="/UploadFiles/Images/<%#Eval("Banner")%>" alt="" width="600px" height="257" />
                                    <span><%#Eval("Title")%></span>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <div class="more"><a href="/En/shopping/index.aspx">查看更多</a></div>

                <div class="hotShop">
                    <h1><span>本季热店</span></h1>
                    <!-- picScroll end -->
                    <div class="picScroll3">
                        <div class="hd">
                        </div>
                        <div class="bd">
                            <span class="next2"></span>
                            <span class="prev2"></span>
                            <ul>
                                <asp:Repeater ID="rptIsHot" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href="/En/shopping/detail.aspx?nid=<%#Eval("ID") %>">
                                                <img src="/UploadFiles/Images/<%#Eval("Banner")%>" alt="" width="600px" height="257" />
                                                <span><%#Eval("Title")%></span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <!-- picScroll 2 end -->
                </div>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="/En/js/jquery.js" type="text/javascript"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            jQuery(".picScroll3").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 2, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
    </form>
</body>
</html>
