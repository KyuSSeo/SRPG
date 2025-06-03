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
    //  채력 비례 1할
    private void OnNewTurn(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        int currentHp = s[StatTypes.HP];
        int maxHp = s[StatTypes.MHP];
        //  최대 Hp 기준 1할씩 hp 
        int reduce = Mathf.Min(currentHp, Mathf.FloorToInt(maxHp * 0.1f));
        s.SetValue(StatTypes.HP, (currentHp - reduce), false);
    }
}
