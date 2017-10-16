<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site.aspx.cs" Inherits="KingTop.WEB.En.shopping.site" %>

<%@ Register Src="/En/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="/En/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="/En/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" ID="Meta" />
    <link rel="stylesheet" type="text/css" href="/En/css/style.css" />
    <link rel="stylesheet" type="text/css" href="/En/css/jquery.mCustomScrollbar.css" />
       <script src="/En/js/jquery.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->
        <!--main start-->
        <div class="mains mt148">
            <div class="wrap">
                <div class="mLeft">
                    <h1>购物指南</h1>
                    <ul class="sideBar">
                        <li><a class="si_1" href="javascript:;">按类别</a>
                            <div class="class_a">
                                <a href="/En/shopping/index.aspx" class="c_a0 ca_on0">全部</a>
                                <asp:Repeater ID="RptRelated" runat="server">
                                    <ItemTemplate>
                                        <a href="/En/shopping/index.aspx?id=<%#Eval("ID") %>" class="c_a<%#Container.ItemIndex+1 %>"><%#Eval("Title")%></a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </li>
                        <li><a class="si_2" href="javascript:;">按字母</a>
                            <div class="letter">
                                <span>
                                    <h2>A - D</h2>
                                    <p><i onclick="javascript:location='/En/shopping/index.aspx?Loacation=1'">A</i><i onclick="javascript:location='/En/shopping/index.aspx?Loacation=2'">B</i><i onclick="javascript:location='/En/shopping/index.aspx?Loacation=3'">C</i><i onclick="javascript:location='/En/index.aspx?Loacation=4'">D</i></p>
                                </span>
                                <span>
                                    <h2>E - H</h2>
                                    <p>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=5'">E</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=6'">F</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=7'">G</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=8'">H</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>I - L</h2>
                                    <p>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=9'">I</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=10'">J</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=11'">K</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=12'">L</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>M - P</h2>
                                    <p>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=13'">M</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=14'">N</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=15'">O</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=16'">P</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>Q - T</h2>
                                    <p>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=17'">Q</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=18'">R</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=19'">S</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=20'">T</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>U - x</h2>
                                    <p>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=21'">U</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=22'">V</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=23'">W</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=24'">X</i>
                                    </p>
                                </span>
                                <span>
                                    <h2>Y - z</h2>
                                    <p>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=25'">Y</i>
                                        <i onclick="javascript:location='/En/shopping/index.aspx?Loacation=26'">Z</i>
                                    </p>
                                </span>
                            </div>
                        </li>
                        <li  class="s_on"><a class="si_3" href="javascript:;">按位置</a>
                            <div class="site"  style="display: block;">
                     <%--      init(num, 0, "2", "lc_f_id2");--%>
                        <a  id="0"  href="javascript:" onmousedown="init(0,<%=this.getDt("0",dtdata0) %>,'2','lc_f_id2')">B1</a>
                        <a id="1"  href="javascript:;" onmousedown="init(1,<%=this.getDt("1",dtdata1) %>,'2','lc_f_id2')">L1</a>
                        <a id="2" href="javascript:;" onmousedown="init(2,<%=this.getDt("2",datalc2) %>,'2','lc_f_id2')">L2</a>
                        <a id="3" href="javascript:;" onmousedown="init(3,<%=this.getDt("3",datalc3 )%>,'2','lc_f_id2')">L3</a>
                        <a id="4" href="javascript:;" onmousedown="init(4,<%=this.getDt("4",datalc4 )%>,'2','lc_f_id2')">L4</a>
                        <a id="5" href="javascript:;" onmousedown="init(5,<%=this.getDt("5",datalc5 )%>,'2','lc_f_id2')">L5</a>
                              
                              <%--  <a href="site.aspx?locaiton=0">B1</a>
                                <a href="site.aspx?locaiton=1">L1</a>
                                <a href="site.aspx?locaiton=2">L2</a>
                                <a href="site.aspx?locaiton=3">L3</a>
                                <a href="site.aspx?locaiton=4">L4</a>
                                <a href="site.aspx?locaiton=5">L5</a--%>
                            </div>
                        </li>
                        <li><a class="si_4" href="/En/shopping/shop.aspx">推荐店铺</a></li>
                    </ul>
                </div>
                <!-- mLeft end -->
                <div class="mRight">
                    <div class="hidebg"></div>
                    <div class="blMap" id="blMap_id">
                        <div class="lc_position">
                            <div class="lc_f" id="lc_f_id">
                                <div class="lc_ico" id="lc_ico_id">
                                </div>
                                <div class="lc content" id="lc_id">
                                    <div class="lc_gundong" id="lc_gundong_id">
                                        <div class="lc_img" id="lc_img_id">
                                            <img src="" />
                                        </div>
                                        <div class="lc_over" id="lc_over_id">
                                        </div>
                                    </div>
                                </div>
                                <div class="lc_btn0" id="lc_btn_id">
                                    <div class="lc_btn_gundong" id="lc_btn_gundong_id">
                                        <img src="" usemap="#imgMap" id="img" border="0" hidefocus="true" />
                                        <map name="imgMap" id="lcMap_id">
                                        </map>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--<img src="../images/b1_map.jpg" alt="">-->
                    </div>
                <%--    <div class="bl_txt" id="bltxt2">
                        <asp:Repeater ID="rptlist" runat="server">
                            <ItemTemplate>
                                <img src="/UploadFiles/images/<%#Eval("Stereogram")%>" alt="">
                                <div class="subLTxt">
                                    <img src="/UploadFiles/images/<%#Eval("ShopLogo")%>" alt="">
                                    <span>
                                        <i>店铺名称：<%#Eval("Title")%></i>
                                        <i>位置：<%#Eval("ShopNo")%> <em class="btnSite">地图</em></i>
                                        <i>电话：<%#Eval("TelPhone")%></i>
                                        <i>销售产品：<%#Eval("SalesPro")%></i>
                                        <i>网址：<a href="http://www.Pandora.com">www.Pandora.com</a></i>
                                    </span>
                                    <p><%# KingTop.Common.Utils.GetSubString(Eval("IntroDetail").ToString(), 120, "...") %></p>
                                    <a href="detail.aspx?nid=<%#Eval("id") %>" id="bl_txt_a">了解详情</a>
                                </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                 
                    <iframe class="bl_txts" name="subLTxtIframe" id="subLTxtIframe" src="" frameborder="false" scrolling="auto" allowtransparency="true"></iframe>
                       </div>
                    <!-- mRight end -->
                </div>
            </div>
            <!--main end-->
            <uc1:Footer runat="server" ID="Footer" />
            <!--footer end-->
            <div class="overbg"></div>
            <div class="lMap">
                <h1 class="close"></h1>
                <div class="lc_position">
                    <div class="lc_f" id="lc_f_id2">
                    </div>
                </div>
                <!--<img src="../images/l_map.png" alt="">-->
            </div>
            <!--lMap end-->
            <!--lc data-->
            <div id="data_lc0" style="display: none;" lc_f_width="1708" lc_f_height="344" lc_btn_imgurl="/En/images/lcimages/b1/b1_btn.png" lc_imgurl="/En/images/lcimages/b1/b1_bg.png">
                <ico icourl="/En/images/lcimages/ico_s.jpg" icovalue="地下一层B1" icoclass="ico_txt1" icopos="left"></ico>
                <ico icourl="/En/images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_7.png" icovalue="ATM" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
                <asp:Repeater ID="rptdata0lclist" runat="server">
                    <ItemTemplate>
                       
                        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"   <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>   lccoords="<%#Eval("lccoords")%>"></lc>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="data_lc1" style="display: none;" lc_f_width="2160" lc_f_height="460" lc_btn_imgurl="/En/images/lcimages/f1/f1_btn.png" lc_imgurl="/En/images/lcimages/f1/f1_bg.png">
                <ico icourl="/En/images/lcimages/ico_s.jpg" icovalue="地上一楼F1" icoclass="ico_txt1" icopos="left"></ico>
                <ico icourl="/En/images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_4.png" icovalue="客服前台" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
                <asp:Repeater ID="rptdatalc1list" runat="server">
                    <ItemTemplate>
                        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="data_lc2" style="display: none;" lc_f_width="1812" lc_f_height="354" lc_btn_imgurl="/En/images/lcimages/f2/f2_btn.png" lc_imgurl="/En/images/lcimages/f2/f2_bg.png">
                <ico icourl="/En/images/lcimages/ico_s.jpg" icovalue="地上二楼F2" icoclass="ico_txt1" icopos="left"></ico>
                <ico icourl="/En/images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_8.png" icovalue="母婴室" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
                <asp:Repeater ID="rptdatalc2list" runat="server">
                    <ItemTemplate>
                        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"   <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="data_lc3" style="display: none;" lc_f_width="1812" lc_f_height="354" lc_btn_imgurl="/En/images/lcimages/f3/f3_btn.png" lc_imgurl="/En/images/lcimages/f3/f3_bg.png">
                <ico icourl="/En/images/lcimages/ico_s.jpg" icovalue="地上三楼F3" icoclass="ico_txt1" icopos="left"></ico>
                <ico icourl="/En/images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_8.png" icovalue="母婴室" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
                <asp:Repeater ID="rptdatalc3list" runat="server">
                    <ItemTemplate>
                        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="data_lc4" style="display: none;" lc_f_width="1206" lc_f_height="396" lc_btn_imgurl="/En/images/lcimages/f4/f4_btn.png" lc_imgurl="/En/images/lcimages/f4/f4_bg.png">
                <ico icourl="/En/images/lcimages/ico_s.jpg" icovalue="地上四楼F4" icoclass="ico_txt1" icopos="left"></ico>
                <ico icourl="/En/images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
                <asp:Repeater ID="rptdatalc4list" runat="server">
                    <ItemTemplate>
                        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="data_lc5" style="display: none;" lc_f_width="1239" lc_f_height="399" lc_btn_imgurl="/En/images/lcimages/f5/f5_btn.png" lc_imgurl="/En/images/lcimages/f5/f5_bg.png">
                <ico icourl="/En/images/lcimages/ico_s.jpg" icovalue="地上五楼F5" icoclass="ico_txt1" icopos="left"></ico>
                <ico icourl="/En/images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
                <ico icourl="/En/images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
                <asp:Repeater ID="rptdatalc5list" runat="server">
                    <ItemTemplate>
                        
                        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!--lc data end-->
            <script type="text/javascript">
             
              //  init(0, 0, "2", "lc_f_id2");//初始化显示的楼层及店铺
              
                var num11 = "";
                var num22 = "";
                function init(num1, num2, id_num, id_name) {
                    num11 = num1;
                    num22 = num2;
                    cre_lc_fun(num1);
                    lc_over_down(num1, num2, id_num, id_name);
                    lc_ovwr_show(document.getElementById('lc_over_id'), 'block', num2);
                }
                if (num11 == "" && num22 == "")
                {
                    init(<%=this.locaiton%>, 0, "2", "lc_f_id2");
                 
                }
                function cre_lc_fun(m) {
                    var lc_ico_str = "";
                    var lc_map_str = "";
                    var lc_over_img_str = "";
                    var lc_over_txtout_str = "";
                    var lc_over_txtover_str = "";
                    var data = document.getElementById("data_lc" + m);
                    document.getElementById("lc_gundong_id").style.width = document.getElementById("lc_btn_gundong_id").style.width = document.getElementById("lc_over_id").style.width = document.getElementById("lc_img_id").style.width = data.getAttribute("lc_f_width") + "px";
                    document.getElementById("lc_gundong_id").style.height = document.getElementById("lc_btn_gundong_id").style.height = document.getElementById("lc_btn_id").style.height = document.getElementById("lc_over_id").style.height = document.getElementById("lc_img_id").style.height = data.getAttribute("lc_f_height") + "px";
                    document.getElementById("lc_id").style.height = (Number(data.getAttribute("lc_f_height")) + 20) + "px";
                    document.getElementById("blMap_id").style.height = (Number(data.getAttribute("lc_f_height")) + 47 + 6 + 20) + "px";
                    document.getElementById("img").setAttribute("src", data.getAttribute("lc_btn_imgurl"));
                    document.getElementById("lc_img_id").getElementsByTagName("img")[0].setAttribute("src", data.getAttribute("lc_imgurl"));
                    for (var j = 0; j < data.getElementsByTagName("ico").length; j++) {
                        lc_ico_str += "<div style=\"background: url(" + data.getElementsByTagName("ico")[j].getAttribute("icourl") + ") top left no-repeat;float: " + data.getElementsByTagName("ico")[j].getAttribute("icopos") + ";\"><span class=\"f16 " + data.getElementsByTagName("ico")[j].getAttribute("icoclass") + "\">" + data.getElementsByTagName("ico")[j].getAttribute("icovalue") + "</span></div>"
                    }
                    document.getElementById("lc_ico_id").innerHTML = lc_ico_str;
                    for (var k = 0; k < data.getElementsByTagName("lc").length; k++) {
                        lc_over_img_str += "<img src=\"" + data.getElementsByTagName("lc")[k].getAttribute("lcoverimgurl") + "\" />";
                        lc_over_txtout_str += "<div class=\"lc_txt_out\" style=\" left: " + data.getElementsByTagName("lc")[k].getAttribute("lcx") + "px; top: " + data.getElementsByTagName("lc")[k].getAttribute("lcy") + "px;\"><div class=\"txt_out\"><span>" + data.getElementsByTagName("lc")[k].getAttribute("lcnum") + "</span></div></div>";
                        lc_over_txtover_str += "<div class=\"lc_txt_over\" style=\" left: " + data.getElementsByTagName("lc")[k].getAttribute("lcx") + "px; top: " + data.getElementsByTagName("lc")[k].getAttribute("lcy") + "px;\"><p class=\"txt_over\"><span>" + data.getElementsByTagName("lc")[k].getAttribute("lcname") + "</span></p></div>";
                        lc_map_str += "<area shape=\"poly\" coords=\"" + data.getElementsByTagName("lc")[k].getAttribute("lccoords") + "\" href=\"javascript:;\" onmouseover=\"lc_ovwr_show(document.getElementById(\'lc_over_id\'),\'block\'," + k + ")\" onmouseout=\"lc_ovwr_show(document.getElementById(\'lc_over_id\'),\'none\'," + k + ")\" onclick=\"lc_over_down(" + m + "," + k + ",\'2\',\'lc_f_id2\')\">";
                    }
                    document.getElementById("lc_over_id").innerHTML = lc_over_img_str + lc_over_txtout_str + lc_over_txtover_str;
                    document.getElementById("lcMap_id").innerHTML = lc_map_str;
                }
                function lc_over_down(num1, num2, id_num, id_name) {
                    bl_txt_fun(num1, num2);
                    var data = document.getElementById("data_lc" + num1);
                    document.getElementById("lcMap_id").getElementsByTagName("area")[num2].removeAttribute("onmouseout");
                    for (var k = 0; k < data.getElementsByTagName("lc").length; k++) {
                        if (k != num2) {
                            document.getElementById("lcMap_id").getElementsByTagName("area")[k].setAttribute("onmouseout", "lc_ovwr_show(document.getElementById(\'lc_over_id\'),\'none\'," + k + ")");
                            lc_ovwr_show(document.getElementById('lc_over_id'), 'none', k);
                        }
                    }
                    lMapFun(num1, num2, id_num, id_name);
                    document.getElementById(id_name).getElementsByTagName("area")[num2].removeAttribute("onmouseout");
                }
                /*地图点位动态创建*/
                function lMapFun(num1, num2, id_num, id_name) {
                    document.getElementById(id_name).style.width = "1155px";
                    document.getElementById(id_name).innerHTML = "<div class=\"lc_ico\" id=\"lc_ico_id" + id_num + "\"></div><div class=\"lc\" id=\"lc_id" + id_num + "\"><div class=\"lc_gundong\" id=\"lc_gundong_id" + id_num + "\"><div class=\"lc_img\" id=\"lc_img_id" + id_num + "\"><img /></div><div class=\"lc_over\" id=\"lc_over_id" + id_num + "\"></div><div class=\"lc_btn\" id=\"lc_btn_id" + id_num + "\"><img usemap=\"#imgMap" + id_num + "\" id=\"img" + id_num + "\" border=\"0\" hidefocus=\"true\"/><map name=\"imgMap" + id_num + "\" id=\"lcMap_id" + id_num + "\"></map></div></div></div>";
                    var data = document.getElementById("data_lc" + num1);
                    var bl = 1166 / data.getAttribute("lc_f_width");
                    document.getElementById("lc_id" + id_num).style.transform = "scale(" + bl + ")";
                    document.getElementById("lc_id" + id_num).style.webkitTransform = "scale(" + bl + ")";
                    document.getElementById("lc_id" + id_num).style.msTransform = "scale(" + bl + ")";
                    document.getElementById("lc_id" + id_num).style.mozTransform = "scale(" + bl + ")";
                    document.getElementById("lc_id" + id_num).style.oTransform = "scale(" + bl + ")";
                    document.getElementById("lc_id" + id_num).style.left = -Number(data.getAttribute("lc_f_width")) * (1 - bl) / 2 + "px";
                    document.getElementById("lc_id" + id_num).style.top = (-Number(data.getAttribute("lc_f_height")) * (1 - bl) / 2 + (460 + 45 - Number(data.getAttribute("lc_f_height")) * bl) / 2) + "px";
                    document.getElementById("lc_id" + id_num).style.width = document.getElementById("lc_gundong_id" + id_num).style.width = document.getElementById("lc_img_id" + id_num).style.width = document.getElementById("lc_over_id" + id_num).style.width = document.getElementById("lc_btn_id" + id_num).style.width = data.getAttribute("lc_f_width") + "px";
                    document.getElementById("lc_id" + id_num).style.height = document.getElementById("lc_gundong_id" + id_num).style.height = document.getElementById("lc_img_id" + id_num).style.height = document.getElementById("lc_over_id" + id_num).style.height = document.getElementById("lc_btn_id" + id_num).style.height = data.getAttribute("lc_f_height") + "px";
                    document.getElementById("lc_img_id" + id_num).getElementsByTagName("img")[0].setAttribute("src", data.getAttribute("lc_imgurl"));
                    document.getElementById("img" + id_num).setAttribute("src", data.getAttribute("lc_btn_imgurl"));
                    this["lc_ico_str" + id_num] = [];
                    this["lc_over_img_str" + id_num] = "";
                    this["lc_over_txtout_str" + id_num] = "";
                    this["lc_over_txtover_str" + id_num] = "";
                    this["lc_map_str" + id_num] = "";
                    for (var j = 0; j < data.getElementsByTagName("ico").length; j++) {
                        this["lc_ico_str" + id_num] += "<div style=\"background: url(" + data.getElementsByTagName("ico")[j].getAttribute("icourl") + ") top left no-repeat;float: " + data.getElementsByTagName("ico")[j].getAttribute("icopos") + ";\"><span class=\"f16 " + data.getElementsByTagName("ico")[j].getAttribute("icoclass") + "\">" + data.getElementsByTagName("ico")[j].getAttribute("icovalue") + "</span></div>"
                    }
                    document.getElementById("lc_ico_id" + id_num).innerHTML = this["lc_ico_str" + id_num];
                    for (var k = 0; k < data.getElementsByTagName("lc").length; k++) {
                        this["lc_over_img_str" + id_num] += "<img src=\"" + data.getElementsByTagName("lc")[k].getAttribute("lcoverimgurl") + "\" />";
                        this["lc_over_txtout_str" + id_num] += "<div class=\"lc_txt_out\" style=\" left: " + data.getElementsByTagName("lc")[k].getAttribute("lcx") + "px; top: " + data.getElementsByTagName("lc")[k].getAttribute("lcy") + "px;\"><div class=\"txt_out\"><span>" + data.getElementsByTagName("lc")[k].getAttribute("lcnum") + "</span></div></div>";
                        this["lc_over_txtover_str" + id_num] += "<div class=\"lc_txt_over\" style=\" left: " + data.getElementsByTagName("lc")[k].getAttribute("lcx") + "px; top: " + data.getElementsByTagName("lc")[k].getAttribute("lcy") + "px;\"><p class=\"txt_over\"><span>" + data.getElementsByTagName("lc")[k].getAttribute("lcname") + "</span></p></div>";
                        this["lc_map_str" + id_num] += "<area shape=\"poly\" coords=\"" + data.getElementsByTagName("lc")[k].getAttribute("lccoords") + "\" href=\"javascript:;\" onmouseover=\"lc_ovwr_show(document.getElementById(\'lc_over_id" + id_num + "\'),\'block\'," + k + ")\" onmouseout=\"lc_ovwr_show(document.getElementById(\'lc_over_id" + id_num + "\'),\'none\'," + k + ")\" onclick=\"bl_txt_aFun(" + num1 + "," + k + ")\">";
                    }
                    document.getElementById("lc_over_id" + id_num).innerHTML = this["lc_over_img_str" + id_num] + this["lc_over_txtout_str" + id_num] + this["lc_over_txtover_str" + id_num];
                    document.getElementById("lcMap_id" + id_num).innerHTML = this["lc_map_str" + id_num];
                    document.getElementById("lc_over_id" + id_num).getElementsByTagName("p")[num2].style.display = "block";
                    document.getElementById("lc_over_id" + id_num).getElementsByTagName("img")[num2].style.display = "block";
                }
                /*bl_txt函数*/
                function bl_txt_fun(num1, num2) {
                    var data = document.getElementById("data_lc" + num1);
                    document.getElementById("subLTxtIframe").src = data.getElementsByTagName("lc")[num2].getAttribute("lclink");
                }
                function bl_txt_aFun(num1, num2) {
                    var data = document.getElementById("data_lc" + num1);
                    window.location.href = data.getElementsByTagName("lc")[num2].getAttribute("lclink2");
                }
                /*触发显示及隐藏*/
                function lc_ovwr_show(t, m, num) {
                    t.getElementsByTagName("img")[num].style.display = m;
                    t.getElementsByTagName("p")[num].style.display = m;
                }
                var int = self.setInterval("lc_btn_Fun()", 50)
                function lc_btn_Fun() {
                    if( document.getElementById("mCSB_1_container"))
                    {
                  
                        document.getElementById("lc_btn_gundong_id").style.left = document.getElementById("mCSB_1_container").style.left;
                    }
                }
                //	楼层js 完

</script>

         
            <script src="/En/js/jqnav.js" type="text/javascript"></script>
            <script src="/En/js/SuperSlide.js" type="text/javascript"></script>
            <script src="/En/js/jquery.mCustomScrollbar.concat.min.js" type="text/javascript"></script>
            <script>
                (function ($) {
                    $(window).on("load", function () {
                        $(".content").mCustomScrollbar({
                            axis: "x",
                            theme: "dark-3",
                            advanced: { autoExpandHorizontalScroll: true }
                        });
                    });

                    s();
                })(jQuery);
                function s()
                {
                    $("#<%=this.locaiton%>").addClass("sa_on").siblings().removeClass("sa_on");
                }
            </script>
    </form>
</body>
</html>
