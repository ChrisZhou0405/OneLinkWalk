$(document).ready(function(){
    //navSub();
    menus();
  reSize();
  sideBar();
  logos();
  lMap();
  tab_tit();
  filtrateGift();
  $(window).resize(function() {
    reSize();
  });
  $('.timesList li:nth-child(3n)').css('margin-right', '0');
  $("#ban_no").hide(0);
  wxShow();
  langShow();
});
/* back top */
$(window).scroll(function(){
	var scrH = $(window).scrollTop();
	var scrB = $(document).height() - $(window).height();
     var $ft = $(window).height() - $('.mLeft').height();
     var $mh = $('.mLeft').height();
	/*if(scrH >= 700){
		$('.backTop').fadeIn();
	}else{
		$('.backTop').fadeOut(20);
	}
	if(scrH >= 538){
		$('.mleft').addClass('mfixed');
	}else{
		$('.mleft').removeClass('mfixed');
	}*/
	if( $ft <= 217 ){
       if( scrB - scrH < 175){
		  $('.mLeft').addClass('fpo');
	   }else{
		 $('.mLeft').removeClass('fpo');
	  }
	}	

if( $ft >= 0 ){$('.mLeft').removeClass('fpo');} 

});

$(".btnTop").click(function(){$("html,body").animate({scrollTop: '0px'}, 500);});

function menus() {
    // 线程 IDs
    var mouseover_tid = [];
    var mouseout_tid = [];

    $(".navList li").each(function (index) {
        $(this).hover(
                // 鼠标进入
                function () {
                    var _self = this;
                    // 停止卷起事件
                    clearTimeout(mouseout_tid[index]);
                    // 当鼠标进入超过 0.2 秒, 展开菜单, 并记录到线程 ID 中
                    mouseover_tid[index] = setTimeout(function () {
                        $(_self).children("a").addClass("on");
                        $(_self).find('span').stop(true, true).slideDown(200);
                    }, 20);
                },

                // 鼠标离开
                function () {
                    var _self = this;
                    // 停止展开事件
                    clearTimeout(mouseover_tid[index]);
                    // 当鼠标离开超过 0.2 秒, 卷起菜单, 并记录到线程 ID 中
                    mouseout_tid[index] = setTimeout(function () {
                        $(_self).children("a").removeClass("on");
                        $(_self).find('span').stop(true, true).slideUp(200);
                    }, 20);
                }

        );
    });

}

/* search */
//function navSub(){
//  $(".navList li").hover(function(){
//    var suba =$(this).children("a");
//    var subN =$(this).children("a").next("span");
//     if(suba.hasClass('on')){
//      suba.removeClass("on");
//      subN.stop(true,true).slideUp(250);
//    }else {
//      suba.addClass("on");
//      subN.stop(true,true).slideDown(250);
//    }

//    });
     
//};

function reSize() {
   var ws_h = document.body.clientHeight;
   var msr_w = $("").width();
   var ms_h = ws_h - 260;
   $(".mains").css('min-height', ms_h);
 }
/* sideBar */
function sideBar(){
   $('.sideBar li > a').click(function() {
     var pThis = $(this).parent();
     var hc = pThis.hasClass('s_on');
     if(!hc){
        pThis.addClass('s_on').children('div').slideDown().end().siblings().removeClass('s_on').children('div').slideUp();;
     }else {
       pThis.removeClass('s_on').children('div').slideUp();
     }
     // pThis.addClass('s_on').children('div').slideDown()
     // .end().siblings().removeClass('s_on').children('div').slideUp();
   });
   /*按类别*/
   $('.class_a > a').click(function() {
    var index = $(this).index();
    var c_num = 'ca_on' + index;
     $(this).addClass(c_num).siblings().removeClass(function() {
      return 'ca_on' + $(this).index();
    });
   });
    /*按字母*/
   $('.letter h2').click(function() {
         $('.letter h2').removeClass("p_on").next("p").slideUp()
       $(this).addClass("p_on").next("p").slideDown();


      // var hClass = $(this).hasClass('p_on');
      // if(!hClass){
      //   $(this).addClass("p_on").next("p").slideDown();
      // }else {
      //   $(this).removeClass("p_on").next("p").slideUp();

      // }
   });
   $('.letter span i').click(function() {
	   $('.letter span i').removeClass("i_on");
      $(this).addClass("i_on");
	  
   });
   /* 按位置 */
   $('.site a').click(function() {
      $(this).addClass("sa_on").siblings().removeClass("sa_on");
   });
}

