using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  능력 효과 영역 등을 표시
public class ConfirmAbilityTargetState : BattleState
{
    //  타일
    private List<Tile> tiles;
    //  능력 범위 타일
    private AbilityArea aArea;
    private AbilityEffectTarget[] targeters;
    private int index = 0;
    public override void Enter()
    {
        base.Enter();   
        aArea = turn.ability.GetComponent<AbilityArea>();
        tiles = aArea.GetTilesInArea(board, pos);
        board.SelectTiles(tiles);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.tile.pos);
        if (turn.targets.Count > 0)
        {
            hitSuccessIndicator.Show();
            SetTarget(0);
        }
    }
    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
        hitSuccessIndicator.Hide();
    }

    //  타일 이동
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.y > 0 || e.info.x > 0)
            SetTarget(index + 1);
        else
            SetTarget(index - 1);
    }
    //  입력, 취소 처리
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

    //  능력 타깃 타일 탐색
    private void FindTargets()
    {
        turn.targets = new List<Tile>();
        targeters = turn.ability.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < tiles.Count; ++i)
            if (IsTarget(tiles[i], targeters))
                turn.targets.Add(tiles[i]);
    }
    
    //  조건을 만족하는지 여부
    private bool IsTarget(Tile tile, AbilityEffectTarget[] list)
    {
        for (int i = 0; i < list.Length; ++i)
            if (list[i].IsTarget(tile))
                return true;

        return false;
    }
    //  대상 타일 표시
    private void SetTarget(int target)
    {
        index = target;
        if (index < 0)
            index = turn.targets.Count - 1;
        if (index >= turn.targets.Count)
            index = 0;

        // 타겟이 존재하면 패널 표시
        if (turn.targets.Count > 0)
        {
            RefreshSecondaryStatPanel(turn.targets[index].pos);
            UpdateHitSuccessIndicator();
        }
    }
    private void UpdateHitSuccessIndicator()
    {
        int chance = 0;
        int amount = 0;
        Tile target = turn.targets[index];

        for (int i = 0; i < targeters.Length; ++i)
        {
            if (targeters[i].IsTarget(target))
            {
                HitRate hitRate = targeters[i].GetComponent<HitRate>();
                chance = hitRate.Calculate(target);

                BaseAbilityEffect effect = targeters[i].GetComponent<BaseAbilityEffect>();
                amount = effect.Predict(target);
                break;
            }
        }

        hitSuccessIndicator.SetStats(chance, amount);
    }
}