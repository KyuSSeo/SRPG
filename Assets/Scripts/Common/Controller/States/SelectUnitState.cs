using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ������ �������� ���� ����
public class SelectUnitState : BattleState
{
    int index = -1;

    public override void Enter()
    {
        base.Enter();
        StartCoroutine("ChangeCurrentUnit");
    }

    IEnumerator ChangeCurrentUnit()
    {
        index = (index + 1) % units.Count;
        turn.Change(units[index]);
        yield return null;
        owner.ChangeState<CommandSelectionState>();
    }

    //  TODO : �����
    //  ��ģ���� ���߷��� ���� 30���� �ѱ��� ���ϳ� ��¥ �ù� ���Ż����ΰ�.
    /*
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        GameObject content = owner.currentTile.contents;
        if (content != null)
        {
            owner.currentUnit = content.GetComponent<Unit>();
            owner.ChangeState<MoveTargetState>();
        }
    }
    */
}
