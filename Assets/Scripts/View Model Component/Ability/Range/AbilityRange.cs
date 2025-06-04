using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbilityRange : MonoBehaviour
{
    //  거리 차이
    public int horizontal = 1;
    //  높이 차이
    public int vertical = int.MaxValue;

    //  방향지정이 필수인가?
    public virtual bool directionOriented { get { return false; } }
    // 스킬 사용의 주체
    protected Unit unit { get { return GetComponentInParent<Unit>(); } }
    //  스킬 사용 타일 추상
    public abstract List<Tile> GetTilesInRange(Board board);
}