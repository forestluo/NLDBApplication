using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Grammar
{
    // 创建数据字典
    private static Dictionary<string, GDescription> words = new Dictionary<string, GDescription>();

    public static int GetWordCount(string strValue)
    {
        // 返回结果
        return words.ContainsKey(strValue) ?
            words[strValue].count : LoadWordCount(strValue);
    }

    public static int LoadWordCount(string strValue)
    {
        // 记录日志
        Log.LogMessage("Grammar", "LoadWordCount", "加载数据记录！");

        // 计数器
        int count = -1;
        // 指令字符串
        string cmdString =
            "SELECT content, count, gamma " +
            "FROM [dbo].[InnerContent] " +
            "WHERE LEN(content) = 2 AND content = @SqlWord;";

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
            sqlCommand.Parameters.AddWithValue("SqlWord", strValue);
            // 创建数据阅读器
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // 循环处理
            while (reader.Read())
            {
                // 创建描述
                GDescription description = new GDescription();
                // 设置参数
                description.operation = false;
                // 设置参数
                count = reader.GetInt32(1);
                // 检查结果
                if (count < 0) count = 0;
                // 设置参数
                description.count = count;
                description.gamma = reader.GetDouble(2);
                // 设置左侧
                description.left = strValue.Substring(0, 1);
                // 设置右侧
                description.right = strValue.Substring(1, 1);
                // 增加记录
                words.Add(strValue, description);
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
        Log.LogMessage("Grammar", "LoadWordCount", "数据记录已加载！");
        // 返回结果
        return count;
    }

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlBoolean AddUpWordCount(SqlString sqlContent, SqlBoolean sqlCacheOnly)
    {
        // 记录日志
        Log.LogMessage("Grammar", "AddUpWordCount", "开始统计！");
        // 循环处理
        foreach (Match item in
            Regex.Matches(sqlContent.Value, @"[\u4E00-\u9FA5]+"))
        {
            // 检查长度
            for (int i = 0; i < item.Value.Length - 1; i ++)
            {
                // 获得子字符串（长度2）
                string strValue = item.Value.Substring(i, 2);
                // 检查结果
                if (strValue == null || strValue.Length != 2) continue;
                // 描述
                GDescription description;
                // 检查结果
                if (!words.ContainsKey(strValue))
                {
                    // 创建描述
                    description = new GDescription();
                    // 设置参数
                    description.count = 0;
                    description.gamma = -1.0;
                    description.operation = true;
                    description.left = strValue.Substring(0, 1);
                    description.right = strValue.Substring(1, 1);
                    // 检查参数
                    if(sqlCacheOnly)
                    {
                        // 增加一条记录
                        words.Add(strValue, description);
                    }
                    else
                    {
                        // 尝试从数据库中加载
                        if (LoadWordCount(strValue) < 0)
                        {
                            // 增加一条记录
                            words.Add(strValue, description);
                        }
                    }
                }
                // 创建描述
                description = words[strValue];
                // 增加计数
                description.count ++;
                // 设置操作标记
                description.operation = true;
                // 设置数值
                words[strValue] = description;
                // 记录日志
                //Log.LogMessage(string.Format("\tcontent({0}) = {1}", strValue, description.count));
            }
        }
        // 记录日志
        Log.LogMessage("Grammar", "AddUpWordCount", "统计已完成！");
        // 返回结果
        return true;
    }

    public static void UpdateWordCount()
    {
        // 记录日志
        Log.LogMessage("Grammar", "UpdateWordCount", "更新数据！");
        Log.LogMessage(string.Format("\twords.count = {0}", words.Count));

        // 生成批量处理语句
        string cmdString =
            "UPDATE [dbo].[InnerContent] " +
                "SET [count] = @SqlCount, [gamma] = @SqlGamma " +
                "WHERE content = @SqlWord; " +
            "IF @@ROWCOUNT <= 0 " +
                "INSERT INTO [dbo].[InnerContent] " +
                "([classification], [content], [count], [gamma]) " +
                    "VALUES ('G2', @SqlWord, @SqlCount, @SqlGamma); ";

        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 记录日志
            Log.LogMessage("Grammar", "UpdateWordCount", "数据连接已开启！");

            // 开启事务处理模式
            SqlTransaction sqlTransaction =
                sqlConnection.BeginTransaction();
            // 记录日志
            Log.LogMessage("Grammar", "UpdateWordCount", "事务处理已开启！");

            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 设置事物处理模式
            sqlCommand.Transaction = sqlTransaction;
            // 记录日志
            Log.LogMessage("Grammar", "UpdateWordCount", "T-SQL指令已创建！");

            // 遍历参数
            foreach (KeyValuePair<string, GDescription> kvp in words)
            {
                // 获得描述
                GDescription description = kvp.Value;
                // 记录日志
                //Log.LogMessage(string.Format("description.content = {0}", kvp.Key));
                //Log.LogMessage(string.Format("\tdescription.count = {0}", description.count));
                //Log.LogMessage(string.Format("\tdescription.left = {0}", description.left));
                //Log.LogMessage(string.Format("\tdescription.right = {0}", description.right));
                //Log.LogMessage(string.Format("\tdescription.gamma = {0}", description.gamma));
                //Log.LogMessage(string.Format("\tdescription.operation = {0}", description.operation));
                // 检查操作标记
                if (!description.operation) continue;
                // 清理参数
                sqlCommand.Parameters.Clear();
                // 设置参数
                sqlCommand.Parameters.AddWithValue("SqlWord", kvp.Key);
                sqlCommand.Parameters.AddWithValue("SqlCount", description.count);
                sqlCommand.Parameters.AddWithValue("SqlGamma", description.gamma);
                // 执行指令（尚未执行）
                sqlCommand.ExecuteNonQuery();
                // 清理操作标记
                description.operation = false;
            }
            // 记录日志
            Log.LogMessage("Grammar", "UpdateWordCount", "批量指令已添加！");

            // 提交事务处理
            sqlTransaction.Commit();
            // 记录日志
            Log.LogMessage("Grammar", "UpdateWordCount", "批量指令已提交！");
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // 检查状态并关闭连接
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // 记录日志
        Log.LogMessage("Grammar", "UpdateWordCount", "数据记录已更新！");
    }

    public static void ReloadWordCount()
    {
        // 记录日志
        Log.LogMessage("Grammar", "ReloadWordCount", "清理数据记录！");

        // 清理字符
        words.Clear();

        // 记录日志
        Log.LogMessage("Grammar", "ReloadWordCount", "加载数据记录！");

        // 指令字符串
        string cmdString =
            "SELECT content, count, gamma FROM [dbo].[InnerContent] " +
            "WHERE classification <> '简明英汉词典' AND LEN(content) = 2;";

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
                if (strValue == null || strValue.Length != 2) continue;               
                // 创建描述
                GDescription description = new GDescription();
                // 设置参数
                description.operation = false;
                // 设置参数
                description.count = reader.GetInt32(1);
                description.gamma = reader.GetDouble(2);
                // 设置左侧
                description.left = strValue.Substring(0, 1);
                // 设置右侧
                description.right = strValue.Substring(1, 1);
                // 检查结果
                if (description.count < 0) description.count = 0;
                // 增加记录
                words.Add(strValue, description);
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
        Log.LogMessage("\twords.count = " + words.Count);
        // 记录日志
        Log.LogMessage("Grammar", "ReloadWordCount", "数据记录已加载！");
    }

    public static void RecalculateWordGamma(bool bAll)
    {
        // 记录日志
        Log.LogMessage("Grammar", "RecalculateWordGamma", "更新数据！");
        // 记录日志
        Log.LogMessage("\twords.count = " + words.Count);

        // 创建新词典
        List<string> keys = new List<string>();
        // 增加关键字
        keys.AddRange(words.Keys);

        // 遍历参数
        foreach (string strWord in keys)
        {
            // 检查结果
            if (strWord == null || strWord.Length != 2) continue;
            // 获得描述
            GDescription description = words[strWord];
            // 检查标记
            if (!bAll)
            {
                // 检查操作标记
                if (!description.operation && description.gamma > 0) continue;
            }
            // 设置操作标记
            description.operation = true;
            // 获得左侧内容
            description.left = strWord.Substring(0, 1);
            // 获得右侧内容
            description.right = strWord.Substring(1, 1);
            //检查结果
            if (description.count < 0) description.count = 0;
            // 获得左侧计数器
            int leftCount = GetCharCount(description.left[0]);
            // 获得右侧计数器
            int rightCount = GetCharCount(description.right[0]);
            // 检查结果
            if (leftCount <= 0 || rightCount <= 0) description.gamma = -1.0;
            else
            {
                // 获得计数
                int count = description.count;
                // 检查结果
                if (count <= 0) count = 1;
                // 计算相关系数
                description.gamma = 0.0;
                description.gamma += (double)count / leftCount;
                description.gamma += (double)count / rightCount;
                description.gamma = 2.0 / description.gamma;
            }
            // 重新设置数值
            words[strWord] = description;
            // 记录日志
            //Log.LogMessage(string.Format("description.content = {0}", strWord));
            //Log.LogMessage(string.Format("\tdescription.count = {0}", description.count));
            //Log.LogMessage(string.Format("\tdescription.leftCount = {0}", leftCount));
            //Log.LogMessage(string.Format("\tdescription.rightCount = {0}", rightCount));
            //Log.LogMessage(string.Format("\tdescription.gamma = {0}", description.gamma));
            //Log.LogMessage(string.Format("\tdescription.operation = {0}", description.operation));
        }

        // 记录日志
        Log.LogMessage("Grammar", "RecalculateWordGamma", "数据已更新！");
    }
}
