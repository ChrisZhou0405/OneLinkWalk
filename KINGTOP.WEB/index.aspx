<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.index" %>

<%@ Register Src="~/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>
<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" ID="Meta" />
</head>
     <script type="text/javascript">

         //平台、设备和操作系统
         var system = {
             win: false,
             mac: false,
             xll: false
         };
         //检测平台
         var p = navigator.platform;
         system.win = p.indexOf("Win") == 0;
         system.mac = p.indexOf("Mac") == 0;
         system.x11 = (p == "X11") || (p.indexOf("Linux") == 0);
         //跳转语句
         if (system.win || system.mac || system.xll) { }
         else
         {

             window.location.href = "/Phone/index.aspx";
         }//手机


</script>

<body>
    <form id="form1" runat="server">
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->
        <div class="wrapper">
            <div class="banner">
                <div class="bd">
                    <ul>
                        <asp:Repeater ID="rptbanner" runat="server">
                            <ItemTemplate>
                                <li><a href="<%#Eval("Links") %>" style="background: url(/UploadFiles/images/<%#Eval("BigImg")%>) 50% no-repeat; background-size: cover;"></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="hd">
                    <ul>
                        <asp:Repeater ID="rptli" runat="server">
                            <ItemTemplate>
                                <li></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <a class="prev"></a><a class="next"></a>
            </div>
            <!--banner end-->
            <!--main start-->
            <div class="main">
                <div class="navLink">
                    <ul>
                        <li>
                            <a href="/shopping/index.aspx" onmouseover="abc(0,0,4);" onmouseout="cad(0)"></a>
                            <div>
                                <svg class="pathA" version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="85px" height="74px" viewBox="-20 -20 140 120">
                                    <g stroke="#89715f">
                                        <path fill="none" stroke-width="2.5" d="M7.5,81.6c0,0,0-34.6,0-36.9c2.4,0,7.1,0,7.1,0" />
                                        <path fill="none" stroke-width="2.5" d="M15.4,81.6c0,0,0-42.8,0-46.4c2.9,0,9.7,0,9.7,0" />
                                        <path fill="none" stroke-width="3" d="M7.8,81.6h10.8c0,0,71.9,0,75.8,0" />
                                        <path fill="none" stroke-width="3" d="M18.6,81.5c0,0,2.9-54.5,13.2-55.2c1-0.1,34.7,0,39.7,0c2.3,1.3,6.8,9.7,11.7,55.2" />
                                        <path fill="none" stroke-width="2.5" d="M76.5,35.2c0,0,8.2,0,10.1,0c0,3.6,0,46.6,0,46.6" />
                                        <path fill="none" stroke-width="2.5" d="M86.9,44.7c0,0,4.5,0,7.6,0c-0.1,3.8,0,37.1,0,37.1" />
                                        <path fill="none" stroke-width="3" d="M20,74c0,0,36.7,0,40.1,0" />
                                        <path fill="none" stroke-width="3" d="M67.9,74c0,0,11.3,0,13.3,0" />
                                        <path fill="none" stroke-width="2" d="M61.7,39.9c-1.6,0.6-3.7,0.3-4.7-1.2c-1-1.4-1-3,0.1-4.6c1-1.5,2.8-1.8,4.4-1.3c1.7,0.5,2.6,2.1,2.2,4.6" />
                                        <path fill="none" stroke-width="2" d="M42.5,39.9c-1.6,0.6-3.7,0.3-4.7-1.2c-1-1.4-1-3,0.1-4.6c1-1.5,2.8-1.8,4.4-1.3c1.7,0.5,2.6,2.1,2.2,4.6" />
                                        <path fill="none" stroke-width="3" d="M40.8,36c0,0,0.1-17.1,0-22.3c-0.1-4.5,3.8-9.5,9.6-9.5c5.8,0,9.6,4.3,9.6,9c0,4.1,0,22.7,0,22.7" />
                                    </g>
                                </svg>
                            </div>
                            <span>购物指南</span>
                        </li>
                        <li>
                            <a href="/cate/index.aspx" onmouseover="abc(1,0,4);" onmouseout="cad(1)"></a>
                            <div>
                                <svg class="pathB" version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="85px" height="74px" viewBox="-23 -10 130 110">
                                    <g stroke="#89715f">
                                        <path fill="none" stroke-width="3" d="M10,42.9c0.5,2.6,6.3,32.3,7.2,36.5c0.2,1.1,0.2,2.4,1.9,2.4c7.8,0,37.2,0,42.9,0c1.1,0,1.6-0.5,1.9-1.8c0.7-3.1,8.1-36.3,8.4-37.4c0.3-1-0.7-1.9-1.7-1.9c-14.5,0-48.7,0-58.7,0C10.3,40.7,9.8,41.7,10,42.9z" />
                                        <path fill="none" d="M16.6,76.8c0,0,45.8,0,47.9,0" />
                                        <path fill="none" stroke-width="2.6" d="M19.4,36.3c0,0-2.2,0-2.6,2.3c0,3.8,0,23.8,0,23.8h26.9c0,0,0-20.9,0-22.7c0-2.3,2.8-3.3,2.8-3.3H19.4z" />
                                        <path fill="#FFFFFF" d="M46.5,35.3c0,0,0,2,0,5.7" />
                                        <path fill="none" stroke-width="1.5" d="M16.6,59c0,0,24.5,0,27.1,0" />
                                        <path fill="none" stroke-width="1.4" d="M10.3,46c1.9-0.1,4.2,0.1,6.8,0.9" />
                                        <path fill="none" stroke-width="1.4" d="M43.6,47.5c1.1,0.2,2.3,0.3,3.5,0.3c5.4-0.1,6-1.8,12-1.8c5.5,0,6.7,1.9,11.7,1.8" />
                                        <path fill="none" stroke-width="2.6" d="M50.2,40.3c1.2-3.9,8.7-9.4,14.2-17.6c3.2-4.7,5.9-11.1,6.5-12.5c0.3-0.7-1-0.2,0-2.2c1-2.1,1.3-0.5,1.5-1C72.7,6.3,73,5.5,76.2,7c3.3,1.5,2.4,2,2.1,2.7c-0.2,0.5,1.2-0.2,0.2,2c-1,2.1-1.3,0.4-1.6,1.2c-0.6,1.3-4.2,9.6-4.9,12.3c-0.7,2.5-1.8,8.2-2.7,15.5" />
                                        <path fill="none" stroke-width="4" d="M58.7,29.9c0,0,7.5,3.1,11.1,5.2" />
                                    </g>
                                </svg>
                            </div>
                            <span>美食荟萃</span>
                        </li>
                        <li>
                            <a href="/activity/index.aspx" onmouseover="abc(2,0,7);" onmouseout="cad(2)"></a>
                            <div>
                                <svg class="pathC" version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="85px" height="74px" viewBox="-12 -5 150 135">
                                    <g stroke="#89715f">
                                        <path fill="none" stroke-width="3" d="M13,109c14.6-0.1,16.4-0.1,22.4,0" />
                                        <path fill="none" stroke-width="3" d="M83.2,109c0,0,18.5-0.1,23.7,0" />
                                        <path fill="none" stroke-width="3" d="M12.5,109c5.4-8.1,12.5-24.7,6.6-44.4" />
                                        <path fill="none" stroke-width="2.5" d="M18.3,63.9c0,0,77.1,0,81.9,0" />
                                        <path fill="none" stroke-width="3" d="M106.9,109c0,0-14.3-17.7-7.3-44.4" />
                                        <path fill="none" stroke-width="3" d="M58.9,70.5c-1.6,9.9-6.9,29.1-23.7,38.5c-4.4-4.3-6.8-6.6-6.8-6.6s23.3-11.6,29.8-32" />
                                        <path fill="none" stroke-width="3" d="M61,70.5c2.2,9.4,8.1,27.1,22,38.4c2.4-2.5,6.7-7.4,6.7-7.4c-2.1-1.2-22.5-13.8-28.5-31.1" />
                                        <path fill="none" stroke-width="3" d="M19.2,64.6c2.4,7.7,13.8,7.4,15.8,0" />
                                        <path fill="none" stroke-width="3" d="M35.5,64.6c2.4,7.7,13.8,7.4,15.8,0" />
                                        <path fill="none" stroke-width="3" d="M51.2,64.6c2.4,7.7,13.8,7.4,15.8,0" />
                                        <path fill="none" stroke-width="3" d="M67.4,64.6c2.4,7.7,13.8,7.4,15.8,0" />
                                        <path fill="none" stroke-width="3" d="M83.5,64.6c2.4,7.7,13.8,7.4,15.8,0" />
                                        <path fill="none" stroke-width="3" d="M32.1,99.9c3.4-7.5,6.8-21.6,2.8-35.4" />
                                        <path fill="none" stroke-width="3" d="M50.5,84.3c1.1-5.6,1.1-14.2,0.9-19.8" />
                                        <path fill="none" stroke-width="3" d="M68.7,83.3c-1-8-1.1-13.2-1.5-18.8" />
                                        <path fill="none" stroke-width="3" d="M86.2,98.7c-3.7-7-5.9-21.9-3-34.2" />
                                        <path fill="none" stroke-width="2.5" d="M18.4,63.8C25.5,62,36.1,58,42,53.8" />
                                        <path fill="none" stroke-width="2.5" d="M35.2,63.8c7.5-2.7,11.4-6.5,14.9-10" />
                                        <path fill="none" stroke-width="2.5" d="M51.6,63.8c2.8-2.6,4.2-7.1,4.9-10" />
                                        <path fill="none" stroke-width="2.5" d="M67.1,63.8c-2.1-3.9-3.1-7.1-3.9-10" />
                                        <path fill="none" stroke-width="2.5" d="M82.5,63.8c-5.8-3.2-9.8-7-12.8-10" />
                                        <path fill="none" stroke-width="2.5" d="M100.1,63.8c-6.2-1.9-15.1-5-22.4-10" />
                                        <path fill="none" stroke-width="2" d="M41.8,53.5h36.1c0,0,0-1.1,0-5.1c-6.4-0.1-36.1,0-36.1,0V53.5z" />
                                        <path fill="none" stroke-width="2" d="M41.8,48.4c0,0,6.7-2.8,13.8-11.1" />
                                        <path fill="none" stroke-width="2" d="M53.3,48.3c0,0,3.2-4.5,4.8-11" />
                                        <path fill="none" stroke-width="2" d="M65.8,48.3c0,0-3.7-5.8-4.9-11" />
                                        <path fill="none" stroke-width="2" d="M77.9,48.4c0,0-8.2-4.2-14.3-11.1" />
                                        <path fill="none" stroke-width="2" d="M55.5,34.2c0,0,0,1,0,3.1c2.6,0,8.5,0,8.5,0v-3.1H55.5z" />
                                        <path fill="none" stroke-width="1.5" d="M59.8,34c0,0,0-13.9,0-19" />
                                        <path fill="none" stroke-width="0.9" d="M59.5,14.8c-0.7-0.2-1-1.1-0.5-1.6c0.7-0.7,1.9-0.2,1.8,0.6C60.7,14.5,60.2,14.9,59.5,14.8z" />
                                        <path fill="none" stroke-width="2" d="M60.2,19.2c0,0,10-3,19.8,0c0,1.9,0,4.1,0,4.1s9.4,2.4,16.5,0.4c-1.7,1.4-3.8,3-3.8,3s1.2,0.5,3.8,1.3c-5.3,1.9-12.3,0.7-16.9,0.1
