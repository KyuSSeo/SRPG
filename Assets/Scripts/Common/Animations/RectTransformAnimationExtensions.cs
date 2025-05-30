using UnityEngine;
using System;
using System.Collections;

//  RectTransform Ȯ�� �޼���
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
        
        //  ���� ��ġ, �� ��ġ ����
        tweener.startValue = t.anchoredPosition;
        tweener.endValue = position;

        //  ���� �ð��� �ӵ� � ����
        tweener.easingControl.duration = duration;
        tweener.easingControl.equation = equation;
        tweener.easingControl.Play();

        //  �ִϸ��̼� ���� Tweener ���� 
        return tweener;
    }
}
