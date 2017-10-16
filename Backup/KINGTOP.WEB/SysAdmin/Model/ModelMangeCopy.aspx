<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelMangeCopy.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Model.ModelMangeCopy" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>模型字段列表</title>
        <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
        <script type="text/javascript" src="../js/publicform.js"></script>
        <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../js/jquery.dialog.js"></script>
        <script type="text/javascript" src="../js/win.js"></script>
        <script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
        <script type="text/javascript" src="../js/Common.js"></script>
        <script src="../JS/listcheck.js" type="text/javascript"></script>
        <style type="text/css">.rowSelected{background-color:#efefef;} a{color:#000000;}</style>
        <script type="text/javascript">
            $(function() {
            $("#btnNew").click(function() { location.href = "FieldManageEdit.aspx?action=add&<%= urlParam%>"; })
                $("body").find("input").each(function() {
                    if ($(this).attr("id").indexOf("_btnEdit") > -1) {
                        $(this).click(function() { location.href = "FieldManageEdit.aspx?action=edit&id=" + $(this).attr("RecordID") + "&<%= urlParam%>"; })
                    }
                });
            });
</script>
<style type="text/css"> 
#searchContainer{<%--position:absolute;--%>top:0;width:100%}
#box{position:relative;}
</style> 
</head>
<body >
    <form id="Form1" name="theForm" method="post" runat="server">
    <input type="hidden" name="hidLogTitle" id="hidLogTitle" />
    <input type="hidden" name="hidQianZui" id="hidQianZui" value="<%=qianzui%>"/>
    <input type="hidden" name="hidFields" id="hidFields" runat ="server" />
    <input type="hidden" name="hidSysFields" id="hidSysFields" runat="server" />
    <div class="container">
        <h4>
            当前位置： 复制模型</h4>

        <div id="box">
        
        <div id="searchContainer">
            <span>
                模型名称:<asp:TextBox ID="txtName" runat="server"></asp:TextBox> (原名：<%=ModelName%>)
               &nbsp;  数据库表名:<asp:TextBox ID="txtdbTableName" runat="server"></asp:TextBox> (原名：<%=TableName %>,不用填写“<%=qianzui%>”)
            </span>
            <asp:Button ID="btnCopy" runat="server" Text="复制"  OnClick="btn_OnCopy"/>
            <input type="button" id="btnReturn" value="返回"  onclick="javascript:location.href='ModelManageList.aspx?NodeCode=<%=NodeCode %>';" />
        </div>
        </div>
        <ul class="ulheader">
            <li style="width: 30px; text-align:center" >
                <input type="checkbox" name="checkBoxAll" id="checkBoxAll" value="" /></li>
            <li style="width:200px;"><a href="javascript:sort('Name')">字段名</a></li>
            <li style="width:200px;">别名</li>
            <li style="width:200px;"><a href="javascript:sort('BasicField')">字段类型</a></li>
         </ul>
        <div id="FiledManageList">
        <asp:Repeater ID="rptModelField" runat="server">
            <ItemTemplate>
                <ul class="ulheader ulbody" onmouseover="$(this).addClass('rowSelected')" onmouseout="$(this).removeClass('rowSelected')">
                    <li style="width: 30px;text-align:center">
                        <input type="checkbox" name="chkId"  value='<%#Eval("Name")%>' readonly checked<%#Eval("IsSystemFiierd").ToString()=="True"?" disabled=true":"" %>/><%#Eval("IsSystemFiierd")%>
                    </li>
                    <li style="width:200px;"><span id="Title<%# Eval("ID")%>"><%#Eval("Name") %></span></li>
                    <li style="width:200px;"><%#Eval("FieldAlias")%></li>
                    <li style="width:200px;"><%#fieldManage.BasicType[Eval("BasicField")]%></li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
        </div>
    </div>
       
    <asp:hiddenfield ID="hdnUrlParm" runat="server"></asp:hiddenfield>
    <input type="hidden" id="IsListPage"  value="1"/>
    </form>
    <script type="text/javascript"><%=jsMessage %></script>
</body>
</html>

<script type="text/javascript">
    var IO = document.getElementById('searchContainer'), Y = IO, H = 0, IE6;
    IE6 = window.ActiveXObject && !window.XMLHttpRequest;
    while (Y) { H += Y.offsetTop; Y = Y.offsetParent };
    if (IE6)
        IO.style.cssText = "position:absolute;top:expression(this.fix?(document" +
        ".documentElement.scrollTop-(this.javascript||" + H + ")):0)";
    window.onscroll = function() {
        var d = document, s = Math.max(d.documentElement.scrollTop, document.body.scrollTop);
        if (s > H && IO.fix || s <= H && !IO.fix) return;
        if (!IE6) IO.style.position = IO.fix ? "" : "fixed";
        IO.fix = !IO.fix;
    };
    try { document.execCommand("BackgroundImageCache", false, true) } catch (e) { };
    //]]>
</script> 