/* logos */
function logos(){
 $('#logos>li').click(function() {
    var indexs = $(this).index('#logos>li') + 1;
    var $clonedCopy=$(this).children('.subLogos').clone(true).fadeIn();
    var indexLen = $('#logos>li').length;
    var pgnum= Math.ceil(indexs/4);

   $(this).siblings('.subLogos').remove();
   $(this).addClass('hover').siblings().removeClass('hover');
    if(indexLen>=pgnum*4){
        $('#logos li').eq(pgnum*4-1).after($clonedCopy);
     }
     else{
        $('#logos li').eq(indexLen-1).after($clonedCopy);
    }

});



 //cate tab logo list
$('#cLogo_1 > li').click(function() {
    var cLogo = $(this).index('#cLogo_1 > li');
    var $clonedCopyc=$(this).children('.subClogos').clone(true).fadeIn();
    var cLen = parseInt($('#cLogo_1 li').length);
    var last = cLen-1;
   $(this).siblings('.subClogos').remove();
   $(this).addClass('hover').siblings().removeClass('hover');

      if ( cLogo <= 4 ){$('#cLogo_1 li').eq(4).after($clonedCopyc);}
    else {$('#cLogo_1 li').eq(last).after($clonedCopyc);}
   if ( cLen <= 5 ){$('#cLogo_1 li').eq(last).after($clonedCopyc); }
   });

$('#cLogo_2 > li').click(function() {
    var cLogo = $(this).index('#cLogo_2 > li');
    var $clonedCopyc=$(this).children('.subClogos').clone(true).fadeIn();
    var cLen = parseInt($('#cLogo_2 li').length);
    var last = cLen-1;
   $(this).siblings('.subClogos').remove();
   $(this).addClass('hover').siblings().removeClass('hover');

      if ( cLogo <= 4 ){$('#cLogo_2 li').eq(4).after($clonedCopyc);}
    else {$('#cLogo_2 li').eq(last).after($clonedCopyc);}
   if ( cLen <= 5 ){$('#cLogo_2 li').eq(last).after($clonedCopyc); }
   });

$('#cLogo_3 > li').click(function() {
    var cLogo = $(this).index('#cLogo_3 > li');
    var $clonedCopyc=$(this).children('.subClogos').clone(true).fadeIn();
    var cLen = parseInt($('#cLogo_3 li').length);
    var last = cLen-1;
   $(this).siblings('.subClogos').remove();
   $(this).addClass('hover').siblings().removeClass('hover');

      if ( cLogo <= 4 ){$('#cLogo_3 li').eq(4).after($clonedCopyc);}
    else {$('#cLogo_3 li').eq(last).after($clonedCopyc);}
   if ( cLen <= 5 ){$('#cLogo_3 li').eq(last).after($clonedCopyc); }
   });

$('#cLogo_4 > li').click(function() {
    var cLogo = $(this).index('#cLogo_4 > li');
    var $clonedCopyc=$(this).children('.subClogos').clone(true).fadeIn();
    var cLen = parseInt($('#cLogo_4 li').length);
    var last = cLen-1;
   $(this).siblings('.subClogos').remove();
   $(this).addClass('hover').siblings().removeClass('hover');

      if ( cLogo <= 4 ){$('#cLogo_4 li').eq(4).after($clonedCopyc);}
    else {$('#cLogo_4 li').eq(last).after($clonedCopyc);}
   if ( cLen <= 5 ){$('#cLogo_4 li').eq(last).after($clonedCopyc); }
   });

$('#cLogo_5 > li').click(function() {
    var cLogo = $(this).index('#cLogo_5 > li');
    var $clonedCopyc=$(this).children('.subClogos').clone(true).fadeIn();
    var cLen = parseInt($('#cLogo_5 li').length);
    var last = cLen-1;
   $(this).siblings('.subClogos').remove();
   $(this).addClass('hover').siblings().removeClass('hover');

      if ( cLogo <= 4 ){$('#cLogo_5 li').eq(4).after($clonedCopyc);}
    else {$('#cLogo_5 li').eq(last).after($clonedCopyc);}
   if ( cLen <= 5 ){$('#cLogo_5 li').eq(last).after($clonedCopyc); }
   });

}


