using System.Collections.Generic;

public partial class CoreContent
{
    // ��λ����
    public static string UNIT_NAME = "";

    public static void ReloadUnitName()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "ReloadUnitName", "���ص�λ��");
        // ��������
        UNIT_NAME = ReloadStringValue("ʵ��", "��λ");
        // ��¼��־
        Log.LogMessage(UNIT_NAME);
    }

    public static void AddUnitName(string name)
    {
        // ָ���ַ���
        string cmdString = "INSERT INTO [dbo].[CoreContent] " +
            "([classification], [content], [attribute]) " +
            "VALUES ('ʵ��', @SqlContent, '��λ'); ";
        // ���ò���
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        // �������
        parameters.Add("SqlContent", name);
        // ִ��ָ��
        NLDB.ExecuteNonQuery(cmdString, parameters);
    }

    public static void AddAllUnitNames()
    {
        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllUnitNames", "���ص�λ��");

        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("��˾");
        AddUnitName("��");
        AddUnitName("�ٿ�");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("��׼����ѹ");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("����ѹ");
        AddUnitName("��");
        AddUnitName("���ӷ���");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("�̶�");
        AddUnitName("��");
        AddUnitName("�ֱ�׼ú");
        AddUnitName("�ֵ���ú");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("�ֱ�");
        AddUnitName("�ֿ�");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��¡");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��Ķ");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("���׹���");
        AddUnitName("����ˮ��");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("���϶�");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("�Ƿ�");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("������");
        AddUnitName("���϶�");
        AddUnitName("������");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("���϶�");
        AddUnitName("��");
        AddUnitName("�տ�˹");
        AddUnitName("��");
        AddUnitName("���");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("��������");
        AddUnitName("��������");
        AddUnitName("������");
        AddUnitName("������");
        AddUnitName("����Ӣ��");
        AddUnitName("����Ӣ��");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("���϶�");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("��������");
        AddUnitName("�ܶ�");
        AddUnitName("��");
        AddUnitName("Ħ��");
        AddUnitName("Ķ");
        AddUnitName("�ɷ�");
        AddUnitName("����");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("ţ��");
        AddUnitName("ŷ");
        AddUnitName("ŷķ");
        AddUnitName("��");
        AddUnitName("��˹��");
        AddUnitName("���");
        AddUnitName("Ƥ��");
        AddUnitName("Ƥ��");
        AddUnitName("Ʒ��");
        AddUnitName("ƽ����");
        AddUnitName("ƽ����");
        AddUnitName("ƽ����");
        AddUnitName("ƽ������");
        AddUnitName("ƽ������");
        AddUnitName("ƽ������");
        AddUnitName("ƽ����");
        AddUnitName("ƽ������");
        AddUnitName("ƽ����");
        AddUnitName("ƽ����");
        AddUnitName("ƽ����");
        AddUnitName("ƽ��ǧ��");
        AddUnitName("ƽ��Ӣ��");
        AddUnitName("ƽ��Ӣ��");
        AddUnitName("ƽ��Ӣ��");
        AddUnitName("ƽ����");
        AddUnitName("��ʽ��");
        AddUnitName("ǧ��");
        AddUnitName("ǧ��");
        AddUnitName("ǧ��");
        AddUnitName("ǧ��");
        AddUnitName("ǧ��");
        AddUnitName("ǧ��");
        AddUnitName("ǧ��");
        AddUnitName("Ǯ");
        AddUnitName("��");
        AddUnitName("�����");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("���϶�");
        AddUnitName("��");
        AddUnitName("ʮ��");
        AddUnitName("ʮ��");
        AddUnitName("ʮ��");
        AddUnitName("ʯ");
        AddUnitName("ʱ");
        AddUnitName("�г�");
        AddUnitName("�н�");
        AddUnitName("����");
        AddUnitName("��Ķ");
        AddUnitName("����");
        AddUnitName("��");
        AddUnitName("�ؿ�˹");
        AddUnitName("��˹��");
        AddUnitName("��");
        AddUnitName("���ĵ�λ");
        AddUnitName("Ͱ");
        AddUnitName("��");
        AddUnitName("���");
        AddUnitName("΢��");
        AddUnitName("΢��");
        AddUnitName("΢��");
        AddUnitName("΢��");
        AddUnitName("Τ");
        AddUnitName("Τ��");
        AddUnitName("��");
        AddUnitName("������");
        AddUnitName("����");
        AddUnitName("Сʱ");
        AddUnitName("Ӣ��");
        AddUnitName("Ӣ��");
        AddUnitName("Ӣ��");
        AddUnitName("Ӣ��");
        AddUnitName("Ӣ��");
        AddUnitName("ӢĶ");
        AddUnitName("ӢǮ");
        AddUnitName("Ӣʯ");
        AddUnitName("ӢѰ");
        AddUnitName("Ӣ������");
        AddUnitName("��");
        AddUnitName("��");
        AddUnitName("����");
        AddUnitName("ת");

        // ��¼��־
        Log.LogMessage("CoreContent", "AddAllUnitNames", "��λ�Ѽ��أ�");
    }
}