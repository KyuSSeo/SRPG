using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ǯ�� ������ ����
public class PoolData
{
    public GameObject prefab;
    public int maxCount;
    public Queue<Poolable> pool;
}
