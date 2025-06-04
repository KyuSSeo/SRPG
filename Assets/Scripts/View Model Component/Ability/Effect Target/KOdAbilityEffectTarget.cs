using UnityEngine;
using System.Collections;

public class KOdAbilityEffectTarget : AbilityEffectTarget
{
    //  채력이 없는 대상이 효과의 대상
    public override bool IsTarget(Tile tile)
    {
        if (tile == null || tile.content == null)
            return false;

        Stats s = tile.content.GetComponent<Stats>();
        return s != null && s[StatTypes.HP] <= 0;
    }
}