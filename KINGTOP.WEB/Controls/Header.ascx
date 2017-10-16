<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="KingTop.WEB.Controls.Header" %>
<div class="header <%=HeadCss %>" >
    <div class="wrap por">
        <a href="/index.aspx" class="logo">
            <img src="/images/logo.png" alt=""></a>
        <!-- searBarBg start -->
        <div class="topBar">
            <span class="searBar">
                <input type="text" placeholder="请输入关键字" id="searchkey" /><input type="button" id="searchbtn"></span>
            <span class="shear">
                <a class="wx" href="javascript:void(0)">
                    <span class="wx_code">
                        <img style="width:166px;height:166px" src="/images/ico/weixin_code.jpg"></span>
                </a>
                <a class="email" href="/about/contact.aspx"></a>
                <a class="lang" href="/index.aspx">
                   <%-- <span class="lang_code">中</span>--%>
                </a>
            </span>
        </div>
        <!-- searBarBg end -->
        <ul class="navList">
            <li><a href="/shopping/index.aspx">购物指南</a>
                <span>
                    <asp:Repeater ID="RptRelated" runat="server">
                        <ItemTemplate>
                            <a href="/shopping/index.aspx?id=<%#Eval("ID") %>" class="c_a<%#Container.ItemIndex+1 %>"><%#Eval("Title")%></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </span>
            </li>
            <li><a href="/cate/index.aspx">美食荟萃</a>
                <span>
                    <a href="/cate/index.aspx">全部美食</a>
                    <a href="/cate/index.aspx?sx=1">亚洲美食</a>
                    <a href="/cate/index.aspx?sx=2">中式佳肴</a>
                    <a href="/cate/index.aspx?sx=3">西方美馔</a>
                    <a href="/cate/index.aspx?sx=4">轻便美食/甜点</a>
                </span></li>
            <li><a href="/activity/index.aspx?sx=0">活动推介</a>
                <span><a href="/activity/index.aspx?sx=0">最新活动</a>
                    <a href="/activity/index.aspx?sx=1">活动日志</a>
                </span></li>
            <li><a href="/vip/nonmember.aspx?node=101005001">会员专区</a>
                <span><a href="/vip/nonmember.aspx?node=101005001">会员招募</a></span></li>
            <li><a href="/serve/index.aspx">服务与设施</a>
                <span>
                    <a href="/serve/index.aspx?sx=0">咨询中心</a>
                    <a href="/serve/index.aspx?sx=1">停车服务</a>
                    <a href="/serve/index.aspx?sx=2">ATM设施</a>
                    <a href="/serve/index.aspx?sx=3">电动汽车充电</a>
                    <a href="/serve/index.aspx?sx=4">母婴室</a>
                </span></li>
            <li><a href="/about/index.aspx">关于我们</a>
                <span>
                    <a href="/about/index.aspx">项目概述</a>
                    <a href="/about/traffic.aspx">到达方式</a>
                    <a href="/about/contact.aspx">联系我们</a>
                    <a href="/about/lease.aspx">场地与商铺租赁</a></span>
            </li>
        </ul>
    </div>
</div>
<script src="../js/jquery.js"></script>
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
            alert("您输入的内容存在特殊字符!");
            return false;
        }
        var cid = "{@cid,false,0}";
        alert(searchinput);
      
        window.location.href  = "/search.aspx?key=" + searchinput;
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


            window.location.href = "/search.aspx?key=" + name;

        });

        function golink(v)
        {
            window.location.href = "/search.aspx?key=" + v;
            window.event.returnValue = false;
        }

    
</script>
