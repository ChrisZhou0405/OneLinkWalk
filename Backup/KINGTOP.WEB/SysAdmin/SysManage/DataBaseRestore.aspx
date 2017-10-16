<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseRestore.aspx.cs"
    Inherits="KingTop.WEB.SysAdmin.SysManage.DataBaseRestore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据库还原</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/listcheck.js"></script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script src="../Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
    function changeMenu(name) {
            $("#tags li").each(function() {
                $(this).removeClass("selectTag");
            });
            $("#li" + name).addClass("selectTag");
            if (name == 'Sql') {
                $("#divSql").show();
                $("#divBak").hide();
                $("#divMemo").hide();
            }
            else if (name == 'Bak') {
                $("#divSql").hide();
                $("#divBak").show();
                $("#divMemo").hide();
            }
            else {
                $("#divSql").hide();
                $("#divBak").hide();
                $("#divMemo").show();
            }

        }

        function setAction2(IsOk) {
            if (IsOk == "true") {
                if (GetSelectTitle()) {
                    showTip();
                    $("#btnHidAction2").click();
                }
            }
        }
        function showTip() {
            $("#divBak").hide();
            $("#divSql").hide();
            $("#divMemo").hide();
            $("#tipDiv").show();
        }
   </script>
   

</head>
<body>
    <form id="form1" runat="server">
   <asp:Button ID="btnHidAction" runat="server" OnClick="btnDelete_Click" Style="display: none" />
   <asp:Button ID="btnHidAction2" runat="server" OnClick="lnkbRestoreDB_Click" Style="display: none" />
    
    <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>
        <ul id="tags">
            <%--<li class="selectTag" id="liSql"><a href="javascript:changeMenu('Sql')">sql文件备份</a>
            </li>--%>
            <li id="liBak" class="selectTag"><a href="javascript:changeMenu('Bak');"><b>数据库还原</b></a></li>
            <li id="liMemo"><a href="javascript:changeMenu('Memo')"><b>还原须知</b></a>
            </li>
        </ul>
        <div class="mynotesearch">
            <div id="tipDiv" style="display:none;width:100%;text-align:center;padding:100px 0;">
                <img src="../images/loading.gif" border=0 /> 请不要关闭，正在还原数据库，强行关闭可能会导致数据库损坏......
            </div>
            <div id="divMemo" style="display:none;padding-left: 10px;line-height:22px;">
                    <ul>
                    <li style="border-bottom:1px solid #CCCCCC;padding:10px">1.不支持不同空间（网站）的备份文件进行还原，只支持此网站空间的数据库备份文件进行还原<br /></li>
                    <li style="border-bottom:1px solid #CCCCCC;padding:10px">2.数据库与网站文件是同一台服务器，备份文件存放地址是网站的dbbackup目录，支持将备份文件下载到本地，然后删除服务<br />器上的备份文件，需要还原的时候直接将文件上传到此目录即可<br /></li>
                    <li style="border-bottom:1px solid #CCCCCC;padding:10px">3.数据库与网站文件不是同一台服务器，则备份文件存放地址在数据库服务器的备份设置目录（注：在“数据库备份”设置,如图<br /><img src="../images/dbbak1.jpg" />)，如果此文件夹中的备份文件被删除，则还原不成功<br /></li>
                    <li style="border-bottom:1px solid #CCCCCC;padding:10px">4.数据库帐号必须拥有“dbcreator”或者“sysadmin”服务器角色才能还原，如下图，<font color=red>如果还原操作的时候失败，很可能是帐号权限<br />不够，需要联系空间服务商添加相应权限即可</font><br /><img src="../images/dbbak2.jpg" /><br /></li>
                    <li style="border-bottom:1px solid #CCCCCC;padding:10px">5.支持其它账户进行还原：
                    启用其他账户：<asp:CheckBox ID="cbUse" runat="server"/> &nbsp; 
                    用户名：<asp:TextBox ID="txtUID" runat=server Width="60px" style="border:1px solid #CCCCCC"></asp:TextBox> &nbsp; 
                    密码：<asp:TextBox ID="txtPwd" TextMode="Password" runat=server Width="60px" style="border:1px solid #CCCCCC"></asp:TextBox>
                    <asp:Button ID="btnSet" runat="server" CssClass="subButton" Text="确定"
                        OnClick="btnSave_Click" /></li>
                        </ul>
                    <br />
            </div>
            <div id="divBak">
                <ul class="ulheader">   
                 <li style="width: 50px; text-align: center">
                        &nbsp;</li>            
                    <li style="width: 35%">备份文件</li>
                  
                    <li style="width: 30%">创建时间</li>
                    <li style="width: 25%">操作</li>
                </ul>
                <asp:Repeater ID="rptBakFileList" runat="server">
                    <ItemTemplate>
                        <ul class="ulheader ulbody">    
                        <li style="width: 50px; text-align: center">
                        <%iLoop++; Response.Write(iLoop); %>
                                <input type="checkbox" style="display:none" name="chkId" value='<%#Eval("FileName")%>' />
                            </li>                      
                            <li style="width: 35%">
                              <span id='Title<%#Eval("FileName") %>'> <%#Eval("FileName")%></span>
                            </li>
                          
                            <li style="width: 30%">
                                <%#Eval("CreateTime")%>
                            </li>
                            <li style="width: 25%">
                                <%--<asp:LinkButton ID="lnkbRestore2" class="abtn" runat="server" OnCommand="lnkbRestore2_Click"
                                    CommandArgument='<%#Eval("FileName")%>' OnClientClick='<%#"return(confirm(\"确定要从此备份还原吗?\"))"%>'>还原</asp:LinkButton>
                                 --%>
                                 <asp:LinkButton ID="lnkbRestore2" class="abtn" runat="server"
                            CommandName="deldp" ToolTip='<%#Eval("FileName")%>' CommandArgument='<%#Eval("FileName") %>'
                            OnClientClick='selectThisRow();selfconfirm({msg:"确定要从此备份文件还原数据库吗？",fn:function(data){setAction2(data)}});return false;'>还原</asp:LinkButton> 


                                 <asp:LinkButton ID="lnkbDelete" class="abtn" runat="server"
                            CommandName="deldp" ToolTip='<%#Eval("FileName")%>' CommandArgument='<%#Eval("FileName") %>'
                            OnClientClick='selectThisRow();selfconfirm({msg:"确定要执行删除操作吗？",fn:function(data){setAction(data)}});return false;'>删除</asp:LinkButton> 

                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="clear: left;height:20px;">
                </div>
            </div>
            <div id="divSql" style="display:none">
              <div id="searchContainer" style="padding-left:10px; display:none">  
                    <img src="../img/dialogImage/iconAlert.gif" style="height: 19px; width: 22px;vertical-align:bottom" />
                   </div>
                    
               <div class="function">
                          <asp:Button ID="btnDelete" runat="server"  Text="删除" OnClientClick="selfconfirm({msg:'确定删除选中的备份文件吗？',fn:function(data){setAction(data);}});return false;"/>
                     </div>
                <ul class="ulheader">
                    <li style="width: 50px; text-align: center">
                        <input type="checkbox" name="checkBoxAll" id="checkBoxAll" value="" /></li>
                    <li style="width: 30%">文件名</li>
                    <li style="width: 15%">大小</li>
                    <li style="width: 15%">创建时间</li>
                    <li style="width: 25%">操作</li>
                </ul>
                <asp:Repeater ID="rptSqlFileList" runat="server">
                    <ItemTemplate>
                        <ul class="ulheader ulbody">
                            <li style="width: 50px; text-align: center">
                                <input type="checkbox" name="chkId" value='<%#Eval("FileName")%>' />
                            </li>
                            <li style="width: 30%">
                                <span id='Title<%#Eval("FileName") %>'><%#Eval("FileName")%></span>
                            </li>
                            <li style="width: 15%">
                                <%#Eval("FileSize")%>
                            </li>
                            <li style="width: 15%">
                                <%#Eval("CreateTime")%>
                            </li>
                            <li style="width: 25%">
                                <asp:LinkButton ID="lnkbRestore" class="abtn" runat="server" OnCommand="lnkbRestore_Click"
                                    CommandArgument='<%#Eval("FileName")%>' OnClientClick='<%#"return(confirm(\"确定要从此备份还原吗?\"))"%>'>还原</asp:LinkButton>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                  
              
            </div>
            
        </div>
    </div> <asp:Literal ID="ltljs" runat="server"></asp:Literal>
    <asp:HiddenField ID="hidIsCommIP" runat="server" Value="false" />
    </form>
</body>
</html>
