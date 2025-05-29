using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// ���� ���� �� �ʱ� ���� ���� �� Ÿ���� ó��
public class InitBattleState :BattleState
{
    // ���� �� �⺻ ó�� �� �ڷ�ƾ ����
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    // �ʱ�ȭ �ڷ�ƾ: �� �ε�, �ʱ� Ÿ�� ����, ���� ���� ��ȯ�� ���������� ����
    // �� ������ ���� �� ���� ��ٵ��� ��.
    IEnumerator Init()
    {   
        //  Ÿ�� ����
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        //  �ʱ� Ÿ�� ����
        SelectTile(p);
        yield return null;
        //  �� �ʱ�ȭ �Ϸ� �� ���� ��ȯ
        owner.ChangeState<MoveTargetState>();
    }
}
