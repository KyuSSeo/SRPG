using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//  RectTransform ���� ����
public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    RectTransform rt;
    private void Awake()
    {
        rt = transform as RectTransform;
    }

    // ���� �۾��� ��ġ ������ anchoredPosition���� ����
    protected override void OnUpdate()
    {
        base.OnUpdate();
        rt.anchoredPosition = currentTweenValue;
    }
}
