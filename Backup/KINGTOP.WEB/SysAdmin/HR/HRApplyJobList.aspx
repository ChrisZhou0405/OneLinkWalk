<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRApplyJobList.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRApplyJobList" %>
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
<script type="text/javascript">
    function OnSendEmail(id) {
        openframe({ title: "发送邮件", url: "sendemail.aspx?NodeCode=<%=NodeCode %>&id="+id, width: 610, height: 420 });
    }
</script>
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
                    <option value="2">应聘职位</option>
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
        <ul class="tooltop">
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype="<%if(string.IsNullOrEmpty(Request.QueryString["resumetype"])){ %> class="hover"<%} %>>所有</a><em>(<%=Arr[0]%>)</em></li>
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=10"<%if(Request.QueryString["resumetype"]=="10"){ %> class="hover"<%} %>>不合格</a><em>(<%=Arr[5]%>)</em></li>
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=1"<%if(Request.QueryString["resumetype"]=="1"){ %> class="hover"<%} %>>一般</a><em>(<%=Arr[1]%>)</em></li>
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=2"<%if(Request.QueryString["resumetype"]=="2"){ %> class="hover"<%} %>>优秀</a><em>(<%=Arr[2]%>)</em></li>
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=3"<%if(Request.QueryString["resumetype"]=="3"){ %> class="hover"<%} %>>面试</a><em>(<%=Arr[3]%>)</em></li>
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=4"<%if(Request.QueryString["resumetype"]=="4"){ %> class="hover"<%} %>>录用</a><em>(<%=Arr[4]%>)</em></li>
            <li><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=11"<%if(Request.QueryString["resumetype"]=="11"){ %> class="hover"<%} %>>回收站</a><em>(<%=Arr[6]%>)</em></li>
            <br />
            <li style="padding:6px 0 5px 3px;">上次查看简历时间是：<%=Request.QueryString["tm"] %></li>
            <li style="padding:6px 0 5px 0"><a href="HRApplyJobList.aspx?<%=GetUrlParam() %>&resumetype=100"<%if(Request.QueryString["resumetype"]=="100"){ %> class="hover"<%} %>>新增</a><em>(<%=Arr[7]%>)</em></li>
        </ul>
        
        <input type="button" id="btnBack" name="btnBack" onclick="location.href='<%if(Request.QueryString["bu"]=="1"){ %>HRResumeCountList.aspx<%=StrPageParams2("HRResumeCountList.aspx",NodeCode) %><%}else{ %>HRJobList.aspx<%=StrPageParams2("hrjoblist.aspx",NodeCode) %><%} %>';" value="返回" title="返回职位列表页面" />
        <asp:Button ID="btnResumeDel" runat="server"  Text="<%$Resources:Common,Del %>" onclick="btnDel_Click" OnClientClick="selfconfirm({msg:'确定要执行删除操作吗？',fn:function(data){setAction(data)}});return false;"/>
        <%--<asp:Button ID="btnNew" runat="server" Text="导出" OnClick="btnAdd_Click" />--%>
        <select id="selJob" runat="server">
        </select>
        <asp:Button ID="btnResumeMove" runat="server" Text="转移" OnClick="btnMove_Click" OnClientClick="return GetSelectTitle();" />
        <select id="selType" runat="server">
            <option value="10">不合格</option>
            <option value="1">一般</option>
            <option value="2">优秀</option>
            <option value="3">面试</option>
            <option value="4">录用</option>
            <option value="11">回收站</option>
        </select>
        <asp:Button ID="btnResumeSetType" runat="server"  Text="分类" onclick="btnSetType_Click" OnClientClick="return GetSelectTitle();" />

        
        
     </div>

    <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
            <td  style="width:45px; text-align:center;"><input type="checkbox" name="SlectAll" id="SlectAll" /></td>
            <td  style="width:100px;">姓名</td>
            <td  style="width:60px;">性别</td>
            <td  style="width:60px;">年龄</td>
            <td  style="width:60px;">工作年限</td>
            <td  style="width:150px;" >应聘岗位</td>
            <td  style="width:80px;" >投递时间</td>
            <td  style="width:150px;" >毕业院校</td>
            <td  style="width:60px;" >是否阅读</td>
            <td  style="width:60px;" >分类</td>
            <td style="width:200px;" >操作</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptInfo" runat="server">
            <ItemTemplate>
                <tr class="listInfotr">
                    <td align="center"><input type="checkbox" name="chkId" id="chkId" value="<%#Eval("ResumeID") %>_<%#Eval("ID") %>" /></td>
                    <td id="Title<%#Eval("ResumeID") %>_<%#Eval("ID") %>"><%#Eval("UserName") %></td>
                    <td><%#Eval("Gender") %></td>
                    <td><%#GetAge(Eval("birthday").ToString ())%></td>
                    <td><%#Eval("WorkYear") %></td>
                    <td><%#Eval("JobTitle")%></td>
                    <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                    <td><%#Eval("Universities")%></td>
                    <td id="tdRead<%#Eval("ResumeID") %>"><%#Eval("IsRead").ToString() == "True" ? "<font color=red>已阅</font>" : "否"%></td>
                    <td><%#GetType(Eval("status").ToString ()) %></td>
                    <td>
                    <asp:LinkButton ID="lnkbView" class="abtn" target="_blank" runat="server" OnClientClick='<%#"OnReadStatus("+Eval("ResumeID").ToString()+");" %>' href='<%#resumeDetailPath+"?em=1&id="+Eval("ResumeID").ToString() %>'>浏览</asp:LinkButton>
                    <asp:LinkButton ID="lnkbPrint" class="abtn" runat="server" target="_blank" href='<%#resumeDetailPath+"?em=1&action=print&id="+Eval("ResumeID").ToString() %>'>打印</asp:LinkButton>
                    <asp:LinkButton ID="lnkbSendEMail" class="abtn" runat="server" target="_blank" OnClientClick='<%#"OnSendEmail("+Eval("ResumeID").ToString()+");return false;" %>'>发邮件</asp:LinkButton>
                    <asp:LinkButton ID="lnkbDelete" class="abtn" runat="server"
                            CommandName="deldp" ToolTip='<%#Eval("UserName")%>' CommandArgument='<%#Eval("ID") %>'
                            OnClientClick='selectThisRow();selfconfirm({msg:"确定要执行删除操作吗？",fn:function(data){setAction(data)}});return false;'>删除</asp:LinkButton> 
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
                  <script language="javascript">
                      function OnReadStatus(resumeid) {
                          $("#tdRead" + resumeid).html("<font color=red>已阅</font>");
                      }
                  </script>
</form>
</body>
</html>