using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  레벨 데이터를 가져와서 게임에 레벨(스테이지)을 생성하는 코드
public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    //  맵 정보를 저장하는 딕셔너리
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    

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

        return retValue;
    }
    public Tile GetTile(Point p)
    {

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
}
