using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  능력의 실제 수행 확인
public class Ability : MonoBehaviour
{
    //  알림
    public const string CanPerformCheck = "Ability.CanPerformCheck";
    public const string FailedNotification = "Ability.FailedNotification";
    public const string DidPerformNotification = "Ability.DidPerformNotification";


    // 수행 시 알림
    public bool CanPerform()
    {
        BaseException exc = new BaseException(true);
        this.PostNotification(CanPerformCheck, exc);
        return exc.toggle;
    }

    //  능력 수행 불가능 시 알림
    public void Perform(List<Tile> targets)
    {
        if (!CanPerform())
        {
            this.PostNotification(FailedNotification);
            return;
        }
        for (int i = 0; i < targets.Count; ++i)
            Perform(targets[i]);
        this.PostNotification(DidPerformNotification);
    }

    // 능력 수행 적용
    private void Perform(Tile target)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            BaseAbilityEffect effect = child.GetComponent<BaseAbilityEffect>();
            effect.Apply(target);
        }
    }
}
