using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;


namespace KingTop.Common.lc
{
   public class DataHelper
    {
        #region DataTable 转 实体类对象List
        /// <summary>
        /// DataTable 转 对象要LIST
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>行中是对象的类，类的属性与数据字段一致</returns>
        public static IList<T> DataTableToIList<T>(DataTable dt)
        {
            // 定义集合    
            IList<T> list = new List<T>();
            // 获得此模型的类型    
            // Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                //T t = new T();
                T obj = Activator.CreateInstance<T>();
                // 获得此模型的公共属性    
                PropertyInfo[] propertys = obj.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列    
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter    
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            var pptype = pi.Name;
                            if (pptype == "AddDate")
                            {
                                value = Convert.ToDateTime(value);
                            }
                            

                            pi.SetValue(obj, value, null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        #endregion

        static JavaScriptSerializer jss = new JavaScriptSerializer();

        public static string Obj2Json(object obj)
        {
            //把集合 转成 json 数组格式字符串
            return jss.Serialize(obj);
        }

        public static string titleBracket(string str)
       {
             string bracket = string.Empty;
             string titleStr = string.Empty;
              Regex regex = new Regex(@"[\(（][^\)）]+[\)）]$");
              MatchCollection m = regex.Matches(str);
                     //匹配括号内容
                   bracket=  mStr(m);

                     //标题
                   titleStr = regex.Replace(str, "");
                   return  titleStr+","+bracket;


       }

      static  string mStr(MatchCollection m)
        {
            string s="";
            for (int i = 0; i < m.Count;i++ )
            {
                s = m[0].ToString();
            }
            return s;
        }

    }
}
