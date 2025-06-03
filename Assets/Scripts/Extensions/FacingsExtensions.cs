using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FacingsExtensions
{
    public static Facings GetFacing(this Unit attacker, Unit target)
    {
        //  格钎 规氢
        Vector2 targetDirection = target.dir.GetNormal();
        //  立辟 规氢
        Vector2 approachDirection = ((Vector2)(target.tile.pos - attacker.tile.pos)).normalized;
        float dot = Vector2.Dot(approachDirection, targetDirection);
        if (dot >= 0.45f)
            return Facings.Back;
        if (dot <= -0.45f)
            return Facings.Front;
        return Facings.Side;
    }
    public static Point GetNormal(this Directions dir)
    {
        switch (dir)
        {
            case Directions.North:
                return new Point(0, 1);
            case Directions.East:
                return new Point(1, 0);
            case Directions.South:
                return new Point(0, -1);
            default:
                return new Point(-1, 0);
        }
    }
}
