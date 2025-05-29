using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 강한 육군
public class WalkMovement : Movement
{
    //  타일 높이를 비교하여 점프가 가능한가?
    protected override bool ExpandSearch(Tile from, Tile tile)
    {
        //  점프 높이보다 타일이 높으면
        if ((Mathf.Abs(from.height - tile.height) > jumpHeight))
            return false;
        //  점프는 가능하지만 무언가가 타일을 점유중이면
        if (tile.contents != null)
            return false;
        return base.ExpandSearch(from, tile);
    }
    public override IEnumerator Traverse(Tile tile)
    {
        unit.Place(tile);

        //  시작점부터 목적지까지 경로 작성
        List<Tile> targets = new List<Tile>();
        while (tile != null)
        {
            targets.Insert(0, tile);
            tile = tile.prev;
        }
        //  경로를 연속적으로 이동
        for (int i = 0; i < targets.Count; i++)
        {
            //  현제 위치부터 다음 위치로
            Tile from = targets[i - 1];
            Tile to = targets[i];
            //  방향 가져오기
            Directions dir = from.GetDirections(to);

            if (unit.dir != dir)
                yield return StartCoroutine(Walk(to));
            if (from.height == to.height)
                yield return StartCoroutine(Jump(to));
        }
        yield return null;
    }

    //  이동
    private IEnumerator Walk(Tile target)
    {
        //  Tweener : 오브젝트의 위치, 회전, 색상, 크기 등을 부드럽게 변화시키기 위한 도구로 사용, 주로 애니메이션 처리
        //  EasingEquations : Tweener 클래스 진행 컨트롤러, 시간 진행, 진행 상태를 관리
        #region Lerp랑 차이점이 뭘까?
        /*
        Lerp 를 통한 직접 제어
        Vector3 start = transform.position;
        Vector3 end = new Vector3(5, 0, 0);
        float t = 0;

        void Update()
        {
            if (t < 1f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(start, end, t);
            }
        }

        Tweener 자동 관리
        transform.MoveTo(new Vector3(5, 0, 0), 1f, EasingEquations.EaseOutBounce);

        */
        #endregion

        Tweener tweener = transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        while (tweener != null)
            yield return null;
    }
    //  점프
    private IEnumerator Jump(Tile target)
    {
        Tweener tweener = transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        Tweener tweener2 = jumper.MoveToLocal(
            new Vector3(0, Tile.stepHeight * 2f, 0),
            tweener.easingControl.duration / 2f,
            EasingEquations.EaseOutQuad
        );

        while (tweener != null)
            yield return null;
    }
}
