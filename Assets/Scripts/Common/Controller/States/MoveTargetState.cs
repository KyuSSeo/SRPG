using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �̵� ��ǥ Ÿ���� ����
public class MoveTargetState : BattleState
{
    // �̵� �Է� �߻� �� ���� ��ġ(pos)�� �Է� ����(e.info)�� ���� ���ο� Ÿ�� ����
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        SelectTile(e.info + pos);
    }
}

