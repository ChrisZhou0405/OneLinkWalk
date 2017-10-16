<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.vip.index" %>

<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" ID="Meta" />
</head>
<body>
    <form id="form1" runat="server">
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->
        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <div class="title">会员专区 </div>
                <div class="picScroll4 VIPc">
                    <div class="hd"></div>
                    <div class="bd">
                        <span class="next1"></span>
                        <span class="prev1"></span>
                        <ul>
                            <li><a href="#">
                                <img src="../images/vban1.jpg" alt=""></a></li>
                            <li><a href="#">
                                <img src="../images/vban2.jpg" alt=""></a></li>
                        </ul>
                    </div>
                </div>
                <!-- picScroll 4 end -->
                <div class="loginBox">
                    <div class="loginTxt">
                        <h2>会员登录</h2>
                        <p>登录名：<input type="text" placeholder="会员卡号 / 手机号 / 邮箱" name=""></p>
                        <p>登录密码：<input type="text" placeholder="请输入密码" name=""></p>
                        <p>
                            <span class="checkPanel on">
                                <input type="checkbox" name="" value="记住登录状态" checked="checked"></span>
                            &nbsp;&nbsp;记住登录状态
            <a href="#">忘记登录密码？</a>
                        </p>
                        <p>
                            <input type="button" value="登录" name="">
                        </p>

                    </div>
                    <a href="#" class="btnmember">成为会员</a>
                </div>
                <!-- loginBox end -->
                <div class="titImg">
                    <img src="../images/vpic.png" alt="">
                    VIP的专属会籍礼遇，令会员在万菱汇的消费玩乐体验更无与伦比。只需于一年内维持所需消费金额，会员便可于下一年继续享有该会籍级别之礼遇。
                </div>
                <ul class="vList">
                    <li class="first"><span>权益</span><i>会员卡</i></li>
                    <li><span>入会后首次购物积分即可获赠专属礼一份</span><i><img src="../images/ico/ok.png" alt=""></i></li>
                    <li><span>持卡在指定店铺享受专属折扣及积分优惠</span><i><img src="../images/ico/ok.png" alt=""></i></li>
                    <li><span>凭积分可参与不定期的积分兑礼等专享会员活动</span><i><img src="../images/ico/ok.png" alt=""></i></li>
                    <li><span>停车优惠</span><i>200 积分 2小时</i></li>
                    <li><span>生日礼遇（当月）</span><i>双倍积分</i></li>

                </ul>

                <!-- loginBox end -->
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <div class="overbg"></div>
        <div class="lMap">
            <h1 class="close"></h1>
            <img src="../images/l_map.png" alt="">
        </div>
        <!--lMap end-->

        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
        </script>
    </form>
</body>
</html>
