using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  �ڱ� Ÿ�Ͽ����� ��� ����(���� ����� �ڽ�)
public class SelfAbilityRange : AbilityRange
{
    //  ���� �ڽ��� ��ġ�� ��ȯ�ϱ�
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>(1);
        retValue.Add(unit.tile);
        return retValue;
    }
}
