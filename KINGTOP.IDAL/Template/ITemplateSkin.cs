using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：何伟 2010-09-01
// 功能描述：对K_TemplateSkin表的的操作

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.IDAL
{
    public interface ITemplateSkin
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.TemplateSkin temModel);

        string TemplateSkinSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
    }
}
