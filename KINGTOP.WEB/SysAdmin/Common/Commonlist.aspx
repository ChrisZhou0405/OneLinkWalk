<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Commonlist.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Common.Commonlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>

    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.14.custom.min.js" type="text/javascript"></script>
    <script language="javascript" src="../JS/ControlManageList.js" type="text/javascript"></script>
</head>
<body>
    <form id="theForm" runat="server">
    <div class="container" id="_ListInfoListTable">
        <h4>位置： <%= KingTop.Web.Admin.AdminPage.GetPageNavStr(NodeCode)%></h4>
        <!--不能换行,否则程序会出错-->
       
            <div id="searchContainer">
                <ul>
                    <li>标题</li>
                    <li>
                        <input type="text" value="" style="width: 150px;" maxlength="300" name="Title" id="Title" runat="server"/></li>
                    <li>
                       
                <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
                    </li>
                </ul>
                <br clear="left" />
            </div>
        


        

            <div class="function" style="text-align: left" id="HQB_Model_ListLink">
                <asp:Repeater runat="server" ID="rptFlowStep">
                    <ItemTemplate><a href="?StepID=<%#Eval("StepID") %>" target="_self"><%#Eval("StepName") %></a></ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="function" style="text-align: center;">
                <asp:Repeater runat="server" ID="rptFlowState">
                    <ItemTemplate><a href="?Action={s}{(FlowState)(=)(<%#Eval("StateValue") %>)}&StepID=<%#Eval("StepID") %>"><%#Eval("StateName") %></a></ItemTemplate>
                </asp:Repeater>
            </div>


            <div class="function">
                <asp:PlaceHolder ID="plParseModelLinkButton" runat="server"><%--<input type="button" onclick ="CloseOrOpenSortTable(this)" value="关闭拖动排序" />--%>


                    <%if (IsHaveRightByOperCode("New"))
                      {%>
                    <input type="button" value="添加" href="Commonedit.aspx?action=new&NodeCode=<%=NodeCode %>" editurlparam="" id="btnNew" />
                    <%}%>


                    <%if (IsHaveRightByOperCode("Delete"))
                      {%>
                    <asp:Button ID="btndel" runat="server" Text="删除" OnClientClick="return confirm('确定删除?');" OnClick="btndel_Click" />

                    <%}%>

                    <%if (IsHaveRightByOperCode("Check"))
                      {%>
                    <asp:Button ID="btnCheck" runat="server" Text="通过审核" OnClick="btnCheck_Click" />

                    <%}%>

                    <%if (IsHaveRightByOperCode("CancelCheck"))
                      {%>
                    <asp:Button ID="btnCancelCheck" runat="server" Text="取消审核" OnClick="btnCancelCheck_Click" />

                    <%}%>


                </asp:PlaceHolder>

                <asp:PlaceHolder ID="plDParseModelLinkButton" runat="server"></asp:PlaceHolder>
            </div>


            <div id="HQB_ListInfo" style="padding: 0; margin: 0;">
                <table class="listInfo" bordercolor="#dbe5e7" border="1">
                    <tr bgcolor="#e6f1fe" height="30px">
                        <td style="width: 45px; text-align: center;">
                            <input type="checkbox" name="SlectAll" id="SlectAll" /></td>
                        <td style="width: 200px;" align="center"><a href="javascript:sort('K_U_Common.Title','3')">标题</a></td>
                        <td style="width: 150px; text-align: left;">用户名</td>
                        <td style="width: 100px; text-align: left;">大图</td>
                        <td style="width: 120px; text-align: left;">添加日期</td>
                        <td style="width: 80px; text-align: left;">审核状态</td>
                        <td style="width: 80px;" align="center"><a href="javascript:sort('K_U_Common.Orders','3')">排序</a></td>
                        <td style="width: 20%;">操作</td>
                    </tr>
                    <asp:Repeater ID="rptListInfo" runat="server" OnItemCommand="rptListInfo_OnItemCommand">
                        <ItemTemplate>
                            <%#GetSortList(Eval("orders").ToString ()) %>
                            <tr class="listInfotr">
                                <span id='Title_<%#Eval("ID") %>' style='display: none'><%#Eval("Title") %></span>
                                <td style="width: 45px; text-align: center;">
                                    <asp:HiddenField ID="hidId" runat="server" Value='<%#Eval("ID") %>' />
                                    <%--<asp:CheckBox ID="_chkID" name="_chkID" runat="server" />--%>
                                    <input type="checkbox" id="_chkID" name="_chkID" value='<%# Eval("ID") %>' />

                                </td>
                                <td isclip="1" style="text-align: left; width: 200px;"><%#Eval("Title") %></td>
                                <td isclip="1" style="text-align: left; width: 150px;"><%#Eval("UserName") %></td>
                                <td style="text-align: left; width: 100px;">
                                    <img src='<%= GetUploadImgUrl()%><%#Eval("BigImg") %>.gif' onerror="this.src='/sysadmin/images/NoPic.jpg'" height="60" /></td>
                                <td style="text-align: left; width: 120px;"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}",Eval("AddDate").ToString()) %></td>
                                <td style="text-align: left; width: 80px;"><%#ctrManageList.ParseFieldValueToText("100011424314585",Eval("FlowState") )%></td>
                                <td id='HQB_Orders_<%#Eval("ID") %>' class="dragOrders" style="width: 80px;" align="center">
                                    <div style="width: 110px">
                                        <div style="float: left; border-right: 1px solid #CCCCCC; height: 22px;" title="拖动排序">
                                            <img src="../images/move.png" style="padding: 0 8px; cursor: pointer;" />
                                        </div>
                                        <div style="float: left; padding-left: 8px">
                                            <div style="display: none;">
                                                <img src="../images/loading.gif" />
                                            </div>
                                            <span>
                                                <input style="width: 50px; text-align: center; height: 14px" type="text" value="<%#Eval("Orders") %>" onblur="setOrders('K_U_Common','<%#Eval("ID") %>',this.value)" /></span>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 20%;"><%if (!string.Equals(ctrManageList.IsDel, "1") && !string.Equals(ctrManageList.IsArchiving, "1"))
                                                          {%>
                                    模型:<input type="button" btntype="Edit" href='<%#"Commonedit.aspx?action=edit&ID=" + Eval("ID").ToString()+"&NodeCode="+Eval("NodeCode").ToString()  %>' value="修改" class="btn" style="height: 22px; cursor: pointer;" />&nbsp;
                                    模块:<input type="button" btntype="Edit" href='<%#"Commonupdate.aspx?action=edit&ID=" + Eval("ID").ToString()+"&NodeCode="+Eval("NodeCode").ToString()  %>' value="修改" class="btn" style="height: 22px; cursor: pointer;" />&nbsp;
                                    
                                    <%}%>
                                    <%if (!string.Equals(ctrManageList.IsDel, "1"))
                                      {%>
                                    <asp:Button ID="Delete" runat="server" Text="删除" CssClass="btn" Style="height: 22px;
                                    cursor: pointer;" OnClientClick="return confirm('确定删除?');" CommandName="del" />

                                    

               <%--<input type="submit" btntype="Delete" onclick="return rptConfirmSetAction(this,'<%#"{e}{(IsDel)(1)}{ID=" + Eval("ID").ToString() + "}"%>    ','确定删除当前记录？');" recordid="<%#Eval("ID")%>" value="删除" class="btn" style="height: 22px; cursor: pointer;" />--%>
                                    
                                    <%}%>&nbsp;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <ul class="page">
                <webdiyer:AspNetPager ID="Split" runat="server" CssClass="page" PageSize="18" AlwaysShow="True"
                    UrlPaging="true" ShowCustomInfoSection="left" CustomInfoSectionWidth="28%" ShowPageIndexBox="always"
                    PageIndexBoxType="DropDownList" CustomInfoHTML="<%$Resources:Common,CustomInfoHTML %>" OnPageChanged="AspNetPager1_PageChanged"
                    HorizontalAlign="Center" NumericButtonCount="6">
                </webdiyer:AspNetPager>
            </ul>

            <asp:HiddenField ID="hdnNotSearchField" runat="server" Value="" />
            <asp:HiddenField ID="hdnBackDeliverUrlParam" runat="server" Value="NodeCode" />
            <asp:HiddenField ID="hdnModelID" Value="100000003414185" runat="server" />
            <asp:HiddenField ID="hdnTableName" Value="K_U_Common" runat="server" />
            <asp:HiddenField ID="hdnDeliverAndSearchUrlParam" runat="server" Value="" />
            <input type="hidden" value="" id="HQB_Model_DeliverUrlParam" /><asp:HiddenField ID="hdnCustomCol" Value="" runat="server" />
            <asp:HiddenField ID="hdnForignTableCol" Value="" runat="server" />
            <asp:HiddenField ID="hdnShowCol" Value="K_U_Common.ID,K_U_Common.FlowState,K_U_Common.Title,K_U_Common.UserName,K_U_Common.BigImg,K_U_Common.AddDate,K_U_Common.Orders" runat="server" />
            <asp:HiddenField ID="hdnNodeCode" runat="server" />
            <input type="hidden" name="hidLogTitle" id="hidLogTitle" />
            <input type="hidden" name="action" id="HQB_Action" value="" />
            <asp:HiddenField ID="hdnIsAllowFlow" runat="server" />
            <input type="hidden" value="<%=backUrlParam %>" id="HQB_BackUrlParam" />

        


    </div>
    <div style="position: absolute; z-index: 1000; border: #e4e4e4 1px solid; background: #f5f5f5; line-height: 22px; display: none;" id="HQB_Replcae_Title_Display" onmouseout="javascript:$(this).css('display','none')">
        <table border="0" cellspacing="5" cellpadding="0">
            <tr>
                <td></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">var SortList = "<%=sortList %>";<%=jsMessage %></script>
    </form>
</body>
</html>
