<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteInfoSet.aspx.cs" Inherits="KingTop.WEB.SysAdmin.SysManage.SiteInfoSet" %>

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
                    <dt width="30%"><b>网站名称:</b></dt>
                    <dd width="70%"><asp:TextBox ID="txtSiteName" runat="server" width="300px"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt width="30%"><b>网站标题:</b></dt>
                    <dd width="70%"><asp:TextBox ID="txtSiteTitle" runat="server" width="300px"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt width="30%"><b>网站地址:</b></dt>
                    <dd width="70%"><asp:TextBox ID="txtSiteURL" runat="server" width="300px"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt width="30%"><b>网站Logo:</b></dt>
                    <dd width="70%"><asp:TextBox ID="txtLogo" runat="server" width="300px"></asp:TextBox>
                    
                     <input type='button' class="btn" onclick="InputImages('theForm', 'txtLogo', 1, '', 4096, '',125,125,0,'0','0')" value = '上传图片' /> 
                     <input type='button' class="btn" onclick="ShowImages('txtLogo', '/UploadFiles/Images/','image')" value = '预览图片'/> 
                     <input type='button' class="btn" onclick="if($('#txtLogo').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'txtLogo', '/UploadFiles/Images/') } });}"  value = '删除' />
                    </dd>
                </dl>
                <dl>
                    <dt width="30%"><b>网站favicon.ico图标:</b></dt>
                    <dd width="70%"><asp:TextBox ID="txtFavicon" runat="server" width="300px"></asp:TextBox>
                    <input type='button' class="btn" onclick="InputImages('theForm', 'txtFavicon', 1, '', 4096, '',125,125,0,'0','0')" value = '上传图片' /> 
                     <input type='button' class="btn" onclick="ShowImages('txtFavicon', '/UploadFiles/Images/','image')" value = '预览图片'/> 
                     <input type='button' class="btn" onclick="if($('#txtFavicon').val()!=''){selfconfirm({ msg: '确定要执行删除操作吗？', fn: function(data) { FilesDel(data,'txtFavicon', '/UploadFiles/Images/') } });}"  value = '删除' />
                    
                    <br />应用示例：&lt;link href=&quot;uploadfiles/images/favicon.ico&quot; type=&quot;image/x-icon&quot; rel=&quot;shortcut icon&quot; /&gt;</dd>
                </dl>
                <dl>
                    <dt width="30%"><b>版权信息:</b><br>支持HTML标记 </dt>
                    <dd width="70%"><asp:TextBox ID="txtCopyRight" runat="server" TextMode="MultiLine" width="400px" Height="100px"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt width="30%"><b>网站META关键词:</b><br>针对搜索引擎设置的关键词</dt>
                    <dd width="70%"><asp:TextBox ID="txtMetaKeywords" runat="server" TextMode="MultiLine" width="400px" Height="100px"></asp:TextBox></dd>
                </dl>
                <dl>
                    <dt width="30%"><b>网站META网页描述:</b><br>针对搜索引擎设置的网页描述 </dt>
                    <dd width="70%"><asp:TextBox ID="txtMetaDescription" runat="server" TextMode="MultiLine" width="400px" Height="100px"></asp:TextBox></dd>
                </dl>
        </div>     
        <div class="Submit" style="padding-left:300px;">
        <asp:Button Text="保存设置" CssClass="subButton" ID="btnSave" runat="server" onclick="btnSave_Click" />
        </div>
    </div>
    </form>
</body>
</html>
