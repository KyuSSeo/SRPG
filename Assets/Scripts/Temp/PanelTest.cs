using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelTests : MonoBehaviour
{
    Panel panel;
    // 버튼 이름 정의
    const string Show = "Show";
    const string Hide = "Hide";
    const string Center = "Center";
    void Start()
    {
        panel = GetComponent<Panel>();
        Panel.Position centerPos = new Panel.Position(Center, TextAnchor.MiddleCenter, TextAnchor.MiddleCenter);
        panel.AddPosition(centerPos);
    }
    void OnGUI()
    {
        // "Show" 버튼 클릭
        if (GUI.Button(new Rect(10, 10, 100, 30), Show))
            panel.SetPosition(Show, true);
        // "Hide" 버튼 클릭 
        if (GUI.Button(new Rect(10, 50, 100, 30), Hide))
            panel.SetPosition(Hide, true);
        // "Center" 버튼 클릭 시 Center 위치로 이동, EaseInOutBack적용
        if (GUI.Button(new Rect(10, 90, 100, 30), Center))
        {
            Tweener t = panel.SetPosition(Center, true);
            t.equation = EasingEquations.EaseInOutBack;
        }
    }
}