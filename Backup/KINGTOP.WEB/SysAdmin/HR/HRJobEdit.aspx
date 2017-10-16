<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRJobEdit.aspx.cs" Inherits="KingTop.WEB.SysAdmin.HR.HRJobEdit" %>
<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>职位管理</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script src="../Calendar/WdatePicker.js" type="text/javascript"></script>
    <link href="../Calendar/skin/WdatePicker.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
    <link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/<%=Resources.Common.formValidationLanguage %>"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });
    </script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    
    <script type="text/javascript" src="../js/common.js"></script>
    <style>
    .dddiv{width:350px;float:left;}
    .div3{width:100px;text-align:right;float:left;padding-right:5px;}
    </style>
</head>
<body>
    <form id="theForm" name="theForm" runat="server">
    <input type="hidden" id="hidLogTitle" runat="server" />
    <input type="hidden" id="hidID" runat="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>
        
        <div id="panel">
         <fieldset>
         <dl>
        	<dt>职位名称</dt>
            <dd>
            <div class="dddiv">
            <input type="text" id="txtTitle" runat="server" class="validate[required]" maxlength="128" style="width:300px"/> <font color=red>*</font>
            </div>
               <div class="dddiv"><div class="div3">职位类型</div>
                <asp:DropDownList ID="ddlJobType" runat="server">
                </asp:DropDownList></div>
               </dd>
         </dl>
         <dl>
        	<dt>工作单位</dt>
            <dd>
            <div class="dddiv">
            <asp:DropDownList ID="ddlWorkUnit" runat="server"></asp:DropDownList>
            </div>
               <div class="dddiv"><div class="div3">工作地点</div>
                
                <asp:DropDownList ID="ddlWorkPlace" runat="server">
                </asp:DropDownList>
               </div>
               </dd>
         </dl>
         <dl>
        	<dt>薪水待遇</dt>
            <dd>
                <div class="dddiv">
                <input type="text" id="txtSalary" runat="server" maxlength="10"/> 0和不填表示面议
                </div>
                <div class="dddiv"><div class="div3">学历要求</div>
                <asp:DropDownList ID="ddlDegreeFrom" runat="server"></asp:DropDownList>
                </div>
            </dd>
         </dl>
         <dl>
        	<dt>年龄要求</dt>
            <dd>
                <div class="dddiv">
                <input type="text" id="txtAge" runat="server" class="validate[custom[onlyNumber]]" maxlength="2"/> 0和不填表示不限
                </div>
                <div class="dddiv">
                <div class="div3">经验要求</div> <input type="text" id="txtExperience" runat="server" maxlength="5"/> 0和不填表示不限
                
                </div>
            </dd>
         </dl>
         <dl>
        	<dt>招聘人数</dt>
            <dd>
                <div class="dddiv">
                <input type="text" id="txtNumber" runat="server" class="validate[custom[onlyNumber]]" maxlength="4"/> 0和不填表示不限
                </div>
                <div class="dddiv"><div class="div3">招聘人邮箱</div> <input type="text" id="txtEMail" class="validate[custom[email]]" runat="server" style="width:220px" maxlength="100"/></div>
            </dd>
         </dl>
         <dl>
        	<dt>发布日期</dt>
            <dd>
                <div class="dddiv">
                <input type="text" id="txtPublishDate" class="Wdate" runat="server" onfocus="WdatePicker({lang:'zh-cn',skin:'whyGreen',dateFmt:'yyyy-MM-dd'})"/>
                </div>
                <div class="dddiv"><div class="div3">截止日期</div> <input type="text" id="txtEndDate" class="Wdate" runat="server" onfocus="WdatePicker({lang:'zh-cn',skin:'whyGreen',dateFmt:'yyyy-MM-dd'})"/> 不填表示长期有效</div>
            </dd>
         </dl>
         
         <dl>
        	<dt>工作职责</dt>
        	<dd><uc1:Editor ID="Editor1" runat="server" width=700 height="150" EditorType=1/></dd>
         </dl>
         
         <dl>
        	<dt>任职资格</dt>
        	<dd><uc1:Editor ID="Editor2" runat="server" width=700 height="150" EditorType=1 IsFirstEditor="false"/></dd>
         </dl>
         
         <dl>
        	<dt>福利情况</dt>
        	<dd><uc1:Editor ID="Editor3" runat="server" width=700 height="150" EditorType=1 IsFirstEditor="false"/></dd>
         </dl>
         <dl>
                        <dt>页面标题(针对搜索引擎设置的标题，针对性的设置，有利于SEO优化)：</dt>
                        <dd><div style="float:left"><asp:TextBox ID="txtPageTitle" Width=500 Height=60 TextMode=MultiLine runat="server"></asp:TextBox> </div><div style="float:left">不填则页面显示默认标题，<br>标题格式为：标题-栏目名称-公司名称。</div></dd>
                    </dl>
                    <dl>
                        <dt>页面Meta关键字(针对搜索引擎设置的关键词。多个关键词请用,号分隔)：</dt>
                        <dd><asp:TextBox ID="txtKeyWords" Width=500 Height=60 TextMode=MultiLine runat="server"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>页面Meta说明(针对搜索引擎设置的网页描述。多个描述请用,号分隔)：</dt>
                        <dd><asp:TextBox ID="txtMetaDesc" Width=500 Height=60 TextMode=MultiLine runat="server"></asp:TextBox> </dd>                                                            
                    </dl>
         <div style="clear: left"></div>
      </fieldset>            
        </div>
        <div class="Submit">
            <asp:Button ID="BtnSave" runat="server" CssClass="subButton" Text="<%$Resources:Common,Save %>"
                OnClick="BtnSave_Click" />
           <input type="button" name="Submit422" Class="subButton" value="<%= Resources.Common.Back %>" onclick='location.href="HRJobList.aspx<%=StrPageParams %>";'>
        </div>
    </div>
    </form>
</body>
</html>
