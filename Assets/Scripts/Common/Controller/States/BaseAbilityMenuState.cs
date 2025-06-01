using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ɷ� �޴� ����
public abstract class BaseAbilityMenuState : BattleState
{
    //  �޴� �׸��
    protected string menuTitle;
    protected List<string> menuOptions;

    public override void Enter()
    {
        //  ���� �� �޴� �ҷ�����
        base.Enter();
        SelectTile(turn.actor.tile.pos);
        LoadMenu();
    }

    public override void Exit()
    {
        //  ���� Ż�� �� �޴� �����
        base.Exit();
        abilityMenuPanelController.Hide();
    }

    // Ȯ��/��� �Է� ó��
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
            //  Ȯ��
            Confirm();
        else
            //  ���, ���
            Cancel();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.x > 0 || e.info.y < 0)
            abilityMenuPanelController.Next();
        else
            abilityMenuPanelController.Previous();
    }

    //  �޴� �ε�
    protected abstract void LoadMenu();
    //  �޴� Ȯ��, ����
    protected abstract void Confirm();
    //  �޴� ���, ���
    protected abstract void Cancel();
}
