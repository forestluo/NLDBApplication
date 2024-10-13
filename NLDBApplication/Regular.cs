using System.Text.RegularExpressions;

public partial class Regular
{
    // �ָ����
    public readonly static string SPLIT_RULE =
        "((?!(��|��|��|\\��|\\��|\\��|��|��|��|��|��|��|��|��|��|��|��|��)).)+";

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
        // ���ؽ��
        return strRule;
    }

    public static string GetRegularString(string strType)
    {
        // �������
        if(strType == "$a")
        {
            // ���ؽ��
            return "(((?!(��|��|��|��|��|��|��|\\��|\\��|\\��|\\s)).)*)";
        }
        // �������
        if (strType == "$b")
        {
            // ���ؽ��
            return "(((?!(��|��|��|��|��|��|��|\\��|\\��|\\��|\\s)).)+)";
        }
        // �������
        if (strType == "$c")
        {
            // ���ؽ��
            return "[һ|��|��|��|��|��|��|��|��]";
        }
        // �������
        if (strType == "$d")
        {
            // ���ؽ��
            return "(-?[1-9]\\d*|0)";
        }
        // �������
        if (strType == "$e")
        {
            // ���ؽ��
            return "[A-Za-z]";
        }
        // �������
        if (strType == "$f")
        {
            // ���ؽ��
            return "(-?([1-9]\\d*|[1-9]\\d*\\.\\d+|0\\.\\d+))";
        }
        // �������
        if (strType == "$n")
        {
            // ���ؽ��
            return "(\\d+)";
        }
        // �������
        if (strType == "$s")
        {
            // ���ؽ��
            return "[A-Za-z]+[A-Za-z'' ]*";
        }
        // �������
        if (strType == "$q")
        {
            // ���ؽ��
            return "(" + CoreContent.QUANTIFIER_NAME + ")";
        }
        // �������
        if (strType == "$u")
        {
            // ���ؽ��
            return "(" + CoreContent.UNIT_NAME + ")";
        }
        // �������
        if (strType == "$v")
        {
            // ���ؽ��
            return "(" + CoreContent.UNIT_SYMBOL + ")";
        }
        // �������
        if (strType == "$y")
        {
            // ���ؽ��
            return "(" + CoreContent.CURRENCY_NAME + ")";
        }
        // �������
        if (strType == "$z")
        {
            // ���ؽ��
            return "(" + CoreContent.CURRENCY_SYMBOL + ")";
        }
        // �������
        if (strType == "parameter")
        {
            // ���ؽ��
            return "(((?!(��|��|��|��|��|\\��|\\��|\\��)).)+)";
        }
        // ���ؽ��
        return null;
    }
}