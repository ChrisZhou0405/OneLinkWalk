<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManageList.aspx.cs" EnableViewState="false" EnableEventValidation="false" Inherits="KingTop.Web.Admin.ModelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
<div class="container"id="_ListInfoListTable">
  <h4>λ�ã� <%GetPageNav(NodeCode);%></h4> <!--���ܻ���,�����������-->
       <form name="searchForm" method="post">
      <div id="searchContainer">
        <ul>
        
        <li><input type="submit" value="����" /></li>
       </ul><br clear="left" />
        </div>
    </form>
    <form id="theForm" runat="server">
    
    <div class="function" style="text-align:left" id="HQB_Model_ListLink"><asp:Repeater runat="server" ID="rptFlowStep"><ItemTemplate><a href="?StepID=<%#Eval("StepID") %>" target="_self"><%#Eval("StepName") %></a></ItemTemplate></asp:Repeater></div>
    <div class="function" style=" text-align:center;"><asp:Repeater runat="server" ID="rptFlowState"><ItemTemplate><a href="?Action={s}{(FlowState)(=)(<%#Eval("StateValue") %>)}&StepID=<%#Eval("StepID") %>"><%#Eval("StateName") %></a></ItemTemplate></asp:Repeater></div>
    <div  class="function">
    <asp:PlaceHolder ID="plParseModelLinkButton" runat="server"><%--<input type="button" onclick ="CloseOrOpenSortTable(this)" value="�ر��϶�����" />--%><input type="button" onclick='backOriginalUrl(<%=originalUrl %>)' value="����" style=" display:<%=originalUrlDisplay%>;"/><%if(!string.Equals(ctrManageList.IsArchiving,"1") && !string.Equals(ctrManageList.IsDel,"1")){%><input type="button" value="����" Href="friendlinkedit.aspx?action=add"  EditUrlParam="" id="btnNew" /><%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" value="ɾ��" onclick="return confirmSetAction(this,'{e}{(IsDel)(1)}','ȷ��ɾ��ѡ����¼������վ��');"  runat="server" id="btnDelete" /><%}%></asp:PlaceHolder>
        <asp:PlaceHolder ID="plDParseModelLinkButton" runat="server"></asp:PlaceHolder>
    </div>
          <div id="HQB_ListInfo" style=" padding:0; margin:0;">
        <table class="listInfo" bordercolor="#dbe5e7" border="1">
        <tr bgcolor="#e6f1fe" height="30px">
        <td  style="width:45px; text-align:center;"><input type="checkbox" name="SlectAll" id="SlectAll" /></td><td  style="width:180px;text-align:left;">��˾����</td><td  style="width:150px;text-align:left;">logo</td><td  style="width:80px;text-align:left;">���״̬</td><td  style="width:80px;text-align:left;">��������</td><td  style="width:80px;" align=center><a href="javascript:sort('K_U_friendlink.Orders','3')">����</a></td><td style="width:80px;" >����</td>
        </tr>
        <tbody>
        <asp:Repeater ID="rptListInfo" runat="server">
        <ItemTemplate>
        <%#GetSortList(Eval("orders").ToString ()) %>
         <tr class="listInfotr"><span id='Title_<%#Eval("ID") %>' style='display:none'><%#Eval("Title") %></span><td style="width:45px; text-align:center;"><input type="checkbox" name="_chkID" value="<%#Eval("ID") %>" /></td><td isClip="1"  style="text-align:left;width:180px;"><%#Eval("Title") %></td><td  style="text-align:left;width:150px;"><img src='<%= GetUploadImgUrl()%><%#Eval("BigImg") %>.gif' onerror="this.src='/sysadmin/images/NoPic.jpg'" height=60/></td><td  style="text-align:left;width:80px;"><%#Eval("FlowState") %></td><td  style="text-align:left;width:80px;"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}",Eval("AddDate").ToString()) %></td><td  id="HQB_Orders_<%#Eval("ID") %>" class="dragOrders" style="width:80px;" align=center>
                    <div style="width:110px"><div style="float:left;border-right:1px solid #CCCCCC;height:22px;" title="�϶�����"><img src="../images/move.png" style="padding:0 8px;cursor: pointer;"/></div>
                        <div style="float:left;padding-left:8px"><div style=" display:none;" ><img src="../images/loading.gif"/></div><span><input style="width:50px;text-align:center;height:14px" type="text" value="<%#Eval("Orders") %>" onblur="setOrders('K_U_friendlink','<%#Eval("ID") %>',this.value)"/></span></div></div></td><td style="width:80px;;" ><%if(!string.Equals(ctrManageList.IsDel,"1") &&  !string.Equals(ctrManageList.IsArchiving,"1")){%><input type="button"  BtnType="Edit"  Href='<%#"friendlinkedit.aspx?action=edit&ID=" + Eval("ID").ToString()%>' value="�޸�" class="btn" style="height:22px;cursor: pointer;"/>&nbsp;<%}%><%if(!string.Equals(ctrManageList.IsDel,"1")){%><input type="submit" BtnType="Delete" onclick="return rptConfirmSetAction(this,'<%#"{e}{(IsDel)(1)}{ID=" + Eval("ID").ToString() + "}"%>','ȷ��ɾ����ǰ��¼��');" RecordID="<%#Eval("ID")%>"  value="ɾ��" class="btn"  style="height:22px;cursor: pointer;"/><%}%>&nbsp;</td></tr>
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

   <asp:HiddenField ID="hdnNotSearchField" runat="server" value="" /><asp:HiddenField ID="hdnBackDeliverUrlParam" runat="server" value="NodeCode"  /> <asp:HiddenField ID="hdnModelID" Value="100000000172313" runat="server" /><asp:HiddenField ID="hdnTableName" Value="K_U_friendlink" runat="server" />  <asp:HiddenField ID="hdnDeliverAndSearchUrlParam" runat="server"  Value=""/> <input type="hidden" value="" id="HQB_Model_DeliverUrlParam" /><asp:HiddenField ID="hdnCustomCol" Value="" runat="server" />   <asp:HiddenField ID="hdnForignTableCol" value="[K_ReviewFlowState]|K_U_friendlink.FlowState|Name|StateValue" runat="server" /><asp:HiddenField ID="hdnShowCol" value="K_U_friendlink.ID,K_U_friendlink.FlowState,K_U_friendlink.Title,K_U_friendlink.BigImg,K_U_friendlink.AddDate,K_U_friendlink.Orders" runat="server" />
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