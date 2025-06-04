using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  �� ���۽� �޴�ǥ�� �̵�, �ൿ, ��� ����
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
    //  �޴� �ε�
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
        // �޴� ǥ��
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        // �ൿ�� ��� ��Ȱ��ȭ
        abilityMenuPanelController.SetLocked(0, turn.hasUnitMoved);
        abilityMenuPanelController.SetLocked(1, turn.hasUnitActed);
    }

    //  Ȯ��
    protected override void Confirm()
    {
        switch (abilityMenuPanelController.selection)
        {
            case 0: // �̵� ��� ���� ����
                owner.ChangeState<MoveTargetState>();
                break;
            case 1: // �ൿ ���� ���� ����
                owner.ChangeState<CategorySelectionState>();
                break;
            case 2: // �ش� ���� �ൿ ������ ���� ���� ���� ����
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }
    //  ���, ���
    protected override void Cancel()

    {   //  �̵� �ǵ�����
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
