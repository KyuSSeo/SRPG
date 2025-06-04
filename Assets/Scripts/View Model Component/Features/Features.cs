using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Features : MonoBehaviour
{
    protected GameObject _target { get; private set; }


    //  부착물의 활성화, 비활성화
    public void Activate(GameObject target)
    {
        if (_target == null)
        {
            _target = target;
            OnApply();
        }
    }
    public void Deactivate()
    {
        if (_target != null)
        {
            OnRemove();
            _target = null;
        }
    }

    //  능력치를 변화시키는 소모품
    public void Apply(GameObject target)
    {
        _target = target;
        OnApply();
        _target = null;
    }

    protected abstract void OnApply();
    protected virtual void OnRemove() { }
}
