using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class Grammar
{
    // 创建数据字典
    private static Dictionary<char, int> chars = new Dictionary<char, int>();

    public static int GetCharCount(char cValue)
    {
        // 返回结果
        return chars.ContainsKey(cValue) ?
            chars[cValue] : LoadCharCount(cValue);
    }

    public static int LoadCharCount(char cValue)
    {
        // 记录日志
        Log.LogMessage("Grammar", "LoadCharCount", "加载数据记录！");

        // 计数器
        int count = -1;
        // 指令字符串
        string cmdString =
            "SELECT content,count FROM [dbo].[CoreContent] " +
            "WHERE classification = '符号' AND [unicode] = UNICODE(@SqlChar);";

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
            sqlCommand.Parameters.AddWithValue("SqlChar", cValue);
            // 创建数据阅读器
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // 循环处理
            while (reader.Read())
            {
                // 检查记录
                if (reader.IsDBNull(0)) continue;
                // 获得计数
                count = reader.GetInt32(1);
                // 检查结果
                if (count < 0) count = 0;
                // 增加一个新键值
                chars.Add(cValue, count);
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
        Log.LogMessage("Grammar", "LoadCharCount", "数据记录已加载！");
        // 返回结果
        return count;
    }

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlBoolean AddUpCharCount(SqlString sqlContent, SqlBoolean sqlCacheOnly)
    {
        // 记录日志
        Log.LogMessage("Grammar", "AddUpCharCount", "开始统计！");
        // 循环处理
        foreach (char cValue in sqlContent.Value)
        {
            // 检查结果
            if (!chars.ContainsKey(cValue)) 
            {
                // 检查参数
                if(sqlCacheOnly)
                {
                    // 增加一条记录
                    chars.Add(cValue, 0);
                }
                else
                {
                    // 尝试从数据库中加载
                    if (LoadCharCount(cValue) < 0) chars.Add(cValue, 0);
                }
            }
            // 增加计数
            chars[cValue] ++;
            // 记录日志
            //Log.LogMessage(string.Format("\tchar({0}).count = {1}", cValue, chars[cValue]));
        }
        // 记录日志
        Log.LogMessage("Grammar", "AddUpCharCount", "统计已完成！");
        // 返回结果
        return true;
    }

    public static void UpdateCharCount()
    {
        // 记录日志
        Log.LogMessage("Grammar", "UpdateCharCount", "更新数据！");
        Log.LogMessage(string.Format("\tchars.count = {0}", chars.Count));

        // 生成批量处理语句
        string cmdString =
            "UPDATE [dbo].[CoreContent] " +
                "SET [count] = @SqlCount " +
                "WHERE [unicode] = UNICODE(@SqlChar) AND " +
                "classification = @SqlClassification AND attribute = @SqlAttribute; " +
            "IF @@ROWCOUNT <= 0 " +
                "INSERT INTO [dbo].[CoreContent] " +
                "([content], [unicode], [count], [classification], [attribute]) " +
                "VALUES (@SqlChar, UNICODE(@SqlChar), @SqlCount, @SqlClassification, @SqlAttribute); ";

        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 记录日志
            Log.LogMessage("Grammar", "UpdateCharCount", "数据连接已开启！");

            // 开启事务处理模式
            SqlTransaction sqlTransaction =
                sqlConnection.BeginTransaction();
            // 记录日志
            Log.LogMessage("Grammar", "UpdateCharCount", "事务处理已开启！");

            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 设置事物处理模式
            sqlCommand.Transaction = sqlTransaction;
            // 记录日志
            Log.LogMessage("Grammar", "UpdateCharCount", "T-SQL指令已创建！");

            // 遍历参数
            foreach (KeyValuePair<char, int> kvp in chars)
            {
                // 获得分类属性
                string[] result = GetClassification(kvp.Key);
                // 检查结果
                if (result == null || result.Length != 2) continue;
                // 清理参数
                sqlCommand.Parameters.Clear();
                // 设置参数
                sqlCommand.Parameters.AddWithValue("SqlChar", kvp.Key);
                sqlCommand.Parameters.AddWithValue("SqlCount", kvp.Value);
                // 设置分类属性
                sqlCommand.Parameters.AddWithValue("SqlAttribute", result[1]);
                sqlCommand.Parameters.AddWithValue("SqlClassification", result[0]);
                // 执行指令（尚未执行）
                sqlCommand.ExecuteNonQuery();
            }
            // 记录日志
            Log.LogMessage("Grammar", "UpdateCharCount", "批量指令已添加！");

            // 提交事务处理
            sqlTransaction.Commit();
            // 记录日志
            Log.LogMessage("Grammar", "UpdateCharCount", "批量指令已提交！");
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // 检查状态并关闭连接
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // 记录日志
        Log.LogMessage("Grammar", "UpdateCharCount", "数据记录已更新！");
    }

    public static void ReloadCharCount()
    {
        // 记录日志
        Log.LogMessage("Grammar", "ReloadCharCount", "清理数据记录！");

        // 清理字符
        chars.Clear();

        // 记录日志
        Log.LogMessage("Grammar", "ReloadCharCount", "加载数据记录！");

        // 指令字符串
        string cmdString =
            "SELECT content,count FROM [dbo].[CoreContent] " +
            "WHERE classification = '符号' AND attribute NOT IN ('单位','货币') AND LEN(content) = 1;";

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
                // 检查记录
                if (reader.IsDBNull(0)) continue;
                // 获得计数
                int count = reader.GetInt32(1);
                // 检查结果
                if (count < 0) count = 0;
                // 获得字符
                char cValue = reader.GetString(0)[0];
                // 检查数据集
                if (!chars.ContainsKey(cValue)) chars.Add(cValue, count);
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
        Log.LogMessage("Grammar", "ReloadCharCount", "数据记录已加载！");
        // 记录日志
        Log.LogMessage(string.Format("\tchars.count = {0}", chars.Count));
    }
}
