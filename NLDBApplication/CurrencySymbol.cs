using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class CoreContent
{
    // ��������
    public static string CURRENCY_SYMBOL = "";

    public static void ReloadCurrencySymbol()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "ReloadCurrencySymbol", "���ػ��ҷ��ţ�");
        // ��������
        CURRENCY_SYMBOL = ReloadStringValue("����", "���ҷ���");
        // ��¼��־
        Log.LogMessage(CURRENCY_SYMBOL);
    }

    public static void AddCurrencySymbol(string symbol)
    {
        // ָ���ַ���
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('����', @SqlContent, '���ҷ���'); ";
        // ���ò���
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // �������
        parameters.Add("SqlContent", symbol);
        // ִ��ָ��
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllCurrencySymbols()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllCurrencySymbols", "���ػ��ҷ��ţ�");

        AddCurrencySymbol("��");

        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllCurrencySymbols", "���ҷ����Ѽ��أ�");
    }
}