using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  이동 목표 타일을 선택
public class MoveTargetState : BattleState
{
    // 이동 입력 발생 시 현재 위치(pos)에 입력 방향(e.info)를 더해 새로운 타일 선택
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }
}