c0-1.1-0.1-2.7-0.1-4.2C74,22,64,22.2,60.2,24C60.2,21.3,60.2,19.2,60.2,19.2z" />
                                    </g>
                                </svg>
                            </div>
                            <span>最新活动</span>
                        </li>
                        <li class="nobg">
                            <a href="/vip/nonmember.aspx?node=101005001" onmouseover="abc(3,0,4);" onmouseout="cad(3)"></a>
                            <div>
                                <svg class="pathD" version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" width="85px" height="74px" viewBox="-10 -20 120 100">
                                    <g stroke="#89715f">
                                        <path fill="none" stroke-width="2" d="M21.945,11.388c0,0,3.998,13.177,4.199,13.809s0.336,1.148,1.091,1.159c3.641,0.054,29.265-0.003,30.874,0
c1.188,0.002,1.377-0.078,1.705-1.022c0.459-1.322,4.529-13.644,5.016-15.245c0.293-0.963-0.674-1.786-1.727-0.992
c-2.818,2.128-11.462,8.747-11.462,8.747s-7.055-8.601-7.755-9.419c-0.698-0.815-1.419-0.79-1.933-0.081
c-2.083,2.875-7.334,10.104-7.334,10.104s-10.256-7.705-11.022-8.299C22.714,9.465,21.532,10.028,21.945,11.388z" />
                                        <path fill="none" stroke-width="2" d="M17.981,38.965l6.338,18.765l6.357-18.898c0.331-0.993,0.579-1.683,0.745-2.071s0.439-0.738,0.821-1.05
