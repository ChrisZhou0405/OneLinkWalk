<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Category.List" %>

<%@ Import Namespace="KingTop.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>商品分类列表</title>
    <link href="../css/public.css" rel="stylesheet" type="text/css" />
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.4.2.min.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script type="text/javascript" src="Category.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h4>
        <%GetPageNav(NodeCode);%>
            <%--位置： <a href="/sysadmin/console/Index.aspx">管理首页</a><a> &gt;</a> <a href="/sysadmin/Categorys/List.aspx?NodeCode=<%=NodeCode %>&IsFirst=1">产品分类</a><a> &gt;</a>
            <span class="breadcrumb_current" style="">分类列表</span>--%>
        </h4>
        <div class="function">
            <asp:Button ID="Button1" runat="server" Text="添加分类" CssClass="btn"  OnClick="btnAdd_Click" />
        </div>
        <ul class="ulheader">
            <li class="li1" style="text-align:center;">分类名称</li>
            <li class="li2">添加子类</li>
            <li class="li3">是否显示</li>
            <li class="li4">首页显示</li>
            <li class="li5" title="点击数字，在显示的输入框中输入排序号，实现排序">排序(点击数字编辑排序)</li>
            <li class="li6">操作</li>
        </ul>
    </div>
    </form>
</body>
</html>
