using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

//  �ð��� ���� ������
public class BoardCreator : MonoBehaviour
{
    [SerializeField] GameObject tileViewPrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;

    // Lazy loading��ü, �ʱ�ȭ�ÿ� �ʿ��� �����͸� ��� �ҷ����� �ʰ� �ʿ��� ������ �����͸� �������� ������ ����  
    private Transform _marker;
    #region transform marker
    public Transform marker
    {
        get
        {
            if (_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
    }
    #endregion

    //  ������ ��ǥ�� Ÿ�� ���� Ȯ���� ����
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    //  ���� ũ�� ���� ������ ����
    [SerializeField] public int width = 10;
    [SerializeField] public int depth = 10;
    [SerializeField] public int height = 8;
    [SerializeField] public Point pos;
    //  �� ���� ��������
    [SerializeField] public LevelData levelData;


    // ���� �� ������ ���� ������ �� ����
    public void GrowArea()
    {
        Rect r = RandomRect();
        GrowRect(r);
    }
    public void ShrinkArea()
    {
        Rect r = RandomRect();
        ShrinkRect(r);
    }

    //  ������ Ÿ�� �ϳ��� ����
    public void Grow()
    {
        GrowSingle(pos);
    }

    public void Shrink()
    {
        ShrinkSingle(pos);
    }
    //  �����Ǵ� Ÿ�� ���� Ȯ�ο�
    public void UpdateMarker()
    {
        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.center : new Vector3(pos.x, 0, pos.y);
    }
    //  �� �ѹ��� �����
    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
    }
    //  �����ϱ�
    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(tiles.Count);
        foreach (Tile t in tiles.Values)
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }
    //  �ҷ�����
    public void Load()
    {
        Clear();
        if (levelData == null)
            return;

        foreach (Vector3 v in levelData.tiles)
        {
            Tile t = Create();
            t.Load(v);
            tiles.Add(t.pos, t);
        }
    }

    //  ���� ����

    private Rect RandomRect()
    {
        int x = Random.Range(0, width - 2);
        int y = Random.Range(0, height - 2);
        int w = Random.Range(1, width - x);
        int h = Random.Range(1, depth - y);
        return new Rect(x, y, w, h);
    }

    //Ÿ�� ����
    private void GrowRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                GrowSingle(p);
            }
        }
    }

    private void ShrinkRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p);
            }
        }
    }

    //  �����տ��� Ÿ���� ������ �ν��Ͻ�ȭ
    private Tile Create()
    {
        GameObject instance = Instantiate(tileViewPrefab) as GameObject;
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
    }

    private Tile GetOrCreate(Point p)
    {
        if (tiles.ContainsKey(p))
            return tiles[p];

        Tile t = Create();
        t.Load(p, 0);
        tiles.Add(p, t);

        return t;
    }

    //  Ÿ�� Ű���
    private void GrowSingle(Point p)
    {
        Tile t = GetOrCreate(p);
        if (t.height < height)
            t.Grow();
    }

    //  Ÿ�� ���
    private void ShrinkSingle(Point p)
    {   //Ÿ���� ���������� Ȯ�� �� 
        if (!tiles.ContainsKey(p))
            return;

        Tile t = tiles[p];
        t.Shrink();
        //  ���� 0���ϸ� �ı�
        if (t.height <= 0)
        {
            tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    //  �� ����
    private void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }
}
