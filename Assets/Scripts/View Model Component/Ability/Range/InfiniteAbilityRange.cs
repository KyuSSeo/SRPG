using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  사거리 제한 없는 기술
public class InfiniteAbilityRange : AbilityRange
{
    //  모든 타일을 사거리로 설정
    public override List<Tile> GetTilesInRange(Board board)
    {
        return new List<Tile>(board.tiles.Values);
    }
}