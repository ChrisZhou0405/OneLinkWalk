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
    
    <div class="function" style="text-align:left" id="HQB_Model_ListLink"><asp:Repeater runat="server" ID="rptFlowStep"><ItemTemplate><a href="?StepID=<%#Eval("StepID") %>" target="_self"><%#Eval("StepName") %></a></ItemTemplate></asp:Repeater></div>
    <div class="function" style=" text-align:center;"><asp:Repeater runat="server" ID="rptFlowState"><ItemTemplate><a href="?Action={s}{(FlowState)(=)(<%#Eval("StateValue") %>)}&StepID=<%#Eval("StepID") %>"><%#Eval("StateName") %></a></ItemTemplate></asp:Repeater></div>
    <div  class="function">
    <asp:PlaceHolder ID="plParseModelLinkButton" runat="server"><input type="button" onclick ="CloseOrOpenSortTable(this)" value="关闭拖动排序" /><input type="button" onclick='backOriginalUrl(<%=originalUrl %>)' value="返回" style=" display:<%=originalUrlDisplay%>;"/><%if(!string.Equals(ctrManageList.IsArchiving,"1") && !string.Equals(ctrManageList.IsDel,"1")){%><input type="button" value="添加" Href="reportDownLoadedit.aspx?action=add"  EditUrlParam="" id="btnNew" /><%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" value="删除" onclick="return confirmSetAction(this,'{e}{(IsDel)(1)}','确定删除选定记录至回收站？');"  runat="server" id="btnDelete" /><%}%></asp:PlaceHolder>
        <asp:PlaceHolder ID="plDParseModelLinkButton" runat="server"></asp:PlaceHolder>
    </div>
          <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
        <td  style="width:45px; text-align:center;"><input type="checkbox" name="SlectAll" id="SlectAll" /></td><td  style="width:300px;">标题</td><td  style="width:100px;">添加日期</td><td  style="width:100px;">审核状态</td><td  style="width:80px;" ><a href="javascript:sort('K_U_reportDownLoad.Orders','3')">排序</a></td><td style="width:80px;" >操作</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptListInfo" runat="server">
        <ItemTemplate>
        <%#GetSortList(Eval("orders").ToString ()) %>
         <tr class="listInfotr"><span id='Title_<%#Eval("ID") %>' style='display:none'><%#Eval("Title") %></span><td style="width:45px; text-align:center;"><input type="checkbox" name="_chkID" value="<%#Eval("ID") %>" /></td><td isClip="1"  style="text-align:left;width:300px;"><%#Eval("Title") %></td><td  style="text-align:left;width:100px;"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}",Eval("AddDate").ToString()) %></td><td  style="text-align:left;width:100px;"><%#Eval("FlowState") %></td><td  id="HQB_Orders_<%#Eval("ID") %>" style="width:80px;"><div style=" display:none;" ><img src="../image/loading.gif"/></div><span><input style="width:70px;" type="text" value="<%#Eval("Orders") %>" onblur="setOrders('K_U_reportDownLoad','<%#Eval("ID") %>',this.value)"  CssClass="validate[required,custom[onlyNumber]]"/></span></td><td style="width:80px;" ><%if(!string.Equals(ctrManageList.IsDel,"1") &&  !string.Equals(ctrManageList.IsArchiving,"1")){%><input type="button"  BtnType="Edit"  Href='<%#"reportDownLoadedit.aspx?action=edit&ID=" + Eval("ID").ToString()%>' value="修改"/>&nbsp;<%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" BtnType="Delete" onclick="return rptConfirmSetAction(this,'<%#"{e}{(IsDel)(1)}{ID=" + Eval("ID").ToString() + "}"%>','确定删除当前记录？');" RecordID="<%#Eval("ID")%>"  value="删除" /><%}%>&nbsp;</td></tr>
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

   <asp:HiddenField ID="hdnNotSearchField" runat="server" value="" /><asp:HiddenField ID="hdnBackDeliverUrlParam" runat="server" value="NodeCode"  /> <asp:HiddenField ID="hdnModelID" Value="100000000172295" runat="server" /><asp:HiddenField ID="hdnTableName" Value="K_U_reportDownLoad" runat="server" />  <asp:HiddenField ID="hdnDeliverAndSearchUrlParam" runat="server"  Value=""/> <input type="hidden" value="" id="HQB_Model_DeliverUrlParam" /><asp:HiddenField ID="hdnCustomCol" Value="" runat="server" />   <asp:HiddenField ID="hdnForignTableCol" value="[K_ReviewFlowState]|K_U_reportDownLoad.FlowState|Name|StateValue" runat="server" /><asp:HiddenField ID="hdnShowCol" value="K_U_reportDownLoad.ID,K_U_reportDownLoad.FlowState,K_U_reportDownLoad.Title,K_U_reportDownLoad.AddDate,K_U_reportDownLoad.Orders" runat="server" />
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
