<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="KingTop.WEB.activity.detail" %>

<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Controls/share.ascx" TagPrefix="uc1" TagName="share" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no, minimum-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control" />
    <meta content="no-cache" http-equiv="pragma" />
    <meta content="0" http-equiv="expires" />
    <meta content="telephone=no, address=no" name="format-detection" />
    <meta name="revised" content="Monday, April 25th, 2016, 15:00 pm" />
    <meta name="author" content="xiaoxiao" />
    <title><%=PageTitle%></title>
    <meta name="keywords" content=<%=PageKeyWords%> />
    <meta name="description" content=<%=PageDescription%> />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
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
                <asp:Repeater ID="rptNews" runat="server">
                    <ItemTemplate>
                        <div class="title">
                            <%# Eval("Title") %> <span><i><%# Eval("ActivityTime") %></i>
                                分享:
                                    <a id="awx" class="ds_wx"></a>
                                    <%--     <a href="#" class="ds_en"></a>--%>
                                    <a id="aqq"  class="ds_qq"></a>
                                    <a id="wb" class="ds_sn"></a>
                                    <a id="adp"  class="ds_bou"></a>

                                <script type="text/javascript" src="http://v3.jiathis.com/code/jia.js" charset="utf-8"></script>
                            </span>
                        </div>
                        <div class="deTxt">
                            <%# Eval("Content") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <div class="overbg"></div>
        <div class="lMap">
            <h1 class="close"></h1>
            <img src="../images/l_map.png" alt="">
        </div>
        <!--lMap end-->
        <script src="../js/share.js"></script>
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
    </form>
    <uc1:share runat="server" ID="share" />
    <script src="../js/share.js"></script>
</body>
</html>
