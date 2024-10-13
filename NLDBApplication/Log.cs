using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class Log
{
    // �Ƿ��¼
    private static bool DEBUG = true;
    // �����־��
    private readonly static int MAX_LOGS = 1024;

    // ��־��¼
    private static List<string[]> logs = new List<string[]>();

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SetLog(SqlBoolean sqlLog)
    {
        // ������ֵ
        DEBUG = sqlLog.Value;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void GetLogs()
    {
        // ������
        int count = 0;
        // �������
        foreach(string[] item in logs)
        {
            // �޸ļ�����
            count ++;
            // ������
            if (item.Length == 1)
            {
                // ������Ϣ
                SqlContext.Pipe.Send(item[0]);
            }
            else if (item.Length == 2)
            {
                // ������Ϣ
                SqlContext.Pipe.Send(item[0] + " > " + item[1]);
            }
            else if (item.Length == 3)
            {
                // ������Ϣ
                SqlContext.Pipe.Send(item[0] + " " + item[1] + " > " + item[2]);
            }
            else if (item.Length == 4)
            {
                // ������Ϣ
                SqlContext.Pipe.Send(item[0] + " " + item[1] + "." + item[2] + " > " + item[3]);
            }
        }
        // ɾ������
        if(count > 0) logs.RemoveRange(0, count);
    }

    public static void LogMessage(string strMessage)
    {
        // ������
        if (DEBUG)
        {
            // �����־��¼
            if (logs.Count >= MAX_LOGS) logs.RemoveAt(0);
            // ������־��¼
            logs.Add(new string[] { DateTime.Now.ToString("HH:mm:ss"), strMessage });
        }
    }

    public static void LogMessage(string strModule, string strFunction, string strMessage)
    {
        // ������
        if(DEBUG)
        {
            // �����־��¼
            if (logs.Count >= MAX_LOGS) logs.RemoveAt(0);
            // ������־��¼
            logs.Add(new string[] { DateTime.Now.ToString("HH:mm:ss"), strModule, strFunction, strMessage });
        }
    }
}