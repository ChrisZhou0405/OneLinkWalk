using System;
using System.Reflection;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Xml;
using KingTop.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月24日
    功能描述： 权限菜单操作类
     * 
    更新日期：4月23日   更新人：陈顺   更新原因/内容：重新整理
    ===============================================================*/
#endregion

namespace KingTop.BLL.SysManage
{
   
    public class RightTool
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.SysManage.IRightTool dal = (IDAL.SysManage.IRightTool)Assembly.Load(path).CreateInstance(path + ".SysManage.RightTool");

        #region 根据操作编码查找用户是否存在此权限
        public bool IsHaveRightByOperCode(string NodeID, string OperType, string accountId)
        {
            return dal.IsHaveRightByOperCode(NodeID,OperType, accountId);
        }
        
        #endregion

        #region 权限分配页面生成横排权限树

        /// <summary>
        /// 用户所有拥有的操作权限数组
        /// </summary>
        public ArrayList ArrUserRight = new ArrayList();

        /// <summary>
        /// 角色操作权限数组，用于判断是否角色权限
        /// </summary>
        public ArrayList ArrIsEnable = new ArrayList();

        /// <summary>
        /// 当前用户组id
        /// <summary>
        public string UserGropCode = "";

        /// <summary>
        /// 当前角色id
        /// <summary>
        public string RoleCode = "";

        /// <summary>
        /// 当前登陆账号
        /// </summary>
        public string LogUserPK = "";

        /// <summary>
        /// 站点ID
        /// </summary>
        public int SiteID = 0;

        /// <summary>
        /// 模板ID
        /// </summary>
        public int TemplateID = 0;

        //给用户组赋权，得到当前用户组的权限和所拥有角色的所有权限，用于选中状态
        public ArrayList SetModuleRightList(string strParMdlCode)
        {
            ArrayList arrReturn=dal.GetModuleRightList(this.UserGropCode,this.RoleCode, strParMdlCode);
            ArrUserRight = arrReturn;
            return arrReturn;
        }

        //给角色赋权，得到当前角色的所有权限，选中状态
        public ArrayList SetModuleRightList2(string strParMdlCode)
        {
            ArrayList arrReturn = dal.GetModuleRightList2(this.RoleCode, strParMdlCode);
            ArrUserRight = arrReturn;
            return arrReturn;
        }

        //给用户赋权，得到当前用户的权限和所拥有角色的所有权限还有该账户的所有权限，用于选中状态
        public ArrayList SetModuleRightList3(string strAccountID,string strParMdlCode)
        {
            ArrayList arrReturn = dal.GetModuleRightList3(this.UserGropCode, this.RoleCode,strAccountID, strParMdlCode);
            ArrUserRight = arrReturn;
            return arrReturn;
        }        

        //给站点模板赋权，得到当前模板的所有权限，用户选中状态
        public ArrayList SetTemplateRightList(int Template,string strParMdlCode)
        {
            ArrayList arrReturn = dal.GetTemplateRightList(Template,strParMdlCode);
            ArrUserRight = arrReturn;
            return arrReturn;
        }

        //调用GetModuleRightList2()方法，但此处的角色是多个角色，用于用户组权限中不可更改角色权限
        public ArrayList ArrEnable(string strParMdlCode)
        {
            ArrayList arrReturn = dal.GetModuleRightList2(this.RoleCode, strParMdlCode);
            ArrIsEnable = arrReturn;
            return arrReturn;
        }

        //调用GetModuleRightList()方法，得到用户所属用户组的所有权限，用于禁用
        public ArrayList ArrEnable2(string strParMdlCode)
        {
            ArrayList arrReturn = dal.GetModuleRightList(this.UserGropCode, this.RoleCode, strParMdlCode);
            ArrIsEnable = arrReturn;
            return arrReturn;
        }

