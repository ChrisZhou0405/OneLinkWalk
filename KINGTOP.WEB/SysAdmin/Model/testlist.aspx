<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageList.aspx.cs" EnableViewState="false" EnableEventValidation="false" Inherits="KingTop.Web.Admin.ModelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
        <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
        
        <script type="text/javascript" src="../js/publicform.js"></script>
        <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
        <link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../js/jquery.dialog.js"></script>
        <script type="text/javascript" src="../js/win.js"></script>
        <script src="../JS/jquery.ui.draggable.js" type="text/javascript"></script>
        <script src="../js/jquery-ui-1.8.14.custom.min.js" type="text/javascript"></script>
       <script language="javascript" src="../JS/ControlManageList.js" type="text/javascript"></script>
       
   
</head>
<body>
<div class="container"id="_ListInfoListTable">
  <h4>位置： <%GetPageNav(NodeCode);%></h4> <!--不能换行,否则程序会出错-->
       <form name="searchForm" method="post">
      <div id="searchContainer">
        <ul>
        
        <li><input type="submit" value="搜索" /></li>
       </ul><br clear="left" />
        </div>
    </form>
    <form id="theForm" runat="server">
    
    <div class="function" style="text-align:left" id="HQB_Model_ListLink"><a href="?Action=">显示全部</a><asp:Repeater runat="server" ID="rptFlowStep"><ItemTemplate><a href="?StepID=<%#Eval("StepID") %>" target="_self"><%#Eval("StepName") %></a></ItemTemplate></asp:Repeater></div>
    <div class="function" style=" text-align:center;"><asp:Repeater runat="server" ID="rptFlowState"><ItemTemplate><a href="?Action={s}{(FlowState)(=)(<%#Eval("StateValue") %>)}&StepID=<%#Eval("StepID") %>"><%#Eval("StateName") %></a></ItemTemplate></asp:Repeater></div>
    <div  class="function">
    <asp:PlaceHolder ID="plParseModelLinkButton" runat="server"><input type="button" onclick ="CloseOrOpenSortTable(this)" value="关闭拖动排序" /><input type="button" onclick='backOriginalUrl(<%=originalUrl %>)' value="返回" style=" display:<%=originalUrlDisplay%>;"/><%if(!string.Equals(ctrManageList.IsArchiving,"1") && !string.Equals(ctrManageList.IsDel,"1")){%><input type="button" value="添加" Href="testedit.aspx?action=add"  EditUrlParam="" id="btnNew" /><%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" value="删除" onclick="return confirmSetAction(this,'{e}{(IsDel)(1)}','确定删除选定记录至回收站？');"  runat="server" id="btnDelete" /><%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" value="生成HTML" onclick="return confirmSetAction(this,'{h}','确定将选定记录生成HTML？');"  runat="server" id="btnCreateHtml" /><%}%><%if(!string.Equals(ctrManageList.IsArchiving,"1") && !string.Equals(ctrManageList.IsDel,"1")){%><input type="button" onclick="batchSpecialSet();"  id="btnAddToSpecial" runat="server"  value="添加到专题" /><%}%><%if(ctrManageList.IsAllowFlow && !string.Equals(ctrManageList.IsArchiving,"1") && !string.Equals(ctrManageList.IsDel,"1")){%><input type="submit"  onclick="return setAction('HQB_PastFlowCheck');" value="通过审核"  runat="server"  id="btnCheck" /><%}%><%if(ctrManageList.IsAllowFlow && !string.Equals(ctrManageList.IsArchiving,"1") && !string.Equals(ctrManageList.IsDel,"1")){%><input type="submit"  onclick="return setAction('HQB_CancelFlowCheck');" value="取消审核"  runat="server"  id="btnCancelCheck" /><%}%><input type="submit" value="推荐" id="btnCommend" runat="server" onclick="return setAction('{e}{(IsCommend)(1)}');" /><input type="submit" value="取消推荐" id="btnCancelCommend" runat="server" onclick="return setAction('{e}{(IsCommend)(0)}');" /><input type="submit" value="置顶" onclick="return setAction('{e}{(IsTop)(1)}');" runat="server" id="btnSetTop" /><input type="submit" value="取消置顶" onclick="return setAction('{e}{(IsTop)(0)}');" runat="server" id="btnCancelSetTop" /></asp:PlaceHolder>
        <asp:PlaceHolder ID="plDParseModelLinkButton" runat="server"></asp:PlaceHolder>
    </div>
          <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
        <td  style="width:45px; text-align:center;"><input type="checkbox" name="SlectAll" id="SlectAll" /></td><td  style="width:80px;" ><a href="javascript:sort('K_U_test.Orders','3')">排序</a></td><td style="width:30%;" >操作</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptListInfo" runat="server">
        <ItemTemplate>
        <%#GetSortList(Eval("orders").ToString ()) %>
         <tr class="listInfotr"><span id='Title_<%#Eval("ID") %>' style='display:none'><%#Eval("Title") %></span><td style="width:45px; text-align:center;"><input type="checkbox" name="_chkID" value="<%#Eval("ID") %>" /></td><td  id="HQB_Orders_<%#Eval("ID") %>" style="width:80px;"><div style=" display:none;" ><img src="../image/loading.gif"/></div><span><input style="width:70px;" type="text" value="<%#Eval("Orders") %>" onblur="setOrders('K_U_test','<%#Eval("ID") %>',this.value)"/></span></td><td style="width:30%;" ><%if(!string.Equals(ctrManageList.IsDel,"1") &&  !string.Equals(ctrManageList.IsArchiving,"1")){%><input type="button"  BtnType="Edit"  Href='<%#"testedit.aspx?action=edit&ID=" + Eval("ID").ToString()%>' value="修改"/>&nbsp;<%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" BtnType="Delete" onclick="return rptConfirmSetAction(this,'<%#"{e}{(IsDel)(1)}{ID=" + Eval("ID").ToString() + "}"%>','确定删除当前记录？');" RecordID="<%#Eval("ID")%>"  value="删除" /><%}%>&nbsp;<%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" BtnType="CreateHtml" onclick="return rptConfirmSetAction(this,'{h}','确定当前记录生成HTML？');" RecordID="<%#Eval("ID")%>"  value="生成HTML" /><%}%>&nbsp;<input type="button" value="查看简历" onclick="setLocation('Recruitlist.aspx?JobID=<%#Eval("ID") %>&')" /><input type="button" value="类型管理" onclick="setLocation('PeriodicalCatalogList.aspx?PeriodicalID=<%#Eval("ID") %>&')" /><input type="button" value="文章列表" onclick="setLocation('PeriodicalArticleList.aspx?PeriodicalID=<%#Eval("ID") %>&')" /></td></tr>
        </ItemTemplate>
        </asp:Repeater>
        </tbody>
        </table>
        </div>
        <ul class="page">
            <webdiyer:AspNetPager ID="Split" runat="server" CssClass="page" PageSize="18" AlwaysShow="True"
                UrlPaging="true" ShowCustomInfoSection="left" CustomInfoSectionWidth="28%" ShowPageIndexBox="always"
                PageIndexBoxType="DropDownList" CustomInfoHTML="<%$Resources:Common,CustomInfoHTML %>"
                HorizontalAlign="Center" NumericButtonCount="6">
            </webdiyer:AspNetPager>
        </ul>

   <asp:HiddenField ID="hdnNotSearchField" runat="server" value="" /><asp:HiddenField ID="hdnBackDeliverUrlParam" runat="server" value="NodeCode"  /> <asp:HiddenField ID="hdnModelID" Value="100000001438546" runat="server" /><asp:HiddenField ID="hdnTableName" Value="K_U_test" runat="server" />  <asp:HiddenField ID="hdnDeliverAndSearchUrlParam" runat="server"  Value=""/> <input type="hidden" value="" id="HQB_Model_DeliverUrlParam" /><asp:HiddenField ID="hdnCustomCol" Value="" runat="server" />   <asp:HiddenField ID="hdnForignTableCol" value="" runat="server" /><asp:HiddenField ID="hdnShowCol" value="K_U_test.ID,K_U_test.FlowState,K_U_test.Orders" runat="server" />
      <asp:HiddenField ID="hdnNodeCode" runat="server" />
      <input type="hidden" name="hidLogTitle" id="hidLogTitle"/>
   <input type="hidden" name="action" id="HQB_Action" value="" />
   <asp:HiddenField ID="hdnIsAllowFlow" runat="server" />
   <input type="hidden" value="<%=backUrlParam %>" id="HQB_BackUrlParam"/>
   </form>
    </div>
      <div style="position:absolute; z-index:1000; border:#e4e4e4 1px solid; background:#f5f5f5;line-height:22px; display:none;" id="HQB_Replcae_Title_Display" onmouseout="javascript:$(this).css('display','none')">
          <table border="0" cellspacing="5" cellpadding="0"><tr><td></td></tr></table>
      </div>
             <script type="text/javascript">var SortList = "<%=sortList %>";<%=jsMessage %></script>
</body>
</html>
