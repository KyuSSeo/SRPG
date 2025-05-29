using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    //  �̵��Ÿ�, ���� ����, ���� ����, �ִϸ��̼� �� ����
    public int range;
    public int jumpHeight;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        jumper = transform.Find("Jumper");
    }

    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    public virtual IEnumerator Traverse(Tile tile)
    {
        unit.Place(tile);
        yield return null;
    }

    //  �̵� ���� ���� ��ȯ
    protected virtual bool ExpandSearch(Tile from, Tile tile)
    {
        return (from.distance + 1) <= range;
    }
    //  Ÿ�� ����� ��ȸ�ϸ� �̵� �Ұ��� Ÿ���� ����
    //  (�Ʊ� ���, ���� ��� �Ұ� ���� ���)
    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            if (tiles[i].contents != null)
                tiles.RemoveAt(i);
    }
    protected virtual IEnumerator Turn(Directions dir)
    {
        TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

        if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
            t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
            t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        unit.dir = dir;

        while (t != null)
            yield return null;
    }
}