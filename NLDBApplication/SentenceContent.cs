using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class SentenceContent
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateSentenceContent()
    {
        // 记录日志
        Log.LogMessage("SentenceContent", "CreateSentenceContent", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除索引
            "IF OBJECT_ID('SentenceContentSIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.SentenceContentSIDIndex; " +
            // 删除索引
            "IF OBJECT_ID('SentenceContentTIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.SentenceContentTIDIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('SentenceContent') IS NOT NULL " +
            "DROP TABLE dbo.SentenceContent; " +
            // 创建数据表
            "CREATE TABLE dbo.SentenceContent " +
            "( " +
            // 编号
            "[sid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 编号
            "[tid]                  INT                     NOT NULL                    DEFAULT 0, " +
            // 内容长度
            "[length]               INT                     NOT NULL                    DEFAULT 0, " +
            // 分类描述
            "[classification]       NVARCHAR(64)            NULL, " +
            // 内容
            "[content]              NVARCHAR(450)           PRIMARY KEY                 NOT NULL, " +
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX SentenceContentSIDIndex ON dbo.SentenceContent (sid); " +
            "CREATE INDEX SentenceContentTIDIndex ON dbo.SentenceContent (tid); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("SentenceContent", "CreateSentenceContent", "数据表已创建！");
    }
}
