using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  이동 목표 타일을 선택

public class MoveTargetState : BattleState
{
    List<Tile> tiles;

    public override void Enter()
    {
        base.Enter();
        Movement mover = turn.actor.GetComponent<Movement>();
        tiles = mover.GetTilesInRange(board);
        board.SelectTiles(tiles);
        RefreshPrimaryStatPanel(pos);
    }

    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        tiles = null;
        statPanelController.HidePrimary();
    }

    // 이동 입력 발생 시 현재 위치(pos)에 입력 방향(e.info)를 더해 새로운 타일 선택
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
        RefreshPrimaryStatPanel(pos);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (tiles.Contains(owner.currentTile))
                owner.ChangeState<MoveSequenceState>();
        }
        else
        {
            owner.ChangeState<CommandSelectionState>();
        }
    }
}