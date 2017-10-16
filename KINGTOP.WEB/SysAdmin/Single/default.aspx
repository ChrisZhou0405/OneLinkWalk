<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1"><title>
	单页栏目
</title><link href="../css/ustyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.form.select.js"></script>
    <script type="text/javascript" src="../js/public.js"></script>
    <link rel="stylesheet" href="../css/template.css" type="text/css" /><link href="../CSS/validationEngine.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery-validationEngine-cn.js"></script>
    <script src="../JS/jquery-validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() { $("#theForm").validationEngine({ promptPosition: "centerRight" }) });
    </script>
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>

    <script type="text/javascript" src="../js/Common.js"></script> 
    <script type="text/javascript">
        function SelectChange() {
            var count = document.getElementById("ddlCustomContentCount").value;
            for (var i = 2; i <= count; i++) {
                document.getElementById("dlCustom" + i).style.display = "";
            }
            for (var j = parseInt(count) + 1; j < 21; j++) {

                document.getElementById("dlCustom" + j).style.display = "none";
            }
        }
        function replaceEditor(a, b, c) {
            while (a.indexOf(b) != -1) {
                a = a.replace(b, c);
            }
            return a
        }
        function ChangeEditor(i, isChecked) {
            var editorHtml = $("#ddMenuDesc").html();
            var startIndex = editorHtml.toLowerCase().lastIndexOf("<script");
            if (startIndex == -1) {
                startIndex = editorHtml.toLowerCase().indexOf("<iframe");
            }
            else {
                var instance = CKEDITOR.instances["txtCustomContent" + i];
                if (instance) {
                    CKEDITOR.remove(instance);
                }
            }
            editorHtml = editorHtml.substring(startIndex, editorHtml.length);

            if (editorHtml.toLowerCase().indexOf("<iframe") > -1) {
                $("#txtCustomContent" + i).css("display", "none");
            }
            editorHtml = replaceEditor(editorHtml, "Editor1$txtEditorContent", "txtCustomContent" + i);
            editorHtml = replaceEditor(editorHtml, "Editor1", i);

            if (isChecked) {
                $("#dd" + i).html($("#dd" + i).html() + editorHtml);
            }
            else {
                $("#txtCustomContent" + i)[0].style.display = "";
                $("#txtCustomContent" + i)[0].style.visibility = "visible"; ;
                var ddHtml = $("#dd" + i).html();
                startIndex = ddHtml.toLowerCase().lastIndexOf("</textarea") + 11;
                $("#dd" + i).html(ddHtml.substring(0, startIndex));
            }
        }

        var showTotal = 1;
        var type = -1;
        var title = "";
        var errmsg = "";
        var editSelfMenuUrl = "";
        function showMessage() {
            var msgContent = "";
            var jumpurl = "";
            //添加成功
            if (type == 0) {
                msgContent = "添加栏目<font color=red>" + title + "</font>成功<br>";
                msgContent += "<a href='ColumnManage.aspx?NodeCode=100015001' style='color:blue'>返回列表</a> | ";
                msgContent += "<a href='SingleColumn.aspx?Action=Edit&NodeID=cf36d960-143c-4060-917e-328543f3efa6&ID=e383f417-5467-4c6d-aa75-3929a37f179d&NCode=105021001&NodeCode=100015001' style='color:red'>继续添加</a> | ";
                msgContent += "<a href='" + editSelfMenuUrl + "' style='color:red'>修改本栏目</a>";
                msgContent += "<br>注：3秒钟后自动转到列表页面";
                jumpurl = "ColumnManage.aspx?NodeCode=100015001";
                alert({ msg: msgContent, status: "1", title: "提示信息", url: jumpurl, time: 3000, width: 400 })
            }
            else if (type == 1) {  //修改成功
                msgContent = "修改栏目<font color=red>" + title + "</font>成功<br>";
                msgContent += "<a href='ColumnManage.aspx' style='color:blue'>返回列表</a> | ";
                msgContent += "<a href='SingleColumn.aspx?Action=Edit&NodeID=cf36d960-143c-4060-917e-328543f3efa6&ID=e383f417-5467-4c6d-aa75-3929a37f179d&NCode=105021001&NodeCode=100015001' style='color:red'>继续修改</a>";
                if (editSelfMenuUrl != "") {
                    msgContent += " | <a href='" + editSelfMenuUrl + "' style='color:red'>添加新栏目（同级）</a>";
                }
                msgContent += "<br>注：3秒钟后自动转到列表页面";
                jumpurl = "ColumnManage.aspx?NodeCode=100015001";
                alert({ msg: msgContent, status: "1", title: "提示信息", url: jumpurl, time: 3000, width: 400 })
            }
            else if (type == 2) {  //操作失败
                msgContent = "操作失败,原因如下：<br>";
                msgContent += "<font style='color:green'>" + errmsg + "</font><br>";
                msgContent += "注：10秒钟后提示框自动关闭";
                alert({ msg: msgContent, status: "2", title: "提示信息", time: 10000, width: 400 })
            }
            else if (type == 3) {  //修改成功
                msgContent = "删除栏目<font color=red>" + title + "</font>成功<br>";
                msgContent += "注：3秒钟后自动转到列表页面";
                jumpurl = "ColumnManage.aspx?NodeCode=100015001";
                alert({ msg: msgContent, status: "1", title: "提示信息", url: jumpurl, time: 3000, width: 400 })
            }
        }
        window.onload = function() {
            if (type > -1) {
                showMessage();
            }
            for (var i = 1; i < 21; i++) {
                var chk = document.getElementById("chkIsHtmlEditor" + i);
                if (chk.checked) {
                    ChangeEditor(i, true);
                }
                if (showTotal > 1 && i <= showTotal && i > 1) {
                    $("#dlCustom" + i).css("display", "");
                }
            }
        }
        function setAction1(data) {
            if (data == "true") {
                document.getElementById("btnHidAction").click();
            }
        }
        function selectTemplateFile(contrName) {
            openframe({ title: "选择模板文件", url: "seltemplate.aspx?contrName=" + contrName, width: '400', height: '400' });
        }
    </script>
