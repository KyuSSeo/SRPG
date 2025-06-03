using UnityEngine;
using System.Collections;

public class DefaultAbilityEffectTarget : AbilityEffectTarget
{
    //  채력을 가진 대상을 효과 적용 대상으로 
    public override bool IsTarget(Tile tile)
    {
        if (tile == null || tile.content == null)
            return false;

        Stats s = tile.content.GetComponent<Stats>();
        return s != null && s[StatTypes.HP] > 0;
    }
}
