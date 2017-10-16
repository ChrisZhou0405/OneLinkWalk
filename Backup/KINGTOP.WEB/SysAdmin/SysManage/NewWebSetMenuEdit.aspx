<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewWebSetMenuEdit.aspx.cs" Inherits="KingTop.WEB.SysAdmin.SysManage.NewWebSetMenuEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建站点</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
    <script type="text/javascript" src="../js/listcheck.js"></script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>    
    <script type="text/javascript" src="../js/common.js"></script>
    
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
<script type="text/javascript" src="../js/skin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat ="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>
        <br />
        <div style="float:left ">说明：修改完毕后提交，可以实现批量修改栏目</div><div><asp:button ID="Button1" runat="server" Text=" 确定 " CssClass="abtn" 
            onclick="btn_Save_Click" /></div>
        <table border=0 cellpadding=0 cellspacing=1 bgcolor="#CCCCCC" style="width:100%">
            <tr align="center" height="30px">
                <td bgcolor="#E6F1FE">栏目名称</td>
                <td bgcolor="#E6F1FE">栏目英文名</td>
                <td bgcolor="#E6F1FE">栏目目录名</td>
                <td bgcolor="#E6F1FE">前台程序路径</td>
                <td bgcolor="#E6F1FE">前台栏目自定义链接</td>
                <td bgcolor="#E6F1FE">页面标题(SEO)</td>
                <td bgcolor="#E6F1FE">关键字(SEO)</td>
                <td bgcolor="#E6F1FE">描述(SEO)</td>
                <td bgcolor="#E6F1FE">删除</td>
            </tr>
            <asp:Repeater ID="rptInfo" runat="server">
            <ItemTemplate>
                <tr align="center">
                    <td bgcolor="#FFFFFF" style="padding:2px"><input name="<%#Eval("NodeCode")%>_1" style="width:80px" value="<%#Eval("NodeName")%>"/></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><input name="<%#Eval("NodeCode")%>_2" style="width:80px" value="<%#Eval("NodelEngDesc")%>"/></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><input name="<%#Eval("NodeCode")%>_3" style="width:80px" value="<%#Eval("NodeDir")%>"/></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><input name="<%#Eval("NodeCode")%>_4" style="width:120px" value="<%#Eval("SubDomain")%>"/></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><input name="<%#Eval("NodeCode")%>_5" style="width:120px" value="<%#Eval("LinkURL")%>"/></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><textarea name="<%#Eval("NodeCode")%>_6" style="width:200px;height:80px"><%#Eval("PageTitle")%></textarea></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><textarea name="<%#Eval("NodeCode")%>_7" style="width:200px;height:80px"><%#Eval("Meta_Keywords")%></textarea></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><textarea name="<%#Eval("NodeCode")%>_8" style="width:200px;height:80px"><%#Eval("Meta_Description")%></textarea></td>
                    <td bgcolor="#FFFFFF" style="padding:2px"><a href="?act=d&NodeCode=<%=NodeCode %>&id=<%#Eval("NodeID") %>&parentNodeCode=<%=Request.QueryString["parentNodeCode"] %>" class ="abtn">
                        删除</a></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
        <br />
        <input type="hidden" id="hidNodeCodeList" runat ="server" />
        <asp:button ID="btn_Save" runat="server" Text=" 确定 " CssClass="abtn" 
            onclick="btn_Save_Click" />
            <br />
        
        <br clear="left" />
    </div>
    </form>
</body>
</html>
