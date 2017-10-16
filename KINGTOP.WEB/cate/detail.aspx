<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="KingTop.WEB.cate.detail" %>

<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Controls/share.ascx" TagPrefix="uc1" TagName="share" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no, minimum-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control" />
    <meta content="no-cache" http-equiv="pragma" />
    <meta content="0" http-equiv="expires" />
    <meta content="telephone=no, address=no" name="format-detection" />
    <meta name="revised" content="Monday, April 25th, 2016, 15:00 pm" />
    <meta name="author" content="xiaoxiao" />
    <title><%=PageTitle%></title>
    <meta name="keywords" content=<%=PageKeyWords%> />
    <meta name="description" content=<%=PageDescription%> />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
</head>
<body>
    

    <form id="form1" runat="server">
     
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->
        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <asp:Repeater ID="rptlist" runat="server">
                    <ItemTemplate>
                        <div class="title">
                            <%#Eval("Title")%>
                            <span>分享: 
                                <a id="awx" class="ds_wx"></a>
                           <%--     <a href="#" class="ds_en"></a>--%>
                                <a id="aqq"  class="ds_qq"></a>
                                <a id="wb" class="ds_sn"></a>
                                <a id="adp"  class="ds_bou"></a>
                            </span>
                        </div>
                        <div class="picScroll4">
                            <div class="hd">
                            </div>
                            <div class="bd">
                                <span class="next1"></span>
                                <span class="prev1"></span>
                                <ul>
                                    <%# GetListIMG(Eval("Shopshow").ToString())%>
                                </ul>
                            </div>
                        </div>
                        <!-- picScroll 4 end -->
                        <div class="shopDtxt">
                            <h1>店铺信息</h1>
                            <img src="/UploadFiles/Images/<%#Eval("ShopLogo")%>" alt="">
                            <span>
                                <i>店铺名称：<%#Eval("Title")%></i>
                                <i>位置：<%#Eval("ShopNo")%><em class="btnSite">地图</em></i>
                                <i>电话：<%#Eval("TelPhone")%></i>
                                <i>销售产品：<%#Eval("SalesPro")%></i>
                                <i>网址：<a  target="_blank" href="http://<%#Eval("SiteURL")%>"><%#Eval("SiteURL")%></a></i>
                            </span>
                            <p><%#Eval("IntroDetail")%></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <div class="hotShop">
                    <h1><span>你可能还会喜欢</span></h1>
                    <!-- picScroll -->
                    <div class="picScroll5">
                        <div class="hd">
                        </div>
                        <div class="bd">
                            <span class="next2"></span>
                            <span class="prev2"></span>
                            <ul>
                                <asp:Repeater ID="RptRelated" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href="detail.aspx?nid=<%#Eval("ID") %>">
                                                <img src="/UploadFiles/Images/<%#Eval("LikeImg")%>" alt="">
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <!-- picScroll 5 end -->
                </div>
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
        </div>
        <!--lMap end-->
        <div id="data_lc0" style="display: none;" lc_f_width="1708" lc_f_height="344" lc_btn_imgurl="../images/lcimages/b1/b1_btn.png" lc_imgurl="../images/lcimages/b1/b1_bg.png">
            <ico icourl="../images/lcimages/ico_s.jpg" icovalue="地下一层B1" icoclass="ico_txt1" icopos="left"></ico>
            <ico icourl="../images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_7.png" icovalue="ATM" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
            <asp:Repeater ID="rptdata0lclist" runat="server">
                <ItemTemplate>
            <%--        <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>"  lclink="<%#Eval("lclink")%>" lccoords="<%#Eval("lccoords")%>"></lc>--%>

                          <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="data_lc1" style="display: none;" lc_f_width="2160" lc_f_height="460" lc_btn_imgurl="../images/lcimages/f1/f1_btn.png" lc_imgurl="../images/lcimages/f1/f1_bg.png">
            <ico icourl="../images/lcimages/ico_s.jpg" icovalue="地上一楼F1" icoclass="ico_txt1" icopos="left"></ico>
            <ico icourl="../images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_4.png" icovalue="客服前台" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
            <asp:Repeater ID="rptdatalc1list" runat="server">
                <ItemTemplate>
                  <%--  <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>"  lclink="<%#Eval("lclink")%>" lccoords="<%#Eval("lccoords")%>"></lc>--%>

                                              <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>

                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="data_lc2" style="display: none;" lc_f_width="1812" lc_f_height="354" lc_btn_imgurl="../images/lcimages/f2/f2_btn.png" lc_imgurl="../images/lcimages/f2/f2_bg.png">
            <ico icourl="../images/lcimages/ico_s.jpg" icovalue="地上二楼F2" icoclass="ico_txt1" icopos="left"></ico>
            <ico icourl="../images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_8.png" icovalue="母婴室" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
            <asp:Repeater ID="rptdatalc2list" runat="server">
                <ItemTemplate>
                <%--    <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>"  lclink="<%#Eval("lclink")%>" lccoords="<%#Eval("lccoords")%>"></lc>--%>

                                             <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>

                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="data_lc3" style="display: none;" lc_f_width="1812" lc_f_height="354" lc_btn_imgurl="../images/lcimages/f3/f3_btn.png" lc_imgurl="../images/lcimages/f3/f3_bg.png">
            <ico icourl="../images/lcimages/ico_s.jpg" icovalue="地上三楼F3" icoclass="ico_txt1" icopos="left"></ico>
            <ico icourl="../images/lcimages/ico_1.png" icovalue="洗手间" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_8.png" icovalue="母婴室" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
            <asp:Repeater ID="rptdatalc3list" runat="server">
                <ItemTemplate>
                <%--    <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>"  lclink="<%#Eval("lclink")%>" lccoords="<%#Eval("lccoords")%>"></lc>--%>
                                               <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>

                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="data_lc4" style="display: none;" lc_f_width="1206" lc_f_height="396" lc_btn_imgurl="../images/lcimages/f4/f4_btn.png" lc_imgurl="../images/lcimages/f4/f4_bg.png">
            <ico icourl="../images/lcimages/ico_s.jpg" icovalue="地上四楼F4" icoclass="ico_txt1" icopos="left"></ico>
            <ico icourl="../images/lcimages/ico_2.png" icovalue="手扶梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_5.png" icovalue="零售" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
            <asp:Repeater ID="rptdatalc4list" runat="server">
                <ItemTemplate>
                <%--    <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>" lclink="<%#Eval("lclink")%>" lccoords="<%#Eval("lccoords")%>"></lc>--%>
                                              <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>

                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="data_lc5" style="display: none;" lc_f_width="1239" lc_f_height="399" lc_btn_imgurl="../images/lcimages/f5/f5_btn.png" lc_imgurl="../images/lcimages/f5/f5_bg.png">
            <ico icourl="../images/lcimages/ico_s.jpg" icovalue="地上五楼F5" icoclass="ico_txt1" icopos="left"></ico>
            <ico icourl="../images/lcimages/ico_3.png" icovalue="观光梯" icoclass="ico_txt2" icopos="right"></ico>
            <ico icourl="../images/lcimages/ico_6.png" icovalue="餐饮" icoclass="ico_txt2" icopos="right"></ico>
            <asp:Repeater ID="rptdatalc5list" runat="server">
                <ItemTemplate>
                   <%-- <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>" lclink="<%#Eval("lclink")%>" lccoords="<%#Eval("lccoords")%>"></lc>--%>
