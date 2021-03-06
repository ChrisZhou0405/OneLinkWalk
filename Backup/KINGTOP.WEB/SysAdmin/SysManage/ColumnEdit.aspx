﻿<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ColumnEdit.aspx.cs" Inherits="KingTop.WEB.SysAdmin.SysManage.ColumnEdit" %>

<%@ Register src="../Controls/Editor.ascx" tagname="Editor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>栏目编辑</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
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
    <script type="text/javascript">
        function replaceEditor(a, b, c) {
            while (a.indexOf(b) != -1) {
                a = a.replace(b, c);
            }
            return a
        }
        function SelectChange() {
            var count = document.getElementById("ddlCustomContentCount").value;
            for (var i = 2; i <= count; i++) {
                document.getElementById("dlCustom" + i).style.display = "";
            }
            for (var j = parseInt(count) + 1; j < 21; j++) {

                document.getElementById("dlCustom" + j).style.display = "none";
            }
        }

        function ChangeEditor(i, isChecked) {
            var editorHtml = $("#ddMenuDesc").html();
            var startIndex = editorHtml.toLowerCase().lastIndexOf("<script");
            if (startIndex == -1) {
                startIndex = editorHtml.toLowerCase().indexOf("<iframe");
            }
            else {
                var instance = CKEDITOR.instances["txtCustomContent" + i];
                if (instance) {
                    CKEDITOR.remove(instance);
                }
            }
            editorHtml = editorHtml.substring(startIndex, editorHtml.length);

            if (editorHtml.toLowerCase().indexOf("<iframe") > -1) {
                $("#txtCustomContent" + i).css("display", "none");
            }
            editorHtml = replaceEditor(editorHtml, "Editor1$txtEditorContent", "txtCustomContent" + i);
            editorHtml = replaceEditor(editorHtml, "Editor1", i);

            if (isChecked) {
                $("#dd" + i).html($("#dd" + i).html() + editorHtml);
            }
            else {
                $("#txtCustomContent" + i)[0].style.display = "";
                $("#txtCustomContent" + i)[0].style.visibility = "visible"; ;
                var ddHtml = $("#dd" + i).html();
                startIndex = ddHtml.toLowerCase().lastIndexOf("</textarea") + 11;
                $("#dd" + i).html(ddHtml.substring(0, startIndex));
            }
        }

        var showTotal = 1;
        var type = -1;
        var title = "";
        var errmsg = "";
        var editSelfMenuUrl = "";
        function showMessage() {
            var msgContent = "";
            var jumpurl = "";
            //添加成功
            if (type == 0) {
                msgContent = "添加栏目<font color=red>" + title + "</font>成功<br>";
                msgContent += "<a href='ColumnManage.aspx?NodeCode=<%=NodeCode%>' style='color:blue'>返回列表</a> | ";
                msgContent += "<a href='ColumnEdit.aspx?<%=KingTop.Common.Utils.GetUrlParams() %>' style='color:red'>继续添加</a> | ";
                msgContent += "<a href='" + editSelfMenuUrl + "' style='color:red'>修改本栏目</a>";
                msgContent += "<br>注：3秒钟后自动转到列表页面";
                jumpurl = "ColumnManage.aspx?NodeCode=<%=NodeCode %>";
                alert({ msg: msgContent, status: "1", title: "提示信息", url: jumpurl, time: 3000, width: 400 })
            }
            else if (type == 1) {  //修改成功
                msgContent = "修改栏目<font color=red>" + title + "</font>成功<br>";
                msgContent += "<a href='ColumnManage.aspx<%=StrPageParams %>' style='color:blue'>返回列表</a> | ";
                msgContent += "<a href='ColumnEdit.aspx?<%=KingTop.Common.Utils.GetUrlParams() %>' style='color:red'>继续修改</a>";
                if (editSelfMenuUrl != "") {
                    msgContent += " | <a href='" + editSelfMenuUrl + "' style='color:red'>添加新栏目（同级）</a>";
                }
                msgContent += "<br>注：3秒钟后自动转到列表页面";
                jumpurl = "ColumnManage.aspx?NodeCode=<%=NodeCode%>";
                alert({ msg: msgContent, status: "1", title: "提示信息", url: jumpurl, time: 3000, width: 400 })
            }
            else if (type == 2) {  //操作失败
                msgContent = "操作失败,原因如下：<br>";
                msgContent += "<font style='color:green'>" + errmsg + "</font><br>";
                msgContent += "注：10秒钟后提示框自动关闭";
                alert({ msg: msgContent, status: "2", title: "提示信息", time: 10000, width: 400 })
            }
            else if (type == 3) {  //修改成功
                msgContent = "删除栏目<font color=red>" + title + "</font>成功<br>";
                msgContent += "注：3秒钟后自动转到列表页面";
                jumpurl = "ColumnManage.aspx?NodeCode=<%=NodeCode%>";
                alert({ msg: msgContent, status: "1", title: "提示信息", url: jumpurl, time: 3000, width: 400 })
            }
        }
        window.onload = function() {
//            if (type > -1) {
//                showMessage();
//            }
            for (var i = 1; i < 21; i++) {
                var chk = document.getElementById("chkIsHtmlEditor" + i);
                if (chk.checked) {
                    ChangeEditor(i, true);
                }
                if (showTotal > 1 && i <= showTotal && i > 1) {
                    $("#dlCustom" + i).css("display", "");
                }
            }
        }
        function setAction1(data) {
            if (data == "true") {
                document.getElementById("btnHidAction").click();
            }
        }


        function selectTemplateFile(contrName) {
            openframe({ title: "选择模板文件", url: "seltemplate.aspx?contrName=" + contrName, width: '400', height: '400' });
        }

        //如果有切换选项，点击提交时先切换到第一选项
        function changeTabOne() {
            $("#tags li.selectTag").removeClass("selectTag");
            $("#tags li").eq(0).addClass("selectTag");
            $("#panel fieldset").hide();
            $("#panel fieldset").eq(0).show();
            if ($("#chkIsFolder").attr("checked") == false && $("#ddlModel").val() == "0") {
                alert({ msg: "必须选择后台管理模块", title: "提示信息" });
                $("#ddlModel").focus();
                return false;
            }
        }

    </script>
    
