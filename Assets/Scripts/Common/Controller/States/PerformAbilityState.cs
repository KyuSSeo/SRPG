using UnityEngine;
using System.Collections;


// 능력을 처리 상태
public class PerformAbilityState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        StartCoroutine(Animate());
    }

    //  능력 연출 코루틴
    private IEnumerator Animate()
    {
        yield return null;
        ApplyAbility();

        // 행동 이후 상태
        if (turn.hasUnitMoved)
            owner.ChangeState<EndFacingState>();
        else
            owner.ChangeState<CommandSelectionState>();
    }

    private void ApplyAbility()
    {
        turn.ability.Perform(turn.targets);
    }
}