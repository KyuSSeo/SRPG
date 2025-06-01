using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  �ൿ���� ��� �ൿ�� �� ������ ����
public class CategorySelectionState : BaseAbilityMenuState
{
    //  �ൿ ���� �Ŵ�
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
        // �޴� ǥ��
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }

    //  �ൿ ���� �Ŵ� �� �������� ��
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

    //  ���� ���·� ���ư���
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    // �⺻ ���� ����
    private void Attack()
    {
        //  �̵����� ��� ��ġ ����
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        owner.ChangeState<CommandSelectionState>();
    }

    //  ���� �� �ൿ �����ϴ� ����Ʈ����
    private void SetCategory(int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}