using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KingTop.Common.lc
{
  public static  class StringSplit
    {
      public static string[] sp(string str, string symbol)
      {
          string[] str2=null;
            try
            {
                string st1 = str.Replace(symbol, "|");
              str2 = st1.Split('|');
            
            }catch(Exception)
            {
            }
    
           return str2;

        
      }

     public static   string strSplit(string str, int length,int maxLength)
      {
          string sptr = string.Empty;
          if (str.Length > maxLength)
          {
              sptr = str.Substring(0, length).ToString();
          }
          else

          {
              sptr = str;
          }
          return sptr+"...";
      }

     public static string replacerns(string str)
     {
     
          str = Regex.Replace(str, @">\s+?<", "><");//去除HTML中的空白字符
          str = Regex.Replace(str, @"\r\n\s*", "");
          return str;
     }
        
    }
}
