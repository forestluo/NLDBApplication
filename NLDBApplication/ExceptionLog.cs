using System.Data.SqlTypes;
using System.Collections.Generic;

public partial class ExceptionLog
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CatchExceptionLog(SqlString sqlTips)
    {
        // 记录日志
        Log.LogMessage("ExceptionLog", "CatchExceptionLog", "捕捉异常！");

        // 指令字符串
        string cmdString =
            "INSERT INTO dbo.ExceptionLog " +
            "([number], [serverity], [state], [prodcedure], [line], [message]) " +
            "VALUES " +
            "(ERROR_NUMBER(), ERROR_SEVERITY(), ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ISNULL(@SqlTips, '') + '> ' + ERROR_MESSAGE()); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 增加参数
        parameters.Add("SqlTips", sqlTips.Value);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);

        // 记录日志
        Log.LogMessage("ExceptionLog", "CatchExceptionLog", "异常已记录！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateExceptionLog()
    {
        // 记录日志
        Log.LogMessage("ExceptionLog", "CreateExceptionLog", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除索引
            "IF OBJECT_ID('ExceptionLogEIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.ExceptionLogEIDIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('ExceptionLog') IS NOT NULL " +
            "DROP TABLE dbo.ExceptionLog; " +
            // 创建数据表
            "CREATE TABLE dbo.ExceptionLog " +
            "( " +
            // 编号
            "[eid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 时间
            "[time]                 DATETIME                NOT NULL                    DEFAULT GETDATE(), " +
            // 错误号
            "[number]               INT                     NOT NULL                    DEFAULT 0, " +
            // 严重性
            "[serverity]            INT                     NOT NULL                    DEFAULT 0, " +
            // 错误状态号
            "[state]                INT                     NOT NULL                    DEFAULT 0, " +
            // 出现错误的存储过程或触发器的名称
            "[prodcedure]           NVARCHAR(256), " +
            // 导致错误的例程中的行号
            "[line]                 INT                     NOT NULL                    DEFAULT 0, " +
            // 错误消息的完整文本
            "[message]              NVARCHAR(MAX), " +
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX ExceptionLogEIDIndex ON dbo.ExceptionLog (eid); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("ExceptionLog", "CreateExceptionLog", "数据表已创建！");
    }
}
