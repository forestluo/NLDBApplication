using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class OuterContent
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateOuterContent()
    {
        // 记录日志
        Log.LogMessage("OuterContent", "CreateOuterContent", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除索引
            "IF OBJECT_ID('OuterContentOIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.OuterContentOIDIndex; " +
            // 删除索引
            "IF OBJECT_ID('OuterContentLContentIndex') IS NOT NULL " +
            "DROP INDEX dbo.OuterContentLContentIndex; " +
            // 删除索引
            "IF OBJECT_ID('OuterContentRContentIndex') IS NOT NULL " +
            "DROP INDEX dbo.OuterContentRContentIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('OuterContent') IS NOT NULL " +
            "DROP TABLE dbo.OuterContent; " +
            // 创建数据表
            "CREATE TABLE dbo.OuterContent " +
            "( " +
            // 编号
            "[oid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 使能标记
            "[enable]               INT                     NOT NULL                    DEFAULT 0, " +
            // 无效标记
            "[invalid]              BIT                     NOT NULL                    DEFAULT 0, " +
            // 字典标记
            "[dictionary]           BIT                     NOT NULL                    DEFAULT 0, " +
            // 计数器
            "[count]                INT                     NOT NULL                    DEFAULT 0, " +
            // 相关系数
            "[gamma]                FLOAT                   NOT NULL                    DEFAULT -1, " +
            // 内容
            "[content]              NVARCHAR(64)            PRIMARY KEY                 NOT NULL, " +
            // 分类描述
            "[classification]       NVARCHAR(32)            NULL, " +
            // 属性描述
            "[attribute]            NVARCHAR(32)            NULL, " +
            // 左侧内容
            "[left_content]         NVARCHAR(32)            NULL, " +
            // 右侧内容
            "[right_content]        NVARCHAR(32)            NULL, " +
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX OuterContentOIDIndex ON dbo.OuterContent (oid); " +
            "CREATE INDEX OuterContentLContentIndex ON dbo.OuterContent (left_content); " +
            "CREATE INDEX OuterContentRContentIndex ON dbo.OuterContent (right_content); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("OuterContent", "CreateOuterContent", "数据表已创建！");
    }
}