c0.382-0.312,0.904-0.468,1.565-0.468c0.483,0,0.932,0.121,1.346,0.363c0.414,0.242,0.738,0.563,0.974,0.964
s0.354,0.805,0.354,1.212c0,0.28-0.039,0.583-0.115,0.907c-0.076,0.325-0.172,0.643-0.286,0.955
c-0.115,0.312-0.229,0.633-0.344,0.964l-6.777,18.288c-0.242,0.7-0.484,1.364-0.726,1.995c-0.242,0.629-0.522,1.184-0.84,1.66
c-0.318,0.478-0.742,0.869-1.27,1.174c-0.528,0.307-1.174,0.459-1.938,0.459c-0.764,0-1.41-0.15-1.938-0.449
s-0.954-0.693-1.279-1.184c-0.325-0.489-0.608-1.047-0.85-1.67s-0.484-1.285-0.726-1.985L12.732,40.76
c-0.115-0.331-0.232-0.655-0.353-0.974c-0.121-0.318-0.223-0.662-0.306-1.031c-0.083-0.369-0.124-0.681-0.124-0.936
c0-0.649,0.261-1.241,0.783-1.775c0.521-0.534,1.177-0.802,1.966-0.802c0.967,0,1.651,0.296,2.052,0.888
S17.562,37.667,17.981,38.965z" />
                                        <path fill="none" stroke-width="2" d="M40.966,60.729V38.679c0-1.146,0.26-2.004,0.783-2.577c0.521-0.573,1.196-0.859,2.023-0.859c0.853,0,1.543,0.284,2.071,0.85
