using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace KingTop.IDAL.Template
{
    public interface ILable
    {
        String GetObject(string tranType, KingTop.Model.SelectParams modelPrams);
        /// <summary>
        /// 获取所有的标签内容
        /// </summary>
        /// <returns></returns>
        DataSet GetLabelContentBySiteId(string tranType, KingTop.Model.SelectParams modelPrams);
        /// <summary>
        /// 获取标签数据
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        DataSet GetLable(string tranType, KingTop.Model.SelectParams modelPrams);
        /// <summary>
        /// 删除标签分类
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        int DeleteLableClass(string tranType, KingTop.Model.SelectParams modelPrams);
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        int DeleteLable(string tranType, KingTop.Model.SelectParams modelPrams);

        int SaveLableClass(string tranType, KingTop.Model.Template.LableClassInfo model);

        int SaveLable(string tranType, KingTop.Model.Template.LableInfo model);

        /// <summary>
        /// 数据库，表信息
        /// </summary>
        /// <returns></returns>
        IList<Model.SysManage.TableInfo> GetDbTable();

        /// <summary>
        /// 自由标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        DataSet GetLableFreeList(string tranType, KingTop.Model.SelectParams modelPrams);

        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        IList<KingTop.Model.SysManage.TableInfo> GetFields(string tableName);

        /// <summary>
        /// 保存自由标签相关信息
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        int SaveLableFree(string tranType, KingTop.Model.Template.LableFreeInfo model);
        /// <summary>
        /// 检查是否存在着相同名称的自由标签
        /// </summary>
        /// <param name="lableName"></param>
        /// <returns></returns>
        bool IsExistLableName(string title, int siteID, int lableID);

        /// <summary>
        /// 删除自由标签
        /// </summary>
        /// <param name="lableId"></param>
        /// <returns></returns>
        int DeleteLableFree(string lableId);

        DataTable ExecSqlString(string sql);

        string LableEnable(int isEnable, int lableid, int tableType);
    }
}
