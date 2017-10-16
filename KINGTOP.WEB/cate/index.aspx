<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.cate.index" %>

<%@ Register Src="~/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <div class="tab_tit2 ctit">
                    <a class="<%=sx==null?"t_on2":"" %> fw1" href="/cate/index.aspx"><i></i>全部美食</a>
                    <a class="fw2 <%=sx=="1"?"t_on2":"" %>" href="/cate/index.aspx?sx=1"><i></i>亚洲美食</a>
                    <a class="fw3 <%=sx=="2"?"t_on2":"" %>" href="/cate/index.aspx?sx=2"><i></i>中式佳肴</a>
                    <a class="fw4 <%=sx=="3"?"t_on2":"" %>" href="/cate/index.aspx?sx=3"><i></i>西方美馔</a>
                    <a class="fw5 <%=sx=="4"?"t_on2":"" %>" href="/cate/index.aspx?sx=4"><i></i>轻便美食/甜点</a>
                </div>
                <div class="tab_txt" >
               <div class="banCate">
                     <%--  <img src="<%=ImgURl %>" alt="" />--%>

                       <img src="/UploadFiles/images/pagebanner.jpg" alt="" />
                    </div>
                    <ul class="logos cLogo" id="cLogo_1">
                        <asp:Repeater ID="RptAllFoodList" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt=""></a>
                                    <div class="subClogos">
                                        <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                        <div class="subLTxt">
                                            <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                            <span>
                                                <i>店铺名称：<%#Eval("Title")%></i>
                                                <i>位置：<%#Eval("ShopNo")%></i>
                                                <i>电话：<%#Eval("TelPhone")%></i>
                                                <i>销售产品：<%#Eval("SalesPro")%></i>
                                                <i>网址：<%#Eval("SiteURL")%></i>
                                            </span>
                                            <p><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 180, "...") %></p>
                                            <a href="detail.aspx?nid=<%#Eval("id") %>">了解详情</a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=AllFoodPageHtml %>
                    </div>
                </div>

                <div class="tab_txt">
                    <div class="banCate">
                       <img src="<%=ImgURl %>" alt="" />
                    </div>
                    <ul class="logos cLogo" id="cLogo_2">
                        <asp:Repeater ID="rptAreaFood" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt=""></a>
                                    <div class="subClogos">
                                        <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                        <div class="subLTxt">
                                            <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                            <span>
                                                <i>店铺名称：<%#Eval("Title")%></i>
                                                <i>位置：<%#Eval("ShopNo")%></i>
                                                <i>电话：<%#Eval("TelPhone")%></i>
                                                <i>销售产品：<%#Eval("SalesPro")%></i>
                                                <i>网址：<%#Eval("SiteURL")%></i>
                                            </span>
                                            <p><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 180, "...") %></p>
                                            <a href="detail.aspx?nid=<%#Eval("id") %>">了解详情</a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=AreaFoodHtml %>
                    </div>
                </div>
                <!-- tab_txt 0 end  -->
                <div class="tab_txt">
                    <div class="banCate">
                        <img src="<%=ImgURl %>" alt="" />
                    </div>
                    <ul class="logos cLogo" id="cLogo_3">
                        <asp:Repeater ID="rptChinaFood" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt=""></a>
                                    <div class="subClogos">
                                        <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                        <div class="subLTxt">
                                            <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                            <span>
                                                <i>店铺名称：<%#Eval("Title")%></i>
                                                <i>位置：<%#Eval("ShopNo")%></i>
                                                <i>电话：<%#Eval("TelPhone")%></i>
                                                <i>销售产品：<%#Eval("SalesPro")%></i>
                                                <i>网址：<%#Eval("SiteURL")%></i>
                                            </span>
                                            <p><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 180, "...") %></p>
                                            <a href="detail.aspx?nid=<%#Eval("id") %>">了解详情</a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=ChinaFoodHtml %>
                    </div>
                </div>
                <!-- tab_txt 1 end  -->
                <div class="tab_txt">
                    <div class="banCate">
                        <img src="<%=ImgURl %>" alt="" />
                    </div>
                    <ul class="logos cLogo" id="cLogo_4">
                        <asp:Repeater ID="rptEastFood" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt=""></a>
                                    <div class="subClogos">
                                        <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                        <div class="subLTxt">
                                            <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                            <span>
                                                <i>店铺名称：<%#Eval("Title")%></i>
                                                <i>位置：<%#Eval("ShopNo")%></i>
                                                <i>电话：<%#Eval("TelPhone")%></i>
                                                <i>销售产品：<%#Eval("SalesPro")%></i>
                                                <i>网址：<%#Eval("SiteURL")%></i>
                                            </span>
                                            <p><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 180, "...") %></p>
                                            <a href="detail.aspx?nid=<%#Eval("id") %>">了解详情</a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=EastFoodHtml %>
                    </div>
                </div>
                <!-- tab_txt 2 end  -->
                <div class="tab_txt">
                    <div class="banCate">
                        <img src="<%=ImgURl %>" alt="" />
                    </div>
                    <ul class="logos cLogo" id="cLogo_5">
                        <asp:Repeater ID="rptSweetFood" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt=""></a>
                                    <div class="subClogos">
                                        <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                        <div class="subLTxt">
                                            <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                            <span>
                                                <i>店铺名称：<%#Eval("Title")%></i>
                                                <i>位置：<%#Eval("ShopNo")%></i>
                                                <i>电话：<%#Eval("TelPhone")%></i>
                                                <i>销售产品：<%#Eval("SalesPro")%></i>
                                                <i>网址：<%#Eval("SiteURL")%></i>
                                            </span>
                                            <p><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 180, "...") %></p>
                                            <a href="detail.aspx?nid=<%#Eval("id") %>">了解详情</a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=SweetFoodHtml %>
                    </div>
                </div>
                <!-- tab_txt 3 end  -->
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
        <script>
            $('.serveList li').click(function () {

            });
            $('.tab_tit2 a').click(function () {
                var t_n2 = $(this).index();
                $(this).addClass('t_on2').siblings().removeClass('t_on2');

                $('.tab_txt').eq(t_n2).stop(true, true).fadeIn().siblings('.tab_txt').stop(true, true).fadeOut();
            });
            $(document).ready(function () {
               // navSub();
                reSize();
                sideBar();
                logos();
                lMap();

                filtrateGift();
                $(window).resize(function () {
                    reSize();
                });
                $('.timesList li:nth-child(3n)').css('margin-right', '0');
                $("#ban_no").hide(0);
                wxShow();
                langShow();
            });



            var sx = "<%=sx%>";
            $(function () {
                if (sx == "") {
                    sx = 0;

                }
                var num = parseInt(sx) + 1;
                $(".fw" + num).attr("class", "fw" + num + " t_on2");

                $(".tab_txt").eq(num-1).attr("style", "display:block")
                //alert($(".tab_txt").eq(0).attr("class"));
            })
        </script>
    </form>
</body>
</html>
