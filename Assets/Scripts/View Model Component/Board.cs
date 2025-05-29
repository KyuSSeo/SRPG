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
}
