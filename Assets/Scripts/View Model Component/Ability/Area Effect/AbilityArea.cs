using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  ȿ�� ����
public abstract class AbilityArea : MonoBehaviour
{
    public abstract List<Tile> GetTilesInArea(Board board, Point pos);
}