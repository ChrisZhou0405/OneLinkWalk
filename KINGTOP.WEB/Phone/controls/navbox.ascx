<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbox.ascx.cs" Inherits="KingTop.WEB.Phone.controls.navbox" %>
<div class="navbox" id="navbox">
   <div class="navbox-con">
      <div class="nav-l">
          <ul>
              <li>购物指南</li>
              <li>美食荟萃</li>
              <li>活动推介</li>
              <li>会员专区</li>
              <li>服务与设施</li>
              <li>关于我们</li>
              <li><a href="#"><img src="/Phone/images/icon5.png" width="28"> 微信</a></li>
              <li><a href="/Phone/about/contact.aspx"><img src="/Phone/images/icon6.png" width="22"> 邮箱</a></li>
          </ul>
      </div>
      <div class="nav-r">
         <div class="nav_tab">
             <ul>
                <li><a href="/Phone/shopping/index.aspx?id=100000000188449">服饰及鞋履</a></li>
                <li><a href="/Phone/shopping/index.aspx?id=100000000896784">珠宝及配饰</a></li>
                <li><a href="/Phone/shopping/index.aspx?id=100000001531614">生活及服务</a></li>
                <li><a href="/Phone/shopping/index.aspx?id=100000002263254">美容及护理</a></li>
                <li><a href="/Phone/shopping/index.aspx?id=100000002948178">童装玩具</a></li>
                  
             </ul>           
         </div>
         
         <div class="nav_tab">
             <ul>
                <li><a href="/Phone/cate/index.aspx?id=100000000144283">亚洲美食</a></li>
                <li><a href="/Phone/cate/index.aspx?id=100000000825845">中式佳肴</a></li>
                <li><a href="/Phone/cate/index.aspx?id=100000001534222">西方美馔</a></li>
                <li><a href="/Phone/cate/index.aspx?id=100000002274564">轻便美食/甜点</a></li>
             </ul>           
         </div>
         
         <div class="nav_tab">
             <ul>
                <li><a href="/Phone/activity/index.aspx">最新活动</a></li>
                <li><a href="/Phone/activity/activity.aspx">活动日志</a></li>
             </ul>           
         </div>
         
         <div class="nav_tab">
             <ul>
                <li><a href="/Phone/vip/vip.aspx">会员招募</a></li>
               <%-- <li><a href="vip/index.html">会员登入</a></li>
                <li><a href="vip/integral.html">会员专属优惠</a></li>--%>
             </ul>           
         </div>
         <div class="nav_tab">
             <ul>
                <li><a href="/Phone/service/index.aspx">咨询中心</a></li>
                <li><a href="/Phone/service/parking.aspx">停车服务</a></li>
                <li><a href="/Phone/service/ATM.aspx">ATM设施</a></li>
                <li><a href="/Phone/service/EVC.aspx">电动汽车充电</a></li>
                <li><a href="/Phone/service/rooms.aspx">母婴室</a></li>
             </ul>           
         </div>
         
         <div class="nav_tab">
             <ul>
                <li><a href="/Phone/about/index.aspx">项目概述</a></li>
                <li><a href="/Phone/about/ways.aspx">到达方式</a></li>
                <li><a href="/Phone/about/contact.aspx">联系我们</a></li>
                <li><a href="/Phone/about/rent.aspx">场地与商铺租赁</a></li>
             </ul>           
         </div>
           <div class="nav_tab">
            
            <img  src="/Phone/images/weixin_code.jpg"/>
                 
         </div>
      
      </div>     
   </div>
</div>

<div class="schbox">
    <input id="searchkey" name="" type="text" class="schtxt1" placeholder="请输入关键字"><input name="" type="button" value="搜  索" class="schbtn1" onclick="GoSearchUrl()" >
</div>

<header class="header">
   <h1><a href="/Phone/index.aspx"><img src="/Phone/images/logo.png"></a></h1>
  <%-- <span class="lang"><a href="#">中文</a> / <a href="#">EN</a></span>--%>
   <span class="schbtn" id="sch"></span>
   <span class="menubtn" id="menu">
     <b></b>
     <b></b>
     <b></b>
   </span>
</header>
<script type="text/javascript">
    function Trim(str, is_global) {
        var result;
        result = str.replace(/(^\s+)|(\s+$)/g, "");
        if (is_global.toLowerCase() == "g") {
            result = result.replace(/\s/g, "");
        }
        return result;
    }
    function GoSearchUrl() {
        var searchinput = document.getElementById("searchkey").value;
        searchinput = Trim(searchinput, "g");
        var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）·—|{}【】‘’；：”“'。，、？]")
        if (pattern.test(searchinput.value)) {
            alert("您输入的内容存在特殊字符!");
            return false;
        }
        var cid = "{@cid,false,0}";
        window.location = "/Phone/aid/search.aspx?key=" + escape(stripscript(searchinput));
        return true;
    }
    function entersearch() {
        var event = window.event || arguments.callee.caller.arguments[0];
        if (event.keyCode == 13) {
            GoSearchUrl();
        }
    }
    function checkComments() {
        var event = window.event || arguments.callee.caller.arguments[0];
        if ((event.keyCode > 32 && event.keyCode < 48) ||
             (event.keyCode > 57 && event.keyCode < 65) ||
             (event.keyCode > 90 && event.keyCode < 97)
             ) {
            event.returnValue = false;
        }
    }
    function stripscript(s) {
        var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）·—|{}【】‘’；：”“'。，、？]")
        var rs = "";
        for (var i = 0; i < s.length; i++) {
            rs = rs + s.substr(i, 1).replace(pattern, '');
        }
        return rs;
    }
</script>