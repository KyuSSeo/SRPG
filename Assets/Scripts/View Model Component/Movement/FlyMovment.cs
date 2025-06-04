using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  공군
public class FlyMovement : Movement
{
    public override IEnumerator Traverse(Tile tile)
    {
        // 시작과 끝지점 확인
        float dist = Mathf.Sqrt(Mathf.Pow(tile.pos.x - unit.tile.pos.x, 2) + Mathf.Pow(tile.pos.y - unit.tile.pos.y, 2));
        unit.Place(tile);

        // 지면에서 떨어져 상공으로 tile과 겹치지 않도록 띄움
        float y = Tile.stepHeight * 10;
        float duration = (y - jumper.position.y) * 0.5f;
        Tweener tweener = jumper.MoveToLocal(new Vector3(0, y, 0), duration, EasingEquations.EaseInOutQuad);
        while (tweener != null)
            yield return null;

        // 방향 회전
        Directions dir;
        Vector3 toTile = (tile.center - transform.position);
        if (Mathf.Abs(toTile.x) > Mathf.Abs(toTile.z))
            dir = toTile.x > 0 ? Directions.East : Directions.West;
        else
            dir = toTile.z > 0 ? Directions.North : Directions.South;
        yield return StartCoroutine(Turn(dir));

        // 위치 이동
        duration = dist * 0.5f;
        tweener = transform.MoveTo(tile.center, duration, EasingEquations.EaseInOutQuad);
        while (tweener != null)
            yield return null;

        // 착륙
        duration = (y - tile.center.y) * 0.5f;
        tweener = jumper.MoveToLocal(Vector3.zero, 0.5f, EasingEquations.EaseInOutQuad);
        while (tweener != null)
            yield return null;
    }
}