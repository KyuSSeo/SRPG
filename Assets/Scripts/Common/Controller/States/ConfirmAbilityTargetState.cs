using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  �ɷ� ȿ�� ���� ���� ǥ��
public class ConfirmAbilityTargetState : BattleState
{
    //  Ÿ��
    private List<Tile> tiles;
    //  �ɷ� ���� Ÿ��
    private AbilityArea aArea;
    private int index = 0;
    public override void Enter()
    {
        base.Enter();
        aArea = turn.ability.GetComponent<AbilityArea>();
        tiles = aArea.GetTilesInArea(board, pos);
        board.SelectTiles(tiles);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.tile.pos);
        SetTarget(0);
    }
    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }

    //  Ÿ�� �̵�
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.y > 0 || e.info.x > 0)
            SetTarget(index + 1);
        else
            SetTarget(index - 1);
    }
    //  �Է�, ��� ó��
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (turn.targets.Count > 0)
            {
                owner.ChangeState<PerformAbilityState>();
            }
        }
        else
            owner.ChangeState<AbilityTargetState>();
    }

    //  �ɷ� Ÿ�� Ÿ�� Ž��
    private void FindTargets()
    {
        turn.targets = new List<Tile>();
        AbilityEffectTarget[] targeters = turn.ability.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < tiles.Count; ++i)
            if (IsTarget(tiles[i], targeters))
                turn.targets.Add(tiles[i]);
    }
    
    //  ������ �����ϴ��� ����
    private bool IsTarget(Tile tile, AbilityEffectTarget[] list)
    {
        for (int i = 0; i < list.Length; ++i)
            if (list[i].IsTarget(tile))
                return true;

        return false;
    }
    //  ��� Ÿ�� ǥ��
    private void SetTarget(int target)
    {
        index = target;
        if (index < 0)
            index = turn.targets.Count - 1;
        if (index >= turn.targets.Count)
            index = 0;

        // Ÿ���� �����ϸ� �г� ǥ��
        if (turn.targets.Count > 0)
            RefreshSecondaryStatPanel(turn.targets[index].pos);
    }
}