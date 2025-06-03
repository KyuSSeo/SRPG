using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  원뿔 모양 범위
public class ConeAbilityRange : AbilityRange
{
    //  방향 사용
    public override bool directionOriented { get { return true; } }

    public override List<Tile> GetTilesInRange(Board board)
    {
        //  유닛 기준
        Point pos = unit.tile.pos;
        List<Tile> retValue = new List<Tile>();
        //  방향
        int dir = (unit.dir == Directions.North || unit.dir == Directions.East) ? 1 : -1;
        int lateral = 1;

        // TODO : X축, Y축 코드가 유사함으로 합치기
        //  남북
        if (unit.dir == Directions.North || unit.dir == Directions.South)
        {
            for (int y = 1; y <= horizontal; ++y)
            {
                int min = -(lateral / 2);
                int max = (lateral / 2);
                for (int x = min; x <= max; ++x)
                {
                    Point next = new Point(pos.x + x, pos.y + (y * dir));
                    Tile tile = board.GetTile(next);
                    if (ValidTile(tile))
                        retValue.Add(tile);
                }
                // 다음 거리 폭을 넓힘
                lateral += 2;
            }
        }
        //  동서
        else
        {
            for (int x = 1; x <= horizontal; ++x)
            {
                int min = -(lateral / 2);
                int max = (lateral / 2);
                for (int y = min; y <= max; ++y)
                {
                    Point next = new Point(pos.x + (x * dir), pos.y + y);
                    Tile tile = board.GetTile(next);
                    if (ValidTile(tile))
                        retValue.Add(tile);
                }
                lateral += 2;
            }
        }
        // 계산된 타일 리스트 반환
        return retValue;
    }

    bool ValidTile(Tile t)
    {
        return t != null && Mathf.Abs(t.height - unit.tile.height) <= vertical;
    }
}
