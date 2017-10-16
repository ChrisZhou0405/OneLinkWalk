<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KingTop.WEB.SysAdmin.upfiles._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>   
    <link href="../../css/ustyle.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="javascript/jquery.uploadify-v2.1.4/uploadify.css" />
    <script type="text/javascript" language="javascript" src="javascript/jquery.uploadify-v2.1.4/jquery-1.4.2.min.js" charset="gb2312"></script>
    <script type="text/javascript" language="javascript" src="javascript/jquery.uploadify-v2.1.4/jquery.uploadify.v2.1.4.js" charset="gb2312"></script>
    <script type="text/javascript" language="javascript" src="javascript/jquery.uploadify-v2.1.4/swfobject.js" charset="gb2312"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $("#uploadify").uploadify({
                'uploader': 'javascript/jquery.uploadify-v2.1.4/uploadify.swf',
                'script': 'upload.ashx',
                'scriptData':{<%=urlParam %>},
                'cancelImg': 'javascript/jquery.uploadify-v2.1.4/cancel.png',
                'folder': 'UploadFile',
                'sizeLimit': <%=MaxSize %>,
                'queueSizeLimit': 10,
                'fileDesc': '<%=AllowExt %>',
                'fileExt': '<%=AllowExt %>',
                'method': 'post',
                'queueID': 'ShowImage',
                'buttonImg': 'javascript/jquery.uploadify-v2.1.4/btn.png',
                'wmode': 'transparent',
                'auto': false,
                'multi': <%=IsMulti %>,
                'onComplete': function(e, q, f, data, d) {
                    if(data=="Error")
                    {
                        $("#uploadify"+q).removeClass('completed');
                        $("#uploadify"+q).addClass('uploadifyError');
                        $("#uploadify"+q).find('.percentage').attr("style","display:none");
                        $("#uploadify"+q).find('.fileName').append(' -Upload Error');
                        $("#uploadify"+q).find('.uploadifyProgress').attr("style","display:none");
                        return;
                    }
                    InputUrl(data,f.size);
                },
                'onAllComplete':function(event, data){
                    setTimeout("parent.Closed()",500);
                }
            });
            
            $("#btnBegin").click(function() { 
                var v=SetOK();
                if(v!="false")
                {
                    $("#uploadify").uploadifySettings("scriptData", {<%=urlParam %>,'ImgSet':v});
                    $("#uploadify").uploadifyUpload(); 
                    return false; 
                }
            
            });
            <%if(!string.IsNullOrEmpty(Request.QueryString["path"])){ %>
            chageTag(2);
            <%} %>
        });
        
        function SetOK()
        {
            var iWidth=$("#txtWidth").val();
            var iHeight=$("#txtHeight").val();
            var ChkValue=$("#chkWater").is (":checked")?"1":"0";
            if($.trim(iWidth)!="")
            {
                for(var i=0;i<iWidth.length ;i++)
                {
                    var iOne=iWidth .substring(i,i+1);
                    if(("0123456789").indexOf (iOne )==-1)
                    {
                        alert("缩略图宽度必须填写数字！");
                        return "false";
                    }
                }
            }
            if($.trim(iHeight)!="")
            {
                for(var i=0;i<iHeight.length ;i++)
                {
                    var iOne=iHeight .substring(i,i+1);
                    if(("0123456789").indexOf (iOne )==-1)
                    {
                        alert("缩略图高度必须填写数字！");
                        return "false";
                    }
                }
            }
            var imgSet=iWidth+","+iHeight+","+ChkValue;
            return imgSet
        }
        
        function InputUrl(urls,fileSize) {
            if (urls == "" || urls == null)
		        return;

            var elementName="<%=Request.QueryString["ElementName"]%>";
            var contrType="<%=Request.QueryString["ControlType"] %>";
            var getSizeControl="<%=Request.QueryString["GetSizeControl"] %>";
            switch (contrType)
            {
                case "0":
                    var html="<option value='"+urls+"'>"+urls+"</option>";
                    $(window.parent.document).find("#"+elementName).append(html);
                    break;
                case "2":
                    AddItemToMultiFile(urls,true);
                    break;
                default:
                    $(window.parent.document).find("#"+elementName).val(urls);
                    if($(window.parent.document).find("#"+elementName+"_Del"))
                    {
                        $(window.parent.document).find("#"+elementName+"_Del").attr("disabled","false");
                    }
                    if($(window.parent.document).find("#"+elementName+"_Look"))
                    {
                        $(window.parent.document).find("#"+elementName+"_Look").attr("disabled","false");
                    }
                    if(getSizeControl!="" && getSizeControl != "undefined")
                    {
                        $(window.parent.document).find("#"+getSizeControl).val(fileSize);
                    }
                    if(contrType=="3")
                    {
                        eval("parent.albums"+elementName.replace("AlbumsNewURL","")).sync(elementName.replace("AlbumsNewURL",""));  //相册
                    }

            }
            if("<%=IsMulti %>"=="false")
            {
                setTimeout("parent.Closed()",100);
            }
		}
		


		function AddItemToMultiFile(data, isAdd) {
            if (data != '' && data != "null") {
                var multiFileID="<%=Request.QueryString["ElementName"]%>";
                var hdnID="<%=Request.QueryString["GetSizeControl"] %>";
                
                var obj = parent.document.getElementById(multiFileID);
                if (isAdd) {
                    obj.options[obj.options.length] = new Option(data, data);
                }
                else {
                    obj.options[obj.selectedIndex] = new Option(data, data);
                }
                MultiFileSynchronousHideValue(multiFileID, hdnID);
            }
        }
        
        function MultiFileSynchronousHideValue(multiFileID, hdnID) {
            var obj = parent.document.getElementById(hdnID);
            var photoUrls = parent.document.getElementById(multiFileID);
            var value = "";
            for (i = 0; i < photoUrls.length; i++) {
                if (value != "") {
                    value = value + "$$$";
                }
                value = value + photoUrls.options[i].value;
            }
            obj.value = value;
        }
        function chageTag(i)
        {
            if(i==1)
            {
                document.getElementById ("tagTd1").className="css1";
                document.getElementById ("tagTd2").className="css2";
                document.getElementById ("divLayer1").style.display ="";
                document.getElementById ("divLayer2").style.display ="none";
            }
            else
            {
                document.getElementById ("tagTd1").className="css3";
                document.getElementById ("tagTd2").className="css4";
                document.getElementById ("divLayer1").style.display ="none";
                document.getElementById ("divLayer2").style.display ="";
            }
        }
    </script>
    <style type="text/css">
        .css1{border-left:1px solid #CCCCCC;border-top:1px solid #CCCCCC;border-right:1px solid #CCCCCC;cursor:pointer;background-Color:#FFFFFF}
        .css2{border-bottom:1px solid #CCCCCC;border-top:1px solid #CCCCCC;border-right:1px solid #CCCCCC;cursor:pointer;background-Color:#F0F0F0}
        .css3{border:1px solid #CCCCCC;cursor:pointer;background-Color:#F0F0F0}
        .css4{border-top:1px solid #CCCCCC;border-right:1px solid #CCCCCC;cursor:pointer;background-Color:#FFFFFF}
    </style>
</head>
<body>    
<table border=0 width="100%" cellpadding=0 cellspacing=0>
    <tr height="30px">
    <td width="10px" style="border-bottom:1px solid #CCCCCC">&nbsp;</td>
    <td width="100px" class="css1" onclick="chageTag(1);" id="tagTd1" align="center">上传</td>
    <td width="100px" class="css2" id="tagTd2" onclick="chageTag(2);" bgcolor="#F0F0F0" align="center">从已上传中选择</td>
    <td style="border-bottom:1px solid #CCCCCC">&nbsp;</td>
    </tr>
    </table>
    <div id="divLayer1" style="padding-top:10px">
    <div style="text-align:center;width:100%;padding-bottom:5px">
    
    <div></div>
     <input type="hidden" id="InitParam" value="<%=urlParam %>"/>   
    
    <div id="ShowImage" style="text-align:left;padding-left:5px;padding-bottom:5px;width:420px;height:<%=divHeight%>;border:1px solid #CCCCCC;OVERFLOW-y: scroll;overflow-x:hidden;"></div> 
    
    
    <div style="padding-top:5px"></div>
    
    <div style="clear:both;font-size:1%;line-height:1%;height:0;"></div>   
    <input type="file" name="uploadify" id="uploadify" />&nbsp;&nbsp;
    <a id="btnBegin" href="javascript:return false"><img alt="开始上传" src="javascript/jquery.uploadify-v2.1.4/btnstart.png" width="120px" height="30px" style="border:0px"/></a> 
    
    </div>
    
    <div id="divID" runat="server" visible="false" style="float :left;width:290px;padding-top:8px; display:none;">
    是否添加水印 <input type="checkbox" id="chkWater" runat ="server" />
    <br />缩略图尺寸设置 宽：<input type="text" id="txtWidth" runat="server" style="width:30px" maxlength="3"/>px 高：<input type="text" id="txtHeight" runat="server"  style="width:30px" maxlength="3"/>px
    <span style='color:#4D4D4D'><br />1.如果缩略图宽和高为空或为零，则不生成缩略图
    <br />2.宽大于零，高为零或空，则按照宽生成等比例缩略图
    <br />3.高大于零，宽为零或空，则按照高生成等比例缩略图
    </span>
    </div>
    <div id="WaterDivId" visible="false" style="padding-top:8px;float:right;width:120px" runat="server">
    
    </div>
    <%if (UpType == "image")
      { %>
    <div style="clear:left">
    <div style="float:left ;">
    <%}
      else
      { %>
      <script language =javascript >
          $(function() {
              $("#divAllowType").attr("style", "padding-top:10px;padding-left:30px;");
          });
      </script>
    <%} %>
    <div id="divAllowType" runat="server" style="width:395px;OVERFLOW:hidden;"></div>
    </div>
    </div>
    </div>
    <div id="divLayer2" style="padding-top:10px;display:none">
        
                    <link rel="stylesheet" type="text/css" href="style.css" />

                    <script language="javascript">
                    
                    function toRename(p_name, p_fullName, p_path,p_param) {
                        document.getElementById ("divRename").style.display="block";
                        document.getElementById ("txtFolderName").value=p_name;
                        document.getElementById ("txtOldName").value=p_fullName;
                        
                        document.getElementById ("txtFolderName").focus();
                        document.getElementById ("submit").value="重命名";
                        document.getElementById ("insertForm").action= "default.aspx?act=rename&path=" + p_path + p_param+"&pfile=<%=Request.QueryString ["pfile"]%>&FormName=<%=Request.QueryString ["FormName"]%>&ElementName=<%=Request.QueryString ["ElementName"]%>&ControlType=<%=Request.QueryString ["ControlType"]%>&UpType=<%=Request .QueryString ["UpType"]%>&ExtType=<%=Request .QueryString ["ExtType"]%>&MaxSize=<%=Request .QueryString ["MaxSize"]%>&GetSizeControl=<%=Request .QueryString ["GetSizeControl"]%>&ThumbWidth=<%=Request .QueryString ["ThumbWidth"]%>&ThumbHeight=<%=Request .QueryString ["ThumbHeight"]%>&BestWidth=<%=Request.QueryString["BestWidth"]%>&BestHeight=<%=Request.QueryString["BestHeight"]%>&IsMulti=<%=Request.QueryString["IsMulti"]%>";
                        
                        document.getElementById ("tips").style.display="block";
                        document.getElementById ("tipsMsg").innerHTML="重命名时加入相对路径(如: \"..\\\", \"a\\\"), 即可实现移动文件, 目录";
                    }
                    // 还原为 模式
                    function toCreate(p_path)
                    {
                        document.getElementById ("txtFolderName").value="";
                        document.getElementById ("txtOldName").value="";
                        document.getElementById ("divRename").style.display="none";
                        document.getElementById ("tips").style.display="none";
                    }
                    
                    function On_Search() {
                        var search=document.getElementById ("keyword").value;
                        var url = "default.aspx?act=search&path=<%= Server.UrlEncode(folderPath) %>&pfile=<%=Request.QueryString ["pfile"]%>&FormName=<%=Request.QueryString ["FormName"]%>&ElementName=<%=Request.QueryString ["ElementName"]%>&ControlType=<%=Request.QueryString ["ControlType"]%>&UpType=<%=Request .QueryString ["UpType"]%>&ExtType=<%=Request .QueryString ["ExtType"]%>&MaxSize=<%=Request .QueryString ["MaxSize"]%>&GetSizeControl=<%=Request .QueryString ["GetSizeControl"]%>&ThumbWidth=<%=Request .QueryString ["ThumbWidth"]%>&ThumbHeight=<%=Request .QueryString ["ThumbHeight"]%>&BestWidth=<%=Request.QueryString["BestWidth"]%>&BestHeight=<%=Request.QueryString["BestHeight"]%>&IsMulti=<%=Request.QueryString["IsMulti"]%>&keyword="+search;
                        location.href=url;
                    }

                    
                    function OnDelete(Name,URL)
                    {
                        if(confirm('确认删除 '+Name+' ?'))
                        {
                            location.href=URL;
                        }
                    }
                    
                    </script> 
                    <div id="leftFrame">            
                        <div class="box" style="padding:5px;margin:5px 8px; display: none;" id="tips">
                            <span class="msg1" id="tipsMsg"></span>
                        </div>
                        
                        <%= builder %>
                        
                        <div class="box" style="display :none" id="divRename">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 60%;">
                                    <form id="insertForm" method="post" action="act=create&amp;path=<%= Server.UrlEncode(folderPath) %>&pfile=<%=Request.QueryString ["pfile"]%>&FormName=<%=Request.QueryString ["FormName"]%>&ElementName=<%=Request.QueryString ["ElementName"]%>&ControlType=<%=Request.QueryString ["ControlType"]%>&UpType=<%=Request .QueryString ["UpType"]%>&ExtType=<%=Request .QueryString ["ExtType"]%>&MaxSize=<%=Request .QueryString ["MaxSize"]%>&GetSizeControl=<%=Request .QueryString ["GetSizeControl"]%>&ThumbWidth=<%=Request .QueryString ["ThumbWidth"]%>&ThumbHeight=<%=Request .QueryString ["ThumbHeight"]%>&BestWidth=<%=Request.QueryString["BestWidth"]%>&BestHeight=<%=Request.QueryString["BestHeight"]%>&IsMulti=<%=Request.QueryString["IsMulti"]%>">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="trtitle" style="width: 30px;">名称</td>
                                            <td><input type="text" name="txtFolderName" id="txtFolderName" size="30" onfocus="this.className='colorfocus'" onblur="this.className='txt';" class="txt" /></td>
                                            <td style="padding:2px"><input type="hidden" name="txtOldName" id="txtOldName" /><input type="submit" name="submit" id="submit" value="新建" class="btn" />&nbsp;&nbsp;<input type="button" name="cancel" id="cancel" value="取消" class="btn" onclick="javascript:toCreate('<%= Server.UrlEncode(folderPath) %>');" />&nbsp;&nbsp;<%--<input type="button" name="btncreate" value="新建文本文档" class="btn" onclick="showPopWin('Notepad.aspx?objfolder=<%= Server.UrlEncode(folderPath) %>', 700, 400, null);" />--%></td>
                                        </tr>
                                    </table>
                                    </form> 
                                </td>
                                <td style="display:none;">
                                    <form id="uploadForm" method="post" action="default.aspx?act=upload&amp;path=<%= Server.UrlEncode(folderPath) %>&pfile=<%=Request.QueryString ["pfile"]%>&FormName=<%=Request.QueryString ["FormName"]%>&ElementName=<%=Request.QueryString ["ElementName"]%>&ControlType=<%=Request.QueryString ["ControlType"]%>&UpType=<%=Request .QueryString ["UpType"]%>&ExtType=<%=Request .QueryString ["ExtType"]%>&MaxSize=<%=Request .QueryString ["MaxSize"]%>&GetSizeControl=<%=Request .QueryString ["GetSizeControl"]%>&ThumbWidth=<%=Request .QueryString ["ThumbWidth"]%>&ThumbHeight=<%=Request .QueryString ["ThumbHeight"]%>&BestWidth=<%=Request.QueryString["BestWidth"]%>&BestHeight=<%=Request.QueryString["BestHeight"]%>&IsMulti=<%=Request.QueryString["IsMulti"]%>" enctype="multipart/form-data">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="trtitle" style="width: 60px;">上传文件</td>
                                                <td style="width: 230px;"><input type="file" name="fileUpload" id="fileUpload" size="23" class="txt" /></td>
                                                <td style="width: 36px"><input type="submit" name="upload" value="添加" class="btn" onclick="javascript:uploadMsg();" /></td>
                                            </tr>
                                    </table>        
                                    </form>             
                                </td>
                            </tr>
                            </table> 
                        </div>
                        <div id="tip" style="display:none">提示内容</div>
                        <div>
                            <div class="searchBox" style="height :20px">
                                <div style="float:left"><%= backHtml %> &nbsp;&nbsp;(目录:<%= folderNum %>&nbsp;文件:<%= fileNum %>)</div><div style="float:right ; width:150px;text-align:right;"><input type="text" id="keyword" name="keyword" value="<%=Request.QueryString["keyword"] %>" style="border:1px solid #CCC;width:100px" maxlength ="20"> <input type="button" class="btn" value="搜索" onclick="On_Search();return false;"  style="width:40px"/></div>
                            </div>
                            <asp:Repeater ID="rptList" runat="server">
                                <HeaderTemplate>
                                    <table border="0" cellpadding="5" cellspacing="1" bgcolor="#CCCCCC" id="intcL" class="m-table">
                                </HeaderTemplate>
                                <ItemTemplate>
                                <%
                                    if (iLoop % 3 == 0)
                                    {
                                        if(iLoop>0)
                                        {
                                            Response.Write ("</tr>");
                                        }
                                        if(iLoop%2==1)
                                        {
                                            %>
                                            <tr class="m-row2 trheight">
                                            <%}else{%>
                                            <tr class="m-row1 trheight">
                                        <%}
                                    }
                                            iLoop++;
                                     %>
                                        <td style="padding:8px;text-align:center;background-color:#FFFFFF" title="文件名：<%#Eval("Name") %><%# string.IsNullOrEmpty(Eval("ImgWidth").ToString())?"":"&#13;尺寸："+Eval("ImgWidth").ToString()+"px * "+Eval("ImgHeight").ToString()+"px" %>&#13;大小：<%# Eval("FormatSize") %> &#13;修改时间：<%# Eval("FormatModifyDate") %>" onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#E4F1F8';" onmouseout="this.style.backgroundColor=c;" valign="middle">
                                            <%# GetFiles(Eval("FormatName").ToString(), Eval("Name").ToString(), folderPath, Eval("Ext").ToString(), Eval("Type").ToString(), Eval("ImgWidth").ToString(), Eval("ImgHeight").ToString())%>
                                            <div style="width:100%;padding-top:5px;text-align:center">
                                            
                                            <%#URLStr %><%#Eval("Name").ToString().Length > 15 ? Eval("Name").ToString().Insert(15, "<br>") : Eval("Name").ToString()%><%#string.IsNullOrEmpty(URLStr)?"":"</a>" %>

                                            </div>
                                        <div align="center" style="padding-top:3px">
                                            <input type="button" class="btn" value="重命名" title ="重命名" style="width:40px" onclick="javascript:toRename('<%# Eval("Name") %>', '<%# Eval("FullName") %>', '<%= Server.UrlEncode(folderPath) %>','&type=<%=Request.QueryString["type"] %>&keyword=<%=Request.QueryString["keyword"] %>');" />
                                            <input type="button" class ="btn" value="原图" style="width:35px;<%#string.IsNullOrEmpty(FileUrl)?"display:none":"" %>" onclick="window.open('/<%#FileUrl %>');" title="点击查看原图"/>
                                            <input type="button" class="btn" value="删" style="width:20px" title ="删除" onclick="javascript:OnDelete('<%# Eval("Name") %>','default.aspx?act=del&amp;path=<%= Server.UrlEncode(folderPath) %>&amp;file=<%# Eval("FullName") %>&amp;filetype=<%# Eval("Type") %>&pfile=<%=Request.QueryString ["pfile"]%>&FormName=<%=Request.QueryString ["FormName"]%>&ElementName=<%=Request.QueryString ["ElementName"]%>&ControlType=<%=Request.QueryString ["ControlType"]%>&UpType=<%=Request .QueryString ["UpType"]%>&ExtType=<%=Request .QueryString ["ExtType"]%>&MaxSize=<%=Request .QueryString ["MaxSize"]%>&GetSizeControl=<%=Request .QueryString ["GetSizeControl"]%>&ThumbWidth=<%=Request .QueryString ["ThumbWidth"]%>&ThumbHeight=<%=Request .QueryString ["ThumbHeight"]%>&keyword=<%=Request.QueryString["keyword"]%>&BestWidth=<%=Request.QueryString["BestWidth"]%>&BestHeight=<%=Request.QueryString["BestHeight"]%>&IsMulti=<%=Request.QueryString["IsMulti"]%>');" />
                                        </div>
                                            
                                        </td>
                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            
                            
                            
                            <div class="page1">
                            <%=PageStr%>
                            </div>
                        </div>
                    </div> 
    </div>
</body>
</html>
