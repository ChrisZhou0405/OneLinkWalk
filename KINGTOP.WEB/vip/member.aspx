<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="member.aspx.cs" Inherits="KingTop.WEB.vip.member" %>

<%@ Register Src="~/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" ID="Meta" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Header runat="server" ID="Header" />
        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <div class="title">会员专区 </div>
                <div class="tab_tit atit"><a class="t_on" href="javascript:;">会员积分</a><a href="javascript:;">换领礼品</a><a href="javascript:;">精选优惠 · 会员权益</a><a href="javascript:;">条款及细则</a></div>

                <div class="tab_txt" style="display: block;">
                    <div class="integral">
                        <div class="cardpic">
                            <img src="../images/card2.png" alt="">
                        </div>
                        <div class="myInfro">
                            <h1>我的信息</h1>
                            <ul>
                                <li>姓名： 万菱汇会员
                                    <input type="button" value="修改" name=""></li>
                                <li class="myico01">电话： 18888888888
                                    <input type="button" value="修改" name=""></li>
                                <li class="myico02">邮箱： Onelinkwalk@mail.com
                                    <input type="button" value="修改" name=""></li>
                                <li class="myico03">卡号： 100646 </li>
                                <li class="myico04">我的积分: <span><i>2</i><i>2</i><i>8</i><i>7</i></span>分</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- tab_txt 0 end  -->
                <div class="tab_txt">
                    <div class="suprCard">
                        <img src="../images/exchange.jpg" alt="">
                    </div>
                    <h1 class="myintegral">我的积分: <span><i>2</i><i>2</i><i>8</i><i>7</i></span>分</h1>
                    <div class="filtrate">
                        <h2>礼品筛选 (共筛选到 26 款礼品)</h2>
                        <p id="fg">礼品分类 : <i class="ion">不限</i><i>护肤品</i><i>小家电</i><i>优惠券</i><i>记事本</i><i>餐饮</i><i>电影票</i><i>现金券</i></p>
                        <p id="fg2" class="nobor">积分数量 : <i class="ion">不限</i><i>0 - 1000</i><i>1000 - 5000</i><i>5000 - 10000</i><i>10000 - 20000</i><i>20000 - 30000</i><i>30000 - 40000</i></p>
                    </div>
                    <ul class="fgList">
                        <li><a href="#">
                            <img src="../images/epic.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-02.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-03.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-04.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-05.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-06.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-07.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-08.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-09.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-10.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-11.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-12.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-13.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-14.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-15.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                        <li><a href="#">
                            <img src="../images/epic-16.jpg" alt=""></a>
                            <h3>OCE 200元现金 Coupon</h3>
                            <span>积分： <i>2500</i></span>
                            <span>兑换地点：<i>小中庭客服中心</i></span>
                        </li>
                    </ul>

                </div>
                <!-- tab_txt 1 end  -->
                <div class="tab_txt" style="display: block;" id="ban_no">
                    <div class="ban">
                        <div class="bd">
                            <ul>
                                <li><a href="#" style="background: url(../images/privilege.jpg) 50% no-repeat; background-size: cover;"></a></li>
                                <li><a href="#" style="background: url(../images/banner2.jpg) 50% no-repeat; background-size: cover;"></a></li>
                                <li><a href="#" style="background: url(../images/banner3.jpg) 50% no-repeat; background-size: cover;"></a></li>
                            </ul>
                        </div>
                        <div class="hd">
                            <ul>
                                <li></li>
                                <li></li>
                                <li></li>
                            </ul>
                        </div>
                        <a class="prev"></a><a class="next"></a>
                    </div>
                    <!-- ban end -->
                    <div class="picScroll6">
                        <div class="hd"></div>
                        <div class="bd">
                            <span class="next2"></span>
                            <span class="prev2"></span>
                            <ul>
                                <li><a href="#">
                                    <img src="../images/privilegeC.jpg" alt=""><span>年度 9 折优惠</span></a></li>
                                <li><a href="#">
                                    <img src="../images/privilegeC-02.jpg" alt=""><span>品牌 8.5 折优惠</span></a></li>
                                <li><a href="#">
                                    <img src="../images/privilegeC-03.jpg" alt=""><span>尊享 7 折优惠</span></a></li>
                                <li><a href="#">
                                    <img src="../images/ac_pic-02.jpg" alt=""><span>尊享 7 折优惠</span></a></li>
                            </ul>
                        </div>
                    </div>
                    <!-- picScroll 2 end -->
                    <div class="titImg mt23">
                        <img src="../images/bt.png" alt="">
                        万菱汇会员在以下指定商户享受专属折扣优惠（以万菱汇现场最终公示为准）
                    </div>
                    <ul class="vList bList">
                        <li class="first"><span>品牌</span><i>折扣</i></li>
                        <li>
                            <img src="../images/blogo.jpg" alt="">
                            <span><a href="#">BALLY</a>位置：B1-301</span><i>9 折</i></li>
                        <li>
                            <img src="../images/blogo-02.jpg" alt="">
                            <span><a href="#">ZARA</a>位置：B1-301</span><i>8.5 折</i></li>
                        <li>
                            <img src="../images/blogo-03.jpg" alt="">
                            <span><a href="#">MANGO</a>位置：B1-301</span><i>8.5 折</i></li>
                        <li>
                            <img src="../images/blogo-04.jpg" alt="">
                            <span><a href="#">MANGO</a>位置：B1-301</span><i>8.5 折</i></li>
                        <li>
                            <img src="../images/blogo-05.jpg" alt="">
                            <span><a href="#">MO&Co</a>位置：B1-301</span><i>7.5 折</i></li>
                        <li>
                            <img src="../images/blogo-06.jpg" alt="">
                            <span><a href="#">OCE</a>位置：B1-301</span><i>9 折</i></li>
                        <li>
                            <img src="../images/blogo-07.jpg" alt="">
                            <span><a href="#">佰草集</a>位置：B1-301</span><i>9 折</i></li>
                    </ul>
                </div>
                <!-- tab_txt 2 end  -->
                <div class="tab_txt">
                    <div class="clause">
                        <h4>条款及细则</h4>
                        <asp:Repeater ID="rpttabcon" runat="server">
                            <ItemTemplate>
                                <a <%#Container.ItemIndex + 1 == 1?"class='cOn'":"" %> href="javascript:;"><%#Eval("NodeName") %></a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="clauseTxt" style="display: block">
                        <%=KingTop.Common.Tools.GetSinglePageContent("101005003004001")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("101005003004002")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("101005003004003")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("101005003004004")%>
                    </div>
                    <div class="clauseTxt">
                        <%=KingTop.Common.Tools.GetSinglePageContent("101005003004005")%>
                    </div>
                </div>
                <!-- tab_txt 3 end  -->
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            jQuery(".ban").slide({ mainCell: ".bd ul", autoPlay: true, effect: "fold", pnLoop: false });
            jQuery(".picScroll6").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 3, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
    </form>
</body>
</html>
