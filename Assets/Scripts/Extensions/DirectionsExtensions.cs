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

    //  타일의 위치를 기준으로 방향 판단
    public static Directions GetDirection(this Point p)
    {
        if (p.y > 0)
            return Directions.North;
        if (p.x > 0)
            return Directions.East;
        if (p.y < 0)
            return Directions.South;
        return Directions.West;
    }

    //  열거형 방위를 int변환하여 각도 변경
    public static Vector3 ToEuler(this Directions dir)
    {
        return new Vector3(0, (int)dir * 90, 0);
    }
}
