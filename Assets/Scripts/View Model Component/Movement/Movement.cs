using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    //  이동거리, 점프 높이, 유닛 정보, 애니메이션 용 정보
    public int range { get { return stats[StatTypes.MOV]; } }
    public int jumpHeight { get { return stats[StatTypes.JMP]; } }

    protected Stats stats;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        jumper = transform.Find("Jumper");
    }
    protected virtual void Start()
    {
        stats = GetComponent<Stats>();
    }

    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }

    public abstract IEnumerator Traverse(Tile tile);


    //  이동 가능 여부 반환
    protected virtual bool ExpandSearch(Tile from, Tile tile)
    {
        return (from.distance + 1) <= range;
    }
    //  타일 목록을 순회하며 이동 불가한 타일을 제외
    //  (아군 통과, 적군 통과 불가 같은 경우)
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