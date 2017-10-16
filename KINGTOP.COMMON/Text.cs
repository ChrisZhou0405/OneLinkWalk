using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Net;

using Microsoft.VisualBasic;

namespace KingTop.Common
{
    /// <summary>
    /// 字符串处理类
    /// </summary>
    public class Text
    {
        /// <summary>
        /// 生成静态页
        /// </summary>
        /// <param name="url">参数为将要生成的那个动态页面的地址</param>
        /// <param name="savepath">要存放地址</param>
        public static void GetHtml(string url, string savepath, string Encode)
        {
            string Result;
            if (string.IsNullOrEmpty(Encode))
                Encode = "utf-8";
            WebResponse MyResponse;
            WebRequest MyRequest = System.Net.HttpWebRequest.Create(url);
            MyResponse = MyRequest.GetResponse();
            using (StreamReader MyReader = new StreamReader(MyResponse.GetResponseStream(), System.Text.Encoding.GetEncoding(Encode)))//这里根据网站的编码格式而定
            {
                Result = MyReader.ReadToEnd();
                MyReader.Close();
            }
            FileStream fs = new FileStream(savepath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("utf-8"));
            sw.WriteLine(Result);
            sw.Close();
            fs.Close();
        }
        public static string GetDateTimeID()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmfff");
        }
        public static string GetRandom(int size)
        {
            string Num = string.Empty;
            System.Random random = new Random();
            for (int i = 0; i < size; i++)
            {

                Num += random.Next(9).ToString();
            }
            return Num;
        }
        public static DateTime ToDateTime(long TimeStamp)
        {
            if (TimeStamp.ToString().Length == 10)
            {
                TimeStamp = StrToInt64(TimeStamp.ToString() + "0000000", 0);
            }
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNowTime = new TimeSpan(TimeStamp);
            return startTime.Add(toNowTime);
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        public static long TimeStamp
        {
            get
            {
                return StrToInt64(SubString(DateTime.Now.Subtract(TimeZone.CurrentTimeZone.ToLocalTime(StrToDateTime("1970-1-1"))).Ticks.ToString(), 10), 0);
            }
        }
        public static string FixKeywords(string Keywords)
        {
            Regex r = new Regex(@"，|\s|、", RegexOptions.IgnoreCase);
            return Text.RemoveDup(Text.CommaTrim(r.Replace(Keywords, ",")));
        }


        /// <summary>
        /// 是否在指定时间范围
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public static bool TimeRange(DateTime? dt1, DateTime? dt2, DateTime? currentTime)
        {
            long t1 = currentTime.Value.Ticks;
            long t2 = dt1.Value.Ticks;
            long t3 = dt2.Value.Ticks;
            if (t2 > t3 && (t1 >= t2 || t1 <= t3)) return true;
            else if (t1 >= t2 && t1 <= t3) return true;
            return false;
        }
        public static double GetQuarter(DateTime date)
        {
            return Math.Ceiling(date.Month / 3D);
        }
        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string[] BubbleSort(string[] r)
        {
            int i, j;
            string temp;
            bool exchange;
            for (i = 0; i < r.Length; i++)
            {
                exchange = false;
                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true;
                    }
                }
                if (!exchange)
                    break;
            }
            return r;
        }
        public static int[] QuickSort(int[] pData, int left, int right)
        {
            int i, j;
            int middle, iTemp;
            i = left;
            j = right;
            middle = pData[(left + right) / 2]; //求中间值 
            do
            {
                while ((pData[i] < middle) && (i < right))//从左扫描大于中值的数 
                    i++;
                while ((pData[j] > middle) && (j > left))//从右扫描大于中值的数 
                    j--;
                if (i <= j)//找到了一对值 
                {
                    //交换 
                    iTemp = pData[i];
                    pData[i] = pData[j];
                    pData[j] = iTemp;
                    i++;
                    j--;
                }
            } while (i <= j);//如果两边扫描的下标交错，就停止（完成一次） 
            //当左边部分有值(left<j)，递归左半边 
            if (left < j)
                pData = QuickSort(pData, left, j);
            //当右边部分有值(right>i)，递归右半边 
            if (right > i)
                pData = QuickSort(pData, i, right);
            return pData;
        }



        public static string HtmlDecode(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace("&amp;", "&");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            sb.Replace("&#39;", "'");

            return sb.ToString();
        }
        public static string HtmlEncode(string s)
        {
            return HtmlEncode(s, false);
        }

        public static string HtmlEncode(string s, bool bln)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(s);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\"", "&quot;");
            sb.Replace("\'", "&#39;");

            if (bln)
            {
                return ShitEncode(sb.ToString());
            }
            else
            {
                return sb.ToString();
            }
        }
        public static string TextDecode(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            StringBuilder sb = new StringBuilder(s);
            sb.Replace("<br />", "\r\n");
            sb.Replace("<br />", "\r");
            sb.Replace("<p></p>", "\r\n\r\n");
            sb.Replace("&amp;", "&");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            sb.Replace("&#39;", "\'");
            sb.Replace("&nbsp;", " ");
            return sb.ToString();
        }
        public static string TextEncode(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            StringBuilder sb = new StringBuilder(s);
            sb.Replace("&", "&amp;");
            sb.Replace(" ", "&nbsp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\"", "&quot;");
            sb.Replace("\'", "&#39;");
            sb.Replace("\r\n\r\n", "<p></p>");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\r", "<br />");
            sb.Replace("\n", "<br />");
            return ShitEncode(sb.ToString());
        }
        /// <summary>
        /// 获取绝对匹配的正则值
        /// 如：a|b|c
        /// 将会被转成 ^a$|^b$|^c$
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GetAbsolutePattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return string.Empty;
            pattern = Regex.Replace(pattern, @"\|{2,}", "|");
            pattern = Regex.Replace(pattern, @"(^\|)|(\|$)", string.Empty);
            pattern = Regex.Replace(pattern, @"\|", "$|^");
            pattern = Regex.Replace(pattern, @"[^\$]$", "$0$$");
            pattern = Regex.Replace(pattern, @"^[^\^]", "^$0");
            return pattern;
        }
        /// <summary>
        /// 过滤非法关键字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ShitEncode(string s)
        {
            string bw=string.Empty;// = Config.Helper.BaseConfig.BadWords;
            if (string.IsNullOrEmpty(bw))
            {
                bw = "妈的|你妈|他妈|妈b|妈比|fuck|shit|我日|法轮|我操|藏独|台独";
            }
            else
            {
                bw = Regex.Replace(bw, @"\|{2,}", "|");
                bw = Regex.Replace(bw, @"(^\|)|(\|$)", string.Empty);
            }
            return Regex.Replace(s, bw, "**", RegexOptions.IgnoreCase);
        }
        public static System.Collections.Specialized.NameValueCollection StrToNameValue(string s)
        {
            return StrToNameValue(s, "&");
        }
        public static System.Collections.Specialized.NameValueCollection StrToNameValue(string s, string Separator)
        {
            System.Collections.Specialized.NameValueCollection nv = new System.Collections.Specialized.NameValueCollection();
            foreach (string t1 in s.Split(new string[] { Separator }, StringSplitOptions.None))
            {
                string[] tmp = t1.Split('=');
                if (tmp.Length != 2) continue;
                if (string.IsNullOrEmpty(tmp[0])) continue;
                nv.Add(tmp[0].Trim(), tmp[1].Trim());
            }
            return nv;
        }
        public static string GetNormalFile(string s)
        {
            return Regex.Replace(s, @"(_+?.*(\.[^\.]*)$)", "$2", RegexOptions.IgnoreCase);
        }
        public static string GetTagFile(string s, string tag)
        {
            return Regex.Replace(GetNormalFile(s), @"(.*)(.*(\.[^\.]*)$)", "$1" + tag + "$2", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 获取方图文件名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetSquareFile(string s)
        {
            return GetTagFile(s, "_Square");
        }
        /// <summary>
        /// 获取缩略图文件名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetThumbnailFile(string s)
        {
            return GetTagFile(s, "_Thumbnail");
        }
        public static string GetNewThumbnailFile(string s)
        {
            string ImgUrl = string.Empty;
            string[] img = s.Split('.');
            if (img.Length > 0&&img.Length>1)
            {
                ImgUrl = img[0] + "." + img[1];
            }
            return ImgUrl;
        }
        public static string IncStr(string s, string v, int l)
        {
            s = s.Trim();
            for (int i = StrLength(s); i < l; i++)
            {
                s = v + s;
            }
            return s;
        }
        public static Decimal Round(Decimal value, int decimals)
        {
            if (value < 0)
            {
                int p = StrToInt32(Math.Pow(10, decimals + 1).ToString(), 0);
                return Math.Round(value + 5 / p, decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }
        public static string RemoveDup(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            object[] o = RemoveDup(s.Split(',')) as object[];
            string[] s1 = new string[o.Length];
            o.CopyTo(s1, 0);
            return string.Join(",", s1);
        }
        public static object RemoveDup(object[] value)
        {
            return RemoveDup(value, true);
        }
        /// <summary>
        /// 数组去重
        /// </summary>
        /// <param name="value"></param>
        /// <param name="IgnoreCase"></param>
        /// <returns></returns>
        public static object RemoveDup(object[] value, bool IgnoreCase)
        {
            Dictionary<object, object> _hashtable = new Dictionary<object, object>();
            foreach (object o in value)
            {
                if (null == o || (o.GetType().Name == "String" && string.IsNullOrEmpty(o.ToString()))) continue;
                string v = (IgnoreCase ? o.ToString().ToLower() : o.ToString());
                if (!_hashtable.ContainsValue(v)) _hashtable.Add(o, v);
            }
            value = new object[_hashtable.Count];
            _hashtable.Keys.CopyTo(value, 0);
            _hashtable.Clear();
            _hashtable = null;
            return value;
        }

        #region 系统模板标签转换
        public static string Replace(string str, string pattern, object m)
        {
            if (null == m) return str;
            return Replace(str, pattern, m.ToString());
        }
        /// <summary>
        /// 系统模板标签转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string Replace(string str, string pattern, string m)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (string.IsNullOrEmpty(m)) m = "";
            return Regex.Replace(str, @"(\{\$" + pattern + "\\})", m, RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
        #endregion
        #region 清除字符串中连续及头尾的逗号
        /// <summary>
        /// 清除字符串中连续及头尾的逗号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CommaTrim(string s)
        {
            return Trim(s, ",");
            //if (string.IsNullOrEmpty(s)) return string.Empty;
            //s = s.Replace(",,", "").Trim();
            //if (s.Substring(0, 1) == ",") s = s.Substring(1, s.Length - 1);
            //if (s.Substring(s.Length - 1, 1) == ",") s = s.Substring(0, s.Length - 1);
            //return s;
        }
        #endregion
        public static string Trim(string s, string pattern)
        {
            return RTrim(LTrim(s, pattern), pattern);
            /*
            s = Regex.Replace(s, @"(^[" + pattern + @"])", string.Empty, RegexOptions.IgnoreCase);//清除第一个
            Regex r = new Regex(@"(\||$)");
            pattern = r.Replace(pattern, "$$$1");
            s = Regex.Replace(s, pattern, string.Empty, RegexOptions.IgnoreCase);//清除最后一个            
            return s;*/
        }
        public static string LTrim(string s, string pattern)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(pattern)) return s;
            return Regex.Replace(s, @"(^[" + pattern + @"])", string.Empty, RegexOptions.IgnoreCase);//清除第一个
        }
        public static string RTrim(string s, string pattern)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            Regex r = new Regex(@"(\||$)");
            pattern = r.Replace(pattern, "$$$1");
            return Regex.Replace(s, "[" + pattern + "]+$", string.Empty, RegexOptions.IgnoreCase);//清除最后一个            
        }
        #region 删除字符串尾部的回车/换行/空格 public static string RTrim(string str)
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }
        #endregion
        /// <summary>
        /// 密码比较，支持 16/32位
        /// </summary>
        /// <param name="Password1"></param>
        /// <param name="Password2"></param>
        /// <returns></returns>
        public static bool ComparePassword(string Password1, string Password2)
        {
            if (string.IsNullOrEmpty(Password1) || string.IsNullOrEmpty(Password2)) return false;
            if (string.Compare(Password1, Password2, true) == 0) return true;
            if (Password1.Length != Password2.Length)
            {
                if (Password1.Length == 16 && Password2.Length == 32)
                {
                    return (0 == string.Compare(Password1, Password2.Substring(8, 16), true));
                }
                else if (Password1.Length == 32 && Password2.Length == 16)
                {
                    return (0 == string.Compare(Password1.Substring(8, 16), Password2, true));
                }
            }
            return false;
        }
        /// <summary>
        /// 获取日期中的周数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekCount(System.DateTime dt)
        {
            System.DateTime day = System.DateTime.Parse(dt.Year + "-1-1");
            System.DayOfWeek dateTime = day.DayOfWeek;
            int DayCount = dt.DayOfYear;
            return (DayCount + WeekToInt(dateTime) - 2) / 7 + 1;
        }
        public static int WeekToInt(System.DayOfWeek weekday)
        {
            return Convert.ToInt16(weekday);
        }
        public static string GetRandomStr(int Count)
        {
            return GetRandomStr(Count, true, true, true);
        }
        private static int RandomCount;
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="Count">字符串长度</param>
        /// <returns></returns>
        public static string GetRandomStr(int Count, bool Numeric, bool LowerStr, bool UpperStr)
        {
            string tmp = string.Empty;
            if (Numeric) tmp += "0,1,2,3,4,5,6,7,8,9,";
            if (LowerStr) tmp += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            if (UpperStr) tmp += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            if (string.IsNullOrEmpty(tmp))
            {
                tmp += "0,1,2,3,4,5,6,7,8,9,";
            }
            string[] str = tmp.Split(new string[] { "," }, StringSplitOptions.None);
            string checkCode = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                Random rand = new Random(unchecked(RandomCount * (int)DateTime.Now.Ticks));
                int t = rand.Next(rand.Next(str.Length - 1));
                if (!string.IsNullOrEmpty(str[t]))
                    checkCode += str[t];
                RandomCount = rand.Next(9999999);
            }
            return checkCode;
        }
        /// <summary>
        /// 获取随机中文字符 GB2312编码
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static string GetRandomChineseStr(int Count)
        {
            string[] hex = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            Random rand = new Random();
            object[] bytes = new object[Count];
            for (int i = 0; i < Count; i++)
            {
                //区位码第1位 
                int r1 = rand.Next(11, 14);
                string str_r1 = hex[r1].Trim();

                //区位码第2位 
                rand = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值 
                int r2;
                if (r1 == 13)
                    r2 = rand.Next(0, 7);
                else
                    r2 = rand.Next(0, 16);
                string str_r2 = hex[r2].Trim();

                //区位码第3位 
                rand = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rand.Next(10, 16);
                string str_r3 = hex[r3].Trim();

                //区位码第4位 
                rand = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                    r4 = rand.Next(1, 16);
                else if (r3 == 15)
                    r4 = rand.Next(0, 15);
                else
                    r4 = rand.Next(0, 16);
                string str_r4 = hex[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };
                //将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);
            }

            //获取GB2312编码页（表） 
            Encoding gb = Encoding.GetEncoding("gb2312");
            //根据汉字编码的字节数组解码出中文汉字 
            string s = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                s += gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
            }
            return s;
        }

        public static string SqlEncode(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return Regex.Replace(str, "'", "''", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 移除JavaScript代码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveJavaScript(string s)
        {
            string pattern = @"<script[^>]*>([\s\S](?!<script))*?</script>";
            return Regex.Replace(s, pattern, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
        /// <summary>
        /// 移除Iframe代码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveIframe(string s)
        {
            string pattern = @"<iframe[^>]*>([\s\S](?!<iframe))*?</iframe>";
            return Regex.Replace(s, pattern, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
        /// <summary>
        /// 移除HTML代码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveHtml(string s)
        {
            string pattern = @"(<[^>]*>)";
            s = Regex.Replace(s, pattern, string.Empty, RegexOptions.IgnoreCase);
            s = Regex.Replace(s, "&nbsp;", string.Empty, RegexOptions.IgnoreCase);
            return s;

        }
        public static Byte ToByte(object value, Byte defValue)
        {
            try
            {
                return Convert.ToByte(value);
            }
            catch { return defValue; }
        }
        public static Byte StrToByte(string strValue, Byte defValue)
        {
            try
            {
                return Convert.ToByte(strValue);
            }
            catch { return defValue; }
        }
        public static Byte StrToByte(object objValue, Byte defValue)
        {
            if (null == objValue) return defValue;
            return StrToByte(objValue.ToString(), defValue);
        }
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    intValue = Convert.ToSingle(strValue);
                }
            }
            return intValue;
        }
        public static string JoinSqlCondition(string ConditionStr, string NewCondition)
        {
            return JoinSqlCondition(ConditionStr, NewCondition, 0);
        }
        /// <summary>
        /// 添加SQL条件
        /// </summary>
        /// <param name="ConditionStr">原SQL语句条件字符</param>
        /// <param name="NewCondition">新增SQL语句条件字符</param>
        /// <param name="Conditions">0=and 1=or</param>
        /// <returns></returns>
        public static string JoinSqlCondition(string ConditionStr, string NewCondition, int Conditions)
        {
            if (string.IsNullOrEmpty(ConditionStr))
                ConditionStr = NewCondition;
            else
            {
                if (Conditions == 0)
                    ConditionStr += " and " + NewCondition;
                else ConditionStr += " or " + NewCondition;
            }
            return ConditionStr;
        }
        public static string JsEncode(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("\"", "\\\"").Replace("/", "\\/").Replace("\n", "\\n").Replace("\r", "\\r");
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

        ///// <summary>
        ///// 简体
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string ToSChinese(string s)
        //{
        //    return Strings.StrConv(s, VbStrConv.SimplifiedChinese, 0);
        //}
        ///// <summary>
        ///// 繁体
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string ToFChinese(string s)
        //{
        //    return Strings.StrConv(s, VbStrConv.TraditionalChinese, 0);
        //}

        /// <summary>
        /// 判断是否包含SQL危险字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        public static DateTime StrToDateTime(string s)
        {
            try { return DateTime.Parse(s); }
            catch { return DateTime.Now; }
        }
        public static DateTime? StrToDate(string s, string format)
        {
            try { return DateTime.ParseExact(s, format, null); }
            catch { return null; }
        }
        public static DateTime? StrToDate(string s)
        {
            try { return DateTime.Parse(s); }
            catch { return null; }
        }
        public static bool? ToBoolean(object o)
        {
            if (null == o || string.IsNullOrEmpty(o.ToString())) return null;
            string s = o.ToString().Trim().ToLower();
            if (s == "1" || s == "true") return true;
            else if (s == "0" || s == "false") return false;
            else return null;
        }
        public static bool StrToBool(object o)
        {
            if (null == o) return false;
            return StrToBool(o.ToString(), false);
        }
        public static bool StrToBool(string s, bool defValue)
        {
            if (string.IsNullOrEmpty(s)) return defValue;
            s = s.Trim().ToLower();
            if (s == "1" || s == "true") return true;
            else if (s == "0" || s == "false") return false;
            else return defValue;
        }
        public static UInt32 StrToUInt32(object o, UInt32 defaultValue)
        {
            if (null == o) return defaultValue;
            return StrToUInt32(o.ToString(), defaultValue);
        }
        public static UInt32 StrToUInt32(string s, UInt32 defaultValue)
        {
            try { return Convert.ToUInt32(s); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// Object类型转换为Int32类型
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int StrToInt32(object o, int defaultValue)
        {
            if (null == o) return defaultValue;
            return StrToInt32(o.ToString(), defaultValue);
        }
        /// <summary>
        /// String类型转换为Int32类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int StrToInt32(string s, int defaultValue)
        {
            try { return Int32.Parse(s); }
            catch { return defaultValue; }
        }
        public static long StrToInt64(object o, long defaultValue)
        {
            if (null == o) return defaultValue;
            return StrToInt64(o.ToString(), defaultValue);
        }
        public static long StrToInt64(string s, long defaultValue)
        {
            try { return Int64.Parse(s); }
            catch { return defaultValue; }
        }
        public static Single StrToSingle(object o, Single defaultValue)
        {
            if (null == o) return defaultValue;
            return StrToSingle(o.ToString(), defaultValue);
        }
        public static Decimal ToDecimal(object o, Decimal defVal)
        {
            if (null == o) return defVal;
            return ToDecimal(o.ToString(), defVal);
        }
        /// <summary>
        /// SQL中的money字段用此类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(string s, Decimal defVal)
        {
            try
            {
                return Convert.ToDecimal(s);
            }
            catch { return defVal; }
        }
        public static Double ToDouble(object s, Double defVal)
        {
            if (null == s || string.IsNullOrEmpty(s.ToString())) return defVal;
            try
            {
                return Convert.ToDouble(s.ToString());
            }
            catch { return defVal; }
        }
        public static Single StrToSingle(string s, Single defaultValue)
        {
            try
            {
                return Single.Parse(s);
            }
            catch { return defaultValue; }
        }
        /// <summary>
        /// 检查字符出现的次数
        /// </summary>
        /// <param name="input"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static int GetStringCount(string str, string compare)
        {
            int index = str.IndexOf(compare);
            if (index != -1) return 1 + GetStringCount(str.Substring(index + compare.Length), compare);
            else return 0;
        }
        public static string escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            Regex r = new Regex(@"[^\x00-\xff]", RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (char str in s.ToCharArray())
            {
                if (r.IsMatch(str.ToString()))
                {
                    byte[] b = System.Text.Encoding.Unicode.GetBytes(str.ToString());
                    sb.Append("%u");
                    sb.Append(b[1].ToString("X2"));
                    sb.Append(b[0].ToString("X2"));
                }
                else sb.Append(str);
            }
            return sb.ToString();
        }
        public static string unescape(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            Regex r = new Regex(@"%u(\w{2,2})(\w{2,2})");
            foreach (Match m in r.Matches(s))
            {
                if (m.Success)
                {
                    byte[] b = new byte[2];
                    b[0] = Convert.ToByte(m.Groups[2].ToString(), 16);
                    b[1] = Convert.ToByte(m.Groups[1].ToString(), 16);
                    s = s.Replace(m.Groups[0].ToString(), System.Text.Encoding.Unicode.GetString(b));
                }
            }
            return s;
        }

        public static string UrlEncode(string s, string pattern)
        {
            Regex r = new Regex(pattern);
            foreach (Match m in r.Matches(s))
            {
                if (m.Success)
                    s = s.Replace(m.Groups[1].ToString(),
                        UrlPathEncode(m.Groups[1].ToString()));
            }
            return s;
        }
        //public static string UrlEncode(string s)
        //{
        //    return UrlEncode(s, Fetch.DefaultEncoding);
        //}
        //public static string UrlEncode(string s, Encoding encoding)
        //{
        //    if (null == encoding) encoding = Fetch.DefaultEncoding;
        //    return System.Web.HttpUtility.UrlEncode(s, encoding);
        //}
        //public static string UrlDecode(string s)
        //{
        //    return UrlDecode(s, Fetch.DefaultEncoding);
        //}
        //public static string UrlDecode(string s, Encoding encoding)
        //{
        //    if (null == encoding) encoding = Fetch.DefaultEncoding;
        //    return System.Web.HttpUtility.UrlDecode(s, encoding);
        //}
        public static string UrlPathEncode(string s)
        {
            s = System.Web.HttpContext.Current.Server.UrlPathEncode(s);
            s = s.Replace("&", "&amp;");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            s = s.Replace("\"", "&quot;");
            s = s.Replace("\'", "&apos;");
            return s;
        }
        public static string AddUrlHttp(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            s = s.Trim();
            if (!s.StartsWith("http://", true, null) &&
                !s.StartsWith("https://", true, null) &&
                !s.StartsWith("ftp://", true, null))
                s = "http://" + s;
            return s;
        }
        /// <summary>
        /// 清除 http:// 、 www. 以及结尾的 /
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveHttpAndWWW(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            s = s.Trim();
            if (s.StartsWith("http://", true, null)) s = s.Substring(7, s.Length - 7);
            s = GetDomain(s);
            if (s.Substring(s.Length - 1) == "/") s = s.Substring(0, s.Length - 1);
            return s;
        }

        /// <summary>
        /// URL修正，URL后面加 "/"
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AddUrlSuffix(string url)
        {
            try
            {
                url = url.Trim();
                url = url.Replace("\\", "/");
                if (url.Substring(url.Length - 1) != "/") url += "/";
            }
            catch { }
            return url;
        }

        public static string JoinUrl(string oldUrl, string newUrl)
        {
            if (newUrl.Substring(0, 1) == "/") newUrl = newUrl.Substring(1, newUrl.Length - 1);
            return AddUrlSuffix(oldUrl) + newUrl;
        }

        /// <summary>
        /// 目录修正，目录后面加 "\"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string AddDirSuffix(string path)
        {
            try
            {
                path = path.Trim();
                path = path.Replace("/", "\\");
                if (path.Substring(path.Length - 1) != "\\") path += "\\";
            }
            catch { }
            return path;
        }
        public static string GetDomain(string s)
        {
            string host = s;
            string[] arr = host.Split('.');
            if (arr.Length < 3 || RegExp.IsIP(host))
            {
                return host;
            }
            string domain = host.Remove(0, host.IndexOf(".") + 1);
            if (domain.StartsWith("com.") || domain.StartsWith("net.") || domain.StartsWith("org.") || domain.StartsWith("gov."))
            {
                return host;
            }
            return domain;
        }
        #region 字符串截取  public static string SubString(string Text, int StartIndex, int Length, string TailString)
        public static string SubString(string Text, int Length)
        {
            return SubString(Text, 0, Length, "");
        }
        public static string SubString(string Text, int StartIndex, int Length, string TailString)
        {
            if (Length < 1) return Text;
            if (StartIndex >= Text.Length) return "";
            int j = 0, l = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                j = j + StrLength(Text.Substring(i, 1));
                if (j <= Length) l++;
                else break;
            }
            if ((StartIndex + l) > Text.Length) l = Text.Length - StartIndex;
            string str = Text.Substring(StartIndex, l);
            if ((l + StartIndex) < Text.Length) str += TailString;
            return str;
        }
        #endregion
        #region 字符串长度，中文2个字节。
        public static int StrLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        #endregion
    }
}
