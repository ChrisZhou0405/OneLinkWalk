#region 引用程序集using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Xml;
using System.Xml.XPath;

using KingTop.Common;
using KingTop.Model.Content;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-26
// 功能描述：对模型链接操作
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ModelManage
    {
        #region 私有变量成员
        private Model.Content.ModelManage mModelManage = null;
        private int order;                // 当前最大排序值        private string operationName;     // 当前模型的所有操作名称        private string operationTitle;    // 当前模型的所有操作别名        private int operationCount;       // 当前模型的所有操作的数目
        private string listButtonValue;   // 选中的按钮接的配置节点中的ID
        private string rptbuttonValue;    // 选中的列表操作列配置节点中的ID
        #endregion

        #region 属性        /// <summary>
        /// 选中的按钮接的配置节点中的ID
        /// </summary>
        public string ListButtonValue
        {
            set { this.listButtonValue = value; }
            get { return this.listButtonValue; }
        }

        /// <summary>
        /// 选中的列表操作列配置节点中的ID
        /// </summary>
        public string RptButtonValue
        {
            set { this.rptbuttonValue = value; }
            get { return this.rptbuttonValue; }
        }
        #endregion

        #region 初始化函数        /// <summary>
        /// 初始化        /// </summary>
        /// <param name="modelManage">ModelManage模实列</param>
        public ModelManage(Model.Content.ModelManage modelManage)
        {
            this.mModelManage = modelManage;
            this.order = Utils.ParseInt(Public.GetTableID("0", "K_ModelField")[1], 999);
        }

        public ModelManage()
        { }
        #endregion

        #region 加载自定义（链接，功能，列表列）
        /// <summary>
        /// 加载自定义（链接，功能，列表列）
        /// </summary>
        /// <param name="arrList"></param>
        /// <param name="objContainer"></param>
        /// <param name="listControl"></param>
        /// <param name="chkList"></param>
        /// <param name="hdnList"></param>
        public void InitSelfDefinedList(string[] arrList, string objContainer, HtmlGenericControl listControl, CheckBoxList chkList, HtmlInputHidden hdnList)
        {
            if (arrList.Length > 0)
            {
                StringBuilder htmlCode;     // HTML代码
                string[] listItem;         // 自定义项
                string showTemplate;       // 显示模板

                htmlCode = new StringBuilder();
                showTemplate = Utils.GetResourcesValue("Model", "SelfDefinedListTemplate").Replace("{objContainer}", objContainer);

                if (chkList != null)
                {
                    ControlUtils.SetGetCheckBoxListSelectValue(chkList, arrList[0]);

                    if (arrList.Length > 1)
                    {
                        hdnList.Value = arrList[1];
                        listItem = arrList[1].Split(new char[] { ',' });

                        foreach (string item in listItem)
                        {
                            string[] temp;

                            temp = item.Split(new char[] { '|' });

                            if (temp.Length > 1)
                            {
                                htmlCode.Append(showTemplate.Replace("{name}", temp[0]).Replace("{value}", temp[1]));
                            }
                        }

                        listControl.InnerHtml = htmlCode.ToString();
                    }
                }
                else
                {
                    hdnList.Value = arrList[0];
                    listItem = arrList[0].Split(new char[] { ',' });

                    foreach (string item in listItem)
                    {
                        string[] temp;

                        temp = item.Split(new char[] { '|' });

                        if (temp.Length > 1)
                        {
                            htmlCode.Append(showTemplate.Replace("{name}", temp[0]).Replace("{value}", temp[1]));
                        }
                    }

                    listControl.InnerHtml = htmlCode.ToString();
                }

            }

        }
        #endregion

        #region 加载文本链接，铵钮链接 XML配置
        public void InitLinkList(CheckBoxList chkl,bool isButtonList,bool isAdd)
        {
            string configPath;
            string xpath;
            XPathNodeIterator navNodeXPath;

            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "ListPath");
            if (isButtonList)
            {
               xpath = "/config/listbutton/link";
            }
            else
            {
                xpath = "/config/listlink/link";
            }
            navNodeXPath = GetNodeIterator(configPath, xpath);

            if (isAdd)      // 添加
            {
                while (navNodeXPath.MoveNext())
                {
                    ListItem item = new ListItem();
                    item.Text = navNodeXPath.Current.GetAttribute("text", "");
                    item.Value = navNodeXPath.Current.GetAttribute("id", "");
                    if (!string.IsNullOrEmpty(navNodeXPath.Current.GetAttribute("isPublic", "")))
                    {
                        item.Selected = true;
                    }
                    chkl.Items.Add(item);
                }
            }
            else   // 编辑
            {
                while (navNodeXPath.MoveNext())
                {
                    ListItem item = new ListItem();
                    item.Text = navNodeXPath.Current.GetAttribute("text", "");
                    item.Value = navNodeXPath.Current.GetAttribute("id", "");
                    chkl.Items.Add(item);
                }
            }
        }
        #endregion

        #region 加载列表操作列
        /// <summary>
        /// 加载列表操作列
        /// </summary>
        /// <param name="chklOperationColumn">当前模型的操作列配置,为空时为添加页加载</param>
        /// <param name="operationColumn"></param>
        public void InitOperationColumn(CheckBoxList chklOperationColumn,string operationColumn)
        {
            string configPath;
            string xpath;
            XPathNodeIterator navNodeXPath;
            string operationColumnMark;

            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "ListPath");
            xpath = "/config/rptbutton/link";
            navNodeXPath = GetNodeIterator(configPath, xpath);

            if (operationColumn == null)  // 添加页面
            {
                while (navNodeXPath.MoveNext())
                {
                    ListItem item = new ListItem();
                    item.Text = navNodeXPath.Current.GetAttribute("text", "");
                    item.Value = navNodeXPath.Current.GetAttribute("id", "");
                    if (!string.IsNullOrEmpty(navNodeXPath.Current.GetAttribute("isPublic","")))
                    {
                        item.Selected = true;
                    }

                    chklOperationColumn.Items.Add(item);
                }
            }
            else  // 编辑页
            {
                operationColumnMark = "," + operationColumn + ",";
                while (navNodeXPath.MoveNext())
                {
                    ListItem item = new ListItem();
                    item.Text = navNodeXPath.Current.GetAttribute("text", "");
                    item.Value = navNodeXPath.Current.GetAttribute("id", "");

                    if (operationColumnMark.Contains("," + item.Value + ","))  // 初始选中
                    {
                        item.Selected = true;
                    }

                    chklOperationColumn.Items.Add(item);
                }
            }
        }
        #endregion

        #region 获取文本链接、链接按钮设置        /// <summary>
        /// 获取文本链接、链接按钮设置        /// </summary>
        /// <param name="hdnList">保存自定义链接的HtmlInputHidden</param>
        /// <param name="chkList"></param>
        /// <returns></returns>
        public string GetLinkList(HtmlInputHidden hdnList, CheckBoxList chkList)
        {
            string listLink;

            listLink = ControlUtils.GetCheckBoxListSelectValue(chkList);

            if (string.IsNullOrEmpty(listLink))
            {
                foreach (ListItem item in chkList.Items)
                {
                    if (item.Value != "none")
                    {
                        listLink += item.Value + ",";
                    }
                }
            }

            listLink += "$" + hdnList.Value;
            return listLink;
        }
        #endregion

        #region 获取解析后的链接设置
        /// <summary>
        ///  获取链接(文本，按钮)设置
        /// </summary>
        /// <param name="originalValue">更新前值</param>
        /// <param name="chkList">链接复选框链接或按钮复选框链接</param>
        /// <param name="isButtonList">是否是按钮链接</param>
        /// <param name="isAdd">操作类型 添加 True  编辑 False</param>
        /// <returns>[0] 需删除的链接 [1] 需添加的链接</returns>
        public  List<LinkList>[] GetLinkList(string originalValue, string chkListSelectedValue,bool isButtonList,bool isAdd)
        {
            string conifgPath;
            string xpath;
            XPathNodeIterator navNodeXPath;     // 链接/按钮配置根节点指针            XPathNodeIterator currentNav;       // 临时指针,指向当前操作的节点            string currentValue;                // 需更新的值列
            string[] originalNodeID;            // 更新前的链接ID
            string[] currentNodeID;             // 需要更新的链接ID
            List<LinkList> originalLinkList;    // 由链接ID对应的更新前的链接模（节点）
            List<LinkList> currentLinkList;     // 由链接ID对应的需更新的链接模（节点）
            List<LinkList>[] returnValue;

            currentValue = chkListSelectedValue;
            originalLinkList = new List<LinkList>();
            currentLinkList = new List<LinkList>();
            returnValue = new List<LinkList>[2];

            // 没有更改链接选择则返回            if (string.Equals(originalValue,currentValue) && !string.IsNullOrEmpty(currentValue))
            {
                return returnValue;
            }

            conifgPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "ListPath");
            if (isButtonList)
            {
                xpath = "/config/listbutton";
            }
            else
            {
                xpath = "/config/listlink";
            }
            navNodeXPath = GetNodeIterator(conifgPath,xpath);
            navNodeXPath.MoveNext();

            currentNodeID = currentValue.Split(new char[] { ',' });
            originalNodeID = originalValue.Split(new char[] { ',' });

            // 嵌套循环除去原来与当前选择相同的值            foreach (string currentID in currentNodeID)
            {
                foreach (string originalID in originalNodeID)
                {
                    if ((currentID == originalID) && !(string.IsNullOrEmpty(currentID) || string.IsNullOrEmpty(originalID)))
                    {
                        originalValue = originalValue.Replace(currentID.ToString() + ",", "").Replace(currentID.ToString(), "");
                        currentValue = currentValue.Replace(currentID.ToString() + ",", "").Replace(currentID.ToString(), "");
                    }
                }
            }

            currentNodeID = currentValue.Split(new char[] { ',' });
            originalNodeID = originalValue.Split(new char[] { ',' });

            // 选择的新链接
            if (currentNodeID.Length > 0 || string.IsNullOrEmpty(currentValue))
            {
                // 默认全部选中
                if (string.IsNullOrEmpty(currentValue) && isAdd)
                {
                    currentNav = navNodeXPath.Current.Select("link/model/field");
                    while (currentNav.MoveNext())
                    {
                        AddLinkList(ref currentNav, ref currentLinkList);
                    }
                }
                else
                {
                    // 遍历数组中的ID获取<model>节点的字段配置信息
                    foreach (string link in currentNodeID)
                    {
                        if (!string.IsNullOrEmpty(link))
                        {
                            currentNav = navNodeXPath.Current.Select("link[@id=" + link + "]/model/field");
                        }
                        else
                        {
                            continue;
                        }

                        // 存在字段配置
                        if (currentNav != null && currentNav.Count > 0)
                        {
                            AddLinkList(ref currentNav, ref currentLinkList);
                        }
                    }
                }
            }

            // 取消的链接            if (originalNodeID.Length > 0)
            {
                // 遍历数组中的ID获取<model>节点的字段配置信息
                foreach (string link in originalNodeID)
                {
                    if (!string.IsNullOrEmpty(link))
                    {
                        currentNav = navNodeXPath.Current.Select("link[@id=" + link + "]/model/field");
                    }
                    else
                    {
                        continue;
                    }

                    // 存在字段配置
                    if (currentNav != null && currentNav.Count > 0)
                    {
                        while (currentNav.MoveNext())
                        {
                            LinkList temp = new LinkList();

                            try
                            {
                                temp.Alias = currentNav.Current.GetAttribute("alias", "");
                                temp.DataType = currentNav.Current.GetAttribute("type", "");
                                temp.DefaultValue = currentNav.Current.GetAttribute("default", "");
                                temp.IsNull = Utils.ParseBool(currentNav.Current.GetAttribute("isnull", ""));
                                temp.IsRadio = Utils.ParseBool(currentNav.Current.GetAttribute("isradiao", ""));
                                temp.Name = currentNav.Current.GetAttribute("name", "");
                                temp.Value = currentNav.Current.GetAttribute("value", "");
                                originalLinkList.Add(temp);
                            }
                            catch
                            {
                                // 异常提示信息:链接配置文件读取异常
                                originalLinkList = null;
                            }
                        }
                    }
                }
            }

            returnValue[0] = originalLinkList;
            returnValue[1] = currentLinkList;
            return returnValue;
        }
        #endregion

        #region 获取更新链接的SQL语句
        /// <summary>
        /// 获取更新链接的SQL语句
        /// </summary>
        /// <param name="mTxtLinkList">文本链接设置通过GetLinkList方法获取</param>
        /// <param name="mBttonLinkList">按钮链接设置通过GetLinkList方法获取</param>
        /// <returns> [0] DDL 语句  [1] DML 语句</returns>
        public string[] GetLinkListSQL(List<LinkList>[] mTxtLinkList, List<LinkList>[] mBttonLinkList)
        {
            StringBuilder delLink;              // 从字段表(K_ModelField)中删除的SQL语句列
            StringBuilder dropLink;             // 从模型表中删除表列的SQL语句列
            StringBuilder addLink;              // 从模型表中添加表列的SQL语句列
            StringBuilder insertLink;           // 从字段表(K_ModelField)中插入的SQL语句列
            List<LinkList>[] equalLink;               // 保存文本链接与按钮链接中相同的删除与添加
            string[] retunValue;                // [0] DDL 语句  [1] DML 语句
            string tableName;
            string id;

            dropLink = new StringBuilder();
            delLink = new StringBuilder();
            addLink = new StringBuilder();
            insertLink = new StringBuilder();
            equalLink = new List<LinkList>[2];
            equalLink[0] = new List<LinkList>();
            equalLink[1] = new List<LinkList>();
            retunValue = new string[2];

            if (this.mModelManage != null)
            {
                tableName = this.mModelManage.TableName;
            }
            else
            {
                // 提示信息 :获取模型表名失败
                // 写入日志
                return retunValue;
            }

            // 删除List中的重复记录
            mTxtLinkList[0] = RemoveDuplicate(mTxtLinkList[0]);
            mTxtLinkList[1] = RemoveDuplicate(mTxtLinkList[1]);
            mBttonLinkList[0] = RemoveDuplicate(mBttonLinkList[0]);
            mBttonLinkList[1] = RemoveDuplicate(mBttonLinkList[1]);
                
            for (int i = 0; i < 2; i++)
            {
                if (mBttonLinkList[i] == null || mTxtLinkList[i] == null)
                {
                    continue;
                }

                // 找出文本链接与按钮链接中相同的删除与添加
                foreach (LinkList txtlink in mTxtLinkList[i])
                {
                    foreach (LinkList btnLink in mBttonLinkList[i])
                    {
                        if (string.Equals(txtlink.Name, btnLink.Name))
                        {
                            equalLink[i].Add(txtlink);
                        }
                    }
                }

                // 消除文本链接与按钮链接中相同的删除与添加
                foreach (LinkList link in equalLink[i])
                {
                    mTxtLinkList[i].Remove(link);
                }

            }

            if (mTxtLinkList[0] != null)
            {
                // 需删除的文本链接
                foreach (LinkList link in mTxtLinkList[0])
                {
                    dropLink.Append(dal.DropTableFieldPackaing(tableName, link.Name));              // 从创建的模型表中删除列
                    delLink.Append(dal.DeleteModelFieldRowPackaing(this.mModelManage.ID, link.Name));          // 从K_ModelField表中删除本字段的记录
                }

            }

            if (mTxtLinkList[1] != null)
            {
                // 添加文本链接
                foreach (LinkList link in mTxtLinkList[1])
                {
                    this.order = this.order + 1;
                    id = Public.GetTableID("0", this.order);
                    addLink.Append(dal.AddTableFieldPackaing(tableName, link));
                    insertLink.Append(dal.InsertModelFieldRowPackaing(link, id, this.mModelManage.ID, this.order));
                }
            }

            if (mBttonLinkList[0] != null)
            {
                // 需删除的按钮链接
                foreach (LinkList link in mBttonLinkList[0])
                {
                    dropLink.Append(dal.DropTableFieldPackaing(tableName, link.Name));              // 从创建的模型表中删除列
                    delLink.Append(dal.DeleteModelFieldRowPackaing(this.mModelManage.ID, link.Name));          // 从K_ModelField表中删除本字段的记录
                }
            }

            if (mBttonLinkList[1] != null)
            {
                // 需添加的按钮链接
                foreach (LinkList link in mBttonLinkList[1])
                {
                    this.order = this.order + 1;
                    id = Public.GetTableID("0", this.order);
                    addLink.Append(dal.AddTableFieldPackaing(tableName, link));
                    insertLink.Append(dal.InsertModelFieldRowPackaing(link, id, this.mModelManage.ID, this.order));
                }
            }

            retunValue[0] = dropLink.ToString() + addLink.ToString();
            retunValue[1] = delLink.ToString() + insertLink.ToString();
            return retunValue;
        }
        #endregion

        #region 公有方法成员 获取链接配置文件的根节点指针
        /// <summary>
        /// 读取链接配置文件
        /// </summary>
        /// <param name="isButtonList">链接类型 True:按钮链接 False:文本链接</param>
        /// <returns></returns>
        public static XPathNodeIterator GetNodeIterator(string configPath,string xpath)
        {
            XPathDocument xpathDoc;
            XPathNavigator navXpath;
            XPathNodeIterator navNodeXPath;
            XPathExpression xpathExp;

            xpathDoc = new XPathDocument(configPath);
            navXpath = xpathDoc.CreateNavigator();
            xpathExp = navXpath.Compile(xpath);
            xpathExp.AddSort("@order", XmlSortOrder.Ascending, XmlCaseOrder.None, System.Threading.Thread.CurrentThread.CurrentCulture.Name, XmlDataType.Number);
            navNodeXPath = navXpath.Select(xpathExp);

            return navNodeXPath;
        }
        #endregion

        #region 消除重复项
        private List<LinkList> RemoveDuplicate(List<LinkList> linkList)
        {
            if (linkList!= null)
            {
                for (int i = 0; i < linkList.Count - 1; i++)
                {
                    for (int j = linkList.Count - 1; j > i; j--)
                    {
                        if (string.Equals(linkList[i].Name, linkList[j].Name))
                        {
                            linkList.RemoveAt(j);
                        }
                    }
                }
            }

            return linkList;
        }
        #endregion

        #region 添加指针选取的节点
        private void AddLinkList(ref XPathNodeIterator currentNav,ref List<LinkList> linkList)
        {
            while (currentNav.MoveNext())
            {
                LinkList temp = new LinkList();

                try
                {
                    temp.Alias = currentNav.Current.GetAttribute("alias", "");
                    temp.DataType = currentNav.Current.GetAttribute("type", "");
                    temp.DefaultValue = currentNav.Current.GetAttribute("default", "");
                    temp.IsNull = Utils.ParseBool(currentNav.Current.GetAttribute("isnull", ""));
                    if (!string.IsNullOrEmpty(currentNav.Current.GetAttribute("isradiao", "")))
                    {
                        temp.IsRadio = Utils.ParseBool(currentNav.Current.GetAttribute("isradiao", ""));
                    }
                    if (!string.IsNullOrEmpty(currentNav.Current.GetAttribute("length", "")))
                    {
                        temp.CharLength = Utils.ParseInt(currentNav.Current.GetAttribute("length", ""), 0);
                    }
                    temp.Name = currentNav.Current.GetAttribute("name", "");
                    temp.Value = currentNav.Current.GetAttribute("value", "");
                    linkList.Add(temp);
                }
                catch
                {
                    // 异常提示信息:链接配置文件读取异常
                    linkList = null;
                }
            }
        }
        #endregion

        #region 当前模型操作与公共操作表同步
        public bool PublicOperSynchronization()
        {
            bool result;                        // 操作结果
            string conifgPath;
            string xpath;
            XPathNodeIterator navNodeXPath;     // 指向xpath的根节点
            string operName;                    // 要加入的操作名称以逗号隔开
            string operTitle;
            string[] oper;                      // 临时变量要加入的操作
            int operCount;

            operCount = 0;
            result = true;
            operName = string.Empty;
            operTitle = string.Empty;
            conifgPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "ListPath");

            xpath = "/config/listbutton";
            navNodeXPath = GetNodeIterator(conifgPath, xpath);
            navNodeXPath.MoveNext();
            oper = GetOper(navNodeXPath, this.listButtonValue,operName, ref operCount);
            if (oper != null)
            {
                operTitle = oper[0];
                operName = oper[1];
            }

            xpath = "/config/rptbutton";
            navNodeXPath = GetNodeIterator(conifgPath, xpath);
            navNodeXPath.MoveNext();
            oper = GetOper(navNodeXPath, this.rptbuttonValue,operName, ref operCount);
            if (oper != null)
            {
                if (string.IsNullOrEmpty(operTitle))
                {
                    operTitle = oper[0];
                    operName = oper[1];
                }
                else
                {
                    operTitle = operTitle + "," + oper[0];
                    operName = operName + "," + oper[1];
                }
            }

            if (operCount > 0)
            {
                result = dal.PublicOperSynchronization(operName, operTitle, operCount);
                this.operationName = operName;
                this.operationTitle = operTitle;
                this.operationCount = operCount;
            }

            return result;
        }
        #endregion

        #region 从配置文件中获取当前模型操作
        // [0] 操作title [1] 操作name
        private string[] GetOper(XPathNodeIterator navNodeXPath, string configNodeID, string originalOperName,ref int operCount)
        {
            XPathNodeIterator currentNav;       // 临时指针,指向当前操作的节点
            string[] currentNodeID;             // 需要更新的链接ID
            string operName;                    // 要加入的操作名称以逗号隔开
            string operTitle;                   // 要加入的操作别名以逗号隔开
            string operNameMarker;
            string[] result;

            result = new string[2];
            operTitle = string.Empty;
            operName = string.Empty;
            operNameMarker = "," + originalOperName + ",";

            if (!string.IsNullOrEmpty(configNodeID))
            {
                currentNodeID = configNodeID.Split(new char[] { ',' });
                foreach (string nodeID in currentNodeID)
                {
                    if (!string.IsNullOrEmpty(nodeID))
                    {
                        currentNav = navNodeXPath.Current.Select("link[@id=" + nodeID + "]");
                        if (currentNav != null && currentNav.Count > 0)
                        {
                            string title; // 操作别名 
                            string name;  // 操作名称
                            currentNav.MoveNext();

                            title = currentNav.Current.GetAttribute("rightsAlias", "");
                            name = currentNav.Current.GetAttribute("rights", "");
                            if (!operNameMarker.Contains("," + name + ","))   // 防止重复
                            {
                                if (string.IsNullOrEmpty(operTitle))
                                {
                                    if (string.IsNullOrEmpty(title))
                                    {
                                        title = currentNav.Current.GetAttribute("text", "");
                                    }
                                    operTitle = title;
                                    operName = name;
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(title))
                                    {
                                        title = currentNav.Current.GetAttribute("text", "");
                                    }
                                    operTitle = operTitle + "," + title;
                                    operName = operName + "," + name;
                                }
                                operCount++;
                            }
                        }
                    }
                }

                result[0] = operTitle;
                result[1] = operName;
            }
            else
            {
                result = null;
            }
            return result;
        }
        #endregion

        #region 通过原值与新值分出要添加的与删除的ID  各ID用逗号隔开 [0] 需要删除 [1] 需要添加
        /// <summary>
        /// 通过原值与新值分出要添加的与删除的ID  各ID用逗号隔开 [0] 需要删除 [1] 需要添加
        /// </summary>
        /// <param name="originalValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <returns>[0] 需要删除 [1] 需要添加</returns>
        public List<string[]> CHKLValueGrouped(string orgValue, string newValue)
        {
            string[] arrNew;
            string[] arrOriginal;
            string originalValue;
            string currentValue;
            List<string[]> lstResult;

            lstResult = new List<string[]>();
            arrNew = newValue.Split(new char[] { ',' });
            arrOriginal = orgValue.Split(new char[] { ',' });
            
            originalValue = "," + orgValue + ",";
            currentValue = "," + newValue + ",";

            foreach (string currentID in arrOriginal)             // 嵌套循环除去原来与当前选择相同的值
            {
                foreach (string originalID in arrNew)
                {
                    if (currentID == originalID && !(string.IsNullOrEmpty(currentID) || string.IsNullOrEmpty(originalID)))
                    {
                        originalValue = originalValue.Replace("," + currentID.ToString() + ",", ",");
                        currentValue = currentValue.Replace("," + currentID.ToString() + ",", ",");
                        break;
                    }
                }
            }

            originalValue = originalValue.Remove(0, 1);

            if (originalValue.Length > 0)
            {
                originalValue = originalValue.Remove(originalValue.Length - 1, 1);
            }

            currentValue = currentValue.Remove(0, 1);

            if (currentValue.Length > 0)
            {
                currentValue = currentValue.Remove(currentValue.Length - 1, 1);
            }

            arrNew = currentValue.Split(new char[] { ',' });
            arrOriginal = originalValue.Split(new char[] { ',' });
            lstResult.Add(arrOriginal);
            lstResult.Add(arrNew);
            return lstResult;
        }
        #endregion
    }
}
