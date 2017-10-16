<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRResumeList.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRResumeList" %>
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
<script language="javascript">
    function SaveMemo(v) {
        var objTitle = document.getElementsByName("chkId");
        var idList = "";
        for (i = 0; i < objTitle.length; i++) {
            if (objTitle[i].checked == true) {
                if (idList == "")
                    idList = objTitle[i].value;
                else
                    idList = idList + "," + objTitle[i].value;
            }
        }
        if (idList == "")
            return;

//        $.ajax(
//        {
//            type: "post",
//            //contentType: "application/json",  //加上后后台获取不到值
//            url: "hrresumelist.aspx",
//            data: {"ac": "memo", "memo":  v , "id":  idList},
//            dataType: 'html',
//            success: function (result) {
//                confirm(result);
//                if (result == "ok") {
//                    var arr = idList.split(",");
//                    for (i = 0; i < arr.length; i++) {
//                        $("#Memo" + arr[i]).html(v);
//                    }
//                }
//                else if (result == "error") {
//                    alert({ msg: "提交失败，失败原因请查看操作日志", title: "提示", status: "2" });

//                }
//            },
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//                //confirm(textStatus);
//                //confirm(XMLHttpRequest.status);
//                //confirm(errorThrown);
//            }
//        }
//        );

        $.post("hrresumelist.aspx", { ac: "memo", memo: v, id: idList }, function (data, textstatus) {
            if (data == "ok") {
                var arr = idList.split(",");
                for (i = 0; i < arr.length; i++) {
                    $("#Memo" + arr[i]).html(v);
                }
            }
            else if (data == "error") {
                alert({ msg: "提交失败，失败原因请查看操作日志", title: "提示", status: "2" });

            }
        }, "html");
    }

    function OnMemo(id) {
        selectThisRow();
        var valu=$("#Memo"+id).html();
        prompt({ msg: "简历备注（最多输入100汉字）", inputtype: "textarea", height: "120", value: valu, fn: function (data) { SaveMemo(data); } });
    }
    function OnSendEmail(id) {
        openframe({ title: "发送邮件", url: "sendemail.aspx?NodeCode=<%=NodeCode %>&id=" + id, width: 610, height: 420 });
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
        <asp:Button ID="btnDelete" runat="server"  Text="批量删除" onclick="btnDel_Click" OnClientClick="selfconfirm({msg:'确定要执行删除操作吗？',fn:function(data){setAction(data)}});return false;"/>
        <asp:Button ID="Button1" runat="server" Text="批量备注" OnClientClick="prompt({msg:'简历备注（最多输入100汉字）',inputtype:'textarea',height:'120',value:'',fn:function(data){SaveMemo(data);}});return false;" OnClick="btnAdd_Click" />
        <%--
        <asp:Button ID="btnNew" runat="server" Text="导出" OnClick="btnAdd_Click" />
        <select id="selJob" runat="server">
        </select>
        <asp:Button ID="btnMove" runat="server" Text="转移" OnClick="btnMove_Click" OnClientClick="return GetSelectTitle();" />
        <select id="selType" runat="server">
            <option value="10">不合格</option>
            <option value="1">一般</option>
            <option value="2">优秀</option>
            <option value="3">面试</option>
            <option value="4">录用</option>
            <option value="11">回收站</option>
        </select>
        <asp:Button ID="btnSetType" runat="server"  Text="分类" onclick="btnSetType_Click" OnClientClick="return GetSelectTitle();" />--%>
     </div>

    <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
            <td  style="width:30px; text-align:center;"><input type="checkbox" name="SlectAll" id="SlectAll" /></td>
            <td  style="width:60px;">姓名</td>
            <td  style="width:40px;">性别</td>
            <td  style="width:40px;">年龄</td>
            <td  style="width:50px;">学历</td>
            <td  style="width:40px;" >经验</td>
            <td  style="width:70px;" >所在地</td>
            <td  style="width:50px;" >要求月薪</td>
            <td  style="width:110px;" >毕业院校</td>
            <td  style="width:60px;" >更新日期</td>
            <td  style="width:180px;" >备注内容</td>
            <td style="width:210px;" >操作</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptInfo" runat="server">
            <ItemTemplate>
                <tr class="listInfotr">
                    <td align="center"><input type="checkbox" name="chkId" id="chkId" value="<%#Eval("ID") %>" /></td>
                    <td id="Title<%#Eval("ID") %>"><%#Eval("UserName") %></td>
                    <td><%#Eval("Gender") %></td>
                    <td><%#GetAge(Eval("birthday").ToString ())%></td>
                    <td><%#GetDegree(Eval("Degree").ToString ()) %></td>
                    <td><%#Eval("WorkYear")%></td>
                    <td><%#Eval("City")%></td>
                    <td><%#GetSalary(Eval("RequiresSalary").ToString ())%></td>
                    <td><%#Eval("Universities")%></td>
                    <td><%#Eval("UpdateDate","{0:yyyy-MM-dd}")%></td>
                    <td id="Memo<%#Eval("ID") %>"><%#Eval("Memo")%></td>
                    <td>
                    <asp:LinkButton ID="lnkbView" target="_blank" class="abtn" runat="server" href='<%#resumeDetailPath+"?em=1&id="+Eval("ID").ToString() %>'>预览</asp:LinkButton>
                    <asp:LinkButton ID="lnkbPrint" target="_blank" class="abtn" runat="server" href='<%#resumeDetailPath+"?em=1&action=print&id="+Eval("ID").ToString() %>'>打印</asp:LinkButton>
                    <asp:LinkButton ID="lnkbMemo" class="abtn" runat="server" 
                    OnClientClick='<%#"OnMemo("+Eval("ID").ToString()+");return false;"%>'>备注</asp:LinkButton>
                    <asp:LinkButton ID="lnkbSendEMail" class="abtn" runat="server" target="_blank" OnClientClick='<%#"OnSendEmail("+Eval("ID").ToString()+");return false;" %>'>发邮件</asp:LinkButton>
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
</form>
</body>
</html>