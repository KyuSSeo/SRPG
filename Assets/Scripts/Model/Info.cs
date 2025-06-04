using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info<T0>
{
    // 하나의 인자 전달
    public T0 arg0;
    public Info(T0 arg0)
    {
        this.arg0 = arg0;
    }
}

// 인자 둘 전달
public class Info<T0, T1> : Info<T0>
{
    public T1 arg1;
    public Info(T0 arg0, T1 arg1) : base(arg0)
    {
        this.arg1 = arg1;
    }
}

// 인자 셋 전달
public class Info<T0, T1, T2> : Info<T0, T1>
{
    public T2 arg2;
    public Info(T0 arg0, T1 arg1, T2 arg2) : base(arg0, arg1)
    {
        this.arg2 = arg2;
    }
}