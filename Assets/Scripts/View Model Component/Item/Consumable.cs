using UnityEngine;
using System.Collections;
public class Consumable : MonoBehaviour
{
    //  아이템 사용, 소모
    public void Consume(GameObject target)
    {
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Apply(target);
    }
}