using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    // Mp정보 가져오기
    private Unit unit;
    private Stats stats;
    public int MP
    {
        get { return stats[StatTypes.MP]; }
        set { stats[StatTypes.MP] = value; }
    }

    public int MMP
    {
        get { return stats[StatTypes.MMP]; }
        set { stats[StatTypes.MMP] = value; }
    }

    //  컴포넌트 가저옴
    private void Awake()
    {
        stats = GetComponent<Stats>();
        unit = GetComponent<Unit>();
    }
    //  알림 등록
    private void OnEnable()
    {
        this.AddObserver(OnMPWillChange, Stats.WillChangeNotification(StatTypes.MP), stats);
        this.AddObserver(OnMMPDidChange, Stats.DidChangeNotification(StatTypes.MMP), stats);
        this.AddObserver(OnTurnBegan, TurnOrderController.TurnBeganNotification, unit);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnMPWillChange, Stats.WillChangeNotification(StatTypes.MP), stats);
        this.RemoveObserver(OnMMPDidChange, Stats.DidChangeNotification(StatTypes.MMP), stats);
        this.RemoveObserver(OnTurnBegan, TurnOrderController.TurnBeganNotification, unit);
    }

    // Mp변화
    private void OnMPWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, 0, stats[StatTypes.MHP]));
    }

    private void OnMMPDidChange(object sender, object args)
    {
        int oldMMP = (int)args;
        if (MMP > oldMMP)
            MP += MMP - oldMMP;
        else
            MP = Mathf.Clamp(MP, 0, MMP);
    }

    //  턴 시작시 10% Mp 반환
    private void OnTurnBegan(object sender, object args)
    {
        if (MP < MMP)
            MP += Mathf.Max(Mathf.FloorToInt(MMP * 0.1f), 1);
    }
}
