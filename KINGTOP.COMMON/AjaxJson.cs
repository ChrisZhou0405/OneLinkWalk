using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;

namespace KingTop.Common
{
    public interface IJson
    {
        string ToJson();
    }

    public static class Json
    {
        private static int m_Version;
        public static int Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }
        //public static Xml.XmlDocument ToXml(IJson source)
        //{
        //    Xml.XmlDocument xml = new Xml.XmlDocument();
        //    XmlNode Root = xml.AppendChild(xml.CreateDefaultElement());
        //    if (null == source) return xml;
        //    Root = Root.AppendChild(xml.CreateElement(source.GetType().Name.ToLower()));
        //    foreach (PropertyInfo info in source.GetType().GetProperties())
        //    {
        //        xml.SetValue(Root, info.Name, info.GetValue(source, null));
        //    }
        //    return xml;
        //}

        public static string ToJson(IJson source, int Version)
        {
            m_Version = Version;
            return ToJson(source);
        }
        public static string ToJson(IJson source)
        {
            return ToJson(source, source.GetType());
        }

        public static string ToJson(IJson source, Type type)
        {
            StringBuilder sb = new StringBuilder("{");
            int l = type.GetProperties().Length;
            for (int i = 0; i < l; i++)
            {
                PropertyInfo info = type.GetProperties()[i];
                sb.AppendFormat(@"""{0}"":", info.Name);
                //sb.AppendFormat("{0}:", info.Name);
                object value = info.GetValue(source, null);
                switch (info.PropertyType.Name)
                {
                    case "Int64":
                    case "Boolean":
                    case "Int32":
                        sb.Append((null != value ? value.ToString().ToLower() : string.Empty)); break;
                    case "String[]":
                        string[] s1 = value as string[];
                        sb.Append("[");
                        for (int j = 0; j < s1.Length; j++)
                        {
                            sb.AppendFormat("\"{0}\"", JavaScriptEncode(s1[j]));
                            if (j < s1.Length - 1) sb.Append(",");
                        }
                        sb.Append("]");
                        break;
                    case "Int32[]":
                        int[] i1 = value as int[];
                        sb.Append("[");
                        for (int j = 0; j < i1.Length; j++)
                        {
                            sb.Append(i1[j]);
                            if (j < i1.Length - 1) sb.Append(",");
                        }
                        sb.Append("]");
                        break;
                    default:
                        sb.AppendFormat("\"{0}\"", EnCode(null == value ? string.Empty : value.ToString()));
                        break;
                }
                if (i < l - 1) sb.Append(",");
            }
            sb.Append("}");
            return sb.ToString();
        }

        public static string EnCode(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace("\\", "\\\\");
            sb.Replace("\r", "\\r");
            sb.Replace("\n", "\\n");
            sb.Replace("\"", "\\\"");
            return sb.ToString();
        }

        public static string JavaScriptEncode(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("\\", "\\\\");
            sb.Replace("\r", "\\0Dx");
            sb.Replace("\n", "\\x0A");
            sb.Replace("\"", "\\x22");
            sb.Replace("\'", "\\x27");
            return sb.ToString();
        }
    }
    
}
