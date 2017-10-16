<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KingTop.WEB.SysAdmin.console.index1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>KingTop CMS -- 图派科技</title>
      <link href="/sysadmin/lib/LigerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="/sysadmin/js/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="/sysadmin/lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <link href="/sysadmin/lib/style.css" rel="stylesheet" type="text/css" />
    <link href="/sysadmin/CSS/style1.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         <%=dataArr %>
         
         var leftWidth = 210;
         var rightWidth = 0;
         //初始化树
         function f_initTree()
         {
            <%= stringdata %>

            for(var i=1;i<<%=j+1 %>;i++)
            {
                 $("#tree"+i).ligerTree({
                     data: eval("data"+(i-1)),
                     callback: function (item) {
                         if (!item.url || !item.tabid) return;
                         f_addTabItem(item.tabid, item.url,item.text);
                     }
                 });
               
            }
         }
         function f_addTabItem(tabid, url, text)
         {
             $("body")[0].manager.addTabItem(tabid, url, text);
         }
         $(function ()
         {
             f_initTree();
             $.ligerFrame({ tabItems: [{ tabid: 'home', url: 'Main.aspx', text: '我的主页', showClose: false}],
                 leftHeader: '切换站点：<%=strSite %>'
             }); 
         });  
         function f_closeWindow(frame)
         { 
             for (var i = 0; i < document.frames.length; i++)
             {
                 if (document.frames[i] == frame)
                 { 
                     $(".l-window[framename=" + document.frames[i].name + "]").remove();
                     return;
                 }
             }
         }
         function f_openWindow(url, title,width, height)
         {
             return $.ligerWindow.show({ width: width, height: height, left: 100, top: 100, title: title, url: url });
         }
         function f_open(url)
         {
             $.ligerWindow.show({ width: 300, height: 300,left:100,top:100, title: '我的标题', url: url });
         }
         function f_openWithContent(content)
         {
             var chtml = "<div style='padding:10px'>" + content + "</div>";
             $.ligerWindow.show({ width: 300, height: 300, left: 200, top: 200, title: '我的标题', content: chtml });
         }
         
            function f_addTabItem1(url, text)
        {
            var tabid = "ligerui" + new Date().toDateString();
            f_addTabItem(tabid, url,text);
        }
        
        function treecontent(number)
        {
            var obj=$(".l-frame-left").find(".l-accordion-toggle");
            for(var i=0;i<obj.length;i++)
            {
                if(number==i)
                {
                    $("#treecontent"+i).css("display","block");
                    $(obj[i]).addClass("l-accordion-toggle-open");
                    $(obj[i]).removeClass("l-accordion-toggle-close");
                    }
                else
                {
                    $("#treecontent"+i).css("display","none");
                    $(obj[i]).removeClass("l-accordion-toggle-open");
                    $(obj[i]).addClass("l-accordion-toggle-close");
                    }
            }
        }
        
        $(function()
        {
            $(".l-frame-left").find(".l-accordion-header").each(function (){
                  $(this).click(function(){
                      $(".l-frame-left").find(".l-accordion-toggle-open").each(function(){
                        $(this).parent().next().css("display","none");
                        $(this).removeClass("l-accordion-toggle-open");
                        $(this).addClass("l-accordion-toggle-close");
                      });
                      $(this).children().first().removeClass("l-accordion-toggle-close");
                      $(this).children().first().addClass("l-accordion-toggle-open");
                      $(this).next().css("display","block");
                  });
              });
        });
       
     </script>
     
     
   
    <style type="text/css">
        body{ padding:0; margin:0; background:#E5EDEF;}
    </style>
</head>
<body>

<div class="wrap">
      <!-- head star-->
      <div class="l-frame-top" >
          <div class="head headbg">    
	      <h1><img src="../images/logo1.png" width="260" height="28" /></h1>
         <div class="nav">
         	<ul class="navlist">
         	    <%=strNode%>
            	<%--<li  id="aa1" class="hover">我的工作台</li>
            	<li  id="aa2" >内容管理</li>
            	<li  id="aa3" >系统管理</li>--%>
            </ul>
         </div>
         <div class="rhead">
         <div class="admin"><strong><%=GetLoginAccountName()%></strong>:所属用户组: <span class="oo"><%=strUserGrop%></span></div>
         <div class="admin2"> <a href="../logout.aspx"><img src="../images/zhuxiao.jpg" width="16" height="15" align="top" /> 注销</a> <a href="http://www.toprand.com/feedback/index.asp"> <img src="../images/help.jpg" width="17" height="15" align="absmiddle" /> 帮助</a> <a href="#"><img src="../images/home.jpg" width="14" height="15"  align="absmiddle"/> 网站首页</a></div>
         
         </div>
         
   </div></div><!-- head over-->
		<div class="l-frame-main" >
    <div class="l-frame-left"> 
         <div class="l-frame-left-content" >
         <%=strNodes%>
         </div>
    </div>
</div>
<div class="l-frame-footer">
<p><span style="float:right">Copyright &copy; 2012 Toprand.com All rights reserved.&nbsp;&nbsp;</span><span style="float:left">&nbsp;&nbsp;客服邮箱：<a href="mailto:support@toprand.com" target="_blank">support@toprand.com</a>&nbsp;&nbsp;&nbsp;服务网站：<a href="http://www.toprand.com/" target="_blank">www.toprand.com</a></span></p> 

</div>
</div>
	    






</body>
</html>
