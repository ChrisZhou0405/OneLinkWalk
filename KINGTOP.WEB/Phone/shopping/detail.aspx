<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="KingTop.WEB.Phone.shopping.detail" %>

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
<link rel="stylesheet" type="text/css" href="/Phone/css/style.css">
<title></title>
</head>
<body>


    <uc1:navbox runat="server" ID="navbox" />

<section>
  <div class="mmain bgwhite" id="mmain">
     
    <div class="curr"><h3>购物指南</h3></div>  
   <ul class="shopnavlist">
        <li><a href="javascript:void(0)"><span class="icon"></span>按类别</a> 
          <div class="sch-shopping">
              <div class="navinput subnav schtxt2">
               <span class="jiao"></span><p class="set">全部</p>
                  <ul id="cg" class="sublist new">
                     <%=this.category %>
                  </ul>
         </div>
          </div>
        </li>
        <li><a href="javascript:void(0)"><span class="icon"></span>按字母</a>
           <div class="sch-shopping">
           <div class="navinput subnav schtxt2">
           <span class="jiao"></span><p class="set">请选择</p>
              <ul id="letter" class="sublist new">
                  <li id="1,2,3,4">A - D</li>
                  <li id="5,6,7,8">E - H</li>
                  <li id="9,10,11,12">I  - L</li>
                  <li id="13,14,15,16">M - P</li>
                  <li id="17,18,19,20">Q - T</li>
                  <li id="21,22,23,24">U - X</li>
                  <li id="21,25,26">Y - Z</li>
              </ul>
         </div>
         
          
          </div>
        </li>
        <li><a href="javascript:void(0)"><span class="icon"></span>按位置</a>
          <div class="sch-shopping">              
          <div class="navinput subnav schtxt2 schtxt3">
           <span class="jiao"></span><p class="set">B1</p>
              <ul id="floor1" class="sublist new">
                  <li id="101002003001">B1</li>
                  <li id="101002003002">L1</li>
                  <li id="101002003003">L2</li>
                  <li id="101002003004">L3</li>
                  <li id="101002003005">L4</li>
                  <li id="101002003006">L5</li>
              </ul>
          </div>
          
          <div class="navinput subnav schtxt2 schtxt4">
           <span class="jiao"></span><p class="set">请选择店铺</p>
              <ul id="sshop" class="sublist new">
            
               <%=this.shops %>
              </ul>
          </div>
          </div>
        </li>
        <li><a href="javascript:void(0);"  id="r"><span class="icon"></span>推荐店铺</a></li>
    </ul> 
    
    

     <%=this.shopDetail %>
   
  
      <%=this.likeShopStr %> 
  </div> 
    


</section>

    <uc1:footer runat="server" ID="footer" />

<div class="bglayout" id="bg"></div>
<div class="share" id="sharebg">
    <a id="aqq"><img src="/Phone/images/fxicon1.png"></a>
    <a id="awx" ><img src="/Phone/images/fxicon2.png"></a>
    <a id="wb" ><img src="/Phone/images/fxicon3.png"></a>

</div>


    <div id="shareweixin" style="border: 1px solid rgba(0, 0, 0, 0.298039); box-shadow: rgba(0, 0, 0, 0.298039) 0px 3px 7px; margin: -200px 0px 0px -200px; font-size: 14px; z-index: 2222; width: 250px; left: 50%; position: fixed; overflow: hidden; top: 50%; height: 260px; background-color: rgb(255, 255, 255); background-clip: padding-box; display:none;">
<div style="padding:9px 15px;border-bottom:1px solid #eeeeee;text-align:left"><a target="_self" onclick="sharetoweixin_cancel()" id="jiathis_weixin_close" class="jiathis_weixin_close" style="text-decoration:none;  margin-top: 2px; color: #000000; float: right;  font-size: 20px;  font-weight: bold; cursor:pointer;line-height: 20px; opacity: 0.2; text-shadow: 0 1px 0 #FFFFFF;">×</a><h3 style="line-height:30px;font-weight:normal">分享到微信朋友圈</h3></div>
<div style="text-align:center;height:118px;"><div style="width:220px;height:220px;margin-top:15px;width:100%;"><p id="qrcode" title="http://www.mstar.cn/"><canvas width="220" height="220" style="display: none;"></canvas><img alt="Scan me!" style="padding-left:10px;width:110px;height:110px;" src="/uploadfiles/images/1476839221.png"></p></div></div>
<div style="padding:11px 10px 0;"><div style="font-size:12px;margin:0;padding:0px;text-align:left;">打开微信，点击底部的“发现”，使用 “扫一扫” 即可将网页分享到我的朋友圈。</div></div>
</div>


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

    TouchSlide({
        slideCell: "#focus2",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: false,//自动播放
        autoPage: true //自动分页
    });

