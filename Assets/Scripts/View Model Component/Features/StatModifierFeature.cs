using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �ɷ�ġ ����
public class StatModifierFeature : Features
{
    //  ������ �ɷ�ġ, ��ȭ��
    public StatTypes type;
    public int amount;

    private Stats stats { get { return _target.GetComponentInParent<Stats>(); } }

    //  ����
    protected override void OnApply()
    {
        stats[type] += amount;
    }

    //  ����
    protected override void OnRemove()
    {
        stats[type] -= amount;
    }
}