c0.528,0.566,0.792,1.429,0.792,2.587v22.049c0,1.158-0.264,2.023-0.792,2.596c-0.528,0.572-1.219,0.859-2.071,0.859
c-0.815,0-1.486-0.29-2.014-0.869C41.23,62.736,40.966,61.873,40.966,60.729z" />
                                        <path fill="none" stroke-width="2" d="M63.836,52.806h-5.193v7.923c0,1.133-0.267,1.991-0.801,2.576c-0.535,0.586-1.21,0.879-2.023,0.879c-0.854,0-1.541-0.29-2.063-0.869
c-0.522-0.578-0.783-1.428-0.783-2.549V39.194c0-1.247,0.287-2.138,0.859-2.672s1.482-0.802,2.73-0.802h7.273
c2.15,0,3.805,0.166,4.963,0.496c1.146,0.318,2.135,0.847,2.969,1.584c0.833,0.738,1.467,1.642,1.899,2.711
c0.433,1.068,0.649,2.271,0.649,3.607c0,2.852-0.879,5.012-2.635,6.481S67.311,52.806,63.836,52.806z" />
                                        <path fill="none" stroke-width="2" d="M62.461,39.977h-3.818v8.552h3.818c1.336,0,2.453-0.14,3.351-0.42c0.897-0.279,1.581-0.738,2.052-1.375
