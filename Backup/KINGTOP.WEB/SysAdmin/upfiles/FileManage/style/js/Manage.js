// 转换为 修改目录名 模式
function toRename(p_name, p_fullName, p_path,p_param) {
    $("#divRename").attr("style", "display:block");
    $("#txtFolderName").val(p_name);
    $("#txtOldName").val(p_fullName);
    
    $("#txtFolderName").focus();
    $("#submit").val("重命名");
    $("#insertForm").attr("action", "filesmanage.aspx?act=rename&path=" + p_path + p_param);
    
    $("#tips").show();
    $("#tipsMsg").text("对文件, 目录重命名时, 可在前面加入相对路径(如: \"..\\\", \"a\\\"), 即可实现移动文件, 目录功能.");
}

// 还原为 创建目录名 模式
function toCreate(p_path)
{
    $("#txtFolderName").val("");
    $("#txtOldName").val("");

    $("#submit").val("新建");
    $("#insertForm").attr("action", "filesmanage.aspx?act=create&path=" + p_path);
    $("#divRename").attr("style", "display:none");
    $("#tips").hide();
}

function uploadMsg()
{
    $("#tips").show();
    $("#tipsMsg").text("正在上传文件中...");
}

function compressMsg()
{
    window.scroll(0, 0);
    $("#tips").show();
    $("#tipsMsg").text("正在压缩目录中...");
}

function unpackMsg()
{
    window.scroll(0, 0);
    $("#tips").show();
    $("#tipsMsg").text("正在对文件解压中...");
}