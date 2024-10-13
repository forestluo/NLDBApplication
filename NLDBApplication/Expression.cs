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
        // ��¼��־
        Log.LogMessage("RegularRule", "clearExpressions", "������ʽ��");
        // ��¼��־
        Log.LogMessage(string.Format("\texpressions.count = {0}", expressions.Count));
        // ��¼��־
        //foreach (Expression item in expressions)
        //{
        //    // ��¼��־
        //    Log.LogMessage(string.
        //            Format("\texpression(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
        //            item.value, item.index, item.length, item.attribute, item.classification));
        //}

        // ����
        int nextLoop; Expression[] pair;
        do
        {
            // ���ó�ʼֵ
            nextLoop = 0; pair = new Expression[2];
            // ѭ������
            foreach (Expression exp1 in expressions)
            {
                // ѭ������
                foreach (Expression exp2 in expressions)
                {
                    // �����
                    if (exp1.id == exp2.id) continue;
                    // ���������
                    if (exp1.index >= exp2.index &&
                        exp1.index + exp1.length <= exp2.index + exp2.length)
                    {
                        // ���ö���
                        nextLoop = 1; pair[0] = exp1; pair[1] = exp2; break;
                    }
                    // ��鸲�����
                    else if (exp1.index < exp2.index &&
                        exp1.index + exp1.length >= exp2.index &&
                        exp1.index + exp1.length <= exp2.index + exp2.length)
                    {
                        // ���ö���
                        nextLoop = 2; pair[0] = exp1; pair[1] = exp2; break;
                    }
                    // ��鸲�����
                    else if (exp1.index >= exp2.index &&
                        exp1.index <= exp2.index + exp2.length &&
                        exp1.index + exp1.length > exp2.index + exp2.length)
                    {
                        // ���ö���
                        nextLoop = 3; pair[0] = exp1; pair[1] = exp2; break;
                    }
                }
                // �������
                if (nextLoop > 0) break;
            }
            // �������
            if (nextLoop > 0)
            {
                // ��ö���
                Expression exp1 = pair[0]; Expression exp2 = pair[1];
                // ��¼��־
                //Log.LogMessage(string.Format("\tnextLoop = {0}", nextLoop));
                // ��¼��־
                //Log.LogMessage(string.
                //        Format("\texp1(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                //        exp1.value, exp1.index, exp1.length, exp1.attribute, exp1.classification));
                // ��¼��־
                //Log.LogMessage(string.
                //        Format("\texp2(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                //        exp2.value, exp2.index, exp2.length, exp2.attribute, exp2.classification));
                // ɾ������
                expressions.Remove(exp1); expressions.Remove(exp2);
                // �����ֵ
                if (nextLoop == 2)
                {
                    // �ϲ�����
                    if(exp1.length > exp2.length)
                    {
                        // ��������
                        exp2.attribute = exp1.attribute;
                        exp2.classification = exp1.classification;
                    }
                    // ע��˳�򣡣�
                    exp2.value = exp1.value.
                        Substring(0, exp2.index - exp1.index) + exp2.value;
                    exp2.length = exp2.index + exp2.length - exp1.index;
                    exp2.index = exp1.index;
                }
                else if(nextLoop == 3)
                {
                    // �ϲ�����
                    if (exp1.length > exp2.length)
                    {
                        // ��������
                        exp2.attribute = exp1.attribute;
                        exp2.classification = exp1.classification;
                    }
                    // ע��˳�򣡣�
                    exp2.value = exp2.value + exp1.value.
                        Substring(exp2.index + exp2.length - exp1.index,
                        (exp1.index + exp1.length) - (exp2.index + exp2.length));
                    exp2.length = exp1.index + exp1.length - exp2.index;
                }
                // ���Ӷ���
                expressions.Add(exp2);
                // ��¼��־
                //Log.LogMessage(string.Format("\texpressions.count = {0}", expressions.Count));
                // ��¼��־
                //Log.LogMessage(string.
                //        Format("\tresult(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                //        exp2.value, exp2.index, exp2.length, exp2.attribute, exp2.classification));
            }
        } while (nextLoop > 0);

        // ��¼��־
        Log.LogMessage("RegularRule", "ExtractExpressions", "���ʽ������ϣ�");
    }

    public static List<Expression> ExtractExpressions(string strContent)
    {
        // ��¼��־
        Log.LogMessage("RegularRule", "ExtractExpressions", "��ȡ���ʽ��");
        // ���
        int id = 0;
        // �����������
        List<Expression> expressions = new List<Expression>();
        // ѭ������
        foreach(KeyValuePair<string, string[]> kvp in rules)
        {
            // ��¼��־
            //Log.LogMessage(string.Format("\trule = {0}", kvp.Key));
            // ѭ������
            foreach (Match match in Regex.Matches(strContent,kvp.Key))
            {
                // ���ӱ��
                id ++;
                // ��������
                Expression expression = new Expression();
                // ���ò���
                expression.id = id;
                expression.index = match.Index;
                expression.value = match.Value;
                expression.length = match.Length;
                expression.attribute = kvp.Value[0];
                expression.classification = kvp.Value[1];
                // ���뵽���������
                expressions.Add(expression);
                // ��¼��־
                //Log.LogMessage(string.Format("\tmatched = {0}", match.Value));
            }
        }
        // ������ʽ
        clearExpressions(ref expressions);
        // ��¼��־
        Log.LogMessage(string.Format("\texpressions.count = {0}", expressions.Count));
        // ��¼��־
        foreach (Expression item in expressions)
        {
            // ��¼��־
            Log.LogMessage(string.
                    Format("\texpression(\"{0}\") : index = {1}, length = {2}, attribute = \"{3}\", classification = \"{4}\"",
                    item.value, item.index, item.length, item.attribute, item.classification));
        }
        // ��¼��־
        Log.LogMessage("RegularRule", "ExtractExpressions", "���ʽ��ȡ��ϣ�");
        // ���ؽ��
        return expressions;
    }
}