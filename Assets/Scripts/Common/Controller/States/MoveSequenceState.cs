using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  이동 명령을 결정지었을 때, 이동하는 중 입력방지
public class MoveSequenceState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine("Sequence");
    }
    IEnumerator Sequence()
    {
        Movement m = turn.actor.GetComponent<Movement>();
        yield return StartCoroutine(m.Traverse(owner.currentTile));
        turn.hasUnitMoved = true;
        owner.ChangeState<CommandSelectionState>();
    }
}
