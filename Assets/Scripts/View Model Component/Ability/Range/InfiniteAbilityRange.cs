using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  ��Ÿ� ���� ���� ���
public class InfiniteAbilityRange : AbilityRange
{
    //  ��� Ÿ���� ��Ÿ��� ����
    public override List<Tile> GetTilesInRange(Board board)
    {
        return new List<Tile>(board.tiles.Values);
    }
}