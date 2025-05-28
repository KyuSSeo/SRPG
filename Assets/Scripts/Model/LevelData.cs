using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 보드 정보의 읽고 쓰기를 매개하는 '데이터 원본' 역할을 하는 스크립트

public class LevelData : ScriptableObject
{
    public List<Vector3> tiles;
}
