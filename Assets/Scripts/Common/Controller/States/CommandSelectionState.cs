using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  턴 시작시 메뉴표시 이동, 행동, 대기 선택
public class CommandSelectionState : BaseAbilityMenuState
{

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

        abilityMenuPanelController.Show(menuTitle, menuOptions);
        abilityMenuPanelController.SetLocked(0, turn.hasUnitMoved);
        abilityMenuPanelController.SetLocked(1, turn.hasUnitActed);
    }

    //  확인
    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0: // Move
                owner.ChangeState<MoveTargetState>();
                break;
            case 1: // Action
                owner.ChangeState<CategorySelectionState>();
                break;
            case 2: // Wait
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }
    //  취소, 대기
    protected override void Cancel()
    {
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
