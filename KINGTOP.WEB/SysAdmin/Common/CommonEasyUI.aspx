<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonEasyUI.aspx.cs" Inherits="KingTop.WEB.SysAdmin.Common.CommonEasyUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/SysAdmin/EasyUI/jquery-1.9.1.js" type="text/javascript"></script>
    <!-- EasyUI -->
    <link href="/SysAdmin/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/SysAdmin/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/SysAdmin/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/SysAdmin/EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../EasyUI/validate.js" type="text/javascript"></script>
    <!--EasyUI 扩展 -->
    <script src="/SysAdmin/EasyUI/jeasyui-extensions/jquery.jdirk.js" type="text/javascript"></script>
    <script src="/SysAdmin/EasyUI/jeasyui-extensions/jeasyui.extensions.js" type="text/javascript"></script>
    <script src="/SysAdmin/EasyUI/jeasyui-extensions/jeasyui.extensions.form.js" type="text/javascript"></script>

    <!-- 上传组件-->
    <script type="text/javascript" src="../js/jquery.dialog.js"></script>
    <script type="text/javascript" src="../js/win.js"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <link href="../CSS/public.css" rel="stylesheet" type="text/css" />
    <link href="../css/dialog.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        /* 窗口选择卡内部样式 */
        .ftitle {
            border-bottom: 1px solid #ccc;
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 10px;
            padding: 5px 0;
        }

        .fitem {
            margin-bottom: 5px;
        }

            .fitem samp {
                display: inline-block;
                margin-right: 5px;
                width: 105px;
                text-align: right;
            }

            .fitem input[type="text"] {
                width: 160px;
            }
        /* 窗口选择卡内部样式 */
        .hwxx1 {
            border-collapse: collapse;
            margin: 5px 0px 5px 8px;
        }

            .hwxx1 td {
                border: 1px solid #D7D7D7;
                padding: 3px 5px;
                text-align: left;
                vertical-align: middle;
                height: 17px;
                min-width: 70px;
            }
    </style>
</head>
<body>

    <div class="easyui-layout" data-options="fit : true,border : false">
        <div data-options="region:'north',border:false" style="height: 40px; overflow: hidden;">
            <form id="searchForm">
                <table class="table table-hover table-condensed">
                    <tr>
                        <th>标题:</th>
                        <td>
                            <input id="sTitle" placeholder="" class="span2" /></td>
                        <th>用户名:</th>
                        <td>
                            <input id="sUserName" placeholder="" class="span2" /></td>
                    </tr>
                </table>
            </form>
        </div>
        <div data-options="region:'center',border:false">
            <table id="tList"></table>
        </div>
    </div>
    <!-- 列表 -->

    <!-- 窗口 -->
    <div id="wEdit" style="padding: 5px;">
        <!-- Edit -->
        <div class="easyui-layout" data-options="fit:true">
            <div data-options="region:'center',border:false">
                <form id="_frm" method="post" class="easyui-form">
                    <div style="padding: 10px 0px;">
                        <input name="ID" id="_ID_Edit" type="hidden" />
                        <div class="fitem">
                            <samp>
                                标题:</samp>
                            <input id="Title" name="Title" type="text" data-options="required:true" placeholder="请输入标题" class="easyui-validatebox" />
                        </div>                        
                        <div class="fitem">
                            <samp>
                                用户名:</samp>
                            <input id="UserName" name="UserName" type="text" placeholder="请输入用户名" class="easyui-validatebox"
                                data-options="required:true" />
                        </div>
                        <div class="fitem">
                            <samp>
                                大图:</samp>
                            <input id="BigImg" name="BigImg" type="text" placeholder="请选择图片" class="easyui-validatebox" style="width: 230px" />&nbsp &nbsp&nbsp
                                <input type='button' onclick="InputImages('_frm', 'BigImg', 1, '', 4096, '', 125, 125, 0, '0', '0')" value='上传图片' />
                        </div>
                        
                        <div class="fitem">
                            <samp>
                                审核状态:</samp>

                            
                            <%--
    <select id="FlowState" name="FlowState" style="width: 165px;" class="easyui-combobox">
        <option value="0" selected="selected">请选择</option>
        <option value="99">审核通过</option>
        <option value="3">取消审核</option>
    </select>
                                
       <select id="FlowState" name="FlowState" style="width: 165px;" class="easyui-combobox" data-options="editable:false,valueField:'Id',textField:'text',url:'AjaxCommon.ashx?action=bindflowstate'">
