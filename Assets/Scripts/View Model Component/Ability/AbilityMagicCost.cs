using UnityEngine;
using System.Collections;

public class AbilityMagicCost : MonoBehaviour
{
    //  소모양
    public int amount;
    //  Mp소모의 주체
    private Ability owner;

    private void Awake()
    {
        owner = GetComponent<Ability>();
    }


    //알림에 등록 
    private void OnEnable()
    {
        //  사용 가능여부 / 사용 완료 
        this.AddObserver(OnCanPerformCheck, Ability.CanPerformCheck, owner);
        this.AddObserver(OnDidPerformNotification, Ability.DidPerformNotification, owner);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnCanPerformCheck, Ability.CanPerformCheck, owner);
        this.RemoveObserver(OnDidPerformNotification, Ability.DidPerformNotification, owner);
    }

    // Mp를 확인하여 사용 가능여부 확인
    private void OnCanPerformCheck(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        if (s[StatTypes.MP] < amount)
        {
            BaseException exc = (BaseException)args;
            exc.FlipToggle();
        }
    }

    //  사용했을 경우 Mp차감
    void OnDidPerformNotification(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        s[StatTypes.MP] -= amount;
    }
}