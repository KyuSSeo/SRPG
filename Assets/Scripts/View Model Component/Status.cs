using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Stats에 적용되는 효과 관리
public class Status : MonoBehaviour
{
    //  상태 적용, 해제 알림
    public const string AddedNotification = "Status.AddedNotification";
    public const string RemovedNotification = "Status.RemovedNotification";

    //  상태효과 T, 조건 U 추가
    public U Add<T, U>() where T : StatusEffect where U : StatusCondition
    {
        //  상태 확인
        T effect = GetComponentInChildren<T>();
        if (effect == null)
        {
            effect = gameObject.AddChildComponent<T>();
            //  추가 알림 발송
            this.PostNotification(AddedNotification, effect);
        }
        // 상태 추가
        return effect.gameObject.AddChildComponent<U>();
    }
    
    //  상태 제거
    public void Remove(StatusCondition target)
    {
        //  상태 가져오기
        StatusEffect effect = target.GetComponentInParent<StatusEffect>();
        target.transform.SetParent(null);
        Destroy(target.gameObject);

        // 남은 조건이 없으면 효과 제거
        StatusCondition condition = effect.GetComponentInChildren<StatusCondition>();
        if (condition == null)
        {
            effect.transform.SetParent(null);
            Destroy(effect.gameObject);
            //  제거 알림 발송
            this.PostNotification(RemovedNotification, effect);
        }
    }
}
