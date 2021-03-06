/**
 * Created by JetBrains PhpStorm.
 * User: taoqili
 * Date: 12-1-30
 * Time: 下午12:50
 * To change this template use File | Settings | File Templates.
 */

//editor.execCommand("wordimage","word_img");


var wordImage = {};
//(function(){
var g = baidu.g,
	flashObj,flashContainer;
wordImage.init = function(opt, callbacks) {
	createFlashUploader(opt, callbacks);
	addUploadListener();
};

function hideFlash(){
    flashObj = null;
    flashContainer.innerHTML = "";
}
/**
 * 绑定开始上传事件
 */
function addUploadListener() {
    g("upload").onclick = function() {
        var p = SetOK();
        if (p == "false") {
            return;
        }
        //以下是先传参，保存到Cache中；因为这个控件没办法直接动态传参过去
        if (p != "") {
            ajax.request(editor.options.imageUrl, {
                paramValue: p,
                paramkey: document.getElementById("hidKeyCode").value,
                onsuccess: function(xhr) {
                    flashObj.upload();
                    //this.style.display = "none";
                    //document.getElementById("upload").style.display = "none";
                },
                onerror: function(xhr) {
                    //alert(xhr.responseText);
                    flashObj.upload();
                    //this.style.display = "none";
                    //document.getElementById("upload").style.display = "none";
                }
            });
        }
        else {
            flashObj.upload();
            //this.style.display = "none";
        }
    };
}

function showLocalPath(id) {
    //单张编辑
    if(editor.word_img.length==1){
        g(id).value = editor.word_img[0];
        return;
    }
	var path = editor.word_img[0];
    var leftSlashIndex  = path.lastIndexOf("/")||0,  //不同版本的doc和浏览器都可能影响到这个符号，故直接判断两种
            rightSlashIndex = path.lastIndexOf("\\")||0,
            separater = leftSlashIndex > rightSlashIndex ? "/":"\\" ;

	path = path.substring(0, path.lastIndexOf(separater)+1);
	g(id).value = path;
}

function createFlashUploader(opt, callbacks) {
    //由于lang.flashI18n是静态属性，不可以直接进行修改，否则会影响到后续内容
    var i18n = utils.extend({},lang.flashI18n);
    //处理图片资源地址的编码，补全等问题
    for(var i in i18n){
        if(!(i in {"lang":1,"uploadingTF":1,"imageTF":1,"textEncoding":1}) && i18n[i]){
            i18n[i] = encodeURIComponent(editor.options.langPath + editor.options.lang + "/images/" + i18n[i]);
        }
    }
    opt = utils.extend(opt,i18n,false);
	var option = {
		createOptions:{
			id:'flash',
			url:opt.flashUrl,
			width:opt.width,
			height:opt.height,
			errorMessage:lang.flashError,
			wmode:browser.safari ? 'transparent' : 'window',
			ver:'10.0.0',
			vars:opt,
			container:opt.container
		}
	};

	option = extendProperty(callbacks, option);
	flashObj = new baidu.flash.imageUploader(option);
    flashContainer = $G(opt.container);
}

function extendProperty(fromObj, toObj) {
	for (var i in fromObj) {
		if (!toObj[i]) {
			toObj[i] = fromObj[i];
		}
	}
	return toObj;
}

//})();

function getPasteData(id) {
	baidu.g("msg").innerHTML = lang.copySuccess + "</br>";
	setTimeout(function() {
		baidu.g("msg").innerHTML = "";
	}, 5000);
	return baidu.g(id).value;
}

