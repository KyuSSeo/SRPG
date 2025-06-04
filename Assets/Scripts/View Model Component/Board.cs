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
    public Point min { get { return _min; } }
    public Point max { get { return _max; } }

    private Point _min;
    private Point _max;
    private Color selectedTileColor = new Color(0, 1, 1, 1);
    private Color defaultTileColor = new Color(1, 1, 1, 1);

    //  ť���� Ȯ���� Ÿ�� ���ÿ�  
    private Point[] dirs = new Point[4]
    {
        new Point(0, 1),
        new Point(0, -1),
        new Point(1, 0),
        new Point(-1, 0)
    };
    //  Ÿ�� �ҷ����� ����
    public void Load(LevelData mapData)
    {
        _min = new Point(int.MaxValue, int.MaxValue);
        _max = new Point(int.MinValue, int.MinValue);

        for (int i = 0; i < mapData.tiles.Count; ++i)
        { 
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            instance.transform.SetParent(transform);
            Tile t = instance.GetComponent<Tile>();
            t.Load(mapData.tiles[i]);
            tiles.Add(t.pos, t);

            _min.x = Mathf.Min(_min.x, t.pos.x);
            _min.y = Mathf.Min(_min.y, t.pos.y);
            _max.x = Mathf.Max(_max.x, t.pos.x);
            _max.y = Mathf.Max(_max.y, t.pos.y);
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
            for (int i = 0; i < 4; ++i)
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
    public void SelectTiles(List<Tile> tiles)
    {
        foreach (Tile t in tiles)
            t.GetComponent<Renderer>().material.color = selectedTileColor;
    }

    public void DeSelectTiles(List<Tile> tiles)
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
