using UnityEngine;
using System.Collections;

//  그냥 TPS 할걸, 파판에는 데미지코드가 20단계에 걸쳐서 이뤄진데요.
public abstract class Modifier
{
    //  모든 수정자는 정렬 순서를 가진다
    public readonly int sortOrder;
    public Modifier(int sortOrder)
    {
        this.sortOrder = sortOrder;
    }
}
