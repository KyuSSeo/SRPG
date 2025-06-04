using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  효과 범위
public abstract class AbilityArea : MonoBehaviour
{
    public abstract List<Tile> GetTilesInArea(Board board, Point pos);
}