</head>
<body>
    <form name="theForm" method="post" action="SingleColumn.aspx?Action=Edit&amp;NodeID=cf36d960-143c-4060-917e-328543f3efa6&amp;ID=e383f417-5467-4c6d-aa75-3929a37f179d&amp;NCode=105021001&amp;NodeCode=100015001" id="theForm">
<div>
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="O7Xw0BVE57gRw14EqM0nsVYK7jpRIHjoEMUGXZcZFYnKvcitmrRuP5MMXPcfJJz9O/facCh0oJiIdIRs6+gZoCn5eRqGCPnSOdi9wE/2JWO6fSAFRzjFhpjlgk/gbJTcYuK2znR+uqTngK1aOJYQXyTsx47CDohXJAOClxILwjCecGV24qlVJBA9dwb4U6w/GtxNaVqoapkdApl7w27KUedMqGR1/W3OnHcC3rKOsRA2qzVRwijdG+5AmdI6Y9i9yHdSLA0MDS2yNT9mpmDRhUFetW1qlxDz+F7VPmcIEwGUs6ADdxzEBk3CMNVy3R4Kdwc/q/5S19B7CMO+jJfp1TVKLqJ9ErYTsHumdpU+1fenr/BSd+RX6hT+Gxrx6rYfZMmSEHFRe0d759XHiCF6BdmRmy63ndmRni+Jk5epryAQ7l2CdbAW/ZiY/u56yAeNif6xrQ3qq/Bq/k7j93ZPjlNw5vhpwrjtqv4TbhQmxNqEW7+rN9SwJyZufIlxpx8Zlh8CUg7yRtm/TY3zKMLch1qr9NP+1cFrjM7LQIZ1fhTPZf13CxU5j4C9rJGzEEaPiyhx2WmJLxOa1NMRxi2VeCP0rbSqEoZXtQHfjyQRufIyswQomC/BLB1zkV5HiiKKmW4yQKNbYGgN/OH+KAB3fysjINOR2rry7JvhoolguHovG0PwIGiWqgMpSDBZBGQQdum+trwgi5ZPaOnmHaRX5w2LWZnmqeXQcPok8z+0olr0oACPq5Q96gvSAvVF+fjaFk+Vjniw6u2XKO5XPB/TSLPtlIL59IPGGv8D3tSJiuMuqH0lDJbnkbFrbvLzx+uPz1EGGIeceNr5c798zH3HigcGYQhy4gmZnmhEd8c8fPyBApaWc6z3R+Jet7aW7vYVygpL5wnxwsxVsBF0lH6PGW0V9G4qQvq6Ukp1Mm/VdFyUhCi1zjG1iM+QcAhILtyCyr29ucZuvOMY6lsJTx1ssPtZ9br0dvJPBg1rPKPU6QSyQnI3t+fXtSt2z8AEFgpq9Z1tdxJ5HIQQhsBNo+UN5qADudai9QZfrfb6Kva0l1taA85ncga2D2O0V7taPejPF+d+H7QJD/JZ9jEyJsaNruPnAEC1/adYJp4yRDv5rDCKHOMdEsFbQgdLO3jpruRx8RnYCzuiAykLwqWGdIuN4Tu0UIiQbr+hiWK2sgG10l3zFwoEtc9A9NGMKb9CrStcKWBOJw==" />
</div>

