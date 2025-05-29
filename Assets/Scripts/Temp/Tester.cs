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
    public void OnMoveEvent(object sender, InfoEventArgs<Point> e)
    {
        Debug.Log($"Move Event Received: Direction = ({e.info.x}, {e.info.y})");
    }

    public void OnFireEvent(object sender, InfoEventArgs<int> e)
    {
        Debug.Log($"Fire Event Received: Button Index = {e.info}");
    }
}
