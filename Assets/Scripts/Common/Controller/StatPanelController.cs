using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelController : MonoBehaviour
{
    private const string ShowKey = "Show";
    private const string HideKey = "Hide";

    // 표시할 객체
    [SerializeField] StatPanel primaryPanel;
    [SerializeField] StatPanel secondaryPanel;

    //  애니메이션
    private Tweener primaryTransition;
    private Tweener secondaryTransition;

    private void Start()
    {
        //  숨긴 상태 시작
        if (primaryPanel.panel.CurrentPosition == null)
            primaryPanel.panel.SetPosition(HideKey, false);
        if (secondaryPanel.panel.CurrentPosition == null)
            secondaryPanel.panel.SetPosition(HideKey, false);
    }
    //  보이기
    public void ShowPrimary(GameObject obj)
    {
        primaryPanel.Display(obj);
        MovePanel(primaryPanel, ShowKey, ref primaryTransition);
    }
    public void ShowSecondary(GameObject obj) 
    {
        secondaryPanel.Display(obj);
        MovePanel(secondaryPanel, ShowKey, ref secondaryTransition);
    }

    //  숨기기
    public void HidePrimary()
    {
        MovePanel(primaryPanel, HideKey, ref primaryTransition);
    }
    public void HideSecondary() 
    {
        MovePanel(secondaryPanel, HideKey, ref secondaryTransition);
    }
    //  움직임 애니메이션
    private void MovePanel(StatPanel obj, string pos, ref Tweener t)
    {
        //  위치 설정하기
        Panel.Position target = obj.panel[pos];
        //  위치가 다를 때 애니메이션 실행
        if (obj.panel.CurrentPosition != target)
        {
            // 애니메이션 중첩 방지
            if (t != null && t.easingControl != null)
                t.easingControl.Stop();
            
            //  애니메이션 실행
            t = obj.panel.SetPosition(pos, true);
            t.easingControl.duration = 0.5f;
            t.easingControl.equation = EasingEquations.EaseOutQuad;
        }
    }
}
