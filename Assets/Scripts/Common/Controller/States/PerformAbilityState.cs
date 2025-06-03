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
        TemporaryAttackExample();

        // 행동 이후 상태
        if (turn.hasUnitMoved)
            owner.ChangeState<EndFacingState>();
        else
            owner.ChangeState<CommandSelectionState>();
    }

    private void TemporaryAttackExample()
    {
        for (int i = 0; i < turn.targets.Count; ++i)
        {
            GameObject obj = turn.targets[i].content;
            Stats stats = obj != null ? obj.GetComponentInChildren<Stats>() : null;
            if (stats != null)
            {
                stats[StatTypes.HP] -= 50;
                if (stats[StatTypes.HP] <= 0)
                    Debug.Log("KO'd Uni!", obj);
            }
        }
    }
}