<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="traffic.aspx.cs" Inherits="KingTop.WEB.about.traffic" %>

<%@ Register Src="~/Controls/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Controls/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/Controls/Meta.ascx" TagPrefix="uc1" TagName="Meta" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Meta runat="server" ID="Meta" />
    <style>
        * {
            margin: 0 auto;
            border: 0;
            padding: 0;
        }

        .ddfs {
            width: 1200px;
            height: 545px;
            position: relative;
        }

        .navlist1 {
            width: 86px;
            height: 387px;
            background: url("/images/ddfs_right.png") no-repeat;
            position: absolute;
            list-style-type: none;
            margin-top: 78px;
            margin-left: 1050px;
        }

            .navlist1 li {
                width: 82px;
                height: 84px;
                display: inline-block;
                margin-top: 4px;
                margin-left: 2px;
            }

                .navlist1 li a {
                    width: 100%;
                    height: 100%;
                    float: left;
                }

                .navlist1 li div {
                    width: 100%;
                    height: 100%;
                    background: url("/images/ico_opa_bg.png") no-repeat;
                    display: block;
                }

        #img {
            width: 100%;
            height: 100% position: absolute;
            z-index: 990;
        }

        .navlist1 {
            -moz-animation: dash 0.3s linear 1 both;
            -webkit-animation: dash 0.3s linear 1 both;
            -o-animation: dash 0.3s linear 1 both;
            -mz-animation: dash 0.3s linear 1 both;
            animation: dash 0.3s linear 1 both;
        }

        @keyframes dash {
            to {
                margin-left: 1100px;
            }
        }

        @-moz-keyframes dash {
            to {
                margin-left: 1100px;
            }
        }

        @-webkit-keyframes dash {
            to {
                margin-left: 1100px;
            }
        }

        @-o-keyframes dash {
            to;

        {
            margin-left: 1100px;
        }

        }

        @-ms-keyframes dash {
            to {
                margin-left: 1100px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- backTop start -->
        <div class="backTop"><a href="javascript:;" class="btnTop"></a></div>
        <!-- backTop end -->
        <!--header start-->
        <uc1:Header runat="server" ID="Header" />
        <!--header end-->
        <!--main start-->
        <div class="mains">
            <div class="wrap">
                <div class="title">到达方式 </div>
                <div class="traffic">
                    <div class="ddfs">
                        <ul class="navlist1">
                            <li style="margin-top: 23px;">
                                <a href="javascript:" onmousedown="changeImg(1)"></a>
                                <div id="opa1"></div>
                            </li>
                            <li>
                                <a href="javascript:" onmousedown="changeImg(2)"></a>
                                <div id="opa2"></div>
                            </li>
                            <li>
                                <a href="javascript:" onmousedown="changeImg(3)"></a>
                                <div id="opa3"></div>
                            </li>
                            <li>
                                <a href="javascript:" onmousedown="changeImg(4)"></a>
                                <div id="opa4"></div>
                            </li>
                        </ul>
                        <img id="img" src="" />
                    </div>
                    <div class="traffics">
                        <h2>「万菱汇交通」</h2>
                        <ul class="tfList">
                            <asp:Repeater ID="rptbanner" runat="server">
                                <ItemTemplate>
                                    <li <%#Container.ItemIndex+1==2?"class=\"li2\"":"" %>>
                                        <h2>
                                            <img src="/UploadFiles/images/<%#Eval("BigImg")%>" alt=""><%#Eval("Title") %>
                                        </h2>
                                        <p><%#Eval("ReachIntro") %></p>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!--main end-->
        <uc1:Footer runat="server" ID="Footer" />
        <!--footer end-->
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jqnav.js" type="text/javascript"></script>
        <script src="../js/SuperSlide.js" type="text/javascript"></script>
        <script>
            function changeImg(n) {
                var bigimg = document.getElementById("img");
                bigimg.src = "/images/ddfs" + n + ".gif";
                document.getElementById("opa" + n).style.display = "none";
                for (var i = 1; i <= 4; i++) {
                    if (i != n) {
                        document.getElementById("opa" + i).style.display = "block";
                    }
                }
            }
            changeImg(1)

        </script>
    </form>
</body>
</html>
