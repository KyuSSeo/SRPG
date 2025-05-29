using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ���� �����͸� �����ͼ� ���ӿ� ����(��������)�� �����ϴ� �ڵ�
//  ��� Ž��
public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    //  �� ������ �����ϴ� ��ųʸ�
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    private Color selectedTileColor = new Color(0, 1, 1, 1);
    private Color defaultTileColor = new Color(1, 1, 1, 1);



    //  ť���� Ȯ���� Ÿ�� ���ÿ�  
    Point[] dirs = new Point[4]
    {
        new Point(0, 1),
        new Point(0, -1),
        new Point(1, 0),
        new Point(-1, 0)
    };
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
        ClearSearch();

        //  Ÿ�� Ȯ�ο� ť, 
        Queue<Tile> cheakNext = new Queue<Tile>();
        Queue<Tile> cheakNow = new Queue<Tile>();
        start.distance = 0;
        cheakNow.Enqueue(start);

        //  Ÿ���� Ȯ�� �ݺ���
        while(cheakNow.Count > 0)
        {
            Tile t = cheakNow.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                // Ž���� Ÿ�� ���� ��������              
                Tile nextTile = GetTile(t.pos + dirs[i]);
                
                //  Ÿ���� �̹� �湮�߰ų�, �Ÿ��� �� ª����?
                if (nextTile == null || nextTile.distance < t.distance+1)
                    continue;

                if (addTile(t, nextTile))
                {   
                    //�Ÿ� ����
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

    //  ������ ��ġ(Point)�� �ִ� Ÿ���� ��ȯ
    public Tile GetTile(Point p)
    {
        return tiles.ContainsKey(p) ? tiles[p] : null;
    }

    //  ���� Ÿ�� ����, ����
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


    //  ��� Ž�� ����, ������ Ž���ߴ� ����� �����ִ� �۾�
    private void ClearSearch()
    {
        foreach (Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }
    //  Ž�� Ÿ�� ���� ��ȯ
    private void SwapRefrence(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }
}