/*lMap*/
function lMap(){
    $('.btnSite').click(function() {$('.overbg,.lMap').fadeIn();});
    $('.lMap h1.close').click(function() {$('.overbg,.lMap').fadeOut();});
}

/*tab_tit*/
function tab_tit(){
  $('.tab_tit a').click(function() {
    // $(this).find('img.none').removeClass('none').siblings('img').addClass('none');
    // $(this).siblings().find('img.none').removeClass('none').siblings('img').addClass('none');
    var t_n = $(this).index();
    $(this).addClass('t_on').siblings().removeClass('t_on');
    $('.tab_txt').eq(t_n).stop(true,true).fadeIn(350).siblings('.tab_txt').stop(true,true).fadeOut(0);

  });
  $('.tab_tit2 a').click(function() {
    var t_n2 = $(this).index();
    $(this).addClass('t_on2').siblings().removeClass('t_on2');
    $('.tab_txt').eq(t_n2).stop(true,true).show().siblings('.tab_txt').stop(true,true).hide();

  });

  $('.clause a').click(function() {
    var c_n = $(this).index() - 1;
    $(this).addClass('cOn').siblings().removeClass('cOn');
    $('.clauseTxt').eq(c_n).slideDown().siblings('.clauseTxt').slideUp();
  });

  $('.serveList li').click(function() {
    var s_n = $(this).index() ;
    $(this).addClass('sOn').siblings().removeClass('sOn');
    $('.tab_txt').eq(s_n).fadeIn(200).siblings('.tab_txt').fadeOut(150);
  });
}
/*filtrate*/
function filtrateGift(){
    $('#fg i,#fg2 i').click(function() {
      $(this).addClass('ion').siblings().removeClass('ion');
    });   
}
//点击微信弹出二维码
function wxShow(){
  $('.wx').click(function(){
     if ($('.wx_code').is(':hidden')) {
        $('.wx_code').slideDown();
        $('.wx').addClass('on').siblings().removeClass('on');
        $('.email_code,.lang_code').hide();
     }
     else{
        $('.wx_code').slideUp();
        $('.wx').removeClass('on');
     }
  })
}
//点击英文
function langShow(){
  $('.lang').click(function(){
     if ($('.lang_code').is(':hidden')) {
        $('.lang_code').slideDown();
        $('.lang').addClass('on').siblings().removeClass('on');
        $('.wx_code,.email_code').hide();
     }
     else{
        $('.lang_code').slideUp();
        $('.lang').removeClass('on');
     }
  })
}

//会员登录单选框
$('.checkPanel').click(function(){
    if(!$(this).hasClass('on')){
      $(this).addClass('on');
      $(this).find('input[type="checkbox"]').attr("checked",true);
    }
    else{
      $(this).removeClass('on');
      $(this).find('input[type="checkbox"]').attr("checked",false);
    }
});
/*把show();改为fadeIn();是淡入动画效果。把show();改为slideDown();是滑下动画效果。对应的是fadeOut跟slideUp*/