<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.En.activity.index" %>

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
                <div class="title">Activities Promotion </div>
                <div class="tab_tit"><a class="t_on" href="/En/activity/index.aspx?sx=0">Latest Events</a><a href="/En/activity/index.aspx?sx=1">Activity log</a></div>
                <div class="tab_txt" style="display: block;">
                    <div class="ban">
                        <div class="bd">
                            <ul>
                                <asp:Repeater ID="rptbanner" runat="server">
                                    <ItemTemplate>
                                        <li><a href="<%#Eval("Links") %>" style="background: url(/UploadFiles/images/<%#Eval("BigImg")%>) 50% no-repeat; background-size: cover;"></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div class="hd">
                            <ul>
                                <asp:Repeater ID="rptli" runat="server">
                                    <ItemTemplate>
                                        <li></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <a class="prev"></a><a class="next"></a>
                    </div>
                    <!-- ban end -->
                    <div class="picScroll6">
                        <div class="hd"></div>
                        <div class="bd">
                            <span class="next2"></span>
                            <span class="prev2"></span>
                            <ul>
                                <asp:Repeater ID="rptPicScroll" runat="server">
                                    <ItemTemplate>
                                        <li><a href="/En/activity/detail.aspx?nid=<%#Eval("ID")%>">
                                            <img src="/UploadFiles/images/<%#Eval("TitleImg")%>" alt=""><span><%#Eval("Title") %></span></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <!-- picScroll 2 end -->
                </div>
                <!-- tab_txt 0 end  -->
                <div class="tab_txt">
                    <div class="times">
                        <asp:DropDownList ID="DDlYear" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="DDLMonth" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnsubmit" runat="server" Text="Find past activity" OnClick="btnsubmit_Click" />
                    </div>
                    <ul class="timesList">
                        <asp:Repeater ID="rptNews" runat="server">
                            <ItemTemplate>
                                <li><a href="detail.aspx?nid=<%#Eval("ID")%>">
                                    <img src="/uploadfiles/images/<%# Eval("TitleImg") %>" alt="">
                                    <span><%# Eval("Title") %><i><%# Eval("ActivityTime") %></i></span>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=PageHtml %>
                    </div>
                </div>
                <!-- tab_txt 1 end  -->
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="/En/js/jquery.js" type="text/javascript"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            jQuery(".ban").slide({ mainCell: ".bd ul", autoPlay: true, effect: "fold", pnLoop: false });
            jQuery(".picScroll6").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 3, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
        <script>
            var sx = "<%=sx%>";
            $(function () {
                if (sx != "") {
                    $(".tab_tit").children("a").eq(sx).click();
                }
            })
        </script>
    </form>
</body>
</html>
