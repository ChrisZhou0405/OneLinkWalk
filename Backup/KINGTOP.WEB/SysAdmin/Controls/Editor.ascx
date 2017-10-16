<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Editor.ascx.cs" Inherits="KingTop.Web.Admin.Controls.Editor" %>
<asp:TextBox ID="txtEditorContent" runat="server" TextMode="MultiLine"></asp:TextBox>
<%=GetEditor()%>
<input type="button" value="文档导入" id="btnImport" style="
    float: right;
    margin: 10px;
    display:none;
" class="subButton" runat="server">