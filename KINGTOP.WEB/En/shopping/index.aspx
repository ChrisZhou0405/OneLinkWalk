<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.En.shopping.index" %>

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
        <div class="mains mt148">
            <div class="wrap">
                <div class="mLeft">
                    <h1>Shopping</h1>
                    <ul class="sideBar">
                        <li class="s_on"><a class="si_1" href="javascript:;">By Category</a>
                            <div class="class_a" <%=Loacation ==""?"style=\"display: block;\"":"style=\"display: none;\"" %>>
                                <a href="/En/shopping/index.aspx" class="c_a0 ca_on0">All</a>
                                <asp:Repeater ID="RptRelated" runat="server">
                                    <ItemTemplate>
                                        <a href="/En/shopping/index.aspx?id=<%#Eval("ID") %>" class="c_a<%#Container.ItemIndex+1 %> <%#Eval("ID").ToString()==id?"ca_on"+(Container.ItemIndex+1)+"":"" %>"><%#Eval("Title")%></a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </li>
                        <li <%=ls.Contains(Loacation)?"class=\"s_on\"":"" %> ><a class="si_2" href="javascript:;">By A-Z</a>
                            <div class="letter" <%=ls.Contains(Loacation)?"style=\"display: block;\"":"" %> >
                                <span>
                                    <h2>A - D</h2>
                                    <p <%=ls1.Contains(Loacation)?"style=\"display: block;\"":"" %>>
                                        <i <%=Loacation.Equals("1")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=1'" >A</i>
                                        <i  <%=Loacation.Equals("2")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=2'" >B</i>
                                        <i  <%=Loacation.Equals("3")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=3'"> C</i>
                                        <i  <%=Loacation.Equals("4")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=4'">D</i></p>
                                </span>
                                <span>
                                    <h2>E - H</h2>
                                    <p <%=ls2.Contains(Loacation)?"style=\"display: block;\"":"" %>>
                                        <i  <%=Loacation.Equals("5")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=5'" >E</i>
                                        <i  <%=Loacation.Equals("6")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=6'" >F</i>
                                        <i  <%=Loacation.Equals("7")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=7'" >G</i>
                                        <i  <%=Loacation.Equals("8")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=8'" >H</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>I - L</h2>
                                    <p <%=ls3.Contains(Loacation)?"style=\"display: block;\"":"" %> >
                                        <i  <%=Loacation.Equals("9")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=9'" >I</i>
                                        <i  <%=Loacation.Equals("10")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=10'" >J</i>
                                        <i  <%=Loacation.Equals("11")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=11'">K</i>
                                        <i  <%=Loacation.Equals("12")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=12'">L</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>M - P</h2>
                                    <p <%=ls4.Contains(Loacation)?"style=\"display: block;\"":"" %> >
                                        <i  <%=Loacation.Equals("13")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=13'" >M</i>
                                        <i  <%=Loacation.Equals("14")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=14'">N</i>
                                        <i  <%=Loacation.Equals("15")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=15'">O</i>
                                        <i  <%=Loacation.Equals("16")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=16'">P</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>Q - T</h2>
                                    <p  <%=ls5.Contains(Loacation)?"style=\"display: block;\"":"" %>>
                                        <i  <%=Loacation.Equals("17")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=17'">Q</i>
                                        <i  <%=Loacation.Equals("18")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=18'" >R</i>
                                        <i  <%=Loacation.Equals("19")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=19'">S</i>
                                        <i  <%=Loacation.Equals("20")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=20'" >T</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>U - X</h2>
                                    <p  <%=ls6.Contains(Loacation)?"style=\"display: block;\"":"" %>>
                                        <i  <%=Loacation.Equals("21")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=21'" >U</i>
                                        <i   <%=Loacation.Equals("22")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=22'" >V</i>
                                        <i  <%=Loacation.Equals("23")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=23'" >W</i>
                                        <i  <%=Loacation.Equals("24")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=24'" >X</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>Y - Z</h2>
                                     <p <%=ls7.Contains(Loacation)?"style=\"display: block;\"":"" %>>
                                        <i  <%=Loacation.Equals("25")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=25'" >Y</i>
                                        <i  <%=Loacation.Equals("26")?"style=\"color:#634d3d;font-weight:bold\"":"" %> onclick="javascript:location='/En/shopping/index.aspx?Loacation=26'" >Z</i>
                                    </p>
                                </span>
                            </div>
                        </li>
                        <li><a class="si_3" href="javascript:;">By Location</a>
                            <div class="site">
                               <%-- <a href="site.aspx?locaiton=0">B1</a>
                                <a href="siteb.aspx?locaiton=1">L1</a>
                                <a href="sitec.aspx?locaiton=2">L2</a>
                                <a href="sited.aspx?locaiton=3">L3</a>
                                <a href="sitee.aspx?locaiton=4">L4</a>
                                <a href="sitef.aspx?locaiton=5">L5</a>--%>
                                 <a href="/En/shopping/site.aspx?locaiton=0">B1</a>
                                <a href="/En/shopping/site.aspx?locaiton=1">L1</a>
                                <a href="/En/shopping/site.aspx?locaiton=2">L2</a>
                                <a href="/En/shopping/site.aspx?locaiton=3">L3</a>
                                <a href="/En/shopping/site.aspx?locaiton=4">L4</a>
                                <a href="/En/shopping/site.aspx?locaiton=5">L5</a>

                            </div>
                        </li>
                        <li><a style="font-size:14px" class="si_4" href="/En/shopping/shop.aspx">Shop Recommendations</a></li>
                    </ul>
                </div>
                <!-- mLeft end -->
                <div class="mRight">
                    <div class="hidebg"></div>
                    <ul class="logos" id="logos">
                        <asp:Repeater ID="rptlist" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt=""></a>
                                    <div class="subLogos">
                                        <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                        <div class="subLTxt">
                                            <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                            <span>
                                                <i>Shop：<%#Eval("Title")%></i>
                                                <i>Location：<%#Eval("ShopNo")%></i>
                                                <i>Tel：<%#Eval("TelPhone")%></i>
                                                <i>Product：<%#Eval("SalesPro")%></i>
                                                <i>URL：<%#Eval("SiteURL")%></i>
                                            </span>
                                            <p style="width:100%;float:left"><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 180, "...") %></p>
                                            <a href="/En/shopping/detail2.aspx?nid=<%#Eval("id") %>&id=<%#Eval("type") %>">Details</a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="page">
                        <%=PageHtml %>
                    </div>
                </div>
                <!-- mRight end -->
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="/En/js/jquery.js"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
    </form>
</body>
</html>
