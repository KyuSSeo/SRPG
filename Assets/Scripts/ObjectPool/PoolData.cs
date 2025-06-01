using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 풀의 정보를 저장
public class PoolData
{
    public GameObject prefab;
    public int maxCount;
    public Queue<Poolable> pool;
}
