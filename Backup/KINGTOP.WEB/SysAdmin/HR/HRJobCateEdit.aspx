<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRJobCateEdit.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRJobCateEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>类别管理</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/<%=Resources.Common.formValidationLanguage %>"></script>
    <script type="text/javascript">
        $(document).ready(function() { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });
        
        
        var type=-1;
        var title="";
        var errmsg="";
        function showMessage() {
        var msgContent="";
        var jumpurl="";
        //添加成功
            if (type == 0) {
                msgContent="添加类别<font color=red>"+title+"</font>成功<br>";
                msgContent+="<a href='HrJobCateList.aspx?NodeCode=<%=NodeCode %>&cateType=<%=Request.QueryString["cateType"] %>' style='color:blue'>返回列表</a> | ";
                msgContent+="<a href='HrJobCateEdit.aspx?action=New&NodeCode=<%=NodeCode %>&cateType=<%=Request.QueryString["cateType"] %>' style='color:red'>继续添加</a>";
                msgContent+="<br>注：3秒钟后自动转到列表页面";
                jumpurl="HrJobCateList.aspx?NodeCode=<%=NodeCode %>&cateType=<%=Request.QueryString["cateType"] %>";
                alert({ msg: msgContent, status: "1", title: "提示信息", url:jumpurl,time: 3000,width:400 })
            }
            else if (type == 1) {  //修改成功
                msgContent="修改用户<font color=red>"+title+"</font>成功<br>";
                msgContent+="<a href='HrJobCateList.aspx<%=StrPageParams %>' style='color:blue'>返回列表</a> | ";
                msgContent += "<a href='HrJobCateEdit.aspx?<%=KingTop.Common.Utils.GetUrlParams() %>' style='color:red'>继续修改</a> | ";
                msgContent += "<a href='HrJobCateEdit.aspx?action=New&NodeCode=<%=NodeCode %>&cateType=<%=Request.QueryString["cateType"] %>' style='color:red'>添加新类别</a><br>";
                msgContent+="注：3秒钟后自动转到列表页面";
                jumpurl = "HrJobCateList.aspx<%=StrPageParams %>";
                alert({ msg: msgContent, status: "1", title: "提示信息", url:jumpurl,time: 3000,width:400 })
            }
            else if (type == 2) {  //操作失败
                msgContent = "操作失败,原因如下：<br>";
                msgContent += "<font style='color:green'>" + errmsg + "</font><br>";
                msgContent+="注：10秒钟后提示框自动关闭";
                alert({ msg: msgContent, status: "2", title: "提示信息", time: 10000, width: 400 })
            }
        }
        window.onload=function(){
            if(type>-1)
            {
                showMessage();
            }
        }
    </script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
</head>
<body>
    <form id="theForm" runat="server">
    <input type="hidden" id="hidLogTitle" runat="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %> &gt; <span class="breadcrumb_current"> 添加/修改类别</span>
        </h4>
        <div id="panel">
            <fieldset>
         <dl>
        	<dt>类别名称：</dt>
        <dd><asp:TextBox ID="txtTitle" runat="server" CssClass="validate[required,length[2,50]]"></asp:TextBox> <font color=red>*</font></dd>
         </dl> 
         <dl>
            <dt>所属父类别:</dt>
            <dd><asp:DropDownList ID="HRJobCate" runat="server"></asp:DropDownList> </dd>
         </dl>
        <dl>
            <dt>类别说明：</dt>
            <dd><asp:TextBox ID="Intro" TextMode="MultiLine" Rows="5" Columns="50" runat="server"></asp:TextBox></dd>
        </dl> 
        <dl>
            <dt>排序ID：</dt>
            <dd><asp:TextBox ID="Orders" Text="0" CssClass="validate[required,custom[onlyNumber],length[0,3]] text-input" runat="server"></asp:TextBox> </dd>
        </dl><div style="clear:left"></div>
       </fieldset>
        </div>
        <div class="Submit">
            <asp:HiddenField ID="hdPCode" runat="server" />
            <asp:HiddenField ID="hdNCode" runat="server" />
            <asp:HiddenField ID="hdnID" runat="server" />
            <asp:HiddenField ID="hdInitChk" runat="server" />
            <asp:Button ID="btnSource" runat="server" CssClass="subButton" Text="<%$Resources:Common,Add %>"
                OnClick="btnSource_Click" />
          <input type="button" name="Submit422" Class="subButton" value="<%= Resources.Common.Back %>" onclick='location.href="HrJobCateList.aspx<%=StrPageParams %>";'>
        </div>
    </div>
    </form>
</body>
</html>
