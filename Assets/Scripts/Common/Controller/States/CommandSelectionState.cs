using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  턴 시작시 메뉴표시 이동, 행동, 대기 선택
public class CommandSelectionState : BaseAbilityMenuState
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
    //  메뉴 로딩
    protected override void LoadMenu()
    {
        if (menuOptions == null)
        {
            menuTitle = "Commands";
            menuOptions = new List<string>(3);
            menuOptions.Add("Move");
            menuOptions.Add("Action");
            menuOptions.Add("Wait");
        }
        // 메뉴 표시
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        // 행동한 경우 비활성화
        abilityMenuPanelController.SetLocked(0, turn.hasUnitMoved);
        abilityMenuPanelController.SetLocked(1, turn.hasUnitActed);
    }

    //  확인
    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0: // 이동 대상 선택 상태
                owner.ChangeState<MoveTargetState>();
                break;
            case 1: // 행동 종류 선택 상태
                owner.ChangeState<CategorySelectionState>();
                break;
            case 2: // 해당 유닛 행동 종료후 다음 유닛 선택 상태
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }
    //  취소, 대기
    protected override void Cancel()

    {   //  이동 되돌리기
        if (turn.hasUnitMoved && !turn.lockMove)
        {
            turn.UndoMove();
            abilityMenuPanelController.SetLocked(0, false);
            SelectTile(turn.actor.tile.pos);
        }
        else
        {   
            owner.ChangeState<ExploreState>();
        }
    }
}
