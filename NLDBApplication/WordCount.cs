using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Grammar
{
    // ���������ֵ�
    private static Dictionary<string, GDescription> words = new Dictionary<string, GDescription>();

    public static int GetWordCount(string strValue)
    {
        // ���ؽ��
        return words.ContainsKey(strValue) ?
            words[strValue].count : LoadWordCount(strValue);
    }

    public static int LoadWordCount(string strValue)
    {
        // ��¼��־
        Log.LogMessage("Grammar", "LoadWordCount", "�������ݼ�¼��");

        // ������
        int count = -1;
        // ָ���ַ���
        string cmdString =
            "SELECT content, count, gamma " +
            "FROM [dbo].[InnerContent] " +
            "WHERE LEN(content) = 2 AND content = @SqlWord;";

        // �������ݿ�����
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // �������ݿ�����
            sqlConnection.Open();
            // ����ָ��
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // ���ò���
            sqlCommand.Parameters.AddWithValue("SqlWord", strValue);
            // ���������Ķ���
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // ѭ������
            while (reader.Read())
            {
                // ��������
                GDescription description = new GDescription();
                // ���ò���
                description.operation = false;
                // ���ò���
                count = reader.GetInt32(1);
                // �����
                if (count < 0) count = 0;
                // ���ò���
                description.count = count;
                description.gamma = reader.GetDouble(2);
                // �������
                description.left = strValue.Substring(0, 1);
                // �����Ҳ�
                description.right = strValue.Substring(1, 1);
                // ���Ӽ�¼
                words.Add(strValue, description);
            }
            // �ر������Ķ���
            reader.Close();
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // ���״̬���ر�����
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // ��¼��־
        Log.LogMessage("Grammar", "LoadWordCount", "���ݼ�¼�Ѽ��أ�");
        // ���ؽ��
        return count;
    }

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlBoolean AddUpWordCount(SqlString sqlContent, SqlBoolean sqlCacheOnly)
    {
        // ��¼��־
        Log.LogMessage("Grammar", "AddUpWordCount", "��ʼͳ�ƣ�");
        // ѭ������
        foreach (Match item in
            Regex.Matches(sqlContent.Value, @"[\u4E00-\u9FA5]+"))
        {
            // ��鳤��
            for (int i = 0; i < item.Value.Length - 1; i ++)
            {
                // ������ַ���������2��
                string strValue = item.Value.Substring(i, 2);
                // �����
                if (strValue == null || strValue.Length != 2) continue;
                // ����
                GDescription description;
                // �����
                if (!words.ContainsKey(strValue))
                {
                    // ��������
                    description = new GDescription();
                    // ���ò���
                    description.count = 0;
                    description.gamma = -1.0;
                    description.operation = true;
                    description.left = strValue.Substring(0, 1);
                    description.right = strValue.Substring(1, 1);
                    // ������
                    if(sqlCacheOnly)
                    {
                        // ����һ����¼
                        words.Add(strValue, description);
                    }
                    else
                    {
                        // ���Դ����ݿ��м���
                        if (LoadWordCount(strValue) < 0)
                        {
                            // ����һ����¼
                            words.Add(strValue, description);
                        }
                    }
                }
                // ��������
                description = words[strValue];
                // ���Ӽ���
                description.count ++;
                // ���ò������
                description.operation = true;
                // ������ֵ
                words[strValue] = description;
                // ��¼��־
                //Log.LogMessage(string.Format("\tcontent({0}) = {1}", strValue, description.count));
            }
        }
        // ��¼��־
        Log.LogMessage("Grammar", "AddUpWordCount", "ͳ������ɣ�");
        // ���ؽ��
        return true;
    }

    public static void UpdateWordCount()
    {
        // ��¼��־
        Log.LogMessage("Grammar", "UpdateWordCount", "�������ݣ�");
        Log.LogMessage(string.Format("\twords.count = {0}", words.Count));

        // ���������������
        string cmdString =
            "UPDATE [dbo].[InnerContent] " +
                "SET [count] = @SqlCount, [gamma] = @SqlGamma " +
                "WHERE content = @SqlWord; " +
            "IF @@ROWCOUNT <= 0 " +
                "INSERT INTO [dbo].[InnerContent] " +
                "([classification], [content], [count], [gamma]) " +
                    "VALUES ('G2', @SqlWord, @SqlCount, @SqlGamma); ";

        // �������ݿ�����
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // �������ݿ�����
            sqlConnection.Open();
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateWordCount", "���������ѿ�����");

            // ����������ģʽ
            SqlTransaction sqlTransaction =
                sqlConnection.BeginTransaction();
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateWordCount", "�������ѿ�����");

            // ����ָ��
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // �������ﴦ��ģʽ
            sqlCommand.Transaction = sqlTransaction;
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateWordCount", "T-SQLָ���Ѵ�����");

            // ��������
            foreach (KeyValuePair<string, GDescription> kvp in words)
            {
                // �������
                GDescription description = kvp.Value;
                // ��¼��־
                //Log.LogMessage(string.Format("description.content = {0}", kvp.Key));
                //Log.LogMessage(string.Format("\tdescription.count = {0}", description.count));
                //Log.LogMessage(string.Format("\tdescription.left = {0}", description.left));
                //Log.LogMessage(string.Format("\tdescription.right = {0}", description.right));
                //Log.LogMessage(string.Format("\tdescription.gamma = {0}", description.gamma));
                //Log.LogMessage(string.Format("\tdescription.operation = {0}", description.operation));
                // ���������
                if (!description.operation) continue;
                // �������
                sqlCommand.Parameters.Clear();
                // ���ò���
                sqlCommand.Parameters.AddWithValue("SqlWord", kvp.Key);
                sqlCommand.Parameters.AddWithValue("SqlCount", description.count);
                sqlCommand.Parameters.AddWithValue("SqlGamma", description.gamma);
                // ִ��ָ���δִ�У�
                sqlCommand.ExecuteNonQuery();
                // ����������
                description.operation = false;
            }
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateWordCount", "����ָ������ӣ�");

            // �ύ������
            sqlTransaction.Commit();
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateWordCount", "����ָ�����ύ��");
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // ���״̬���ر�����
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // ��¼��־
        Log.LogMessage("Grammar", "UpdateWordCount", "���ݼ�¼�Ѹ��£�");
    }

    public static void ReloadWordCount()
    {
        // ��¼��־
        Log.LogMessage("Grammar", "ReloadWordCount", "�������ݼ�¼��");

        // �����ַ�
        words.Clear();

        // ��¼��־
        Log.LogMessage("Grammar", "ReloadWordCount", "�������ݼ�¼��");

        // ָ���ַ���
        string cmdString =
            "SELECT content, count, gamma FROM [dbo].[InnerContent] " +
            "WHERE classification <> '����Ӣ���ʵ�' AND LEN(content) = 2;";

        // �������ݿ�����
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // �������ݿ�����
            sqlConnection.Open();
            // ����ָ��
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // ���������Ķ���
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // ѭ������
            while (reader.Read())
            {
                // �������
                string strValue = reader.GetString(0);
                // �����
                if (strValue == null || strValue.Length != 2) continue;               
                // ��������
                GDescription description = new GDescription();
                // ���ò���
                description.operation = false;
                // ���ò���
                description.count = reader.GetInt32(1);
                description.gamma = reader.GetDouble(2);
                // �������
                description.left = strValue.Substring(0, 1);
                // �����Ҳ�
                description.right = strValue.Substring(1, 1);
                // �����
                if (description.count < 0) description.count = 0;
                // ���Ӽ�¼
                words.Add(strValue, description);
            }
            // �ر������Ķ���
            reader.Close();
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // ���״̬���ر�����
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // ��¼��־
        Log.LogMessage("\twords.count = " + words.Count);
        // ��¼��־
        Log.LogMessage("Grammar", "ReloadWordCount", "���ݼ�¼�Ѽ��أ�");
    }

    public static void RecalculateWordGamma(bool bAll)
    {
        // ��¼��־
        Log.LogMessage("Grammar", "RecalculateWordGamma", "�������ݣ�");
        // ��¼��־
        Log.LogMessage("\twords.count = " + words.Count);

        // �����´ʵ�
        List<string> keys = new List<string>();
        // ���ӹؼ���
        keys.AddRange(words.Keys);

        // ��������
        foreach (string strWord in keys)
        {
            // �����
            if (strWord == null || strWord.Length != 2) continue;
            // �������
            GDescription description = words[strWord];
            // �����
            if (!bAll)
            {
                // ���������
                if (!description.operation && description.gamma > 0) continue;
            }
            // ���ò������
            description.operation = true;
            // ����������
            description.left = strWord.Substring(0, 1);
            // ����Ҳ�����
            description.right = strWord.Substring(1, 1);
            //�����
            if (description.count < 0) description.count = 0;
            // �����������
            int leftCount = GetCharCount(description.left[0]);
            // ����Ҳ������
            int rightCount = GetCharCount(description.right[0]);
            // �����
            if (leftCount <= 0 || rightCount <= 0) description.gamma = -1.0;
            else
            {
                // ��ü���
                int count = description.count;
                // �����
                if (count <= 0) count = 1;
                // �������ϵ��
                description.gamma = 0.0;
                description.gamma += (double)count / leftCount;
                description.gamma += (double)count / rightCount;
                description.gamma = 2.0 / description.gamma;
            }
            // ����������ֵ
            words[strWord] = description;
            // ��¼��־
            //Log.LogMessage(string.Format("description.content = {0}", strWord));
            //Log.LogMessage(string.Format("\tdescription.count = {0}", description.count));
            //Log.LogMessage(string.Format("\tdescription.leftCount = {0}", leftCount));
            //Log.LogMessage(string.Format("\tdescription.rightCount = {0}", rightCount));
            //Log.LogMessage(string.Format("\tdescription.gamma = {0}", description.gamma));
            //Log.LogMessage(string.Format("\tdescription.operation = {0}", description.operation));
        }

        // ��¼��־
        Log.LogMessage("Grammar", "RecalculateWordGamma", "�����Ѹ��£�");
    }
}
