<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="background.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Editor.ueditor.dialogs.background.background" %>
<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="background.css">
</head>
<body>
    <div id="bg_container" class="wrapper">
        <div id="BackgroundTab">
            <div id="tabhead" class="tabheade">
                <span class="focus" tabsrc="normal"><var id="lang_background_normal"></var></span>
                <span tabSrc="local"><var id="lang_background_local"></var></span>
                <span class="" tabsrc="imgManager"><var id="lang_background_imgManager"></var></span>
            </div>
            <div id="tabbody" class="tabbody">
                <div id="normal" class="panel">
                    <fieldset  class="bgarea">
                        <legend><var id="lang_background_set"></var></legend>
                        <div class="content">
                            <div>
                                <input class="iptradio" type="radio" name="t" value="none" checked="checked" onclick="net(this)"><label><var id="lang_background_none"></var></label>
                            </div>
                            <div class="wrapcolor">
                                <div class="color">
                                    <input class="iptradio" type="radio" name="t" value="color" onclick="net(this)"><label><var id="lang_background_color"></var>:</label>
                                </div>
                                <div id="colorPicker"></div>
                                <div class="clear"></div>
                            </div>
                            <div class="wrapcolor pl">
                                <label><var id="lang_background_netimg"></var>:</label><input class="txt" type="text" id="url">
                            </div>
                            <div id="alignment" class="alignment">
                                <var id="lang_background_align"></var>:<select id="repeatType" onchange="selectAlign(this)">
                                    <option value="center"></option>
                                    <option value="repeat-x"></option>
                                    <option value="repeat-y"></option>
                                    <option value="repeat"></option>
                                    <option value="self"></option>
                                </select>
                            </div>
                            <div id="custom" >
                                <var id="lang_background_position"></var>:x:<input type="text" size="1" id="x" maxlength="4" value="0">px&nbsp;&nbsp;y:<input type="text" size="1" id="y" maxlength="4" value="0">px
                            </div>
                        </div>
                    </fieldset>

                </div>
                <div id="local" class="panel">
                        <div id="flashContainer" style="margin:10px"></div>
                        <div><div id="upload"></div><div id="duiqi"></div><div id="localFloat"></div>
                        <div style="clear:left"></div>
                        <table border=0 style="color :#0066CC">
                        <tr>
                            <td width=10></td>
                            <td>
                            <input type="hidden" value="<%=paramValue %>" id="hidInitParam" />
                            <input type="hidden" value="<%=paramKey %>" id="hidKeyCode" />
                            是否添加水印 <input type="checkbox" id="chkWater" runat ="server" style="width:15px;height:15px"/><br />
                             允许上传的文件类型：<%=AllowExtMemo%><br />最大可以上传<%=MaxSize %> MB
                            </span>
                            </td>
                            <td id="WaterDivId" runat="server" style="padding-left:10px">
                            </td>
                        </tr>
                        </table>
                        
                          </div>
                        </div>
                    <div id="imgManager" class="panel">
                        
                        
                        <link rel="stylesheet" type="text/css" href="../video/Style.css" />

                        <script language="javascript">
                        
                        function toRename(p_name, p_fullName, p_path,p_param) {
                            document.getElementById ("divRename").style.display="block";
                            document.getElementById ("txtFolderName").value=p_name;
                            document.getElementById ("txtOldName").value=p_fullName;
                            
                            document.getElementById ("txtFolderName").focus();
                            document.getElementById ("submit").value="重命名";
                            document.getElementById ("insertForm").action= "background.aspx?act=rename&path=" + p_path + p_param;
                            
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
                            var search=document.getElementById ("keyword").value;
                            var url = "background.aspx?act=search&path=<%= Server.UrlEncode(folderPath) %>&type=<%=Request.QueryString["type"] %>&keyword="+search;
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
                                                <%# GetFiles(Eval("FormatName").ToString(), Eval("Name").ToString(), folderPath, Eval("Ext").ToString(), Eval("Type").ToString(), Eval("ImgWidth").ToString(), Eval("ImgHeight").ToString())%>
                                                <div style="width:100%;padding-top:5px;text-align:center">
                                                
                                                <%#URLStr %><%#Eval("Name").ToString().Length > 15 ? Eval("Name").ToString().Insert(15, "<br>") : Eval("Name").ToString()%><%#string.IsNullOrEmpty(URLStr)?"":"</a>" %>

                                                </div>
                                            <div align="center" style="padding-top:3px">
                                                <input type="button" class="btn" value="重命名" title ="重命名" style="width:40px" onclick="javascript:toRename('<%# Eval("Name") %>', '<%# Eval("FullName") %>', '<%= Server.UrlEncode(folderPath) %>','&type=<%=Request.QueryString["type"] %>&keyword=<%=Request.QueryString["keyword"] %>');" />
                                                <input type="button" class ="btn" value="原图" style="width:35px;<%#string.IsNullOrEmpty(FileUrl)?"display:none":"" %>" onclick="window.open('/<%#FileUrl %>');" title="点击查看原图"/>
                                                <input type="button" class="btn" value="删除" style="width:35px" title ="删除" onclick="javascript:OnDelete('<%# Eval("Name") %>','background.aspx?act=delete&amp;path=<%= Server.UrlEncode(folderPath) %>&amp;file=<%# Eval("FullName") %>&amp;filetype=<%# Eval("Type") %>&type=<%=Request.QueryString["type"] %>&keyword=<%=Request.QueryString["keyword"]%>');" />
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
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../internal.js"></script>
    <script type="text/javascript" src="../tangram.js"></script>
    <script type="text/javascript" src="background.js"></script>
    <script type="text/javascript" src="wordimage.js"></script>
    <script type="text/javascript">
        //全局变量
        var flashOptions;
        var callbacks;
        var imageUrls = [],          //用于保存从服务器返回的图片信息数组
            selectedImageCount = 0;  //当前已选择的但未上传的图片数量

        editor.setOpt({
            imageFieldName:"upfile",
            compressSide:0,
            maxImageSideLength:900
        });
        utils.domReady(function(){
            flashOptions = {
                container:"flashContainer",                                                    //flash容器id
                url:editor.options.imageUrl,                                           // 上传处理页面的url地址
                ext:'{"param1":"<%=paramKey %>", "param2":"<%=paramValue %>","SaveType":"<%=Request.QueryString["SaveType"] %>","SiteDir":"<%=sitedir %>","UpType":"image"}',                                 //可向服务器提交的自定义参数列表
                fileType:'{"description":"'+lang.fileType+'", "extension":"<%=AllowExt %>"}',     //上传文件格式限制
                flashUrl:'../image/imageUploader.swf',                                                  //上传用的flash组件地址
                width:580,          //flash的宽度
                height:188,         //flash的高度
                gridWidth:121,     // 每一个预览图片所占的宽度
                gridHeight:120,    // 每一个预览图片所占的高度
                picWidth:100,      // 单张预览图片的宽度
                picHeight:100,     // 单张预览图片的高度
                uploadDataFieldName:editor.options.imageFieldName,    // POST请求中图片数据的key
                picDescFieldName:'pictitle',      // POST请求中图片描述的key
                maxSize:<%=MaxSize %>,                         // 文件的最大体积,单位M
                compressSize:2,                   // 上传前如果图片体积超过该值，会先压缩,单位M
                maxNum:1,                         // 单次最大可上传多少个文件
                compressSide:editor.options.compressSide,                 //等比压缩的基准，0为按照最长边，1为按照宽度，2为按照高度
                compressLength:editor.options.maxImageSideLength        //能接受的最大边长，超过该值Flash会自动等比压缩
            };
            //回调函数集合，支持传递函数名的字符串、函数句柄以及函数本身三种类型
            callbacks = {
                // 选择文件的回调
                selectFileCallback: function(selectFiles){
                    selectedImageCount += selectFiles.length;
                    if(selectedImageCount) baidu.g("upload").style.display = "";
                    dialog.buttons[0].setDisabled(true); //初始化时置灰确定按钮
                },
                // 删除文件的回调
                deleteFileCallback: function(delFiles){
                    selectedImageCount -= delFiles.length;
                    if (!selectedImageCount) {
                        //baidu.g("upload").style.display = "none";
                        dialog.buttons[0].setDisabled(false);         //没有选择图片时重新点亮按钮
                    }
                },

                // 单个文件上传完成的回调
                uploadCompleteCallback: function(data){
                    try{
                        var info = eval("(" + data.info + ")");
                        info && imageUrls.push(info);
                        selectedImageCount--;
                    }catch(e){}

                },
                // 单个文件上传失败的回调,
                uploadErrorCallback: function (data){
                    if(!data.info){
                        alert(lang.netError)
                    }
                    //console && console.log(data);
                },
                // 全部上传完成时的回调
                allCompleteCallback: function(){
                    dialog.buttons[0].setDisabled(false);    //上传完毕后点亮按钮
                    ShowTab("normal");
                    $G("url").value=imageUrls[imageUrls.length-1].url;
                    hideFlash();
                }
                // 文件超出限制的最大体积时的回调
                //exceedFileCallback: 'exceedFileCallback',
                // 开始上传某个文件时的回调
                //startUploadCallback: startUploadCallback
            };
            wordImage.init(flashOptions,callbacks);
            
            <%
        if(!string.IsNullOrEmpty(Request.QueryString["path"]))
        {
        %>
            ShowTab("imgManager");
                    
        <%
        }
        %>
            
        });
        
        function ShowTab(id)
        {
        var tabElements = $G("BackgroundTab").children,
                    tabHeads = tabElements[0].children,
                    tabBodys = tabElements[1].children;
                    for(j=0;j<tabHeads.length;j++)
                    {
                        if(tabBodys[j].id==id)
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
        }
        
        function InsertSelect(url)
        {
            ShowTab("normal");
            $G("url").value=url;
        }
        
       
        function SetOK()
        {
            var Chkobj=document.getElementById ("chkWater");
            var ChkValue=0;
            if(Chkobj.checked==true)
            {
                ChkValue=1;
            }
            var imgSet =  "0,0," +ChkValue ;
            if (imgSet == document.getElementById("hidInitParam").value) {
                return "";
            }
            document.getElementById("hidInitParam").value=imgSet;
            return imgSet;
        }
    </script>
</body>
</html>
