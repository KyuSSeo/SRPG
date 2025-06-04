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
        BaseAbilityEffect[] effects = turn.ability.GetComponentsInChildren<BaseAbilityEffect>();
        for (int i = 0; i < turn.targets.Count; ++i)
        {
            Tile target = turn.targets[i];
            for (int j = 0; j < effects.Length; ++j)
            {
                BaseAbilityEffect effect = effects[j];
                AbilityEffectTarget targeter = effect.GetComponent<AbilityEffectTarget>();
                if (targeter.IsTarget(target))
                {
                    HitRate rate = effect.GetComponent<HitRate>();
                    int chance = rate.Calculate(target);
                    if (Random.Range(0, 101) > chance)
                    {
                        continue;
                    }
                    effect.Apply(target);
                }
            }
        }
    }
}