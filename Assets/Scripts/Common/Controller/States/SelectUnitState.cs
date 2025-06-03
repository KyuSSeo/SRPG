using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ������ �������� ���� ����
public class SelectUnitState : BattleState
{
    //  �ʱ� ���� ���� ����
    int index = -1;

    public override void Enter()
    {
        //  ���� ���� �ñ⿡ �Ʒ� �ڷ�ƾ ����
        base.Enter();
        StartCoroutine("ChangeCurrentUnit");
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    IEnumerator ChangeCurrentUnit()
    {
        //  ���� ����
        index = (index + 1) % units.Count;
        // ���� �� ���� ����
        turn.Change(units[index]);
        RefreshPrimaryStatPanel(pos);
        yield return null;
        //  ��� ���·� �̵�
        owner.ChangeState<CommandSelectionState>();
    }
}
