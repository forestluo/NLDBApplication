using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class CoreContent
{
    // 货币正则串
    public static string CURRENCY_NAME = "";

    public static void ReloadCurrencyName()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "ReloadCurrencyName", "加载货币！");
        // 设置正则串
        CURRENCY_NAME = ReloadStringValue("实词", "货币");
        // 记录日志
        Log.LogMessage(CURRENCY_NAME);
    }

    public static void AddCurrencyName(string name)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('实词', @SqlContent, '货币'); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlContent", name);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllCurrencyNames()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "AddAllCurrencyNames", "加载货币！");

        AddCurrencyName("阿富汗尼");
        AddCurrencyName("埃斯库多");
        AddCurrencyName("澳大利亚元");
        AddCurrencyName("澳元");
        AddCurrencyName("巴波亚");
        AddCurrencyName("镑");
        AddCurrencyName("比尔");
        AddCurrencyName("比塞塔");
        AddCurrencyName("比索");
        AddCurrencyName("玻利瓦");
        AddCurrencyName("达拉西");
        AddCurrencyName("丹麦克郎");
        AddCurrencyName("迪拉姆");
        AddCurrencyName("第纳尔");
        AddCurrencyName("盾");
        AddCurrencyName("多步拉");
        AddCurrencyName("法郎");
        AddCurrencyName("菲律宾比索");
        AddCurrencyName("分");
        AddCurrencyName("福林");
        AddCurrencyName("港元");
        AddCurrencyName("格查尔");
        AddCurrencyName("古德");
        AddCurrencyName("瓜拉尼");
        AddCurrencyName("韩国元");
        AddCurrencyName("韩元");
        AddCurrencyName("基纳");
        AddCurrencyName("基普");
        AddCurrencyName("加拿大元");
        AddCurrencyName("加元");
        AddCurrencyName("角");
        AddCurrencyName("科多巴");
        AddCurrencyName("科朗");
        AddCurrencyName("克郎");
        AddCurrencyName("克鲁塞罗");
        AddCurrencyName("克瓦查");
        AddCurrencyName("宽札");
        AddCurrencyName("拉菲亚");
        AddCurrencyName("兰特");
        AddCurrencyName("厘");
        AddCurrencyName("里拉");
        AddCurrencyName("里兰吉尼");
        AddCurrencyName("里亚尔");
        AddCurrencyName("利昂");
        AddCurrencyName("列弗");
        AddCurrencyName("列克");
        AddCurrencyName("列伊");
        AddCurrencyName("卢布");
        AddCurrencyName("伦皮拉");
        AddCurrencyName("洛蒂");
        AddCurrencyName("马克");
        AddCurrencyName("梅蒂卡尔");
        AddCurrencyName("美分");
        AddCurrencyName("美元");
        AddCurrencyName("奈拉");
        AddCurrencyName("努扎姆");
        AddCurrencyName("挪威克郎");
        AddCurrencyName("欧元");
        AddCurrencyName("潘加");
        AddCurrencyName("普拉");
        AddCurrencyName("人民币");
        AddCurrencyName("日元");
        AddCurrencyName("瑞典克郎");
        AddCurrencyName("瑞士法郎");
        AddCurrencyName("塞迪");
        AddCurrencyName("苏克雷");
        AddCurrencyName("索尔");
        AddCurrencyName("塔卡");
        AddCurrencyName("塔拉");
        AddCurrencyName("台币");
        AddCurrencyName("泰铢");
        AddCurrencyName("图格里克");
        AddCurrencyName("瓦图");
        AddCurrencyName("乌吉亚");
        AddCurrencyName("先令");
        AddCurrencyName("谢克尔");
        AddCurrencyName("新家坡元");
        AddCurrencyName("新台币");
        AddCurrencyName("新西兰元");
        AddCurrencyName("新元");
        AddCurrencyName("印度卢布");
        AddCurrencyName("英镑");
        AddCurrencyName("元");
        AddCurrencyName("越南盾");
        AddCurrencyName("扎伊尔");
        AddCurrencyName("铢");
        AddCurrencyName("兹罗提");

        // 记录日志
        Log.LogMessage("CoreContent", "AddAllCurrencyNames", "货币已加载！");
    }
}