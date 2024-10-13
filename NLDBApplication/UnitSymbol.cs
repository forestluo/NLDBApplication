using System.Collections.Generic;

public partial class CoreContent
{
    // 符号正则串
    public static string UNIT_SYMBOL = "";

    public static void ReloadUnitSymbol()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "ReloadUnitSymbol", "加载单位符号！");
        // 设置正则串
        UNIT_SYMBOL = ReloadStringValue("符号", "单位");
        // 记录日志
        Log.LogMessage(UNIT_SYMBOL);
    }

    public static void AddUnitSymbol(string symbol)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('符号', @SqlContent, '单位'); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlContent", symbol);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllUnitSymbols()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "AddAllUnitSymbols", "加载单位符号！");

        AddUnitSymbol("′");
        AddUnitSymbol("″");
        AddUnitSymbol("°");
        AddUnitSymbol("A");
        //AddUnitSymbol("?");
        AddUnitSymbol("Bq");
        AddUnitSymbol("C");
        AddUnitSymbol("℃");
        AddUnitSymbol("cal");
        AddUnitSymbol("cm");
        //AddUnitSymbol("cm?");
        //AddUnitSymbol("cm?");
        AddUnitSymbol("d");
        AddUnitSymbol("db");
        //AddUnitSymbol("?");
        AddUnitSymbol("ev");
        AddUnitSymbol("F");
        AddUnitSymbol("℉");
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
        AddUnitSymbol("kΩ");
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
        AddUnitSymbol("mΩ");
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
        AddUnitSymbol("Ω");

        // 记录日志
        Log.LogMessage("CoreContent", "AddAllUnitSymbols", "单位符号已加载！");
    }
}