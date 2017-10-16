<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.En.vip.index" %>

<%@ Register Src="~/En/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/En/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/En/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




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
                <div class="title">Members Only </div>
                <div class="picScroll4 VIPc">
                    <div class="hd"></div>
                    <div class="bd">
                        <span class="next1"></span>
                        <span class="prev1"></span>
                        <ul>
                            <li><a href="#">
                                <img src="/En/images/vban1.jpg" alt=""></a></li>
                            <li><a href="#">
                                <img src="/En/images/vban2.jpg" alt=""></a></li>
                        </ul>
                    </div>
                </div>
                <!-- picScroll 4 end -->
                <div class="loginBox">
                    <div class="loginTxt">
                        <h2>Member Login</h2>
                        <p>log-in name：<input type="text" placeholder="membershipCard / Phone / Mailbox" name=""></p>
                        <p>login password：<input type="text" placeholder="Please enter a password" name=""></p>
                        <p>
                            <span class="checkPanel on">
                                <input type="checkbox" name="" value="remember login status" checked="checked"></span>
                            &nbsp;&nbsp;remember login status
            <a href="#">forgot your password？</a>
                        </p>
                        <p>
                            <input type="button" value="log in" name="">
                        </p>

                    </div>
                    <a href="#" class="btnmember">become member</a>
                </div>
                <!-- loginBox end -->
                <div class="titImg">
                    <img src="/En/images/vpic.png" alt="">
                   VIP's exclusive membership privileges, so that members of the Lingling consumer spending more unparalleled experience. In order to maintain the required amount of consumption within one year, Members will continue to enjoy the privileges of the membership level in the following year.
                </div>
                <ul class="vList">
                    <li class="first"><span>rights and interests</span><i>membership card</i></li>
                    <li><span>After the first purchase points to receive exclusive gift of a gift</span><i><img src="/En/images/ico/ok.png" alt=""></i></li>
                    <li><span>Card holders enjoy exclusive discounts and bonus points at designated stores</span><i><img src="/En/images/ico/ok.png" alt=""></i></li>
                    <li><span>With points can participate in occasional bonus points and other members enjoy exclusive activities</span><i><img src="/En/images/ico/ok.png" alt=""></i></li>
                    <li><span>Parking offers</span><i>200 points for 2 hours</i></li>
                    <li><span>Birthday Benefits (current month)</span><i>Double integral</i></li>

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
            <img src="/En/images/l_map.png" alt="">
        </div>
        <!--lMap end-->

        <script src="/En/js/jquery.js" type="text/javascript"></script>
        <script src="/En/js/jqnav.js" type="text/javascript"></script>
        <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
        </script>
    </form>
</body>
</html>
