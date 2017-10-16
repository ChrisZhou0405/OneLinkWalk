<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="KingTop.WEB.Phone.about.contact" %>

<%@ Register Src="~/Phone/controls/navbox.ascx" TagPrefix="uc1" TagName="navbox" %>
<%@ Register Src="~/Phone/controls/footer.ascx" TagPrefix="uc1" TagName="footer" %>



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
     <div class="curr"><h3>联系我们</h3></div>     
     <div class="contact">
         <div class="con lxus">
               <%=KingTop.Common.Tools.GetSinglePageContent("101009007002")%>
          </div>
          
        <div class="con msg">
          <input id="Title" name="" type="text" placeholder="您的姓名" class="msgtxt1">
          <input id="Email" name="" type="text" placeholder="您的邮箱" class="msgtxt1">  
          <textarea id="Content" name="" cols="" rows="" class="msgtxt1 msgtxt2">请填写您的留言...</textarea> 
          <input id="btnS" name="" type="button" value="提 交" class="msgbtn1">       
        </div>
     </div>
       
  </div> 
  

</section>

    <uc1:footer runat="server" ID="footer" />

<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>
<script src="/Phone/js/TouchSlide.1.0.js"></script>
<script>
    TouchSlide({
        slideCell: "#focus1",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: false,//自动播放
        autoPage: true //自动分页
    });

</script>
 </body>
</html>

  <script type="text/javascript">

      $(function () {
          btnClick();
      });

  

      function btnClick()
      {
          $("#btnS").click(function () {
              var Title = $("#Title").val().trim();
              var Email = $("#Email").val().trim();
              var Content = $("#Content").val().trim();
              var em = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;

              if (Title == "") {
                  alert("请输入您的姓名！");
                  $("#Title").focus();
              } else if (Email == "" || !em.test(Email)) {
                  alert("请输入正确的邮箱！");
                  $("#Email").focus();

              } else if (Content == "" || Content == "请填写您的留言...") {
                  alert("请输入您的留言！");
                  $("#Content").focus();
              } else {
                  
                  $.get("/Phone/about/message.ashx/", { Title: Title, Email: Email, Content: Content },
                  function (data) {
                      var dataObj = eval("(" + data + ")");//转换为json对象
                      if (dataObj.statu == "ok") {
                          clear();
                          alert("提交成功");

                      } else {
                          alert("提交失败");
                      }
                  });
            
              }


          })

          }
      function clear() {
          $("#Title").val("");
          $("#Email").val("");
          $("#Content").val("请填写您的留言...");

      }
   </script>