<%--                       <lc lcnum="L5-1-2" lcname="凯悦空中花园" lcoverimgurl="../images/lcimages/f5/f5_01.png" lcx="750" lcy="105"  lclink="site2.html" lclink2="detail.html" lccoords="142,151,209,151,287,107,528,106,589,152,643,152,664,168,809,168,849,314,861,324,1140,325,1151,322,1162,318,1171,313,1178,306,1183,297,1186,288,1185,276,1184,264,1180,251,1176,243,1060,67,1050,60,788,60,782,66,783,76,625,75,616,73,610,69,603,65,598,61,592,58,581,56,249,56,175,94,170,97,166,101,142,143"></lc>--%>

                                            <lc lcnum="<%#Eval("lcnum")%>" lcname="<%#Eval("Title")%>" lcoverimgurl="<%#Eval("lcoverimgurl")%>" lcx="<%#Eval("lcx")%>" lcy="<%#Eval("lcy")%>" <%# this.geturl(Eval("cgID").ToString() , Eval("fgID").ToString())  %>     lccoords="<%#Eval("lccoords")%>"></lc>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!--lc data end-->


        <uc1:share runat="server" ID="share" />

        <script type="text/javascript">
            //	楼层js
            //lMapFun(0,0,"2","lc_f_id2");//初始化显示的楼层及店铺
            //第一个是楼层，第二个是店铺
         
            lMapFun(<%=this.floorId%>,<%=this.getDt(""+floorId+"") %>,'2','lc_f_id2');
            var data = document.getElementById("data_lc" + <%=this.floorId%>);//0第一个是楼层
            document.getElementById("lc_f_id2").getElementsByTagName("area")[<%=this.getDt(""+floorId+"") %>].removeAttribute("onmouseout");//第二个是店铺
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
                document.getElementById("subLTxtIframe").src=data.getElementsByTagName("lc")[num2].getAttribute("lclink");
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
            //	楼层js 完
</script>
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
        <script type="text/javascript">
            jQuery(".picScroll4").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 1, prevCell: ".prev1", nextCell: ".next1", pnLoop: false });
            jQuery(".picScroll5").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "left", autoPlay: false, vis: 4, prevCell: ".prev2", nextCell: ".next2", pnLoop: false });
        </script>
    </form>


   
</body>
</html>
<script src="../js/share.js"></script>
