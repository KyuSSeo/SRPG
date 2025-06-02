using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  능력치 수정
public class StatModifierFeature : Features
{
    //  수정할 능력치, 변화량
    public StatTypes type;
    public int amount;

    private Stats stats { get { return _target.GetComponentInParent<Stats>(); } }

    //  적용
    protected override void OnApply()
    {
        stats[type] += amount;
    }

    //  해제
    protected override void OnRemove()
    {
        stats[type] -= amount;
    }
}
