using Microsoft.VisualBasic;

public partial class Chinese
{
    public static bool IsChinese(char cValue)
    {
        int value = cValue & 0xFFFF;
        // 返回结果
        return value >= 0x4E00 && value <= 0x9FA5;
    }

    public static bool IsChinese(string strValue)
    {
        for (int i = 0; i < strValue.Length; i++)
        {
            if (!IsChinese(strValue[i])) return false;
        }
        // 返回结果
        return true;
    }

    public static string TraditionalConvert(string strValue)
    {
        // 转繁体
        return Strings.StrConv(strValue, VbStrConv.TraditionalChinese, 0);
    }

    public static string SimplifiedConvert(string strValue)
    {
        // 转简体
        return Strings.StrConv(strValue, VbStrConv.SimplifiedChinese, 0);
    }
}
