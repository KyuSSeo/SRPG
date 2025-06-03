using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteStatusEffect : StatusEffect
{
    private State myState;

    //  �˸� ���
    private void OnEnable()
    {
        myState = GetComponentInParent<State>();
        if (myState)
            this.AddObserver(OnCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), myState);
    }
    //  �˸� ��� ����
    private void OnDisable()
    {
        this.RemoveObserver(OnCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), myState);
    }
    //  �� �ӵ��� �ٸ��� ī��Ʈ�� ��ġ ����
    private void OnCounterWillChange(object sender, object args)
    {
        ValueChangeException exc = args as ValueChangeException;
        MultDeltaModifier m = new MultDeltaModifier(0, 2);
        exc.AddModifier(m);
    }
}
