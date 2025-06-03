using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  행동에서 어떠한 행동을 할 것인지 고르기
public class CategorySelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPrimary(turn.actor.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    //  행동 하위 매뉴
    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuTitle = "Action";
            menuOptions = new List<string>(3);
            menuOptions.Add("Attack");
            menuOptions.Add("White Magic");
            menuOptions.Add("Black Magic");
        }
        // 메뉴 표시
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }

    //  행동 하위 매뉴 중 선택했을 때
    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0:
                Attack();
                break;
            case 1:
                SetCategory(0);
                break;
            case 2:
                SetCategory(1);
                break;
        }
    }

    //  이전 상태로 돌아가기
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    // 기본 공격 수행
    private void Attack()
    {
        //  이동했을 경우 위치 고정
        turn.ability = turn.actor.GetComponentInChildren<AbilityRange>().gameObject;
        owner.ChangeState<AbilityTargetState>();
    }

    //  공격 외 행동 선택하는 리스트상태
    private void SetCategory(int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}