<div>

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="mbaHxHDkK6Qs61+WfLPEySo0PzTueUK/0xgygYtCnppJnYxKhtHpjKEHPV83PdHPNUwsjj0r811fyd6vO9Le1rIfL3DOqlgXJ4QR/wjHHYbMgwfPezz8i611NKCh9qA8xBZRqxKhJqllnh8pH5kPOeWl9pooZ/Hu6ogaGMpbUL5qJNJDIoHPCIe2Q2UajYikhm2NFaTg5plNI/V26IVuMyUMBmxwANePtydrlIxdm0PxjwEbWlGFnk9XqH0vMyCGjB6aPKFE8tIrYzShCgX5+tkJy0HWHOgSQfcjW8ZtgnJqNDIX+Dja2ANtU7SFavbJtRhcps2mROV4RAk4f1UWPNPpBbSpc7b7cH4wc4/AY7JWka46Wzo0wKl9MkCnMYAIZDN4y9NVAP6jZzWKU452PiDcHQkpKUCHulBqRv3XVmgr/TNi02Kzj73+/5Jo/a6Fj7+WRia9s+p/ot2LutinMsMmn3UqnFxS4MttpghA6IxZKo+JhleYedTiOZzWV0KmY81YtWQl686vC5m6b/vzwQJbHbS6gHy4Ilw0xS+KmzxjogBGS1x5oTzYoR4With7QH93MoyKzOMrnxvoykNuU99OJRxR4y69h9AayvRp3bn8UpLcudvPUZfm+JnHmiVT9DUjlAQe0fTnFNpr3llEe/0RNUP+tykqHhTDpuvHJHSrYtMX9qD3u5ws+tCSw1kB5t5rWlcAf8+ArVN9Wp3rPUzeyY+3hsfYveuUnGoIP9GEH6xI0FVdP1OBQ9KxbyJvyhsyWB5vvZT7lplzuUz9un0xDfC+kjI/poLXu/AjYLSukU9f" />

