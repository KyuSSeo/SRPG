using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  턴 정보 관리
public class Turn
{
    #region Public
    //  턴 수행 유닛
    public Unit actor;
    //  움직였는가?
    public bool hasUnitMoved;
    //  행동했는가?
    public bool hasUnitActed;
    //  이동이 가능한가? (이동 불가능한 이유가 있나?)
    public bool lockMove;
    public GameObject ability;
    #endregion


    # region Private
    //  턴 시작 시 타일
    private Tile startTile;
    //  턴 시작 시 방향
    private Directions startDir;
    #endregion
    // 턴 시작 시 호출
    public void Change(Unit current)
    {
        //  호출 시 유닛의 상태
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        startTile = actor.tile;
        startDir = actor.dir;
    }

    
    //  움직임 되돌리기
    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
}