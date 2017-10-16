<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="KingTop.WEB.search" %>

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
        <uc1:Header runat="server" ID="Header" />
        <div class="mains mt148">
            <div class="wrap">
                <div class="searched">
                    <h3><span>搜索“<%=key %>”找到相关信息<%=sum%>条</span>SEARCH</h3>
                    <ul>
                        <asp:Repeater ID="rptlist" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href="<%# GetLink(Eval("ID").ToString(),Eval("NodeCode").ToString()) %>">
                                        <%#GetString(Eval("Title").ToString(),key) %>

                                    </a>

                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="./js/jquery.js" type="text/javascript"></script>
        <script src="./js/jqnav.js" type="text/javascript"></script>
        <script src="./js/SuperSlide.js" type="text/javascript"></script>
    </form>
</body>
</html>
