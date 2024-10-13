public partial class PairSplitter
{
    // 结束分割符
    private readonly static
        char[] PAIR_END_SPLITTERS = { '”', '）', '’', '》', '】', '〉' };
    // 起始分割符
    private readonly static
        char[] PAIR_START_SPLITTERS = { '“', '（', '‘', '《', '【', '〈' };
    // 成对分割符
    private readonly static
        char[] PAIR_SPLITTERS = { '“', '”', '（', '）', '《', '》', '‘', '’', '【', '】', '〈', '〉' };

    public static char GetPairEnd(char cValue)
    {
        if (cValue == '“') return '”';
        if (cValue == '（') return '）';
        if (cValue == '‘') return '’';
        if (cValue == '《') return '》';
        if (cValue == '【') return '】';
        if (cValue == '〈') return '〉';
        // 返回结果
        return cValue;
    }

    public static bool IsPairEnd(char cValue)
    {
        foreach (char item in PAIR_END_SPLITTERS)
        {
            if (cValue == item) return true;
        }
        // 返回结果
        return false;
    }

    public static bool IsPairStart(char cValue)
    {
        foreach (char item in PAIR_START_SPLITTERS)
        {
            if (cValue == item) return true;
        }
        // 返回结果
        return false;
    }

    public static bool IsPairSplitter(char cValue)
    {
        foreach (char item in PAIR_SPLITTERS)
        {
            if (cValue == item) return true;
        }
        // 返回结果
        return false;
    }

    public static bool IsPairMatched(char cStart, char cEnd)
    {
        if (cStart == '“' && cEnd == '”') return true;
        if (cStart == '（' && cEnd == '）') return true;
        if (cStart == '‘' && cEnd == '’') return true;
        if (cStart == '《' && cEnd == '》') return true;
        if (cStart == '【' && cEnd == '】') return true;
        if (cStart == '〈' && cEnd == '〉') return true;
        // 返回结果
        return false;
    }

    public static bool HasPairSplitter(string strValue)
    {
        for (int i = 0; i < strValue.Length; i++)
        {
            if (IsPairSplitter(strValue[i])) return true;
        }
        // 返回结果
        return false;
    }
}