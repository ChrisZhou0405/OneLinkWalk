<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.Phone.shopping.index" %>

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
<link rel="stylesheet" type="text/css" href="../css/style.css">
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
           <span class="jiao"></span><p id="setShop" class="set">请选择店铺</p>
              <ul id="sshop" class="sublist new">
             
               <%=this.shops %>
              </ul>
          </div>
          </div>
        </li>
      
         <li><a href="javascript:void(0);"  id="r"><span class="icon"></span>推荐店铺</a></li>
    </ul> 
    
    
     <ul class="catelist" id="catelist">
            <%=this.catelist %>
       </ul>  

  </div> 
  

</section>


<uc1:footer runat="server" ID="footer" />

<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>

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

        linkClick();

        
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

    function sshopLiClick()
    {
        $("#sshop li").click(function () {
            var shopno = $(this).attr('id');
            var shopName = $(this).html();
            $("#setShop").html(shopName);

            getShopd(shopno);
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

    function linkClick()
    {
        var idStr = "<%=this.id%>";
        if(idStr !="")
        {
            $("#" + idStr).click();
        }

    }
</script>

<script src="/Phone/js/TouchSlide.1.0.js"></script>
