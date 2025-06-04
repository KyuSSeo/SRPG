using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ������ �������� ���� ����
public class SelectUnitState : BattleState
{
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

    private IEnumerator ChangeCurrentUnit()
    {
        //  ���� ����
        //  index = (index + 1) % units.Count;
        //  ���� �� ���� ����
        //  turn.Change(units[index]);
        
        //  ���� ��� �� ����
        owner.round.MoveNext();
        SelectTile(turn.actor.tile.pos);
        RefreshPrimaryStatPanel(pos);
        yield return null;
        //  ��� ���·� �̵�
        owner.ChangeState<CommandSelectionState>();
    }
}
