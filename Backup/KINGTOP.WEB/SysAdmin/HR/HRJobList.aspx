<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRJobList.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRJobList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>职位管理</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
<link href="../css/dialog.css" rel="stylesheet" type="text/css" />
<script src="../JS/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="../js/jquery.dialog.js"></script>
<script type="text/javascript" src="../js/win.js"></script>
<script type="text/javascript" src="../js/publicform.js"></script>
<script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
<script src="../js/jquery-ui-1.8.14.custom.min.js" type="text/javascript"></script>
<script src="../js/commonsort.js" type="text/javascript"></script>
<script src="../js/common.js" type="text/javascript"></script>
<script src="../js/listchecksort.js" type="text/javascript"></script>
</head>
<body>
<form id="theForm" name="theForm" method="post" runat="server">
 <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat ="server" />
 <!--排序用-->
 <input type="hidden" name="hdnTableName" id="hdnTableName" value="K_HRJob" /> 
 <!--点击删除按钮（确认框）用-->
 <asp:Button ID="btnHidAction" runat="server" OnClick="btnDel_Click" style="display:none" />
<div class="container">
     <h4>位置： <%GetPageNav(NodeCode); %></h4> 
     
     <div id="searchContainer">
           <p>
                职位名称
                <asp:TextBox ID="txtTitle" runat="server" Widcth="100px" >
                </asp:TextBox>
                
                职位类型
                <asp:DropDownList ID="ddlJobType" runat="server">
                </asp:DropDownList>
                
                工作单位
                <asp:DropDownList ID="ddlWorkUnit" runat="server">
                </asp:DropDownList>
                               
                 工作地点
                <asp:DropDownList ID="ddlWorkPlace" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btnSearch"  runat="server" Text="<%$Resources:Common,Search %>" OnClick="btnSearch_Click" />
                
            </p>
         </div>
     
     <div class="function">
        <ul class="tooltop">
            <li><a href="HRJobList.aspx?NodeCode=<%=NodeCode %>" class="hover" style="border-right:0px">招聘管理</a> |</li>
            <li><a href="hrresumecountlist.aspx?NodeCode=<%=NodeCode %>" style="border-right:0px">招聘统计</a></li>
        </ul>
        <asp:Button ID="btnNew" runat="server" Text="<%$Resources:Common,Add %>" OnClick="btnAdd_Click" />
        <asp:Button ID="btnCheck" runat="server" Text="审核通过" OnClick="btnCheck_Click" OnClientClick="return GetSelectTitle();" />
        <asp:Button ID="btnCancelCheck" runat="server"  Text="审核不通过" onclick="btnCancelCheck_Click" OnClientClick="return GetSelectTitle();" />

        <asp:Button ID="btnDelete" runat="server"  Text="<%$Resources:Common,Del %>" onclick="btnDel_Click" OnClientClick="selfconfirm({msg:'确定要执行删除操作吗？',fn:function(data){setAction(data)}});return false;"/>
        
     </div>

    <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
            <td  style="width:3%; text-align:center;"><input type="checkbox" name="SlectAll" id="SlectAll" /></td>
            <td  style="width:20%;">职位名称</td>
            <td  style="width:10%;">职位类型</td>
            <td  style="width:10%;">工作单位</td>
            <td  style="width:6%;">工作地点</td>
            <td  style="width:5%;" >招聘人数</td>
            <td  style="width:8%;" >发布日期</td>
            <td  style="width:8%;" >截止日期</td>
            <td  style="width:6%;" >审核状态</td>
            <td  style="width:6%;" >排序</td>
            <td>操作</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptInfo" runat="server">
            <ItemTemplate>
                <tr class="listInfotr">
                <%#GetSortList(Eval("orders").ToString())%>
                    <td align="center"><input type="checkbox" name="chkId" id="chkId" value="<%#Eval("ID") %>" /></td>
                    <td id="Title<%#Eval("ID") %>"><%#Eval("Title") %></td>
                    <td><%#GetCateTitle(Eval("JobType").ToString (),"A")%></td>
                    <td><%#GetCateTitle(Eval("WorkUnit").ToString (),"B") %></td>
                    <td><%#GetCateTitle(Eval("WorkPlace").ToString(), "C")%></td>
                    <td><%#Eval("Number") %></td>
                    <td><%#Eval("PublishDate","{0:yyyy-MM-dd}") %></td>
                    <td><%#Eval("EndDate","{0:yyyy-MM-dd}") %></td>
                    <td><%#GetCheckName(Eval("FlowState").ToString()) %></td>
                    <td  id="HQB_Orders_<%#Eval("ID") %>" class="dragOrders" align="center"><div style="width:110px"><div style="float:left;border-right:1px solid #CCCCCC;height:22px;" title="拖动排序"><img src="../images/move.png" style="padding:0 8px;cursor: pointer;"/></div>
                        <div style="float:left;padding-left:8px"><div style="display:none;" ><img src="../images/loading.gif"/></div><span><input style="width:50px;text-align:center;height:14px" type="text" value="<%#Eval("Orders") %>" onblur="setOrders('K_HRJob','<%#Eval("ID") %>',this.value)"/></span></div>
                        </div>
                     </td>
                    <td>
                    <asp:LinkButton ID="lnkbEdit" class="abtn" runat="server" href='<%#"HrJobEdit.aspx?action=Edit&ID="+Eval("ID").ToString()+"&NodeCode="+NodeCode %>'>修改</asp:LinkButton>
                    <asp:LinkButton ID="lnkbDelete" class="abtn" runat="server"
                            CommandName="deldp" ToolTip='<%#Eval("Title")%>' CommandArgument='<%#Eval("ID") %>'
                            OnClientClick='selectThisRow();selfconfirm({msg:"确定要执行删除操作吗？",fn:function(data){setAction(data)}});return false;'>删除</asp:LinkButton> 
                     <asp:LinkButton ID="lnkbResumeView" class="abtn" runat="server" href='<%#"HRApplyJobList.aspx?start=1&tm="+Eval("LastViewResume").ToString()+"&jobid="+Eval("ID").ToString()+"&NodeCode="+NodeCode %>'>看简历</asp:LinkButton>
                    </td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
          </tbody>
          </table>
          <span class="function fr"></span>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="20" AlwaysShow="True"
                            ShowCustomInfoSection="Left"  
                            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                            UrlPaging="True" CssClass="page">
                        </webdiyer:AspNetPager>
                   
                  </div>
                  <script language="javascript">var SortList = "<%=sortList %>";</script>
</form>
</body>
</html>