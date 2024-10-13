using System.Text;

public partial class Convert
{
    public static string LatinConvert(string strValue)
    {
        // 全角转半角
        //return Strings.StrConv(strValue.Value, VbStrConv.Narrow, 0);

        // 创建字符串
        StringBuilder sb = new StringBuilder(strValue.Length);
        // 循环处理
        foreach (char cValue in strValue)
        {
            // 特殊处理
            if (cValue == 12288) sb.Append(' ');
            // 检查字符范围
            else if (cValue < 65281) sb.Append(cValue);
            else if (cValue > 65374) sb.Append(cValue);
            // 转换成半角
            else sb.Append((char)(cValue - 65248));
        }
        // 返回结果
        return sb.ToString();
    }

    public static string UnicodeConvert(string strValue)
    {
        // 全角转半角
        //return Strings.StrConv(strValue.Value, VbStrConv.Wide, 0);

        // 创建字符串
        StringBuilder sb = new StringBuilder(strValue.Length);
        // 循环处理
        foreach (char cValue in strValue)
        {
            // 特殊处理
            if (cValue == 32) sb.Append((char)12288);
            // 检查字符范围
            else if (cValue < 33) sb.Append(cValue);
            else if (cValue > 126) sb.Append(cValue);
            // 转换成全角
            else sb.Append((char)(cValue + 65248));
        }
        // 返回结果
        return sb.ToString();
    }
}
