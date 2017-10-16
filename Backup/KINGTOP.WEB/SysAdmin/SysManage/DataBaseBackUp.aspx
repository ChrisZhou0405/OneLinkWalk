<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseBackUp.aspx.cs"
    Inherits="KingTop.WEB.SysAdmin.SysManage.DataBaseBackUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据库备份</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>

    <script type="text/javascript" src="../js/publicform.js"></script>

    <script type="text/javascript" src="../js/listcheck.js"></script>

    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery.dialog.js"></script>

    <script type="text/javascript" src="../js/win.js"></script>

    <script type="text/javascript" src="../js/common.js"></script>

    <script src="../Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript">
        function tiaozhuan(backfilepath) {

            if (backfilepath == "") {
                alert({ msg: '请到-系统设置-数据库设置 下设置bak文件备份路径!', title: '提示信息' });
            }
            changeMenu('Bak');
        }
        function changeMenu(name) {
            $("#tags li").each(function() {
                $(this).removeClass("selectTag");
            });
            $("#li" + name).addClass("selectTag");
            if (name == 'Sql') {
                $("#divSql").show();
                $("#divBak").hide();
            }
            else {
                $("#divSql").hide();
                $("#divBak").show();
            }

        }
        function showTip() {
            $("#divBak").hide();
            $("#divSql").hide();
            $("#tipDiv").show();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>
        <ul id="tags">
            <li class="selectTag" id="liBak"><a href="javascript:tiaozhuan('<%=strBakFilePath %>');">数据库备份</a></li>
            <%--<li id="liSql"><a href="javascript:changeMenu('Sql')">sql文件备份</a>--%>
            </li>
            
        </ul>
        <div class="mynotesearch">
            <div id="tipDiv" style="display:none;width:100%;text-align:center;padding:100px 0;">
            <img src="../images/loading.gif" border=0 /> 请不要关闭，正在备份数据库...
            </div>
            <div id="divBak">
                <div style="padding-left: 10px; background-color: #F5F5F5;">
                    <table width="90%">
                        <tr>
                            <td height="30px"><img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: middle" /></td>
                            <td>1.</td>
                            <td>数据库与网站文件是同一台服务器时，则备份文件保存到网站目录下的dbbackup文件夹</td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top" height="30px"><img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: middle" /></td>
                            <td style="vertical-align:top">2.</td>
                            <td style="line-height:22px;">
                            数据库与网站文件不是同一台服务器，需要在数据库服务器的机器上设置备份目录（<font color="red">如果您没有数据库服务器的操作权限，则需要联系空间提供商，让对方提供一个备份目录</font>），
                            然后设置备份目录:
                            <asp:TextBox ID="txtFilesDir" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSetFilesDir" runat="server" Text="设置" CssClass="subButton" OnClick="btnSetFilesDir_Click" />
                            （注：需要填写全路径，例如：C:\web\dbback）
                            </td>
                        </tr>
                        <tr>
                            <td height="30px"><img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: bottom" /></td>
                            <td>3.</td>
                            <td style="line-height:22px;">为了您的数据安全，请备份后将文件下载到本地，然后（通过FTP）删除文件，需要还原的时候，将备份文件通过FTP上传到相应目录，再执行还原操作即可；<br /></td>
                        </tr>
                    </table>
                    </div>
                <div class="Submit">
                    <asp:Button ID="btnSave2" runat="server" Text="备份数据库" CssClass="subButton" OnClick="btnSave2_Click" OnClientClick="showTip();"/></div>
            </div>
            <div id="divSql" style="display: none">
                <div id="searchContainer" style="padding-left: 10px;line-height:22px;">
                    <img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: bottom" />此种备份方式会在网站服务器的dbbackup目录中生成sql备份文件，<font color=red>注：此方式不适合数据量大的备份</font>
                    <%--<br />
                    <img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: bottom" />不能备份10M以上的数据表<br />--%><br />
                    
                    <img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: bottom" />为了您的数据安全，请备份后将文件下载到本地，然后（通过FTP）删除文件，需要还原的时候，将备份文件通过FTP上传到相应目录，再执行还原操作即可；<br />
                    <img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: bottom" />备份表结构在还原时先删除数据表，然后再插入数据；<br />
                    <img src="../images/dialogImage/iconAlert.gif" style="height: 19px; width: 22px; vertical-align: bottom" />不备份表结构，还原的时候如果数据库中已经存在数据，则可能会存在数据冲突，导致还原失败；<br />
                    </div>
                <div style="text-align:right; padding-top:20px; padding-bottom:10px; padding-right:150px;">
                    <input type="checkbox" id="chkIsCreateTable" checked runat="server" />是否备份表结构 &nbsp; 
                    <asp:Button ID="Button1" runat="server" CssClass="subButton" Text="备份数据表" OnClientClick="if(GetSelectTitle()){showTip();}else{return false;}"
                        OnClick="btnSave_Click" />
                </div>
                <ul class="ulheader">
                    <li style="width: 100px; text-align: center">
                        <input type="checkbox" name="checkBoxAll" id="checkBoxAll" value="" /></li>
                    <li style="width: 40%">数据库表</li>
                    <li style="width: 10%">记录条数</li>
                    <li style="width: 10%">使用空间</li>
                    <li style="width: 10%">数据库大小</li>
                    <li style="width: 10%">索引大小</li>
                </ul>
                <asp:Repeater ID="rptDataTableList" runat="server">
                    <ItemTemplate>
                        <ul class="ulheader ulbody">
                            <li style="width: 100px; text-align: center">
                                <input type="checkbox" name="chkId" value='<%#Eval("name")%>' <%#KingTop.Common.Utils.ParseInt(Eval("reserved").ToString().Replace("KB",""),0)>10000?"disabled":""%> />
                            </li>
                            <li style="width: 40%"><span id='Title<%#Eval("name") %>'>
                                <%#Eval("name")%></span> </li>
                            <li style="width: 10%">
                                <%#Eval("rows")%>
                            </li>
                            <li style="width: 10%">
                                <%#Eval("reserved")%>
                            </li>
                            <li style="width: 10%">
                                <%#Eval("data")%>
                            </li>
                            <li style="width: 10%">
                                <%#Eval("index_size")%>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                   <div style="text-align:right; padding-top:20px; padding-right:150px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="subButton" Text="开始备份数据" OnClientClick="if(GetSelectTitle()){showTip();}else{return false;}"
                        OnClick="btnSave_Click" /></div>
            </div>
            
        </div>
    </div>
    </form>
</body>
</html>
