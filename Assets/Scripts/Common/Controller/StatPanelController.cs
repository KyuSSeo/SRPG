using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanelController : MonoBehaviour
{
    private const string ShowKey = "Show";
    private const string HideKey = "Hide";

    // ǥ���� ��ü
    [SerializeField] StatPanel primaryPanel;
    [SerializeField] StatPanel secondaryPanel;

    //  �ִϸ��̼�
    private Tweener primaryTransition;
    private Tweener secondaryTransition;

    private void Start()
    {
        //  ���� ���� ����
        if (primaryPanel.panel.CurrentPosition == null)
            primaryPanel.panel.SetPosition(HideKey, false);
        if (secondaryPanel.panel.CurrentPosition == null)
            secondaryPanel.panel.SetPosition(HideKey, false);
    }
    //  ���̱�
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

    //  �����
    public void HidePrimary()
    {
        MovePanel(primaryPanel, HideKey, ref primaryTransition);
    }
    public void HideSecondary() 
    {
        MovePanel(secondaryPanel, HideKey, ref secondaryTransition);
    }
    //  ������ �ִϸ��̼�
    private void MovePanel(StatPanel obj, string pos, ref Tweener t)
    {
        //  ��ġ �����ϱ�
        Panel.Position target = obj.panel[pos];
        //  ��ġ�� �ٸ� �� �ִϸ��̼� ����
        if (obj.panel.CurrentPosition != target)
        {
            // �ִϸ��̼� ��ø ����
            if (t != null && t.easingControl != null)
                t.easingControl.Stop();
            
            //  �ִϸ��̼� ����
            t = obj.panel.SetPosition(pos, true);
            t.easingControl.duration = 0.5f;
            t.easingControl.equation = EasingEquations.EaseOutQuad;
        }
    }
}
