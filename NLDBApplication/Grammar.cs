using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class Grammar
{
    private struct GDescription
    {
        public int count;
        public string left;
        public string right;
        public double gamma;
        public bool operation;
    };

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlBoolean AddUpGrammarCount(SqlString sqlContent, SqlBoolean sqlCacheOnly)
    {
        // 记录日志
        Log.LogMessage("Grammar", "AddUpGrammarCount", "开始统计！");

        // 统计字符数量
        AddUpCharCount(sqlContent, sqlCacheOnly);
        // 统计词汇数量
        AddUpWordCount(sqlContent, sqlCacheOnly);

        // 记录日志
        Log.LogMessage("Grammar", "AddUpGrammarCount", "统计已完成！");
        // 返回结果
        return true;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void UpdateGrammarCount()
    {
        // 记录日志
        Log.LogMessage("Grammar", "UpdateGrammarCount", "更新数据！");

        // 更新字符计数
        UpdateCharCount();
        // 更新词汇计数
        UpdateWordCount();

        // 记录日志
        Log.LogMessage("Grammar", "UpdateGrammarCount", "数据已更新！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ReloadGrammarCount()
    {
        // 记录日志
        Log.LogMessage("Grammar", "ReloadGrammarCount", "加载数据！");

        // 加载字符计数
        ReloadCharCount();
        // 加载词汇计数
        ReloadWordCount();

        // 记录日志
        Log.LogMessage("Grammar", "ReloadGrammarCount", "数据已加载！");
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void RecalculateGamma(SqlBoolean sqlAll)
    {
        // 记录日志
        Log.LogMessage("Grammar", "RecalculateGamma", "更新数据！");

        // 重新计算
        RecalculateWordGamma(sqlAll.Value);

        // 记录日志
        Log.LogMessage("Grammar", "RecalculateGamma", "数据已更新！");
    }

    public static string[] GetClassification(char cValue)
    {
        // 属性
        string strAttribute = null;
        // 分类
        string strClassification = null;

        if (cValue <= 0x007F)
        {
            strClassification = "符号";
            strAttribute = "C0控制符及基本拉丁文";
        }
        else if (cValue <= 0x00FF) { strClassification = "符号"; strAttribute = "C1控制符及拉丁文补充-1"; }
        else if (cValue <= 0x017F) { strClassification = "符号"; strAttribute = "拉丁文扩展-A"; }
        else if (cValue <= 0x024F) { strClassification = "符号"; strAttribute = "拉丁文扩展-B"; }
        else if (cValue <= 0x02AF) { strClassification = "符号"; strAttribute = "国际音标扩展"; }
        else if (cValue <= 0x02FF) { strClassification = "符号"; strAttribute = "空白修饰字母"; }
        else if (cValue <= 0x036F) { strClassification = "符号"; strAttribute = "结合用读音符号"; }
        else if (cValue <= 0x03FF) { strClassification = "符号"; strAttribute = "希腊文及科普特文"; }
        else if (cValue <= 0x04FF) { strClassification = "符号"; strAttribute = "西里尔字母"; }
        else if (cValue <= 0x052F) { strClassification = "符号"; strAttribute = "西里尔字母补充"; }
        else if (cValue <= 0x058F) { strClassification = "符号"; strAttribute = "亚美尼亚语"; }
        else if (cValue <= 0x05FF) { strClassification = "符号"; strAttribute = "希伯来文"; }
        else if (cValue <= 0x06FF) { strClassification = "符号"; strAttribute = "阿拉伯文"; }
        else if (cValue <= 0x074F) { strClassification = "符号"; strAttribute = "叙利亚文"; }
        else if (cValue <= 0x077F) { strClassification = "符号"; strAttribute = "阿拉伯文补充"; }
        else if (cValue <= 0x07BF) { strClassification = "符号"; strAttribute = "马尔代夫语"; }
        else if (cValue <= 0x07FF) { strClassification = "符号"; strAttribute = "西非书面語言"; }
        else if (cValue <= 0x085F) { strClassification = "符号"; strAttribute = "阿维斯塔语及巴列维语"; }
        else if (cValue <= 0x087F) { strClassification = "符号"; strAttribute = "Mandaic"; }
        else if (cValue <= 0x08AF) { strClassification = "符号"; strAttribute = "撒马利亚语"; }
        else if (cValue <= 0x097F) { strClassification = "符号"; strAttribute = "天城文书"; }
        else if (cValue <= 0x09FF) { strClassification = "符号"; strAttribute = "孟加拉语"; }
        else if (cValue <= 0x0A7F) { strClassification = "符号"; strAttribute = "锡克教文"; }
        else if (cValue <= 0x0AFF) { strClassification = "符号"; strAttribute = "古吉拉特文"; }
        else if (cValue <= 0x0B7F) { strClassification = "符号"; strAttribute = "奥里亚文"; }
        else if (cValue <= 0x0BFF) { strClassification = "符号"; strAttribute = "泰米尔文"; }
        else if (cValue <= 0x0C7F) { strClassification = "符号"; strAttribute = "泰卢固文"; }
        else if (cValue <= 0x0CFF) { strClassification = "符号"; strAttribute = "卡纳达文"; }
        else if (cValue <= 0x0D7F) { strClassification = "符号"; strAttribute = "德拉维族语"; }
        else if (cValue <= 0x0DFF) { strClassification = "符号"; strAttribute = "僧伽罗语"; }
        else if (cValue <= 0x0E7F) { strClassification = "符号"; strAttribute = "泰文"; }
        else if (cValue <= 0x0EFF) { strClassification = "符号"; strAttribute = "老挝文"; }
        else if (cValue <= 0x0FFF) { strClassification = "符号"; strAttribute = "藏文"; }
        else if (cValue <= 0x109F) { strClassification = "符号"; strAttribute = "缅甸语"; }
        else if (cValue <= 0x10FF) { strClassification = "符号"; strAttribute = "格鲁吉亚语"; }
        else if (cValue <= 0x11FF) { strClassification = "符号"; strAttribute = "朝鲜文"; }
        else if (cValue <= 0x137F) { strClassification = "符号"; strAttribute = "埃塞俄比亚语"; }
        else if (cValue <= 0x139F) { strClassification = "符号"; strAttribute = "埃塞俄比亚语补充"; }
        else if (cValue <= 0x13FF) { strClassification = "符号"; strAttribute = "切罗基语"; }
        else if (cValue <= 0x167F) { strClassification = "符号"; strAttribute = "统一加拿大土著语音节"; }
        else if (cValue <= 0x169F) { strClassification = "符号"; strAttribute = "欧甘字母"; }
        else if (cValue <= 0x16FF) { strClassification = "符号"; strAttribute = "如尼文"; }
        else if (cValue <= 0x171F) { strClassification = "符号"; strAttribute = "塔加拉语"; }
        else if (cValue <= 0x173F) { strClassification = "符号"; strAttribute = "Hanunóo"; }
        else if (cValue <= 0x175F) { strClassification = "符号"; strAttribute = "Buhid"; }
        else if (cValue <= 0x177F) { strClassification = "符号"; strAttribute = "Tagbanwa"; }
        else if (cValue <= 0x17FF) { strClassification = "符号"; strAttribute = "高棉语"; }
        else if (cValue <= 0x18AF) { strClassification = "符号"; strAttribute = "蒙古文"; }
        else if (cValue <= 0x18FF) { strClassification = "符号"; strAttribute = "Cham"; }
        else if (cValue <= 0x194F) { strClassification = "符号"; strAttribute = "Limbu"; }
        else if (cValue <= 0x197F) { strClassification = "符号"; strAttribute = "德宏泰语"; }
        else if (cValue <= 0x19DF) { strClassification = "符号"; strAttribute = "新傣仂语"; }
        else if (cValue <= 0x19FF) { strClassification = "符号"; strAttribute = "高棉语记号"; }
        else if (cValue <= 0x1A1F) { strClassification = "符号"; strAttribute = "Buginese"; }
        else if (cValue <= 0x1A5F) { strClassification = "符号"; strAttribute = "Batak"; }
        else if (cValue <= 0x1AEF) { strClassification = "符号"; strAttribute = "Lanna"; }
        else if (cValue <= 0x1B7F) { strClassification = "符号"; strAttribute = "巴厘语"; }
        else if (cValue <= 0x1BB0) { strClassification = "符号"; strAttribute = "巽他语"; }
        else if (cValue <= 0x1BFF) { strClassification = "符号"; strAttribute = "Pahawh Hmong"; }
        else if (cValue <= 0x1C4F) { strClassification = "符号"; strAttribute = "雷布查语"; }
        else if (cValue <= 0x1C7F) { strClassification = "符号"; strAttribute = "Ol Chiki"; }
        else if (cValue <= 0x1CDF) { strClassification = "符号"; strAttribute = "曼尼普尔语"; }
        else if (cValue <= 0x1D7F) { strClassification = "符号"; strAttribute = "语音学扩展"; }
        else if (cValue <= 0x1DBF) { strClassification = "符号"; strAttribute = "语音学扩展补充"; }
        else if (cValue <= 0x1DFF) { strClassification = "符号"; strAttribute = "结合用读音符号补充"; }
        else if (cValue <= 0x1EFF) { strClassification = "符号"; strAttribute = "拉丁文扩充附加"; }
        else if (cValue <= 0x1FFF) { strClassification = "符号"; strAttribute = "希腊语扩充"; }
        else if (cValue <= 0x206F) { strClassification = "符号"; strAttribute = "常用标点"; }
        else if (cValue <= 0x209F) { strClassification = "符号"; strAttribute = "上标及下标"; }
        // 货币符号
        else if (cValue <= 0x20CF) { strClassification = "符号"; strAttribute = "货币符号"; }
        else if (cValue <= 0x20FF) { strClassification = "符号"; strAttribute = "组合用记号"; }
        else if (cValue <= 0x214F) { strClassification = "符号"; strAttribute = "字母式符号"; }
        else if (cValue <= 0x218F) { strClassification = "符号"; strAttribute = "数字形式"; }
        else if (cValue <= 0x21FF) { strClassification = "符号"; strAttribute = "箭头"; }
        else if (cValue <= 0x22FF) { strClassification = "符号"; strAttribute = "数学运算符"; }
        else if (cValue <= 0x23FF) { strClassification = "符号"; strAttribute = "杂项工业符号"; }
        else if (cValue <= 0x243F) { strClassification = "符号"; strAttribute = "控制图片"; }
        else if (cValue <= 0x245F) { strClassification = "符号"; strAttribute = "光学识别符"; }
        else if (cValue <= 0x24FF) { strClassification = "符号"; strAttribute = "封闭式字母数字"; }
        else if (cValue <= 0x257F) { strClassification = "符号"; strAttribute = "制表符"; }
        else if (cValue <= 0x259F) { strClassification = "符号"; strAttribute = "方块元素"; }
        else if (cValue <= 0x25FF) { strClassification = "符号"; strAttribute = "几何图形"; }
        else if (cValue <= 0x26FF) { strClassification = "符号"; strAttribute = "杂项符号"; }
        else if (cValue <= 0x27BF) { strClassification = "符号"; strAttribute = "印刷符号"; }
        else if (cValue <= 0x27EF) { strClassification = "符号"; strAttribute = "杂项数学符号-A"; }
        else if (cValue <= 0x27FF) { strClassification = "符号"; strAttribute = "追加箭头-A"; }
        else if (cValue <= 0x28FF) { strClassification = "符号"; strAttribute = "盲文点字模型"; }
        else if (cValue <= 0x297F) { strClassification = "符号"; strAttribute = "追加箭头-B"; }
        else if (cValue <= 0x29FF) { strClassification = "符号"; strAttribute = "杂项数学符号-B"; }
        else if (cValue <= 0x2AFF) { strClassification = "符号"; strAttribute = "追加数学运算符"; }
        else if (cValue <= 0x2BFF) { strClassification = "符号"; strAttribute = "杂项符号和箭头"; }
        else if (cValue <= 0x2C5F) { strClassification = "符号"; strAttribute = "格拉哥里字母"; }
        else if (cValue <= 0x2C7F) { strClassification = "符号"; strAttribute = "拉丁文扩展-C"; }
        else if (cValue <= 0x2CFF) { strClassification = "符号"; strAttribute = "古埃及语"; }
        else if (cValue <= 0x2D2F) { strClassification = "符号"; strAttribute = "格鲁吉亚语补充"; }
        else if (cValue <= 0x2D7F) { strClassification = "符号"; strAttribute = "提非纳文"; }
        else if (cValue <= 0x2DDF) { strClassification = "符号"; strAttribute = "埃塞俄比亚语扩展"; }
        else if (cValue <= 0x2E7F) { strClassification = "符号"; strAttribute = "追加标点"; }
        else if (cValue <= 0x2EFF) { strClassification = "符号"; strAttribute = "CJK 部首补充"; }
        else if (cValue <= 0x2FDF) { strClassification = "符号"; strAttribute = "康熙字典部首"; }
        else if (cValue <= 0x2FFF) { strClassification = "符号"; strAttribute = "表意文字描述符"; }
        else if (cValue <= 0x303F) { strClassification = "符号"; strAttribute = "CJK 符号和标点"; }
        else if (cValue <= 0x309F) { strClassification = "符号"; strAttribute = "日文平假名"; }
        else if (cValue <= 0x30FF) { strClassification = "符号"; strAttribute = "日文片假名"; }
        else if (cValue <= 0x312F) { strClassification = "符号"; strAttribute = "注音字母"; }
        else if (cValue <= 0x318F) { strClassification = "符号"; strAttribute = "朝鲜文兼容字母"; }
        else if (cValue <= 0x319F) { strClassification = "符号"; strAttribute = "象形字注释标志"; }
        else if (cValue <= 0x31BF) { strClassification = "符号"; strAttribute = "注音字母扩展"; }
        else if (cValue <= 0x31EF) { strClassification = "符号"; strAttribute = "CJK 笔画"; }
        else if (cValue <= 0x31FF) { strClassification = "符号"; strAttribute = "日文片假名语音扩展"; }
        else if (cValue <= 0x32FF) { strClassification = "符号"; strAttribute = "封闭式 CJK 文字和月份"; }
        else if (cValue <= 0x33FF) { strClassification = "符号"; strAttribute = "CJK 兼容"; }
        else if (cValue <= 0x4DBF) { strClassification = "符号"; strAttribute = "CJK 统一表意符号扩展 A"; }
        else if (cValue <= 0x4DFF) { strClassification = "符号"; strAttribute = "易经六十四卦符号"; }
        // 基础汉字
        else if (cValue <= 0x9FBF) { strClassification = "符号"; strAttribute = "CJK 统一表意符号"; }
        else if (cValue <= 0xA48F) { strClassification = "符号"; strAttribute = "彝文音节"; }
        else if (cValue <= 0xA4CF) { strClassification = "符号"; strAttribute = "彝文字根"; }
        else if (cValue <= 0xA61F) { strClassification = "符号"; strAttribute = "Vai"; }
        else if (cValue <= 0xA6FF) { strClassification = "符号"; strAttribute = "统一加拿大土著语音节补充"; }
        else if (cValue <= 0xA71F) { strClassification = "符号"; strAttribute = "声调修饰字母"; }
        else if (cValue <= 0xA7FF) { strClassification = "符号"; strAttribute = "拉丁文扩展-D"; }
        else if (cValue <= 0xA82F) { strClassification = "符号"; strAttribute = "Syloti Nagri"; }
        else if (cValue <= 0xA87F) { strClassification = "符号"; strAttribute = "八思巴字"; }
        else if (cValue <= 0xA8DF) { strClassification = "符号"; strAttribute = "Saurashtra"; }
        else if (cValue <= 0xA97F) { strClassification = "符号"; strAttribute = "爪哇语"; }
        else if (cValue <= 0xA9DF) { strClassification = "符号"; strAttribute = "Chakma"; }
        else if (cValue <= 0xAA3F) { strClassification = "符号"; strAttribute = "Varang Kshiti"; }
        else if (cValue <= 0xAA6F) { strClassification = "符号"; strAttribute = "Sorang Sompeng"; }
        else if (cValue <= 0xAADF) { strClassification = "符号"; strAttribute = "Newari"; }
        else if (cValue <= 0xAB5F) { strClassification = "符号"; strAttribute = "越南傣语"; }
        else if (cValue <= 0xABA0) { strClassification = "符号"; strAttribute = "Kayah Li"; }
        else if (cValue <= 0xD7AF) { strClassification = "符号"; strAttribute = "朝鲜文音节"; }
        // 不可见字符
        else if (cValue <= 0xDBFF) { strClassification = "符号"; strAttribute = "High-half zone of UTF-16"; }
        // 不可见字符
        else if (cValue <= 0xDFFF) { strClassification = "符号"; strAttribute = "Low-half zone of UTF-16"; }
        else if (cValue <= 0xF8FF) { strClassification = "符号"; strAttribute = "自行使用区域"; }
        else if (cValue <= 0xFAFF) { strClassification = "符号"; strAttribute = "CJK 兼容象形文字"; }
        else if (cValue <= 0xFB4F) { strClassification = "符号"; strAttribute = "字母表達形式"; }
        else if (cValue <= 0xFDFF) { strClassification = "符号"; strAttribute = "阿拉伯表達形式A"; }
        else if (cValue <= 0xFE0F) { strClassification = "符号"; strAttribute = "变量选择符"; }
        else if (cValue <= 0xFE1F) { strClassification = "符号"; strAttribute = "竖排形式"; }
        else if (cValue <= 0xFE2F) { strClassification = "符号"; strAttribute = "组合用半符号"; }
        else if (cValue <= 0xFE4F) { strClassification = "符号"; strAttribute = "CJK 兼容形式"; }
        else if (cValue <= 0xFE6F) { strClassification = "符号"; strAttribute = "小型变体形式"; }
        else if (cValue <= 0xFEFF) { strClassification = "符号"; strAttribute = "阿拉伯表達形式B"; }
        else if (cValue <= 0xFFEF) { strClassification = "符号"; strAttribute = "半型及全型形式"; }
        // 不可见字符
        else if (cValue <= 0xFFFF) { strClassification = "符号"; strAttribute = "特殊"; }
        // 返回结果
        return new string[] { strClassification, strAttribute };
    }
}
