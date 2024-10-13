public partial class Punctuation
{
    public static bool IsPunctuation(char cValue)
    {
        // ���ؽ��
        return Splitter.IsMajorSplitter(cValue)
            || PairSplitter.IsPairSplitter(cValue);
    }

    public static bool HasPunctuation(string strValue)
    {
        for (int i = 0; i < strValue.Length; i++)
        {
            if (IsPunctuation(strValue[i])) return true;
        }
        // ���ؽ��
        return false;
    }
}
