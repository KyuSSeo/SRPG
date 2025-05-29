using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  상태 참조 유지 및, 다른 상태로 전환을 하는 스크립트
public class StateMachine : MonoBehaviour
{
    protected State _currentState;
    protected bool _inTransition;

    public virtual State CurrentState {
        get { return _currentState; }
        set { Trensition(value); }
    }

    public virtual T GetState<T> () where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }
    public virtual void ChangeState<T> () where T : State
    {
        CurrentState = GetState<T>();
    }
    protected virtual void Trensition (State value)
    {
        //  동일한 상태로 전환 시도 시 종료
        if (_currentState == value || _inTransition)
            return;

        _inTransition = true;
        if (_currentState != null)
            _currentState.Exit();

        _currentState = value;

        if (_currentState != null)
            _currentState.Enter();
        _inTransition=false;
    }
}
