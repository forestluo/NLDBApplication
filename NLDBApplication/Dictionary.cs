using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

public partial class Dictionary
{
    // 最大长度
    private readonly static int MAX_LENGTH = 20;
    // 创建数据字典
    private static Dictionary<string, int> items = new Dictionary<string, int>();

    public static int GetDictionaryCount(string strValue)
    {
        // 返回结果
        return items.ContainsKey(strValue) ?
            items[strValue] : LoadDictionaryCount(strValue);
    }

    public static int LoadDictionaryCount(string strValue)
    {
        // 检查参数
        if (strValue == null || strValue.Length != 2) return 0;
        // 记录日志
        //Log.LogMessage("Dictionary", "LoadDictionaryCount", "加载数据记录！");

        // 计数器
        int count = 0;
        // 指令字符串
        string cmdString =
            "SELECT TOP 1 content, count " +
            "FROM [dbo].[Dictionary] WHERE content = @SqlContent;";

        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 设置参数
            sqlCommand.Parameters.AddWithValue("SqlContent", strValue);
            // 创建数据阅读器
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // 循环处理
            while (reader.Read())
            {
                // 增加记录
                items.Add(strValue, reader.GetInt32(1));
                //Log.LogMessage("\tdictionary.value = " + strValue);
                //Log.LogMessage("\tdictionary.value.count = " + reader.GetInt32(1));
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

        // 记录日志
        Log.LogMessage("Dictionary", "LoadDictionaryCount", "数据记录已加载！");
        // 返回结果
        return count;
    }

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlBoolean AddUpDictionaryCount(SqlString sqlContent, SqlBoolean sqlCacheOnly)
    {
        // 记录日志
        Log.LogMessage("Dictionary", "AddUpDictionaryCount", "开始统计！");

        // 记录日志
        Log.LogMessage("\tcontent = " + sqlContent.Value);
        // 循环处理
        foreach (Match item in
            Regex.Matches(sqlContent.Value, @"[\u4E00-\u9FA5]+"))
        {
            // 记录日志
            Log.LogMessage("\tcontent.segment = " + item.Value);
            // 执行循环
            for (int j = 1;j <= MAX_LENGTH;j ++)
            {
                // 检查长度
                for (int i = 0; i <= item.Value.Length - j; i ++)
                {
                    // 获得子字符串（长度2）
                    string strValue = item.Value.Substring(i, j);
                    // 检查结果
                    if (strValue == null || strValue.Length != j) continue;
                    // 检查结果
                    if (!items.ContainsKey(strValue))
                    {
                        // 检查参数
                        if (sqlCacheOnly) continue;
                        else
                        {
                            // 尝试从数据库中加载
                            if (LoadDictionaryCount(strValue) <= 0) continue;
                        }
                    }
                    // 修改计数器
                    items[strValue] ++;
                    // 记录日志
                    //Log.LogMessage("\titems.value = " + strValue);
                    //Log.LogMessage("\titems.count = " + items[strValue]);
                }
            }
        }
        // 记录日志
        Log.LogMessage("Dictionary", "AddUpDictionaryCount", "统计已完成！");
        // 返回结果
        return true;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateDictionary ()
    {
        // 记录日志
        Log.LogMessage("Dictionary", "CreateDictionary", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除索引
            "IF OBJECT_ID('DictionaryDIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.DictionaryDIDIndex; " +
            // 删除索引
            "IF OBJECT_ID('DictionaryContentIndex') IS NOT NULL " +
            "DROP INDEX dbo.DictionaryContentIndex; " +
            // 删除索引
            "IF OBJECT_ID('Dictionary') IS NOT NULL " +
            "DROP TABLE dbo.Dictionary; " +
            // 创建字典表
            "CREATE TABLE dbo.Dictionary " +
            "( " +
            // 编号
            "[did]                  INT                     IDENTITY(1, 1)               NOT NULL, " +
            // 使能
            "[enable]               INT                     NOT NULL                     DEFAULT 0, " +
            // 计数器
            "[count]                INT                     NOT NULL                     DEFAULT 1, " +
            // 内容长度
            "[length]               INT                     NOT NULL                     DEFAULT 0, " +
            // 内容描述
            "[content]              NVARCHAR(450)           NOT NULL, " +
            // 分类描述
            "[classification]       NVARCHAR(64)            NULL, " +
            // 备注
            "[remark]               NVARCHAR(MAX)           NULL, " +
            // 操作标志
            "[operation]            INT                     NOT NULL                    DEFAULT 0, " +
            // 结果状态
            "[operation_result]     INT                     NOT NULL                    DEFAULT 0 " +
            "); " +
            // 创建简单索引
            "CREATE INDEX DictionaryDIDIndex ON dbo.Dictionary(did); " +
            "CREATE INDEX DictionaryContentIndex ON dbo.Dictionary(content); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("Dictionary", "CreateDictionary", "数据表已创建！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void UpdateDictionaryCount()
    {
        // 记录日志
        Log.LogMessage("Dictionary", "UpdateDictionaryCount", "更新数据！");
        Log.LogMessage(string.Format("\titems.count = {0}", items.Count));

        // 生成批量处理语句
        string cmdString =
            "UPDATE [dbo].[Dictionary] " +
                "SET [count] = @SqlCount WHERE content = @SqlContent; ";

        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 记录日志
            Log.LogMessage("Dictionary", "UpdateDictionaryCount", "数据连接已开启！");

            // 开启事务处理模式
            SqlTransaction sqlTransaction =
                sqlConnection.BeginTransaction();
            // 记录日志
            Log.LogMessage("Dictionary", "UpdateDictionaryCount", "事务处理已开启！");

            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 设置事物处理模式
            sqlCommand.Transaction = sqlTransaction;
            // 记录日志
            Log.LogMessage("Dictionary", "UpdateDictionaryCount", "T-SQL指令已创建！");

            // 遍历参数
            foreach (KeyValuePair<string, int> kvp in items)
            {
                // 清理参数
                sqlCommand.Parameters.Clear();
                // 设置参数
                sqlCommand.Parameters.AddWithValue("SqlContent", kvp.Key);
                sqlCommand.Parameters.AddWithValue("SqlCount", kvp.Value);
                // 执行指令（尚未执行）
                sqlCommand.ExecuteNonQuery();
            }
            // 记录日志
            Log.LogMessage("Dictionary", "UpdateDictionaryCount", "批量指令已添加！");

            // 提交事务处理
            sqlTransaction.Commit();
            // 记录日志
            Log.LogMessage("Dictionary", "UpdateDictionaryCount", "批量指令已提交！");
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // 检查状态并关闭连接
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // 记录日志
        Log.LogMessage("Dictionary", "UpdateDictionaryCount", "数据记录已更新！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ReloadDictionaryCount(SqlBoolean sqlEnableOnly)
    {
        // 记录日志
        Log.LogMessage("Dictionary", "ReloadDictionaryCount", "清理数据记录！");

        // 清理字符
        items.Clear();

        // 记录日志
        Log.LogMessage("Dictionary", "ReloadDictionaryCount", "加载数据记录！");

        // 指令字符串
        string cmdString =
            !sqlEnableOnly ? "SELECT DISTINCT content, count FROM [dbo].[Dictionary]; "
            : "SELECT DISTINCT content, count FROM [dbo].[Dictionary] WHERE enable >= 0; ";

        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 创建数据阅读器
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // 循环处理
            while (reader.Read())
            {
                // 获得内容
                string strValue = reader.GetString(0);
                // 检查结果
                if (strValue == null || strValue.Length <= 0) continue;
                // 检查索引
                if(!items.ContainsKey(strValue))
                {
                    // 增加记录
                    items.Add(strValue, reader.GetInt32(1));
                }
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

        // 记录日志
        Log.LogMessage("\titems.count = " + items.Count);
        // 记录日志
        Log.LogMessage("Dictionary", "ReloadDictionaryCount", "数据记录已加载！");
    }
}
