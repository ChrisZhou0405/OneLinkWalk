// 为复选框注册点击事件
jQuery(function () {
    if ($("#SlectAll")[0] != null) {
        $("#SlectAll").click(function () {
            if ($(this).attr("checked") == true) {
                $("#HQB_ListInfo").find("input[type='checkbox']").each(function () {
                    $(this).attr("checked", "checked");
                    $(this).parent().parent().addClass("trclick");
                }
                );
                $(this).parent().parent().removeClass("trclick");
            }
            else {
                $("#HQB_ListInfo").find("input[type='checkbox']").each(function () {
                    $(this).removeAttr("checked");
                    $(this).parent().parent().removeClass("trclick");
                }
                );
            }
        });
    }


    $(".listInfotr").each(function (i) {
        $(this).click(function () {
            var chkArray = $(this).children("td").children(":checkbox");
            if (chkArray.length > 0) {
                var b = chkArray[0].checked;
                var src = arguments[0].target || window.event.srcElement;
                if (b) {
                    if (src.type != 'checkbox' && src.tagName != "A") {                 //避免重复2次修改

                        $(this).removeClass("trclick");
                        chkArray[0].checked = false;
                    }

                } else {

                    if (src.type != 'checkbox') {                      //避免重复2次修改
                        $(this).addClass("trclick");
                        chkArray[0].checked = true;
                    }
                }
            }
        })
    })
})
