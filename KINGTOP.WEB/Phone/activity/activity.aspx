<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activity.aspx.cs" Inherits="KingTop.WEB.Phone.activity.activity" %>

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
     
    <div class="curr"><h3>活动推介</h3></div>  
        
    <ul class="actnavlist">
        <li><a href="/Phone/activity/index.aspx">最新活动</a></li>
        <li class="hover"><a href="/Phone/activity/activity.aspx">活动日志</a></li>
     </ul>
    
    <div class="act-sch">
    
        <div class="navinput subnav actxt1">
           <span class="jiao"></span><p class="set">年</p>
              <ul class="sublist new" id="y">
              <%=this.yearStr %>
              </ul>
         </div>
         
         
         <div class="navinput subnav actxt1">
           <span class="jiao"></span><p class="set">月</p>
              <ul class="sublist new" id="m">
               <%=this.monthStr %>
              </ul>
         </div>

      <input id="lookup" name="" type="button" value="查找往期活动" class="actschbtn">
    </div> 
    
    
    <ul class="actlist" id="actlist">
        <%if (this.did != "")
          {

          }
          else
          {
             Response.Write(this.lisStr);
          } %>
    
    </ul>   
    
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
<script src="../js/share.js"></script>


 </body>
</html>

<script type="text/javascript">
    $(function () {
        var did = "<%=this.did.ToString()%>";
      
        if( did !="")
        {
          
            getActivityDetailF(did);
        }
  
    var y = "-1";
    var m = "-1";
    $("#y li").click(function () {
        y= ($(this).val());
    });
    $("#m li").click(function () {
         m=($(this).val());
    });


    $("#lookup").click(function () {

        $.get("/Phone/activity/getActivity.ashx", { y: y, m: m },
         function (data) {
             var dataObj = eval("(" + data + ")");
             if (dataObj.statu == "ok") {
                 if ($("#actdt"))
                 {
                     $("#actdt").remove();
                 }
                 if ($("#actlist")) {
                     $("#actlist").remove();
                 }
               
                     $("#mmain").append('<ul class="actlist" id="actlist"> </ul>');
                

                     
                 $("#actlist").html("");
                
                 $("#actlist").html("" + dataObj.data + "");
                 actlistClick();
               
                
             
             }
         });

       
    });
        actlistClick();
    })

   


    function actlistClick()
    {
        if("#actlist"){
            $("#actlist li").click(function(){
                var id = $(this).attr('id');
                getActivityDetailF(id)
            });
        }

    }

    function getActivityDetailF(id)
    {
        $.get("/Phone/activity/getActivityDetail.ashx", { id: id },
                          function (data) {
                              var dataObj = eval("(" + data + ")");
                              if (dataObj.statu == "ok") {
                                  $("#actlist").remove();
                                  $("#mmain").append("" + dataObj.data + "");
                                  sharebtnClick();
                              }
                          });
    }

    function sharebtnClick() {
        $("#sharebtn").click(function () {
            $("#bg").show();
            $("#sharebg").show();
        })
    }

   
   

</script>
