/// <reference path="jquery-3.2-vsdoc2.js" />
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-25
// 功能描述：模型列表页面通用的JS功能代码
===========================================================================*/
var HQB_Model_AddUrlParam_;
$(pageInit);

function pageInit() {
    // 当没有搜索项时隐藏搜索按钮
    displaySearchBtn();
    // 截取(长度限制)列表中的字符串
    clipListItemContent();
    // 为复选框注册点击事件
    checkBoxInit();
    // 给文本链接加URL传递参数
    AddDeliverUrlParam();

    $("#btnNew").click(function() { location.href = $("#btnNew").attr("Href") + "&" + HQB_Model_AddUrlParam_; });

    $("body").find("input[BtnType='Edit']").each(function() {
        $(this).click(function() { location.href = $(this).attr("Href") + "&" + HQB_Model_AddUrlParam_; })
    });
}
// 为复选框注册点击事件
function checkBoxInit() {
    if ($("#SlectAll")[0] != null) {
        $("#SlectAll").click(function() {
            if ($(this).attr("checked") == true) {
                $("#HQB_ListInfo").find("input[type='checkbox']").each(function() {
                $(this).attr("checked", "checked");
                $(this).parent().parent().addClass("trclick");
                }
                );
                $(this).parent().parent().removeClass("trclick");
            }
            else {
                $("#HQB_ListInfo").find("input[type='checkbox']").each(function() {
                $(this).removeAttr("checked");
                $(this).parent().parent().removeClass("trclick");
                }
                );
            }
        });
    }


    $(".listInfotr").each(function(i) {
        $(this).click(function() {
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
}

// 当没有搜索项时隐藏搜索按钮
function displaySearchBtn() {
    var i = 0;
    $("#searchContainer").find("ul li").each(function() { i++; });

    if (i < 2) {
        $("#searchContainer").hide();
    }

}

// 列表中复选框全选或取消
function isSelected() {
    var isTrue;
    var titleContent;

    titleContent = "";
    isTrue = false;

    $("#_ListInfoListTable").find("input[type='checkbox']:not('#SlectAll')").each(function() {
        if ($(this).attr("checked") == true) {
            if (titleContent == "") {
                titleContent = $("#Title_" + $(this).val()).text();
            } else {
                titleContent = titleContent + "," + $("#Title_" + $(this).val()).text();
            }

            isTrue = true;
        }
    }
    );

    if (!isTrue) {
        alert({ msg: '请选择要操作的记录！', title: '提示信息' })
    }

    $("#hidLogTitle").val(titleContent);
    return isTrue;
}


// 给文本链接加URL传递参数
function AddDeliverUrlParam() {
    var deliverUrlParam, backDeliverUrlParam;
    var arrDeliverUrlParam, arrBackDeliverUrlParam;
    var arrTemp;

    HQB_Model_AddUrlParam_ = "";
    deliverUrlParam = $("#HQB_Model_DeliverUrlParam").val();
    backDeliverUrlParam = $("#HQB_BackUrlParam").val();
    arrDeliverUrlParam = deliverUrlParam.split("&")
    arrBackDeliverUrlParam = backDeliverUrlParam.split("&");

    for (var i = 0; i < arrDeliverUrlParam.length; i++) {
        for (var k = 0; k < arrBackDeliverUrlParam.length; k++) {
            if (arrDeliverUrlParam[i] == arrBackDeliverUrlParam[k]) {
                arrBackDeliverUrlParam[i] = "";
                break;
            }
        }
    }

    for (var m = 0; m < arrDeliverUrlParam.length; m++) {
        if (arrDeliverUrlParam[m] != "") {
            if (HQB_Model_AddUrlParam_ != "") {
                HQB_Model_AddUrlParam_ = HQB_Model_AddUrlParam_ + "&" + arrDeliverUrlParam[m];
            } else {
                HQB_Model_AddUrlParam_ = arrDeliverUrlParam[m];
            }
        }
    }

    if (arrBackDeliverUrlParam != null && arrBackDeliverUrlParam.length > 0) {
        if (HQB_Model_AddUrlParam_ != "") {
            HQB_Model_AddUrlParam_ = HQB_Model_AddUrlParam_ + "&" + arrBackDeliverUrlParam.join("&");
        } else {
            HQB_Model_AddUrlParam_ = arrBackDeliverUrlParam.join("&");
        }
    }

    if (HQB_Model_AddUrlParam_ != "") {
        $("body").find("a").each(function() {
            if ($(this).attr("href") != null) {
                try {
                    if ($(this).attr("href").indexOf("?") > -1) {
                        arrTemp = $(this).attr("href").split("?")[1].split("&");

                        for (var k = 0; k < arrTemp.length; k++) {
                            for (var m = 0; m < arrBackDeliverUrlParam.length; m++) {
                                if (arrTemp[k] == arrBackDeliverUrlParam[m]) {
                                    arrTemp[k] = "";
                                }
                            }
                        }

                        if (arrTemp.length > 0) {
                            $(this).attr("href", $(this).attr("href").split("?")[0] + "?" + arrTemp.join("&") + "&" + HQB_Model_AddUrlParam_);
                        } else {
                            $(this).attr("href", $(this).attr("href").split("?")[0] + "?" + HQB_Model_AddUrlParam_);
                        }
                    } else {
                        $(this).attr("href", $(this).attr("href") + "?" + HQB_Model_AddUrlParam_);
                    }
                } catch (e) { }
            }
        });
    }
}

function checkNum(numValue) {
    if (numValue == null || numValue == "")
        return false;

    for (k = 0; k < numValue.length; k++) {
        if (("0123456789").indexOf(numValue.substr(k, 1)) == -1) {
            return false;
        }
    }

    return true;
}

function setOrders(tableName, id, orderValue) {
    var param;
    if (!checkNum(orderValue)) {
        alert({ msg: "排序号必须是数字！", title: "操作提示" });
        return;
    }
    param = "{tableName:'" + tableName + "',id:'" + id + "',orderValue:" + orderValue + "}";

    if ($("#HQB_Orders_" + id)[0] != null) {
        $("#HQB_Orders_" + id).find("div").each(function() { $(this).css("display", "block"); });
        $("#HQB_Orders_" + id).find("span").each(function() { $(this).css("display", "none"); });
    }

    $.ajax(
    {
        type: "POST",
        contentType: "application/json",
        url: "/sysadmin/Model/ModelAjaxDeal.asmx/SetOrder",
        data: param,
        dataType: 'json',
        success: function(result) {
            if ($("#HQB_Orders_" + id)[0] != null) {
                $("#HQB_Orders_" + id).find("div").each(function() { $(this).css("display", "none"); });
                $("#HQB_Orders_" + id).find("span").each(function() { $(this).css("display", "block"); });
            }
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            if ($("#HQB_Orders_" + id)[0] != null) {

                $("#HQB_Orders_" + id).find("div").each(function() { $(this).css("display", "none"); });
                $("#HQB_Orders_" + id).find("span").each(function() { $(this).css("display", "block"); $(this).find("input[type='text']").each(function() { $(this).val("更新失败请重试"); }); });
            }
        }
    }
    );
}



//截取(长度限制)列表中的字符串
function clipListItemContent() {
    var strWidth;
    var width;
    var strCount;

    $("#HQB_ListInfo ul li[isClip='1']").mouseover(function() {
        var position = $(this).position();
        $("#HQB_Replcae_Title_Display").css("width", $(this).css("width"));
        $("#HQB_Replcae_Title_Display").css("top", position.top + "px");
        $("#HQB_Replcae_Title_Display").css("left", position.left + "px");
        $("#HQB_Replcae_Title_Display table tr td").html($(this).html());
        $("#HQB_Replcae_Title_Display").css("display", "block");
    }
    );
}

// 打开链接页,并需要传递当前页(调用页)地址
function setLocation(url) {
    var deliverUrlParam;

    deliverUrlParam = "OriginalUrl={page:" + $("#hdnTableName").val().replace("K_U_", "").replace("K_F_", "") + ",nodeCode:" + $("#hdnNodeCode").val() + "}&&NodeCode=" + $("#hdnNodeCode").val();
    deliverUrlParam += $("#HQB_Model_DeliverUrlParam").val();
    deliverUrlParam = removeRepeatParam(deliverUrlParam);
    window.location.href = url + deliverUrlParam;
}

// 返回调用页
function backOriginalUrl(urlParam) {
    var url, backUrlParm;

    url = urlParam.page + "list.aspx?";
    backUrlParm = "NodeCode=" + urlParam.nodeCode + "&" + $("#HQB_BackUrlParam").val();
    backUrlParm = removeRepeatParam(backUrlParm);
    window.location.href = url + backUrlParm;
}

// 删除重复的URL参数
function removeRepeatParam(urlParam) {
    var arrParam, arrResult;
    var result = "";
    var checkISAdded = ",";
    var filedName;

    if (urlParam != "") {
        arrParam = urlParam.split("&");
        arrResult = new Array(arrParam.length);

        for (var i = 0; i < arrParam.length; i++) {
            if (arrParam[i] != "") {
                filedName = arrParam[i].split("=")[0];
                if (filedName != null && filedName != "" != undefined) {
                    if (checkISAdded.indexOf("," + filedName + ",") < 0) {
                        arrResult[i] = arrParam[i];
                        checkISAdded += filedName + ",";
                    }
                }
            }
        }

        for (var i = 0; i < arrResult.length; i++) {
            if (arrResult[i] != "" && arrResult[i] != null && arrResult[i] != undefined) {
                if (result == "") {
                    result = arrResult[i];
                } else {
                    result += "&" + arrResult[i];
                }
            }
        }
    }
    return result;
}

var HQB_Log_IsValide;
var HQB_Log_ID;
var HQB_CommandObject;
HQB_Log_IsValide = false;

// 按钮操作
function setAction(action) {
    var reg;

    reg = /.*\.[a-zA-Z]+.*/gi;
    if (reg.test(action)) {
        window.open(action);
    } else { 
        $("#HQB_Action").val(action);
        return isSelected();
    }
}

// 按钮操作
function confirmSetAction(obj, action, message) {
    HQB_CommandObject = obj;
    selfconfirm({ msg: message, fn: function(data) { SetSubmitCommand(data, action) } })
    return HQB_Log_IsValide;
}

function reConfirm(obj, message) {


    if (!HQB_Log_IsValide) {
        selfconfirm({ msg: message, fn: function (data) { setTitle(data) } })
    }

    return HQB_Log_IsValide;
}


function SetSubmitCommand(data, action) {
    if (data == "true") {
        if (isSelected()) {
            if (HQB_CommandObject) {
                $("#HQB_Action").val(action);
                HQB_Log_IsValide = true;
                $(HQB_CommandObject).click();
            }
        } else { }
    }
}

// Repeater中的按钮操作
function rptConfirmSetAction(obj, action, message) {
    HQB_CommandObject = obj;
    selfconfirm({ msg: message, fn: function(data) { SetRptSubmitCommand(data, action) } })
    return HQB_Log_IsValide;
}

function SetRptSubmitCommand(data, action) {
    if (data == "true") {
        if (HQB_CommandObject) {
            $("#HQB_Action").val(action);
            HQB_Log_IsValide = true;
            $("#hidLogTitle").val($("#Title_" + $(HQB_CommandObject).attr("RecordID")).text());
            $(HQB_CommandObject).click();
        }
    }
}

// 记录批量添加至专题
function batchSpecialSet() {
    var id = "", urlAddress = "";

    $("#_ListInfoListTable").find("input[type='checkbox']:not('#SlectAll')").each(function() {
        if ($(this).attr("checked") == true) {
            isTrue = true;
            id = id + $(this).val() + ",";
        }
    }
    );

    if (id != "") {
        id = id.substr(0, id.length - 1);
        urlAddress = "AppendToSpecial.aspx?action=content&id=" + id + "&modelid=" + $("#hdnModelID").val() + "&NodeCode=" + $("#hdnNodeCode").val();
        openframe({ title: "添加到专题栏目", url: urlAddress, width: '640', height: '280' })
    }
    else {
        alert({ msg: '请选择要添加的记录项！', title: '提示信息' })
    }
}

// 记录批量添加至节点(栏目)
function batchNodeSet() {
    var id = "", urlAddress = "";

    $("#_ListInfoListTable").find("input[type='checkbox']:not('#SlectAll')").each(function() {
        if ($(this).attr("checked") == true) {
            isTrue = true;
            id = id + $(this).val() + ",";
        }
    }
    );

    if (id != "") {
        id = id.substr(0, id.length - 1);
        urlAddress = "AppendToNode.aspx?id=" + id + "&modelid=" + $("#hdnModelID").val() + "&NodeCode=" + $("#hdnNodeCode").val();
        openframe({ title: "添加到节点/栏目", url: urlAddress, width: '640', height: '280' })
    }
    else {
        alert({ msg: '请选择要添加的记录项！', title: '提示信息' })
    }
}

// 关闭Dialog
function CloseDialog() {
    $("body").find("#norDialog").remove().end();
    $("#norDialogBackground").remove();
}


// 排序 sortType 1 只要升序 2 只要降序  3 两者皆要
function sort(fieldName, sortType) {
    var sortStr, frontSort, ascSort, descSort, urlParam;

    ascSort = fieldName + " asc ";
    descSort = fieldName + " desc ";
    frontSort = getCookie("_ContentTemplateListSort_");

    switch (sortType) {
        case "1":
            sortStr = ascSort;
            setCookies("_ContentTemplateListSort_", "1");
            break;
        case "2":
            sortStr = descSort;
            setCookies("_ContentTemplateListSort_", "0");
            break;
        case "3":
            if (frontSort == null || frontSort == "") {
                sortStr = ascSort;
                setCookies("_ContentTemplateListSort_", "1");
            }
            else if (frontSort == "1") {
                sortStr = descSort;
                setCookies("_ContentTemplateListSort_", "0");
            }
            else {
                sortStr = ascSort;
                setCookies("_ContentTemplateListSort_", "1");
            }
            break;
        default:
            sortStr = "";
            break;
    }

    try {
        urlParam = getUrlParam("&Sort=" + $("#hdnTableName").val());

        if (urlParam != null) {
            sortStr = urlParam + "&Sort=" + sortStr;
        }
        else {
            sortStr = "?Sort=" + sortStr;
        }
    }
    catch (e) {
        sortStr = "?Sort=" + sortStr;
    }

    location.href = sortStr;
}

// 获取当前URL中的参数部分
function getUrlParam(filterContent) {
    var urlParam;
    var reg;
    var matchArr;

    reg = new RegExp("[?]{1}(([a-z A-Z 0-9 _]+)[=]{1}([a-z A-Z 0-9 _]+)[&]{0,1})+");
    matchArr = reg.exec(location.href);

    if (matchArr != null) {
        urlParam = matchArr[0];
        urlParam = urlParam.replace(filterContent, "");
    } else {
        urlParam = null;
    }

    return urlParam;
}

// 关闭弹出的对话框，再弹出另外一个
function closeFrameAndOpenDilog(obj) {
    CloseDialog();
    alert(obj);
}

var isOpen = true;
$(function() {
    var SortArr = SortList.split(",");
    $('tbody').sortable({
        items: 'tr:not(:first)',
        handle: '.dragOrders',
        stop: function(event, ui) {
            var tableName = $("#hdnTableName").val();
            var idList = "";
            $(".listInfotr").find("input[type='checkbox']").each(function() {
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
                                success: function(result) {
                                    var k = 0;
                                    $(".listInfotr").find("input[type='text']").each(function() {
                                        $(this).val(SortArr[k]);
                                        k++;
                                    });
                                },
                                error: function(XMLHttpRequest, textStatus, errorThrown) {
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