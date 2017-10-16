<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicOperList.aspx.cs" Inherits="KingTop.WEB.SysAdmin.SysManage.PublicOperList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>模块管理</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
    <script type="text/javascript" src="../js/listcheck.js"></script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
        <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat ="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>

        <div class="function">
            <asp:Button ID="btnHidAction" runat="server" OnClick="btnDelete_Click" style="display:none" />
            <asp:Button ID="btnNew" runat="server"  Text="<%$Resources:Common,Add %>"
                OnClick="btnNew_Click" />
            <asp:Button ID="btnDelete"  runat="server" Text="<%$Resources:Common,Del %>" OnClientClick="selfconfirm({msg:'确定要执行删除操作吗？',fn:function(data){setAction(data)}});return false;"
              />
        </div>
        <ul class="ulheader">
            <li style="width: 5%; text-align:center">
                <input type="checkbox" name="checkBoxAll" id="checkBoxAll" value="" /></li>
            <li style="width: 15%">名称</li>
            <li style="width: 15%">英文名（操作码）</li>
            <li style="width: 15%">状态</li>
            <li style="width: 15%">操作</li>
        </ul>
        <asp:Repeater ID="rptModelList" runat="server">
            <ItemTemplate>
                <ul class="ulheader ulbody">
                    <li style="width: 5%;text-align:center">
                        <input type="checkbox" name="chkId" id="chkId" value='<%#Eval("OperName")%>' /></li>
                    <li style="width: 15%"><a href='<%#"PublicOperEdit.aspx?action=Edit&NodeCode="+NodeCode+"&NodeID="+NodeID+"&ID="+Eval("OperName").ToString()%>' id="Title<%# Eval("OperName")%>"><%#Eval("Title")%></a> </li>
                    <li style="width: 15%">
                        <%#Eval("OperName")%>
                    </li>

                    <li style="width: 15%">
                        <%#KingTop.Common.Utils.ParseState((Eval("IsValid").ToString()),"0")%>
                    </li>
                    <li style="width: 15%">
                    <a href='<%#"PublicOperEdit.aspx?action=Edit&NodeCode="+NodeCode+"&NodeID="+NodeID+"&ID="+Eval("OperName").ToString()%>' class="abtn">修改</a>
                    <asp:LinkButton ID="lnkbDelete" class="abtn" runat="server" OnCommand="ModelList_Del"
                           CommandName="deldp" ToolTip='<%#Eval("Title")%>' CommandArgument='<%#Eval("OperName") %>'
                           OnClientClick='selectThisRow();selfconfirm({msg:"确定要执行删除操作吗？",fn:function(data){setAction(data)}});return false;'>删除</asp:LinkButton> 
                       </li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
        
        <ul class="page">
            <webdiyer:AspNetPager ID="Split" runat="server" CssClass="page" PageSize="20" AlwaysShow="True" UrlPaging="true"
                ShowCustomInfoSection="left" CustomInfoSectionWidth="28%" ShowPageIndexBox="always"
                PageIndexBoxType="DropDownList" CustomInfoHTML="<%$Resources:Common,CustomInfoHTML %>" 
                HorizontalAlign="Center" NumericButtonCount="6">
            </webdiyer:AspNetPager>
        </ul>
        <br clear="left" />
    </div>
    </form>
</body>
</html>