using System.Collections.Generic;

public partial class CoreContent
{
    // ��������
    public static string UNIT_SYMBOL = "";

    public static void ReloadUnitSymbol()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "ReloadUnitSymbol", "���ص�λ���ţ�");
        // ��������
        UNIT_SYMBOL = ReloadStringValue("����", "��λ");
        // ��¼��־
        Log.LogMessage(UNIT_SYMBOL);
    }

    public static void AddUnitSymbol(string symbol)
    {
        // ָ���ַ���
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('����', @SqlContent, '��λ'); ";
        // ���ò���
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // �������
        parameters.Add("SqlContent", symbol);
        // ִ��ָ��
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllUnitSymbols()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllUnitSymbols", "���ص�λ���ţ�");

        AddUnitSymbol("��");
        AddUnitSymbol("��");
        AddUnitSymbol("��");
        AddUnitSymbol("A");
        //AddUnitSymbol("?");
        AddUnitSymbol("Bq");
        AddUnitSymbol("C");
        AddUnitSymbol("��");
        AddUnitSymbol("cal");
        AddUnitSymbol("cm");
        //AddUnitSymbol("cm?");
        //AddUnitSymbol("cm?");
        AddUnitSymbol("d");
        AddUnitSymbol("db");
        //AddUnitSymbol("?");
        AddUnitSymbol("ev");
        AddUnitSymbol("F");
        AddUnitSymbol("�H");
        AddUnitSymbol("g");
        AddUnitSymbol("Gy");
        AddUnitSymbol("H");
        //AddUnitSymbol("hm?");
        AddUnitSymbol("hz");
        //AddUnitSymbol("?");
        AddUnitSymbol("j");
        AddUnitSymbol("K");
        AddUnitSymbol("kg");
        AddUnitSymbol("km");
        //AddUnitSymbol("km?");
        //AddUnitSymbol("km?");
        AddUnitSymbol("kn");
        AddUnitSymbol("kpa");
        AddUnitSymbol("kw");
        AddUnitSymbol("kwh");
        AddUnitSymbol("k��");
        //AddUnitSymbol("?");
        AddUnitSymbol("l");
        AddUnitSymbol("lm");
        AddUnitSymbol("lx");
        AddUnitSymbol("m");
        //AddUnitSymbol("m?");
        //AddUnitSymbol("m?");
        AddUnitSymbol("mg");
        AddUnitSymbol("min");
        AddUnitSymbol("ml");
        AddUnitSymbol("mm");
        //AddUnitSymbol("mm?");
        //AddUnitSymbol("mm?");
        AddUnitSymbol("ms");
        AddUnitSymbol("m��");
        //AddUnitSymbol("?");
        AddUnitSymbol("n");
        AddUnitSymbol("nm");
        AddUnitSymbol("ns");
        AddUnitSymbol("pa");
        AddUnitSymbol("ps");
        AddUnitSymbol("rad");
        AddUnitSymbol("s");
        AddUnitSymbol("sr");
        AddUnitSymbol("Sv");
        AddUnitSymbol("T");
        AddUnitSymbol("tex");
        AddUnitSymbol("u");
        AddUnitSymbol("V");
        AddUnitSymbol("w");
        AddUnitSymbol("wb");
        AddUnitSymbol("um");
        AddUnitSymbol("us");
        AddUnitSymbol("��");

        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllUnitSymbols", "��λ�����Ѽ��أ�");
    }
}