        /// <summary>
        /// 循环生成树
        /// </summary>
        /// <param name="strParCode">父级节点编码，一开始是"",循环得到所有文件夹节点</param>
        /// <param name="objParCell">父级放树控件的容器</param>
        /// <param name="ParentGropID">父节点ID，CreateTreeLeaf()方法的参数</param>
        public void CreateTree(string strParCode, ref System.Web.UI.WebControls.TableCell objParCell, string ParentGropID, DataTable TreeDt, string accountId, string userGroup)
        {
            string strNodeType = "";
            string strCode = "", strName = "";
            string strNodeCode = "";
            string flowState = "";
            string tableName = "";
            bool isShowCheck = true;
            //循环得到文件夹节点数据(strParCode开始是"")
            if(TreeDt==null)
            {
                KingTop.BLL.SysManage.ModuleNode objNode=new ModuleNode ();
                TreeDt=objNode.GetModeNodeFromCache();
            }
            //DataTable DTtree = dal.GetTreeData(strParCode,Utils.ParseInt(SiteID,0));
            DataRow[] dr;
            if (strParCode == "")
                dr = TreeDt.Select("len(NodeCode)=3 and WebSiteID=" + SiteID, "NodelOrder ASC,NodeCode ASC");
            else
                dr = TreeDt.Select("NodeCode like '"+strParCode+"%' AND len(NodeCode)="+(strParCode.Length+3)+" and WebSiteID=" + SiteID, "NodelOrder ASC,NodeCode ASC");
            
            //得到父用户组中所有节点编码
            //DataTable DParentNod = dal.GetParentGropNodeID(ParentGropID);
            System.Web.UI.WebControls.Table objTbl = new Table();
            System.Web.UI.WebControls.TableRow objRow = new TableRow();
            System.Web.UI.WebControls.TableCell objCell = new TableCell();

            try
            {
                for (int i = 0; i <dr.Length ; i++)
                {
                    strNodeType =dr[i]["NodeType"].ToString().Trim().ToLower();
                    strCode = dr[i]["NodeCode"].ToString().Trim();
                    strName = dr[i]["NodeName"].ToString().Trim();
                    strNodeCode = dr[i]["NodeID"].ToString().Trim();
                    flowState = dr[i]["ReviewFlowID"].ToString();
                    tableName = (dr[i]["TableName"].ToString()+"aaaa").ToLower ().Replace("k_f_","k_u_") ;  //加aaaaa防null值报错
                    //if (flowState == "000000000000000" && tableName.Substring(0, 4) == "k_u_") 不明白为什么要加tableName.Substring(0, 4) == "k_u_"条件
                    if (flowState == "000000000000000")
                    {
                        isShowCheck = true;
                    }
                    else
                    {
                        isShowCheck = false;
                    }

                    if (!HasLeftMenuRights(int.Parse (accountId), userGroup, strCode))
                    {
                        continue;
                    }

                    //如果是文件夹类型
                    if (strNodeType == "1")
                    {
                        objTbl = new Table();
                        objRow = new TableRow();
                        objCell = new TableCell();
                        objTbl = this.CreateTable(objParCell, "tbl" + strParCode);
                        objRow = this.CreateRow(ref objTbl);

                        

                        //生成树单元格
                        objCell = this.CreateTreeCell(ref objRow, true, strName, strCode);
                        //循环得到下一级节点

                        this.CreateTree(strCode, ref objCell, ParentGropID, TreeDt,accountId ,userGroup );
                    }
                    else
                    {
                        //如果父用户组节点编码有数据(即该用户组有父用户组且父用户组权限不为空) 
                        if (ParentGropID!="")
                        {
                                //只有该节点是父用户组节点的子集时才生成叶子节点
                            if (CheckHasParentNodeRight(ParentGropID,strCode))
                                {
                                    objTbl = new Table();
                                    objRow = new TableRow();
                                    objCell = new TableCell();
                                    objTbl = this.CreateTable(objParCell, "tbl" + strParCode);
                                    objRow = this.CreateRow(ref objTbl);
                                    //生成树单元格
                                    objCell = this.CreateTreeCell(ref objRow, true, strName, strCode);
                                    //生成叶子节点
                                    CreateTreeLeaf(strCode, ref objCell, strNodeCode, ParentGropID, isShowCheck,accountId ,userGroup);
                                    //找到了就跳出循环，不用继续froeache()
                                    continue;
                                }
                        }
                        //父用户组节点编码没有数据，表明该用户组是一级用户组，直接生成叶子节点
                        else
                        {
                            objTbl = new Table();
                            objRow = new TableRow();
                            objCell = new TableCell();
                            objTbl = this.CreateTable(objParCell, "tbl" + strParCode);
                            objRow = this.CreateRow(ref objTbl);
                            //生成树单元格
                            objCell = this.CreateTreeCell(ref objRow, true, strName, strCode);
                            //生成叶子节点
                            CreateTreeLeaf(strCode, ref objCell, strNodeCode, ParentGropID, isShowCheck,accountId ,userGroup );
                        }                        
                    }
                }
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        //判断是否在父用户组中拥有该节点权限,子权限组不能大于父权限组的权限
        private bool CheckHasParentNodeRight(string ParentGropID,string NodeCode)
        {
            DataRow[] dr;
            //在父权限组中判断
            DataTable dt = GetUserGroupPermitFromCache();
            dr = dt.Select("UserGroupCode='"+ParentGropID+"' AND NodeCode Like '"+NodeCode+"%'");
            if (dr.Length > 0)
                return true;
            //在角色中判断
            dt = GetUserGroupRoleFromCache();
            DataTable dt1 = GetRolePermitFromCache();
            dr = dt.Select("UserGroupCode='" + ParentGropID + "'");
            for (int i = 0; i < dr.Length; i++)
            {
                DataRow[] dr1 = dt1.Select("RoleCode='" + dr[i]["RoleCode"].ToString() + "' AND NodeCode Like '" + NodeCode + "%'");
                if (dr1.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        //生成模板权限树
        public void CreateTree(string strParCode, ref System.Web.UI.WebControls.TableCell objParCell, int TemplateID)
        {
            string strNodeType = "";
            string strCode = "", strName = "";
            string strNodeCode = "";
            string flowState = "";
            string tableName = "";
            bool isShowCheck = true;
            //循环得到文件夹节点数据(strParCode开始是"")
            DataTable DTtree = dal.GetTemplateTreeData(strParCode, TemplateID);
            System.Web.UI.WebControls.Table objTbl = new Table();
            System.Web.UI.WebControls.TableRow objRow = new TableRow();
            System.Web.UI.WebControls.TableCell objCell = new TableCell();
            try
            {
                for (int i = 0; i < DTtree.Rows.Count; i++)
                {
                    strNodeType = DTtree.Rows[i]["NodeType"].ToString().Trim().ToLower();
                    strCode = DTtree.Rows[i]["NodeCode"].ToString().Trim();
                    strName = DTtree.Rows[i]["NodeName"].ToString().Trim();
                    strNodeCode = DTtree.Rows[i]["ID"].ToString().Trim();
                    flowState = DTtree.Rows[i]["ReviewFlowID"].ToString();
                    tableName = (DTtree.Rows[i]["TableName"].ToString() + "aaaa").ToLower().Replace("k_f_", "k_u_");  //加aaaaa防null值报错
                    //if (flowState == "000000000000000" && tableName.Substring(0, 4) == "k_u_") 不明白为什么要加tableName.Substring(0, 4) == "k_u_"条件
                    if (flowState == "000000000000000")
                    {
                        isShowCheck = true;
                    }
                    else
                    {
                        isShowCheck = false;
                    }
                    //如果是文件夹类型
                    if (strNodeType == "1")
                    {
                        objTbl = new Table();
                        objRow = new TableRow();
                        objCell = new TableCell();
                        objTbl = this.CreateTable(objParCell, "tbl" + strParCode);
                        objRow = this.CreateRow(ref objTbl);
                        //生成树单元格
                        objCell = this.CreateTreeCell(ref objRow, true, strName, strCode);
                        //循环得到下一级节点
                        this.CreateTree(strCode, ref objCell, TemplateID);
                    }
                    else
                    {                        
                        objTbl = new Table();
                        objRow = new TableRow();
                        objCell = new TableCell();
                        objTbl = this.CreateTable(objParCell, "tbl" + strParCode);
                        objRow = this.CreateRow(ref objTbl);
                        //生成树单元格
                        objCell = this.CreateTreeCell(ref objRow, true, strName, strCode);
                        //生成叶子节点
                        CreateTreeLeaf(strCode, ref objCell, strNodeCode, TemplateID, isShowCheck);
                    }
                }
                //释放资源
                DTtree.Dispose();
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
            //告诉GC回收
            DTtree = null;
        }

        public System.Web.UI.WebControls.Table CreateTable(System.Web.UI.WebControls.TableCell objCell, string strID)
        {
            System.Web.UI.WebControls.Table objTbl = new Table();
            objTbl.Style.Add("width", "100%");
            if (strID.Length == 6)
            {
                objTbl.Style.Add("display", "none");
            }
            objTbl.CellPadding = 0;
            objTbl.CellSpacing = 0;
            if (strID.Length > 0) { objTbl.Attributes.Add("name", strID); }
            objCell.Controls.Add(objTbl);
            return objTbl;
        }

        public System.Web.UI.WebControls.TableRow CreateRow(ref System.Web.UI.WebControls.Table objTbl)
        {
            System.Web.UI.WebControls.TableRow objRow = new TableRow();
            objTbl.Rows.Add(objRow);
            return objRow;
        }

        public System.Web.UI.WebControls.TableCell CreateCell(ref System.Web.UI.WebControls.TableRow objTblRow)
        {
            System.Web.UI.WebControls.TableCell objCell = new TableCell();
            objCell.Style.Add("width", "100%");
            objTblRow.Cells.Add(objCell);
            return objCell;
        }

        //生成树单元格
        public System.Web.UI.WebControls.TableCell CreateTreeCell(ref System.Web.UI.WebControls.TableRow objTblRow, bool blnExpand, string strText, string strDetailID)
        {
            System.Web.UI.WebControls.TableCell objCell = new TableCell();
            objCell.Style.Add("width", "1px");
            objCell.Attributes.Add("valign", "top");
            objCell.Style.Add("background-color", "gainsboro");//
            System.Web.UI.WebControls.Image objImg = new Image();
            objImg.ID = "imgcol" + strDetailID;
            if (blnExpand)
            {
                if(strDetailID.Length ==3)
                    objImg.ImageUrl = "../images/dtree/nolines_plus.gif";
                else
                    objImg.ImageUrl = "../images/dtree/nolines_minus.gif";
            }
            else
            {
                objImg.ImageUrl = "../images/dtree/nolines_plus.gif";
            }
            objImg.Attributes.Add("onclick", "showDetail('tbl" + strDetailID + "',this)");
            objCell.Controls.Add(objImg);

            objTblRow.Cells.Add(objCell);
            objCell = new TableCell();
            objCell.Style.Add("width", "100%");
            objCell.HorizontalAlign = HorizontalAlign.Left;

            System.Web.UI.HtmlControls.HtmlGenericControl objChk = new HtmlGenericControl();
            objChk.TagName = "input";
            objChk.Attributes.Add("type", "checkbox");
            objChk.Attributes.Add("onclick", "chkOnClick('chk" + strDetailID + "',this)");
            objChk.ID = "chk" + strDetailID;
            objCell.Controls.Add(objChk);

            System.Web.UI.HtmlControls.HtmlGenericControl objControl = new HtmlGenericControl();
            objControl.TagName = "label";
            objControl.Attributes.Add("for", objImg.ID);
            objControl.InnerHtml = strText;
            objCell.Controls.Add(objControl);
            objTblRow.Controls.Add(objCell);
            return objCell;
        }

        /// <summary>
        /// 生成叶子节点
        /// </summary>
        /// <param name="strMdlCode">父级操作编码</param>
        /// <param name="objParCell">父级放树控件的容器</param>
        /// <param name="objParCell">父用户组ID</param>
        /// <param name="IsShowCheck">是否显示审核权限</param>
        private void CreateTreeLeaf(string strMdlCode, ref TableCell objParCell, string strNodeCode, string ParentGropID,bool IsShowCheck, string accountId, string userGroup)
        {
            string strBasic = "";
            DataRow[] dr;
            //根据站点ID,父操作编码的到具体操作
            //DataTable dt = dal.GetTreeLeafData(Utils.ParseInt(SiteID, 0), strMdlCode);

            DataTable dt = GetModuleActionFromCache();
            DataTable UserDt = GetAccountPermitFromCache();  //用户权限
            DataTable GroupDt = GetUserGroupPermitFromCache();  //用户组中查询

            dr = dt.Select("NodeCode='" + strMdlCode + "'"); //得到该节点对应的所有操作

            //得到父用户组操作列表
            //DataTable DParentPer = dal.GetParentGropPer(ParentGropID);
            string strCode = "", strDesc = "";
            Table objTbl = new Table();
            TableRow objRow = new TableRow();
            TableCell objCell = new TableCell();
            bool blnHasRight = false;
            bool blnIsEnable = false;
            string strBindOperCode = "";
            string strPerCode = "";
            string strNodeId = string.Empty;
            if (dr.Length > 0)
            {
                System.Web.UI.WebControls.TableCell objCellEx = new TableCell();
                objTbl = this.CreateTable(objParCell, "tbl" + strMdlCode);
                objTbl.Style.Add("display", "inline");
                
                objRow = this.CreateRow(ref objTbl);
                objCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                System.Web.UI.WebControls.Image objImg = new Image();
                objImg.ID = "img" + strMdlCode;
                objImg.ImageUrl = "../images/dtree/hline.gif";
                objCell.Controls.Add(objImg);
                objRow.Controls.Add(objCell);
                bool isHide=true;
                objCell = new TableCell();
                for (int i = 0; i < dr.Length; i++)
                {
                    strCode = dr[i]["OperCode"].ToString().Trim();
                    strDesc = dr[i]["OperName"].ToString().Trim();
                    strBasic = dr[i]["IsDefaultOper"].ToString().Trim();
                    strBindOperCode = dr[i]["ID"].ToString().Trim();
                    strPerCode = dr[i]["ID"].ToString().Trim();
                    strNodeId = dr[i]["NodeId"].ToString().Trim();
                    if ((dr[i]["OperEngDesc"].ToString().ToLower() == "check" || dr[i]["OperEngDesc"].ToString().ToLower() == "cancelcheck") && IsShowCheck == false)
                    {
                        isHide=true;
                    }
                    else
                    {
                        isHide=false;
                    }

                    //检查权限，只显示当前用户拥有的权限
                    if (!CheckActionRights(strMdlCode, accountId, userGroup, strPerCode, UserDt, GroupDt))
                    {
                        continue;
                    }

                    if (ParentGropID!="")
                    {
                        //根据父用户组判断是否要生成树叶

                        if (CheckHasParentActionRight(ParentGropID, strMdlCode, strPerCode))
                            {
                                //权限判定，有权限的选中
                            if(!isHide)    
                                blnHasRight = this.HaveRight(strBindOperCode + "," + strNodeId);
                            else
                                blnHasRight=true;

                                //是角色权限的，灰色显示
                                blnIsEnable = IsArrEnable(strBindOperCode + "," + strNodeId);
                                //生成叶子节点单元格
                                CreateLeafCell(ref objCell, "chk" + strMdlCode + strCode, "treeleaf", strDesc, strBasic, strBindOperCode, strNodeCode,strCode, blnHasRight, blnIsEnable,isHide);
                               //进入下次循环，继续froeach()
                                //break;
                            }
                    }
                    else
                    {
                        //权限判定
                        if(!isHide)  
                             blnHasRight= this.HaveRight(strBindOperCode + "," + strNodeId);
                        else
                            blnHasRight=true;

                        blnIsEnable = IsArrEnable(strBindOperCode + "," + strNodeId);
                        //生成叶子节点单元格
                        CreateLeafCell(ref objCell, "chk" + strMdlCode + strCode, "treeleaf", strDesc, strBasic, strBindOperCode, strNodeCode,strCode, blnHasRight, blnIsEnable,isHide);                       
                    }
                    
                }
                objRow.Controls.Add(objCell);
                objTbl.Controls.Add(objRow);
                objParCell.Controls.Add(objTbl);
            }
            //释放资源，回收
            dt.Dispose();
            dt = null;
        }

        //判断是否在父用户组中节点的操作权限,子权限的操作不能大于父节点的操作
        private bool CheckHasParentActionRight(string ParentGropID, string NodeCode, string PermitCode)
        {
            DataRow[] dr;
            //在父权限组中判断
            DataTable dt = GetUserGroupPermitFromCache();
            dr = dt.Select("UserGroupCode='" + ParentGropID + "' AND PermitCode='"+PermitCode+"' AND NodeCode Like '" + NodeCode + "%'");
            if (dr.Length > 0)
                return true;
            //在角色中判断
            dt = GetUserGroupRoleFromCache();
            DataTable dt1 = GetRolePermitFromCache();
            dr = dt.Select("UserGroupCode='" + ParentGropID + "'");
            for (int i = 0; i < dr.Length; i++)
            {
                DataRow[] dr1 = dt1.Select("RoleCode='" + dr[i]["RoleCode"].ToString() + "' AND PermitCode='" + PermitCode + "' AND NodeCode Like '" + NodeCode + "%'");
                if (dr1.Length > 0)
                {
                    return true;
                    break;
                }
            }

            return false;
        }

        //生成模板权限叶子节点
        private void CreateTreeLeaf(string strMdlCode, ref TableCell objParCell, string strNodeCode, int TemplateID,bool IsShowCheck)
        {
            string strBasic = "";
            //根据模板ID,父操作编码的到具体操作

            DataTable dt = dal.GetTemplateTreeLeafData(TemplateID, strMdlCode);
            string strCode = "", strDesc = "";
            Table objTbl = new Table();
            TableRow objRow = new TableRow();
            TableCell objCell = new TableCell();
            bool blnHasRight = false;
            string strBindOperCode = "";
            string strPerCode = "";
            string strNodeId = string.Empty;
            if (dt.Rows.Count > 0)
            {
                System.Web.UI.WebControls.TableCell objCellEx = new TableCell();
                objTbl = this.CreateTable(objParCell, "tbl" + strMdlCode);
                objTbl.Style.Add("display", "inline");
                objRow = this.CreateRow(ref objTbl);
                objCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                System.Web.UI.WebControls.Image objImg = new Image();
                objImg.ID = "img" + strMdlCode;
                objImg.ImageUrl = "../images/dtree/hline.gif";
                bool isHide = true;
                objCell.Controls.Add(objImg);
                objRow.Controls.Add(objCell);

                objCell = new TableCell();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strCode = dt.Rows[i]["OperCode"].ToString().Trim();
                    strDesc = dt.Rows[i]["OperName"].ToString().Trim();
                    strBasic = dt.Rows[i]["IsDefaultOper"].ToString().Trim();
                    strBindOperCode = dt.Rows[i]["ID"].ToString().Trim();
                    strPerCode = dt.Rows[i]["ID"].ToString().Trim();
                    if ((dt.Rows[i]["OperEngDesc"].ToString().ToLower() == "check" || dt.Rows[i]["OperEngDesc"].ToString().ToLower() == "cancelcheck") && IsShowCheck == false)
                    {
                        isHide = true;
                    }
                    else
                    {
                        isHide = false;
                    }
                    //权限判定
                    if (!isHide)
                        blnHasRight = this.HaveRight(strBindOperCode);
                    else
                        blnHasRight = true;
                    //生成叶子节点单元格

                    CreateLeafCell(ref objCell, "chk" + strMdlCode + strCode, "treeleaf", strDesc, strBasic, strBindOperCode, strNodeCode, strCode, blnHasRight, false, isHide);
                }
                objRow.Controls.Add(objCell);
                objTbl.Controls.Add(objRow);
                objParCell.Controls.Add(objTbl);
            }
            //释放资源，回收

            dt.Dispose();
            dt = null;
        }

        //生成叶子节点单元格
        private System.Web.UI.WebControls.TableCell CreateLeafCell(ref System.Web.UI.WebControls.TableCell objCell, string strID, string strName, string strDesc, string strBasic, string strBindOperCode, string strNodeCode,string strOperCode, bool blnChk, bool blnIsEna,bool isHide)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl objChk = new HtmlGenericControl();
            System.Web.UI.HtmlControls.HtmlGenericControl objControl = new HtmlGenericControl();
            objCell.Style.Add("width", "100%");
            objCell.Style.Add("valign", "top");
            

            objCell.HorizontalAlign = HorizontalAlign.Left;
            objChk.TagName = "input";
            objChk.Attributes.Add("type", "checkbox");
            //if (blnChk)
            //{
            //    objChk.Attributes.Add("checked", blnChk.ToString().ToLower());
            //}
            if (blnIsEna)
            {
                objChk.Attributes.Add("disabled", "false");
            }
            if (isHide)
            {

                objChk.Attributes.Add("style", "display:none");
            }
            else if (blnChk)
            {
                objChk.Attributes.Add("checked", blnChk.ToString().ToLower());
            }

            objChk.Attributes.Add("onclick", "chkOnClickEx('" + strID + "',this)");
            objChk.Attributes.Add("basic", strBasic);
            objChk.ID = strID;
            objChk.Attributes.Add("name", strName);
            objChk.Attributes.Add("BindOperCode", strBindOperCode);
            objChk.Attributes.Add("NodeCode", strNodeCode);
            objChk.Attributes.Add("OperCode", strOperCode);
            objCell.Controls.Add(objChk);
            objControl.TagName = "label";
            if (isHide)
                objControl.Attributes.Add("style", "display:none");

            objControl.Attributes.Add("for", strID);
            objControl.InnerText = strDesc;
            objCell.Controls.Add(objControl);
            // objTblRow.Controls.Add(objCell);
            return objCell;
        }


        //判断是否有权限，用户选中
        public bool HaveRight(string strOprCode)
        {
            return HaveRight(this.ArrUserRight, strOprCode);
        }

        //判断是否角色权限，用户禁用
        public bool IsArrEnable(string strOprCode)
        {
            return HaveRight(this.ArrIsEnable, strOprCode);
        }

        /// </summary>
        /// <param name="arrModulOpr">操作权限数组</param>
        /// <param name="strOprCode">操作编码</param>
        /// <returns></returns>
        public bool HaveRight(ArrayList arrModulOpr, string strOprCode)
        {
            //return true;
            if (arrModulOpr == null) { arrModulOpr = new ArrayList(); }
            bool blnReturn = false;
            for (int i = 0; i < arrModulOpr.Count; i++)
            {
                if (strOprCode.ToLower() == arrModulOpr[i].ToString().ToLower())
                {
                    blnReturn = true; break;
                }
            }
            return blnReturn;
        }

        //保存权限(用户组)
        public void SaveData(string strUserGropCode, XmlNode objNodes, string strSubRigth)
        {
            this.UserGropCode = strUserGropCode;
            dal.SaveData(strUserGropCode, objNodes, strSubRigth);

            //更新缓存
            KingTop.Common.AppCache.Remove("UserGroupPermitCache");
            this.GetUserGroupPermitFromCache();
        }
        
        //保存权限(角色)
        public void SaveData2(string strRoleCode, XmlNode objNodes)
        {
            this.RoleCode = strRoleCode;
            dal.SaveData2(strRoleCode, objNodes);

            //更新缓存
            KingTop.Common.AppCache.Remove("RolePermitCache");
            this.GetRolePermitFromCache();
        }

        //保存权限(用户)
        public void SaveData3(string strAccountCode, XmlNode objNodes)
        {
            dal.SaveData3(strAccountCode, objNodes);

            //更新缓存
            KingTop.Common.AppCache.Remove("AccountPermitCache");
            GetAccountPermitFromCache();
        }

        //保存权限(模板)
        public void SaveDataTemplatePer(int TemplateID, XmlNode objNodes)
        {
            this.TemplateID = TemplateID;
            dal.SaveDataTemplatePer(TemplateID, objNodes);
        }
        #endregion

        public bool SetRight(string nodeCode, string accountId,string userGroup, Page p, Repeater rpt,out string ActionList)
        {
            ActionList = "";
            //超级管理员不隐藏
            if (accountId == "0")
            {
                return true;
            }

            DataTable dt = GetModuleActionFromCache();
            DataRow[] dr = dt.Select("NodeCode='" + nodeCode + "'");
            int j = 0;
            DataTable UserDt = GetAccountPermitFromCache();  //用户权限
            DataTable GroupDt = GetUserGroupPermitFromCache();  //用户组中查询
            for (int i = 0; i < dr.Length ; i++)
            {
                if(!CheckActionRights(nodeCode,accountId,userGroup,dr[i]["ID"].ToString (),UserDt ,GroupDt))
                {
                    j++;
                    PageCheck(dr[i]["OperEngDesc"].ToString().Trim(), p, false);
                    RepeaterCheck(dr[i]["OperEngDesc"].ToString().Trim(), rpt, false);
                    ActionList += "," + dr[i]["OperEngDesc"].ToString().Trim();
                }
            }
            if (j == dr.Length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckActionRights(string nodeCode, string userid, string userGroup, string action,DataTable UserDt,DataTable GroupDt)
        {
            if (userid == "0")
            {
                return true;
            }
            DataRow[] dr;
            DataTable dt;

            //用户权限
            dr = UserDt.Select("AccountID=" + userid + " and PermitCode='" + action + "' and NodeCode='" + nodeCode + "'");
            if (dr.Length > 0)
            {
                return true;
            }

            //用户组中查询
            dr = GroupDt.Select("UserGroupCode='" + userGroup + "' and PermitCode='" + action + "' and NodeCode='" + nodeCode + "'");
            if (dr.Length > 0)
            {
                return true;
            }

            //角色中查询
            dt = GetRolePermitFromCache();
            DataTable dt2 = GetUserGroupRoleFromCache();
            DataRow[] dr2 = dt2.Select("UserGroupCode='" + userGroup + "'");
            for (int i = 0; i < dr2.Length; i++)
            {
                dr = dt.Select("RoleCode='" + dr2[i]["RoleCode"].ToString() + "' and PermitCode='" + action + "' and NodeCode='" + nodeCode + "'");
                if (dr.Length > 0)
                {
                    return true;
                }
            }

            

            return false;

        }

        #region 页面权限判断
        private void PageCheck(string name, Page p, bool b)
        {
            GetButton("btn" + name, p, b);
            GetHtmlLink("lnk" + name, p, b);
            GetLinkButton("lnkb" + name, p, b);
        }

        private void GetButton(string name, Page p, bool b)
        {
            try
            {
                Control obj = p.Page.FindControl(name);
                if (obj != null)
                {
                    obj.Visible = b;
                }
            }
            catch
            {
            }
        }

        private void GetHtmlLink(string name, Page p, bool b)
        {
            try
            {
                System.Web.UI.HtmlControls.HtmlAnchor obj = (System.Web.UI.HtmlControls.HtmlAnchor)p.Page.FindControl(name);
                if (obj != null)
                {
                    obj.Visible = b;
                }
            }
            catch
            {
            }
        }

        private void GetLinkButton(string name, Page p, bool b)
        {
            try
            {
                System.Web.UI.WebControls.LinkButton obj = (System.Web.UI.WebControls.LinkButton)p.Page.FindControl(name);
                if (obj != null)
                {
                    obj.Visible = b;
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Repeater中操作权限判断
        private void RepeaterCheck(string name, System.Web.UI.WebControls.Repeater rpt, bool b)
        {
            if (rpt != null)
            {
                foreach (RepeaterItem ri in rpt.Items)
                {
                    System.Web.UI.WebControls.LinkButton lb = GetLinkButton("lnkb" + name, ri);
                    if (lb != null)
                    {
                        lb.Visible = b;
                        continue;
                    }
                    System.Web.UI.HtmlControls.HtmlAnchor hl = GetHtmlLink("lnk" + name, ri);
                    if (hl != null)
                    {
                        hl.Visible = b;
                        continue;
                    }
                    GetButton("btn" + name, ri,b);
                }
            }
        }

        private void GetButton(string name, System.Web.UI.WebControls.RepeaterItem ri,bool b)
        {
            try
            {
                System.Web.UI.WebControls.Button obj = (System.Web.UI.WebControls.Button)ri.FindControl(name);
                if (obj != null)
                {
                    obj.Visible = b;
                }
            }
            catch
            {
                GetHtmlButton(name, ri, b);
            }
        }

        private void GetHtmlButton(string name, System.Web.UI.WebControls.RepeaterItem ri, bool b)
        {
            try
            {
                Control obj1 = ri.FindControl(name);
                if (obj1 != null)
                {
                    obj1.Visible = b;
                }
            }
            catch { }
        }


        private System.Web.UI.HtmlControls.HtmlAnchor GetHtmlLink(string name, System.Web.UI.WebControls.RepeaterItem ri)
        {
            System.Web.UI.HtmlControls.HtmlAnchor obj = null;
            try
            {
                obj = (System.Web.UI.HtmlControls.HtmlAnchor)ri.FindControl(name);
            }
            catch
            {
            }
            return obj;
        }

        private System.Web.UI.WebControls.LinkButton GetLinkButton(string name, System.Web.UI.WebControls.RepeaterItem ri)
        {
            System.Web.UI.WebControls.LinkButton obj = null;
            try
            {
                obj = (System.Web.UI.WebControls.LinkButton)ri.FindControl(name);
            }
            catch
            {
            }
            return obj;
        }

        #endregion

        #region 以下由gavin编写，将权限缓存，判断和读取节点和操作都从缓存中读取数据
        //以下由gavin编写，将权限缓存，判断和读取节点和操作都从缓存中读取数据
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }

        public DataTable GetUserGroupPermitFromCache()
        {
            if (AppCache.IsExist("UserGroupPermitCache"))
            {
                return (DataTable)AppCache.Get("UserGroupPermitCache");
            }
            else
            {
                DataTable dt = GetList("USERGROUPPERMITAll", Utils.getOneParams(""));
                AppCache.AddCache("UserGroupPermitCache", dt, 1440);
                return dt;
            }
        }

        public DataTable GetRolePermitFromCache()
        {
            if (AppCache.IsExist("RolePermitCache"))
            {
                return (DataTable)AppCache.Get("RolePermitCache");
            }
            else
            {
                DataTable dt = GetList("ROLEPERMITAll", Utils.getOneParams(""));
                AppCache.AddCache("RolePermitCache", dt, 1440);
                return dt;
            }
        }

        public DataTable GetAccountPermitFromCache()
        {
            if (AppCache.IsExist("AccountPermitCache"))
            {
                return (DataTable)AppCache.Get("AccountPermitCache");
            }
            else
            {
                DataTable dt = GetList("USERPERMITAll", Utils.getOneParams(""));
                AppCache.AddCache("AccountPermitCache", dt, 1440);
                return dt;
            }
        }

        //用户组角色
        public DataTable GetUserGroupRoleFromCache()
        {
            if (AppCache.IsExist("UserGroupRoleCache"))
            {
                return (DataTable)AppCache.Get("UserGroupRoleCache");
            }
            else
            {
                DataTable dt = GetList("GROUPROLEAll", Utils.getOneParams(""));
                AppCache.AddCache("UserGroupRoleCache", dt, 1440);
                return dt;
            }
        }

        //操作缓存
        public DataTable GetModuleActionFromCache()
        {
            if (AppCache.IsExist("ModuleActionCache"))
            {
                return (DataTable)AppCache.Get("ModuleActionCache");
            }
            else
            {
                DataTable dt = GetList("ACTIONAll", Utils.getOneParams(""));
                AppCache.AddCache("ModuleActionCache", dt, 1440);
                return dt;
            }
        }

        //权限判断
        public bool HasLeftMenuRights(int UserID, string UserGroupCode, string NodeCode)
        {
            if (UserID == 0)
                return true;

            DataTable dt;
            DataRow[] dr;
            //从用户组权限中查找
            dt = GetUserGroupPermitFromCache();
            dr = dt.Select("UserGroupCode='" + UserGroupCode + "' and NodeCode like '" + NodeCode + "%'");
            if (dr.Length > 0)
            {
                return true;
            }

            //从角色中查找
            DataTable dt1 = GetRolePermitFromCache();
            DataRow[] dr1;
            dt = GetUserGroupRoleFromCache();
            dr = dt.Select("UserGroupCode='" + UserGroupCode + "'");
            for (int i = 0; i < dr.Length; i++)
            {
                dr1 = dt1.Select("RoleCode='" + dr[i]["RoleCode"].ToString() + "' and NodeCode like '" + NodeCode + "%'");
                if (dr1.Length > 0)
                {
                    return true;
                }
            }

            //从用户权限中查找
            dt = GetAccountPermitFromCache();
            dr = dt.Select("AccountID=" + UserID + " and NodeCode like '" + NodeCode + "%'");
            if (dr.Length > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
