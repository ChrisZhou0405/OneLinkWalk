<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editupload.aspx.cs" Inherits="KingTop.WEB.SysAdmin.upfiles.uploadify.editupload" %>
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
        var filesUrls="";
        var URL_Path="<%=URL %>";
        $(document).ready(function() {
            $("#uploadify").uploadify({
                'uploader': 'javascript/jquery.uploadify-v2.1.4/uploadify.swf',
                'script': 'upload.ashx',
                'scriptData':{<%=urlParam %>},
                'cancelImg': 'javascript/jquery.uploadify-v2.1.4/cancel.png',
                'folder': 'UploadFile',
                'sizeLimit': <%=MaxSize %>,
                'queueSizeLimit': 5,
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
                    
                    if(filesUrls!="")
                    {
                        window.parent.CKEDITOR.tools.callFunction(<%=Request.QueryString["CKEditorFuncNum"] %> ,filesUrls,'') ;
                    }
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
		        
		        if(filesUrls=="")
		        {
		            filesUrls=URL_Path+urls;
		        }
		        else
		        {
		           filesUrls+="$$$"+ URL_Path+urls;
		        }

		}
    </script>
</head>
<body>    
    <div style="text-align:center;width:100%;padding-bottom:5px;">
    <div></div>
     <input type="hidden" id="InitParam" value="<%=urlParam %>"/>   
    
    <div id="ShowImage" style="text-align:left;padding-left:5px;padding-bottom:5px;width:410px;height:<%=divHeight%>;border:1px solid #CCCCCC;OVERFLOW-y: scroll;overflow-x:hidden;"></div> 
    
    
    <div style="padding-top:5px"></div>
    
    <div style="clear:both;font-size:1%;line-height:1%;height:0;"></div>   
    <input type="file" name="uploadify" id="uploadify" />&nbsp;&nbsp;
    <a id="btnBegin" href="javascript:return false"><img alt="开始上传" src="javascript/jquery.uploadify-v2.1.4/btnstart.png" width="120px" height="30px" style="border:0px"/></a> 
    
    </div>
    
    <div id="divID" runat="server" visible="false" style="float :left;width:290px;padding-top:8px;display:none;">
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
</body>
</html>