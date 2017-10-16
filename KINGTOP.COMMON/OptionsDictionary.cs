using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Common
{
    public class OptionsDictionary
    {
        Dictionary<int, string> Cert = new Dictionary<int, string>();
        Dictionary<string, string> Cert2 = new Dictionary<string, string>();

        public OptionsDictionary(string FindOption)
        {
            switch (FindOption.ToLower())
            {
                case "propertys":  //商家性质类型
                    Cert.Add(0, "外资(欧美)");
                    Cert.Add(1, "外资(非欧美)");
                    Cert.Add(2, "合资(欧美)");
                    Cert.Add(3, "合资(非欧美)");
                    Cert.Add(4, "国企");
                    Cert.Add(5, "民企(非欧美)");
                    Cert.Add(6, "外企代表处");
                    Cert.Add(7, "其他性质");
                    break;
                case "employees": //商家规模、人数
                    Cert.Add(0, "少于50人");
                    Cert.Add(1, "50-150人");
                    Cert.Add(2, "150-500人");
                    Cert.Add(3, "500人以上");
                    break;
                case "field": //自定义字段类型
                    Cert.Add(0, "单行文本框(text)");
                    Cert.Add(1, "下拉列表(select)");
                    Cert.Add(2, "单选按钮(radio)");
                    Cert.Add(3, "复选按钮(checkbox)");
                    Cert.Add(4, "选择图片(img)");
                    Cert.Add(5, "选择文件(files)");
                    Cert.Add(6, "多行文本框(ntext)");
                    Cert.Add(7, "密码框(password)");
                    Cert.Add(8, "短时间类型(smallDatetime)");
                    Cert.Add(9, "多选(nselect)");
                    break;
                case "fields":
                    Cert.Add(0, "请选择");
                    Cert.Add(1, "单行文本框(text)");
                    Cert.Add(2, "多行文本域(ntext)");
                    Cert.Add(3, "多行文本编辑器(edit)");
                    Cert.Add(4, "单选按钮(radio)");
                    Cert.Add(5, "复选按钮(checkbox)");
                    Cert.Add(6, "下拉单选(select)");
                    Cert.Add(7, "下拉多选(nselect)");
                    Cert.Add(8, "数字");
                    Cert.Add(9, "货币");
                    Cert.Add(10, "日期");
                    Cert.Add(11, "图片");
                    Cert.Add(12, "文件");
                    Cert.Add(13, "密码框(password)");
                    Cert.Add(14, "超链接");
                    break;
                case "modelcalss":
                    Cert2.Add("", "请选择");
                    Cert2.Add("0", "商户模型");
                    Cert2.Add("1", "会员模型");
                    break;
                case "rang":
                    Cert2.Add("", "请选择");
                    Cert2.Add("5", "★★★★★★");
                    Cert2.Add("4", "★★★★");
                    Cert2.Add("3", "★★★");
                    Cert2.Add("2", "★★");
                    Cert2.Add("1", "★");
                    Cert2.Add("0", "无");
                    break;
                case "color": //颜色列表(按字母顺序排列)
                    Cert2.Add("", "颜色");
                    Cert2.Add("#00CC00", "");
                    Cert2.Add("#000", "");
                    Cert2.Add("#0000FF", "");
                    Cert2.Add("#B3EE3A", "");
                    Cert2.Add("#CC0000", "");
                    Cert2.Add("#EEC900", "");
                    Cert2.Add("#FFF", "");
                    Cert2.Add("#FFFF00", "");
                    Cert2.Add("#FF00FF", "");
                    break;
                case "link":  //友情链接类型(1/文字链接，2/Logo链接)
                    Cert2.Add("1", "文字链接");
                    Cert2.Add("2", "Logo链接");
                    break;
                case "memberstate": //会员状态 (1/正常，2/锁定)
                    Cert2.Add("1", "正常");
                    Cert2.Add("2", "锁定");
                    break;
                case "emailrang":  //邮件状态
                    Cert2.Add("1", "一般邮件");
                    Cert2.Add("2", "重要邮件");
                    Cert2.Add("3", "紧急邮件");
                    break;
                case "marriage":  //婚姻状态
                    Cert2.Add("0", "未婚");
                    Cert2.Add("1", "已婚");
                    Cert2.Add("2", "保密");
                    break;
                case "scoresource"://积分来源
                    Cert.Add(1, "购物");
                    Cert.Add(2, "评论");
                    Cert.Add(3, "兑换");
                    break;
                case "infomationsource"://站内消息
                    Cert.Add(0, "订单");
                    Cert.Add(1, "账户");
                    break;
                case "messagesource"://留言类别
                    Cert.Add(0, "留言");
                    Cert.Add(1, "投诉");
                    Cert.Add(2, "询问");
                    Cert.Add(3, "售后");
                    Cert.Add(4, "求购");
                    break;
                case "degreefrom"://学历DEGREEFROM
                    Cert2.Add ("","--请选择--");
                    Cert2.Add ("1","初中");
                    Cert2.Add ("2","高中");
                    Cert2.Add ("3","中技");
                    Cert2.Add ("4","中专");
                    Cert2.Add ("5","大专");
                    Cert2.Add ("6","本科");
                    Cert2.Add ("7","硕士");
                    Cert2.Add ("8","博士");
                    Cert2.Add ("9","其他");
                    break;
                case "level"://英语、计算机水平
                    Cert2.Add("", "--请选择--");
                    Cert2.Add("0", "一般");
                    Cert2.Add("1", "熟练");
                    Cert2.Add("2", "精通");
                    break;
            }
        }


        /// <summary>
        /// 返回泛型的记录数目
        /// </summary>
        /// <returns></returns>
        public int getDictionaryCounts
        {
            get
            {
                return Cert.Count;
            }
        }

        /// <summary>
        /// 返回字典中指字键所对应的值
        /// </summary>
        /// <param name="DictName">键的名称</param>
        /// <returns>键对应的值</returns>
        public string getDictionaryValue(int DictName)
        {
            try { return Cert[DictName].ToString(); }
            catch { return null; }
        }

        public string getDictionaryValue(string DictName)
        {
            try { return Cert2[DictName].ToString(); }
            catch { return null; }
        }

        /// <summary>
        /// 返回泛型字典的原型
        /// </summary>
        public Dictionary<int, string> getDictionary
        {
            get
            {
                return Cert;
            }
        }

       public Dictionary<string, string> getDictionary2
        {
            get
            {
                return Cert2;
            }
        }

    }
}