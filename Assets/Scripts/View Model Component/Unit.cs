using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  보드에서 해당 유닛이 어느 타일에 배치되어 있는지
//  어떤 방향을 보고 있는지 확인하는 스크립트
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;

    public void Place(Tile target)
    {
        if (tile != null && tile.contents == gameObject)
            tile.contents = null;
        tile = target;

        if (target != null)
            target.contents = gameObject;
    }
    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}
