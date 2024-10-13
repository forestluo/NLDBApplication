using System;
using System.IO;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedType(Format.UserDefined, MaxByteSize = 256)]
public struct UFilterRule: INullable, IBinarySerialize
{
    // 成员字段
    public string rule;
    public string replace;

    public void Read(BinaryReader reader)
    {
        // 检查结果
        if (!reader.ReadBoolean())
        {
            rule = reader.ReadString();
            replace = reader.ReadString();
        }
    }

    public void Write(BinaryWriter writer)
    {
        writer.Write(IsNull);
        // 检查结果
        if (!IsNull)
        {
            writer.Write(rule);
            writer.Write(replace);
        }
    }

    public override string ToString()
    {
        // 替换为您自己的代码
        return rule + "|" + replace;
    }

    public bool IsNull
    {
        get
        {
            // 返回结果
            return rule is null || replace is null;
        }
    }

    public static UFilterRule Null
    {
        get
        {
            // 返回结果
            return new UFilterRule();
        }
    }
    
    public static UFilterRule Parse(SqlString sqlString)
    {
        // 检查参数
        if (sqlString.IsNull) return Null;
        // 生成对象
        UFilterRule filterRule = new UFilterRule();
        // 查找位置
        int position =
            sqlString.Value.IndexOf("|");
        // 检查结果
        if (position >= 0)
        {
            // 获得成员变量
            filterRule.rule = sqlString.Value.Substring(0, position);
            filterRule.replace = sqlString.Value.Substring(position + 1);
        }
        // 在此处放置代码
        return filterRule;
    }  
}