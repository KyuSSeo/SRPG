using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionsExtensions
{
    //  두 타일의 위치를 비교하여 방향을 판단
    public static Directions GetDirections(this Tile t1, Tile t2)
    {
        if (t1.pos.y < t2.pos.y)
            return Directions.North;
        if (t1.pos.x < t2.pos.x)
            return Directions.East;
        if (t1.pos.y > t2.pos.y)
            return Directions.South;
        return Directions.West;
    }

    public static Vector3 ToEuler(this Directions dir)
    {
        return new Vector3(0, (int)dir * 90, 0);
    }
}