</script>



 </body>
</html>

<script type="text/javascript">
    $(function () {

      
        sshopLiClick();
        $("#floor1 li").click(function () {
            var nc = $(this).attr('id');
          
            getShops(nc);


        });

        $("#letter li").click(function () {
            var letters = $(this).attr('id');
            getShopL(letters);


        });

        $("#cg li").click(function () {
            var cgId = $(this).attr('id');
         
            getShopC(cgId);
        });

        $("#r").click(function (event) {
            event.preventDefault();
            recommend();
        });
    });

    function getShopd(shopno) {
        $.get("/Phone/shopping/getShopD.ashx", { shopno: shopno },
          function (data) {
              $("#sshop").css('display', 'none');
          
              var dataObj = eval("(" + data + ")");
              if (dataObj.statu == "ok") {

                  del();
                  if ($("#actdt")) {
                      $("#actdt").remove();
                  }
                  $("#catelist").remove();
                  $("#mmain").append(dataObj.data);

              } else {
                  alert("没有相关的数据");
              }

          });

    }

    function getShops(nc) {
        $.get("/Phone/shopping/getShopS.ashx", { nc: nc },
         function (data) {
             var dataObj = eval("(" + data + ")");
             if (dataObj.statu == "ok") {
                 //del();
                 $("#sshop").html("");
                 $("#sshop").html(dataObj.data);
                 sshopLiClick();
             }

         });
    }

    function getShopL(letters) {

        $.get("/Phone/shopping/getShopL.ashx", { lq: letters },
         function (data) {
             var dataObj = eval("(" + data + ")");
             if (dataObj.statu == "ok") {
                 del();
                 if ($("#catelist")) {
                     $("#catelist").remove();

                 }
                 $("#mmain").append(dataObj.data);

             }

         });
    }
    function getShopC(cgId) {

        $.get("/Phone/shopping/getShopC.ashx", { cid: cgId },
         function (data) {
             var dataObj = eval("(" + data + ")");
             if (dataObj.statu == "ok") {
                 del();
                 if ($("#catelist")) {
                     $("#catelist").remove();

                 }
                 $("#mmain").append(dataObj.data);

             }

         });
    }

    function recommend() {
      
        $.get("/Phone/shopping/getRecommend.ashx",
         function (data) {
           

             var dataObj = eval("(" + data + ")");
             if (dataObj.statu == "ok") {
                 del();
                 if ($("#catelist")) {
                     $("#catelist").remove();

                 }
                 $("#mmain").append(dataObj.data);
                 TS();
             }

         });
    }

    function sshopLiClick()
    {
        $("#sshop li").click(function () {
            var shopno = $(this).attr('id');
            var shopName = $(this).html();
            $("#setShop").html(shopName);
            getShopd(shopno);
        });
    }


    //开始自动分页
    function TS() {
        TouchSlide({
            slideCell: "#focus2",
            titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
            mainCell: ".bd ul",
            effect: "leftLoop",
            autoPlay: false,//自动播放
            autoPage: true //自动分页
        });
    }

    function del() {
        if ($("#actdt")) {
            $("#actdt").remove();
        }
        if ($("#liketit")) {
            $("#liketit").remove();
        }
        if ($("#focus2")) {
            $("#focus2").remove();
        }
        if ($(".recommendlist")) {
            $(".recommendlist").remove();
        }
        if ($(".lookup")) {
            $(".lookup").remove();
        }
        if ($("#catelist")) {
            $("#catelist").remove();
        }
        if (".like_tit") {
            $(".like_tit").remove();
        }
        if (".actdt") {
            $(".actdt").remove();
        }

    }
</script>

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

    TouchSlide({
        slideCell: "#focus2",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: false,//自动播放
        autoPage: true //自动分页
    });

</script>

<script src="../js/share.js"></script>
