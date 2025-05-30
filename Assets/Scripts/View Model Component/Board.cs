using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  레벨 데이터를 가져와서 게임에 레벨(스테이지)을 생성하는 코드
//  경로 탐색
public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    //  맵 정보를 저장하는 딕셔너리
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    private Color selectedTileColor = new Color(0, 1, 1, 1);
    private Color defaultTileColor = new Color(1, 1, 1, 1);



    //  큐에서 확인할 타일 선택용  
    Point[] dirs = new Point[4]
    {
        new Point(0, 1),
        new Point(0, -1),
        new Point(1, 0),
        new Point(-1, 0)
    };
    //  타일 불러오는 과정
    public void Load(LevelData mapData)
    {
        for (int i = 0; i < tiles.Count; ++i)
        { 
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(mapData.tiles[i]);
            tiles.Add(t.pos, t);
        }
    }


    //  경로 탐색 알고리즘
    public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile)
    {
        List<Tile> retValue = new List<Tile>();
        retValue.Add(start);
        //TODO : 경로 탐색 알고리즘 구현
        ClearSearch();

        //  타일 확인용 큐, 
        Queue<Tile> cheakNext = new Queue<Tile>();
        Queue<Tile> cheakNow = new Queue<Tile>();
        start.distance = 0;
        cheakNow.Enqueue(start);

        //  타일을 확인 반복문
        while(cheakNow.Count > 0)
        {
            Tile t = cheakNow.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                // 탐색할 타일 정보 가져오기              
                Tile nextTile = GetTile(t.pos + dirs[i]);
                
                //  타일을 이미 방문했거나, 거리가 더 짧은가?
                if (nextTile == null || nextTile.distance < t.distance+1)
                    continue;

                if (addTile(t, nextTile))
                {   
                    //거리 갱신
                    nextTile.distance = t.distance + 1;
                    nextTile.prev = t;
                    cheakNext.Enqueue(nextTile);
                    retValue.Add(nextTile);
                }
            }
            if (cheakNow.Count == 0)
                SwapRefrence(ref cheakNow, ref cheakNext);
        }
        return retValue;
    }

    //  지정한 위치(Point)에 있는 타일을 반환
    public Tile GetTile(Point p)
    {
        return tiles.ContainsKey(p) ? tiles[p] : null;
    }

    //  선택 타일 강조, 해제
    public void SelectTiles(HashSet<Tile> tiles)
    {
        foreach (Tile t in tiles)
            t.GetComponent<Renderer>().material.color = selectedTileColor;
    }

    public void DeSelectTiles(HashSet<Tile> tiles)
    {
        foreach (Tile t in tiles)
            t.GetComponent<Renderer>().material.color = defaultTileColor;
    }


    //  경로 탐색 이전, 이전에 탐색했던 결과를 지워주는 작업
    private void ClearSearch()
    {
        foreach (Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }
    //  탐색 타일 정보 교환
    private void SwapRefrence(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }
}
