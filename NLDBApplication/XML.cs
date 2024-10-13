using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class XML
{
    // 错误数组
    private readonly static char[] errors = new char[] { 'o', 'O', 'l' };
    // 转义数组
    private readonly static string[][] ESCAPES = new string[][]
	{
        new string[]{"&amp;",     "\x26"},
        new string[]{"&quot;",    "\x22"},
        new string[]{"&nbsp;",    "\xa0"},
        new string[]{"&shy;",     "\xad"},
        new string[]{"&Agrave;",  "\xc0"},
        new string[]{"&agrave;",  "\xe0"},
        new string[]{"&Aacute;",  "\xc1"},
        new string[]{"&aacute;",  "\xe1"},
        new string[]{"&Acirc;",   "\xc2"},
        new string[]{"&acirc;",   "\xe2"},
        new string[]{"&Atilde;",  "\xc3"},
        new string[]{"&atilde;",  "\xe3"},
        new string[]{"&Auml;",    "\xc4"},
        new string[]{"&auml;",    "\xe4"},
        new string[]{"&Aring;",   "\xc5"},
        new string[]{"&aring;",   "\xe5"},
        new string[]{"&AElig;",   "\xc6"},
        new string[]{"&aelig;",   "\xe6"},
        new string[]{"&Ccedil;",  "\xc7"},
        new string[]{"&ccedil;",  "\xe7"},
        new string[]{"&Egrave;",  "\xc8"},
        new string[]{"&egrave;",  "\xe8"},
        new string[]{"&Eacute;",  "\xc9"},
        new string[]{"&eacute;",  "\xe9"},
        new string[]{"&Ecirc;",   "\xca"},
        new string[]{"&ecirc;",   "\xea"},
        new string[]{"&Euml;",    "\xcb"},
        new string[]{"&euml;",    "\xeb"},
        new string[]{"&Igrave;",  "\xcc"},
        new string[]{"&igrave;",  "\xec"},
        new string[]{"&Iacute;",  "\xcd"},
        new string[]{"&iacute;",  "\xed"},
        new string[]{"&Icirc;",   "\xce"},
        new string[]{"&icirc;",   "\xee"},
        new string[]{"&Iuml;",    "\xcf"},
        new string[]{"&iuml;",    "\xef"},
        new string[]{"&ETH;",     "\xd0"},
        new string[]{"&eth;",     "\xf0"},
        new string[]{"&Ntilde;",  "\xd1"},
        new string[]{"&ntilde;",  "\xf1"},
        new string[]{"&Ograve;",  "\xd2"},
        new string[]{"&ograve;",  "\xf2"},
        new string[]{"&Oacute;",  "\xd3"},
        new string[]{"&oacute;",  "\xf3"},
        new string[]{"&Ocirc;",   "\xd4"},
        new string[]{"&ocirc;",   "\xf4"},
        new string[]{"&Otilde;",  "\xd5"},
        new string[]{"&otilde;",  "\xf5"},
        new string[]{"&Ouml;",    "\xd6"},
        new string[]{"&ouml;",    "\xf6"},
        new string[]{"&Oslash;",  "\xd8"},
        new string[]{"&oslash;",  "\xf8"},
        new string[]{"&Ugrave;",  "\xd9"},
        new string[]{"&ugrave;",  "\xf9"},
        new string[]{"&Uacute;",  "\xda"},
        new string[]{"&uacute;",  "\xfa"},
        new string[]{"&Ucirc;",   "\xdb"},
        new string[]{"&ucirc;",   "\xfb"},
        new string[]{"&Uuml;",    "\xdc"},
        new string[]{"&uuml;",    "\xfc"},
        new string[]{"&Yacute;",  "\xdd"},
        new string[]{"&yacute;",  "\xfd"},
        new string[]{"&THORN;",   "\xde"},
        new string[]{"&thorn;",   "\xfe"},
        new string[]{"&szlig;",   "\xdf"},
        new string[]{"&yuml;",    "\xff"},
        new string[]{"&iexcl;",   "\xa1"},
        new string[]{"&cent;",    "\xa2"},
        new string[]{"&pound;",   "\xa3"},
        new string[]{"&curren;",  "\xa4"},
        new string[]{"&yen;",     "\xa5"},
        new string[]{"&brvbar;",  "\xa6"},
        new string[]{"&sect;",    "\xa7"},
        new string[]{"&die;",     "\xa8"},
        new string[]{"&copy;",    "\xa9"},
        new string[]{"&laquo;",   "\xab"},
        new string[]{"&reg;",     "\xae"},
        new string[]{"&macron;",  "\xaf"},
        new string[]{"&deg;",     "\xb0"},
        new string[]{"&plusmn;",  "\xb1"},
        new string[]{"&sup2;",    "\xb2"},
        new string[]{"&sup3;",    "\xb3"},
        new string[]{"&acute;",   "\xb4"},
        new string[]{"&micro;",   "\xb5"},
        new string[]{"&para;",    "\xb6"},
        new string[]{"&middot;",  "\xb7"},
        new string[]{"&cedil;",   "\xb8"},
        new string[]{"&supl;",    "\xb9"},
        new string[]{"&raquo;",   "\xbb"},
        new string[]{"&frac14;",  "\xbc"},
        new string[]{"&frac12;",  "\xbd"},
        new string[]{"&frac34;",  "\xbe"},
        new string[]{"&iquest;",  "\xbf"},
        new string[]{"&times;",   "\xd7"},
        new string[]{"&divide;",  "\xf7"},

        new string[]{"&ldquo;",   "\x201c"},
        new string[]{"&rdquo;",   "\x201d"}
    };

    public static string XMLEscape(string strValue)
    {
		// 将 & 优先转换，避免后续重复叠加转换
		if (strValue.Contains("&"))
			strValue = strValue.Replace("&", "&amp;");

		if (strValue.Contains("<"))
			strValue = strValue.Replace("<", "&lt;");
		if (strValue.Contains(">"))
			strValue = strValue.Replace(">", "&gt;");
		if (strValue.Contains("'"))
			strValue = strValue.Replace("'", "&apos;");
		if (strValue.Contains(" "))
			strValue = strValue.Replace(" ", "&nbsp;");
		if (strValue.Contains("\""))
			strValue = strValue.Replace("\"", "&quot;");
		// 返回结果
		return strValue;
	}

    public static string XMLUnescape(string strValue)
    {
        // 记录日志
        //Log.LogMessage("XML", "XMLUnescape", "开始反转义！");

		// 创建词典
		Dictionary<string, string> escapes = new Dictionary<string, string>();
		// 匹配循环
		foreach (Match item in Regex.Matches(strValue, @"&#[0-9|o|O|l]{1,5};"))
        {
			// 记录日志
			//Log.LogMessage("XML", "XMLUnescape", item.Value);

			int value = 0;
			// 获得数字部分
			string strNumber =
				item.Value.Substring(2, item.Value.Length - 3);
            // 检查结果
            if(strNumber.IndexOfAny(errors) >= 0)
            {
                // 将l替换成1
                strNumber = strNumber.Replace("l", "1");
                // 将o替换成0
                strNumber = strNumber.Replace("o", "0");
                // 将O替换成0
                strNumber = strNumber.Replace("O", "0");
            }
            // 尝试解析
            value = System.Convert.ToInt32(strNumber);
			// 加入词典
			if (!escapes.ContainsKey(item.Value))
				escapes.Add(item.Value, new string((char)value, 1));
		}
        // 匹配循环
        foreach (Match item in Regex.Matches(strValue, @"&#[x|X]([0-9|a-f|A-F|o|O|l]{1,4});"))
        {
            // 记录日志
            //Log.LogMessage("XML", "XMLUnescape", item.Value);

            int value = 0;
            // 获得数字部分
            string strNumber =
                item.Value.Substring(3, item.Value.Length - 4);
            // 检查结果
            if (strNumber.IndexOfAny(errors) >= 0)
            {
                // 将l替换成1
                strNumber = strNumber.Replace("l", "1");
                // 将o替换成0
                strNumber = strNumber.Replace("o", "0");
                // 将O替换成0
                strNumber = strNumber.Replace("O", "0");
            }
            // 转换
            value = System.Convert.ToInt32(strNumber, 16);
            // 加入词典
            if (!escapes.ContainsKey(item.Value))
                escapes.Add(item.Value, new string((char)value, 1));
        }
        // 开始替换
        foreach (KeyValuePair<string, string> kvp in escapes)
        {
            // 执行替换操作
            strValue = strValue.Replace(kvp.Key, kvp.Value);
        }
        // 将字符串转义还原
        foreach (string[] item in ESCAPES)
        {
            // 执行替换操作
            if (strValue.Contains(item[0])) strValue = strValue.Replace(item[0], item[1]);
        }
        // 记录日志
        //Log.LogMessage("XML", "XMLUnescape", "反转义结束！");
        // 返回结果
        return strValue;
	}
}