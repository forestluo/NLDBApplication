using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class RegularRule
{
    public struct Expression
    {
        public int id;
        public int index;
        public int length;
        public string value;
        public string attribute;
        public string classification;
    }

    private static void clearExpressions(ref List<Expression> expressions)
    {
        // 记录日志
        Log.LogMessage("RegularRule", "clearExpressions", "清理表达式！");
        // 记录日志
        Log.LogMessage(string.Format("\texpressions.count = {0}", expressions.Count));
        // 记录日志
        //foreach (Expression item in expressions)
        //{
        //    // 记录日志
        //    Log.LogMessage(string.
        //            Format("\texpression(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
        //            item.value, item.index, item.length, item.attribute, item.classification));
        //}

        // 对象
        int nextLoop; Expression[] pair;
        do
        {
            // 设置初始值
            nextLoop = 0; pair = new Expression[2];
            // 循环处理
            foreach (Expression exp1 in expressions)
            {
                // 循环处理
                foreach (Expression exp2 in expressions)
                {
                    // 检查结果
                    if (exp1.id == exp2.id) continue;
                    // 检查包含情况
                    if (exp1.index >= exp2.index &&
                        exp1.index + exp1.length <= exp2.index + exp2.length)
                    {
                        // 设置对象
                        nextLoop = 1; pair[0] = exp1; pair[1] = exp2; break;
                    }
                    // 检查覆盖情况
                    else if (exp1.index < exp2.index &&
                        exp1.index + exp1.length >= exp2.index &&
                        exp1.index + exp1.length <= exp2.index + exp2.length)
                    {
                        // 设置对象
                        nextLoop = 2; pair[0] = exp1; pair[1] = exp2; break;
                    }
                    // 检查覆盖情况
                    else if (exp1.index >= exp2.index &&
                        exp1.index <= exp2.index + exp2.length &&
                        exp1.index + exp1.length > exp2.index + exp2.length)
                    {
                        // 设置对象
                        nextLoop = 3; pair[0] = exp1; pair[1] = exp2; break;
                    }
                }
                // 检查索引
                if (nextLoop > 0) break;
            }
            // 检查索引
            if (nextLoop > 0)
            {
                // 获得对象
                Expression exp1 = pair[0]; Expression exp2 = pair[1];
                // 记录日志
                //Log.LogMessage(string.Format("\tnextLoop = {0}", nextLoop));
                // 记录日志
                //Log.LogMessage(string.
                //        Format("\texp1(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                //        exp1.value, exp1.index, exp1.length, exp1.attribute, exp1.classification));
                // 记录日志
                //Log.LogMessage(string.
                //        Format("\texp2(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                //        exp2.value, exp2.index, exp2.length, exp2.attribute, exp2.classification));
                // 删除对象
                expressions.Remove(exp1); expressions.Remove(exp2);
                // 检查数值
                if (nextLoop == 2)
                {
                    // 合并内容
                    if(exp1.length > exp2.length)
                    {
                        // 设置属性
                        exp2.attribute = exp1.attribute;
                        exp2.classification = exp1.classification;
                    }
                    // 注意顺序！！
                    exp2.value = exp1.value.
                        Substring(0, exp2.index - exp1.index) + exp2.value;
                    exp2.length = exp2.index + exp2.length - exp1.index;
                    exp2.index = exp1.index;
                }
                else if(nextLoop == 3)
                {
                    // 合并内容
                    if (exp1.length > exp2.length)
                    {
                        // 设置属性
                        exp2.attribute = exp1.attribute;
                        exp2.classification = exp1.classification;
                    }
                    // 注意顺序！！
                    exp2.value = exp2.value + exp1.value.
                        Substring(exp2.index + exp2.length - exp1.index,
                        (exp1.index + exp1.length) - (exp2.index + exp2.length));
                    exp2.length = exp1.index + exp1.length - exp2.index;
                }
                // 增加对象
                expressions.Add(exp2);
                // 记录日志
                //Log.LogMessage(string.Format("\texpressions.count = {0}", expressions.Count));
                // 记录日志
                //Log.LogMessage(string.
                //        Format("\tresult(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                //        exp2.value, exp2.index, exp2.length, exp2.attribute, exp2.classification));
            }
        } while (nextLoop > 0);

        // 记录日志
        Log.LogMessage("RegularRule", "ExtractExpressions", "表达式清理完毕！");
    }

    public static List<Expression> ExtractExpressions(string strContent)
    {
        // 记录日志
        Log.LogMessage("RegularRule", "ExtractExpressions", "提取表达式！");
        // 编号
        int id = 0;
        // 创建数组对象
        List<Expression> expressions = new List<Expression>();
        // 循环处理
        foreach(KeyValuePair<string, string[]> kvp in rules)
        {
            // 记录日志
            //Log.LogMessage(string.Format("\trule = {0}", kvp.Key));
            // 循环处理
            foreach (Match match in Regex.Matches(strContent,kvp.Key))
            {
                // 增加编号
                id ++;
                // 创建对象
                Expression expression = new Expression();
                // 设置参数
                expression.id = id;
                expression.index = match.Index;
                expression.value = match.Value;
                expression.length = match.Length;
                expression.attribute = kvp.Value[0];
                expression.classification = kvp.Value[1];
                // 加入到数组对象中
                expressions.Add(expression);
                // 记录日志
                //Log.LogMessage(string.Format("\tmatched = {0}", match.Value));
            }
        }
        // 清理表达式
        clearExpressions(ref expressions);
        // 记录日志
        Log.LogMessage(string.Format("\texpressions.count = {0}", expressions.Count));
        // 记录日志
        foreach (Expression item in expressions)
        {
            // 记录日志
            Log.LogMessage(string.
                    Format("\texpression(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                    item.value, item.index, item.length, item.attribute, item.classification));
        }
        // 记录日志
        Log.LogMessage("RegularRule", "ExtractExpressions", "表达式提取完毕！");
        // 返回结果
        return expressions;
    }
}