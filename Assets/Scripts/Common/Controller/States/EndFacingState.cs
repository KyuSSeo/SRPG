using UnityEngine;
using System.Collections;


//  최종 방향 결정
public class EndFacingState : BattleState
{
    //  방향 결정 시작 순간 방향
    private Directions startDir;
    public override void Enter()
    {
        base.Enter();
        startDir = turn.actor.dir;
        SelectTile(turn.actor.tile.pos);
    }
    // 방향키로 방향 전환
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        turn.actor.dir = e.info.GetDirection();
        turn.actor.Match();
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        switch (e.info)
        {
            //  확인 / 취소
            case 0:
                owner.ChangeState<SelectUnitState>();
                break;
            case 1:
                turn.actor.dir = startDir;
                turn.actor.Match();
                owner.ChangeState<CommandSelectionState>();
                break;
        }
    }
}