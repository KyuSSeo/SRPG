using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ���� �����͸� �����ͼ� ���ӿ� ����(��������)�� �����ϴ� �ڵ�
public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    //  �� ������ �����ϴ� ��ųʸ�
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    

    //  Ÿ�� �ҷ����� ����
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
    //  ��� Ž�� �˰���
    public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile)
    {
        List<Tile> retValue = new List<Tile>();
        retValue.Add(start);
        //TODO : ��� Ž�� �˰��� ����

        return retValue;
    }
    public Tile GetTile(Point p)
    {

    }
    //  ��� Ž�� ����, ������ Ž���ߴ� ����� �����ִ� �۾�
    private void ClearSearch()
    {
        foreach (Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }
}
