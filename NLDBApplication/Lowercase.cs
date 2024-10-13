public partial class Lowercase
{
    public static bool IsLowercase(char cValue)
    {
        // 返回结果
        return cValue >= 97 && cValue <= 122;
    }

    public static char GetLowercase(int index)
    {
        // 返回结果
        return index >= 1 && index <= 26 ? (char)(96 + index) : '?';
    }
}
