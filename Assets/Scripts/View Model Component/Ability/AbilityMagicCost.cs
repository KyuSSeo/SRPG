using UnityEngine;
using System.Collections;

public class AbilityMagicCost : MonoBehaviour
{
    //  �Ҹ��
    public int amount;
    //  Mp�Ҹ��� ��ü
    private Ability owner;

    private void Awake()
    {
        owner = GetComponent<Ability>();
    }


    //�˸��� ��� 
    private void OnEnable()
    {
        //  ��� ���ɿ��� / ��� �Ϸ� 
        this.AddObserver(OnCanPerformCheck, Ability.CanPerformCheck, owner);
        this.AddObserver(OnDidPerformNotification, Ability.DidPerformNotification, owner);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnCanPerformCheck, Ability.CanPerformCheck, owner);
        this.RemoveObserver(OnDidPerformNotification, Ability.DidPerformNotification, owner);
    }

    // Mp�� Ȯ���Ͽ� ��� ���ɿ��� Ȯ��
    private void OnCanPerformCheck(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        if (s[StatTypes.MP] < amount)
        {
            BaseException exc = (BaseException)args;
            exc.FlipToggle();
        }
    }

    //  ������� ��� Mp����
    void OnDidPerformNotification(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        s[StatTypes.MP] -= amount;
    }
}