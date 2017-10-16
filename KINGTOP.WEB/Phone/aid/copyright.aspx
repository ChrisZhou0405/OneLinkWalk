<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="copyright.aspx.cs" Inherits="KingTop.WEB.Phone.aid.copyright" %>

<%@ Register Src="~/Phone/controls/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/Phone/controls/navbox.ascx" TagPrefix="uc1" TagName="navbox" %>



<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="320">
<meta name="format-detection" content="telephone=no" />
<link rel="stylesheet" type="text/css" href="/Phone/css/style.css">
<title></title>
</head>
<body>

    <uc1:navbox runat="server" ID="navbox" />

<section>
  <div class="mmain bgwhite">
     <div class="curr"><h3>版权声明</h3></div>     
     
     <ul class="copyrightlist">
         <%=KingTop.Common.Tools.GetSinglePageContent("101008002")%>
     </ul>
       
  </div> 
  

</section>


    <uc1:footer runat="server" ID="footer" />

<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>

 </body>
</html>
