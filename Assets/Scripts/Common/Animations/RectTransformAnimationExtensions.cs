using UnityEngine;
using System;
using System.Collections;

//  RectTransform 확장 메서드
public static class RectTransformAnimationExtensions
{
    public static Tweener AnchorTo(this RectTransform t, Vector3 position)
    {
        return AnchorTo(t, position, Tweener.DefaultDuration);
    }

    public static Tweener AnchorTo(this RectTransform t, Vector3 position, float duration)
    {
        return AnchorTo(t, position, duration, Tweener.DefaultEquation);
    }

    public static Tweener AnchorTo(this RectTransform t, Vector3 position, float duration, Func<float, float, float, float> equation)
    {
        RectTransformAnchorPositionTweener tweener = t.gameObject.AddComponent<RectTransformAnchorPositionTweener>();

        //  시작 위치, 끝 위치 설정
        tweener.startTweenValue = t.anchoredPosition;
        tweener.endTweenValue = position;

        //  지속 시간과 속도 곡선 설정
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play();

        //  애니메이션 제어 Tweener 전달 
        return tweener;
    }
}
