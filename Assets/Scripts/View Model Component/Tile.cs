using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 타일 이동 관련
    [SerializeField] public Tile prev;
    [SerializeField] public int distance;
    //  위치와 높이 정보
    public Point pos;
    public int height;

    //  기타 엔티티의 정보
    public GameObject content;
    //  높이 정보 4단계
    public const float stepHeight = 0.25f;
    
    //  타일 중심에 캐릭터가 위치하도록
    public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }
   
    //  타일 변형
    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }

    //  타일 정보 가져오기
    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    // 시각적 업데이트를 위한 함수
    private void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }
}
