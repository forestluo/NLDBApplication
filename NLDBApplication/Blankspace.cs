using System.Data.SqlTypes;
using System.Text.RegularExpressions;

public partial class Blankspace
{
    // Unicode空白字符
    //private readonly static
    //    char[] BLANKSPACE_CHARS =
    //    {
    //        // Unicode characters with White_Space property
    //        (char)0x0009, (char)0x000A, (char)0x000B, (char)0x000C, (char)0x000D,
    //        (char)0x0020, (char)0x0085, (char)0x00A0, (char)0x1680, (char)0x2000,
    //        (char)0x2001, (char)0x2002, (char)0x2003, (char)0x2004, (char)0x2005,
    //        (char)0x2006, (char)0x2007, (char)0x2008, (char)0x2009, (char)0x200A,
    //        (char)0x2028, (char)0x2029, (char)0x202F, (char)0x205F, (char)0x3000,
    //        // Related Unicode characters without White_Space property
    //        (char)0x180E, (char)0x200B, (char)0x200C, (char)0x200D, (char)0x2060, (char)0xFEFF
    //    };

    public static bool IsInvisible(char cValue)
    {
        // Unicode不可见区域
        switch ((int)cValue)
        {
            case 0x1680:
            case 0x180E:
            case 0x2028:
            case 0x2029:
            case 0x202F:
            case 0x205F:
            case 0x2060:
            case 0x3000:
            case 0xFEFF:
                return true;
        }
        // Unicode不可见区域
        if (cValue >= 0xD7B0 &&
            cValue <= 0xF8FF) return true;
        // Unicode不可见区域
        if (cValue >= 0xFFF0 &&
            cValue <= 0xFFFF) return true;
        // Unicode不可见区域
        if (cValue >= 0x2000 &&
            cValue <= 0x200D) return true;
        // 返回结果
        return cValue < 32 || cValue == 0x7F;
    }

    public static bool IsBlankspace(char cValue)
    {
        // 返回结果
        return cValue == 0x20 ? true : IsInvisible(cValue);
    }

    public static string ClearBlankspace(string strValue)
    {
        // 返回结果
        return Regex.Replace(strValue, @"\s", "");
    }

    public static string ClearInvisible(string strValue)
    {
        // 将不可见字符替换成空格
        return Regex.Replace(strValue, @"([\x00-\x1F]|\x7F|\u1680|\u180E|[\u2000-\u200D]|[\u2028-\u2029]|\u202F|[\u205F-\u2060]|\u3000|[\uD7B0-\uF8FF]|\uFEFF|[\uFFF0-\uFFFF])+", " ");
    }
}