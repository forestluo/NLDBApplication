using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class Log
{
    // 是否记录
    private static bool DEBUG = true;
    // 最大日志两
    private readonly static int MAX_LOGS = 1024;

    // 日志记录
    private static List<string[]> logs = new List<string[]>();

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SetLog(SqlBoolean sqlLog)
    {
        // 设置数值
        DEBUG = sqlLog.Value;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void GetLogs()
    {
        // 计数器
        int count = 0;
        // 检查数量
        foreach(string[] item in logs)
        {
            // 修改计数器
            count ++;
            // 检查参数
            if (item.Length == 1)
            {
                // 发送消息
                SqlContext.Pipe.Send(item[0]);
            }
            else if (item.Length == 2)
            {
                // 发送消息
                SqlContext.Pipe.Send(item[0] + " > " + item[1]);
            }
            else if (item.Length == 3)
            {
                // 发送消息
                SqlContext.Pipe.Send(item[0] + " " + item[1] + " > " + item[2]);
            }
            else if (item.Length == 4)
            {
                // 发送消息
                SqlContext.Pipe.Send(item[0] + " " + item[1] + "." + item[2] + " > " + item[3]);
            }
        }
        // 删除内容
        if(count > 0) logs.RemoveRange(0, count);
    }

    public static void LogMessage(string strMessage)
    {
        // 检查参数
        if (DEBUG)
        {
            // 检查日志记录
            if (logs.Count >= MAX_LOGS) logs.RemoveAt(0);
            // 加入日志记录
            logs.Add(new string[] { DateTime.Now.ToString("HH:mm:ss"), strMessage });
        }
    }

    public static void LogMessage(string strModule, string strFunction, string strMessage)
    {
        // 检查参数
        if(DEBUG)
        {
            // 检查日志记录
            if (logs.Count >= MAX_LOGS) logs.RemoveAt(0);
            // 加入日志记录
            logs.Add(new string[] { DateTime.Now.ToString("HH:mm:ss"), strModule, strFunction, strMessage });
        }
    }
}