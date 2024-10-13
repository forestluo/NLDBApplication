using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class TextContent
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateTextContent()
    {
        // 记录日志
        Log.LogMessage("TextContent", "CreateTextContent", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除索引
            "IF OBJECT_ID('TextContentTIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.TextContentTIDIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('TextContent') IS NOT NULL " +
            "DROP TABLE dbo.TextContent; " +
            // 创建数据表
            "CREATE TABLE dbo.TextContent " +
            "( " +
            // 编号
            "[tid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 内容长度
            "[length]               INT                     NOT NULL                    DEFAULT 0, " +
            // 分类描述
            "[classification]       NVARCHAR(64)            NULL, " +
            // 内容
            "[content]              NVARCHAR(MAX)           NOT NULL, " +
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX TextContentTIDIndex ON dbo.TextContent (tid); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("TextContent", "CreateTextContent", "数据表已创建！");
    }
}
