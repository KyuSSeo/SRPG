using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  범위 내 모든 타일이 능력 대상
public class FullAbilityArea : AbilityArea
{
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        AbilityRange ar = GetComponent<AbilityRange>();
        return ar.GetTilesInRange(board);
    }
}
