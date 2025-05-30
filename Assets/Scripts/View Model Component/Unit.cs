using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  보드에서 해당 유닛이 어느 타일에 배치되어 있는지
//  어떤 방향을 보고 있는지 확인하는 스크립트
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;


    //  유닛을 타일 위에 배치, 타일과의 관계 정리, 재설정
    public void Place(Tile target)
    {
        // 이전 타일이 이 유닛을 content로 가지고 있다면 연결 해제
        if (tile != null && tile.contents == gameObject)
            tile.contents = null;

        // 새 타일로 유닛의 참조를 갱신
        tile = target;
        // 새 타일이 존재하면, 해당 타일의 content에 이 유닛 등록
        if (target != null)
            target.contents = gameObject;
    }

    //  타일 중앙에 위치, 방향을 바라보도록 설정
    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}
