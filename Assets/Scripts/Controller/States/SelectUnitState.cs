using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  유닛을 선택했을 때의 상태
public class SelectUnitState : BattleState
{
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        GameObject content = owner.currentTile.contents;
        if (content != null)
        {
            owner.currentUnit = content.GetComponent<Unit>();
            owner.ChangeState<MoveTargetState>();
        }
    }
}
