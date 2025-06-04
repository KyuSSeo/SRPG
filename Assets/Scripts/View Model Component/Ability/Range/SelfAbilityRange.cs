using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  자기 타일에서만 사용 가능(적용 대상이 자신)
public class SelfAbilityRange : AbilityRange
{
    //  유닛 자신의 위치를 반환하기
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>(1);
        retValue.Add(unit.tile);
        return retValue;
    }
}
