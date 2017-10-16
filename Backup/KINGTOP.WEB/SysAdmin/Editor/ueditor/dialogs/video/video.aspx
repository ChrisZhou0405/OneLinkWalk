<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="video.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Editor.ueditor.dialogs.video.video" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="video.css" />
    
</head>
<body>
<div class="wrapper">
    <div id="videoTab">
        <div id="tabHeads">
            <span tabSrc="videoLocal" class="focus"><var id="lang_tab_localV"></var></span>
            <span tabSrc="videoSelect"><var id="lang_tab_selectV"></var></span>
            <span tabSrc="video"><var id="lang_tab_insertV"></var></span>
            <span tabSrc="videoSearch"><var id="lang_tab_searchV"></var></span>
        </div>
        <div id="tabBodys">
            
            <div id="videoLocal" class="panel">
                
                
                    <div class="controller">
                        <!--<span id="divStatus"></span>-->
                        <span id="spanButtonPlaceHolder"></span>
                    </div>
                    <div class="fieldset flash" id="fsUploadProgress"></div>
                    <div style="width:420px;float:left;padding-left:10px;line-height:18px;color:#0066CC">1.允许上传的文件类型有：<%=AllowExt %><br />2.最大可以上传<%=MaxSize %> MB；如果网速不好，文件太大将会上传失败。可以用FTP将文件上传到服务器uploadfiles/medias/文件夹中，然后切换到“插入视频”在视频网址中输入视频地址："/uploadfiles/medias/****.swf"<br />
