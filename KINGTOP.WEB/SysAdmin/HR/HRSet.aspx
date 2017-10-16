<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRSet.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRSet" %>
<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="theForm" runat="server">
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>
        <div id="panel">
                <dl>
                    <dt width="50%"><b>用户注册为会员后才能提交简历:</b>
                    
                    </dt>
                    <dd width="50%"><input type="checkbox" id="chkIsMember" runat="server" /> 

                    选择后，用户需要先注册并完成简历填写，才能应聘相应岗位（提交简历）
                    </dd>
                </dl>
                <dl>
                    <dt width="50%"><b>简历是否发送到相关负责人邮箱:</b>
                    </dt>
                    <dd width="50%">
                    <input type="checkbox" id="chkIsEmail" runat="server" /> 

                    选择后用户提交的简历不仅发送到网站后台，还直接发送到相关负责人邮箱。注：选择后下列信息必须填写
                    </dd>
                </dl>
                <dl>
                    <dt width="50%"><b>简历详细页前台路径:</b>
                    </dt>
                    <dd width="50%">
                    <input type="text" id="txtResumeDetailFilePath" runat="server" style="width:300px" />
                    配置后台“浏览简历和打印简历”功能的页面路径,{SiteDir}为站点目录，不能去掉
                    </dd>
                </dl>
                <dl>
                    <dt width="40%"><b>后台发邮件标题：</b></dt>
                    <dd width="60%">
                    <asp:TextBox ID="txtEMailTitle" runat="server" width="300px"></asp:TextBox>
                    配置简历和应聘管理中的发送邮件模版，例如配置面试通知的邮件模版
                    </dd>
                </dl>
                <dl>
                    <dt width="40%"><b>后台发邮件内容：</b><br /></dt>
                    <dd width="60%">
                    <uc1:Editor ID="txtEMailContent" runat="server" width=700 height="220" EditorType=1/>
                    {UserName}简历姓名，{MSDate}为面试时间，{SendDate}为发送时间
                    <span  style="color:red" id="spanzhContent"></span>
                    </dd>
                </dl>
                <dl>
                    <dt width="50%"><b>发送人邮箱:</b></dt>
                    <dd width="50%"><asp:TextBox ID="txtEmail" runat="server" width="300px"></asp:TextBox> 例：someone@toprand.com</dd>
                </dl>
                <dl>
                    <dt width="50%"><b>发送人邮箱密码：</b></dt>
                    <dd width="50%"><asp:TextBox ID="txtPassword" runat="server" width="300px" 
                            TextMode="Password"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt width="50%"><b>发送邮件服务器(SMTP)：</b> </dt>
                    <dd width="50%"><asp:TextBox ID="txtSmtpServer" runat="server" width="300px"></asp:TextBox>
                    用来发送邮件的SMTP服务器，如果你不清楚此参数含义，请联系你的空间商
                    </dd>
                </dl>
                <dl>
                    <dt width="50%"><b>端口号：</b> </dt>
                    <dd width="50%"><asp:TextBox ID="txtPort" runat="server" width="300px"></asp:TextBox>
                    端口号必需是正整数，默认是25端口
                    </dd>
                </dl>          
        </div>     
        <div class="Submit" style="padding-left:300px;">
        <asp:Button Text="保存设置" CssClass="subButton" ID="btnSave" runat="server" onclick="btnSave_Click" />
        </div>
    </div>
    </form>
</body>
</html>
