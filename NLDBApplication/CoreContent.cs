using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class CoreContent
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ReloadCoreContent()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "ReloadCoreContent", "加载核心内容！");

        // 加载所有量词名称
        ReloadQuantifierName();

        // 加载所有单位名称
        ReloadUnitName();
        // 加载所有单位符号
        ReloadUnitSymbol();

        // 加载所有货币名称
        ReloadCurrencyName();
        // 加载所有货币符号
        ReloadCurrencySymbol();

        // 记录日志
        Log.LogMessage("CoreContent", "ReloadCoreContent", "核心内容已加载！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateCoreContent(SqlBoolean sqlInitialize)
    {
        // 记录日志
        Log.LogMessage("CoreContent", "CreateCoreContent", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除之前的索引
            "IF OBJECT_ID('CoreContentCIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.CoreContentCIDIndex; " +
            // 删除之前的索引
            "IF OBJECT_ID('CoreContentContentIndex') IS NOT NULL " +
            "DROP INDEX dbo.CoreContentContentIndex; " +
            // 删除之前的索引
            "IF OBJECT_ID('CoreContentUnicodeIndex') IS NOT NULL " +
            "DROP INDEX dbo.CoreContentUnicodeIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('CoreContent') IS NOT NULL " +
            "DROP TABLE dbo.CoreContent; " +
            // 创建数据表
            "CREATE TABLE dbo.CoreContent " +
            "( " +
            // 编号
            "[cid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 计数器
            "[count]                INT                     NOT NULL                    DEFAULT 0, " +
            // 内容
            "[content]              NVARCHAR(64)            NOT NULL, " +
            // Unicode编码值
            "[unicode]              INT                     NOT NULL                    DEFAULT 0, " +
            // 分类描述
            "[classification]       NVARCHAR(32)            NULL, " +
            // 属性
            "[attribute]            NVARCHAR(32), " +
            // 备注
            "[remark]               NVARCHAR(32)            NULL, " +
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX CoreContentCIDIndex ON dbo.CoreContent([cid]); " +
            "CREATE INDEX CoreContentContentIndex ON dbo.CoreContent([content]); " +
            "CREATE INDEX CoreContentUnicodeIndex ON dbo.CoreContent([unicode]); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("CoreContent", "CreateCoreContent", "数据表已创建！");

        // 初始化数据表
        if(sqlInitialize) InitializeCoreContent();
    }

    public static void InitializeCoreContent()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "InitializeCoreContent", "初始化数据表！");

        // 加入所有量词名称
        AddAllQuantifierNames();

        // 加入所有单位名称
        AddAllUnitNames();
        // 加入所有单位符号
        AddAllUnitSymbols();

        // 加入所有货币名称
        AddAllCurrencyNames();
        // 加入所有货币符号
        AddAllCurrencySymbols();

        // 加入所有姓氏
        AddAllNames();
        // 加入所有动词
        AddAllVerbs();
        // 加入所有功能词
        AddAllFunctions();

        // 记录日志
        Log.LogMessage("CoreContent", "InitializeCoreContent", "数据表已经初始化！");
    }

    public static string ReloadStringValue(string classification, string attribute)
    {
        // 计数器
        int count = 0;
        // 清理正则串
        string strValue = "";

        // 指令字符串
        string cmdString =
            "SELECT content FROM " +
            "( " +
            "SELECT DISTINCT([content]) AS content " +
            "FROM [dbo].[CoreContent] " +
            "WHERE [classification] = @SqlClassification AND [attribute] = @SqlAttribute " +
            ") AS T " +
            "ORDER BY LEN(content) DESC;";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 增加参数
        parameters.Add("SqlAttribute", attribute);
        parameters.Add("SqlClassification", classification);

        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 遍历参数
            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                sqlCommand.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }
            // 创建数据阅读器
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // 循环处理
            while (reader.Read())
            {
                // 修改计数器
                count++;
                // 获得参数
                strValue += (string)reader[0] + "|";
            }
            // 关闭数据阅读器
            reader.Close();
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // 检查状态并关闭连接
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }
        // 修正结果
        if (count > 0 && strValue.Length > 0)
            strValue = strValue.Substring(0, strValue.Length - 1);
        // 返回结果
        return strValue;
    }
}
