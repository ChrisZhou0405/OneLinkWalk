<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRJobCateList.aspx.cs"
    Inherits="KingTop.WEB.SysAdmin.HR.HRJobCate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户组管理</title>
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/publicform.js"></script>
    <script type="text/javascript" src="../js/listcheck.js"></script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script type="text/javascript" src="../js/Common.js"></script>
    <script language="javascript" type="text/javascript">
        function GetChildCate(id) {
            $.ajax({
                type: "post",
                contentType: "application/text",
                url: "hrjobcatelist.aspx?isajax=1&id="+id+"&cateType=<%=Request.QueryString["cateType"] %>&NodeCode=<%=NodeCode %>",
                dataType: "text",
                success: function (msg) {
                    $("#id"+id).after(msg);
                    var leng=((id.length/3)-1)*13;
                    var s="<a href='javascript:void(0)' onclick=\"CloseChildCate('"+id+"')\"><img src=\"../images/leftmenutitle.png\" border=0 style='margin-left:"+leng+"px'/> "+id;
                    $("#id"+id).find("li").eq(1).html(s);
                    ListCheck(id);
                }
        });
        }
        function CloseChildCate(id)
        {
            $(".ulbody").find("input[type='checkbox']").each(function(){
                var v=$(this).val();
                if(v.indexOf(id)==0 && v!=id)
                {
                    $(this).parent().parent().remove();
                }
            });
            var leng=((id.length/3)-1)*13;
            var s="<a href='javascript:void(0)' onclick=\"GetChildCate('"+id+"')\"><img src=\"../images/DTree/plus.gif\" border=0 style='margin-left:"+leng+"px'/> "+id;
            $("#id"+id).find("li").eq(1).html(s);
        }


        function ListCheck(id) {
            $(".ulbody").each(function (i) {
                var chkValue=$(this).children("li").children("#chkId").val();
                if(chkValue.indexOf(id)==0 && chkValue!=id)
                {
                    $(this).hover(
			            function () {
			                $(this).addClass("ullist")
			            },
			            function () {
			                $(this).removeClass("ullist")
			            }
		             );

                    $(this).click(function () {
                        var chkArray = $(this).children("li").children(":checkbox");
                        if (chkArray.length > 0) {
                            var b = chkArray[0].checked;
                            var src = arguments[0].target || window.event.srcElement;
                            if (b) {
                                if (src.type != 'checkbox' && src.tagName != "A") {                 //避免重复2次修改       

                                    $(this).removeClass("ulclick");
                                    chkArray[0].checked = false;
                                }

                            } else {

                                if (src.type != 'checkbox') {                      //避免重复2次修改                       
                                    $(this).addClass("ulclick");
                                    chkArray[0].checked = true;
                                }
                            }
                        }
                    })
                }
            })



            $("input:checkbox").each(function () {
                $(this).click(function () {
                    if (ischekBoxAll($(this).attr("name"))) {
                        if ($(this)[0].checked) {
                            $(".ulbody :checkbox").each(function (i) {
                                $(this)[0].checked = true;
                                $(this).parent().parent().addClass("ulclick");
                            });
                        } else {
                            $(".ulbody :checkbox").each(function (i) {
                                $(this)[0].checked = false;
                                $(this).parent().parent().removeClass("ulclick");
                            });
                        }

                    } else {

                        if ($(this).attr("type") == "checkbox") {
                            if ($(this)[0].checked) {
                                $(this).parent().parent().addClass("ulclick");
                            }
                            else {
                                $(this).parent().parent().removeClass("ulclick");
                            }
                        }
                    }

                })
            });

            function ischekBoxAll(str) {
                var len = str.length;
                if (str.substr(len - 3, len) == "All") {
                    return true;
                } else {
                    return false;
                }
            }
        }

        function OnEdit(id)
        {
            var urlAddress="HRJobCateEdit.aspx?ID="+id+"&action=Edit&NodeCode=<%=NodeCode %>&cateType=<%=Request.QueryString["cateType"] %>";
            openframe({ title: "编辑", url: urlAddress, width: 500, height: 500 });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="hidLogTitle" id="hidLogTitle" runat ="server" />
    <div class="container">
        <h4>
            位置： <%GetPageNav(NodeCode); %>
        </h4>
     <div id="searchContainer">
            <p>
            <span>名称：</span>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnQuery" runat="server" Text="搜索" OnClick="btnQuery_Click" />
            </p>
        </div>
        <div class="function">
            <asp:Button ID="btnHidAction" runat="server" OnClick="butdel_Click" style="display:none" />
            <asp:Button ID="btnNew" runat="server"  Text="<%$Resources:Common,Add %>" OnClick="Butnew_Click" />
            <asp:Button ID="btnCheck" runat="server" Text="审核通过" OnClick="btnCheck_Click" OnClientClick="return GetSelectTitle()"/>
            <asp:Button ID="btnCancelCheck" runat="server"  Text="审核不通过" onclick="btnCancelCheck_Click" OnClientClick="return GetSelectTitle()"/>
            
            <asp:Button ID="btnDelete" runat="server"  Text="<%$Resources:Common,Del %>"  OnClientClick="selfconfirm({msg:'确定要执行删除操作吗？此操作会将子类一起删除',fn:function(data){setAction(data)}});return false;"/>
            
        </div>
        <ul class="ulheader">
            <li style="width: 5%;text-align:center">
                <input type="checkbox" name="checkBoxAll" id="checkBoxAll" value="" /></li>
            <li style="width: 15%">
                    <div align="left">
                        编码</div>
                </li>
            <li style="width: 15%">
                    <div align="left">
                        名称</div>
                </li>
                <li style="width: 15%">
                    <div align="left">
                        父类</div>
                </li>
                <li style="width: 10%">
                    <div align="left">
                        审核状态</div>
                </li>
                <li style="width: 10%">
                    <div align="left">
                        添加日期</div>
                </li>
                 <li style="width: 20%">
                    操作
                </li>
            </ul>
            <asp:Repeater ID="rptInfoList" runat="server">
                <ItemTemplate>
                    <ul class="ulheader ulbody" id="id<%#Eval("ID")%>" >
                    <li style="width: 5%;text-align:center"> <input type="checkbox" value='<%#Eval("ID")%>' name="chkId" id="chkId" />
                      </li>
                      <li style="width: 15%">
                           <%if (!isAllShow)
                             {%>
                                <%# GetCateImg(Eval("ID").ToString()) %>
                             <%} %> <%#Eval("ID")%>
                      </li>
                        <li style="width: 15%">
                        <span id="Title<%#Eval("ID") %>" style="display:none"><%#Eval("Title") %></span>
                            <%#GetName(Eval("Title").ToString(), Eval("ID").ToString())%>
                        </li>
                        <li style="width: 15%">
                            <%#GetFatherName(Eval("ParentID").ToString())%>
                        </li>
                        <li style="width: 10%">
                            <%#GetCheckName(Eval("FlowState").ToString())%>
                        </li>
                        <li style="width: 10%">
                            <%#Eval("AddDate","{0:yyyy-MM-dd}")%>
                        </li>
                        <li style="width: 20%">
                          <%-- OnClientClick='<%#"OnEdit(\""+Eval("ID").ToString()+"\")" %>'--%>
                            <asp:LinkButton ID="lnkbEdit" class="abtn" runat="server" href='<%#"HrJobCateEdit.aspx?action=Edit&ID="+Eval("ID").ToString()+"&NodeCode="+NodeCode+"&cateType="+Request.QueryString["cateType"] %>'>修改</asp:LinkButton>
                            <asp:LinkButton ID="lnkbDelete" class="abtn" runat="server"
                            CommandName="deldp" ToolTip='<%#Eval("Title")%>' CommandArgument='<%#Eval("ID") %>'
                            OnClientClick='selectThisRow();selfconfirm({msg:"确定要执行删除操作吗？此操作会将子类一起删除",fn:function(data){setAction(data)}});return false;'>删除</asp:LinkButton>                            
                        </li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
    </div>
    </form>
</body>
</html>
