﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="share.ascx.cs" Inherits="KingTop.WEB.Controls.share" %>
<script src="../js/jquery.js"></script>

<div id="shareweixin" style="border: 1px solid rgba(0, 0, 0, 0.298039); box-shadow: rgba(0, 0, 0, 0.298039) 0px 3px 7px; margin: -200px 0px 0px -200px; font-size: 14px; z-index: 2222; width: 250px; left: 50%; position: fixed; overflow: hidden; top: 50%; height: 260px; display: block; background-color: rgb(255, 255, 255); background-clip: padding-box; display:none">
<div style="padding:9px 15px;border-bottom:1px solid #eeeeee;text-align:left"><a target="_self" onclick="sharetoweixin_cancel()" id="jiathis_weixin_close" class="jiathis_weixin_close" style="text-decoration:none;  margin-top: 2px; color: #000000; float: right;  font-size: 20px;  font-weight: bold; cursor:pointer;line-height: 20px; opacity: 0.2; text-shadow: 0 1px 0 #FFFFFF;">×</a><h3 style="line-height:30px;font-weight:normal">分享到微信朋友圈</h3></div>
<div style="text-align:center;height:118px;"><div style="width:220px;height:220px;margin-top:15px;width:100%;"><p id="qrcode" title="http://www.mstar.cn/"><canvas width="220" height="220" style="display: none;"></canvas><img alt="Scan me!" style="padding-left:10px;width:110px;height:110px;" src="/uploadfiles/images/1476839221.png"></p></div></div>
<div style="padding:11px 10px 0;"><div style="font-size:12px;margin:0;padding:0px;text-align:left;">打开微信，点击底部的“发现”，使用 “扫一扫” 即可将网页分享到我的朋友圈。</div></div>
</div>
