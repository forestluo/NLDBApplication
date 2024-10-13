using System.Text.RegularExpressions;

public partial class Regular
{
    // 分割规则
    public readonly static string SPLIT_RULE =
        "((?!(，|：|；|\\。|\\？|\\！|“|”|（|）|《|》|‘|’|【|】|〈|〉)).)+";

    public static string RecoverRegularRule(string strRule)
    {
        if (strRule.IndexOf("$a") >= 0)
            strRule = strRule.Replace("$a", GetRegularString("$a"));
        if (strRule.IndexOf("$b") >= 0)
            strRule = strRule.Replace("$b", GetRegularString("$b"));
        if (strRule.IndexOf("$c") >= 0)
            strRule = strRule.Replace("$c", GetRegularString("$c"));
        if (strRule.IndexOf("$d") >= 0)
            strRule = strRule.Replace("$d", GetRegularString("$d"));
        if (strRule.IndexOf("$e") >= 0)
            strRule = strRule.Replace("$e", GetRegularString("$e"));
        if (strRule.IndexOf("$f") >= 0)
            strRule = strRule.Replace("$f", GetRegularString("$f"));
        if (strRule.IndexOf("$n") >= 0)
            strRule = strRule.Replace("$n", GetRegularString("$n"));
        if (strRule.IndexOf("$s") >= 0)
            strRule = strRule.Replace("$s", GetRegularString("$s"));

        if (strRule.IndexOf("$q") >= 0)
            strRule = strRule.Replace("$q", GetRegularString("$q"));
        if (strRule.IndexOf("$u") >= 0)
            strRule = strRule.Replace("$u", GetRegularString("$u"));
        if (strRule.IndexOf("$v") >= 0)
            strRule = strRule.Replace("$v", GetRegularString("$v"));
        if (strRule.IndexOf("$y") >= 0)
            strRule = strRule.Replace("$y", GetRegularString("$y"));
        if (strRule.IndexOf("$z") >= 0)
            strRule = strRule.Replace("$z", GetRegularString("$z"));
        // 返回结果
        return strRule;
    }

    public static string GetRegularString(string strType)
    {
        // 检查类型
        if(strType == "$a")
        {
            // 返回结果
            return "(((?!(，|：|；|…|—|《|》|\\。|\\？|\\！|\\s)).)*)";
        }
        // 检查类型
        if (strType == "$b")
        {
            // 返回结果
            return "(((?!(，|：|；|…|—|《|》|\\。|\\？|\\！|\\s)).)+)";
        }
        // 检查类型
        if (strType == "$c")
        {
            // 返回结果
            return "[一|二|三|四|五|六|七|八|九]";
        }
        // 检查类型
        if (strType == "$d")
        {
            // 返回结果
            return "(-?[1-9]\\d*|0)";
        }
        // 检查类型
        if (strType == "$e")
        {
            // 返回结果
            return "[A-Za-z]";
        }
        // 检查类型
        if (strType == "$f")
        {
            // 返回结果
            return "(-?([1-9]\\d*|[1-9]\\d*\\.\\d+|0\\.\\d+))";
        }
        // 检查类型
        if (strType == "$n")
        {
            // 返回结果
            return "(\\d+)";
        }
        // 检查类型
        if (strType == "$s")
        {
            // 返回结果
            return "[A-Za-z]+[A-Za-z'' ]*";
        }
        // 检查类型
        if (strType == "$q")
        {
            // 返回结果
            return "(" + CoreContent.QUANTIFIER_NAME + ")";
        }
        // 检查类型
        if (strType == "$u")
        {
            // 返回结果
            return "(" + CoreContent.UNIT_NAME + ")";
        }
        // 检查类型
        if (strType == "$v")
        {
            // 返回结果
            return "(" + CoreContent.UNIT_SYMBOL + ")";
        }
        // 检查类型
        if (strType == "$y")
        {
            // 返回结果
            return "(" + CoreContent.CURRENCY_NAME + ")";
        }
        // 检查类型
        if (strType == "$z")
        {
            // 返回结果
            return "(" + CoreContent.CURRENCY_SYMBOL + ")";
        }
        // 检查类型
        if (strType == "parameter")
        {
            // 返回结果
            return "(((?!(，|：|；|…|—|\\。|\\？|\\！)).)+)";
        }
        // 返回结果
        return null;
    }
}