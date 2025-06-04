using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  �ɷ�ġ�� �ڵ�
public class Stats : MonoBehaviour
{
    //  �˸� ĳ��
    static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    //  �迭�� ���� �ɷ�ġ ����
    private int[] _data = new int[(int)StatTypes.Count];
    public int this[StatTypes s]
    {
        get { return _data[(int)s]; }
        set { SetValue(s, value, true); }
    }

    // �˸�    ���ڿ� ��ȯ
    public static string WillChangeNotification(StatTypes type)
    {
        if (!_willChangeNotifications.ContainsKey(type))
            _willChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        return _willChangeNotifications[type];
    }

    public static string DidChangeNotification(StatTypes type)
    {
        if (!_didChangeNotifications.ContainsKey(type))
            _didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        return _didChangeNotifications[type];
    }

    public void SetValue(StatTypes type, int value, bool allowExceptions)
    {
        //  ���� ������ ����
        int oldValue = this[type];
        if (oldValue == value)
            return;

        if (allowExceptions)
        {
            // �ܺο��� ���� ó��
            ValueChangeException exc = new ValueChangeException(oldValue, value);

            // ���� �� �˸� �߼�
            this.PostNotification(WillChangeNotification(type), exc);

            // �� ����
            value = Mathf.FloorToInt(exc.GetModifiedValue());

            // ���� ��ȿȭ ���
            if (exc.toggle == false || value == oldValue)
                return;
        }

        _data[(int)type] = value;
        //  ���� �Ϸ� �˸�
        this.PostNotification(DidChangeNotification(type), oldValue);
    }
}
