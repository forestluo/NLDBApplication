using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class FilterRule
{
    // 过滤规则
    private static Dictionary<string, string> rules = new Dictionary<string, string>();

    public static string FilterContent(string strValue)
    {
        // 标志位
        bool filtered;

        do
        {
            // 初始化标志位
            filtered = false;
            // 执行循环
            foreach (KeyValuePair<string, string> kvp in rules)
            {
                // 查询匹配项
                if (Regex.IsMatch(strValue, kvp.Key))
                {
                    // 记录日志
                    //Log.LogMessage("rules.rule = " + kvp.Key);
                    //Log.LogMessage("\tstrValue = " + strValue);

                    // 设置标记位
                    filtered = true;
                    // 替换字符串
                    strValue = Regex.Replace(strValue, kvp.Key, kvp.Value);
                    // 记录日志
                    //Log.LogMessage("\tReplace = " + strValue);
                }
            }

        } while (filtered);

        // 返回结果
        return strValue;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ReloadFilterRule()
    {
        // 记录日志
        Log.LogMessage("FilterRule", "ReloadFilterRule", "清除数据记录！");

        // 删除之前所有记录
        rules.Clear();

        // 记录日志
        Log.LogMessage("FilterRule", "ReloadFilterRule", "加载数据记录！");

        // 指令字符串
        string cmdString =
            "SELECT [rule], [replace], [classification] " +
            "FROM [dbo].[FilterRule] WHERE classification = '正则替换';";

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
                // 检查参数
                if (reader.IsDBNull(0)) continue;
                // 获得规则
                string rule = reader.GetString(0);
                // 检查参数
                if (reader.IsDBNull(2)) continue;
                // 获得分类
                string classification = reader.GetString(2);
                // 获得替代
                string replace = reader.GetString(1);
                // 增加一条规则
                rules.Add(rule, replace);
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
        Log.LogMessage("rules.count = " + rules.Count);
        // 记录日志
        Log.LogMessage("FilterRule", "ReloadFilterRule", "数据记录已加载！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateFilterRule(SqlBoolean sqlInitialize)
    {
        // 记录日志
        Log.LogMessage("FilterRule", "CreateFilterRule", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除之前的表
            "IF OBJECT_ID('FilterRule') IS NOT NULL " +
            "DROP TABLE dbo.FilterRule; " +
            // 创建数据表
            "CREATE TABLE dbo.FilterRule " +
            "( " +
            // 编号
            "[fid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 分类描述
            "[classification]       NVARCHAR(64)            NULL, " +
            // 规则
            "[rule]                 NVARCHAR(256)           PRIMARY KEY                 NOT NULL, " +
            // 替代
            "[replace]              NVARCHAR(256), " +
            // 规则要求
            "[requirements]         NVARCHAR(MAX)           NULL " +
            "); ";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("FilterRule", "CreateFilterRule", "数据表已创建！");

        // 初始化过滤规则
        if(sqlInitialize) InitializeFilterRule();
    }

    public static void InitializeFilterRule ()
    {
        // 记录日志
        Log.LogMessage("FilterRule", "InitializeFilterRule", "初始化数据表！");

        // 初始化过滤规则
        AddFilterRule("[\\u0020]\\s", " ");

        AddFilterRule("<(br|hr|input)((\\s|.)*)/>", " ");
        AddFilterRule("<(img|doc|url|input)((\\s|.)*)>", " ");
        AddFilterRule("<[a-zA-Z]+\\s*[^>]*>(.*?)</[a-zA-Z]+>", "$1");

        AddFilterRule("---|--", "—");
        AddFilterRule("[']{2}", "'");
        AddFilterRule("[<]{2}", "<");
        AddFilterRule("[>]{2}", ">");
        AddFilterRule("[～]{2}", "～");
        AddFilterRule("[—]{2}", "—");
        AddFilterRule("，(：|\\s)+", "，");
        AddFilterRule("(、|，|\\s)+，", "，");
        AddFilterRule("：(，|\\s)+", "：");
        AddFilterRule("(、|：|\\s)+：", "：");
        AddFilterRule("(，|、|；|\\s)+；", "；");
        AddFilterRule("；(，|：|。|？|！|\\s)+", "；");
        AddFilterRule("(，|、|：|。|\\s)+。", "。");
        AddFilterRule("。(，|：|；|？|！|\\s)+", "。");
        AddFilterRule("(，|、|：|？|\\s)+？", "？");
        AddFilterRule("？(，|、|：|！|；|。|\\s)+", "？");
        AddFilterRule("(，|、|：|！|\\s)+！", "！");
        AddFilterRule("！(，|、|：|？|；|。|\\s)+", "！");
        AddFilterRule("(\\.\\.\\.|……|。。。|，，，|．．．|～～～)", "…");
        AddFilterRule("\\s(\\<|\\>|【|】|〈|〉|“|”|‘|’|《|》|\\(|\\)|（|）|…|～|—|、|？|！|；|。|：|，)", "$1");
        AddFilterRule("(\\<|\\>|【|】|〈|〉|“|”|‘|’|《|》|\\(|\\)|（|）|…|～|—|、|？|！|；|。|：|，)\\s", "$1");
        // 记录日志
        Log.LogMessage("FilterRule", "InitializeFilterRule", "数据表已经初始化！");
    }

    public static void AddFilterRule(SqlString sqlRule, SqlString sqlReplace)
    {
        // 在数据库中也增加一条规则
        string cmdString =
            "INSERT INTO [dbo].[FilterRule] " +
            "([classification], [rule], [replace]) VALUES ('正则替换', @SqlRule, @SqlReplace);";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlRule", sqlRule.Value);
        parameters.Add("SqlReplace", sqlReplace.Value);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }
}