</select>                                   
                       

                                

--%>
                            
<select id="FlowState" name="FlowState" style="width: 165px;" class="easyui-combobox" data-options="editable:false,valueField:'Id',textField:'text'">
</select>
                             

                        </div>

                        <div class="fitem">
                            <samp>
                                排序数字:</samp>
                            <input id="Order" name="Order" type="text" placeholder="请输入排序数字" class="easyui-validatebox"
                                data-options="required:true" validtype="integer" />&nbsp &nbsp &nbsp 数字越大越靠前
                        </div>

                        <div class="fitem">
                            <samp>
                                详细内容:</samp>
                            <%--<uc1:Editor ID="Editor1" runat="server" width="700" height="350" EditorType="1" />--%>


                            <textarea id="TextArea"></textarea>

                            <input type="hidden" name="TxtDetail" id="TxtDetail" />

                            <%--<textarea id="Editor1" cols="20" rows="2"></textarea>
                            <script type="text/javascript" src="../Editor/ckeditor/ckeditor.js"></script>
                            <link href="../Editor/ckeditor/content.css" rel="stylesheet" type="text/css" />
                            <script type="text/javascript">
                                var Editor1$txtEditorContent$$ckeditor$$obj = CKEDITOR.replace('Editor1', { language: 'zh-cn', height: '350px', width: '700px' });
                            </script>--%>
                        </div>
                    </div>
                </form>
            </div>
            <div data-options="region:'south',border:false" style="text-align: right; padding: 5px 0 0;">
                <a id="_btnCategoryData" class="easyui-linkbutton" data-options="iconCls:'icon-ok'"
                    href="javascript:void(0)" style="width: 80px">确定</a> <a class="easyui-linkbutton"
                        data-options="iconCls:'icon-cancel'" href="javascript:void(0)" onclick="javascript:$('#wEdit').dialog('close');"
                        style="width: 80px">取消</a>
            </div>
        </div>
        <!-- Edit -->
    </div>
    <!-- 窗口 -->
    <!--编辑器组件  -->
    <script type="text/javascript" charset="utf-8" src="../Editor/ueditor/editor_all_min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../Editor/ueditor/editor_config.js"></script>
    <link rel="stylesheet" type="text/css" href="../Editor/ueditor/themes/default/ueditor.css" />
    <script type="text/javascript">
        var NodeCode = '<%=this.NodeCode%>';
        var editor1 = null;
        $(function () {
            // 列表
            $("#tList").datagrid({
                method: "get",
                url: "AjaxCommon.ashx?nodecode=" + NodeCode + "&action=list",
                idField: 'Id',
                pagination: true,
                pageSize: 20,
                pageList: [10, 20, 30, 40, 50],
                rownumbers: true,
                singleSelect: false,
                fit: true,
                fitColumns: true,
                //selectOnCheck: false,
                frozenColumns: [[
                    { field: 'id', checkbox: true }
                ]],
                columns: [[
                   {
                       field: 'Title',
                       title: '标题',
                       width: 200,
                       align: 'center'
                   }, {
                       field: 'UserName',
                       title: '用户名',
                       width: 100,
                       align: 'center'
                   }, {
                       field: 'BigImg',
                       title: '大图',
                       width: 100,
                       align: 'center'
                   }, {
                       field: 'AddDate',
                       title: '添加日期',
                       width: 180,
                       align: 'center'
                   }, {
                       field: 'FlowState',
                       title: '审核状态',
                       width: 80,
                       align: 'center'
                   }, {
                       field: 'Orders',
                       title: '排序',
                       width: 80,
                       align: 'center'
                   }, {
                       field: 'action',
                       title: '操作',
                       width: 100,
                       align: 'center',
                       formatter: function (value, row, index) {
                           var str = '';
                           str += '<img onclick="EditShop(' + row.Id + ');" src="../EasyUI/themes/icons/pencil.png" title="编辑"/>';
                           str += '&nbsp;&nbsp;';
                           str += '<img onclick="DelShop(' + row.Id + ');" src="../EasyUI/themes/icons/cancel.png" title="删除"/>';
                           return str;
                       }
                   }
                ]],
                enableHeaderClickMenu: true,
                enableHeaderContextMenu: true,
                enableRowContextMenu: false,
                toolbar: [{
                    id: '_btnAdd',
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function () { AddShop(); } // 1.添加
                }, '-', {
                    id: '_btnEdit',
                    text: '编辑',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var row = $('#tList').datagrid('getSelected');
                        EditShop(row.Id); // 2.编辑
                    }
                }, '-', {
                    id: '_btnDel',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        MutiDel();
                    }
                }, '-', {
                    id: '_btnCheck',
                    text: '通过审核',
                    iconCls: 'icon-edit',
                    handler: function () {
                        MutiCheck();
                    }
                }, '-', {
                    id: '_btnCancelCheck',
                    text: '取消审核',
                    iconCls: 'icon-edit',
                    handler: function () {
                        MutiCancelCheck();
                    }
                }, '-', {
                    id: '_btnSearch',
                    text: '搜索',
                    iconCls: 'icon-search',
                    handler: function () { SearchFun(); }
                }, '-', {
                    id: '_btnRefresh',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () { $('#tList').datagrid('reload'); }
                }],
                onDblClickRow: function (row) {
                    var row = $('#tList').datagrid('getSelected');
                    EditShop(row.Id); // 2.编辑
                }
            });
            // 窗口
            $('#wEdit').window({
                iconCls: 'icon-save',
                title: '编辑信息',
                width: 750,
                height: 500,
                zIndex: 999,
                closed: true,
                resizable: false, //调整大小
                cache: false, //缓存
                maximizable: false, //最大按钮
                minimizable: false, //最小按钮
                autoVCenter: true, //该属性如果设置为 true，则使窗口保持纵向居中，默认为 true。
                autoHCenter: true, //该属性如果设置为 true，则使窗口保持横向居中，默认为 true。
                inContainer: true
            });

            
            if (editor1 == null) {
                var options1 = {
                    toolbars: [['source', 'bold', 'italic', 'underline', '|', 'pasteplain', 'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|', 'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'link', 'unlink', 'emotion', 'insertimage', 'insertvideo', 'attachment']], minFrameHeight: 230, initialContent: ""
                };
                editor1 = new baidu.editor.ui.Editor(options1);                editor1.render('TextArea');
            }
            else {
                editor1.setContent("");
            }

            

        });

        //重置编辑器
        function NewEditor() {
            if (editor1 == null) {
                var options1 = {
                    toolbars: [['source', 'bold', 'italic', 'underline', '|', 'pasteplain', 'forecolor', 'backcolor', '|', 'fontfamily', 'fontsize', '|', 'lineheight', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'link', 'unlink', 'emotion', 'insertimage', 'insertvideo', 'attachment']], minFrameHeight: 230, initialContent: ""
                };
                editor1 = new baidu.editor.ui.Editor(options1);                editor1.render('TextArea');
            }
            else {
                editor1.setContent("");
            }
        }

        // 1.添加
        function AddShop() {
            $('#_frm').form('clear'); //重置表单     
            $('#wEdit').dialog('open');
            $('#_btnCategoryData').unbind('click');
            $('#_btnCategoryData').bind('click', function () { frmSaveData('add') });
            //下拉框审核状态
            //$("#FlowState").val("0");
            //$(".combo-text").val("请选择");            
            $('#FlowState').combobox('reload', 'AjaxCommon.ashx?action=bindflowstate');
            //$('#FlowState').combobox('setValue', 0);
            NewEditor();
            //$.messager.show($("#FlowState").val());
        }



        // 2.编辑
        function EditShop(id) {
            if (id != null) {
                $('#_frm').form('reset'); // 重置表单
                $.easyui.loading({ msg: "正在加载...", locale: "#wEdit" }); // 加载提示                
                $('#wEdit').window('open');
                $('#FlowState').combobox('reload', 'AjaxCommon.ashx?action=bindflowstate');
                NewEditor();
                $.getJSON("AjaxCommon.ashx?nodecode=" + NodeCode + "&action=getbyid&id=" + id, function (data) {

                    try {
                        $('#_frm').form("load", {
                            ID: data.Id,
                            Title: data.Title,
                            UserName: data.UserName,
                            BigImg: data.BigImg,
                            FlowState: data.FlowState,
                            Order: data.Orders
                        });
                        editor1.setContent(data.Detail!=""?data.Detail:"");
                    } catch (e) {
                        $.messager.show(e.toString());
                    }
                    
                    $.easyui.loaded("#wEdit"); // 加载提示关闭
                });
                // 4.绑定提交
                $('#_btnCategoryData').unbind('click');
                $('#_btnCategoryData').bind('click', function () { frmSaveData('edit') });
            }
            else {
                $.messager.show("请选择!");
            }
        }

        // 3.删除
        function DelShop(id) {
            if (id != null) {
                $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
                    if (r) {
                        $.easyui.loading({ msg: "正在处理中...", locale: "#tList" });
                        $.getJSON("AjaxCommon.ashx?nodecode=" + NodeCode, { action: "del", "id": id }, function (data) {
                            if (data.success) {
                                $('#tList').datagrid('reload'); //重新载入
                            } else {
                                $.messager.alert('提示', data.msg, 'info');
                            }
                            $.easyui.loaded("#tList"); //关闭加载提示
                        });
                    }
                });
            }
            else {
                $.messager.show("请选择!");
            }
        }

        //批量删除
        function MutiDel() {
            var rows = $('#tList').datagrid('getChecked');
            if (rows.length > 0) {
                $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
                    if (r) {
                        var ListId = "";
                        for (var i = 0; i < rows.length; i++) {
                            ListId += rows[i].Id + ",";
                        }
                        $.getJSON("AjaxCommon.ashx?nodecode=" + NodeCode, { action: "del", "id": ListId }, function (data) {
                            $('#tList').datagrid('reload'); //重新载入
                        });
                        //$("#_btnRefresh").click();
                    }
                });
            }
            else {
                $.messager.show("请选择!");
            }
        }

        //批量通过审核
        function MutiCheck() {
            var rows = $('#tList').datagrid('getChecked');
            if (rows.length > 0) {
                var ListId = "";
                for (var i = 0; i < rows.length; i++) {
                    ListId += rows[i].Id + ",";
                }
                $.getJSON("AjaxCommon.ashx?nodecode=" + NodeCode, { action: "check", "id": ListId }, function (data) {
                    $('#tList').datagrid('reload'); //重新载入
                });
            }
            else {
                $.messager.show("请选择!");
            }
        }

        //批量取消审核
        function MutiCancelCheck() {
            var rows = $('#tList').datagrid('getChecked');
            if (rows.length > 0) {
                var ListId = "";
                for (var i = 0; i < rows.length; i++) {
                    ListId += rows[i].Id + ",";
                }
                $.getJSON("AjaxCommon.ashx?nodecode=" + NodeCode, { action: "cancelcheck", "id": ListId }, function (data) {
                    $('#tList').datagrid('reload'); //重新载入
                });

            }
            else {
                $.messager.show("请选择!");
            }
        }

        function SearchFun() {
            $('#tList').datagrid('load', { sTitle: $("#sTitle").val(), sUserName: $("#sUserName").val() });
        }

        // 4.提交数据
        function frmSaveData(action) {
            $("#TxtDetail").val(editor1.getContent());
            $('#_frm').form('submit', {
                url: 'AjaxCommon.ashx?nodecode=' + NodeCode + '&action=' + action,
                method: 'post',
                onSubmit: function () {
                    var isValid = $(this).parent().form("validate");
                    return isValid;
                },
                success: function (data) {
                    var json = eval('(' + data + ')');
                    if (json.success) {
                        $.messager.show(json.msg);
                        // 重新载入数据
                        $('#wEdit').dialog('close');
                        $('#tList').datagrid('reload'); //重新载入
                    }
                    else {
                        $.messager.alert('提示', data, 'info');
                    }
                }
            });
        }
    </script>

</body>
</html>
