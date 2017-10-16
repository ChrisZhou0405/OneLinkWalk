<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Category.Edit" EnableEventValidation="false" %>
<%@ Register Src="../controls/Editor.ascx" TagName="Editor" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品分类编辑</title>
    <script src="../JS/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-validationEngine-cn.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script src="../JS/publicform.js" type="text/javascript"></script>
    <script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    
    
    <link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />
    <link href="../ColorPicker/colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/public.css" rel="stylesheet" type="text/css" />
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $("#theForm").validationEngine({ promptPosition: "centerRight" });
        });
        
        function msg() {
            if (type > -1) {
                var name = "商品分类编辑";
                var listUrl = "List.aspx?NodeCode=<%=NodeCode%>";
                var addUrl = "Edit.aspx?Action=NEW&NodeCode=<%=NodeCode%>";
                var updateUrl = "?Action=EDIT&NodeCode=<%=NodeCode%>&id=" + id;
                showEditMessage(name, listUrl, addUrl, updateUrl);
            }
            
        }  
    </script>
    
</head>
<body>
    <form id="theForm" runat="server">
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hidParentID" runat="server" />
    
    <div class="container">
        <h4>
            位置： <a href="/sysadmin/console/Index.aspx">管理首页</a><a> &gt;</a> <a href="/sysadmin/Category/List.aspx">商品分类</a><a> &gt;</a> <span class="breadcrumb_current">新增 / 编辑</span>
        </h4>
        
        <div id="panel">
            <fieldset>
                <dl>
                    <dt>分类名称：</dt>
                    <dd><asp:TextBox  ID="txtName" Width="200"  runat="server"></asp:TextBox><span style="color:#ff0000;"> * </span></dd>
                </dl>
                <dl>
                    <dt>上级分类：</dt>
                    <dd><asp:DropDownList ID="ddlParentCategory" runat="server">
                           <asp:ListItem Value="0">├顶级分类</asp:ListItem>
                       </asp:DropDownList></dd>
                </dl>
                <dl>
                    <dt>所属内容栏目：</dt>
                    <dd><asp:DropDownList ID="ddlContent" runat="server">
                           <asp:ListItem Value="0">├产品栏目</asp:ListItem>
                       </asp:DropDownList></dd>
                </dl>
                <dl>
                    <dt>是否显示：</dt>
                    <dd><asp:RadioButtonList ID="rblIsVaild" RepeatDirection="Horizontal" runat="server">
                          <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                          <asp:ListItem Value="0">否</asp:ListItem>
                       </asp:RadioButtonList></dd>
                </dl>
                <dl>
                    <dt>首页显示：</dt>
                    <dd><asp:RadioButtonList ID="rblIsIndex" RepeatDirection="Horizontal" runat="server">
                          <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                          <asp:ListItem Value="0">否</asp:ListItem>
                       </asp:RadioButtonList></dd>
                </dl>
                <dl>
                    <dt>排序：</dt>
                    <dd><asp:TextBox ID="txtOrders" Width="60" Text="0" runat="server"></asp:TextBox> </dd>
                </dl>
                <dl>
                    <dt>URL重写：</dt>
                    <dd><asp:TextBox ID="txtURLRewriter" Width="380" runat="server"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt>分类图标：</dt>
                    <dd><asp:TextBox ID="txtImg" Width="380" runat="server"></asp:TextBox>
                     <input type='button' onclick="InputImages('theForm', 'txtImg', 1, '', 4096, '',0,0,0,'0','0')"
                        value='上传图片' />
                    <input type='button' onclick="ShowImages('txtImg', '<%=GetUploadImgUrl()%>','image')"
                        value='预览图片' />
                    <input type='button' onclick="if($('#txtImg').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'txtImg', '<%=GetUploadImgUrl()%>') } });}"
                        value='删除' />
                    </dd>
                </dl>
                <dl>
                    <dt>分类说明：</dt>
                    <dd>
                    <uc1:Editor ID="Editor1" runat="server" width="700" height="150" EditorType="1"/>
                    </dd>
                </dl>
                <dl>
                    <dt>页面标题(SEO)：</dt>
                    <dd><asp:TextBox ID="txtPageTitle" Width="500" runat="server"></asp:TextBox> 设置分类的关键字，让用户可以通过搜索引擎搜索到此分类的浏览页面</dd>
                </dl>
                <dl>
                    <dt>页面关键字(SEO)：</dt>
                    <dd><asp:TextBox ID="txtPageKeywords" Width="500" runat="server"></asp:TextBox> 设置分类的页面标题</dd>
                </dl>
                <dl>
                    <dt>页面描述(SEO)：</dt>
                    <dd><div style="float:left"><asp:TextBox ID="txtPageDescription" TextMode="MultiLine" Width="500" Height="60" runat="server"></asp:TextBox></div><div style="float:left;padding-left:5px"> 告诉搜索引擎此分类浏览页面的主要内容，有助于搜索引擎更好的收录此分类浏览页面</div></dd>
                </dl>
            </fieldset>
          </div>
          <div class="Submit" style="padding:10px 0 0 205px;">
            <asp:Button ID="Button1" Text="添 加" runat="server" CssClass="subButton" OnClick="Button1_Click" /> 
                       <input type="button" value="返 回" class="subButton" onclick="location.href='List.aspx?NodeCode=<%=NodeCode%>';" />
                       </div>
    </div>
    </form>
</body>
</html>