3.建议将视频转换为*.flv;*.swf格式，这样可以控制视频大小，又不会存在浏览器不兼容导致视频不能播放问题。<a href="../../../chs_bsvcsetup.rar" style="color:#0066CC" target="_blank">下载视频转换器</a>
</div>
                    
                    <span id="startUpload" style="display: none;"></span>

                
            </div>
            <div id="videoSelect" class="panel" style="display: none">
                    
                    <link rel="stylesheet" type="text/css" href="Style.css" />

                    <script language="javascript">
                    function toRename(p_name, p_fullName, p_path,p_param) {
                        document.getElementById ("divRename").style.display="block";
                        document.getElementById ("txtFolderName").value=p_name;
                        document.getElementById ("txtOldName").value=p_fullName;
                        
                        document.getElementById ("txtFolderName").focus();
                        document.getElementById ("submit").value="重命名";
                        document.getElementById ("insertForm").action= "video.aspx?act=rename&path=" + p_path + p_param;
                        
                        document.getElementById ("tips").style.display="block";
                        document.getElementById ("tipsMsg").innerHTML="对文件, 目录重命名时, 可在前面加入相对路径(如: \"..\\\", \"a\\\"), 即可实现移动文件, 目录功能.";
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
                        var search=document.getElementById ("#keyword").value;
                        var url = "video.aspx?act=search&path=<%= Server.UrlEncode(folderPath) %>&type=<%=Request.QueryString["type"] %>&keyword="+search;
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
                            <span class="msg" id="tipsMsg"></span>
                        </div>
                        
                        <%= builder %>
                        
                        <div class="box" style="display :none" id="divRename">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 60%;">
                                    <form id="insertForm" method="post" action="act=create&amp;path=<%= Server.UrlEncode(folderPath) %>">
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
                                <div style="float:left"><%= backHtml %> &nbsp;&nbsp;(目录:<%= folderNum %>&nbsp;文件:<%= fileNum %>)</div><div style="float:right ; width:150px;text-align:right;"><input type="text" id="keyword" name="keyword" value="<%=Request.QueryString["keyword"] %>" style="border:1px solid #CCC;width:100px" maxlength ="20"> <input type="button" class="btn" value="搜索" onclick="On_Search();return false;"  style="width:40px"/></div>
                            </div>
                            <asp:Repeater ID="rptList" runat="server">
                                <HeaderTemplate>
                                    <table border="0" cellpadding="5" cellspacing="1" bgcolor="#CCCCCC" id="intcL" class="m-table">
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                <%
                                    if (iLoop % 4 == 0)
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
                                            <%# GetFiles(Eval("FormatName").ToString(), Eval("Name").ToString(), folderPath, Eval("Ext").ToString(),Eval("Type").ToString ())%>
                                            <div style="width:100%;padding-top:5px;text-align:center">
                                            
                                            <%#URLStr %><%#Eval("Name").ToString().Length > 18 ? Eval("Name").ToString().Insert(15, "<br>") : Eval("Name").ToString()%><%#string.IsNullOrEmpty(URLStr)?"":"</a>" %>

                                            </div>
                                        <div align="center" style="padding-top:3px">
                                            <input type="button" class="btn" value="重命名" title ="重命名" onclick="javascript:toRename('<%# Eval("Name") %>', '<%# Eval("FullName") %>', '<%= Server.UrlEncode(folderPath) %>','&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword=<%=Request.QueryString["keyword"] %>');" />
                                            <input type="button" class ="btn" value="原图" onclick="window.open('/<%#FileUrl %>');" title="点击查看原图"<%#string.IsNullOrEmpty(FileUrl)?"Style='display:none'":"" %>/>
                                            <input type="button" class="btn" value="删除" title ="删除" onclick="javascript:OnDelete('<%# Eval("Name") %>','video.aspx?act=delete&amp;path=<%= Server.UrlEncode(folderPath) %>&amp;file=<%# Eval("FullName") %>&amp;filetype=<%# Eval("Type") %>&type=<%=Request.QueryString["type"] %>&CKEditor=<%=Request.QueryString["CKEditor"] %>&CKEditorFuncNum=<%=Request.QueryString["CKEditorFuncNum"] %>&keyword=<%=Request.QueryString["keyword"]%>');" />
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
            
            <div id="video" class="panel" style="display: none">
               <table><tr><td><label for="videoUrl" class="url"><var id="lang_video_url"></var></label></td><td><input id="videoUrl" type="text"></td></tr></table>
               <div id="preview"></div>
               <div id="videoInfo">
                   <fieldset>
                       <legend><var id="lang_video_size"></var></legend>
                       <table>
                           <tr><td><label for="videoWidth"><var id="lang_videoW"></var></label></td><td><input class="txt" id="videoWidth" type="text"/></td></tr>
                           <tr><td><label for="videoHeight"><var id="lang_videoH"></var></label></td><td><input class="txt" id="videoHeight" type="text"/></td></tr>
                       </table>
                   </fieldset>
                   <fieldset>
                      <legend><var id="lang_alignment"></var></legend>
                      <div id="videoFloat"></div>
                  </fieldset>
               </div>
            </div>
            <div id="videoSearch" class="panel" style="display: none">
                <table style="margin-top: 5px;">
                    <tr>
                        <td><input id="videoSearchTxt"  type="text" /></td>
                        <td>
                            <select id="videoType">
                                <option value="0"></option>
                                <option value="29"></option>
                                <option value="1"></option>
                                <option value="5"></option>
                                <option value="15"></option>
                                <option value="21"></option>
                                <option value="31"></option>
                            </select>
                        </td>
                        <td><input id="videoSearchBtn" type="button"/></td>
                        <td><input id="videoSearchReset" type="button" /></td>
                    </tr>
                </table>
                <div id="searchList"></div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript" src="../internal.js"></script>
<script type="text/javascript" src="video.js"></script>

<script type="text/javascript" src="../../third-party/swfupload/swfupload.js"></script>
<script type="text/javascript" src="../../third-party/swfupload/swfupload.queue.js"></script>
<script type="text/javascript" src="../../third-party/swfupload/fileprogress.js"></script>
<script type="text/javascript" src="../attachment/callbacks.js"></script>
<script type="text/javascript" src="../attachment/fileTypeMaps.js"></script>

<script type="text/javascript">
    var swfupload,
        filesList = [];
    editor.setOpt({
        fileFieldName: "upfile"
    });

    window.onload = function() {
        video.init();
        $focus($G("videoUrl"));
        <%
        if(!string.IsNullOrEmpty(Request.QueryString["path"]))
        {
        %>
                    var tabElements = $G("videoTab").children,
                    tabHeads = tabElements[0].children,
                    tabBodys = tabElements[1].children;
                    for(j=0;j<tabHeads.length;j++)
                    {
                        if(tabBodys[j].id=="videoSelect")
                        {
                            tabHeads[j].className="focus";
                            tabBodys[j].style.display="";
                        }
                        else
                        {
                            tabHeads[j].className="";
                            tabBodys[j].style.display="none";
                        }
                    }
                    
        <%
        }
        %>
        

        var settings = {
            upload_url: editor.options.fileUrl,           //附件上传服务器地址
            file_post_name: editor.options.fileFieldName,      //向后台提交的表单名
            flash_url: "../../third-party/swfupload/swfupload.swf",
            flash9_url: "../../third-party/swfupload/swfupload_fp9.swf",
            post_params: { "UpType": "media", "SiteDir": "<%=sitedir %>" }, //解决session丢失问题
            file_size_limit: "<%=MaxSize %> MB",                                 //文件大小限制，此处仅是前端flash选择时候的限制，具体还需要和后端结合判断
            file_types: "<%=AllowExt %>",                                         //允许的扩展名，多个扩展名之间用分号隔开，支持*通配符
            file_types_description: "All Files",                      //扩展名描述
            file_upload_limit: 100,                                   //单次可同时上传的文件数目
            file_queue_limit: 10,                                      //队列中可同时上传的文件数目
            custom_settings: {                                         //自定义设置，用户可在此向服务器传递自定义变量
                progressTarget: "fsUploadProgress",
                startUploadId: "startUpload"
            },
            debug: false,

            // 按钮设置
            button_image_url: "../../themes/default/images/fileScan.png",
            button_width: "100",
            button_height: "25",
            button_placeholder_id: "spanButtonPlaceHolder",
            button_text: '<span class="theFont">' + lang.browseFiles + '</span>',
            button_text_style: ".theFont { font-size:14px;}",
            button_text_left_padding: 10,
            button_text_top_padding: 4,

            // 所有回调函数 in handlers.js
            swfupload_preload_handler: preLoad,
            swfupload_load_failed_handler: loadFailed,
            file_queued_handler: fileQueued,
            file_queue_error_handler: fileQueueError,
            //选择文件完成回调
            file_dialog_complete_handler: function(numFilesSelected, numFilesQueued) {
                var me = this;        //此处的this是swfupload对象
                if (numFilesQueued > 0) {
                    dialog.buttons[0].setDisabled(true);
                    var start = $G(this.customSettings.startUploadId);
                    start.style.display = "";
                    start.onclick = function() {
                        me.startUpload();
                        start.style.display = "none";
                    }
                }
            },
            upload_start_handler: uploadStart,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: function(file, serverData) {
                try {
                    var info = eval("(" + serverData + ")");
                } catch (e) { }
                var progress = new FileProgress(file, this.customSettings.progressTarget);
                if (info.state == "SUCCESS") {
                    progress.setComplete();
                    progress.setStatus("<span style='color: #0b0;font-weight: bold'>" + lang.uploadSuccess + "</span>");
                    filesList.push({ url: info.url, type: info.fileType, original: info.original });
                    progress.toggleCancel(true, this, lang.delSuccessFile);
                } else {
                    progress.setError();
                    progress.setStatus(info.state);
                    progress.toggleCancel(true, this, lang.delFailSaveFile);
                }

            },
            //上传完成回调
            upload_complete_handler: uploadComplete,
            //队列完成回调
            queue_complete_handler: function(numFilesUploaded) {
                dialog.buttons[0].setDisabled(false);
                //                var status = $G("divStatus");
                //                var num = status.innerHTML.match(/\d+/g);
                //                status.innerHTML = ((num && num[0] ?parseInt(num[0]):0) + numFilesUploaded) +lang.statusPrompt;
            }
        };
        swfupload = new SWFUpload(settings);
    };

    function insertLocal() {
        var map = fileTypeMaps,
                str = "";
        videoObjs = [];

        for (var i = 0, ci; ci = filesList[i++]; ) {
            videoObjs.push({
                url: ci.url,
                width: 420,
                height: 280,
                align: "none"
            });
        }
        editor.execCommand('insertvideo', videoObjs);
        swfupload.destroy();
    }

    //在已上传中选择，用户双击文件
    function InsertSelect(url) {
        videoObjs = [];
        videoObjs.push({
            url: url,
            width: 420,
            height: 280,
            align: "none"
        });
        editor.execCommand('insertvideo', videoObjs);
        dialog.close();
    }
    

</script>
</body>
</html>