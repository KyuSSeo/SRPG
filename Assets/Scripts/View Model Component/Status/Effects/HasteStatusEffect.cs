using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteStatusEffect : StatusEffect
{
    private Stats myStats;

    //  �˸� ���
    private void OnEnable()
    {
        myStats = GetComponentInParent<Stats>();
        if (myStats)
            this.AddObserver(OnCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), myStats);
    }
    //  �˸� ��� ����
    private void OnDisable()
    {
        this.RemoveObserver(OnCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), myStats);
    }
    //  �� �ӵ��� �ٸ��� ī��Ʈ�� ��ġ ����
    private void OnCounterWillChange(object sender, object args)
    {
        ValueChangeException exc = args as ValueChangeException;
        MultDeltaModifier m = new MultDeltaModifier(0, 2);
        exc.AddModifier(m);
    }
}
