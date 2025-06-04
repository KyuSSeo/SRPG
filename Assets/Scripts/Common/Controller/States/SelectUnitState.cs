using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  유닛을 선택했을 때의 상태
public class SelectUnitState : BattleState
{
    public override void Enter()
    {
        //  상태 진입 시기에 아래 코루틴 실행
        base.Enter();
        StartCoroutine("ChangeCurrentUnit");
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    private IEnumerator ChangeCurrentUnit()
    {
        //  유닛 선택
        //  index = (index + 1) % units.Count;
        //  현재 턴 유닛 지정
        //  turn.Change(units[index]);
        
        //  라운드 기반 턴 관리
        owner.round.MoveNext();
        SelectTile(turn.actor.tile.pos);
        RefreshPrimaryStatPanel(pos);
        yield return null;
        //  명령 상태로 이동
        owner.ChangeState<CommandSelectionState>();
    }
}
