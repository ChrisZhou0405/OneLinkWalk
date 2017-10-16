<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="upimage.aspx.cs" Inherits="KingTop.WEB.SysAdmin.upfiles.FlashImageUpload.upimage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en">
	<head>
		<title>????</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
		<link href="../../css/ustyle.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" src="../swfobject.js"></script>
		<script src="../../JS/jquery-3.2.min.js" type="text/javascript"></script>
		<script type="text/javascript">
		    var initParam
		    window.onload = function() {
		        initParam=$("#InitParam").val();
		        var params = {
		            uploadServerUrl: "imageupload.aspx?InitParam=" + initParam,
		            jsFunction: "upload", 		
		            filter: "<%=AllowExt %>",
		        };
		        swfobject.embedSWF("uploadImage.swf", "myContent", "500", "280", "10.0.0", "expressInstall.swf", params);
		    }
		
		function upload()
		{
		    var url;
		    $.ajax(
            {
                type: "post",
                contentType: "application/json",
                url: "../Silverlight/getuploadfiles.asmx/UploadFiles",
                data: "{sk:'"+$("#cacheKey").val()+"'}",
                dataType: 'json',
                success: function(result) {
                    url=result.d;
                    if (url == "" || url == null)
		                return;

		            if (url.indexOf(",") != -1) {
		                var arr = url.split(",");
		                for (var i = 0; i < arr.length; i++) {
		                    InputUrl(arr[i]);
		                }
		            }
		            else {
		                InputUrl(url);
		            }
                },
            }
            );
            
            //parent.Closed();
		}

		function InputUrl(urls) {
		    if (urls == "" || urls == null)
		        return;

		    AddItemToMultiFile(urls,true);
		}

		String.prototype.replaceAll = function(s1, s2) {

		    return this.replace(new RegExp(s1, "gm"), s2);

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
        
        function SetOK()
    {
        var iWidth=$("#txtWidth").val();
        var iHeight=$("#txtHeight").val();
        var ChkValue=$("#chkWater").is (":checked")?"1":"0";
        if(trim(iWidth)!="")
        {
            for(var i=0;i<iWidth.length ;i++)
            {
                var iOne=iWidth .substring(i,i+1);
                if(("0123456789").indexOf (iOne )==-1)
                {
                    alert("缩略图宽度必须填写数字！");
                    return false;
                }
            }
        }
        if(trim(iHeight)!="")
        {
            for(var i=0;i<iHeight.length ;i++)
            {
                var iOne=iHeight .substring(i,i+1);
                if(("0123456789").indexOf (iOne )==-1)
                {
                    alert("缩略图高度必须填写数字！");
                    return false;
                }
            }
        }
        var imgSet=iWidth+","+iHeight+","+ChkValue;
        var objValue=$("#InitParam").val();
        if(objValue.indexOf("ImgSet")!=-1)
        {
            var arr=objValue.split("ImgSet");
            objValue=arr[0]+"ImgSet="+imgSet;
        }
        else
        {
            objValue=objValue+"_tp_ImgSet="+imgSet;
        }
        $("#InitParam").val(objValue);
        
        var params = {
		    uploadServerUrl: "imageupload.aspx?InitParam=" + objValue,
		    jsFunction: "upload", 		
		    filter: "<%=AllowExt %>",
		};
		swfobject.embedSWF("uploadImage.swf", "myContent", "500", "280", "10.0.0", "expressInstall.swf", params);
		alert("设置完毕！");
    }
    
    function trim(str) {
        return str.replace(/(^\s*)|(\s*$)/g, "");
    }

	</script>
	</head>
	
	<body style="margin:0px">
	<form id="form1" runat="server">
	    <input type="hidden" id="cacheKey" runat="server" />
        <input type="hidden" id="InitParam" runat="server" />
		<div id="myContent">
		
		</div>
		<div id="divID" runat="server" style="float :left;width:320px;padding-left:10px">
        是否打印水印 <input type="checkbox" id="chkWater" runat ="server" />
        <br />缩略图尺寸设置 宽：<input type="text" id="txtWidth" runat="server" style="width:30px" maxlength="3"/>px 高：<input type="text" id="txtHeight" runat="server"  style="width:30px" maxlength="3"/>px
        <span style='color:#4D4D4D'><br />1.如果缩略图宽和高为空或为零，则不生成缩略图
        <br />2.宽大于零，高为零或空，则按照宽生成等比例缩略图
        <br />3.高大于零，宽为零或空，则按照高生成等比例缩略图
        </span>
        <br />修改以上设置后，请点击<input type="button" id="btnOK" value="OK" style="height: 18px; background: none repeat scroll 0% 0% rgb(37, 186, 217); color: rgb(255, 255, 255); border-left: 1px solid rgb(100, 215, 239); border-width: 1px; border-style: solid; border-color: rgb(100, 215, 239) rgb(37, 186, 217) rgb(37, 186, 217) rgb(100, 215, 239);" onclick ="SetOK()" />
        
        </div>
        <div id="WaterDivId" runat="server">
        
        </div>
        <div style="clear:left ">
        <div id="divAllowType" runat="server" style="padding-left:10px;"></div>
		</form>
	</body>
</html>
