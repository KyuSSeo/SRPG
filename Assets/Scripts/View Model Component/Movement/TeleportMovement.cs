using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  �����̵�
public class TeleportMovement : Movement
{
    public override IEnumerator Traverse(Tile tile)
    {
        //  Ÿ�� ����
        unit.Place(tile);

        //  ���ۺ��� ���ư��� �ִϸ��̼�
        Tweener spin = jumper.RotateToLocal(
            new Vector3(0, 360, 0), 0.5f,
            EasingEquations.EaseInOutQuad
        );

        spin.easingControl.loopCount = 1;
        // PingPong : ȸ�� �� ���� ������
        spin.easingControl.loopType = EasingControl.LoopType.PingPong;
       
        // ������ ��ü ũ�⸦ 0���� ���̴� �ִϸ��̼�
        Tweener shrink = transform.ScaleTo(Vector3.zero, 0.5f, EasingEquations.EaseInBack);
        
        // shrink �ִϸ��̼��� ���� ���
        while (shrink != null)
            yield return null;

        // ���� ������ ��ġ�� �� Ÿ���� �߽����� �̵� (�����̵� ����)
        transform.position = tile.center;

        // ������ �ٽ� ���̰� �ϱ� ���� ũ�� ���� �ִϸ��̼�
        Tweener grow = transform.ScaleTo(Vector3.one, 0.5f, EasingEquations.EaseOutBack);
        
        // grow �ִϸ��̼��� ���� ���
        while (grow != null)
            yield return null;
    }
}
