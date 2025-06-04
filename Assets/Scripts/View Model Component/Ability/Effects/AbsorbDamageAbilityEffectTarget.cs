using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

//  피해량에 비례해 자신의 Hp회복
public class AbsorbDamageAbilityEffectTarget : BaseAbilityEffect
{
    public int trackedSiblingIndex;
    //  피해양
    private BaseAbilityEffect effect;
    //  회복량
    private int amount;

    private void Awake()
    {
        effect = GetTrackedEffect();
    }

    //  알림에 등록, 해제
    private void OnEnable()
    {
        //명중 / 빗나감 알림
        this.AddObserver(OnEffectHit, BaseAbilityEffect.HitNotification, effect);
        this.AddObserver(OnEffectMiss, BaseAbilityEffect.MissedNotification, effect);
    }
    private void OnDisable()
    {
        this.RemoveObserver(OnEffectHit, BaseAbilityEffect.HitNotification, effect);
        this.RemoveObserver(OnEffectMiss, BaseAbilityEffect.MissedNotification, effect);
    }
    public override int Predict(Tile target)
    {
        return 0;
    }


    //  효과 적용
    protected override int OnApply(Tile target)
    {
        //  Hp 회복
        Stats s = GetComponentInParent<Stats>();
        s[StatTypes.HP] += amount;
        return amount;
    }

    //  피해양을 회복양에 저장
    private void OnEffectHit(object sender, object args)
    {
        amount = (int)args;
    }

    //  빗나감으로 회복x
    private void OnEffectMiss(object sender, object args)
    {
        amount = 0;
    }

    private BaseAbilityEffect GetTrackedEffect()
    {
        Transform owner = GetComponentInParent<Ability>().transform;
        if (trackedSiblingIndex >= 0 && trackedSiblingIndex < owner.childCount)
        {
            Transform sibling = owner.GetChild(trackedSiblingIndex);
            return sibling.GetComponent<BaseAbilityEffect>();
        }
        return null;
    }
}
