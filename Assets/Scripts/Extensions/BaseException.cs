using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 턴, 객체의 상황에 따른 예외처리  
public class BaseException
{
    public bool toggle { get; private set; }
    private bool defaultToggle;

    public BaseException (bool defaultToggle)
    {
        this.defaultToggle = defaultToggle;
        toggle = defaultToggle;
    }

    public void FlipToggle()
    {
        toggle = !defaultToggle;
    }
}
