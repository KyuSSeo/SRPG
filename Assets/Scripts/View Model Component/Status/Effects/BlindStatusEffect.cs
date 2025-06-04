using UnityEngine;
using System.Collections;

public class BlindStatusEffect : StatusEffect
{
    //  이벤트 등록
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
        //  공격자 실명
        if (owner == info.arg0)
        {
            info.arg2 += 50;
        }
        //  수비자 실명
        else if (owner == info.arg1)
        {
            info.arg2 -= 20;
        }
    }
}