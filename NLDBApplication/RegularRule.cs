using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class RegularRule
{
    // 正则规则
    private static Dictionary<string, string[]> rules = new Dictionary<string, string[]>();

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void d_ExtractExpressions(SqlString sqlContent)
    {
        ExtractExpressions(sqlContent.Value);
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ReloadRegularRule()
    {
        // 记录日志
        Log.LogMessage("RegularRule", "ReloadRegularRule", "清除数据记录！");

        // 删除之前所有记录
        rules.Clear();

        // 记录日志
        Log.LogMessage("RegularRule", "ReloadRegularRule", "加载数据记录！");

        // 指令字符串
        string cmdString =
            "SELECT [rule], [attribute], [classification] " +
            "FROM [dbo].[RegularRule] " +
            "WHERE classification IN ('正则','数词','数量词'); ";

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
                // 获得参数
                if (reader.IsDBNull(0)) continue;
                string rule = reader.GetString(0);
                if (reader.IsDBNull(1)) continue;
                string attribute = reader.GetString(1);
                if (reader.IsDBNull(2)) continue;
                string classification = reader.GetString(2);
                // 内容转义
                rule = Regular.RecoverRegularRule(rule);
                // 增加一条规则
                rules.Add(rule, new string[] { attribute, classification });
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
        Log.LogMessage("RegularRule", "ReloadRegularRule", "数据记录已加载！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CreateRegularRule(SqlBoolean sqlInitialize)
    {
        // 记录日志
        Log.LogMessage("RegularRule", "CreateRegularRule", "创建数据表！");

        // 指令字符串
        string cmdString =
            // 删除之前的索引
            "IF OBJECT_ID('RegularRuleRIDIndex') IS NOT NULL " +
            "DROP INDEX dbo.RegularRuleRIDIndex; " +
            // 删除之前的表
            "IF OBJECT_ID('RegularRule') IS NOT NULL " +
            "DROP TABLE dbo.RegularRule; " +
            // 创建数据表
            "CREATE TABLE dbo.RegularRule " +
            "( " +
            // 编号
            "[rid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
            // 分类描述
            "[classification]       NVARCHAR(64)            NULL, " +
            // 规则
            "[rule]                 NVARCHAR(450)           PRIMARY KEY                 NOT NULL, " +
            // 替代
            "[attribute]            NVARCHAR(64), " +
            // 规则要求
            "[requirements]         NVARCHAR(MAX)           NULL " +
            "); " +
            // 创建简单索引
            "CREATE INDEX RegularRuleRIDIndex ON dbo.RegularRule(rid);";

        // 执行指令
        NLDB.ExecuteNonQuery(cmdString);

        // 记录日志
        Log.LogMessage("RegularRule", "CreateRegularRule", "数据表已创建！");

        // 初始化数据表
        if (sqlInitialize) InitializeRegularRule();
    }

    public static void InitializeRegularRule()
    {
        // 临时变量
        string strValue;
        string strPrefix;
        string strSuffix;

        // 记录日志
        Log.LogMessage("RegularRule", "InitializeRegularRule", "初始化数据表！");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载正则规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        // 正则表达式
        // 编号
        AddRegularRule("正则", "0\\d*", "编号");
        AddRegularRule("正则", "No.\\d*", "编号");
        // 整数
        AddRegularRule("正则", "-?[1-9]\\d*", "整数");
        AddRegularRule("正则", "[1-9]\\d*[\\s]?-[\\s]?[1-9]\\d*", "整数");
        AddRegularRule("正则", "[1-9]\\d{0,2}(,\\d{3})+", "整数");
        AddRegularRule("正则", "[1-9]\\d*[\\s]?[十|百|千|万|亿]", "整数");
        AddRegularRule("正则", "[1-9]\\d*[\\s]?[十|百|千|万|亿|兆]亿", "整数");
        // 百分数
        AddRegularRule("正则", "\\d+(\\.\\d+)?%", "百分数");
        AddRegularRule("正则", "\\d+(\\.\\d+)?%-\\d+(\\.\\d+)?%", "百分数");
        // 分数
        AddRegularRule("正则", "[1-9]\\d*[\\s]?/[\\s]?[1-9]\\d*", "分数");
        AddRegularRule("正则", "[1-9]\\d*[\\s]?/[\\s]?[1-9]\\d*-[1-9]\\d*/[1-9]\\d*", "分数");
        // 英文单词
        AddRegularRule("正则", "[A-Za-z][\'A-Za-z]*", "英文");
        // 浮点数
        AddRegularRule("正则", "-?([1-9]\\d*\\.\\d+|0\\.\\d*[1-9]\\d*|0?\\.0+)", "浮点数");
        AddRegularRule("正则", "([1-9]\\d*\\.\\d+|0\\.\\d*[1-9]\\d*|0?\\.0+)[\\s]?[十|百|千|万|亿]", "浮点数");
        AddRegularRule("正则", "([1-9]\\d*\\.\\d+|0\\.\\d*[1-9]\\d*|0?\\.0+)[\\s]?[十|百|千|万|亿|兆]亿", "浮点数");

        // 日期
        AddRegularRule("正则", "(\\d{4}|\\d{2})[\\s]?年", "日期");
        AddRegularRule("正则", "(0?[1-9]|1[0-2])[\\s]?月", "日期");
        AddRegularRule("正则", "((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?号", "日期");
        AddRegularRule("正则", "((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日", "日期");
        AddRegularRule("正则", "(\\d{4}|\\d{2})[\\s]?年[\\s]?((1[0-2])|(0?[1-9]))[\\s]?月", "日期");
        AddRegularRule("正则", "(0?[1-9]|1[0-2])[\\s]?月((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日", "日期");
        AddRegularRule("正则", "(\\d{4}|\\d{2})-((1[0-2])|(0?[1-9]))[\\s]?-[\\s]?(([12][0-9])|(3[01])|(0?[1-9]))", "日期");
        AddRegularRule("正则", "(\\d{4}|\\d{2})[\\s]?年((1[0-2])|(0?[1-9]))[\\s]?月(([12][0-9])|(3[01])|(0?[1-9]))[\\s]?日", "日期");
        // 时间
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?时", "时间");
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?:[\\s]?([0-5][0-9])", "时间");
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?时([0-5][0-9])[\\s]?分", "时间");
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?点([0-5][0-9])[\\s]?分", "时间");
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?:[\\s]?([0-5][0-9])[\\s]?:[\\s]?([0-5][0-9])", "时间");
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?时[\\s]?([0-5][0-9])分([0-5][0-9])[\\s]?秒", "时间");
        AddRegularRule("正则", "((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日[\\s]?((1|0?)[0-9]|2[0-3])[\\s]?时[\\s]?([0-5][0-9])[\\s]?分", "时间");
        // 时间段
        AddRegularRule("正则", "(\\d{4}|\\d{2})[\\s]?年[\\s]?-[\\s]?(\\d{4}|\\d{2})[\\s]?年", "时间段");
        AddRegularRule("正则", "(\\d{4}|\\d{2})[\\s]?/[\\s]?(\\d{4}|\\d{2})[\\s]?财年", "时间段");
        AddRegularRule("正则", "((1|0?)[0-9]|2[0-3])[\\s]?[点|时][\\s]?-[\\s]?((1|0?)[0-9]|2[0-3])[\\s]?[点|时]", "时间段");
        AddRegularRule("正则", "((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?[日|号][\\s]?-[\\s]?((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?[日|号]", "时间段");
        AddRegularRule("正则", "(\\d{4}|\\d{2})年((1[0-2])|(0?[1-9]))[\\s]?月[\\s]?-[\\s]?(\\d{4}|\\d{2})年((1[0-2])|(0?[1-9]))[\\s]?月", "时间段");
        AddRegularRule("正则", "((1[0-2])|(0?[1-9]))[\\s]?月[\\s]?(([12][0-9])|(3[01])|(0?[1-9]))[\\s]?日[\\s]?-[\\s]?((1[0-2])|(0?[1-9]))[\\s]?月[\\s]?(([12][0-9])|(3[01])|(0?[1-9]))[\\s]?日", "时间段");
        AddRegularRule("正则", "(\\d{4}|\\d{2})[\\s]?年[\\s]?((1[0-2])|(0?[1-9]))[\\s]?月[\\s]?(([12][0-9])|(3[01])|(0?[1-9]))[\\s]?日[\\s]?-[\\s]?(\\d{4}|\\d{2})[\\s]?年[\\s]?((1[0-2])|(0?[1-9]))[\\s]?月[\\s]?(([12][0-9])|(3[01])|(0?[1-9]))[\\s]?日", "时间段");

        // 特殊标识
        // 身份证号码
        AddRegularRule("正则", "\\d{15}(\\d\\d[0-9xX])?", "身份证");
        // 国内电话号码（易混淆）
        AddRegularRule("正则", "(\\d{4}|\\d{3})?[\\s]?-[\\s]?(\\d{8}|\\d{7})", "电话");
        AddRegularRule("正则", "(\\d{4}|\\d{3})?[\\s]?-[\\s]?(\\d{4}|\\d{3})?-[\\s]?(\\d{4}|\\d{3})", "电话");
        // HTML标记（易混淆、有死循环可能）
        // AddRegularRule("正则", "<(.*)(.*)>.*<\\/\\1>|<(.*) \\/>", "HTML标记");
        // EMail地址
        AddRegularRule("正则", "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", "电子邮箱");
        // IP地址
        AddRegularRule("正则", "((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)", "IP地址");
        // URL地址（易混淆）
        AddRegularRule("正则", "\\b(([\\w-]+://?|www[.])[^\\s()<>]+(?:\\([\\w\\d]+\\)|([^[:punct:]\\s]|/)))", "URL地址");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载数词规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        // 数词
        AddRegularRule("数词", "$n[\\.|\\)]", "编号");
        AddRegularRule("数词", "“[\\s]?$n[\\s]?”", "编号");
        AddRegularRule("数词", "[0-9][\\dA-Za-z]*", "编号");
        AddRegularRule("数词", "$e+$n", "编号");
        AddRegularRule("数词", "$e+$n$e*", "编号");
        AddRegularRule("数词", "“[\\s]?$n[\\s]?·$n[\\s]?”", "编号");
        AddRegularRule("数词", "\\([\\s]?$n[\\s]?\\)|\\<[\\s]?$n[\\s]?\\>|\\{[\\s]?$n[\\s]?\\}", "编号");

        AddRegularRule("数词", "$d[多|余](万|亿)", "数量");
        AddRegularRule("数词", "$d[\\s]?:[\\s]?$d", "比例");
        AddRegularRule("数词", "$d[\\s]?(~|至)[\\s]?$d", "区间");

        AddRegularRule("数词", "(零|$c){2,}", "序号");
        AddRegularRule("数词", "十$c", "十位数");
        AddRegularRule("数词", "$c十$c?", "十位数");
        AddRegularRule("数词", "$c百(零|$c十)?$c?", "百位数");
        AddRegularRule("数词", "$c千((零|$c百)?(零|$c十)?$c?)", "千位数");
        AddRegularRule("数词", "$c千((零|$c百)?(零|$c十)?$c?)万", "万位数");
        AddRegularRule("数词", "$c万((零|$c千)?((零|$c百)?(零|$c十)?$c?))", "万位数");

        AddRegularRule("数词", "$f个百分点", "百分点");
        AddRegularRule("数词", "百分之零点(几|(零|$c)+)", "百分数");
        AddRegularRule("数词", "百分之(($c?百)|((($c十)|十)?$c?))", "百分数");
        AddRegularRule("数词", "百分之(($c?百)|((($c十)|十)?$c?))($c|几|左右)?", "百分数");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载序号规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        strSuffix = "$q";

        strValue = "第$d";
        AddRegularRule("数词", strValue, "序数");
        strValue = "第[\\s]?$d[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "序数");

        strValue = "第$c";
        AddRegularRule("数词", strValue, "序数");
        strValue = "第[\\s]?$c[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "序数");

        strValue = "第十$c?";
        AddRegularRule("数词", strValue, "序数");
        strValue = "第[\\s]?十$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "序数");

        strValue = "第$c十$c?";
        AddRegularRule("数词", strValue, "序数");
        strValue = "第[\\s]?$c十$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "序数");

        strValue = "第$c百(零|$c十)?$c?";
        AddRegularRule("数词", strValue, "序数");
        strValue = "第[\\s]?$c百(零|$c十)?$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "序数");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载数量词规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        strSuffix = "($q|$u)";

        strValue = "$d" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strValue = "$d[\\s]?[多|余]" + strSuffix;
        AddRegularRule("数量词", strValue, "约数");

        strValue = "几(亿|万|千|百|十)[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "约数");

        strValue = "$c[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strValue = "十$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strValue = "$c十$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strValue = "$c百(零|$c十)?$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strValue = "$c千(零|$c百)?(零|$c十)?$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strValue = "(十|$c)万(零|$c千)?(零|$c百)?(零|$c十)?$c?[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        strSuffix = "$v";
        strValue = "$f[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载约数规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        strSuffix = "($q|$u)";

        strValue = "$f[\\s]?(亿|万|千|百)?(余|多)?" + strSuffix;
        AddRegularRule("数量词", strValue, "约数");

        strValue = "(十|$c)[\\s]?(亿|万|千|百|十)(余|多)?" + strSuffix;
        AddRegularRule("数量词", strValue, "约数");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载货币规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        strSuffix = "$y";

        strValue = "$f[\\s]?(百|千|万)?(亿|万)?(余|多)?" + strSuffix;
        AddRegularRule("数量词", strValue, "货币");

        strValue = "$f[\\s]?[多|余]?(百|千|万)?(亿|万)?(余|多)?" + strSuffix;
        AddRegularRule("数量词", strValue, "货币");

        strValue = "[1-9]\\d{0,2}(,\\d{3})+(百|千|万)?(亿|万)?(余|多)?" + strSuffix;
        AddRegularRule("数量词", strValue, "货币");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载单位规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        // 单位
        // 修正规则
        strPrefix = "($q|$u|$v|$y)";
        strSuffix = "($q|$u|$v|$y)";

        strValue = strPrefix + "[\\s]?/[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "单位");

        strValue = strPrefix + "[\\s]?每[\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "单位");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载时间规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        // 时间
        AddRegularRule("数量词", "[1-9]\\d*[\\s]?个月", "月");
        AddRegularRule("数量词", "[过]?年[前|中|底|后]", "年");
        AddRegularRule("数量词", "(第)?(一|二|三|四)季[度]?", "季度");
        AddRegularRule("数量词", "周[一|二|三|四|五|六|日]", "周");
        AddRegularRule("数量词", "星期[一|二|三|四|五|六|日]", "星期");
        AddRegularRule("数量词", "((1[0-2])|(0?[1-9]))[\\s]?月份", "月份");
        AddRegularRule("数量词", "([1-9]\\d*[\\s]?、)+([1-9]\\d*[\\s]?)(一|二|三|四|五|六|七|八|九|十|十一|十二)个月", "月");
        AddRegularRule("数量词", "(((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?、)*((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日", "日期");
        AddRegularRule("数量词", "((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?至[\\s]?((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日", "日期");
        AddRegularRule("数量词", "((1[0-2])|(0?[1-9]))[\\s]?月[\\s]?(((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日)(、((0?[1-9])|((1|2)[0-9])|30|31)[\\s]?日)*", "日期");

        ////////////////////////////////////////////////////////////////////////////////
        //
        // 加载其他规则。
        //
        ////////////////////////////////////////////////////////////////////////////////

        // 特殊词汇
        strSuffix = "(方|点|个|项|次|根|颗|条|名|套|份)";
        strValue = "[双|两|叁|多][\\s]?" + strSuffix;
        AddRegularRule("数量词", strValue, "数量");

        // 航班
        AddRegularRule("数量词", "$e+$n次[\\s]?航班", "航班");
        // 股票
        AddRegularRule("数量词", "(A|B|H)股", "股票");
        // 油料
        AddRegularRule("数量词", "$n#", "油料");
        AddRegularRule("数量词", "$f个油", "油耗");
        // 维生素
        AddRegularRule("数量词", "维生素(A|B|C|D|E|K|H|P|M|T|U)\\d{0,2}", "维生素");

        // 记录日志
        Log.LogMessage("RegularRule", "InitializeRegularRule", "数据表已初始化！");
    }

    public static void AddRegularRule(string classification, string rule, string attribute)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[" + NLDB.REGULAR_RULE_TABLE + "] " +
            "([classification], [rule], [attribute]) VALUES (@SqlClassification,@SqlRule,@SqlAttribute); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlRule", rule);
        parameters.Add("SqlAttribute", attribute);
        parameters.Add("SqlClassification", classification);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }
}
