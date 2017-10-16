/// <reference path="jquery-3.2-vsdoc2.js" />

var isOpen = true;
$(function () {
    var SortArr = SortList.split(",");
    $('tbody').sortable({
        stop: function (event, ui) {
            var tableName = $("#hdnTableName").val();
            var idList = "";
            $(".listInfotr").find("input[type='checkbox']").each(function () {
                if (idList == "")
                    idList = $(this).val();
                else
                    idList += "," + $(this).val();
            });

            var param;
            param = "{tableName:'" + tableName + "',idlist:'" + idList + "',sortList:'" + SortList + "'}";
            $.ajax(
                            {
                                type: "POST",
                                contentType: "application/json",
                                url: "/sysadmin/Model/ModelAjaxDeal.asmx/SetOrderDrag",
                                data: param,
                                dataType: 'json',
                                success: function (result) {
                                    var k = 0;
                                    $(".listInfotr").find("input[type='text']").each(function () {
                                        $(this).val(SortArr[k]);
                                        k++;
                                    });
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert({ msg: '排序更新数据失败！', title: '操作提示' })
                                }
                            }
                            );
        }
    });
});

function CloseOrOpenSortTable(obj) {
    if (isOpen) {
        $('tbody').sortable('disable');
        obj.value = "开启拖动排序"
        isOpen = false;
    }
    else {
        $('tbody').sortable('enable');
        obj.value = "关闭拖动排序"
        isOpen = true;
    }
}