</div>
    <input name="hidLogTitle" type="hidden" id="hidLogTitle" value="公司简介" />
    <div class="container">
        <h4>
            位置： <a href="#">系统管理</a> &gt; <a href="#">栏目管理</a> &gt; <a href="#">栏目管理</a> &gt; <span class="breadcrumb_current"> 添加/修改单页栏目</span>

        </h4>
        <div id="con">
            <ul id="tags">
                <li class="selectTag"><a href="javascript:;">基本信息</a> </li>  
                <li><a href="javascript:;">栏目选项</a></li>              
                <li><a href="javascript:;" style="display:none">自设内容</a></li>
            </ul>
            <div id="panel">

            
                <fieldset>
                    <dl>
                        <dt>栏目名称：</dt>
                        <dd>
                           <input name="txtNodeName" type="text" value="公司简介" id="txtNodeName" class="validate[required,length[1,30]]" style="width:231px;" /> <font color=red>*</font>
                           
                           
                        </dd>
                    </dl>

                    <dl>
                        <dt>英文名称：</dt>
                        <dd><input name="txtNodelEngDesc" type="text" value="Company Profile" id="txtNodelEngDesc" /></dd>
                    </dl> 
                    <dl>
                        <dt>是否父节点(只有父节点下才能包含子节点，父节点不能属于某个模块)：</dt>
                        <dd> 
                            <input id="chkIsFolder" type="checkbox" name="chkIsFolder" /><label for="chkIsFolder">是</label>

                        </dd>
                    </dl>
                    <dl style="display:none">
                        <dt>所属模块：</dt>
                        <dd>
                        <select name="ddlModel"><option value="9a132b3b-c0f6-4951-892c-0cd87ad2ff39">单页内容管理</option></select>
                        </dd>
                    </dl>

                    <dl style="display:none">
                        <dt>是否生成HTML：</dt>
                        <dd>
                        <input id="radCreateContentPageFalse" type="radio" name="IsCreateContentPage" value="radCreateContentPageFalse" checked="checked" /><label for="radCreateContentPageFalse">生成HTML</label><br>
                        <input id="radCreateContentPageTrue" type="radio" name="IsCreateContentPage" value="radCreateContentPageTrue" /><label for="radCreateContentPageTrue">不生成HTML，使用动态页方式</label>
                        </dd>          
                    </dl>  
                    <dl style="display:none">

                        <dt>单页模板：</dt>
                        <dd><input name="txtDefaultTemplate" type="text" id="txtDefaultTemplate" style="width:280px;" /> <input type="button" class="subButton" onclick="selectTemplateFile('txtDefaultTemplate')" value="选择模板..." /> (例如：/内容页模板/默认文章内容页模板.html)</dd>          
                    </dl>
                    <dl>
                        <dt>前台程序路径：</dt>
                        <dd><input name="txtSubDomain" type="text" value="/about/index.aspx" id="txtSubDomain" style="width:231px;" />(例如：/aboutindex.aspx?m=105001002 )</dd>

                    </dl>
                    <dl>
                        <dt>前台栏目自定义链接：</dt>
                        <dd><input name="txtLinkURL" type="text" value="/about/公司简介.html" id="txtLinkURL" style="width:231px;" />(例如：/关于我们.html，当启用伪静态时必须填写)</dd>
                    </dl>
                    <dl>
                        <dt>页面标题(针对搜索引擎设置的标题，针对性的设置，有利于SEO优化)：</dt>

                        <dd><div style="float:left"><textarea name="txtPageTitle" rows="2" cols="20" id="txtPageTitle" style="height:60px;width:500px;"></textarea> </div><div style="float:right">不填则页面显示默认标题，<br>标题格式为：栏目名称-公司名称。<br>例如：联系我们-深圳市图派科技有限公司</div></dd>
                    </dl>
                    <dl>
                        <dt>栏目Meta关键字(针对搜索引擎设置的关键词。多个关键词请用,号分隔)：</dt>
                        <dd><textarea name="txtKeyWords" rows="2" cols="20" id="txtKeyWords" style="height:60px;width:500px;">致力于成为亚洲地区顶尖的被动电子元器件制造商。主要研发和制造各类射频贴片电感、贴片磁珠、磁珠排、LC滤波器、介质天线、压敏电阻、功率电感、各类插装式和贴片式绕线电感等系列产品 </textarea></dd>

                    </dl>
                    <dl>
                        <dt>栏目Meta说明(针对搜索引擎设置的网页描述。多个描述请用,号分隔)：</dt>
                        <dd><textarea name="txtMetaDesc" rows="2" cols="20" id="txtMetaDesc" style="height:60px;width:500px;">致力于成为亚洲地区顶尖的被动电子元器件制造商。主要研发和制造各类射频贴片电感、贴片磁珠、磁珠排、LC滤波器、介质天线、压敏电阻、功率电感、各类插装式和贴片式绕线电感等系列产品 </textarea> </dd>                                                            
                    </dl>
                    
                    <dl>
                        <dt> 排序号：</dt>

                        <dd><input name="txtNodelOrder" type="text" value="0" id="txtNodelOrder" class="validate[required,custom[onlyNumber],length[0,3]] text-input" style="width:50px;" /></dd>
                    </dl>
                    <div style="clear:left"></div>
                 </fieldset>                    
                <fieldset style="display: none;">
                <dl>
                        <dt>单页提示：</dt>
                        <dd><input name="txtTips" type="text" id="txtTips" style="width:200px;" />(鼠标移至栏目名称上时将显示设定的提示文字（不支持HTML）)</dd>

                    </dl>
                
                    <dl>
                        <dt>栏目Banner：</dt>
                        <dd><input name="txtBanner" type="text" id="txtBanner" style="width:252px;" /> <input name="Button4" type="button" id="Button4" value="上传" Class="subButton" onclick="InputFile('theForm','txtBanner','image',1,'jpg|gif|bmp|png|swf|flv');" /></dd>
                    </dl>
                    <dl>
                        <dt>栏目图标(如果前台栏目导航选择图片方式，则需上传，下同)：</dt>

                        <dd><input name="txtNodelIcon" type="text" id="txtNodelIcon" style="width:252px;" /> <input name="Button3" type="button" id="Button3" value="上传" Class="subButton" onclick="InputFile('theForm','txtNodelIcon','image',1,'jpg|gif|bmp|png');" /></dd>
                    </dl>
                    <dl>
                        <dt>当前栏目图标：</dt>
                        <dd><input name="txtCurrentImg" type="text" id="txtCurrentImg" style="width:252px;" /> <input name="Button1" type="button" id="Button1" value="上传" Class="subButton" onclick="InputFile('theForm','txtCurrentImg','image',1,'jpg|gif|bmp|png');" /></dd>
                    </dl>
                    <dl>

                        <dt>鼠标移上去图标（鼠标移上去图标与当前栏目图标一样，则不必上传）：</dt>
                        <dd><input name="txtMouseOverImg" type="text" id="txtMouseOverImg" style="width:252px;" /> <input name="Button2" type="button" id="Button2" value="上传" Class="subButton" onclick="InputFile('theForm','txtMouseOverImg','image',1,'jpg|gif|bmp|png');" /></dd>
                    </dl>
                    <dl>
                        <dt>打开方式：</dt>
                        <dd><input id="radSelf" type="radio" name="OpenType" value="radSelf" checked="checked" /><label for="radSelf">在原窗口打开</label><input id="radNew" type="radio" name="OpenType" value="radNew" /><label for="radNew">在新窗口打开</label></dd>          
                    </dl>

                    
                    <dl style="display:none">
                        <dt>列表页后缀：</dt>
                        <dd>
                        <select name="ddlListPagePostFix" id="ddlListPagePostFix">
	<option selected="selected" value=""></option>
	<option value="html">html</option>
	<option value="htm">htm</option>

	<option value="shtml">shtml</option>
	<option value="shtm">shtm</option>

