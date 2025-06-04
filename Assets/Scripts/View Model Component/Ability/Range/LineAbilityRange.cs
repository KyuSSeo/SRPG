using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  일직선 상
public class LineAbilityRange : AbilityRange
{
    //  방향을 사용
    public override bool directionOriented { get { return true; } }

    //  범위 타일 리스트 
    public override List<Tile> GetTilesInRange(Board board)
    {
        //  유닛 기준 시작점
        Point startPos = unit.tile.pos;
        Point endPos;
        List<Tile> retValue = new List<Tile>();

        switch (unit.dir)
        {
            case Directions.North:
                endPos = new Point(startPos.x, board.max.y);
                break;
            case Directions.East:
                endPos = new Point(board.max.x, startPos.y);
                break;
            case Directions.South:
                endPos = new Point(startPos.x, board.min.y);
                break;
            default: // West
                endPos = new Point(board.min.x, startPos.y);
                break;
        }

        while (startPos != endPos)
        {
            if (startPos.x < endPos.x) startPos.x++;
            else if (startPos.x > endPos.x) startPos.x--;

            if (startPos.y < endPos.y) startPos.y++;
            else if (startPos.y > endPos.y) startPos.y--;

            Tile t = board.GetTile(startPos);
            if (t != null && Mathf.Abs(t.height - unit.tile.height) <= vertical)
                retValue.Add(t);
        }

        return retValue;
    }
}