using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  순간이동
public class TeleportMovement : Movement
{
    public override IEnumerator Traverse(Tile tile)
    {
        //  타일 갱신
        unit.Place(tile);

        //  빙글빙글 돌아가는 애니메이션
        Tweener spin = jumper.RotateToLocal(
            new Vector3(0, 360, 0), 0.5f,
            EasingEquations.EaseInOutQuad
        );

        spin.easingControl.loopCount = 1;
        // PingPong : 회전 후 원래 각도로
        spin.easingControl.loopType = EasingControl.LoopType.PingPong;
       
        // 유닛의 전체 크기를 0으로 줄이는 애니메이션
        Tweener shrink = transform.ScaleTo(Vector3.zero, 0.5f, EasingEquations.EaseInBack);
        
        // shrink 애니메이션이 종료 대기
        while (shrink != null)
            yield return null;

        // 실제 유닛의 위치를 새 타일의 중심으로 이동 (순간이동 적용)
        transform.position = tile.center;

        // 유닛을 다시 보이게 하기 위한 크기 복구 애니메이션
        Tweener grow = transform.ScaleTo(Vector3.one, 0.5f, EasingEquations.EaseOutBack);
        
        // grow 애니메이션이 종료 대기
        while (grow != null)
            yield return null;
    }
}
