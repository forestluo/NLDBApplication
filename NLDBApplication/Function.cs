using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class CoreContent
{
    public static void AddFunction(string value, string attribute)
    {
        // 指令字符串
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) VALUES ('功能词', @SqlContent, @SqlAttribute); ";
        // 设置参数
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // 加入参数
        parameters.Add("SqlContent", value);
        parameters.Add("SqlAttribute", attribute);
        // 执行指令
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllFunctions()
    {
        AddFunction("照着", "介词");
        AddFunction("者", "助词");
        AddFunction("这", "代词");
        AddFunction("这儿", "代词");
        AddFunction("这个", "代词");
        AddFunction("这会儿", "代词");
        AddFunction("这里", "代词");
        AddFunction("这么", "代词");
        AddFunction("这么点儿", "代词");
        AddFunction("这么些", "代词");
        AddFunction("这么样", "代词");
        AddFunction("这么着", "代词");
        AddFunction("这些", "代词");
        AddFunction("这些个", "代词");
        AddFunction("这样", "代词");
        AddFunction("这阵儿", "代词");
        AddFunction("这阵子", "代词");
        AddFunction("真", "副词");
        AddFunction("真的", "副词");
        AddFunction("真个", "副词");
        AddFunction("真是", "副词");
        AddFunction("真正", "副词");
        AddFunction("正", "副词");
        AddFunction("正好", "副词");
        AddFunction("正巧", "副词");
        AddFunction("正在", "副词");
        AddFunction("之", "助词");
        AddFunction("之", "代词");
        AddFunction("之后", "方位词");
        AddFunction("之间", "方位词");
        AddFunction("之内", "方位词");
        AddFunction("之前", "方位词");
        AddFunction("之上", "方位词");
        AddFunction("之外", "方位词");
        AddFunction("之下", "方位词");
        AddFunction("之中", "方位词");
        AddFunction("直", "副词");
        AddFunction("只", "副词");
        AddFunction("只顾", "副词");
        AddFunction("只管", "副词");
        AddFunction("只好", "副词");
        AddFunction("只是", "连词");
        AddFunction("只是", "副词");
        AddFunction("只消", "副词");
        AddFunction("只要", "连词");
        AddFunction("只有", "连词");
        AddFunction("只有", "副词");
        AddFunction("至", "副词");
        AddFunction("至", "连词");
        AddFunction("至多", "副词");
        AddFunction("至今", "副词");
        AddFunction("至少", "副词");
        AddFunction("至为", "副词");
        AddFunction("至于", "连词");
        AddFunction("致", "连词");
        AddFunction("致使", "连词");
        AddFunction("中", "方位词");
        AddFunction("终", "副词");
        AddFunction("终归", "副词");
        AddFunction("终究", "副词");
        AddFunction("终久", "副词");
        AddFunction("终日", "副词");
        AddFunction("终于", "副词");
        AddFunction("重新", "副词");
        AddFunction("重行", "副词");
        AddFunction("骤然", "副词");
        AddFunction("逐步", "副词");
        AddFunction("逐个", "副词");
        AddFunction("逐渐", "副词");
        AddFunction("逐年", "副词");
        AddFunction("逐日", "副词");
        AddFunction("逐一", "副词");
        AddFunction("逐月", "副词");
        AddFunction("专", "副词");
        AddFunction("专门", "副词");
        AddFunction("准", "副词");
        AddFunction("准保", "副词");
        AddFunction("着", "助词");
        AddFunction("着呐", "助词");
        AddFunction("着呢", "助词");
        AddFunction("着实", "副词");
        AddFunction("自", "代词");
        AddFunction("自", "介词");
        AddFunction("自从", "介词");
        AddFunction("自个", "代词");
        AddFunction("自己", "代词");
        AddFunction("自来", "副词");
        AddFunction("自然", "连词");
        AddFunction("自然", "副词");
        AddFunction("总", "副词");
        AddFunction("总共", "副词");
        AddFunction("总归", "副词");
        AddFunction("总是", "副词");
        AddFunction("总算", "副词");
        AddFunction("总之", "连词");
        AddFunction("纵", "连词");
        AddFunction("纵令", "连词");
        AddFunction("纵然", "连词");
        AddFunction("纵使", "连词");
        AddFunction("足", "副词");
        AddFunction("足见", "连词");
        AddFunction("足足", "副词");
        AddFunction("最", "副词");
        AddFunction("最多", "副词");
        AddFunction("最好", "副词");
        AddFunction("最少", "副词");
        AddFunction("最为", "副词");
        AddFunction("遵照", "介词");
        AddFunction("无须", "副词");
        AddFunction("无庸", "副词");
        AddFunction("无足", "副词");
        AddFunction("毋", "副词");
        AddFunction("毋宁", "连词");
        AddFunction("毋庸", "副词");
        AddFunction("兀地", "副词");
        AddFunction("勿", "副词");
        AddFunction("勿宁", "连词");
        AddFunction("务", "副词");
        AddFunction("务必", "副词");
        AddFunction("务须", "副词");
        AddFunction("悉", "副词");
        AddFunction("稀", "副词");
        AddFunction("下", "方位词");
        AddFunction("先", "副词");
        AddFunction("先后", "副词");
        AddFunction("显然", "副词");
        AddFunction("险些", "副词");
        AddFunction("相当", "副词");
        AddFunction("相继", "副词");
        AddFunction("想必", "副词");
        AddFunction("向", "介词");
        AddFunction("向来", "副词");
        AddFunction("些微", "副词");
        AddFunction("新近", "副词");
        AddFunction("兴许", "副词");
        AddFunction("行将", "副词");
        AddFunction("幸而", "副词");
        AddFunction("幸好", "副词");
        AddFunction("幸亏", "副词");
        AddFunction("休", "副词");
        AddFunction("许", "副词");
        AddFunction("旋即", "副词");
        AddFunction("迅即", "副词");
        AddFunction("呀", "助词");
        AddFunction("俨然", "副词");
        AddFunction("要", "连词");
        AddFunction("要", "副词");
        AddFunction("要不", "连词");
        AddFunction("要不然", "连词");
        AddFunction("要不是", "连词");
        AddFunction("要就", "连词");
        AddFunction("要就是", "连词");
        AddFunction("要么", "连词");
        AddFunction("要末", "连词");
        AddFunction("要是", "连词");
        AddFunction("也", "助词");
        AddFunction("也", "副词");
        AddFunction("也罢", "助词");
        AddFunction("也好", "助词");
        AddFunction("也许", "副词");
        AddFunction("业已", "副词");
        AddFunction("一", "副词");
        AddFunction("一边", "连词");
        AddFunction("一并", "副词");
        AddFunction("一旦", "副词");
        AddFunction("一道", "副词");
        AddFunction("一定", "副词");
        AddFunction("一度", "副词");
        AddFunction("一发", "副词");
        AddFunction("一方面", "连词");
        AddFunction("一概", "副词");
        AddFunction("一共", "副词");
        AddFunction("一经", "连词");
        AddFunction("一块儿", "副词");
        AddFunction("一来", "连词");
        AddFunction("一连", "副词");
        AddFunction("一溜烟", "副词");
        AddFunction("一律", "副词");
        AddFunction("一面", "连词");
        AddFunction("一齐", "副词");
        AddFunction("一起", "副词");
        AddFunction("一气", "副词");
        AddFunction("一切", "代词");
        AddFunction("一时", "副词");
        AddFunction("一同", "副词");
        AddFunction("一头", "连词");
        AddFunction("一味", "副词");
        AddFunction("一向", "副词");
        AddFunction("一一", "副词");
        AddFunction("一再", "副词");
        AddFunction("一则", "连词");
        AddFunction("一眨眼", "副词");
        AddFunction("一者", "连词");
        AddFunction("一直", "副词");
        AddFunction("一准", "副词");
        AddFunction("一总", "副词");
        AddFunction("依", "介词");
        AddFunction("依次", "副词");
        AddFunction("依照", "介词");
        AddFunction("已", "副词");
        AddFunction("已经", "副词");
        AddFunction("以", "介词");
        AddFunction("以", "连词");
        AddFunction("以便", "连词");
        AddFunction("以便于", "连词");
        AddFunction("以后", "方位词");
        AddFunction("以及", "连词");
        AddFunction("以免", "连词");
        AddFunction("以内", "方位词");
        AddFunction("以前", "方位词");
        AddFunction("以上", "方位词");
        AddFunction("以外", "方位词");
        AddFunction("以下", "方位词");
        AddFunction("以至", "连词");
        AddFunction("以致", "连词");
        AddFunction("异常", "副词");
        AddFunction("抑或", "连词");
        AddFunction("益", "副词");
        AddFunction("益发", "副词");
        AddFunction("因", "连词");
        AddFunction("因", "介词");
        AddFunction("因此", "连词");
        AddFunction("因而", "连词");
        AddFunction("因为", "介词");
        AddFunction("因为", "连词");
        AddFunction("硬", "副词");
        AddFunction("硬是", "副词");
        AddFunction("永", "副词");
        AddFunction("永远", "副词");
        AddFunction("尤", "副词");
        AddFunction("尤其", "副词");
        AddFunction("尤其是", "连词");
        AddFunction("尤为", "副词");
        AddFunction("由", "介词");
        AddFunction("由于", "连词");
        AddFunction("由于", "介词");
        AddFunction("犹", "副词");
        AddFunction("犹", "连词");
        AddFunction("有的", "代词");
        AddFunction("有点儿", "副词");
        AddFunction("有时", "副词");
        AddFunction("有些", "副词");
        AddFunction("有些", "代词");
        AddFunction("又", "副词");
        AddFunction("于", "介词");
        AddFunction("于是", "连词");
        AddFunction("与", "连词");
        AddFunction("与", "介词");
        AddFunction("与其", "连词");
        AddFunction("预先", "副词");
        AddFunction("愈", "副词");
        AddFunction("愈加", "副词");
        AddFunction("愈为", "副词");
        AddFunction("愈益", "副词");
        AddFunction("原", "副词");
        AddFunction("原来", "副词");
        AddFunction("远", "副词");
        AddFunction("约", "副词");
        AddFunction("约摸", "副词");
        AddFunction("约莫", "副词");
        AddFunction("越", "副词");
        AddFunction("越发", "副词");
        AddFunction("越加", "副词");
        AddFunction("再", "副词");
        AddFunction("再不", "连词");
        AddFunction("再不然", "连词");
        AddFunction("再次", "副词");
        AddFunction("再度", "副词");
        AddFunction("再三", "副词");
        AddFunction("再说", "连词");
        AddFunction("再则", "连词");
        AddFunction("再者", "连词");
        AddFunction("在", "副词");
        AddFunction("在", "介词");
        AddFunction("在下", "代词");
        AddFunction("切", "副词");
        AddFunction("且", "连词");
        AddFunction("且", "副词");
        AddFunction("顷刻", "副词");
        AddFunction("穷", "副词");
        AddFunction("全", "副词");
        AddFunction("全都", "副词");
        AddFunction("全盘", "副词");
        AddFunction("全然", "副词");
        AddFunction("权", "副词");
        AddFunction("权且", "副词");
        AddFunction("却", "副词");
        AddFunction("确", "副词");
        AddFunction("确乎", "副词");
        AddFunction("确然", "副词");
        AddFunction("确实", "副词");
        AddFunction("然", "连词");
        AddFunction("然而", "连词");
        AddFunction("然后", "连词");
        AddFunction("让", "介词");
        AddFunction("人家", "代词");
        AddFunction("任", "连词");
        AddFunction("任", "介词");
        AddFunction("任何", "代词");
        AddFunction("任凭", "连词");
        AddFunction("任凭", "介词");
        AddFunction("仍旧", "副词");
        AddFunction("仍然", "副词");
        AddFunction("日见", "副词");
        AddFunction("日渐", "副词");
        AddFunction("日趋", "副词");
        AddFunction("日日", "副词");
        AddFunction("日益", "副词");
        AddFunction("日臻", "副词");
        AddFunction("如", "连词");
        AddFunction("如此", "代词");
        AddFunction("如果", "连词");
        AddFunction("如何", "代词");
        AddFunction("如若", "连词");
        AddFunction("若", "连词");
        AddFunction("若是", "连词");
        AddFunction("啥", "代词");
        AddFunction("煞", "副词");
        AddFunction("霎时", "副词");
        AddFunction("上", "方位词");
        AddFunction("尚", "副词");
        AddFunction("尚", "连词");
        AddFunction("尚且", "连词");
        AddFunction("稍", "副词");
        AddFunction("稍顷", "副词");
        AddFunction("稍稍", "副词");
        AddFunction("稍微", "副词");
        AddFunction("稍为", "副词");
        AddFunction("稍许", "副词");
        AddFunction("设", "连词");
        AddFunction("设若", "连词");
        AddFunction("设使", "连词");
        AddFunction("深", "副词");
        AddFunction("深为", "副词");
        AddFunction("甚", "副词");
        AddFunction("甚而", "连词");
        AddFunction("甚而至于", "连词");
        AddFunction("甚为", "副词");
        AddFunction("甚至", "连词");
        AddFunction("甚至于", "连词");
        AddFunction("生", "副词");
        AddFunction("省得", "连词");
        AddFunction("十分", "副词");
        AddFunction("什么", "代词");
        AddFunction("时", "副词");
        AddFunction("时常", "副词");
        AddFunction("时而", "副词");
        AddFunction("时刻", "副词");
        AddFunction("时时", "副词");
        AddFunction("实", "副词");
        AddFunction("实在", "副词");
        AddFunction("始终", "副词");
        AddFunction("势必", "副词");
        AddFunction("咱", "代词");
        AddFunction("咱们", "代词");
        AddFunction("暂", "副词");
        AddFunction("暂且", "副词");
        AddFunction("早", "副词");
        AddFunction("早就", "副词");
        AddFunction("早晚", "副词");
        AddFunction("早已", "副词");
        AddFunction("早早", "副词");
        AddFunction("则", "副词");
        AddFunction("则已", "助词");
        AddFunction("贼", "副词");
        AddFunction("怎", "代词");
        AddFunction("怎么", "代词");
        AddFunction("怎么样", "代词");
        AddFunction("怎么着", "代词");
        AddFunction("怎奈", "连词");
        AddFunction("怎样", "代词");
        AddFunction("曾", "副词");
        AddFunction("曾经", "副词");
        AddFunction("乍", "副词");
        AddFunction("照", "介词");
        AddFunction("是", "副词");
        AddFunction("是", "连词");
        AddFunction("首先", "副词");
        AddFunction("倏然", "副词");
        AddFunction("殊", "副词");
        AddFunction("谁", "代词");
        AddFunction("谁知道", "连词");
        AddFunction("顺", "介词");
        AddFunction("丝毫", "副词");
        AddFunction("死", "副词");
        AddFunction("死死", "副词");
        AddFunction("似", "副词");
        AddFunction("似乎", "副词");
        AddFunction("素常", "副词");
        AddFunction("素来", "副词");
        AddFunction("速速", "副词");
        AddFunction("算了", "助词");
        AddFunction("算是", "副词");
        AddFunction("虽", "连词");
        AddFunction("虽然", "连词");
        AddFunction("虽说", "连词");
        AddFunction("虽说是", "连词");
        AddFunction("虽则", "连词");
        AddFunction("随", "连词");
        AddFunction("随便", "连词");
        AddFunction("随后", "副词");
        AddFunction("随即", "副词");
        AddFunction("随时", "副词");
        AddFunction("所", "助词");
        AddFunction("所以", "连词");
        AddFunction("索性", "副词");
        AddFunction("他", "代词");
        AddFunction("他们", "代词");
        AddFunction("它", "代词");
        AddFunction("它们", "代词");
        AddFunction("太", "副词");
        AddFunction("倘或", "连词");
        AddFunction("倘若", "连词");
        AddFunction("倘使", "连词");
        AddFunction("忒", "副词");
        AddFunction("特", "副词");
        AddFunction("特别", "副词");
        AddFunction("特别是", "连词");
        AddFunction("替", "介词");
        AddFunction("挺", "副词");
        AddFunction("通", "副词");
        AddFunction("通常", "副词");
        AddFunction("通共", "副词");
        AddFunction("通过", "介词");
        AddFunction("通盘", "副词");
        AddFunction("通通", "副词");
        AddFunction("同", "副词");
        AddFunction("同", "介词");
        AddFunction("同", "连词");
        AddFunction("统", "副词");
        AddFunction("统共", "副词");
        AddFunction("统统", "副词");
        AddFunction("透", "副词");
        AddFunction("透顶", "副词");
        AddFunction("突", "副词");
        AddFunction("徒", "副词");
        AddFunction("徒然", "副词");
        AddFunction("哇", "助词");
        AddFunction("外", "方位词");
        AddFunction("万", "副词");
        AddFunction("万般", "副词");
        AddFunction("万分", "副词");
        AddFunction("万万", "副词");
        AddFunction("万一", "连词");
        AddFunction("往", "介词");
        AddFunction("往往", "副词");
        AddFunction("微微", "副词");
        AddFunction("为", "介词");
        AddFunction("为的是", "连词");
        AddFunction("为了", "连词");
        AddFunction("唯", "副词");
        AddFunction("唯独", "连词");
        AddFunction("唯独", "副词");
        AddFunction("唯其", "连词");
        AddFunction("唯有", "连词");
        AddFunction("唯有", "副词");
        AddFunction("惟", "副词");
        AddFunction("惟独", "连词");
        AddFunction("惟独", "副词");
        AddFunction("惟其", "连词");
        AddFunction("惟有", "连词");
        AddFunction("惟有", "副词");
        AddFunction("委", "副词");
        AddFunction("委实", "副词");
        AddFunction("未", "副词");
        AddFunction("未必", "副词");
        AddFunction("未便", "副词");
        AddFunction("未尝", "副词");
        AddFunction("未免", "副词");
        AddFunction("未始", "副词");
        AddFunction("未曾", "副词");
        AddFunction("我", "代词");
        AddFunction("我们", "代词");
        AddFunction("无比", "副词");
        AddFunction("无不", "副词");
        AddFunction("就令", "连词");
        AddFunction("就使", "连词");
        AddFunction("就是", "助词");
        AddFunction("就是", "连词");
        AddFunction("就是", "副词");
        AddFunction("就是了", "助词");
        AddFunction("就算", "连词");
        AddFunction("就要", "副词");
        AddFunction("居然", "副词");
        AddFunction("俱", "副词");
        AddFunction("据", "介词");
        AddFunction("距", "介词");
        AddFunction("距离", "介词");
        AddFunction("遽然", "副词");
        AddFunction("决", "副词");
        AddFunction("决计", "副词");
        AddFunction("绝", "副词");
        AddFunction("绝顶", "副词");
        AddFunction("绝对", "副词");
        AddFunction("看", "助词");
        AddFunction("看样子", "连词");
        AddFunction("可", "连词");
        AddFunction("可", "副词");
        AddFunction("可见", "连词");
        AddFunction("可能", "副词");
        AddFunction("可是", "连词");
        AddFunction("恐怕", "副词");
        AddFunction("酷", "副词");
        AddFunction("快", "副词");
        AddFunction("快要", "副词");
        AddFunction("况", "连词");
        AddFunction("况且", "连词");
        AddFunction("亏", "副词");
        AddFunction("啦", "助词");
        AddFunction("来", "衬字词");
        AddFunction("来", "助词");
        AddFunction("来的", "助词");
        AddFunction("来着", "助词");
        AddFunction("老", "副词");
        AddFunction("老是", "副词");
        AddFunction("叻", "助词");
        AddFunction("嘞", "助词");
        AddFunction("冷不丁", "副词");
        AddFunction("冷不防", "副词");
        AddFunction("冷孤丁", "副词");
        AddFunction("愣", "副词");
        AddFunction("离", "介词");
        AddFunction("里", "方位词");
        AddFunction("哩", "助词");
        AddFunction("历来", "副词");
        AddFunction("立即", "副词");
        AddFunction("立刻", "副词");
        AddFunction("立时", "副词");
        AddFunction("例如", "连词");
        AddFunction("连", "副词");
        AddFunction("连", "介词");
        AddFunction("连连", "副词");
        AddFunction("连着", "副词");
        AddFunction("良", "副词");
        AddFunction("聊", "副词");
        AddFunction("了", "助词");
        AddFunction("了", "副词");
        AddFunction("咧", "助词");
        AddFunction("临", "介词");
        AddFunction("另", "代词");
        AddFunction("另", "副词");
        AddFunction("另外", "代词");
        AddFunction("另外", "副词");
        AddFunction("另外", "连词");
        AddFunction("另一方面", "连词");
        AddFunction("咯", "助词");
        AddFunction("喽", "助词");
        AddFunction("陆续", "副词");
        AddFunction("屡", "副词");
        AddFunction("屡次", "副词");
        AddFunction("屡屡", "副词");
        AddFunction("略", "副词");
        AddFunction("略略", "副词");
        AddFunction("略微", "副词");
        AddFunction("略为", "副词");
        AddFunction("论", "介词");
        AddFunction("啰", "助词");
        AddFunction("马上", "副词");
        AddFunction("吗", "助词");
        AddFunction("嘛", "助词");
        AddFunction("蛮", "副词");
        AddFunction("满", "副词");
        AddFunction("满处", "副词");
        AddFunction("无从", "副词");
        AddFunction("无端", "副词");
        AddFunction("无非", "副词");
        AddFunction("无怪", "连词");
        AddFunction("无怪乎", "连词");
        AddFunction("无怪于", "连词");
        AddFunction("无论", "连词");
        AddFunction("无奈", "连词");
        AddFunction("无宁", "连词");
        AddFunction("无日", "副词");
        AddFunction("无上", "副词");
        AddFunction("无时", "副词");
        AddFunction("慢说", "连词");
        AddFunction("慢说是", "连词");
        AddFunction("么", "助词");
        AddFunction("么", "衬字词");
        AddFunction("没", "副词");
        AddFunction("没有", "副词");
        AddFunction("每", "代词");
        AddFunction("每每", "副词");
        AddFunction("们", "助词");
        AddFunction("猛", "副词");
        AddFunction("猛不防", "副词");
        AddFunction("猛地", "副词");
        AddFunction("猛然", "副词");
        AddFunction("免得", "连词");
        AddFunction("明", "副词");
        AddFunction("明明", "副词");
        AddFunction("莫", "副词");
        AddFunction("莫非", "副词");
        AddFunction("莫非是", "副词");
        AddFunction("莫如", "副词");
        AddFunction("莫若", "副词");
        AddFunction("莫说", "连词");
        AddFunction("莫说是", "连词");
        AddFunction("蓦地", "副词");
        AddFunction("蓦然", "副词");
        AddFunction("某", "代词");
        AddFunction("拿", "介词");
        AddFunction("哪", "助词");
        AddFunction("哪儿", "代词");
        AddFunction("哪会儿", "代词");
        AddFunction("哪里", "代词");
        AddFunction("哪怕", "连词");
        AddFunction("哪怕是", "连词");
        AddFunction("内", "方位词");
        AddFunction("那", "代词");
        AddFunction("那", "连词");
        AddFunction("那儿", "代词");
        AddFunction("那个", "代词");
        AddFunction("那会儿", "代词");
        AddFunction("那里", "代词");
        AddFunction("那么", "代词");
        AddFunction("那么", "连词");
        AddFunction("那么点儿", "代词");
        AddFunction("那么些", "代词");
        AddFunction("那么样", "代词");
        AddFunction("那么着", "代词");
        AddFunction("那末", "连词");
        AddFunction("那些", "代词");
        AddFunction("那些个", "代词");
        AddFunction("那样", "代词");
        AddFunction("那阵儿", "代词");
        AddFunction("那阵子", "代词");
        AddFunction("乃", "代词");
        AddFunction("乃", "副词");
        AddFunction("乃至", "连词");
        AddFunction("乃至于", "连词");
        AddFunction("难道", "副词");
        AddFunction("难怪", "副词");
        AddFunction("难免", "副词");
        AddFunction("呐", "助词");
        AddFunction("呢", "助词");
        AddFunction("你", "代词");
        AddFunction("你们", "代词");
        AddFunction("您", "代词");
        AddFunction("宁", "副词");
        AddFunction("宁可", "副词");
        AddFunction("宁肯", "副词");
        AddFunction("宁愿", "副词");
        AddFunction("噢", "助词");
        AddFunction("哦", "助词");
        AddFunction("偶", "副词");
        AddFunction("偶而", "副词");
        AddFunction("偶尔", "副词");
        AddFunction("偶或", "副词");
        AddFunction("怕", "副词");
        AddFunction("怕是", "副词");
        AddFunction("譬如", "连词");
        AddFunction("偏", "副词");
        AddFunction("偏偏", "副词");
        AddFunction("偏巧", "副词");
        AddFunction("频", "副词");
        AddFunction("频频", "副词");
        AddFunction("凭", "介词");
        AddFunction("凭", "连词");
        AddFunction("凭着", "介词");
        AddFunction("颇", "副词");
        AddFunction("颇为", "副词");
        AddFunction("齐", "副词");
        AddFunction("其", "代词");
        AddFunction("其实", "副词");
        AddFunction("其他", "代词");
        AddFunction("其余", "代词");
        AddFunction("岂", "副词");
        AddFunction("岂但", "连词");
        AddFunction("起初", "副词");
        AddFunction("起码", "副词");
        AddFunction("起先", "副词");
        AddFunction("恰好", "副词");
        AddFunction("恰恰", "副词");
        AddFunction("恰巧", "副词");
        AddFunction("俄尔", "副词");
        AddFunction("而", "连词");
        AddFunction("而后", "连词");
        AddFunction("而况", "连词");
        AddFunction("而且", "连词");
        AddFunction("而已", "助词");
        AddFunction("尔后", "连词");
        AddFunction("凡", "副词");
        AddFunction("凡是", "副词");
        AddFunction("反", "副词");
        AddFunction("反倒", "副词");
        AddFunction("反而", "副词");
        AddFunction("反复", "副词");
        AddFunction("反正", "副词");
        AddFunction("反之", "连词");
        AddFunction("方", "副词");
        AddFunction("方才", "副词");
        AddFunction("仿佛", "副词");
        AddFunction("非", "副词");
        AddFunction("非常", "副词");
        AddFunction("非但", "连词");
        AddFunction("非特", "连词");
        AddFunction("分外", "副词");
        AddFunction("否", "助词");
        AddFunction("否则", "连词");
        AddFunction("该", "代词");
        AddFunction("该", "副词");
        AddFunction("概", "副词");
        AddFunction("赶", "介词");
        AddFunction("赶紧", "副词");
        AddFunction("敢情", "副词");
        AddFunction("敢是", "副词");
        AddFunction("刚", "副词");
        AddFunction("刚刚", "副词");
        AddFunction("刚好", "副词");
        AddFunction("刚巧", "副词");
        AddFunction("高低", "副词");
        AddFunction("格外", "副词");
        AddFunction("各", "代词");
        AddFunction("各自", "代词");
        AddFunction("给", "介词");
        AddFunction("根本", "副词");
        AddFunction("根据", "介词");
        AddFunction("跟", "介词");
        AddFunction("跟", "连词");
        AddFunction("更", "副词");
        AddFunction("更加", "副词");
        AddFunction("更其", "副词");
        AddFunction("更为", "副词");
        AddFunction("共", "副词");
        AddFunction("共同", "副词");
        AddFunction("共总", "副词");
        AddFunction("够", "副词");
        AddFunction("姑", "副词");
        AddFunction("姑且", "副词");
        AddFunction("固", "副词");
        AddFunction("固然", "连词");
        AddFunction("故", "连词");
        AddFunction("怪", "副词");
        AddFunction("怪不得", "连词");
        AddFunction("关于", "介词");
        AddFunction("管", "介词");
        AddFunction("管", "连词");
        AddFunction("光", "副词");
        AddFunction("光景", "连词");
        AddFunction("归", "介词");
        AddFunction("果", "副词");
        AddFunction("果然", "副词");
        AddFunction("果然", "连词");
        AddFunction("果真", "连词");
        AddFunction("果真", "副词");
        AddFunction("过", "副词");
        AddFunction("过", "助词");
        AddFunction("过分", "副词");
        AddFunction("过于", "副词");
        AddFunction("毫", "副词");
        AddFunction("好", "副词");
        AddFunction("好", "连词");
        AddFunction("好不", "副词");
        AddFunction("好歹", "副词");
        AddFunction("好了", "助词");
        AddFunction("好生", "副词");
        AddFunction("好像", "副词");
        AddFunction("好在", "副词");
        AddFunction("呵", "助词");
        AddFunction("何", "代词");
        AddFunction("何必", "副词");
        AddFunction("何不", "副词");
        AddFunction("何尝", "副词");
        AddFunction("何等", "副词");
        AddFunction("何苦", "副词");
        AddFunction("何况", "连词");
        AddFunction("何其", "副词");
        AddFunction("何曾", "副词");
        AddFunction("和", "连词");
        AddFunction("和", "介词");
        AddFunction("很", "连词");
        AddFunction("很", "副词");
        AddFunction("千万", "副词");
        AddFunction("前", "方位词");
        AddFunction("啊", "助词");
        AddFunction("按", "介词");
        AddFunction("按照", "介词");
        AddFunction("叭", "助词");
        AddFunction("吧", "助词");
        AddFunction("把", "助词");
        AddFunction("把", "介词");
        AddFunction("横竖", "副词");
        AddFunction("后", "方位词");
        AddFunction("忽", "副词");
        AddFunction("忽地", "副词");
        AddFunction("忽而", "副词");
        AddFunction("忽然", "副词");
        AddFunction("还", "副词");
        AddFunction("还是", "副词");
        AddFunction("还是", "连词");
        AddFunction("恍", "副词");
        AddFunction("活", "副词");
        AddFunction("或", "副词");
        AddFunction("或", "连词");
        AddFunction("或是", "连词");
        AddFunction("或许", "副词");
        AddFunction("或则", "连词");
        AddFunction("或者", "连词");
        AddFunction("霍地", "副词");
        AddFunction("霍然", "副词");
        AddFunction("基本", "副词");
        AddFunction("基本上", "副词");
        AddFunction("基于", "介词");
        AddFunction("及", "连词");
        AddFunction("及至", "介词");
        AddFunction("即", "副词");
        AddFunction("即", "连词");
        AddFunction("即便", "连词");
        AddFunction("即将", "副词");
        AddFunction("即刻", "副词");
        AddFunction("即使", "连词");
        AddFunction("极", "副词");
        AddFunction("极度", "副词");
        AddFunction("极端", "副词");
        AddFunction("极其", "副词");
        AddFunction("极为", "副词");
        AddFunction("籍", "介词");
        AddFunction("几", "副词");
        AddFunction("几乎", "副词");
        AddFunction("己", "代词");
        AddFunction("既", "连词");
        AddFunction("既而", "副词");
        AddFunction("既然", "连词");
        AddFunction("既是", "连词");
        AddFunction("继而", "副词");
        AddFunction("暨", "连词");
        AddFunction("加上", "连词");
        AddFunction("加以", "连词");
        AddFunction("加之", "连词");
        AddFunction("假如", "连词");
        AddFunction("假若", "连词");
        AddFunction("假使", "连词");
        AddFunction("间", "方位词");
        AddFunction("间或", "副词");
        AddFunction("简直", "副词");
        AddFunction("渐", "副词");
        AddFunction("渐次", "副词");
        AddFunction("渐渐", "副词");
        AddFunction("鉴于", "连词");
        AddFunction("鉴于", "介词");
        AddFunction("将", "副词");
        AddFunction("将次", "副词");
        AddFunction("将要", "副词");
        AddFunction("叫", "介词");
        AddFunction("较", "副词");
        AddFunction("较", "介词");
        AddFunction("较比", "副词");
        AddFunction("较为", "副词");
        AddFunction("教", "介词");
        AddFunction("皆", "副词");
        AddFunction("接连", "副词");
        AddFunction("结果", "连词");
        AddFunction("她", "代词");
        AddFunction("她们", "代词");
        AddFunction("借", "介词");
        AddFunction("仅", "副词");
        AddFunction("仅仅", "副词");
        AddFunction("尽", "副词");
        AddFunction("尽", "介词");
        AddFunction("尽管", "副词");
        AddFunction("尽管", "连词");
        AddFunction("尽量", "副词");
        AddFunction("进而", "连词");
        AddFunction("经", "介词");
        AddFunction("经常", "副词");
        AddFunction("经过", "介词");
        AddFunction("净", "副词");
        AddFunction("竟", "副词");
        AddFunction("竟然", "副词");
        AddFunction("竟自", "副词");
        AddFunction("究竟", "副词");
        AddFunction("久久", "副词");
        AddFunction("就", "副词");
        AddFunction("就", "介词");
        AddFunction("罢", "助词");
        AddFunction("罢了", "助词");
        AddFunction("呗", "助词");
        AddFunction("备", "副词");
        AddFunction("备加", "副词");
        AddFunction("倍", "副词");
        AddFunction("倍儿", "副词");
        AddFunction("倍加", "副词");
        AddFunction("被", "介词");
        AddFunction("本", "副词");
        AddFunction("本", "介词");
        AddFunction("本来", "副词");
        AddFunction("本人", "代词");
        AddFunction("本着", "介词");
        AddFunction("甭", "副词");
        AddFunction("甭说", "连词");
        AddFunction("甭说是", "连词");
        AddFunction("比", "介词");
        AddFunction("比方", "连词");
        AddFunction("比较", "副词");
        AddFunction("比如", "连词");
        AddFunction("彼", "副词");
        AddFunction("彼此", "代词");
        AddFunction("必", "副词");
        AddFunction("必定", "副词");
        AddFunction("必然", "副词");
        AddFunction("必须", "副词");
        AddFunction("边", "副词");
        AddFunction("便", "连词");
        AddFunction("便", "副词");
        AddFunction("便是", "连词");
        AddFunction("别", "副词");
        AddFunction("别管", "连词");
        AddFunction("别看", "连词");
        AddFunction("别人", "代词");
        AddFunction("别说", "连词");
        AddFunction("别说是", "连词");
        AddFunction("并", "副词");
        AddFunction("并", "连词");
        AddFunction("并且", "连词");
        AddFunction("不", "副词");
        AddFunction("不必", "副词");
        AddFunction("不便", "副词");
        AddFunction("不成", "助词");
        AddFunction("不大", "副词");
        AddFunction("不单", "连词");
        AddFunction("不单是", "连词");
        AddFunction("不但", "连词");
        AddFunction("不独", "连词");
        AddFunction("不断", "副词");
        AddFunction("不妨", "副词");
        AddFunction("不管", "连词");
        AddFunction("不光", "连词");
        AddFunction("不过", "副词");
        AddFunction("不仅", "连词");
        AddFunction("不拘", "连词");
        AddFunction("不堪", "副词");
        AddFunction("不愧", "副词");
        AddFunction("不料", "连词");
        AddFunction("不论", "连词");
        AddFunction("不免", "副词");
        AddFunction("不然", "连词");
        AddFunction("不如", "连词");
        AddFunction("不胜", "副词");
        AddFunction("不时", "副词");
        AddFunction("不说", "连词");
        AddFunction("不特", "连词");
        AddFunction("不惟", "连词");
        AddFunction("不问", "连词");
        AddFunction("不消", "副词");
        AddFunction("不屑", "副词");
        AddFunction("不要说", "连词");
        AddFunction("不意", "连词");
        AddFunction("不用", "副词");
        AddFunction("不由", "副词");
        AddFunction("不曾", "副词");
        AddFunction("不止", "连词");
        AddFunction("不止", "副词");
        AddFunction("不只", "连词");
        AddFunction("不至于", "副词");
        AddFunction("不致", "副词");
        AddFunction("不足", "副词");
        AddFunction("才", "副词");
        AddFunction("差不多", "副词");
        AddFunction("差点儿", "副词");
        AddFunction("差一点儿", "副词");
        AddFunction("常", "副词");
        AddFunction("常常", "副词");
        AddFunction("朝", "介词");
        AddFunction("朝着", "介词");
        AddFunction("趁", "介词");
        AddFunction("诚", "副词");
        AddFunction("诚然", "连词");
        AddFunction("乘", "介词");
        AddFunction("迟早", "副词");
        AddFunction("冲", "介词");
        AddFunction("初", "副词");
        AddFunction("除", "介词");
        AddFunction("除非", "连词");
        AddFunction("除了", "介词");
        AddFunction("处处", "副词");
        AddFunction("纯", "副词");
        AddFunction("纯粹", "副词");
        AddFunction("此", "代词");
        AddFunction("此外", "连词");
        AddFunction("次第", "副词");
        AddFunction("从", "副词");
        AddFunction("从", "介词");
        AddFunction("从此", "副词");
        AddFunction("从而", "连词");
        AddFunction("从来", "副词");
        AddFunction("从新", "副词");
        AddFunction("匆匆", "副词");
        AddFunction("猝然", "副词");
        AddFunction("打", "介词");
        AddFunction("打从", "介词");
        AddFunction("大", "副词");
        AddFunction("大半", "副词");
        AddFunction("大大", "副词");
        AddFunction("大都", "副词");
        AddFunction("大多", "副词");
        AddFunction("大凡", "副词");
        AddFunction("大概", "副词");
        AddFunction("大伙儿", "代词");
        AddFunction("大家", "代词");
        AddFunction("大家伙儿", "代词");
        AddFunction("大体", "副词");
        AddFunction("大体上", "副词");
        AddFunction("大为", "副词");
        AddFunction("大约", "副词");
        AddFunction("大致", "副词");
        AddFunction("大致上", "副词");
        AddFunction("单", "副词");
        AddFunction("单单", "副词");
        AddFunction("但", "连词");
        AddFunction("但凡", "连词");
        AddFunction("但是", "连词");
        AddFunction("当场", "副词");
        AddFunction("当即", "副词");
        AddFunction("当面", "副词");
        AddFunction("当然", "副词");
        AddFunction("当下", "副词");
        AddFunction("当真", "副词");
        AddFunction("倒", "副词");
        AddFunction("倒是", "副词");
        AddFunction("到处", "副词");
        AddFunction("到底", "副词");
        AddFunction("得", "助词");
        AddFunction("得了", "助词");
        AddFunction("的", "助词");
        AddFunction("的话", "助词");
        AddFunction("的确", "副词");
        AddFunction("登时", "副词");
        AddFunction("地", "助词");
        AddFunction("迭", "副词");
        AddFunction("顶", "副词");
        AddFunction("顶顶", "副词");
        AddFunction("顶好", "副词");
        AddFunction("定", "副词");
        AddFunction("定然", "副词");
        AddFunction("定准", "副词");
        AddFunction("动辄", "副词");
        AddFunction("都", "副词");
        AddFunction("陡然", "副词");
        AddFunction("独", "副词");
        AddFunction("独独", "副词");
        AddFunction("独自", "副词");
        AddFunction("断", "副词");
        AddFunction("断断", "副词");
        AddFunction("断乎", "副词");
        AddFunction("断然", "副词");
        AddFunction("对", "介词");
        AddFunction("对于", "介词");
        AddFunction("顿", "副词");
        AddFunction("顿时", "副词");
        AddFunction("多", "代词");
        AddFunction("多", "副词");
        AddFunction("多半", "副词");
        AddFunction("多亏", "副词");
        AddFunction("多么", "副词");
        AddFunction("多少", "副词");
        AddFunction("多少", "代词");
        AddFunction("俄而", "副词");
    }
}