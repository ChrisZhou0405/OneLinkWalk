using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSFileManage
{
    /// <summary>
    /// 实体类
    /// </summary>
    public class FileFolderInfo
    {
        private string _Name;
        private string _FullName;
        private string _FormatName;
        private string _Ext;
        private long _Size;
        private string _Type;
        private DateTime _ModifyDate;
        private string _ImgHeight;
        private string _ImgWidth;

        /// <summary>
        /// 构造方法
        /// </summary>
        public FileFolderInfo()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="p_name">文件名</param>
        /// <param name="p_fullName">包含完整路径的文件名</param>
        /// <param name="p_formatName">处理过的文件名, 含超链接</param>
        /// <param name="p_ext">扩展名</param>
        /// <param name="p_size">文件大小</param>
        /// <param name="p_type">类型</param>
        /// <param name="p_modifyDate">修改日期</param>
        /// <param name="p_imgHeight">图片高度</param>
        /// <param name="p_imgWidth">图片宽度</param>
        public FileFolderInfo(string p_name, string p_fullName, string p_formatName, string p_ext, long p_size, string p_type, DateTime p_modifyDate,string p_imgHeight,string p_imgWidth)
        {
            this._Name = p_name;
            this._FullName = p_fullName;
            this._FormatName = p_formatName;
            this._Ext = p_ext;
            this._Size = p_size;
            this._Type = p_type;
            this._ModifyDate = p_modifyDate;
            this._ImgHeight = p_imgHeight;
            this._ImgWidth = p_imgWidth;
        }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public string ImgWidth
        {
            get { return _ImgWidth; }
            set { _ImgWidth = value; }
        }


        /// <summary>
        /// 图片高度
        /// </summary>
        public string ImgHeight
        {
            get { return _ImgHeight; }
            set { _ImgHeight = value; }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// 包含完整路径的文件名
        /// </summary>
        public string FullName
        {
            get { return _FullName; }
        }

        /// <summary>
        /// 文件名, 含超链接 (已处理)
        /// </summary>
        public string FormatName
        {
            get { return _FormatName; }
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext
        {
            get { return _Ext; }
        }

        /// <summary>
        /// 文件大小 (已处理)
        /// </summary>
        public string FormatSize
        {
            get
            {
                if (this._Size == 0)
                {
                    return string.Empty;
                }

                if (this._Size.ToString().Length < 8)
                {
                    return this._Size / 1024 + " KB";
                }
                else
                {
                    return this._Size / 1024 / 1024 + " MB";
                }
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get { return _Type; }
        }

        /// <summary>
        /// 修改日期 (已处理)
        /// </summary>
        public string FormatModifyDate
        {
            get { return DateTime.Parse(this._ModifyDate.ToString("U")).AddHours(8).ToString("yyyy-MM-dd hh:mm:ss"); }
        }
    }
}
