using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAbilityArea : AbilityArea
{
    //  지정한 위치의 타일을 반환
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        List<Tile> retValue = new List<Tile>();
        Tile tile = board.GetTile(pos);
        if (tile != null)
            retValue.Add(tile);
        return retValue;
    }
}