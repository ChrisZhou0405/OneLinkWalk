<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRResumeCountList.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRResumeCountList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>简历管理</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
<link href="../css/dialog.css" rel="stylesheet" type="text/css" />
<script src="../JS/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="../js/jquery.dialog.js"></script>
<script type="text/javascript" src="../js/win.js"></script>
<script type="text/javascript" src="../js/publicform.js"></script>
<script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
<script src="../js/common.js" type="text/javascript"></script>
<script src="../js/listchecksort.js" type="text/javascript"></script>
</head>
<body>
<form id="theForm" name="theForm" method="post" runat="server">
 <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat ="server" />
 <!--点击删除按钮（确认框）用-->
 <asp:Button ID="btnHidAction" runat="server" OnClick="btnDel_Click" style="display:none" />
<div class="container">
     <h4>位置： <%GetPageNav(NodeCode); %></h4> 
     
     <div id="searchContainer">
           <p>
                关键字
                <asp:TextBox ID="txtTitle" runat="server" Width="80px" maxlength="20">
                </asp:TextBox>
                <select id="KeyWorkType" runat="server">
                    <option value="1">姓名</option>
                    <option value="2">职位</option>
                    <option value="3">毕业院校</option>
                    <option value="4">专业</option>
                    <option value="5">居住地</option>
                </select>
                
                性别
                <select id="selGender" runat="server">
                    <option value="">不限</option>
                    <option value="男">男</option>
                    <option value="女">女</option>
                </select>
                阅读状态
                <select id="selIsRead" runat="server">
                    <option value="">不限</option>
                    <option value="0">未阅</option>
                    <option value="1">已阅</option>
                    
                </select>           
                 学历
                <asp:DropDownList ID="ddlStartDegree" runat="server">
                </asp:DropDownList>
                -
                <asp:DropDownList ID="ddlEndDegree" runat="server">
                </asp:DropDownList>

                
                工作年限
                <asp:TextBox ID="txtStartWorkYear" runat="server" MaxLength="2" Width="20px" >
                </asp:TextBox>
                -
                <asp:TextBox ID="txtEndWorkYear" runat="server" MaxLength="2" Width="20px" >
                </asp:TextBox>
                年龄
                <asp:TextBox ID="txtStartAge" runat="server" MaxLength="2" Width="20px" >
                </asp:TextBox>
                -
                <asp:TextBox ID="txtEndAge" runat="server" MaxLength="2" Width="20px" >
                </asp:TextBox>

                <asp:Button ID="btnSearch"  runat="server" Text="<%$Resources:Common,Search %>" OnClick="btnSearch_Click" />
            </p>
         </div>
     
     <div class="function">
        <ul class="tooltop" style="margin:0 0 10px 0">
            <li><a href="HRJobList.aspx?NodeCode=<%=NodeCode %>" style="border-right:0px">招聘管理</a> |</li>
            <li><a href="HRResumeCountList.aspx?NodeCode=<%=NodeCode %>" class="hover" style="border-right:0px">招聘统计</a></li>
        </ul>
     </div>

    <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
            <td  style="width:150px;">招聘职位</td>
            <td  style="width:60px;" align="center">总计</td>
            <td  style="width:60px;" align="center">一般</td>
            <td  style="width:60px;" align="center">优秀</td>
            <td  style="width:60px;" align="center">面试</td>
            <td  style="width:60px;" align="center">录用</td>
            <td  style="width:60px;" align="center">不合格</td>
            <td  style="width:60px;" align="center">回收站</td>
            
            <td  style="width:60px;" title="上次浏览简历后投递的简历之和" align="center">新增</td>
            <td  style="width:100px;" align="center">上次浏览简历时间</td>
            <td style="width:80px;">操作</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptInfo" runat="server">
            <ItemTemplate>
                <tr class="listInfotr">
                    <td><%#Eval("Title") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"1","2") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"1","0") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"2","0") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"3","0") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"4","0") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"10","0") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"11","0") %></td>
                    <td align="center"><%#GetCount(Eval("ID").ToString (),"1","1") %></td>
                    <td align="center"><%#Eval("LastViewResume","{0:yyyy-MM-dd}")%></td>
                    <td>
                        <asp:LinkButton ID="lnkbResumeView" class="abtn" runat="server" href='<%#"HRApplyJobList.aspx?start=1&bu=1&tm="+Eval("LastViewResume").ToString()+"&jobid="+Eval("ID").ToString()+"&NodeCode="+NodeCode %>'>看简历</asp:LinkButton>
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

</form>
</body>
</html>