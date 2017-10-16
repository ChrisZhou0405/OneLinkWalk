#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-02
// 功能描述：进度条
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    /// <summary>
    /// 网页进度条
    /// </summary>
    public abstract class HProgressBar
    {
        #region 进度条的初始化
        /// <summary>
        /// 进度条的初始化
        /// </summary>
        public static void Start()
        {
            Start("正在加载...");
        }

        public static void Start(string msg)
        {
            string barCodeContent;

            barCodeContent = "<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<title></title>\r\n\r\n<style>body {text-align:center;margin-top: 50px;}#ProgressBarSide {background:none repeat scroll 0 0 #EEFAFF;border:1px none #2F2F2F;height:25px;width:65%;}#ProgressBarSide {height:25px;border:1px #2F2F2F;width:65%;background:#EEFAFF;}</style>\r\n<script language=\"javascript\">\r\nfunction SetPorgressBar(msg, pos)\r\n{\r\ndocument.getElementById('ProgressBar').style.width = pos + \"%\";\r\nWriteText('Msg1',msg + \" 已完成\" + pos + \"%\");\r\n}\r\nfunction SetCompleted(msg)\r\n{\r\nif(msg==\"\")\r\nWriteText(\"Msg1\",\"完成。\");\r\nelse\r\nWriteText(\"Msg1\",msg);\r\n}\r\nfunction WriteText(id, str)\r\n{\r\nvar strTag = '<span style=\"font-family:Verdana, Arial, Helvetica;font-size=11.5px;color:#DD5800\">' + str + '</span>';\r\ndocument.getElementById(id).innerHTML = strTag;\r\n}\r\n</script>\r\n</head>\r\n<body>\r\n<div id=\"Msg1\"><span style=\"font-family:Verdana, Arial, Helvetica;font-size=11.5px;color:#DD5800\">" + msg + "</span></div>\r\n<div id=\"ProgressBarSide\" align=\"left\" style=\"color:Silver;border-width:1px;border-style:Solid; margin:0 auto;\">\r\n<div id=\"ProgressBar\" style=\"background-color:#008BCE; height:25px; width:0%;color:#fff;\"></div>\r\n</div>\r\n</body>\r\n</html>\r\n";
            System.Web.HttpContext.Current.Response.Write(barCodeContent);
            System.Web.HttpContext.Current.Response.Flush();
        }
        #endregion

        #region 滚动进度条
        /// <summary>
        /// 滚动进度条
        /// </summary>
        /// <param name="msg">在进度条上方显示的信息</param>
        /// <param name="pos">显示进度的百分比数字</param>
        public static void Roll(string msg, int pos)
        {
            string jsBlock ;
            
            jsBlock = "<script language=\"javascript\">SetPorgressBar('" + msg + "'," + pos + ");</script>";
            System.Web.HttpContext.Current.Response.Write(jsBlock);
            System.Web.HttpContext.Current.Response.Flush();
        }
        #endregion
    }
}
