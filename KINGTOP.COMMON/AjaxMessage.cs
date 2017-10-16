using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace KingTop.Common
{
    public class AjaxMessage : IJson
    {
        public AjaxMessage()
        {
        }
        public AjaxMessage(int Code, string Message)
        {
            this.m_Code = Code;
            this.m_Message = Message;
        }
        private int m_Code;
        public int Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }
        public int ItemID { get; set; }
        private string m_Message;
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
        private string m_Source;
        public string Source
        {
            get { return m_Source; }
            set { m_Source = value; }
        }
        private string m_Data;
        public string Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
        //public Xml.XmlDocument ToXml(Encoding enc)
        //{
        //    Xml.XmlDocument xml = new Xml.XmlDocument();
        //    XmlNode Root = xml.AppendChild(xml.CreateDefaultElement());
        //    XmlNode Node = Root.AppendChild(xml.CreateElement("EMessage"));
        //    xml.SetValue(Node, "Code", this.m_Code);
        //    xml.SetValue(Node, "Message", this.m_Message, true);
        //    xml.SetValue(Node, "Source", this.m_Source, true);
        //    xml.SetValue(Node, "Data", this.m_Data, true);
        //    xml.SetValue(Node, "ItemID", this.ItemID, true);
        //    if (null != enc) xml.AlterXmlDeclaration("1.0", enc.BodyName);
        //    return xml;
        //} 
        //public Xml.XmlDocument ToXml()
        //{
        //    return ToXml(null);
        //}
        public string ToJson()
        {
            return KingTop.Common.Json.ToJson(this);
        }
        public string ToJson(int Version)
        {
            return KingTop.Common.Json.ToJson(this, Version);
        }
    }
}

