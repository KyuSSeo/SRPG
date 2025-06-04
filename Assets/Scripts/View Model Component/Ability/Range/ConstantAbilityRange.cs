using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAbilityRange : AbilityRange
{
    //  유닛 위치에서 탐색 가능한 타일
    public override List<Tile> GetTilesInRange(Board board)
    {
        return board.Search(unit.tile, ExpandSearch);
    }
    //  거리, 높이 조건 탐색
    bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - unit.tile.height) <= vertical;
    }
}