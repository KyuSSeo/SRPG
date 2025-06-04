using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitRate : MonoBehaviour
{
    //  명중 관련 알림
    public const string AutomaticHitCheckNotification = "HitRate.AutomaticHitCheckNotification";
    public const string AutomaticMissCheckNotification = "HitRate.AutomaticMissCheckNotification";
    public const string StatusCheckNotification = "HitRate.StatusCheckNotification";
   
    protected Unit attacker;
    
    //  명중 확률
    public abstract int Calculate(Tile target);

    public virtual bool RollForHit(Tile target)
    {
        int roll = Random.Range(0, 101);
        int chance = Calculate(target);
        return roll <= chance;
    }

    protected virtual void Start()
    {
        attacker = GetComponentInParent<Unit>();
    }

    //  명중 여부 전달
    protected virtual bool AutomaticHit(Unit target)
    {
        MatchException exc = new MatchException(attacker, target);
        this.PostNotification(AutomaticHitCheckNotification, exc);
        return exc.toggle;
    }
    protected virtual bool AutomaticMiss(Unit target)
    {
        MatchException exc = new MatchException(attacker, target);
        this.PostNotification(AutomaticMissCheckNotification, exc);
        return exc.toggle;
    }

    //  상태에 영향을 받는 명중
    protected virtual int AdjustForStatusEffects(Unit target, int rate)
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
