
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL.Content;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-26
// 功能描述：对K_ReviewFlow表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.Content
{
    public class ReviewFlow
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.Content.IReviewFlow dal = (IDAL.Content.IReviewFlow)Assembly.Load(path).CreateInstance(path + ".Content.ReviewFlow");

        #region 根据传入的参数查询K_ReviewFlow,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ReviewFlow,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 增、改K_ReviewFlow表
        /// <summary>
        /// 增、改K_ReviewFlow表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="RevModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.Content.ReviewFlow revModel)
        {
            return dal.Save(trantype, revModel);
        }
        #endregion

        #region 设置或者删除K_ReviewFlow记录
        /// <summary>
        /// 设置或者删除K_ReviewFlow记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ReviewFlowSet(string tranType, string setValue, string IDList)
        {
            return dal.ReviewFlowSet(tranType, setValue, IDList);
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            dal.PageData(pager);
        }

        public void PageData(KingTop.Model.Pager pager, string strModelId, string strFlowId,string strStateValue)
        {
            dal.PageData(pager, strModelId, strFlowId, strStateValue);
        }
        #endregion

        #region 审核状态更改
        
        
        /// <summary>
        /// 审核状态更改
        /// </summary>
        /// <param name="strModelId"></param>
        /// <param name="strFlowStateId"></param>
        /// <returns></returns>
        public string FlowStateUpdate(string strModelId, string strFlowStateId, string strNewsId, bool isScuess, Model.Content.ReviewFlowLog modelReviewFlowLog)
        {
            ModelManage BLLModelManage = new ModelManage();
            Model.SelectParams param = new KingTop.Model.SelectParams();
            param.S1 = strModelId;
            DataTable dtModelManage = BLLModelManage.GetList("ONE", param);
            string strTableName = dtModelManage.Rows[0]["tableName"].ToString();
            dtModelManage.Dispose();
            if (strFlowStateId == "")  //选择状态,则直接给值
            {
                strFlowStateId = dal.GetFlowStateId(strTableName, strNewsId);
            }
            return FlowStateUpdateNext1(strTableName, strFlowStateId, isScuess, strNewsId, modelReviewFlowLog);
        }


        /// <summary>
        /// 查询当前状态id
        /// </summary>
        /// <param name="strModelId"></param>
        /// <param name="strFlowStateId"></param>
        /// <returns></returns>
        public string GetFlowStateId(string strModelId,string strNewsId)
        {
            ModelManage BLLModelManage = new ModelManage();
            Model.SelectParams param = new KingTop.Model.SelectParams();
            param.S1 = strModelId;
            DataTable dtModelManage = BLLModelManage.GetList("ONE", param);
            string strTableName = dtModelManage.Rows[0]["tableName"].ToString();
            dtModelManage.Dispose();
            return dal.GetFlowStateId(strTableName, strNewsId);
        }

        /// <summary>
        /// 获取当前ID状态表
        /// </summary>
        /// <param name="strFlowStateId"></param>
        /// <returns></returns>
        public DataTable GetdtFlowState(string strFlowStateId)
        {
            return dal.GetdtFlowState(strFlowStateId);
        }
      
        string FlowStateUpdateNext1(string strTableName,string strFlowStateId,bool isScuess,string strNewsId, Model.Content.ReviewFlowLog modelReviewFlowLog)
        {
            DataTable dtFlowStep = GetdtFlowState(strFlowStateId);
            modelReviewFlowLog.Desc = dtFlowStep.Rows[0]["Desc"].ToString();
            string strStateValue = null;
            if (isScuess) //如果是通过审核
            {
                strStateValue = dtFlowStep.Rows[0]["SuccessState"].ToString();              
            }
            else
            {
                strStateValue = dtFlowStep.Rows[0]["FailState"].ToString();
            }
            modelReviewFlowLog.IsSuccess = isScuess;
            string strMessage = dal.FlowStateUpdate(strTableName, strNewsId, strStateValue); //更改审核状态
            if (strMessage == "0")  //更改失败
            {
                return strMessage;
            }
            else  //更改成功后则写入批注记录
            {
                ReviewFlowLog bllReviewFlowLog = new ReviewFlowLog();
                return bllReviewFlowLog.Save("NEW", modelReviewFlowLog);
            }            
        }
        #endregion
    }
}
