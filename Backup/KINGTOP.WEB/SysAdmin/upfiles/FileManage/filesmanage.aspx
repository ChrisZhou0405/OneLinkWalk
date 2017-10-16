<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="filesmanage.aspx.cs" Inherits="CMSFileManage.filesmanage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="author" content="TakWai" />
    <title>FileManager</title>
    <link rel="stylesheet" type="text/css" href="Style/css/Style.css" />
    <link rel="stylesheet" type="text/css" href="Style/css/Layout.css" />
    <script type="text/javascript" src="style/js/jquery-1.1.3.1.pack.js"></script>
    <script type="text/javascript" src="style/js/Manage.js"></script>
    
    <link rel="stylesheet" type="text/css" href="style/js/subModal/subModal.css" />
    <script type="text/javascript" src="style/js/subModal/common.js"></script>
    <script type="text/javascript" src="style/js/subModal/submodal.js"></script>   
 </head>
<body>
<div id="container">
<script language="javascript">
    //自适应容器高宽
    (function($) {
        var loadImg = function(url, fn) {
            var img = new Image();
            img.src = url;
            if (img.complete) {
                fn.call(img);
            } else {
                img.onload = function() {
                    fn.call(img);
                    img.onload = null;
                };
            };
        };

        $.fn.imgAutoSize = function(padding) {
            //var maxWidth = this.innerWidth() - (padding || 0);
            var maxWidth = 90;
            var maxHeight = 90;
            this.find('img').each(function(i, img) {
                loadImg(this.src, function() {
                    if (this.width > this.height) {
                        if (this.width > maxWidth) {
                            var height = maxWidth / this.width * this.height,
                            width = maxWidth;
                            img.width = width;
                            img.height = height;
                        };
                    }
                    else {
                        if (this.height > maxHeight) {
                            var width = maxHeight / this.height * this.width,
                            height = maxHeight;
                            img.width = width;
                            img.height = height;
                        };
                    };
                });
            });
        };
    })(jQuery);

    jQuery(function($) {
        // .imgWrap这个是图片外部容器，使用了本插件后超大的图片宽度将会限制在容器宽度  
        // 如果要控制图片与容器的边距，如20像素： $('.imgWrap').imgAutoSize(20);
        $('.divImg').imgAutoSize();
        //fnnn();
        setTimeout("fnnn();", 500);
    });

    function fnnn() {
          $("#intcL").find('.trheight').each(function() {
            var h = 0;
            var w = 0;
            $(this).find(".divImg").each(function(img) {
                var divH = $(this).children("div").height();
                $(this).find(".imgCon").each(function() {
                    var imgConH = this.height;
                    if ((imgConH + divH) > h) {
                        h = (imgConH + divH);
                    }
                    if (this.width > w) {
                        w = this.width;
                    }
                });
            });
            h = h + 20;
            if(h==20)
            {
            h=100;
//                return;
            }
            $(this).find(".divImg").each(function() {
                var imgH = 0;
                var divH = $(this).children("div").height();
                $(this).find(".imgCon").each(function() {
                    imgH = this.height + divH+20;
                    if (h > imgH && this.height>0) {
                        paddH = (h - imgH) / 2;
                        $(this).attr("style", "padding-top:" + paddH + "px;");
                    }
                });

                $(this).attr("style", "border:1px solid #CCC;padding:2px;text-align:center;height:" + h + "px;;line-height:20px;");
            });
        });
    }
    function On_Search() {
        var search=$("#keyword").val();
        if($.trim (search)=="")
        {
//            alert("请输入搜索关键词!");
//            document.getElementById ("keyword").focus ();
//            return;
        }
        var url = "filesmanage.aspx?act=search&path=<%= Server.UrlEncode(folderPath) %>&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword="+search;
        location.href=url;
    }
    
    function OnCKEditor(URL)
    {
        window.parent.CKEDITOR.tools.callFunction(<%=Request.QueryString["CKEditorFuncNum"]  %>,URL,'') ;
    }
    
    function OnDelete(Name,URL)
    {
        if(confirm('确认删除 '+Name+' ?'))
        {
            location.href=URL;
        }
    }
    </script> 

    <div>
        <div id="leftFrame">            
            <div class="box" style="padding:5px;margin:5px 0 5px 0; display: none;" id="tips">
                <span class="msg" id="tipsMsg"></span>
            </div>
            
            <%= builder %>
            
            <div class="box" style="display :none" id="divRename">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 60%;">
                        <form id="insertForm" method="post" action="Main.aspx?act=create&amp;path=<%= Server.UrlEncode(folderPath) %>">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="trtitle" style="width: 30px;">名称</td>
                                <td><input type="text" name="txtFolderName" id="txtFolderName" size="20" onfocus="this.className='colorfocus'" onblur="this.className='txt';" class="txt" /></td>
                                <td style="padding:2px"><input type="hidden" name="txtOldName" id="txtOldName" /><input type="submit" name="submit" id="submit" value="新建" class="btn" />&nbsp;&nbsp;<input type="button" name="cancel" id="cancel" value="取消" class="btn" onclick="javascript:toCreate('<%= Server.UrlEncode(folderPath) %>');" />&nbsp;&nbsp;<%--<input type="button" name="btncreate" value="新建文本文档" class="btn" onclick="showPopWin('Notepad.aspx?objfolder=<%= Server.UrlEncode(folderPath) %>', 700, 400, null);" />--%></td>
                            </tr>
                        </table>
                        </form> 
                    </td>
                    <td style="display:none;">
                        <form id="uploadForm" method="post" action="Main.aspx?act=upload&amp;path=<%= Server.UrlEncode(folderPath) %>" enctype="multipart/form-data">
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
                    <div style="float:left"><%= backHtml %> &nbsp;&nbsp;(目录:<%= folderNum %>&nbsp;文件:<%= fileNum %>)</div><div style="float:right ; width:145px;text-align:right;"><div style="float:left ;text-align:right ;"><input type="text" id="keyword" name="keyword" value="<%=Request.QueryString["keyword"] %>" style="border:1px solid #CCC;width:100px" maxlength ="20"></div><div style="padding-top:2px;"><input type="image" src="style/images/search.jpg" onclick="On_Search();return false;"  /></div></div>
                </div>
                <asp:Repeater ID="rptList" runat="server">
                    <HeaderTemplate>
                        <table border="0" cellpadding="5" cellspacing="0" id="intcL" class="m-table">
                        
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
                            <td style="margin:5px 5px 5px 5px;text-align:center;" valign="middle">
                            <div class="divImg" align="center" style="border:1px solid #CCC;padding:2px; float:left;" title="文件名：<%#Eval("Name") %><%# string.IsNullOrEmpty(Eval("ImgWidth").ToString())?"":"&#13;尺寸："+Eval("ImgWidth").ToString()+"px * "+Eval("ImgHeight").ToString()+"px" %>&#13;大小：<%# Eval("FormatSize") %> &#13;修改时间：<%# Eval("FormatModifyDate") %>" onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#E4F1F8';" onmouseout="this.style.backgroundColor=c;">
                                <%# GetFiles(Eval("FormatName").ToString(), Eval("Name").ToString(), folderPath, Eval("Ext").ToString(),Eval("Type").ToString ())%>
                                <div style="width:100%;padding-top:5px;text-align:center">
                                
                                <%#URLStr %><%#Eval("Name").ToString().Length > 15 ? Eval("Name").ToString().Insert(15, "<br>") : Eval("Name").ToString()%><%#string.IsNullOrEmpty(URLStr)?"":"</a>" %>
                                <%--<a href="#" onclick="javascript:toRename('<%# Eval("Name") %>', '<%# Eval("FullName") %>', '<%= Server.UrlEncode(folderPath) %>','&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword=<%=Request.QueryString["keyword"] %>');">
                                    <img src="style/Images/IcoAdd.gif" align="absmiddle" /> 重命名
                                </a>
                                <a href="filesmanage.aspx?act=delete&amp;path=<%= Server.UrlEncode(folderPath) %>&amp;file=<%# Eval("FullName") %>&amp;filetype=<%# Eval("Type") %>&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword=<%=Request.QueryString["keyword"]%>" onclick="javascript:return confirm('确认删除 <%# Eval("Name") %> ?');">
                                    <img src="style/Images/IcoDelete.gif" align="absmiddle" /> 删除
                                </a>--%>
                                </div>
                            </div>
                            <div align="center" style="padding-top:3px">
                                <input type="button" class="btn" value="重命名" title ="重命名" onclick="javascript:toRename('<%# Eval("Name") %>', '<%# Eval("FullName") %>', '<%= Server.UrlEncode(folderPath) %>','&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword=<%=Request.QueryString["keyword"] %>');" />
                                <input type="button" class ="btn" value="原图" onclick="window.open('/<%#FileUrl %>');" title="点击查看原图"<%#string.IsNullOrEmpty(FileUrl)?"Style='display:none'":"" %>/>
                                <input type="button" class="btn" value="删" title ="删除" onclick="javascript:OnDelete('<%# Eval("Name") %>','filesmanage.aspx?act=del&amp;path=<%= Server.UrlEncode(folderPath) %>&amp;file=<%# Eval("FullName") %>&amp;filetype=<%# Eval("Type") %>&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword=<%=Request.QueryString["keyword"]%>');" />
                            </div>
                                
                            </td>
                        
                    </ItemTemplate>
                    <FooterTemplate>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                
                
                
                <div class="page">
                <%=PageStr%>
                </div>
            </div>
        </div>     
    </div>
    <style>
#openlayer{position:absolute;display:none;z-index:99999;border:10px solid #f4f4f4}
</style>

    <div id="openlayer"></div>
<script>
//鼠标移到图片上显示大图，关闭该功能，原因是只能在弹出区域显示大图
//    var demo = document.getElementById("intcL");
//    var gg = demo.getElementsByTagName("img");
//    var ei = document.getElementById("openlayer");
//    for (i = 0; i < gg.length; i++) {
//        var ts = gg[i];
//        ts.onmousemove = function(event) {
//            event = event || window.event;
//            ei.style.display = "block";
//            ei.innerHTML = '<img src="' + this.src + '" />';
//            ei.style.top = document.body.scrollTop + event.clientY + 10 + "px";
//            ei.style.left = document.body.scrollLeft + event.clientX + 10 + "px";
//        }
//        ts.onmouseout = function() {
//            ei.innerHTML = "";
//            ei.style.display = "none";
//        }
//        ts.onclick = function() {
//            window.open(this.src);
//        }
//    }
</script>

</div>
</body>
</html>