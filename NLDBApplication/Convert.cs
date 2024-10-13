using System.Text;

public partial class Convert
{
    public static string LatinConvert(string strValue)
    {
        // ȫ��ת���
        //return Strings.StrConv(strValue.Value, VbStrConv.Narrow, 0);

        // �����ַ���
        StringBuilder sb = new StringBuilder(strValue.Length);
        // ѭ������
        foreach (char cValue in strValue)
        {
            // ���⴦��
            if (cValue == 12288) sb.Append(' ');
            // ����ַ���Χ
            else if (cValue < 65281) sb.Append(cValue);
            else if (cValue > 65374) sb.Append(cValue);
            // ת���ɰ��
            else sb.Append((char)(cValue - 65248));
        }
        // ���ؽ��
        return sb.ToString();
    }

    public static string UnicodeConvert(string strValue)
    {
        // ȫ��ת���
        //return Strings.StrConv(strValue.Value, VbStrConv.Wide, 0);

        // �����ַ���
        StringBuilder sb = new StringBuilder(strValue.Length);
        // ѭ������
        foreach (char cValue in strValue)
        {
            // ���⴦��
            if (cValue == 32) sb.Append((char)12288);
            // ����ַ���Χ
            else if (cValue < 33) sb.Append(cValue);
            else if (cValue > 126) sb.Append(cValue);
            // ת����ȫ��
            else sb.Append((char)(cValue + 65248));
        }
        // ���ؽ��
        return sb.ToString();
    }
}
