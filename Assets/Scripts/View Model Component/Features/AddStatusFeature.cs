using UnityEngine;
using System.Collections;
public abstract class AddStatusFeature<T> : Features where T : StatusEffect
{
    private StatusCondition statusCondition;

    protected override void OnApply()
    {
        Status status = GetComponentInParent<Status>();
        statusCondition = status.Add<T, StatusCondition>();
    }

    protected override void OnRemove()
    {
        if (statusCondition != null)
            statusCondition.Remove();
    }
}