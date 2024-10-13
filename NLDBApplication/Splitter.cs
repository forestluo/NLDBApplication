using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

public partial class Splitter
{
    // 次要分割符
    private readonly static
        char[] MINOR_SPLITTERS = { '、', '—', '…', '·' };
    // 主要分割符
    private readonly static
        char[] MAJOR_SPLITTERS = {'。', '，', '；', '：', '?', '！' };

    public static bool IsMinorSplitter(char cValue)
    {
        foreach (char item in MINOR_SPLITTERS)
        {
            if (cValue == item) return true;
        }
        // 返回结果
        return false;
    }

    public static bool IsMajorSplitter(char cValue)
    {
        foreach (char item in MAJOR_SPLITTERS)
        {
            if (cValue == item) return true;
        }
        // 返回结果
        return false;
    }

    public static bool HasMajorSplitter(string strValue)
    {
        for(int i = 0;i < strValue.Length;i ++)
        {
            if (IsMajorSplitter(strValue[i])) return true;
        }
        // 返回结果
        return false;
    }
}
