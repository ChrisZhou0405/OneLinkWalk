<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rights.aspx.cs" Inherits="KingTop.WEB.Phone.vip.rights" %>

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
  <div class="mmain bgwhite">
     <div class="curr"><h3>会员专区</h3></div>
     <div class="navinput subnav">
           <span class="jiao"></span><p class="set">精选优品·会员权益</p>
          <ul class="sublist new">
           <li><a href="integral.html">会员积分</a></li>
           <li><a href="change.html">换领礼品</a></li>
           <li><a href="rights.html">精选优品·会员权益</a></li>
           <li><a href="order.html">条款及细则</a></li></ul>
           </div>
        

        <div id="focus" class="focus">
            <div class="hd">
                <ul></ul>
            </div>
            <div class="bd">
                <ul>
                <li><a href="#"><img src="../images/vpic5.jpg"></a></li>
                <li><a href="#"><img src="../images/vpic5.jpg"></a></li>
                <li><a href="#"><img src="../images/vpic5.jpg"></a></li>
                </ul>
            </div>
        </div>


           <div id="focus2" class="focus focus2 focus4">
            <div class="hd">
                <ul></ul>
            </div>
            <div class="bd">
                <ul>
                <li>
                  <div class="lidiv"><a href="#"><img src="../images/vpic6.jpg"><span>年度 9 折优惠</span></a></div>
                  <div class="lidiv"><a href="#"><img src="../images/vpic6.jpg"><span>品牌 8.5 折优惠</span></a></div>
                </li>
                <li>
                  <div class="lidiv"><a href="#"><img src="../images/vpic6.jpg"><span>年度 9 折优惠</span></a></div>
                  <div class="lidiv"><a href="#"><img src="../images/vpic6.jpg"><span>品牌 8.5 折优惠</span></a></div>
                </li>
                <li>
                  <div class="lidiv"><a href="#"><img src="../images/vpic6.jpg"><span>年度 9 折优惠</span></a></div>
                  <div class="lidiv"><a href="#"><img src="../images/vpic6.jpg"><span>品牌 8.5 折优惠</span></a></div>
                </li>
                </ul>
                <span class="prev"></span>
		       <span class="next"></span>
            </div>
        </div>
        <div class="vip">
            <div class="cheep">
                <h3>[ 商户折扣 ]</h3>
                <p>万菱汇会员在以下指定商户享受专属折扣优惠（以万菱汇现场最终公示为准）</p>
                
               <table width="100%" border="0" cellspacing="0" cellpadding="0" class="viptable">
              <tr>
                <th width="40%">品牌</th>
                <th></th>
                <th>折扣</th>
              </tr>
              <tr>
                <td><img src="../images/bdpic1.png"></td>
                <td><p>BALLY</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">9 折</span></td>
              </tr>
              
               <tr>
                <td><img src="../images/bdpic2.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              
               <tr>
                <td><img src="../images/bdpic3.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              
              <tr>
                <td><img src="../images/bdpic4.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              
              <tr>
                <td><img src="../images/bdpic5.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              <tr>
                <td><img src="../images/bdpic1.png"></td>
                <td><p>BALLY</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">9 折</span></td>
              </tr>
              
               <tr>
                <td><img src="../images/bdpic2.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              
               <tr>
                <td><img src="../images/bdpic3.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              
              <tr>
                <td><img src="../images/bdpic4.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>
              
              <tr>
                <td><img src="../images/bdpic5.png"></td>
                <td><p>ZARA</p><p>位置：B1-301</p></td>
                <td><span class="f15 red">8.5 折</span></td>
              </tr>              
            </table>
            
            <div class="pages">
         <a href="#">< 上一页</a>
         <span><b>6</b> / 9</span>
         <a href="#">下一页 ></a>
       </div> 
            </div>     
        </div>

     </div>
</section>

    <uc1:footer runat="server" ID="footer" />


<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>
<script src="/Phone/js/TouchSlide.1.0.js"></script>
<script>
    TouchSlide({
        slideCell: "#focus",
        titCell: ".hd ul", //开启自动分页 autoPage:true ，此时设置 titCell 为导航元素包裹层
        mainCell: ".bd ul",
        effect: "leftLoop",
        autoPlay: true,//自动播放
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
