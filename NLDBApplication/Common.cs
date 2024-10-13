using System.Data.SqlTypes;

public partial class Common
{
    public static bool IsDigit(char cValue)
    {
        // 返回结果
        return cValue >= '0' && cValue <= '9';
    }

    public static bool IsTooLong(string strValue)
    {
        // 返回结果
        return strValue is null ? false : strValue.Length > 450;
    }

    public static int GetCharCount(string strValue, char cValue)
    {
        int count = 0;
        // 循环处理
        for (int i = 0; i < strValue.Length; i++)
        {
            if (strValue[i] == cValue) count++;
        }
        // 返回结果
        return count;
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString XMLEscape(SqlString strValue)
    {
        // 返回结果
        return XML.XMLEscape(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString XMLUnescape(SqlString strValue)
    {
        // 返回结果
        return XML.XMLUnescape(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString ClearInvisible(SqlString strValue)
    {
        // 返回结果
        return Blankspace.ClearInvisible(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString ClearBlankspace(SqlString strValue)
    {
        // 返回结果
        return Blankspace.ClearBlankspace(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString LatinConvert(SqlString strValue)
    {
        // 转半角
        return Convert.LatinConvert(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString UnicodeConvert(SqlString strValue)
    {
        // 转全角
        return Convert.UnicodeConvert(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString TraditionalConvert(SqlString strValue)
    {
        // 转繁体
        return Chinese.TraditionalConvert(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString SimplifiedConvert(SqlString strValue)
    {
        // 转简体
        return Chinese.SimplifiedConvert(strValue.Value);
    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString FilterContent(SqlString strValue)
    {
        string strResult;

        // 清理不可见字符
        strResult = Blankspace.ClearInvisible(strValue.Value);
        // 转小写
        strResult = Convert.LatinConvert(strResult);

        // 循环处理
        int nLength;
        do
        {
            // 获得字符串长度
            nLength = strResult.Length;
            // 转XML
            strResult = XML.XMLUnescape(strResult);

        } while (strResult.Length < nLength);

        // 清理不可见字符
        strResult = Blankspace.ClearInvisible(strResult);
        // 过滤内容
        strResult = FilterRule.FilterContent(strResult);
        // 返回结果
        return strResult.Trim();
    }
}
