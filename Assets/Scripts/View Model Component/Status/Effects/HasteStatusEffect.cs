using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteStatusEffect : StatusEffect
{
    private Stats myStats;

    //  알림 등록
    private void OnEnable()
    {
        myStats = GetComponentInParent<Stats>();
        if (myStats)
            this.AddObserver(OnCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), myStats);
    }
    //  알림 등록 해제
    private void OnDisable()
    {
        this.RemoveObserver(OnCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), myStats);
    }
    //  턴 속도를 다르는 카운트에 수치 가산
    private void OnCounterWillChange(object sender, object args)
    {
        ValueChangeException exc = args as ValueChangeException;
        MultDeltaModifier m = new MultDeltaModifier(0, 2);
        exc.AddModifier(m);
    }
}
