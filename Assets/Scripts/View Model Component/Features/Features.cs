using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Features : MonoBehaviour
{
    protected GameObject _target { get; private set; }


    //  �������� Ȱ��ȭ, ��Ȱ��ȭ
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

    //  �ɷ�ġ�� ��ȭ��Ű�� �Ҹ�ǰ
    public void Apply(GameObject target)
    {
        _target = target;
        OnApply();
        _target = null;
    }

    protected abstract void OnApply();
    protected virtual void OnRemove() { }
}