</head>
<body>
    <form id="theForm" runat="server">
    <input type="hidden" id="hidLogTitle" runat="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %> &gt; <span class="breadcrumb_current"> 添加/修改栏目</span>
        </h4>
        <div id="con">
            <ul id="tags">
                <li class="selectTag"><a href="javascript:;">基本信息</a> </li>
                <li><a href="javascript:;">栏目选项</a></li>
                <li><a href="javascript:;" style="display:none">模板选项</a></li>
                <li><a href="javascript:;">生成选项</a></li>
                <li><a href="javascript:;" style="display:none">权限设置</a></li>
                <li><a href="javascript:;" style="display:none">自设内容</a></li>
            </ul>
            <div id="panel">
            
                <fieldset>
                    <dl>
                        <dt>栏目名称：</dt>
                        <dd>
                           <asp:TextBox ID="txtNodeName" runat="server"  Width="231px" CssClass="validate[required,length[1,30]]"></asp:TextBox> <font color=red>*</font>
                           <asp:TextBox ID="txtNodeID" runat="server"  ReadOnly="true" Visible=false BackColor="Gainsboro"></asp:TextBox>
                           <asp:TextBox ID="txtNodeCode" runat="server" Visible=false></asp:TextBox>
                        </dd>
                    </dl>
                    <dl>
                        <dt>英文名称：</dt>
                        <dd><asp:TextBox ID="txtNodelEngDesc"  Width="231px" runat="server"></asp:TextBox>(当前站点前台需要显示栏目英文名时填写) </dd>
                    </dl>
                    <dl>
                        <dt>栏目目录名：</dt>
                        <dd><asp:TextBox ID="txtNodeDir" CssClass="validate[custom[noSpecialCaracters]]"  Width="231px" runat="server"></asp:TextBox>(如果站点发布为静态，必须填写）</dd>
                    </dl>
                    <dl>
                        <dt>是否父栏目(只有父栏目下才能包含子栏目，父栏目不能属于某个模块)：</dt>
                        <dd> 
                            <asp:CheckBox ID="chkIsFolder" runat="server" Text="是" onclick="if(this.checked==true){$('#ddlModel')[0].selectedIndex =0;$('#ddlModel')[0].disabled=true;}else{$('#ddlModel')[0].disabled=false;}" />
                        </dd>
                    </dl>
                    <dl>
                        <dt>后台管理模块(非父栏目必须选择)：</dt>
                        <dd><asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="true"></asp:DropDownList> <asp:Label ID="lblModel" runat ="server" ForeColor="Red" Text="*"></asp:Label></dd>
                    </dl>
                    <dl>
                        <dt>前台程序路径：</dt>
                        <dd><asp:TextBox ID="txtSubDomain" runat="server" Width="231px"></asp:TextBox>(例如：/about/index.aspx?m=105001002 )</dd>
                    </dl>
                    <dl>
                        <dt>前台栏目自定义链接：</dt>
                        <dd><asp:TextBox ID="txtLinkURL" runat="server" Width="231px"></asp:TextBox>(例如：/关于我们.html，当启用伪静态时必须填写)</dd>
                    </dl>
                    <dl>
                        <dt>页面标题(针对搜索引擎设置的标题，针对性的设置，有利于SEO优化)：</dt>
                        <dd><div style="float:left"><asp:TextBox ID="txtPageTitle" Width=500 Height=60 TextMode=MultiLine runat="server"></asp:TextBox> </div><div style="float:left">不填则页面显示默认标题，<br>标题格式为：栏目名称-公司名称。<br>例如：联系我们-深圳市图派科技有限公司</div></dd>
                    </dl>
                    <dl>
                        <dt>栏目Meta关键字(针对搜索引擎设置的关键词。多个关键词请用,号分隔)：</dt>
                        <dd><asp:TextBox ID="txtKeyWords" Width=500 Height=60 TextMode=MultiLine runat="server"></asp:TextBox></dd>
                    </dl>
                    <dl>
                        <dt>栏目Meta说明(针对搜索引擎设置的网页描述。多个描述请用,号分隔)：</dt>
                        <dd><asp:TextBox ID="txtMetaDesc" Width=500 Height=60 TextMode=MultiLine runat="server"></asp:TextBox> </dd>                                                            
                    </dl>
                    <dl>
                        <dt> 排序号：</dt>
                        <dd><asp:TextBox ID="txtNodelOrder" runat="server"  CssClass="validate[required,custom[onlyNumber],length[0,3]] text-input" Text="0" Width="50px"></asp:TextBox></dd>
                    </dl>
                    <div style="clear:left"></div>
                 </fieldset>
                 
                       
                 
                 <fieldset style="display: none;">
                    <dl id="dlMenuType" runat="server"><dt>栏目类型</dt>
                    <dd>
                    <select id="selMenuType" name="selMcenuType" onchange="location.href=this.value;">
                        <option value="ColumnEdit.aspx<%=strurlpar %>">普通栏目</option>
                        <option value="SingleColumn.aspx<%=strurlpar %>">单页栏目</option>
                        <option value="OutLinkColumn.aspx<%=strurlpar %>">外部连接栏目</option>
                    </select>
                    选择后修改栏目，可以更改栏目类型，比如将单页类型栏目更改为普通类型栏目
                    </dd>
                    </dl>
                    <dl>
                        <dt>栏目提示：</dt>
                        <dd><asp:TextBox ID="txtTips" Width=200 runat="server"></asp:TextBox>(鼠标移至栏目名称上时将显示设定的提示文字（不支持HTML）)</dd>
                    </dl>
                     <dl style="display:none">
                        <dt>是否启用子域名：</dt>
                        <dd> <asp:RadioButtonList ID="RbtEnableSubDomain" runat="server" Height="16px" RepeatDirection=Horizontal Width="124px">
                              <asp:ListItem Value="False" Selected="True">不启用</asp:ListItem>
                              <asp:ListItem Value="True">启用</asp:ListItem>
                          </asp:RadioButtonList> </dd>
                    </dl>

                    <dl>
                        <dt>栏目Banner：</dt>
                        <dd><asp:TextBox ID="txtBanner" runat="server" Width="252px"></asp:TextBox> <input type="button" id="Button4" style="width:60px" value="上传" runat="server" Class="btn" onclick="InputFile('theForm','txtBanner','image',1,'jpg|gif|bmp|png|swf|flv','','','','',0,'','');" /></dd>
                    </dl>
                    <dl>
                        <dt>栏目图标(如果前台栏目导航选择图片方式，则需上传，下同)：</dt>
                        <dd><asp:TextBox ID="txtNodelIcon" runat="server" Width="252px"></asp:TextBox> <input type="button" id="Button3" style="width:60px" value="上传" runat="server" Class="btn" onclick="InputFile('theForm','txtNodelIcon','image',1,'jpg|gif|bmp|png','','','','',0,'','');" /></dd>
                    </dl>
                    <dl>
                        <dt>当前栏目图标：</dt>
                        <dd><asp:TextBox ID="txtCurrentImg" runat="server" Width="252px"></asp:TextBox> <input type="button" id="Button1" style="width:60px" value="上传" runat="server" Class="btn" onclick="InputFile('theForm','txtCurrentImg','image',1,'jpg|gif|bmp|png','','','','',0,'','');" /></dd>
                    </dl>
                    <dl>
                        <dt>鼠标移上去图标（鼠标移上去图标与当前栏目图标一样，则不必上传）：</dt>
                        <dd><asp:TextBox ID="txtMouseOverImg" runat="server" Width="252px"></asp:TextBox> <input type="button" id="Button2" style="width:60px" value="上传" runat="server" Class="btn" onclick="InputFile('theForm','txtMouseOverImg','image',1,'jpg|gif|bmp|png','','','','',0,'','');" /></dd>
                    </dl>
                    
                    
                    <dl>
                        <dt>后台左边菜单栏显示：</dt>
                        <dd>    
                            <asp:CheckBox ID="chkIsLeftDisplay" runat="server" Checked=true Text="是" />（默认为是）
                        </dd>
                    </dl>
                    <dl>
                        <dt>前台头部栏目显示：</dt>
                        <dd>    
                            <asp:CheckBox ID="chkIsTopMenuShow" runat="server" Checked=true Text="是" />（默认为是）
                        </dd>
                    </dl>
                    <dl>
                        <dt>前台左边栏目显示：</dt>
                        <dd>    
                            <asp:CheckBox ID="chkIsLeftMenuShow" runat="server" Checked=true Text="是" />（默认为是）
                        </dd>
                    </dl>               
                    <dl>
                        <dt>是否有效：</dt>
                        <dd><asp:RadioButtonList ID="RBL_IsValid" runat="server" Height="16px" RepeatDirection=Horizontal
                                          Width="124px">
                                          <asp:ListItem Value="0">禁用</asp:ListItem>
                                          <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                                      </asp:RadioButtonList></dd>
                    </dl>
                     <dl>
                        <dt>打开方式：</dt>
                        <dd><asp:RadioButton ID="radSelf" runat="server" Text="在原窗口打开" GroupName="OpenType" Checked="true" /><asp:RadioButton ID="radNew" runat="server" Text="在新窗口打开" GroupName="OpenType" /></dd>          
                    </dl>
                    <dl style="display:none">
                        <dt>访问权限：</dt>
                        <dd>
                        <asp:RadioButton ID="radkf" runat="server" Text="开放栏目" GroupName="PurviewType" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;任何人（包括游客）可以浏览和查看此栏目下的信息。<br>
                        <asp:RadioButton ID="radbkf" runat="server" Text="半开放栏目" GroupName="PurviewType" />&nbsp;&nbsp;&nbsp;&nbsp;任何人（包括游客）都可以浏览。游客不可查看，其他会员根据会员组的栏目权限设置决定是否可以查看。<br>
                        <asp:RadioButton ID="radrz" runat="server" Text="认证栏目" GroupName="PurviewType" />&nbsp;&nbsp;&nbsp;&nbsp;游客不能浏览和查看，其他会员根据会员组的栏目权限设置决定是否可以浏览和查看。 <br>
                        <asp:RadioButton ID="radgd" runat="server" Text="归档栏目" GroupName="PurviewType" />&nbsp;&nbsp;&nbsp;&nbsp;任何人（包括会员、管理员）都不可以在此栏目下添加信息。 
                        </dd>          
                </dl>
                    <dl style="display:none">
                    <dt>是否允许评论：</dt>
                    <dd>    
                        <asp:CheckBox ID="chkIsEnableComment" runat="server" Checked=true Text="是" />（默认为是）
                    </dd>
                    </dl>
                    <dl>
                    <dt>审核流程：</dt>
                    <dd><asp:DropDownList ID="ddlReviewFlow" runat="server"></asp:DropDownList></dd>
                    </dl> 
                    <dl>
                        <dt>栏目说明：</dt>
                        <dd id="ddMenuDesc">
                        <uc1:Editor ID="Editor1" runat="server" width=700 height="150" EditorType=1/>
                        </dd>
                    </dl>
                    <div style="clear:left"></div>
                 </fieldset>                 
                 
                 
                 
                <fieldset style="display: none;">                
                    <dl>
                        <dt>栏目首页模板：</dt>
                        <dd><asp:TextBox Width=280 ID="txtDefaultTemplate" runat="server"></asp:TextBox> <input type="button" class="subButton" onclick="selectTemplateFile('txtDefaultTemplate')" value="选择模板..." /> (例如：/内容页模板/默认文章内容页模板.html)</dd>          
                    </dl>
                    <dl runat="server" id="dlListPageTemplate">
                        <dt>栏目列表页模板：</dt>
                        <dd><asp:TextBox Width=280 ID="txtListPageTemplate" runat="server"></asp:TextBox> <input type="button" class="subButton" onclick="selectTemplateFile('txtListPageTemplate')" value="选择模板..." /> </dd>          
                    </dl>     
                    <%--<dl runat="server" id="dlContentTemplate">
                        <dt>内容页模板：</dt>
                        <dd><asp:TextBox Width=280 ID="txtContentTemplate" runat="server"></asp:TextBox> <input type="button" class="subButton" onclick="selectTemplateFile('txtContentTemplate')" value="选择模板..." /> </dd>          
                    </dl>     --%> 
                    <div style="clear:left"></div>
                 </fieldset>
                 
                 
                 <fieldset style="display: none;">                
                    <dl style="display:none">
                        <dt>列表页生成HTML：</dt>
                        <dd>
                        <asp:RadioButton ID="radCreateListPageFalse" runat="server" Text="生成HTML" Checked="true" GroupName="IsCreateListPage" /><br>
                        <asp:RadioButton ID="radCreateListPageTrue" runat="server" Text="不生成HTML，使用动态页方式" GroupName="IsCreateListPage" />
                        </dd>          
                    </dl>
                    <dl style="display:none">
                        <dt>增量更新HTML页数：</dt>
                        <dd><asp:TextBox Width=280 ID="txtIncrementalUpdatePages" runat="server"></asp:TextBox></dd>          
                    </dl>     
                    <dl style="display:none">
                        <dt>缓存动态页首页：</dt>
                        <dd><asp:CheckBox ID="chkIsEnableIndexCache" runat="server" Text="当栏目页为动态方式时，缓存本栏目的首页内容" /></dd>          
                    </dl>      
                    <dl style="display:none">
                        <dt>列表页生成类型，以栏目英文名称进行命名：</dt>
                        <dd>
                            <asp:RadioButton ID="radListPageSavePathType1" runat="server" Text="列表文件分目录保存在所属栏目的文件夹中，如果所属栏目未设置文件夹，则保存在父栏目文件夹中" GroupName="ListPageSavePathType" /><br>
                            例： News / index_1.html      （栏目列表第一页）<br>
                            <asp:RadioButton ID="radListPageSavePathType2" runat="server" Text="列表文件统一保存在指定的“list”文件夹中" GroupName="ListPageSavePathType" /><br>
                            例： list / gwxw_236_1.html （栏目列表第一页）<br>
                            <asp:RadioButton ID="radListPageSavePathType3" runat="server" Text="列表文件统一保存在一级栏目文件夹中" GroupName="ListPageSavePathType" /><br>
                            例：gwxw_236.html（栏目首页）<br>
                            gwxw_236_1.html（栏目列表第一页）<br>
                        </dd>          
                    </dl> 
                    <dl style="display:none">
                        <dt>列表页后缀：</dt>
                        <dd>
                        <asp:DropDownList ID="ddlListPagePostFix" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="html">html</asp:ListItem>
                            <asp:ListItem Value="htm">htm</asp:ListItem>
                            <asp:ListItem Value="shtml">shtml</asp:ListItem>
                            <asp:ListItem Value="shtm">shtm</asp:ListItem>
                        </asp:DropDownList>
                        </dd>          
                    </dl>
                    <%--<dl style="display:none">
                        <dt>内容页生成HTML：</dt>
                        <dd>
                        <asp:RadioButton ID="radCreateContentPageFalse" runat="server" Checked="true" Text="生成HTML" GroupName="IsCreateContentPage" /><br>
                        <asp:RadioButton ID="radCreateContentPageTrue" runat="server" Text="不生成HTML，使用动态页方式" GroupName="IsCreateContentPage" />
                        </dd>          
                    </dl>
                    <dl>
                        <dt>内容页的文件名规则：</dt>
                        <dd>
                            <asp:RadioButton ID="radContentPageSavePathType1" runat="server" Checked="true" Text=" 统一保存在指定的“c”文件夹中,并且按月份保存" GroupName="ContentPageSavePathType" /><br>
                            如： c / 201008 / 100000978028767.html<br>
                            <asp:RadioButton ID="radContentPageSavePathType2" runat="server" Text=" 统一保存在指定的“c”文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： c / 100000978028767.html<br>
                            
                            <asp:RadioButton ID="radContentPageSavePathType5" runat="server" Text=" 内容页文件保存在所属频道（一级栏目）的“c”文件夹中,并且按月份保存" GroupName="ContentPageSavePathType" /><br>
                            如： News / c / 201008 / 100000978028767.html<br>
                            <asp:RadioButton ID="radContentPageSavePathType6" runat="server" Text=" 内容页文件保存在所属频道（一级栏目）的“c”文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： News / c / 100000978028767.html<br>
                            
                            <asp:RadioButton ID="radContentPageSavePathType4" runat="server" Text=" 内容页文件保存在所属频道（一级栏目）的文件夹中,并且按月份保存" GroupName="ContentPageSavePathType" /><br>
                            如： News /  201008 / 100000978028767.html<br>
                            <asp:RadioButton ID="radContentPageSavePathType3" runat="server" Text=" 内容页文件保存在所属频道（一级栏目）的文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： News / 100000978028767.html<br>
                           
                        </dd> 
                    </dl>
                    --%>
                    <dl runat="server" id="dlContentTemplate">
                        <dt>内容页程序路径：</dt>
                        <dd><asp:TextBox Width=280 ID="txtContentTemplate" runat="server"></asp:TextBox>不填则为模块程序路径下content.aspx,<br />如果本栏目内容页是非content.aspx,或者与前台列表不在同一文件夹，则必须填写，例如：/news/newsDetail.aspx </dd>          
                    </dl>
                    <dl>
                      
                        <dt>内容页路径规则：</dt>
                        <dd>
                            
                            <asp:RadioButton ID="radContentPageSavePathType6"  runat="server" Checked="true" Text=" 内容页文件保存在所属栏目目录名文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： News / CompanyNews/100000978028767.html<br>
                            <asp:RadioButton ID="radContentPageSavePathType5" runat="server" Text=" 内容页文件保存在所属栏目名称文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： 新闻中心 / 公司新闻 / 100000978028767.html<br>
                            <span style="display:none">
                            <asp:RadioButton ID="radContentPageSavePathType1" runat="server" Text=" 统一保存在指定的“c”文件夹中,并且按月份保存" GroupName="ContentPageSavePathType" /><br>
                            如： c / 201008 / 100000978028767.html<br>
                            <asp:RadioButton ID="radContentPageSavePathType2" runat="server" Text=" 统一保存在指定的“c”文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： c / 100000978028767.html<br>
                            
                            </span>
                            
                            
                            <asp:RadioButton ID="radContentPageSavePathType4" runat="server" Text=" 内容页文件保存在所属频道（一级栏目）的文件夹中,并且按月份保存" GroupName="ContentPageSavePathType" /><br>
                            如： News /  201008 / 100000978028767.html<br>
                            
                            <asp:RadioButton ID="radContentPageSavePathType3" runat="server" Text=" 内容页文件保存在所属频道（一级栏目）的文件夹中" GroupName="ContentPageSavePathType" /><br>
                            如： News / 100000978028767.html<br>
                            
                            <asp:RadioButton ID="radContentPageSavePathType8" runat="server" Text=" 文件内容保存到根目录，命名方式为：一级栏目+“_”+文件名名称，适合伪静态" GroupName="ContentPageSavePathType" /><br>
                            如： News_100000978028767.html<br>
                            
                            <asp:RadioButton ID="radContentPageSavePathType7" runat="server" Text=" 自定义 " GroupName="ContentPageSavePathType" />
                            <asp:TextBox ID="txtzdyURL" runat="server" Width="280" MaxLength="50"></asp:TextBox> 自定义路径，如 /News/Html
                            
                           
                        </dd> 
                    </dl>
                    <dl>
                        <dt>内容页命名规则：</dt>
                        <dd>
                        <asp:RadioButton ID="radCreateContentPageFalse" runat="server" Checked="true" Text="ID命名方式" GroupName="IsCreateContentPage" /> （如：100000978028767.html）<br>
                        <asp:RadioButton ID="radCreateContentPageTrue" runat="server" Text="标题命名方式" GroupName="IsCreateContentPage" /> （如：新形态的媒体力量.html）
                        </dd>          
                    </dl>
                    
                                       
                    <dl style="display:none">
                        <dt>自动生成关联页面的方式：</dt>
                        <dd>
                            <asp:RadioButton ID="radAutoCreateHtmlType1" runat="server" Text="手动生成，由管理员在生成管理手动生成全部所需的页面" GroupName="AutoCreateHtmlType" /><br>                            
                            <asp:RadioButton ID="radAutoCreateHtmlType2" runat="server" Text="只自动生成内容页" GroupName="AutoCreateHtmlType" /><br>                           
                            <asp:RadioButton ID="radAutoCreateHtmlType3" runat="server" Text="自动生成内容页和所属栏目的列表页" GroupName="AutoCreateHtmlType" /><br>   
                            <asp:RadioButton ID="radAutoCreateHtmlType4" runat="server" Text="自动生成内容页和所属栏目及父栏目的列表页" GroupName="AutoCreateHtmlType" /><br>  
                            <asp:RadioButton ID="radAutoCreateHtmlType5" runat="server" Text="自动生成内容页和所属栏目及父栏目的列表页以及自动关联的专题页" GroupName="AutoCreateHtmlType" /><br>  
                            <asp:RadioButton ID="radAutoCreateHtmlType6" runat="server" Text="自动生成所有关联的页（在发表、更新文章时，除了自动生成内容页和所属栏目及父栏目的列表页以外，还会自动会生成指定栏目的列表页）。" GroupName="AutoCreateHtmlType" />                         
                        </dd>          
                    </dl> 
                    <div style="clear:left"></div>
                 </fieldset>                 
                 
                 
                 <fieldset style="display: none;">
                 <div style="padding:20px">
                 <asp:GridView ID="grvRight" runat="server" AutoGenerateColumns="False" 
                         onrowcreated="grvRight_RowCreated1" CellPadding="0" CellSpacing="2" >    
                         <RowStyle BackColor="#EDEDF3" ForeColor="#4A3C8C" Height="30" HorizontalAlign="Center"/>
                        <HeaderStyle BackColor="#666699" Font-Bold="True" ForeColor="#F7F7F7" Height="40" />
                        <AlternatingRowStyle BackColor="#F7F7F7"/>                
                </asp:GridView>
                </div>
                 <div style="clear:left"></div>
                 </fieldset> 
                 
                 
                <fieldset style="display: none;">
                    <dl>
                        <dt>自设内容项目数：</dt>
                        <dd><asp:DropDownList ID="ddlCustomContentCount" runat="server" onChange="SelectChange();"></asp:DropDownList></dd>
                     </dl>
                     <dl>
                        <dt>自设内容1：<br /><input type="checkbox" id="chkIsHtmlEditor1" value="1" name="chkIsHtmlEditor1" onclick="ChangeEditor(1,this.checked)" runat="server"/><label for="chkIsHtmlEditor1">支持HTML</label></dt>
                        <dd id="dd1"><asp:TextBox Width="600" Height="150" ID="txtCustomContent1" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom2" style="display:none">
                        <dt>自设内容2：<br /><input type="checkbox" id="chkIsHtmlEditor2" value="1" name="chkIsHtmlEditor2" onclick="ChangeEditor(2,this.checked)" runat="server"/><label for="chkIsHtmlEditor2">支持HTML</label></dt>
                        <dd id="dd2"><asp:TextBox Width="600" Height="150" ID="txtCustomContent2" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom3" style="display:none">
                        <dt>自设内容3：<br /><input type="checkbox" id="chkIsHtmlEditor3" value="1" name="chkIsHtmlEditor3" onclick="ChangeEditor(3,this.checked)" runat="server"/><label for="chkIsHtmlEditor3">支持HTML</label></dt>
                        <dd id="dd3"><asp:TextBox Width="600" Height="150" ID="txtCustomContent3" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom4" style="display:none">
                        <dt>自设内容4：<br /><input type="checkbox" id="chkIsHtmlEditor4" value="1" name="chkIsHtmlEditor4" onclick="ChangeEditor(4,this.checked)" runat="server"/><label for="chkIsHtmlEditor4">支持HTML</label></dt>
                        <dd id="dd4"><asp:TextBox Width="600" Height="150" ID="txtCustomContent4" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom5" style="display:none">
                        <dt>自设内容5：<br /><input type="checkbox" id="chkIsHtmlEditor5" value="1" name="chkIsHtmlEditor5" onclick="ChangeEditor(5,this.checked)" runat="server"/><label for="chkIsHtmlEditor5">支持HTML</label></dt>
                        <dd id="dd5"><asp:TextBox Width="600" Height="150" ID="txtCustomContent5" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom6" style="display:none">
                        <dt>自设内容6：<br /><input type="checkbox" id="chkIsHtmlEditor6" value="1" name="chkIsHtmlEditor6" onclick="ChangeEditor(6,this.checked)" runat="server"/><label for="chkIsHtmlEditor6">支持HTML</label></dt>
                        <dd id="dd6"><asp:TextBox Width="600" Height="150" ID="txtCustomContent6" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom7" style="display:none">
                        <dt>自设内容7：<br /><input type="checkbox" id="chkIsHtmlEditor7" value="1" name="chkIsHtmlEditor7" onclick="ChangeEditor(7,this.checked)" runat="server"/><label for="chkIsHtmlEditor7">支持HTML</label></dt>
                        <dd id="dd7"><asp:TextBox Width="600" Height="150" ID="txtCustomContent7" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom8" style="display:none">
                        <dt>自设内容8：<br /><input type="checkbox" id="chkIsHtmlEditor8" value="1" name="chkIsHtmlEditor8" onclick="ChangeEditor(8,this.checked)" runat="server"/><label for="chkIsHtmlEditor8">支持HTML</label></dt>
                        <dd id="dd8"><asp:TextBox Width="600" Height="150" ID="txtCustomContent8" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom9" style="display:none">
                        <dt>自设内容9：<br /><input type="checkbox" id="chkIsHtmlEditor9" value="1" name="chkIsHtmlEditor9" onclick="ChangeEditor(9,this.checked)" runat="server"/><label for="chkIsHtmlEditor9">支持HTML</label></dt>
                        <dd id="dd9"><asp:TextBox Width="600" Height="150" ID="txtCustomContent9" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom10" style="display:none">
                        <dt>自设内容10：<br /><input type="checkbox" id="chkIsHtmlEditor10" value="1" name="chkIsHtmlEditor10" onclick="ChangeEditor(10,this.checked)" runat="server"/><label for="chkIsHtmlEditor10">支持HTML</label></dt>
                        <dd id="dd10"><asp:TextBox Width="600" Height="150" ID="txtCustomContent10" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom11" style="display:none">
                        <dt>自设内容11：<br /><input type="checkbox" id="chkIsHtmlEditor11" value="1" name="chkIsHtmlEditor11" onclick="ChangeEditor(11,this.checked)" runat="server"/><label for="chkIsHtmlEditor11">支持HTML</label></dt>
                        <dd id="dd11"><asp:TextBox Width="600" Height="150" ID="txtCustomContent11" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom12" style="display:none">
                        <dt>自设内容12：<br /><input type="checkbox" id="chkIsHtmlEditor12" value="1" name="chkIsHtmlEditor12" onclick="ChangeEditor(12,this.checked)" runat="server"/><label for="chkIsHtmlEditor12">支持HTML</label></dt>
                        <dd id="dd12"><asp:TextBox Width="600" Height="150" ID="txtCustomContent12" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom13" style="display:none">
                        <dt>自设内容13：<br /><input type="checkbox" id="chkIsHtmlEditor13" value="1" name="chkIsHtmlEditor13" onclick="ChangeEditor(13,this.checked)" runat="server"/><label for="chkIsHtmlEditor13">支持HTML</label></dt>
                        <dd id="dd13"><asp:TextBox Width="600" Height="150" ID="txtCustomContent13" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom14" style="display:none">
                        <dt>自设内容14：<br /><input type="checkbox" id="chkIsHtmlEditor14" value="1" name="chkIsHtmlEditor14" onclick="ChangeEditor(14,this.checked)" runat="server"/><label for="chkIsHtmlEditor14">支持HTML</label></dt>
                        <dd id="dd14"><asp:TextBox Width="600" Height="150" ID="txtCustomContent14" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom15" style="display:none">
                        <dt>自设内容15：<br /><input type="checkbox" id="chkIsHtmlEditor15" value="1" name="chkIsHtmlEditor15" onclick="ChangeEditor(15,this.checked)" runat="server"/><label for="chkIsHtmlEditor15">支持HTML</label></dt>
                        <dd id="dd15"><asp:TextBox Width="600" Height="150" ID="txtCustomContent15" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom16" style="display:none">
                        <dt>自设内容16：<br /><input type="checkbox" id="chkIsHtmlEditor16" value="1" name="chkIsHtmlEditor16" onclick="ChangeEditor(16,this.checked)" runat="server"/><label for="chkIsHtmlEditor16">支持HTML</label></dt>
                        <dd id="dd16"><asp:TextBox Width="600" Height="150" ID="txtCustomContent16" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom17" style="display:none">
                        <dt>自设内容17：<br /><input type="checkbox" id="chkIsHtmlEditor17" value="1" name="chkIsHtmlEditor17" onclick="ChangeEditor(17,this.checked)" runat="server"/><label for="chkIsHtmlEditor17">支持HTML</label></dt>
                        <dd id="dd17"><asp:TextBox Width="600" Height="150" ID="txtCustomContent17" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom18" style="display:none">
                        <dt>自设内容18：<br /><input type="checkbox" id="chkIsHtmlEditor18" value="1" name="chkIsHtmlEditor18" onclick="ChangeEditor(18,this.checked)" runat="server"/><label for="chkIsHtmlEditor18">支持HTML</label></dt>
                        <dd id="dd18"><asp:TextBox Width="600" Height="150" ID="txtCustomContent18" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom19" style="display:none">
                        <dt>自设内容19：<br /><input type="checkbox" id="chkIsHtmlEditor19" value="1" name="chkIsHtmlEditor19" onclick="ChangeEditor(19,this.checked)" runat="server"/><label for="chkIsHtmlEditor19">支持HTML</label></dt>
                        <dd id="dd19"><asp:TextBox Width="600" Height="150" ID="txtCustomContent19" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                    <dl id="dlCustom20" style="display:none">
                        <dt>自设内容20：<br /><input type="checkbox" id="chkIsHtmlEditor20" value="1" name="chkIsHtmlEditor20" onclick="ChangeEditor(20,this.checked)" runat="server"/><label for="chkIsHtmlEditor20">支持HTML</label></dt>
                        <dd id="dd20"><asp:TextBox Width="600" Height="150" ID="txtCustomContent20" runat="server" TextMode="MultiLine"></asp:TextBox></dd>          
                    </dl>
                     
                    <div style="clear:left"></div>
                 </fieldset>
            </div>
            <div class="Submit">
                <asp:Button ID="btnHidAction" runat="server" OnClick="btnDel_Click" style="display:none" />
                <asp:Button ID="btnEdit" runat="server" CssClass="subButton" Text="<%$Resources:Common,Add %>" OnClientClick="return changeTabOne();" OnClick="btnEdit_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="subButton" Text="<%$Resources:Common,Del %>"  OnClientClick="selfconfirm({msg:'确定要执行删除操作吗？',fn:function(data){setAction1(data)}});return false;"/>
                <input type="button" name="Submit422" Class="subButton" value="<%= Resources.Common.Back %>" onclick='location.href="ColumnManage.aspx?NodeCode=<%=NodeCode%>";'>
            </div>
        </div>
    </form>
</body>
</html>
