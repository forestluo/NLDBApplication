using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class NLDB
{
    // 数据表名
    public readonly static string
        DICTIONARY_TABLE = "Dictionary";
    public readonly static string
        LOGIC_RULE_TABLE = "LogicRule";
    public readonly static string
        FILTER_RULE_TABLE = "FilterRule";
    public readonly static string
        PHRASE_RULE_TABLE = "PhraseRule";
    public readonly static string
        REGULAR_RULE_TABLE = "RegularRule";
    // 数据表名
    public readonly static string
        TEXT_CONTENT_TABLE = "TextContent";
    public readonly static string
        CORE_CONTENT_TABLE = "CoreContent";
    public readonly static string
        INNER_CONTENT_TABLE = "InnerContent";
    public readonly static string
        OUTER_CONTENT_TABLE = "OuterContent";
    public readonly static string
        SENTENCE_CONTENT_TABLE = "SentenceContent";

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString GetVersion ()
    {
        // 返回结果
        return new SqlString ("NLDB v3.0.0.0");
    }

    public static void ExecuteNonQuery (string cmdString)
    {
        // 创建数据库连接
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // 开启数据库连接
            sqlConnection.Open();
            // 创建指令
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // 执行指令
            sqlCommand.ExecuteNonQuery();
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // 检查状态并关闭连接
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }
    }

    public static void ExecuteNonQuery (string cmdString, Dictionary<string, string> parameters)
    {
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
            foreach(KeyValuePair<string, string> kvp in parameters)
            {
                sqlCommand.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }
            // 执行指令
            sqlCommand.ExecuteNonQuery();
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // 检查状态并关闭连接
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }
    }
}
