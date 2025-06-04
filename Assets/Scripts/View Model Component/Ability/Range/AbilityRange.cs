using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbilityRange : MonoBehaviour
{
    //  �Ÿ� ����
    public int horizontal = 1;
    //  ���� ����
    public int vertical = int.MaxValue;

    //  ���������� �ʼ��ΰ�?
    public virtual bool directionOriented { get { return false; } }
    // ��ų ����� ��ü
    protected Unit unit { get { return GetComponentInParent<Unit>(); } }
    //  ��ų ��� Ÿ�� �߻�
    public abstract List<Tile> GetTilesInRange(Board board);
}