</select>
                        </dd>          
                    </dl>
                    
                    <dl>
                        <dt>左边菜单栏显示：</dt>
                        <dd>    
                            <input id="chkIsLeftDisplay" type="checkbox" name="chkIsLeftDisplay" checked="checked" /><label for="chkIsLeftDisplay">是</label>（默认为是）
                        </dd>

                    </dl>  
                    <dl>
                        <dt>前台头部栏目显示：</dt>
                        <dd>    
                            <input id="chkIsTopMenuShow" type="checkbox" name="chkIsTopMenuShow" checked="checked" /><label for="chkIsTopMenuShow">是</label>（默认为是）
                        </dd>
                    </dl>
                    <dl>
                        <dt>前台左边栏目显示：</dt>

                        <dd>    
                            <input id="chkIsLeftMenuShow" type="checkbox" name="chkIsLeftMenuShow" checked="checked" /><label for="chkIsLeftMenuShow">是</label>（默认为是）
                        </dd>
                    </dl>        
                    <dl>
                        <dt>是否有效：</dt>
                        <dd><table id="RBL_IsValid" border="0" style="height:16px;width:124px;">
	<tr>
		<td><input id="RBL_IsValid_0" type="radio" name="RBL_IsValid" value="0" /><label for="RBL_IsValid_0">禁用</label></td><td><input id="RBL_IsValid_1" type="radio" name="RBL_IsValid" value="1" checked="checked" /><label for="RBL_IsValid_1">启用</label></td>

	</tr>
