﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.En.about.index" %>

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
                <div class="title">Project Overview </div>
                <div class="tab_tit atit">
                    <asp:Repeater ID="rptmonth" runat="server">
                        <ItemTemplate>
                            <a <%#Container.ItemIndex + 1 == 1?"class='t_on'":"" %> href="javascript:;"><%#Eval("Title") %></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <asp:Repeater ID="rptontent" runat="server">
                    <ItemTemplate>
                        <div class="tab_txt" <%#Container.ItemIndex+1==1?"style='display:block'":"style='display:none;'" %>>
                            <%#Eval("ProjectIntro") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
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