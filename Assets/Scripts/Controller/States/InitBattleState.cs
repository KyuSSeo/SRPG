using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 전투 시작 시 초기 보드 설정 및 타일을 처리
public class InitBattleState :BattleState
{
    // 진입 시 기본 처리 후 코루틴 동작
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    // 초기화 코루틴: 맵 로드, 초기 타일 선택, 다음 상태 전환을 순차적으로 수행
    // 맵 생성이 끝날 때 까지 기다뎌야 함.
    IEnumerator Init()
    {   
        //  타일 생성
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        //  초기 타일 선택
        SelectTile(p);
        yield return null;
        //  맵 초기화 완료 후 상태 전환
        owner.ChangeState<MoveTargetState>();
    }
}