</table></dd>
                    </dl>
                    <dl>
                        <dt>栏目说明：</dt>
                        <dd id="ddMenuDesc">
                        <textarea name="Editor1$txtEditorContent" rows="2" cols="20" id="Editor1_txtEditorContent" style="display:none">&lt;html&gt;&lt;head&gt;&lt;link rel=&quot;stylesheet&quot; type=&quot;text/css&quot; href=&quot;/sysadmin/editor/ueditor/themes/default/iframe.css&quot;&gt;&lt;style type=&quot;text/css&quot;&gt;.selectTdClass{background-color:#3399FF !important;}table.noBorderTable td{border:1px dashed #ddd !important}table{clear:both;margin-bottom:10px;border-collapse:collapse;word-break:break-all;}.pagebreak{display:block;clear:both !important;cursor:default !important;width: 100% !important;margin:0;}.anchorclass{background: url('/sysadmin/editor/ueditor/themes/default/</textarea>
<script type="text/javascript" charset="utf-8" src="../Editor/ueditor/editor_all.js"></script><script type="text/javascript" charset="utf-8" src="../Editor/ueditor/editor_config.js"></script><link rel="stylesheet" type="text/css" href="../Editor/ueditor/themes/default/ueditor.css"/><textarea id="Editor1" style="width:700px;height:150px;"><html><head><link rel="stylesheet" type="text/css" href="/sysadmin/editor/ueditor/themes/default/iframe.css"><style type="text/css">.selectTdClass{background-color:#3399FF !important;}table.noBorderTable td{border:1px dashed #ddd !important}table{clear:both;margin-bottom:10px;border-collapse:collapse;word-break:break-all;}.pagebreak{display:block;clear:both !important;cursor:default !important;width: 100% !important;margin:0;}.anchorclass{background: url('/sysadmin/editor/ueditor/themes/default/</textarea>
<script type="text/javascript">var editor_Editor1 = new baidu.editor.ui.Editor({toolbars: [[ 'source',
                                'bold', 'italic', 'underline', 'autotypeset', '|', 'pasteplain',  'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|',
                               'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                                'link', 'unlink', 'insertimage', 'attachment', '|',
                                'snapscreen', 'wordimage'
                               ]] });
                               editor_Editor1.render('Editor1');
                               function getEditContent(){
                                    var s=editor_Editor1.getContent();
                                    alert(s);
                                    //setTimeout("document.getElementById('Editor1_txtEditorContent').value=s;",1000);
                               }
                              // setTimeout("getEditContent();",1000);
                               </script>


                        </dd>
                    </dl>
                </fieldset>            
                 
                 
                <fieldset style="display: none;">
                    <dl>
                        <dt>自设内容项目数：</dt>
                        <dd><select name="ddlCustomContentCount" id="ddlCustomContentCount" onChange="SelectChange();">
	<option selected="selected" value="1">1</option>

	<option value="2">2</option>
	<option value="3">3</option>
	<option value="4">4</option>
	<option value="5">5</option>
	<option value="6">6</option>
	<option value="7">7</option>

	<option value="8">8</option>
	<option value="9">9</option>
	<option value="10">10</option>

</select></dd>
                     </dl>
                     <dl>
                        <dt>自设内容1：<br /><input name="chkIsHtmlEditor1" type="checkbox" id="chkIsHtmlEditor1" value="1" onclick="ChangeEditor(1,this.checked)" /><label for="chkIsHtmlEditor1">支持HTML</label></dt>

                        <dd id="dd1"><textarea name="txtCustomContent1" rows="2" cols="20" id="txtCustomContent1" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom2" style="display:none">
                        <dt>自设内容2：<br /><input name="chkIsHtmlEditor2" type="checkbox" id="chkIsHtmlEditor2" value="1" onclick="ChangeEditor(2,this.checked)" /><label for="chkIsHtmlEditor2">支持HTML</label></dt>
                        <dd id="dd2"><textarea name="txtCustomContent2" rows="2" cols="20" id="txtCustomContent2" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom3" style="display:none">
                        <dt>自设内容3：<br /><input name="chkIsHtmlEditor3" type="checkbox" id="chkIsHtmlEditor3" value="1" onclick="ChangeEditor(3,this.checked)" /><label for="chkIsHtmlEditor3">支持HTML</label></dt>

                        <dd id="dd3"><textarea name="txtCustomContent3" rows="2" cols="20" id="txtCustomContent3" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom4" style="display:none">
                        <dt>自设内容4：<br /><input name="chkIsHtmlEditor4" type="checkbox" id="chkIsHtmlEditor4" value="1" onclick="ChangeEditor(4,this.checked)" /><label for="chkIsHtmlEditor4">支持HTML</label></dt>
                        <dd id="dd4"><textarea name="txtCustomContent4" rows="2" cols="20" id="txtCustomContent4" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom5" style="display:none">
                        <dt>自设内容5：<br /><input name="chkIsHtmlEditor5" type="checkbox" id="chkIsHtmlEditor5" value="1" onclick="ChangeEditor(5,this.checked)" /><label for="chkIsHtmlEditor5">支持HTML</label></dt>

                        <dd id="dd5"><textarea name="txtCustomContent5" rows="2" cols="20" id="txtCustomContent5" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom6" style="display:none">
                        <dt>自设内容6：<br /><input name="chkIsHtmlEditor6" type="checkbox" id="chkIsHtmlEditor6" value="1" onclick="ChangeEditor(6,this.checked)" /><label for="chkIsHtmlEditor6">支持HTML</label></dt>
                        <dd id="dd6"><textarea name="txtCustomContent6" rows="2" cols="20" id="txtCustomContent6" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom7" style="display:none">
                        <dt>自设内容7：<br /><input name="chkIsHtmlEditor7" type="checkbox" id="chkIsHtmlEditor7" value="1" onclick="ChangeEditor(7,this.checked)" /><label for="chkIsHtmlEditor7">支持HTML</label></dt>

                        <dd id="dd7"><textarea name="txtCustomContent7" rows="2" cols="20" id="txtCustomContent7" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom8" style="display:none">
                        <dt>自设内容8：<br /><input name="chkIsHtmlEditor8" type="checkbox" id="chkIsHtmlEditor8" value="1" onclick="ChangeEditor(8,this.checked)" /><label for="chkIsHtmlEditor8">支持HTML</label></dt>
                        <dd id="dd8"><textarea name="txtCustomContent8" rows="2" cols="20" id="txtCustomContent8" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom9" style="display:none">
                        <dt>自设内容9：<br /><input name="chkIsHtmlEditor9" type="checkbox" id="chkIsHtmlEditor9" value="1" onclick="ChangeEditor(9,this.checked)" /><label for="chkIsHtmlEditor9">支持HTML</label></dt>

                        <dd id="dd9"><textarea name="txtCustomContent9" rows="2" cols="20" id="txtCustomContent9" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom10" style="display:none">
                        <dt>自设内容10：<br /><input name="chkIsHtmlEditor10" type="checkbox" id="chkIsHtmlEditor10" value="1" onclick="ChangeEditor(10,this.checked)" /><label for="chkIsHtmlEditor10">支持HTML</label></dt>
                        <dd id="dd10"><textarea name="txtCustomContent10" rows="2" cols="20" id="txtCustomContent10" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom11" style="display:none">
                        <dt>自设内容11：<br /><input name="chkIsHtmlEditor11" type="checkbox" id="chkIsHtmlEditor11" value="1" onclick="ChangeEditor(11,this.checked)" /><label for="chkIsHtmlEditor11">支持HTML</label></dt>

                        <dd id="dd11"><textarea name="txtCustomContent11" rows="2" cols="20" id="txtCustomContent11" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom12" style="display:none">
                        <dt>自设内容12：<br /><input name="chkIsHtmlEditor12" type="checkbox" id="chkIsHtmlEditor12" value="1" onclick="ChangeEditor(12,this.checked)" /><label for="chkIsHtmlEditor12">支持HTML</label></dt>
                        <dd id="dd12"><textarea name="txtCustomContent12" rows="2" cols="20" id="txtCustomContent12" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom13" style="display:none">
                        <dt>自设内容13：<br /><input name="chkIsHtmlEditor13" type="checkbox" id="chkIsHtmlEditor13" value="1" onclick="ChangeEditor(13,this.checked)" /><label for="chkIsHtmlEditor13">支持HTML</label></dt>

                        <dd id="dd13"><textarea name="txtCustomContent13" rows="2" cols="20" id="txtCustomContent13" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom14" style="display:none">
                        <dt>自设内容14：<br /><input name="chkIsHtmlEditor14" type="checkbox" id="chkIsHtmlEditor14" value="1" onclick="ChangeEditor(14,this.checked)" /><label for="chkIsHtmlEditor14">支持HTML</label></dt>
                        <dd id="dd14"><textarea name="txtCustomContent14" rows="2" cols="20" id="txtCustomContent14" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom15" style="display:none">
                        <dt>自设内容15：<br /><input name="chkIsHtmlEditor15" type="checkbox" id="chkIsHtmlEditor15" value="1" onclick="ChangeEditor(15,this.checked)" /><label for="chkIsHtmlEditor15">支持HTML</label></dt>

                        <dd id="dd15"><textarea name="txtCustomContent15" rows="2" cols="20" id="txtCustomContent15" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom16" style="display:none">
                        <dt>自设内容16：<br /><input name="chkIsHtmlEditor16" type="checkbox" id="chkIsHtmlEditor16" value="1" onclick="ChangeEditor(16,this.checked)" /><label for="chkIsHtmlEditor16">支持HTML</label></dt>
                        <dd id="dd16"><textarea name="txtCustomContent16" rows="2" cols="20" id="txtCustomContent16" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom17" style="display:none">
                        <dt>自设内容17：<br /><input name="chkIsHtmlEditor17" type="checkbox" id="chkIsHtmlEditor17" value="1" onclick="ChangeEditor(17,this.checked)" /><label for="chkIsHtmlEditor17">支持HTML</label></dt>

                        <dd id="dd17"><textarea name="txtCustomContent17" rows="2" cols="20" id="txtCustomContent17" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom18" style="display:none">
                        <dt>自设内容18：<br /><input name="chkIsHtmlEditor18" type="checkbox" id="chkIsHtmlEditor18" value="1" onclick="ChangeEditor(18,this.checked)" /><label for="chkIsHtmlEditor18">支持HTML</label></dt>
                        <dd id="dd18"><textarea name="txtCustomContent18" rows="2" cols="20" id="txtCustomContent18" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom19" style="display:none">
                        <dt>自设内容19：<br /><input name="chkIsHtmlEditor19" type="checkbox" id="chkIsHtmlEditor19" value="1" onclick="ChangeEditor(19,this.checked)" /><label for="chkIsHtmlEditor19">支持HTML</label></dt>

                        <dd id="dd19"><textarea name="txtCustomContent19" rows="2" cols="20" id="txtCustomContent19" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                    <dl id="dlCustom20" style="display:none">
                        <dt>自设内容20：<br /><input name="chkIsHtmlEditor20" type="checkbox" id="chkIsHtmlEditor20" value="1" onclick="ChangeEditor(20,this.checked)" /><label for="chkIsHtmlEditor20">支持HTML</label></dt>
                        <dd id="dd20"><textarea name="txtCustomContent20" rows="2" cols="20" id="txtCustomContent20" style="height:150px;width:600px;"></textarea></dd>          
                    </dl>
                     
                    <div style="clear:left"></div>
                 </fieldset>

            </div>
            <div class="Submit">
                <input type="submit" name="btnHidAction" value="" id="btnHidAction" style="display:none" />
                <input type="button" name="btnEdit" value="修改" id="btnEdit" class="subButton" onclick="getEditContent();" />
                <input type="submit" name="btnDel" value="删除" onclick="selfconfirm({msg:'确定要执行删除操作吗？',fn:function(data){setAction1(data)}});return false;" id="btnDel" class="subButton" />
                <input type="button" name="Submit422" Class="subButton" value="返回" onclick='location.href="ColumnManage.aspx?NodeCode=100015001";'>
            </div>
        </div>
        </div>

    
<script>$(function(){showTotal=1});</script></form>
</body>
</html>
