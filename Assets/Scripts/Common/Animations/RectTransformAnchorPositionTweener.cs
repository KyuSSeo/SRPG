using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//  RectTransform 참조 유지
public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    RectTransform rt;
    private void Awake()
    {
        rt = transform as RectTransform;
    }

    // 현재 작업된 위치 정보를 anchoredPosition으로 전달
    protected override void OnUpdate()
    {
        base.OnUpdate();
        rt.anchoredPosition = currentTweenValue;
    }
}
