using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  �� ���� ����
public class Turn
{
    #region Public
    //  �� ���� ����
    public Unit actor;
    //  �������°�?
    public bool hasUnitMoved;
    //  �ൿ�ߴ°�?
    public bool hasUnitActed;
    //  �̵��� �����Ѱ�? (�̵� �Ұ����� ������ �ֳ�?)
    public bool lockMove;
    public GameObject ability;
    #endregion


    # region Private
    //  �� ���� �� Ÿ��
    private Tile startTile;
    //  �� ���� �� ����
    private Directions startDir;
    #endregion
    // �� ���� �� ȣ��
    public void Change(Unit current)
    {
        //  ȣ�� �� ������ ����
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        startTile = actor.tile;
        startDir = actor.dir;
    }

    
    //  ������ �ǵ�����
    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
}