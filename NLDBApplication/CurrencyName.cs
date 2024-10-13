using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

public partial class CoreContent
{
    // ��������
    public static string CURRENCY_NAME = "";

    public static void ReloadCurrencyName()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "ReloadCurrencyName", "���ػ��ң�");
        // ��������
        CURRENCY_NAME = ReloadStringValue("ʵ��", "����");
        // ��¼��־
        Log.LogMessage(CURRENCY_NAME);
    }

    public static void AddCurrencyName(string name)
    {
        // ָ���ַ���
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('ʵ��', @SqlContent, '����'); ";
        // ���ò���
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // �������
        parameters.Add("SqlContent", name);
        // ִ��ָ��
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllCurrencyNames()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllCurrencyNames", "���ػ��ң�");

        AddCurrencyName("��������");
        AddCurrencyName("��˹���");
        AddCurrencyName("�Ĵ�����Ԫ");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("�Ͳ���");
        AddCurrencyName("��");
        AddCurrencyName("�ȶ�");
        AddCurrencyName("������");
        AddCurrencyName("����");
        AddCurrencyName("������");
        AddCurrencyName("������");
        AddCurrencyName("�������");
        AddCurrencyName("����ķ");
        AddCurrencyName("���ɶ�");
        AddCurrencyName("��");
        AddCurrencyName("�ಽ��");
        AddCurrencyName("����");
        AddCurrencyName("���ɱ�����");
        AddCurrencyName("��");
        AddCurrencyName("����");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("����");
        AddCurrencyName("�ŵ�");
        AddCurrencyName("������");
        AddCurrencyName("����Ԫ");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("����");
        AddCurrencyName("����");
        AddCurrencyName("���ô�Ԫ");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("��");
        AddCurrencyName("�ƶ��");
        AddCurrencyName("����");
        AddCurrencyName("����");
        AddCurrencyName("��³����");
        AddCurrencyName("���߲�");
        AddCurrencyName("����");
        AddCurrencyName("������");
        AddCurrencyName("����");
        AddCurrencyName("��");
        AddCurrencyName("����");
        AddCurrencyName("��������");
        AddCurrencyName("���Ƕ�");
        AddCurrencyName("����");
        AddCurrencyName("�и�");
        AddCurrencyName("�п�");
        AddCurrencyName("����");
        AddCurrencyName("¬��");
        AddCurrencyName("��Ƥ��");
        AddCurrencyName("���");
        AddCurrencyName("���");
        AddCurrencyName("÷�ٿ���");
        AddCurrencyName("����");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("����");
        AddCurrencyName("Ŭ��ķ");
        AddCurrencyName("Ų������");
        AddCurrencyName("ŷԪ");
        AddCurrencyName("�˼�");
        AddCurrencyName("����");
        AddCurrencyName("�����");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("������");
        AddCurrencyName("��ʿ����");
        AddCurrencyName("����");
        AddCurrencyName("�տ���");
        AddCurrencyName("����");
        AddCurrencyName("����");
        AddCurrencyName("����");
        AddCurrencyName("̨��");
        AddCurrencyName("̩��");
        AddCurrencyName("ͼ�����");
        AddCurrencyName("��ͼ");
        AddCurrencyName("�ڼ���");
        AddCurrencyName("����");
        AddCurrencyName("л�˶�");
        AddCurrencyName("�¼���Ԫ");
        AddCurrencyName("��̨��");
        AddCurrencyName("������Ԫ");
        AddCurrencyName("��Ԫ");
        AddCurrencyName("ӡ��¬��");
        AddCurrencyName("Ӣ��");
        AddCurrencyName("Ԫ");
        AddCurrencyName("Խ�϶�");
        AddCurrencyName("������");
        AddCurrencyName("��");
        AddCurrencyName("������");

        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllCurrencyNames", "�����Ѽ��أ�");
    }
}