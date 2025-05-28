using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    public void OnEnable()
    {
        InputController.moveEvent += OnMoveEvent;
        InputController.fireEvent += OnFireEvent;
    }
    public void OnDisable()
    {
        InputController.moveEvent -= OnMoveEvent;
        InputController.fireEvent -= OnFireEvent;
    }
    public void OnMoveEvent(object aender, InfoEventArgs<Point> e) 
    {
        throw new System.NotImplementedException();
    }
    public void OnFireEvent(object aender, InfoEventArgs<int> a)
    {
        throw new System.NotImplementedException();
    }
}
