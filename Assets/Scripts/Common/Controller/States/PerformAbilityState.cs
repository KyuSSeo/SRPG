using UnityEngine;
using System.Collections;


// �ɷ��� ó�� ����
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

    //  �ɷ� ���� �ڷ�ƾ
    private IEnumerator Animate()
    {
        yield return null;
        ApplyAbility();

        // �ൿ ���� ����
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