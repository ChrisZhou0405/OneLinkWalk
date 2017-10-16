<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="location.aspx.cs" Inherits="KingTop.WEB.Phone.shopping.location" %>

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
  <div class="mmain bgwhite">
     
    <div class="curr"><h3>购物指南</h3></div>  
    <ul class="shopnavlist">
        <li><a href="javascript:void(0)"><span></span>按类别</a> 
          <div class="sch-shopping">
              <select class="actxt1 schtxt2">
                  <option>全部</option>
              </select>
          </div>
        </li>
        <li><a href="javascript:void(0)"><span></span>按字母</a>
           <div class="sch-shopping">
              <select class="actxt1 schtxt2">
                  <option>A - D</option>
                  <option>E - F</option>
                  <option>I  - L</option>
                  <option>M - P</option>
                  <option>Q - T</option>
                  <option>U - X</option>
                  <option>Y - Z</option>
              </select>
          </div>
        </li>
        <li><a href="javascript:void(0)"><span></span>按位置</a>
          <div class="sch-shopping">
              <select class="actxt1 schtxt2 schtxt3">
                  <option>B1</option>
              </select>
               <select class="actxt1 schtxt2 schtxt4">
                  <option>屈臣氏</option>
              </select>
          </div>
        </li>
        <li><a href="recommend.html"><span></span>推荐店铺</a></li>
    </ul> 
    
    
   <div class="actdt">
     <p><img src="../images/qupic1.jpg"></p>
     <div class="shop">
         <div class="pic"><img src="/Phone/images/qupic2.jpg"></div>
         <div class="txt">
            <p>店铺名称：Pandora</p>
            <p>位置：L1-37</p>
            <p>电话：010-8888 8888</p>
            <p>销售产品：钻石、珠宝</p>
            <p>网址：www.Pandora.com</p>
        </div>
     </div>
     <p>屈臣氏是亚洲享负盛名的保健及美妆零售商，业务遍布11个亚洲及欧洲市场，包括中国（中国内地、香港、台湾及澳门）宾、印度尼西...</p>
     
     <div class="lookup"><a href="detail.html">了解详情</a></div>
   </div> 
   
   
    
  </div> 
  

</section>

    <uc1:footer runat="server" ID="footer" />


<div class="bglayout" id="bg"></div>
<div class="share" id="sharebg">
    <a href="#"><img src="/Phone/images/fxicon1.png"></a>
    <a href="#"><img src="/Phone/images/fxicon2.png"></a>
    <a href="#"><img src="/Phone/images/fxicon3.png"></a>
</div>


<script src="/Phone/js/jquery.js"></script>
<script src="/Phone/js/touch.min.js"></script>
<script src="/Phone/js/jqnav.js"></script>




 </body>
</html>
