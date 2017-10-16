function GetCategoryList()
{
    $.post("Category.aspx", { Action: "getcategorylist", NodeCode: request("NodeCode") }, function (data, textstatus) {
     switch(parseInt(data.Code)){
            case 200:$("ul.ulbody").remove();$(".ulheader").after(data.Message);BindUL();break;
            default: alert({msg:'['+data.Code+'] '+data.Message});break;
          }
     },"json");
}
function BindUL()
{
    $("ul.ulbody").each(function(){
        $(this).mouseover(function(){ $(this).css("background","#E8F3F6");});
        $(this).mouseout(function(){ $(this).css("background","#fff");});
    });
    
    $(".orders").bind({
      click: function() {
         $(this).css({background:'#fff',padding:'0px'}).html('<input type=\"text\" style=\"width:60px;height:14px;border:#e4e4e4 1px solid;text-align:center;padding-bottom:2px;\" value=\"'+$(this).html()+'\" onblur=\"UpdateOrders('+$(this).attr("ref")+',this)\" />');
         $(this).unbind("click").find("input").focus();
      }
    });
} 

function Del(id,obj)
{
    selfconfirm({msg:'您确定要删除该数据吗？',fn:function(data){if(data=="true"){DelItem(id,obj)};$(this).hide() }});
}
function DelItem(id,obj)
{
    $.post("Category.aspx",{Action:"removeitem",id:id},function(data,textstatus){
     switch(parseInt(data.Code)){
            case 200:$(obj).parents("ul").remove();break;
            default: alert({msg:'['+data.Code+'] '+data.Message});break;
          }
     },"json");
}
function ShowValid(id,obj)
{
    $.post("Category.aspx",{Action:"showvalid",id:id},function(data,textstatus){
     switch(parseInt(data.Code)){
            case 200:
                    if(data.Data=="1")
                    $(obj).attr("src",'/sysadmin/images/yes.gif');
                    else
                    $(obj).attr("src",'/sysadmin/images/no.gif');
                    break;
            default: alert({msg:'['+data.Code+'] '+data.Message});break;
          }
     },"json");
}
function ShowIndex(id,obj)
{
    $.post("Category.aspx",{Action:"showindex",id:id},function(data,textstatus){
     switch(parseInt(data.Code)){
            case 200:
                    if(data.Data=="1")
                    $(obj).attr("src",'/sysadmin/images/yes.gif');
                    else
                    $(obj).attr("src",'/sysadmin/images/no.gif');
                    break;
            default: alert({msg:'['+data.Code+'] '+data.Message});break;
          }
     },"json");
}
function UpdateOrders(id,obj)
{
    var orders = parseInt($(obj).val(),0);
    var span=$(obj).parent("span");
    $(span).html(orders);
    $.post("Category.aspx",{Action:"updateorders",id:id,orders:orders},function(data,textstatus){
     switch(parseInt(data.Code)){
            case 200:GetCategoryList();break;
            default: GetCategoryList();break;
          }
     },"json");
}

function chkdelete() {
    var chks = document.getElementsByName("chkId");
    var tempval = 0;
    for (var i = 0; i < chks.length; i++) {
        if (chks[i].checked == true) {
            tempval += 1;
        }
    }
    if (tempval == 0) { return false; }
    else { return confirm("确定要删除吗？"); }
}
/**
 * 折叠分类列表
 */
function rowClicked(obj)
{
    var imgPlus = "/sysadmin/images/DTree/plus.gif";
  if($(obj).attr("src")==imgPlus)
      $(obj).attr("src", "/sysadmin/images/DTree/minus.gif");
  else
     $(obj).attr("src",imgPlus);
  obj = $(obj).parents(".ulbody");
  var ull = $(".ulbody");
  var lvl = parseInt($(obj).attr("ref"));
  var fnd = false;
  var display= $(obj).attr("ishow");
  if(display=="0")
    $(obj).attr("ishow","1");
  else
     $(obj).attr("ishow","0");
  for (i = 0; i < ull.length; i++)
  {
      var row =ull.eq(i);
      if ($(row).html()== $(obj).html())
         fnd = true;
      else
      {
          if (fnd == true)
          {
              var cur = parseInt($(row).attr("ref"));
              if (cur > lvl)
              {
                  if(display=="0")
                      $(row).show().attr("ishow", "1").find("img").eq(0).attr("src", "/sysadmin/images/DTree/minus.gif");
                  else
                     $(row).hide();
              }
              else
              {
                  fnd = false;
                  break;
              }
          }
      }
  }
}

/*--获取网页传递的参数--*/
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function EditOrders() {
    
}

(function($){
   GetCategoryList();
   
   

})(jQuery)


