using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    private Unit owner;

    private void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
            this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotifaication, owner);
    }
    private void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    }
    //  ä�� ��� 1��
    private void OnNewTurn(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        int currentHp = s[StatTypes.HP];
        int maxHp = s[StatTypes.MHP];
        //  �ִ� Hp ���� 1�Ҿ� hp 
        int reduce = Mathf.Min(currentHp, Mathf.FloorToInt(maxHp * 0.1f));
        s.SetValue(StatTypes.HP, (currentHp - reduce), false);
    }
}