c0.471-0.636,0.707-1.469,0.707-2.5c0-1.234-0.363-2.24-1.088-3.016C66.667,40.391,64.994,39.977,62.461,39.977z" />
                                    </g>
                                </svg>
                            </div>
                            <span>会员专区</span>
                        </li>
                    </ul>
                </div>

                <div class="picScroll">
                    <div class="hd">
                        <a class="next1"></a>
                        <a class="prev1"></a>
                    </div>
                    <div class="bd">
                        <ul>
                            <asp:Repeater ID="rptlocationA" runat="server">
                                <ItemTemplate>
                                    <li><a href="<%#Eval("IP") %>">
                                        <img src="/UploadFiles/images/<%#Eval("SmallImg")%>" alt=""><span><%#Eval("Title") %></span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <!-- picScroll end -->
                <div class="picScroll2">
                    <div class="hd">
                        <a class="next2"></a>
                        <a class="prev2"></a>
                    </div>
                    <div class="bd">
                        <ul>
                            <asp:Repeater ID="rptlocationB" runat="server">
                                <ItemTemplate>
                                    <li><a href="<%#Eval("IP") %>">
                                        <img src="/UploadFiles/images/<%#Eval("SmallImg")%>" alt=""><span><%#Eval("Title") %></span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <!-- picScroll 2 end -->

                <!-- indexBg end -->
                <div class="theme">
                    <asp:Repeater ID="rptlocationC" runat="server">
                        <ItemTemplate>
                            <a href="<%#Eval("IP") %>">
                                <img src="/UploadFiles/images/<%#Eval("SmallImg")%>" alt=""><span><%#Eval("Title") %></span></a>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptlocationD" runat="server">
                        <ItemTemplate>
                            <a href="<%#Eval("IP") %>" class="t_ac">
                                <img src="/UploadFiles/images/<%#Eval("SmallImg")%>" alt=""><span><%#Eval("Title") %></span></a>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptlocationE" runat="server">
                        <ItemTemplate>
                            <a href="<%#Eval("IP") %>">
                                <img src="/UploadFiles/images/<%#Eval("SmallImg")%>" alt=""><span><%#Eval("Title") %></span></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!-- theme end -->
            </div>
            <!--main end-->
        </div>
        <!--wrapper end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="js/jquery.js" type="text/javascript"></script>
        <script src="js/jqnav.js" type="text/javascript"></script>
        <script src="js/SuperSlide.js" type="text/javascript"></script>
        <!-- svg -->
        <script>
            var lengthA = lengthB = lengthC = lengthD = [],//定义path所生成的素描长度数
                length = [lengthA, lengthB, lengthC, lengthD],
                iID = [],//定义循环函数名
                pathA = document.querySelectorAll('.pathA path'),//定义svg A中的唯一path元素
                pathB = document.querySelectorAll('.pathB path'),//定义svg B中的唯一path元素
                pathC = document.querySelectorAll('.pathC path'),//定义svg C中的唯一path元素
                pathD = document.querySelectorAll('.pathD path'),//定义svg D中的唯一path元素
                path = [pathA, pathB, pathC, pathD];

            //定义动态初始化方法/参数为svg所在节点数
            var init = function (k) {
                for (var i = 0; i < path[k].length; i++) {
                    length[k][i] = Math.floor(path[k].item(i).getTotalLength()) + 1;
                    path[k].item(i).style.strokeDasharray = length[k][i]; //定义动态dasharray
                    path[k].item(i).style.strokeDashoffset = length[k][i]; //定义动态dashoffset
                }
            }

            //初始化dasharray、dashoffset
            init(0);
            init(1);
            init(2);
            init(3);

            //定义绘画函数/参考k:svg所在节点数;n:初始绘画节点数；speed:绘画速度（最小为0，越大越快）
            function draw(k, n, speed) {
                length[k][n] -= speed;
                if (length[k][n] < 0) {
                    length[k][n] = 0
                }
                path[k].item(n).style.strokeDashoffset = length[k][n];
                if (length[k][n] == 0) {
                    clearInterval(iID[n]);
                    m = n;
                    m++;
                    if (m < path[k].length) {
                        iID[m] = setInterval("draw(" + k + "," + m + "," + speed + ")", 1);
                    }
                }
            }
            //设置鼠标经过函数
            function abc(k, n, speed) {
                init(k);
                iID[n] = setInterval("draw(" + k + "," + n + "," + speed + ")", 1);
            }
            //设置鼠标离开函数
            function cad(k) {
                for (var i = 0; i < path[k].length; i++) {
                    clearInterval(iID[i]);
                }
            }
        </script>
        <script type="text/javascript">
            function s_h() {
                var $height = document.body.clientHeight;
                var $width = $(".wrapper").width();
                if ($height <= 694) {
                    var b_h = $height * 0.64;
                    $(".navLink").css("height", ($height * 0.36 - 130));
                    $(".navLink ul li a + div + span").css("font-size", Math.floor(($height * 0.38 - 130) * 22 / 120) + "px");

                } else {
                    var b_h = $height - 213;
                    $(".navLink").css("height", "120");
                    $(".navLink ul li a + div + span").css("font-size", "22px");
                }
                $(".banner,.banner .bd li a").height(b_h);
                $(".banner,.banner .bd ul li").width($width);
            }
            jQuery(".banner").slide({ titCell: ".hd ul", mainCell: ".bd ul", effect: "leftLoop", vis: "auto", autoPlay: true, autoPage: true, delayTime: 900, interTime: 5000 });
            jQuery(".picScroll").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "fade", autoPlay: true, vis: "auto", prevCell: ".prev1", nextCell: ".next1", delayTime: 900, interTime: 4000, pnLoop: false });
            jQuery(".picScroll2").slide({ titCell: ".hd ul", mainCell: ".bd ul", autoPage: true, effect: "fade", autoPlay: true, vis: "auto", prevCell: ".prev2", nextCell: ".next2", delayTime: 850, interTime: 4000, pnLoop: false });
            s_h();
            $(window).resize(function () {
                s_h();
            });
        </script>
    </form>
</body>
</html>


