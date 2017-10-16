<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberSet.aspx.cs"
    Inherits="KingTop.Web.Admin.MemberSet" %>
    <%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员配置</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/public.css" rel="stylesheet" type="text/css" />
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script language="javascript" type="text/javascript">
        function checkFrm() {
            var rt = $("#ddlRegisterType").val();
            //var rt=$('#ddlRegisterType option:selected').val();
            var jht = $("#txtJHEmailTitle").val();
            var zht = $("#txtFindPwdEMailTitle").val();
            if (rt == "2") {
                if (jht == "") {
                    $("#spanjhTitle").html("必须填写");
                    $("#txtJHEmailTitle").focus();
                    return false;
                }
                if(!editor_txtJHEMailContent.hasContents()){
                //if (!UE.getEditor('txtJHEMailContent_txtEditorContent').hasContents()) {
                    $("#spanjhContent").html("必须填写");
                    editor_txtJHEMailContent.focus();
                    return false;
                }
            }
            if (zht == "") {
                $("#spanzhTitle").html("必须填写");
                $("#txtFindPwdEMailTitle").focus();
                return false;
            }
            if(!editor_txtFindPwdEMailContent.hasContents()){
                $("#spanzhContent").html("必须填写");
                editor_txtFindPwdEMailContent.focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="theForm" runat="server">
    <div class="container">
        <h4>
            位置： <a href="#">会员管理</a> > <a href="#">会员配置</a></h4>
        <div id="panel">
            <fieldset>
                <dl>
                    <dt width="40%">
                        会员注册方式</dt>
                    <dd width="60%">
                        <asp:CheckBox ID="cbRegType1" runat="server" /> 会员注册后需要后台审核 &nbsp; 
                        <asp:CheckBox ID="cbRegType2" runat="server" /> 会员注册后需要邮件激活
                    </dd>
                </dl>
                
                <dl>
                    <dt width="40%"><b>激活邮件标题：</b></dt>
                    <dd width="60%"><asp:TextBox ID="txtJHEmailTitle" runat="server" width="400px"></asp:TextBox> <span style="color:red" id="spanjhTitle"></span></dd>
                </dl>
                <dl>
                    <dt width="40%"><b>激活邮件内容：</b><br />{UserName}为会员帐号变量，{URL}为激活连接变量</dt>
                    <dd width="60%">
                    
                    <uc1:Editor ID="txtJHEMailContent" runat="server" width=700 height="220" EditorType=1/>
                     <span  style="color:red" id="spanjhContent"></span>
                    </dd>
                </dl>
                <dl>
                    <dt width="40%"><b>找回密码邮件标题：</b> </dt>
                    <dd width="60%">
                    <asp:TextBox ID="txtFindPwdEMailTitle" runat="server" width="400px"></asp:TextBox>
                    <span  style="color:red" id="spanzhTitle"></span>
                    </dd>
                </dl>
                <dl>
                    <dt width="40%"><b>找回密码邮件内容：</b><br />{UserName}为会员帐号变量，{URL}为找回密码连接变量</dt>
                    <dd width="60%">
                    <uc1:Editor ID="txtFindPwdEMailContent" runat="server" width=700 height="220" EditorType=1 IsFirstEditor="false"/>
                    <span  style="color:red" id="spanzhContent"></span>
                    </dd>
                </dl>
                <dl>
                    <dt width="40%"><b>禁止注册的用户名：</b><br>在右边指定的用户名将被禁止注册，<br>每个用户名请用“|”符号分隔 </dt>
                    <dd width="60%"><asp:TextBox ID="txtDisUserName" runat="server" TextMode="MultiLine" width="400px" Height="100px"></asp:TextBox></dd>
                </dl>
            </fieldset>
            
            </div>
        
        <div class="Submit">
            <asp:Button ID="btnSave" runat="server" CssClass="subButton" Text="<%$Resources:Common,Update %>" OnClientClick="return checkFrm();" OnClick="btnSave_Click" />
        </div>
    </div>
    </form>
</body>
</html>
