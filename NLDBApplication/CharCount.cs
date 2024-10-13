using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class Grammar
{
    // ���������ֵ�
    private static Dictionary<char, int> chars = new Dictionary<char, int>();

    public static int GetCharCount(char cValue)
    {
        // ���ؽ��
        return chars.ContainsKey(cValue) ?
            chars[cValue] : LoadCharCount(cValue);
    }

    public static int LoadCharCount(char cValue)
    {
        // ��¼��־
        Log.LogMessage("Grammar", "LoadCharCount", "�������ݼ�¼��");

        // ������
        int count = -1;
        // ָ���ַ���
        string cmdString =
            "SELECT content,count FROM [dbo].[CoreContent] " +
            "WHERE classification = '����' AND [unicode] = UNICODE(@SqlChar);";

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
            sqlCommand.Parameters.AddWithValue("SqlChar", cValue);
            // ���������Ķ���
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // ѭ������
            while (reader.Read())
            {
                // ����¼
                if (reader.IsDBNull(0)) continue;
                // ��ü���
                count = reader.GetInt32(1);
                // �����
                if (count < 0) count = 0;
                // ����һ���¼�ֵ
                chars.Add(cValue, count);
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
        Log.LogMessage("Grammar", "LoadCharCount", "���ݼ�¼�Ѽ��أ�");
        // ���ؽ��
        return count;
    }

    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlBoolean AddUpCharCount(SqlString sqlContent, SqlBoolean sqlCacheOnly)
    {
        // ��¼��־
        Log.LogMessage("Grammar", "AddUpCharCount", "��ʼͳ�ƣ�");
        // ѭ������
        foreach (char cValue in sqlContent.Value)
        {
            // �����
            if (!chars.ContainsKey(cValue)) 
            {
                // ������
                if(sqlCacheOnly)
                {
                    // ����һ����¼
                    chars.Add(cValue, 0);
                }
                else
                {
                    // ���Դ����ݿ��м���
                    if (LoadCharCount(cValue) < 0) chars.Add(cValue, 0);
                }
            }
            // ���Ӽ���
            chars[cValue] ++;
            // ��¼��־
            //Log.LogMessage(string.Format("\tchar({0}).count = {1}", cValue, chars[cValue]));
        }
        // ��¼��־
        Log.LogMessage("Grammar", "AddUpCharCount", "ͳ������ɣ�");
        // ���ؽ��
        return true;
    }

    public static void UpdateCharCount()
    {
        // ��¼��־
        Log.LogMessage("Grammar", "UpdateCharCount", "�������ݣ�");
        Log.LogMessage(string.Format("\tchars.count = {0}", chars.Count));

        // ���������������
        string cmdString =
            "UPDATE [dbo].[CoreContent] " +
                "SET [count] = @SqlCount " +
                "WHERE [unicode] = UNICODE(@SqlChar) AND " +
                "classification = @SqlClassification AND attribute = @SqlAttribute; " +
            "IF @@ROWCOUNT <= 0 " +
                "INSERT INTO [dbo].[CoreContent] " +
                "([content], [unicode], [count], [classification], [attribute]) " +
                "VALUES (@SqlChar, UNICODE(@SqlChar), @SqlCount, @SqlClassification, @SqlAttribute); ";

        // �������ݿ�����
        SqlConnection sqlConnection = new SqlConnection("context connection = true");

        try
        {
            // �������ݿ�����
            sqlConnection.Open();
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateCharCount", "���������ѿ�����");

            // ����������ģʽ
            SqlTransaction sqlTransaction =
                sqlConnection.BeginTransaction();
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateCharCount", "�������ѿ�����");

            // ����ָ��
            SqlCommand sqlCommand =
                new SqlCommand(cmdString, sqlConnection);
            // �������ﴦ��ģʽ
            sqlCommand.Transaction = sqlTransaction;
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateCharCount", "T-SQLָ���Ѵ�����");

            // ��������
            foreach (KeyValuePair<char, int> kvp in chars)
            {
                // ��÷�������
                string[] result = GetClassification(kvp.Key);
                // �����
                if (result == null || result.Length != 2) continue;
                // �������
                sqlCommand.Parameters.Clear();
                // ���ò���
                sqlCommand.Parameters.AddWithValue("SqlChar", kvp.Key);
                sqlCommand.Parameters.AddWithValue("SqlCount", kvp.Value);
                // ���÷�������
                sqlCommand.Parameters.AddWithValue("SqlAttribute", result[1]);
                sqlCommand.Parameters.AddWithValue("SqlClassification", result[0]);
                // ִ��ָ���δִ�У�
                sqlCommand.ExecuteNonQuery();
            }
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateCharCount", "����ָ������ӣ�");

            // �ύ������
            sqlTransaction.Commit();
            // ��¼��־
            Log.LogMessage("Grammar", "UpdateCharCount", "����ָ�����ύ��");
        }
        catch (System.Exception ex) { throw ex; }
        finally
        {
            // ���״̬���ر�����
            if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        // ��¼��־
        Log.LogMessage("Grammar", "UpdateCharCount", "���ݼ�¼�Ѹ��£�");
    }

    public static void ReloadCharCount()
    {
        // ��¼��־
        Log.LogMessage("Grammar", "ReloadCharCount", "�������ݼ�¼��");

        // �����ַ�
        chars.Clear();

        // ��¼��־
        Log.LogMessage("Grammar", "ReloadCharCount", "�������ݼ�¼��");

        // ָ���ַ���
        string cmdString =
            "SELECT content,count FROM [dbo].[CoreContent] " +
            "WHERE classification = '����' AND attribute NOT IN ('��λ','����') AND LEN(content) = 1;";

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
                // ����¼
                if (reader.IsDBNull(0)) continue;
                // ��ü���
                int count = reader.GetInt32(1);
                // �����
                if (count < 0) count = 0;
                // ����ַ�
                char cValue = reader.GetString(0)[0];
                // ������ݼ�
                if (!chars.ContainsKey(cValue)) chars.Add(cValue, count);
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
        Log.LogMessage("Grammar", "ReloadCharCount", "���ݼ�¼�Ѽ��أ�");
        // ��¼��־
        Log.LogMessage(string.Format("\tchars.count = {0}", chars.Count));
    }
}
