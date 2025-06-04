using UnityEngine;
using System.Collections;
public class StatusCondition : MonoBehaviour
{
    //  상태를 제거
    public virtual void Remove()
    {
        Status s = GetComponentInParent<Status>();
        if (s)
            s.Remove(this);
    }
}