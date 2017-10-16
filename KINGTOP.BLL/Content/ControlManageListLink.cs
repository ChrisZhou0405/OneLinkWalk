#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Configuration;
using System.Reflection;

using KingTop.Template;
using KingTop.Template.ParamType;
using KingTop.IDAL.Content;
using KingTop.Common;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-31
// 功能描述：处理解析后的模型列表 -- （文本/按钮）链接功能// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.BLL.Content
{
    public partial class ControlManageList
    {
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        public void LinkDelete(string id)
        {
            string msg;
            Regex reg;
            Match match;
            reg = new Regex(@"\{d\}\{\((?<1>[a-z A-Z 0-9 _]+)\)\((?<2>.{1,2})\)\((?<3>.*)\)\}");

            msg = string.Empty;
            match = reg.Match(this.action);

            if (match.Success)
            {
                msg = dal.LinkDelete(this.tableName, match.Groups[1].Value, match.Groups[3].Value, match.Groups[2].Value);
            }
            else
            {
                if (!string.IsNullOrEmpty(id))
                {
                    msg = dal.LinkDelete(this.tableName, id);
                }
            }

            if (msg == "1")  // 删除成功
            {
                LogContent = "删除记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 成功。";
                LogException = string.Empty;
            }
            else
            {
                LogContent = "删除记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 失败。";
                LogException = msg;
            }
        }
        #endregion

        #region 字段更新
        // 字段更新
        public void LinkEdit(string id)
        {
            Regex reg;
            MatchCollection matchCollection;
            List<string> lstSqlField;           // 更新字段名
            string[] sqlFieldValue;             // 更新值
            string[] sqlWhereField;             // 更新条件中字段名
            string[] sqlWhereFieldValue;        // 更新条件中字段值
            string[] sqlWhereFieldOpera;        // 更新条件中比较符
            string[] sqlWhereFieldJoin;         // 更新条件中各条件的连接关键字 and or
            string msg;

            lstSqlField = new List<string>();

            // 配置值 格式：action={e}{(字段)(值)}{条件}
            reg = new Regex(@"\{\((?<1>.+)\)\((?<2>.+)\)\}");
            matchCollection = reg.Matches(this.action);
            sqlWhereField = null;
            sqlWhereFieldValue = null;
            sqlWhereFieldOpera = null;
            sqlWhereFieldJoin = null;
            sqlFieldValue = null;
            msg = string.Empty;

            // 遍历所有更新项
            if (matchCollection.Count > 0)
            {
                sqlFieldValue = new string[matchCollection.Count];
                int i = 0;  // 临时计数器

                foreach (Match m in matchCollection)
                {
                    lstSqlField.Add(m.Groups[1].Value);
                    sqlFieldValue[i] = m.Groups[2].Value;
                    i = i + 1;
                }
            }
            else
            {
                msg = "0";
            }

            if (string.IsNullOrEmpty(id) && msg != "0")
            {
                // 更新条件
                reg = new Regex(@"\{(?<1>[^( ) { }]+?)(?<4>[= < >]{1})(?<2>[^( ) { }]+(?<3>\s*[u|n]\s*)?)\}");
                matchCollection = reg.Matches(this.action);

                if (matchCollection.Count > 0)
                {
                    sqlWhereField = new string[matchCollection.Count];
                    sqlWhereFieldValue = new string[matchCollection.Count];
                    sqlWhereFieldOpera = new string[matchCollection.Count];
                    sqlWhereFieldJoin = new string[matchCollection.Count];

                    int i = 0;  // 临时计数器

                    foreach (Match m in matchCollection)
                    {
                        sqlWhereField[i] = m.Groups[1].Value;
                        sqlWhereFieldOpera[i] = m.Groups[4].Value;
                        sqlWhereFieldValue[i] = m.Groups[2].Value;
                        sqlWhereFieldJoin[i] = m.Groups[3].Value;
                        i = i + 1;
                    }
                }
                else
                {
                    msg = "0";
                }
            }

            msg = dal.LinkEdit(this.tableName, id, lstSqlField.ToArray(), sqlFieldValue, sqlWhereField, sqlWhereFieldValue, sqlWhereFieldOpera, sqlWhereFieldJoin);

            if (lstSqlField.Contains("IsDel"))  // 逻辑删除操作
            {
                if (msg == "1")  // 删除成功
                {
                    LogContent = "删除记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 成功。";
                    LogException = string.Empty;
                }
                else
                {
                    LogContent = "删除记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 失败。";
                    LogException = msg;
                }
            }
            else
            {
                if (msg == "1")  // 更新成功
                {
                    LogContent = "更新记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 的字段 " + string.Join(",", lstSqlField.ToArray()) + " 为值 " + string.Join(",", sqlFieldValue) + " 成功。";
                    LogException = string.Empty;
                }
                else
                {
                    LogContent = "更新记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 的字段 " + string.Join(",", lstSqlField.ToArray()) + " 为值 " + string.Join(",", sqlFieldValue) + " 失败。";
                    LogException = msg;
                }
            }
        }
        #endregion

        #region 搜索
        /// <summary>
        /// 链接 -- 搜索
        /// </summary>
        /// <returns></returns>
        public string LinkSearch()
        {
            string actionType;                  // 操作类型
            Regex reg;                          // 配置搜索格式
            MatchCollection matchCollection;
            string[] sqlWhereField;             // 更新条件中字段名
            string[] sqlWhereFieldValue;        // 更新条件中字段值

            string[] sqlWhereFieldOpera;        // 更新条件中比较符
            string[] sqlWhereFieldJoin;         // 更新条件中各条件的连接关键字 and or
            string returnValue;

            this.action = this.urlParam;
            actionType = GetLinkType();
            sqlWhereField = null;
            sqlWhereFieldValue = null;
            sqlWhereFieldOpera = null;
            sqlWhereFieldJoin = null;

            if (actionType == "s")
            {
                // 格式：Action={s}{(当前表字段)(操作符)(值)} ... [u或n] ....  {(当前表字段)(操作符)(值或其它表字段)}
                reg = new Regex(@"\{\((?<1>[a-z A-Z _ 0-9]+)\)\((?<2>.{1,2})\)\((?<3>.*)\)\}(?<4>[u n]{1})?");
                matchCollection = reg.Matches(this.action);

                if (matchCollection.Count > 0)
                {
                    sqlWhereField = new string[matchCollection.Count];
                    sqlWhereFieldValue = new string[matchCollection.Count];
                    sqlWhereFieldJoin = new string[matchCollection.Count];
                    sqlWhereFieldOpera = new string[matchCollection.Count];
                    int i = 0;      // 临时变量

                    foreach (Match m in matchCollection)
                    {
                        sqlWhereField[i] = m.Groups[1].Value.Trim();
                        sqlWhereFieldOpera[i] = m.Groups[2].Value;
                        sqlWhereFieldValue[i] = m.Groups[3].Value;
                        sqlWhereFieldJoin[i] = m.Groups[4].Value;
                    }
                }
            }

            returnValue = dal.LinkSearch(this.tableName, sqlWhereField, sqlWhereFieldValue, sqlWhereFieldOpera, sqlWhereFieldJoin);

            // 流程步骤条件
            if (!string.IsNullOrEmpty(this.stepID) && !this.action.Contains("{s}{(FlowState)(=)"))
            {
                DataTable dtFlowState;
                string stateValue;
                string stateSqlWhere;

                stateValue = string.Empty;
                if (this.dsFlowStepState == null)
                {
                    this.dsFlowStepState = dal.GetEnabledFlowStep(this._accountID, this._siteID, this._nodeCode, this.stepID);
                }
                if (this.dsFlowStepState != null && this.dsFlowStepState.Tables.Count > 1)
                {
                    dtFlowState = this.dsFlowStepState.Tables[1];
                }
                else
                {
                    dtFlowState = null;
                }

                if (dtFlowState != null && dtFlowState.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtFlowState.Rows)
                    {
                        if (string.IsNullOrEmpty(stateValue))
                        {
                            stateValue = dr["StateValue"].ToString();
                        }
                        else
                        {
                            stateValue += "," + dr["StateValue"].ToString();
                        }
                    }

                    stateSqlWhere = dal.FlowStateSearch(this.tableName, stateValue);
                    if (!string.IsNullOrEmpty(returnValue))
                    {
                        returnValue += " and " + stateSqlWhere;
                    }
                    else
                    {
                        returnValue = stateSqlWhere;
                    }
                }
            }
            return returnValue;
        }
        #endregion

        #region 获取操作类型
        // 获取操作类型
        public string GetLinkType()
        {
            Regex reg;
            Match match;
            string actionType;

            reg = new Regex(@"\{(?<1>[dpesh]{1})\}");
            match = reg.Match(this.action);

            if (match.Success)  // 字段更新等操作
            {
                actionType = match.Groups[1].Value;
            }
            else    // 预设操作如专题设置
            {
                actionType = this.action;
            }

            return actionType;
        }
        #endregion

        #region 通过审核/取消审核
        public void FlowCheck(bool isScuess)
        {
            string infoID;

            infoID = HttpContext.Current.Request.Form["_chkID"];

            if (!string.IsNullOrEmpty(infoID))
            {
                dal.AlterFlowState(this.tableName, infoID, infoID.Split(new char[] { ',' }).Length, isScuess, this._userNo, this._ip, this._nodeCode, this._siteID, this._accountID);
            }
        }
        #endregion

        #region 生成HTML
        public void CreateHtml(string idStr, string siteDir, string uploadImgDir, string uploadFileUrl, string uploadMediaUrl, string siteURL)
        {
            Publish publish;            // HTML发布处理
            PublishParam publishParam;  // 发布参数配置
            List<string> lstMenu;

            if (!string.IsNullOrEmpty(idStr.Trim()))
            {
                publish = new Publish(this.SiteID, siteDir, siteURL);
                publish.UploadImgUrl = uploadImgDir;
                publish.MediasUrl = uploadMediaUrl;
                publish.FilesUrl = uploadFileUrl;
                publishParam = new PublishParam();
                lstMenu = new List<string>();

                publish.IsDisplayProgress = false;         // 不显示发布进度条
                publishParam.IsSiteIndex = true;           // 站点首页
                publishParam.IsMenuIndex = true;           // 栏目首页
                publishParam.IsMenuList = true;            // 栏目列表
                publishParam.IsContent = true;             // 内容页
                publishParam.UnPublished = false;          // 只生成未生成页面
                lstMenu.Add(this.NodeCode);

                publishParam.Type = PublishType.ContentIDEnum;
                publishParam.PublishTypeParam = new string[] { idStr };             //内容ID 多个ID可由 , 分隔
                publishParam.LstMenu = lstMenu;

                try
                {
                    publish.Execute(publishParam);      // 执行发布
                    LogContent = "内容记录 " + HttpContext.Current.Request.Form["hidLogTitle"] + " 发布HTML 成功。";
                    LogException = string.Empty;
                }
                catch (Exception ex)
                {
                    LogException = ex.Message;
                }
            }

        }
        #endregion
    }
}
