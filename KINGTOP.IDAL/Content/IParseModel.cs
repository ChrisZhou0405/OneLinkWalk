using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace KingTop.IDAL.Content
{
    public interface IParseModel
    {
        DataTable GetField(string modelID);
        string SaveConfig(Hashtable hsListWidth, Hashtable hsListOrder, string modelID, string operColumnWidth,Hashtable advancedConfig);
        DataTable GetRecommendAreaPosition(string areaID);
   }
}
