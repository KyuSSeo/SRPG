using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  능력치용 코드
public class Stats : MonoBehaviour
{
    //  알림 캐싱
    static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    //  배열을 통해 능력치 관리
    private int[] _data = new int[(int)StatTypes.Count];
    public int this[StatTypes s]
    {
        get { return _data[(int)s]; }
        set { SetValue(s, value, true); }
    }

    // 알림    문자열 변환
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
        //  값이 같으면 무시
        int oldValue = this[type];
        if (oldValue == value)
            return;

        if (allowExceptions)
        {
            // 외부에서 예외 처리
            ValueChangeException exc = new ValueChangeException(oldValue, value);

            // 변경 전 알림 발송
            this.PostNotification(WillChangeNotification(type), exc);

            // 값 변경
            value = Mathf.FloorToInt(exc.GetModifiedValue());

            // 변경 무효화 기능
            if (exc.toggle == false || value == oldValue)
                return;
        }

        _data[(int)type] = value;
        //  변경 완료 알림
        this.PostNotification(DidChangeNotification(type), oldValue);
    }
}
