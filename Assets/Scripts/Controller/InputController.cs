using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    const float _repeatThreshold = 0.5f;
    const float _repeatRate = 0.25f;
    const float _tapRate = 0.1f;
    float _horNext, _verNext;
    bool _horHold, _verHold;
    //  ��ư ����
    string[] _buttons = new string[] { "Fire1", "Fire2", "Fire3" };

    //  �̺�Ʈ
    public static event EventHandler<InfoEventArgs<int>> fireEvent;
    public static event EventHandler<InfoEventArgs<Point>> moveEvent;

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        int x = 0, y = 0;
        //  ����ó��
        if (!Mathf.Approximately(h, 0))
        {
            if (Time.time > _horNext)
            {
                x = (h < 0f) ? -1 : 1;
                _horNext = Time.time + (_horHold ? _repeatRate : _repeatThreshold);
                _horHold = true;
            }
        }
        else
        {
            _horHold = false;
            _horNext = 0;
        }
        //  ���� ó��
        if (!Mathf.Approximately(v, 0))
        {
            if (Time.time > _verNext)
            {
                y = (v < 0f) ? -1 : 1;
                _verNext = Time.time + (_verHold ? _repeatRate : _repeatThreshold);
                _verHold = true;
            }
        }
        else
        {
            _verHold = false;
            _verNext = 0;
        }

        //  ���� �̵� �̺�Ʈ ����
        if (x != 0 || y != 0)
            Move(new Point(x, y));


        //  ��ư �̺�Ʈ
        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetButtonUp(_buttons[i]))
                Fire(i);
        }
    }


    private void Fire(int i)
    {
        if (fireEvent != null)
            fireEvent(this, new InfoEventArgs<int>(i));
    }

    private void Move(Point p)
    {
        if (moveEvent != null)
            moveEvent(this, new InfoEventArgs<Point>(p));
    }
}
