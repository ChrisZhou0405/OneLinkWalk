using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 上传文件设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Config
{
    [Serializable]
    public class UploadConfig : IConfigInfo
    {
        public UploadConfig()
        {
        }

        #region 私有字段
        //上传文件配置和缩略图配置
        private string _imageUrl = ""; //图片域名，不填写则为默认域名
        private string _imageSavePath = "UploadFiles/Images"; //图片保存路径，默认保存在根目录下UploadFiles/Images下
        private string _fileUrl = ""; //文件域名，不填写则为默认域名
        private string _fileSavePath = "UploadFiles/Files"; //文件保存路径
        private string _mediaUrl = ""; //文件域名，不填写则为默认域名
        private string _mediaSavePath = "UploadFiles/Medias"; //文件保存路径
        private string _isEnableUpload = "1"; //是否允许上传，0=否，1=是
        private string _uploadControl = "0"; //上传使用控件，upload.aspx=使用vs上传控件，neatupload.aspx=使用neatupload上传控件
        private string _uploadImageSize = "1024"; //允许上传的最大图片大小 单位K
        private string _uploadFilesSize = "2048"; //允许上传的最大文件大小 单位K
        private string _uploadMediaSize = "10240"; //允许上传的最大视频大小 单位K
        private string _uploadMediaType = "rm|mp3|wav|mid|midi|ra|avi|mpg|mpeg|asf|asx|wma|mov|flv|swf"; //允许上传的视频类型
        private string _uploadFilesType = "doc|docx|xls|ppt|wps|zip|rar|txt|jpg|jpeg|gif|bmp|swf|png|flv|swf|rm|html|htm"; //允许上传的文件类型
        private string _uploadImageType = "jpg|jpeg|gif|bmp|png"; //允许上传的图片类型
        private string _thumbnailWidth = "120"; //缩略图宽度
        private string _thumbnailHeight = "120"; //缩略图高度
        private string _isEnableWatermark = "1"; //是否启用水印功能，1=是，0=否
        private string _watermarkType = "1"; //水印类型，0=文字，1=图片
        private string _watermarkPic = "Images/logo.gif"; //水印图片路径
        private string _watermarkText = "华强北在线"; //水印文字
        private string _watermarkFontSize = "12"; //水印文字大小
        private string _watermarkFontType = "黑体"; //水印字体类型，如“黑体”
        private string _watermarkFontStyle = "0"; //水印字体样式，0=正常，1=粗体，2=斜体，3=下划线
        private string _watermarkFontBorder = "1"; //水印字体边框大小，0表示没有边框
        private string _watermarkFontBorderColor = "CC0000"; //水印字体边框颜色
        private string _watermarkFontColor = "FFFFFF"; //水印字体颜色
        private string _watermarkTransparency = "80"; //水印透明度
        private string _watermarkLocation = "0"; //水印位置，0=右上，1=左上，2=右下，3=左下，4=居中 
        #endregion

        #region 属性
        #region 上传文件配置和缩略图配置
        /// <summary>
        /// 图片域名，不填写则为默认域名
        /// </summary>
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
        /// <summary>
        /// 图片保存路径，默认保存在根目录下UploadFiles/Images下
        /// </summary>
        public string ImageSavePath
        {
            get { return _imageSavePath; }
            set { _imageSavePath = value; }
        }
        /// <summary>
        /// 文件域名，不填写则为默认域名
        /// </summary>
        public string FileUrl
        {
            get { return _fileUrl; }
            set { _fileUrl = value; }
        }
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FileSavePath
        {
            get { return _fileSavePath; }
            set { _fileSavePath = value; }
        }
        /// <summary>
        /// 视频域名，不填写则为默认域名
        /// </summary>
        public string MediaUrl
        {
            get { return _mediaUrl; }
            set { _mediaUrl = value; }
        }
        /// <summary>
        /// 视频保存路径
        /// </summary>
        public string MediaSavePath
        {
            get { return _mediaSavePath; }
            set { _mediaSavePath = value; }
        }
        /// <summary>
        /// 是否允许上传，0=否，1=是
        /// </summary>
        public string IsEnableUpload
        {
            get { return _isEnableUpload; }
            set { _isEnableUpload = value; }
        }
        /// <summary>
        /// 上传使用控件，upload.aspx=使用vs上传控件，neatupload.aspx=使用neatupload上传控件
        /// </summary>
        public string UploadControl
        {
            get { return _uploadControl; }
            set { _uploadControl = value; }
        }
        /// <summary>
        /// 允许上传的最大文件大小
        /// </summary>
        public string UploadFilesSize
        {
            get { return _uploadFilesSize; }
            set { _uploadFilesSize = value; }
        }
        /// <summary>
        /// 允许上传的最大图片大小
        /// </summary>
        public string UploadImageSize
        {
            get { return _uploadImageSize; }
            set { _uploadImageSize = value; }
        }
        /// <summary>
        /// 允许上传的最大视频大小
        /// </summary>
        public string UploadMediaSize
        {
            get { return _uploadMediaSize; }
            set { _uploadMediaSize = value; }
        }
        /// <summary>
        /// 允许上传的视频类型
        /// </summary>
        public string UploadMediaType
        {
            get { return _uploadMediaType; }
            set { _uploadMediaType = value; }
        }
        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public string UploadFilesType
        {
            get { return _uploadFilesType; }
            set { _uploadFilesType = value; }
        }
        /// <summary>
        /// 允许上传的图片类型
        /// </summary>
        public string UploadImageType
        {
            get { return _uploadImageType; }
            set { _uploadImageType = value; }
        }
        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public string ThumbnailWidth
        {
            get { return _thumbnailWidth; }
            set { _thumbnailWidth = value; }
        }
        /// <summary>
        /// 缩略图高度
        /// </summary>
        public string ThumbnailHeight
        {
            get { return _thumbnailHeight; }
            set { _thumbnailHeight = value; }
        }
        /// <summary>
        /// 是否启用水印功能，1=是，0=否
        /// </summary>
        public string IsEnableWatermark
        {
            get { return _isEnableWatermark; }
            set { _isEnableWatermark = value; }
        }
        /// <summary>
        /// 水印类型，0=文字，1=图片
        /// </summary>
        public string WatermarkType
        {
            get { return _watermarkType; }
            set { _watermarkType = value; }
        }
        /// <summary>
        /// 水印图片路径
        /// </summary>
        public string WatermarkPic
        {
            get { return _watermarkPic; }
            set { _watermarkPic = value; }
        }
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WatermarkText
        {
            get { return _watermarkText; }
            set { _watermarkText = value; }
        }
        /// <summary>
        /// 水印文字大小
        /// </summary>
        public string WatermarkFontSize
        {
            get { return _watermarkFontSize; }
            set { _watermarkFontSize = value; }
        }
        /// <summary>
        /// 水印字体类型
        /// </summary>
        public string WatermarkFontType
        {
            get { return _watermarkFontType; }
            set { _watermarkFontType = value; }
        }
        /// <summary>
        /// 水印字体样式，0=正常，1=粗体，2=斜体，3=下划线
        /// </summary>
        public string WatermarkFontStyle
        {
            get { return _watermarkFontStyle; }
            set { _watermarkFontStyle = value; }
        }
        /// <summary>
        /// 水印字体边框大小，0表示没有边框
        /// </summary>
        public string WatermarkFontBorder
        {
            get { return _watermarkFontBorder; }
            set { _watermarkFontBorder = value; }
        }
        /// <summary>
        /// 水印字体边框颜色
        /// </summary>
        public string WatermarkFontBorderColor
        {
            get { return _watermarkFontBorderColor; }
            set { _watermarkFontBorderColor = value; }
        }
        /// <summary>
        /// 水印字体颜色
        /// </summary>
        public string WatermarkFontColor
        {
            get { return _watermarkFontColor; }
            set { _watermarkFontColor = value; }
        }
        /// <summary>
        /// 水印透明度
        /// </summary>
        public string WatermarkTransparency
        {
            get { return _watermarkTransparency; }
            set { _watermarkTransparency = value; }
        }
        /// <summary>
        /// 水印位置，0=右上，1=左上，2=右下，3=左下，4=居中
        /// </summary>
        public string WatermarkLocation
        {
            get { return _watermarkLocation; }
            set { _watermarkLocation = value; }
        }
        #endregion
        #endregion
    }
}
