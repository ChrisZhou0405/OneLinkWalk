<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelManageCode.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Model.ModelManageCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <link rel="stylesheet" href="../css/template.css" type="text/css" />
    <link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/<%=Resources.Common.formValidationLanguage %>"></script>
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });
    </script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script type="text/javascript" src="../js/Common.js"></script>
</head>
<body>
    <form id="theForm" runat="server">
    <input type="hidden" id="hidLogTitle" runat="server" />
    <input type="hidden" id="hidFields" runat ="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %> &gt; <span class="breadcrumb_current"> 生成代码片段</span>
        </h4>
        <div id="con">
            <ul id="tags">
                <li class="selectTag"><a href="javascript:;">列表（最新N条）</a> </li>
                <li><a href="javascript:;">列表（分页）</a></li>
                <li><a href="javascript:;">详细内容</a></li>
                <li><a href="javascript:;">编辑（添加）</a></li>
                <li><a href="javascript:;">编辑（修改）</a></li>
            </ul>
            <div id="panel">
                <fieldset>
                    <dl>
                        <dt>选择列<br /><input type="checkbox" id="chkAll1" value="1" name="chkAll1" /> 全部</dt>
                        <dd>
                            <table border=0>
                           <asp:Repeater ID="rptField1" runat="server">
                            <ItemTemplate>
                                <%
                                    if (iLoop % 6 == 0)
                                    {
                                        if (iLoop == 0)
                                        {
                                            Response.Write("<tr>");
                                        }
                                        else
                                        {
                                            Response.Write ("</tr><tr>");
                                        }
                                    }
                                    iLoop++;
                                     %>
                                <td>
                                <input type="checkbox" name="chkField1" value="<%#Eval("Name") %>" <%#IsChecked1(Eval("Name").ToString()) %>/> <%#Eval("Name")%> (<%#Eval("FieldAlias")%>)
                                </td>
                            </ItemTemplate>
                           </asp:Repeater>
                           </tr>
                           </table>
                        </dd>
                    </dl>
                    <dl>
                        <dt>排序</dt>
                        <dd>
                           <asp:TextBox ID="txtOrder" runat="server"  Width="231px" Text ="Order By Orders Desc"></asp:TextBox> <font color=red>*</font>
                        </dd>
                    </dl>
                    
                    <dl>
                        <dt>top条数</dt>
                        <dd>
                           <asp:TextBox ID="txtTOPNum" runat="server"  Width="231px" Text="10"></asp:TextBox> <font color=red>*</font>
                        </dd>
                    </dl>
                    <dl>
                        <dt>条件</dt>
                        <dd>
                           <input type="text" name="txtWhere1" value="<%=Server.HtmlEncode("Where NodeCode='\"+nc+\"' AND IsDel=0 AND FlowState=99") %>"  style="width:600px" />
                        </dd>
                    </dl>
                    <dl style="display:none">
                        <dt>模版代码</dt>
                        <dd><asp:TextBox ID="txtTemplate1" TextMode="MultiLine" style="width:700px;height:80px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.cs代码片段<br />生成的cs代码片段</dt>
                        <dd> 
                            <asp:TextBox ID="txtCs1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox>
                        </dd>
                    </dl>
                    <div style="clear:left"></div>
                 </fieldset>
                 
                 <fieldset style="display: none;">
                     <dl>
                        <dt>选择列<br /><input type="checkbox" id="chkAll2" value="1" name="chkAll2" /> 全部</dt>
                        <dd>
                            <table border=0>
                           <asp:Repeater ID="rptField2" runat="server">
                            <ItemTemplate>
                                <%
                                    if (iLoop % 6 == 0)
                                    {
                                        if (iLoop == 0)
                                        {
                                            Response.Write("<tr>");
                                        }
                                        else
                                        {
                                            Response.Write ("</tr><tr>");
                                        }
                                    }
                                    iLoop++;
                                     %>
                                <td>
                                <input type="checkbox" name="chkField2" value="<%#Eval("Name") %>" <%#IsChecked2(Eval("Name").ToString()) %>/> <%#Eval("Name")%> (<%#Eval("FieldAlias")%>)
                                </td>
                            </ItemTemplate>
                           </asp:Repeater>
                           </tr>
                           </table>
                        </dd>
                    </dl>
                    <dl>
                        <dt>排序</dt>
                        <dd>
                           <asp:TextBox ID="txtOrder2" runat="server"  Width="231px" Text ="Orders Desc"></asp:TextBox> 不用填写 ORDER BY
                        </dd>
                    </dl>
                    <dl>
                        <dt>每页条数</dt>
                        <dd>
                           <asp:TextBox ID="txtPageSize" runat="server"  Width="231px" Text ="20"></asp:TextBox> <font color=red>*</font>
                        </dd>
                    </dl>
                    <dl>
                        <dt>条件</dt>
                        <dd>
                           <input type="text" name="txtWhere2" value="<%=Server.HtmlEncode("Where NodeCode='\"+nc+\"' AND IsDel=0 AND FlowState=99") %>"  style="width:600px" />
                        </dd>
                    </dl>
                    <dl style="display:none">
                        <dt>模版代码</dt>
                        <dd><asp:TextBox ID="txtTemplate2" TextMode="MultiLine" style="width:700px;height:80px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx2" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.cs代码片段<br />生成的cs代码片段</dt>
                        <dd> 
                            <asp:TextBox ID="txtCs2" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox>
                        </dd>
                    </dl>
                    <div style="clear:left"></div>
                 </fieldset>                 
                 
                 
                 
                <fieldset style="display: none;">                
                    <dl>
                        <dt>选择列<br /><input type="checkbox" id="chkAll3" value="1" name="chkAll3" /> 全部</dt>
                        <dd>
                            <table border=0>
                           <asp:Repeater ID="rptField3" runat="server">
                            <ItemTemplate>
                                <%
                                    if (iLoop % 6 == 0)
                                    {
                                        if (iLoop == 0)
                                        {
                                            Response.Write("<tr>");
                                        }
                                        else
                                        {
                                            Response.Write ("</tr><tr>");
                                        }
                                    }
                                    iLoop++;
                                     %>
                                <td>
                                <input type="checkbox" name="chkField3" value="<%#Eval("Name") %>" <%#IsChecked3(Eval("Name").ToString()) %>/> <%#Eval("Name")%> (<%#Eval("FieldAlias")%>)
                                </td>
                            </ItemTemplate>
                           </asp:Repeater>
                           </tr>
                           </table>
                        </dd>
                    </dl>
                    <dl style="display:none">
                        <dt>模版代码</dt>
                        <dd><asp:TextBox ID="txtTemplate3" TextMode="MultiLine" style="width:700px;height:80px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段（Repeater绑定）<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx3_1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.cs代码片段（Repeater绑定）<br />生成的cs代码片段</dt>
                        <dd> 
                            <asp:TextBox ID="txtCs3_1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox>
                        </dd>
                    </dl>
                    
                    <dl>
                        <dt>.aspx代码片段（变量方式）<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx3_2" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.cs代码片段（变量方式）<br />生成的cs代码片段</dt>
                        <dd> 
                            <asp:TextBox ID="txtCs3_2" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox>
                        </dd>
                    </dl>
                    
                    <div style="clear:left"></div>
                 </fieldset>                 
                 
                 
                 <fieldset style="display: none;">
                     <dl>
                        <dt>选择列<br /><input type="checkbox" id="chkAll4" value="1" name="chkAll4" checked/> 全部</dt>
                        <dd>
                            <table border=0>
                           <asp:Repeater ID="rptField4" runat="server">
                            <ItemTemplate>
                                <%
                                    if (iLoop % 6 == 0)
                                    {
                                        if (iLoop == 0)
                                        {
                                            Response.Write("<tr>");
                                        }
                                        else
                                        {
                                            Response.Write ("</tr><tr>");
                                        }
                                    }
                                    iLoop++;
                                     %>
                                <td>
                                <input type="checkbox" name="chkField4" value="<%#Eval("Name") %>" <%#IsChecked3(Eval("Name").ToString()) %>/> <%#Eval("Name")%> (<%#Eval("FieldAlias")%>)
                                </td>
                            </ItemTemplate>
                           </asp:Repeater>
                           </tr>
                           </table>
                        </dd>
                    </dl>
                    <dl style="display:none">
                        <dt>模版代码</dt>
                        <dd><asp:TextBox ID="txtTemplate" TextMode="MultiLine" style="width:700px;height:80px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.cs代码片段（客户端控件）<br />生成的cs代码片段</dt>
                        <dd> 
                            <asp:TextBox ID="txtCs4_1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段（客户端控件）<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx4_1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段（服务器端控件）<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx4_2" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    
                    
                    <div style="clear:left"></div>
                 </fieldset> 
                 
                 
                <fieldset style="display: none;">
                    <dl>
                        <dt>选择列<br /><input type="checkbox" id="chkAll5" value="1" name="chkAll5" checked/> 全部</dt>
                        <dd>
                            <table border=0>
                           <asp:Repeater ID="rptField5" runat="server">
                            <ItemTemplate>
                                <%
                                    if (iLoop % 6 == 0)
                                    {
                                        if (iLoop == 0)
                                        {
                                            Response.Write("<tr>");
                                        }
                                        else
                                        {
                                            Response.Write ("</tr><tr>");
                                        }
                                    }
                                    iLoop++;
                                     %>
                                <td>
                                <input type="checkbox" name="chkField5" value="<%#Eval("Name") %>" <%#IsChecked3(Eval("Name").ToString()) %>/> <%#Eval("Name")%> (<%#Eval("FieldAlias")%>)
                                </td>
                            </ItemTemplate>
                           </asp:Repeater>
                           </tr>
                           </table>
                        </dd>
                    </dl>
                    <dl style="display:none">
                        <dt>模版代码</dt>
                        <dd><asp:TextBox ID="txtTemplate5" TextMode="MultiLine" style="width:700px;height:80px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.cs代码片段（客户端控件）<br />生成的cs代码片段</dt>
                        <dd> 
                            <asp:TextBox ID="txtCs" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段（客户端控件）<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx5_1" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <dl>
                        <dt>.aspx代码片段（服务器端控件）<br />生成的aspx代码片段</dt>
                        <dd><asp:TextBox ID="txtAspx5_2" ReadOnly TextMode="MultiLine" style="width:700px;height:200px" runat="server"></asp:TextBox> </dd>
                    </dl>
                    <div style="clear:left"></div>
                 </fieldset>
            </div>
            <div class="Submit">
                <asp:Button ID="btnHidAction" runat="server" style="display:none" />
                <asp:Button ID="btnEdit" runat="server" CssClass="subButton" 
                    Text="确 定" OnClientClick="changeTabOne();" 
                    onclick="btnEdit_Click" />
                <input type="button" name="Submit422" Class="subButton" value="<%= Resources.Common.Back %>" onclick='location.href="ModelManageList.aspx?NodeCode=<%=NodeCode%>";'>
            </div>
        </div>
    </form>
    <script type="text/javascript"><%=jsMessage %></script>
</body>
</html>