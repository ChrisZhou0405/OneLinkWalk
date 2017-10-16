<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.Phone.vip.index" %>

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
<link rel="stylesheet" type="text/css" href="../css/style.css">
<title></title>
</head>
<body>
    <uc1:navbox runat="server" ID="navbox" />


<section>
  <div class="mmain bgwhite">
     <div class="curr"><h3>会员专区</h3></div>
        <div class="vip">
          <div id="focus1" class="focus focus2" style="margin:0">
            <div class="hd">
                <ul></ul>
            </div>
            <div class="bd">
                <ul>
                <li><a href="#"><div class="txt"><img src="../images/vpic2.png"><h3>会员卡</h3><p>非会员当日累计消费满300元可免费办理。</p><p>会员卡办理须以购物小票及本人有效身份证件（身份证、护照、驾驶执照、军官证等）为凭证，到首层小中庭客户服务中心即可办理。</p></div></a></li>
                
                <li><a href="#"><div class="txt"><img src="../images/vpic2.png"><h3>会员卡</h3><p>非会员当日累计消费满300元可免费办理。</p>
                <p>会员卡办理须以购物小票及本人有效身份证件（身份证、护照、驾驶执照、军官证等）为凭证，到首层小中庭客户服务中心即可办理。</p></div></a></li>
                
                <li><a href="#">
                <div class="txt"><img src="../images/vpic2.png">
                <h3>会员卡</h3><p>非会员当日累计消费满300元可免费办理。</p>
                <p>会员卡办理须以购物小票及本人有效身份证件（身份证、护照、驾驶执照、军官证等）为凭证，到首层小中庭客户服务中心即可办理。</p></div></a></li>
                </ul>
            </div>
            <span class="prev"></span>
            <span class="next"></span>
          </div> 
          
          <div class="login">
             <h2>会员登录</h2>
             <p>登录名：</p>
             <input name="" type="text" placeholder="会员卡号 / 手机号 / 邮箱" class="lgtxt1"> 
             <p>登录密码：</p>   
             <input name="" type="text" placeholder="请输入密码" class="lgtxt1">
             <p><a href="#" class="fr">忘记登录密码？</a><input name="" type="checkbox" value="" class="lgtxt2"> 记住登录状态</p> 
             <input name="" type="button" value="登录" class="loginbtn">   
          </div>
          <a href="#" class="loginbtn loginbtn2">成为会员</a>
           
           <div class="vip-rights">
             <h2>会员<br>权益</h2>
             <p>VIP的专属会籍礼遇，令会员在万菱汇的消费玩乐体验更无与伦比。只需于一年内维持所需消费金额，会员便可于下一年继续享有该会籍级别之礼遇。</p>
             <table width="100%" border="0" cellspacing="0" cellpadding="0" class="viptable">
              <tr>
                <th class="spa">权益</th>
                <th width="30%">会员卡</th>
              </tr>
              <tr>
                <td class="spa">入会后首次购物积分即可获赠专属礼一份</td>
                <td><img src="../images/okicon1.png"></td>
              </tr>
              <tr>
                <td class="spa">持卡在指定店铺享受专属折扣及积分优惠</td>
                <td><img src="../images/okicon1.png"></td>
              </tr>
               <tr>
                <td class="spa">凭积分可参与不定期的积分兑礼等专享会员活动</td>
                <td><img src="../images/okicon1.png"></td>
              </tr>
               <tr>
                <td class="spa">停车优惠</td>
                <td>200 积分 2小时</td>
              </tr>
               <tr>
                <td class="spa">生日礼遇（当月）</td>
                <td>双倍积分</td>
              </tr>
            </table>

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
