using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class CoreContent
{
    // 符号正则串
    public static string CURRENCY_SYMBOL = "";

    public static void ReloadCurrencySymbol()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "ReloadCurrencySymbol", "加载货币符号！");
        // 设置正则串
        CURRENCY_SYMBOL = ReloadStringValue("符号", "货币符号");
        // 记录日志
        Log.LogMessage(CURRENCY_SYMBOL);
    }

    public static void AddCurrencySymbol(string symbol)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('符号', @SqlContent, '货币符号'); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlContent", symbol);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllCurrencySymbols()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "AddAllCurrencySymbols", "加载货币符号！");

        AddCurrencySymbol("￥");

        // 记录日志
        Log.LogMessage("CoreContent", "AddAllCurrencySymbols", "货币符号已加载！");
    }
}