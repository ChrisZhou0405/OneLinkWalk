
//发送给QQ好友
function sharetoqqfriend() {
    var p = {
        url: window.location.href, /*获取URL，可加上来自分享到QQ标识，方便统计*/
        desc: '欢迎来到万菱汇集团官网，有空可以上去看看！', /*分享理由(风格应模拟用户对话),支持多分享语随机展现（使用|分隔）*/
        title: '万菱汇集团', /*分享标题(可选)*/
        summary: '', /*分享摘要(可选)*/
        pics: '', /*分享图片(可选)*/
        flash: '', /*视频地址(可选)*/
        site: '广东万菱汇集团官网' /*分享来源(可选) 如：QQ分享*/
    };
    var param = "";
    for (var i in p) {
        param += "&" + i + "=" + p[i];
    }
    if (param != "") {
        param = param.substr(1);
    }
    window.open("http://connect.qq.com/widget/shareqq/index.html?" + param);
}

//分享到新浪微博
function sharetosina() {
    var sharesinastring = 'http://v.t.sina.com.cn/share/share.php?title=欢迎来到万菱汇集团官网，有空可以上去看看&url=' + location.href + '&content=utf-8&sourceUrl=/En/index.aspx&pic=';
    window.open(sharesinastring, "_blank", "width=650,height=450");
}


$("#aqq").click(function () {
    sharetoqqfriend();
});

$("#wb").click(function () {
    sharetosina();
});
$("#awx").click(function () {
    $('#shareweixin').css('display', 'block');
});


$('#adp').click(function () {
    var d=document,e=encodeURIComponent,s1=window.getSelection,s2=d.getSelection,s3=d.selection,s=s1?s1():s2?s2():s3?s3.createRange().text:'',r='http://www.douban.com/recommend/?url='+e(d.location.href)+'&title='+e(d.title)+'&sel='+e(s)+'&v=1',x=function(){if(!window.open(r,'douban','toolbar=0,resizable=1,scrollbars=yes,status=1,width=450,height=330'))location.href=r+'&r=1'};if(/Firefox/.test(navigator.userAgent)){setTimeout(x,0)}else{x()}
});

function sharetoweixin_cancel() {
    $('#shareweixin').css('display', 'none');


   
}