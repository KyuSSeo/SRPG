using UnityEngine;
using System.Collections;
public class Consumable : MonoBehaviour
{
    //  ������ ���, �Ҹ�
    public void Consume(GameObject target)
    {
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Apply(target);
    }
}