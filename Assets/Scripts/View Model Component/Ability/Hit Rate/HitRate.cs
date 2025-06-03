using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitRate : MonoBehaviour
{
    //  명중 관련 알림
    public const string AutomaticHitCheckNotification = "HitRate.AutomaticHitCheckNotification";
    public const string AutomaticMissCheckNotification = "HitRate.AutomaticMissCheckNotification";
    public const string StatusCheckNotification = "HitRate.StatusCheckNotification";
    //  명중 확률
    public abstract int Calculate(Unit attacker, Unit target);

    //  명중 여부 전달
    protected virtual bool AutomaticHit(Unit attacker, Unit target)
    {
        MatchException exc = new MatchException(attacker, target);
        this.PostNotification(AutomaticHitCheckNotification, exc);
        return exc.toggle;
    }
    protected virtual bool AutomaticMiss(Unit attacker, Unit target)
    {
        MatchException exc = new MatchException(attacker, target);
        this.PostNotification(AutomaticMissCheckNotification, exc);
        return exc.toggle;
    }

    //  상태에 영향을 받는 명중
    protected virtual int AdjustForStatusEffects(Unit attacker, Unit target, int rate)
    {
        Info<Unit, Unit, int> args = new Info<Unit, Unit, int>(attacker, target, rate);
        this.PostNotification(StatusCheckNotification, args);
        return args.arg2;
    }

    //  최종 명중률 100
    protected virtual int Final(int evade)
    {
        return 100 - evade;
    }
}
