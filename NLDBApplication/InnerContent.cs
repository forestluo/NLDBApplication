using System.Collections.Generic;

public partial class InnerContent
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateInnerContent()
    {
        // 记录日志
        Log.LogMessage("InnerContent", "CreateInnerContent", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除索引
            "IF OBJECT_ID('InnerContentIIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.InnerContentIIDIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('InnerContent') IS NOT NULL " +
            "DROP TABLE dbo.InnerContent; " +
            // 创建数据表
            "CREATE TABLE dbo.InnerContent " +
            "( " +
            // 编号
            "[iid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
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
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX InnerContentIIDIndex ON dbo.InnerContent (iid); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("InnerContent", "CreateInnerContent", "数据表已创建！");
    }

    public static void AddInnerContent(string classification, string content, string attribute)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[InnerContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES (@SqlClassification, @SqlContent, @SqlAttribute); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlClassification", classification);
        parameters.Add("SqlContent", content);
        parameters.Add("SqlAttribute", attribute);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }
}