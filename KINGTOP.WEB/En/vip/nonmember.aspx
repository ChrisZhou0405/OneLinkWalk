<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nonmember.aspx.cs" Inherits="KingTop.WEB.En.vip.nonmember" %>

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
        <uc1:Header runat="server" ID="Header" />
        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <div class="title">Member recruitment </div>
                <div class="tab_tit"><a class="t_on" href="javascript:;">Membership Card</a><a href="javascript:;">Enjoy the card</a></div>
                <div class="tab_txt" style="display: block;">
                    <div class="cardBox">
                        <div class="picScroll4 nonc">
                            <div class="hd"></div>
                            <div class="bd">
                                <span class="next1"></span>
                                <span class="prev1"></span>
                                <ul>
                                    <asp:Repeater ID="rptli" runat="server">
                                        <ItemTemplate>
                                            <li><a href="<%#Eval("Links") %>">
                                                <img src="/UploadFiles/images/<%#Eval("BigImg")%>" alt=""></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <!-- picScroll 4 end -->
                        <div class="cardTxt">
                            <%=KingTop.Common.Tools.GetSinglePageContent("104005001001002")%>
                                                                         
                        </div>
                    </div>
                    <!-- cardBox end -->
                    <div class="clause">
                        <h4>Terms and Conditions</h4>
                        <asp:Repeater ID="rpttabcon" runat="server">
                            <ItemTemplate>
                                <a <%#Container.ItemIndex + 1 == 1?"class='cOn'":"" %> href="javascript:;"><%#Eval("NodelEngDesc") %></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="clauseTxt" style="display: block">
                        <%=KingTop.Common.Tools.GetSinglePageContent("104005003004001")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("104005003004002")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("104005003004003")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("104005003004004")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("104005003004005")%>
                    </div>
                </div>
                <!-- tab_txt 0 end  -->
                <div class="tab_txt">
                    <div class="suprCard">
                        <img src="/En/images/suprCard.jpg" alt="">
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
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
        </script>
    </form>
</body>
</html>
