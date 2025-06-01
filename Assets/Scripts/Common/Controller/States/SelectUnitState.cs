using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  유닛을 선택했을 때의 상태
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
}
