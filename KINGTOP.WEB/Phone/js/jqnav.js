$(document).ready(function(){
 document.documentElement.style.fontSize = $(document.documentElement).width()/3.75 + 'px';
$(window).on('resize', function() {  document.documentElement.style.fontSize = $(document.documentElement).width()/3.75 + 'px';});  

$('#menu').click(function () {
   
 if(!$(this).children().hasClass('hover')){
	 $(this).children().addClass('hover');
	 $("#navbox").slideDown(250);
	}
	else{
	  $(this).children().removeClass('hover');
	  $("#navbox").slideUp(250);
	  $("#navbox .nav-r").removeClass('hover');
	  $("#navbox .nav-l li").removeClass('hover');
		}
});

touch.on('#sch', 'tap', function(){
  $(".schbox").slideToggle(250);
});

touch.on('#map', 'tap', function(){
  $(".page").show();
});

touch.on('.closed', 'tap', function(){
   $(".page").hide();
});

$(".navinput p").click(function(){
		var ul=$(this).next(".new");
		if(ul.css("display")=="none"){
			ul.slideDown();
		}else{
			ul.slideUp();
		}
	});
	
$(".set").click(function(){
		var _name = $(this).attr("name");
		if( $("[name="+_name+"]").length > 1 ){
			$("[name="+_name+"]").removeClass("hover");
			$(this).parent().addClass("hover");
		} else {
			if( $(this).parent().hasClass("hover") ){
				$(this).parent().removeClass("hover");
			} else {
				$(this).parent().addClass("hover");
			}
		}
	});	
$(".navinput li").click(function(){
		var li=$(this).html();
		$(this).parent("ul").prev("p").html(li);
		$(".new").hide();
		$("p").parent().removeClass("hover");  
});

$(".navinput1 li").click(function(){
		var index=$(this).index();
		$("#case-about .tab").eq(index).show().siblings().hide();
});

$("#navbox .nav-l li").click(function(){
	var index=$(this).index();
	$(this).addClass('hover').siblings().removeClass('hover');
	$('#navbox .nav-r').addClass('hover');
	$('#navbox .nav-r .nav_tab').eq(index).show().siblings().hide();
})


$("#sharebtn").click(function(){
	$("#bg").show();
	$("#sharebg").show();
})
$("#bg").click(function(){
	$("#bg").hide();
	$("#sharebg").hide();
})

$(".shopnavlist li a").click(function(){
  if(!$(this).parent().hasClass('hover')){
	$(this).parent().addClass('hover').siblings().removeClass('hover');
    $(this).parent().children('.sch-shopping').slideDown(250);
	$(this).parent().siblings().children('.sch-shopping').slideUp(250);
  }
  else{
	 $(this).parent().removeClass('hover');
	 $(this).parent().children('.sch-shopping').slideUp(250);
	}
})

});




