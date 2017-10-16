<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="KingTop.WEB.SysAdmin.upfiles.showimage.default2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
<style>
body {background:#FFFFFF;margin:5px 0;font:12px Verdana, Arial, Tahoma;text-align:center;vertical-align:middle;color:#FFFFFF}
img {border:none}
.txt_1 {font:bold 24px Verdana, Tahoma;color:#fff}
img.thumb_img {cursor:pointer;display:block;margin-bottom:10px}
img#main_img {cursor:pointer;display:block;}
#gotop {cursor:pointer;display:block;}
#gobottom {cursor:pointer;display:block;}
#showArea {height:355px;margin:10px;overflow:hidden}
.info {color:#666;font:normal 9px Verdana;margin-top:20px}
.info a:link, .info a:visited {color:#666;text-decoration:none}
.info a:hover {color:#fff;text-decoration:none}
</style>
</head>
<body>
<table width="800" border="0" align="center" cellpadding="0" cellspacing="5">
  <tr>
    <td width="670" align="center" style="border:1px solid #CCCCCC;padding:5px;"><%=dt %></td>
    <td width="130" align="center" valign="top">
    <img src="images/gotop.gif" width="100" height="14" id="gotop" />
    <div id="showArea">
        <%=sltlist %>
    </div>
    <img src="images/gobottom.gif" width="100" height="14" id="gobottom" /></td>
  </tr>
</table>

</body>
</html>
<script language="javascript" type="text/javascript">
    function $(e) { return document.getElementById(e); }
    document.getElementsByClassName = function(cl) {
        var retnode = [];
        var myclass = new RegExp('\\b' + cl + '\\b');
        var elem = this.getElementsByTagName('*');
        for (var i = 0; i < elem.length; i++) {
            var classes = elem[i].className;
            if (myclass.test(classes)) retnode.push(elem[i]);
        }
        return retnode;
    }
    var MyMar;
    var speed = 1; //速度，越大越慢
    var spec = 1; //每次滚动的间距, 越大滚动越快
    var ipath = 'images/'; //图片路径
    var thumbs = document.getElementsByClassName('thumb_img');
    for (var i = 0; i < thumbs.length; i++) {
        thumbs[i].onmouseover = function() { $('main_img').src = this.src; };
        thumbs[i].onclick = function() { $('main_img').src = this.src; }
    }
    $('main_img').onclick = function() { $('main_img').src = this.src;  }
    $('gotop').onmouseover = function() { this.src = ipath + 'gotop2.gif'; MyMar = setInterval(gotop, speed); }
    $('gotop').onmouseout = function() { this.src = ipath + 'gotop.gif'; clearInterval(MyMar); }
    $('gobottom').onmouseover = function() { this.src = ipath + 'gobottom2.gif'; MyMar = setInterval(gobottom, speed); }
    $('gobottom').onmouseout = function() { this.src = ipath + 'gobottom.gif'; clearInterval(MyMar); }
    function gotop() { $('showArea').scrollTop -= spec; }
    function gobottom() { $('showArea').scrollTop += spec; }
</script>
