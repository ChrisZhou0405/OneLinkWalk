<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="import.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Editor.ueditor.import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../../js/public.js"></script>
    <script type="text/javascript" src="../../js/publicform.js"></script>
    <link href="../../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../../js/win.js"></script>
    <script type="text/javascript" src="../../js/Common.js"></script>

    <script>
        function GetContent() {
            var con = document.getElementById("txtArea").value;
            if(con!="")
            {
                parent.<%=Request.QueryString["editid"] %>.setContent(con);
                parent.Closed();
            }
        }

        function Uploading() {
//            if($("#docfile").val()!="")
//            {
                $("#conDiv").hide();
                $("#tipDiv").show();
//                return true;
//            }
//            else
//            {
//                alert({msg:'请选择需要导入的文件',title:'提示'});
//                return false;
//            }
        }

        $(function(){
            GetContent();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tipDiv" style="display:none;width:100%;text-align:center;padding:50px 0;">
            <img src="../../images/loading.gif" border=0 /> 请不要关闭，正在导入文档......
            </div>
    <div id="conDiv" style="line-height:22px;">
    <fieldset style="padding-left:30px;border:0px">
    <legend></legend>
    <textarea id="txtArea" name="txtArea" rows=20 cols=50 style="display:none" runat="server"></textarea>
    <input type="file" id="docfile" name="docfile" runat="server"/>
    <br />
    <input type="checkbox" id="chkClearHtml" name="chkClearHtml" style="margin-top:10px" checked value="1"/> 清除冗余样式
    <br />
    <input type="button" id="btnSubmit" onclick="Uploading();" onserverclick="btnSubmit_Click" value=" 确定 " style="border:0px;height:26px;width:80px;margin-top:10px" class="subButton" runat="server"/>
    </fieldset>
    <fieldset style="padding-left:30px;margin-top:15px;border:1px solid #ccc">
    <legend>&nbsp; 说 明&nbsp; </legend>
    1.支持导入的文档有:Word,Excel,PPT,PDF
    <br />
    2.文档大小不能超过4M，PPT和PDF文档以图片方式导入
    <br />
    3.导入时会将原编辑器里的内容清空
    <br />
    4.EXCEL只能导入单工作表：如图示<img src="../../images/excel.jpg" border=0 />
    </fieldset>
    </div>
    </form>
</body>
</html>
