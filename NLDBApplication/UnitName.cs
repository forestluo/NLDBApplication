using System.Collections.Generic;

public partial class CoreContent
{
    // 单位正则串
    public static string UNIT_NAME = "";

    public static void ReloadUnitName()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "ReloadUnitName", "加载单位！");
        // 设置正则串
        UNIT_NAME = ReloadStringValue("实词", "单位");
        // 记录日志
        Log.LogMessage(UNIT_NAME);
    }

    public static void AddUnitName(string name)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('实词', @SqlContent, '单位'); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlContent", name);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllUnitNames()
    {
        // 记录日志
        Log.LogMessage("CoreContent", "AddAllUnitNames", "加载单位！");

        AddUnitName("埃");
        AddUnitName("安");
        AddUnitName("安培");
        AddUnitName("盎司");
        AddUnitName("巴");
        AddUnitName("百克");
        AddUnitName("百米");
        AddUnitName("百帕");
        AddUnitName("百升");
        AddUnitName("磅");
        AddUnitName("贝");
        AddUnitName("贝尔");
        AddUnitName("标准大气压");
        AddUnitName("长吨");
        AddUnitName("尺");
        AddUnitName("寸");
        AddUnitName("打兰");
        AddUnitName("大气压");
        AddUnitName("担");
        AddUnitName("电子伏特");
        AddUnitName("斗");
        AddUnitName("度");
        AddUnitName("短吨");
        AddUnitName("吨");
        AddUnitName("吨标准煤");
        AddUnitName("吨当量煤");
        AddUnitName("尔格");
        AddUnitName("法拉");
        AddUnitName("费密");
        AddUnitName("分");
        AddUnitName("分贝");
        AddUnitName("分克");
        AddUnitName("分米");
        AddUnitName("分升");
        AddUnitName("分钟");
        AddUnitName("弗隆");
        AddUnitName("伏");
        AddUnitName("伏安");
        AddUnitName("伏特");
        AddUnitName("格令");
        AddUnitName("公斤");
        AddUnitName("公里");
        AddUnitName("公亩");
        AddUnitName("公顷");
        AddUnitName("光年");
        AddUnitName("海里");
        AddUnitName("毫");
        AddUnitName("毫安");
        AddUnitName("毫巴");
        AddUnitName("毫伏");
        AddUnitName("毫克");
        AddUnitName("毫米");
        AddUnitName("毫米汞柱");
        AddUnitName("毫米水柱");
        AddUnitName("毫升");
        AddUnitName("合");
        AddUnitName("赫兹");
        AddUnitName("亨");
        AddUnitName("亨利");
        AddUnitName("弧度");
        AddUnitName("华氏度");
        AddUnitName("及耳");
        AddUnitName("加仑");
        AddUnitName("焦耳");
        AddUnitName("角分");
        AddUnitName("角秒");
        AddUnitName("节");
        AddUnitName("斤");
        AddUnitName("开尔文");
        AddUnitName("开氏度");
        AddUnitName("坎德拉");
        AddUnitName("克");
        AddUnitName("克拉");
        AddUnitName("夸脱");
        AddUnitName("兰氏度");
        AddUnitName("勒");
        AddUnitName("勒克斯");
        AddUnitName("厘");
        AddUnitName("厘克");
        AddUnitName("厘米");
        AddUnitName("厘升");
        AddUnitName("里");
        AddUnitName("立方");
        AddUnitName("立方分米");
        AddUnitName("立方厘米");
        AddUnitName("立方码");
        AddUnitName("立方米");
        AddUnitName("立方英尺");
        AddUnitName("立方英寸");
        AddUnitName("两");
        AddUnitName("量滴");
        AddUnitName("列氏度");
        AddUnitName("流");
        AddUnitName("流明");
        AddUnitName("马力");
        AddUnitName("码");
        AddUnitName("美担");
        AddUnitName("米");
        AddUnitName("米制马力");
        AddUnitName("密耳");
        AddUnitName("秒");
        AddUnitName("摩尔");
        AddUnitName("亩");
        AddUnitName("纳法");
        AddUnitName("纳米");
        AddUnitName("奈培");
        AddUnitName("年");
        AddUnitName("牛顿");
        AddUnitName("欧");
        AddUnitName("欧姆");
        AddUnitName("帕");
        AddUnitName("帕斯卡");
        AddUnitName("配克");
        AddUnitName("皮法");
        AddUnitName("皮米");
        AddUnitName("品脱");
        AddUnitName("平方尺");
        AddUnitName("平方寸");
        AddUnitName("平方分");
        AddUnitName("平方分米");
        AddUnitName("平方公里");
        AddUnitName("平方毫米");
        AddUnitName("平方厘");
        AddUnitName("平方厘米");
        AddUnitName("平方里");
        AddUnitName("平方码");
        AddUnitName("平方米");
        AddUnitName("平方千米");
        AddUnitName("平方英尺");
        AddUnitName("平方英寸");
        AddUnitName("平方英里");
        AddUnitName("平方丈");
        AddUnitName("蒲式耳");
        AddUnitName("千伏");
        AddUnitName("千卡");
        AddUnitName("千克");
        AddUnitName("千米");
        AddUnitName("千帕");
        AddUnitName("千升");
        AddUnitName("千瓦");
        AddUnitName("钱");
        AddUnitName("顷");
        AddUnitName("球面度");
        AddUnitName("人");
        AddUnitName("日");
        AddUnitName("勺");
        AddUnitName("摄氏度");
        AddUnitName("升");
        AddUnitName("十克");
        AddUnitName("十米");
        AddUnitName("十升");
        AddUnitName("石");
        AddUnitName("时");
        AddUnitName("市尺");
        AddUnitName("市斤");
        AddUnitName("市里");
        AddUnitName("市亩");
        AddUnitName("市升");
        AddUnitName("特");
        AddUnitName("特克斯");
        AddUnitName("特斯拉");
        AddUnitName("天");
        AddUnitName("天文单位");
        AddUnitName("桶");
        AddUnitName("瓦");
        AddUnitName("万伏");
        AddUnitName("微法");
        AddUnitName("微克");
        AddUnitName("微米");
        AddUnitName("微升");
        AddUnitName("韦");
        AddUnitName("韦伯");
        AddUnitName("西");
        AddUnitName("西门子");
        AddUnitName("像素");
        AddUnitName("小时");
        AddUnitName("英磅");
        AddUnitName("英尺");
        AddUnitName("英寸");
        AddUnitName("英担");
        AddUnitName("英里");
        AddUnitName("英亩");
        AddUnitName("英钱");
        AddUnitName("英石");
        AddUnitName("英寻");
        AddUnitName("英制马力");
        AddUnitName("月");
        AddUnitName("丈");
        AddUnitName("兆瓦");
        AddUnitName("转");

        // 记录日志
        Log.LogMessage("CoreContent", "AddAllUnitNames", "单位已加载！");
    }
}