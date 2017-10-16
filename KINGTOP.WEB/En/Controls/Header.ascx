<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="KingTop.WEB.En.Controls.Header" %>
<div class="header <%=HeadCss %>" >
    <div class="wrap por">
        <a href="/En/index.aspx" class="logo">
            <img src="/En/images/logo.png" alt=""></a>
        <!-- searBarBg start -->
        <div class="topBar">
            <span class="searBar">
                <input type="text" placeholder="Search" id="searchkey" /><input type="button"  id="searchbtn"></span>
            <span class="shear">
                <a class="wx" href="javascript:void(0)">
                    <span class="wx_code">
                        <img style="width:166px;height:166px" src="/En/images/ico/weixin_code.jpg"></span>
                </a>
                <a class="email" href="/En/about/contact.aspx"></a>
                <a class="lang" href="/En/index.aspx">
                   <%-- <span class="lang_code">中</span>--%>
                </a>
            </span>
        </div>
        <!-- searBarBg end -->
        <ul class="navList">
            <li><a href="/En/shopping/index.aspx">Shopping</a>
                <span>
                    <asp:Repeater ID="RptRelated" runat="server">
                        <ItemTemplate>
                            <a href="/En/shopping/index.aspx?id=<%#Eval("ID") %>" class="c_a<%#Container.ItemIndex+1 %>"><%#Eval("Title")%></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </span>
            </li>
            <li><a href="/En/cate/index.aspx">Dining</a>
                <span>
                    <a href="/En/cate/index.aspx">All Delights</a>
                    <a href="/En/cate/index.aspx?sx=1">Asian Delights</a>
                    <a href="/En/cate/index.aspx?sx=2">Chinese Cuisine</a>
                    <a href="/En/cate/index.aspx?sx=3">Western Delicacies</a>
                    <a href="/En/cate/index.aspx?sx=4">Deli / Dessert</a>
                </span></li>
            <li><a href="/En/activity/index.aspx?sx=0">Happenings</a>
                <span><a href="/En/activity/index.aspx?sx=0">Events & Promotions</a>
                    <a href="/En/activity/index.aspx?sx=1">Event Calendar</a>
                </span></li>
            <li><a href="/En/vip/nonmember.aspx?node=104005001">Member Zone</a>
                <span><a href="/En/vip/nonmember.aspx?node=104005001">Member Recruitment</a></span></li>
            <li><a href="/En/serve/index.aspx">Services & Facilities</a>
                <span>
                    <a href="/En/serve/index.aspx?sx=0">Information</a>
                    <a href="/En/serve/index.aspx?sx=1">Parking</a>
                    <a href="/En/serve/index.aspx?sx=2">ATM</a>
                    <a href="/En/serve/index.aspx?sx=3">Electric Vehicle Charging Service</a>
                    <a href="/En/serve/index.aspx?sx=4">Infant Room</a>
                </span></li>
            <li><a href="/En/about/index.aspx">About Us</a>
                <span>
                    <a href="/En/about/index.aspx">About OneLink Walk</a>
                    <a href="/En/about/traffic.aspx">How to Get There</a>
                    <a href="/En/about/contact.aspx">Contact Us</a>
                    <a href="/En/about/lease.aspx">Leasing</a></span>
            </li>
        </ul>
    </div>
</div>
<%--<script type="text/javascript">
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
            alert("There are special characters in the text you entered!");
            return false;
        }
        var cid = "{@cid,false,0}";
        window.location = "/En/search.aspx?key=" + escape(stripscript(searchinput));
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
</script>--%>



<script type="text/javascript">


    $("#searchkey").keypress(function (event) {
        if (key != "") {

        }
        var key = event.which;//e.which是按键的值
        if (key == 13) {
            var name = $("#searchkey").val();
            golink(name);

        }
    })

    $("#searchbtn").click(function () {

        var name = $("#searchkey").val();


        window.location.href = "/En/search.aspx?key=" + name;

    });

    function golink(v) {
        window.location.href = "/En/search.aspx?key=" + v;
        window.event.returnValue = false;
    }


</script>
