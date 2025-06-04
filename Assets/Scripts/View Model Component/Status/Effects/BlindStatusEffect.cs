using UnityEngine;
using System.Collections;

public class BlindStatusEffect : StatusEffect
{
    //  �̺�Ʈ ���
    private void OnEnable()
    {
        this.AddObserver(OnHitRateStatusCheck, HitRate.StatusCheckNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnHitRateStatusCheck, HitRate.StatusCheckNotification);
    }

    private void OnHitRateStatusCheck(object sender, object args)
    {
        Info<Unit, Unit, int> info = args as Info<Unit, Unit, int>;
        Unit owner = GetComponentInParent<Unit>();
        //  ������ �Ǹ�
        if (owner == info.arg0)
        {
            info.arg2 += 50;
        }
        //  ������ �Ǹ�
        else if (owner == info.arg1)
        {
            info.arg2 -= 20;
        }
    }
}