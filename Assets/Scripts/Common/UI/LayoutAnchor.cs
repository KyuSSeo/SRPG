using UnityEngine;

//  RectTransform�� �ʼ��� �䱸�ϵ��� ��

[RequireComponent(typeof(RectTransform))]

public class LayoutAnchor : MonoBehaviour
{
    //  ����� �Ǵ� RectTransform
    RectTransform myRT;
    RectTransform parentRT;


    //  �ʱ�ȭ
    private void Awake() => Init();
    private void Init()
    {
        myRT = transform as RectTransform;
        parentRT = transform.parent as RectTransform;
        if (parentRT == null)
            Debug.Log("�θ� ���� ��� ����", gameObject);
    }

    //  �־��� TextAnchor �� �������� ��� ��ǥ ���ϱ�
    private Vector2 GetPosition(RectTransform rt, TextAnchor anchor)
    {
        Vector2 retValue = Vector2.zero;

        //  X��ǥ Ȯ��
        switch (anchor)
        {
            case TextAnchor.LowerCenter:
            case TextAnchor.MiddleCenter:
            case TextAnchor.UpperCenter:
                retValue.x += rt.rect.width * 0.5f;
                break;
            case TextAnchor.LowerRight:
            case TextAnchor.MiddleRight:
            case TextAnchor.UpperRight:
                retValue.x += rt.rect.width;
                break;
        }
        //  Y��ǥ Ȯ��
        switch (anchor)
        {
            case TextAnchor.MiddleLeft:
            case TextAnchor.MiddleCenter:
            case TextAnchor.MiddleRight:
                retValue.y += rt.rect.height * 0.5f;
                break;
            case TextAnchor.UpperLeft:
            case TextAnchor.UpperCenter:
            case TextAnchor.UpperRight:
                retValue.y += rt.rect.height;
                break;
        }

        return retValue;
    }


    public Vector2 AnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        //  ���� ��ġ
        Vector2 myOffset = GetPosition(myRT, myAnchor);

        //  �θ� ���� ��ġ
        Vector2 parentOffset = GetPosition(parentRT, parentAnchor);

        //  ��Ŀ �߽�
        Vector2 anchorCenter = new Vector2(
            Mathf.Lerp(myRT.anchorMin.x, myRT.anchorMax.x, myRT.pivot.x),
            Mathf.Lerp(myRT.anchorMin.y, myRT.anchorMax.y, myRT.pivot.y)
            );

        //  ��Ŀ ��ġ ����
        Vector2 myAnchorOffset = new Vector2(
            parentRT.rect.width * anchorCenter.x,
            parentRT.rect.height * anchorCenter.y
            );

        //  �ǹ� ����
        Vector2 myPivotOffset = new Vector2(
            myRT.rect.width * myRT.pivot.x,
            myRT.rect.height * myRT.pivot.y
            );

        //  ���� ��ġ ����
        Vector2 pos = parentOffset - myAnchorOffset - myOffset + myPivotOffset + offset;
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);

        return pos;
    }
    //  UI ���� ��ġ, �θ��� ��Ŀ ���� ��ġ�� ��� �̵�
    public void SnapToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        myRT.anchoredPosition = AnchorPosition(myAnchor, parentAnchor, offset);
    }

    //  UI �ִϸ��̼� ��ġ, �θ��� ��Ŀ ���� ��ġ�� �ε巴�� �̵�
    public Tweener MoveToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        return myRT.AnchorTo(AnchorPosition(myAnchor, parentAnchor, offset));
    }
}
