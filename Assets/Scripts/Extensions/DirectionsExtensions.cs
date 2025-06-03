using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionsExtensions
{
    //  �� Ÿ���� ��ġ�� ���Ͽ� ������ �Ǵ�
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

    //  Ÿ���� ��ġ�� �������� ���� �Ǵ�
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

    //  ������ ������ int��ȯ�Ͽ� ���� ����
    public static Vector3 ToEuler(this Directions dir)
    {
        return new Vector3(0, (int)dir * 90, 0);
    }
}
