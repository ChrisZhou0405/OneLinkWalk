<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="errorpage.aspx.cs" Inherits="KingTop.WEB.errorpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>404 出错了！</title>
</head>
<style type="text/css">
    html, body, div, h1, h2, h3, h4, h5, h6, ul, ol, dl, li, dt, dd, p, blockquote, pre, form, fieldset, table, th, td, span, input, textarea
    {
        margin: 0;
        padding: 0;
    }
    body
    {
        font-size: 12px;
        line-height: 18px;
        color: #777;
    }
    li, ol
    {
        list-style: none;
    }
    ins
    {
        text-decoration: none;
    }
    i, em
    {
        font-style: normal;
    }
    a
    {
        text-decoration: none;
        color: #5a5a5a;
    }
    a:hover
    {
        cursor: pointer;
        text-decoration: none;
    }
    a:active
    {
        star: expression(this.onFocus=this.blur());
    }
    :focus
    {
        outline: 0;
    }
    .clear
    {
        clear: both;
        line-height: 0px;
        overflow: hidden;
        zoom: 1;
        font-size: 0px;
        content: '.';
    }
    a img
    {
        border: none;
    }
    .clear
    {
        clear: both;
        font-size: 0px;
        line-height: 0px;
    }
    .Error
    {
        width: 780px;
        height: 500px;
        margin: 50px auto 50px auto;
        background: url(/404cn.jpg);
    }
    .Error_Link
    {
        margin: 237px 0 0 420px;
        float: left;
    }
    .Error_Link a
    {
        background: url(/dise.gif) no-repeat left center;
        display: block;
        padding-left: 15px;
        font-size: 14px;
        font-family: "微软雅黑";
        line-height: 24px;
    }
    .Error_Link a:hover
    {
        font-size: 15px;
    }
</style>
<body>
    <div class="Error">
        <div class="Error_Link">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <a href="<%#((System.Data.DataRow)Container.DataItem)["linkUrl"]%>" title="HOME">
                        <%#((System.Data.DataRow)Container.DataItem)["NodeName"]%></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</body>
</html>
