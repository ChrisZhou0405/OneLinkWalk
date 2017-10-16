<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="KingTop.WEB.En.search" %>

<%@ Register Src="~/En/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/En/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/En/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>










<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" id="Meta" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Header runat="server" id="Header" />
        <div class="mains mt148">
            <div class="wrap">
                <div class="searched">
                    <h3><span>Search“<%=key %>”find the relevant information<%=sum%>Article</span>SEARCH</h3>
                    <ul>
                        <asp:Repeater ID="rptlist" runat="server">
                            <ItemTemplate>
                                <li><a href="<%# GetLink(Eval("ID").ToString(),Eval("NodeCode").ToString()) %>"><%#GetString(Eval("Title").ToString(),key) %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" id="Footer" />
        <!--footer end-->
       
        <script src="/En/js/jquery.js"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
    </form>
</body>
</html>
