using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

//  시각적 보드 편집기
public class BoardCreator : MonoBehaviour
{
    [SerializeField] GameObject tileViewPrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;

    // Lazy loading객체, 초기화시에 필요한 데이터를 모두 불러오지 않고 필요한 시점에 데이터를 가져오는 디자인 패턴  
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

    //  지정된 좌표의 타일 유무 확인을 위함
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    //  맵의 크기 등의 정보를 지정
    [SerializeField] public int width = 10;
    [SerializeField] public int depth = 10;
    [SerializeField] public int height = 8;
    [SerializeField] public Point pos;
    //  맵 정보 가져오기
    [SerializeField] public LevelData levelData;


    // 빠른 맵 생성을 위해 랜덤한 맵 생성
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

    //  각각의 타일 하나씩 수정
    public void Grow()
    {
        GrowSingle(pos);
    }

    public void Shrink()
    {
        ShrinkSingle(pos);
    }
    //  수정되는 타일 정보 확인용
    public void UpdateMarker()
    {
        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.center : new Vector3(pos.x, 0, pos.y);
    }
    //  맵 한번에 지우기
    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
    }
    //  저장하기
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
    //  불러오기
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

    //  랜덤 생성

    private Rect RandomRect()
    {
        int x = Random.Range(0, width - 2);
        int y = Random.Range(0, height - 2);
        int w = Random.Range(1, width - x);
        int h = Random.Range(1, depth - y);
        return new Rect(x, y, w, h);
    }

    //타일 조정
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

    //  프리팹에서 타일을 가져와 인스턴스화
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

    //  타일 키우기
    private void GrowSingle(Point p)
    {
        Tile t = GetOrCreate(p);
        if (t.height < height)
            t.Grow();
    }

    //  타일 축소
    private void ShrinkSingle(Point p)
    {   //타일의 존재유무를 확인 후 
        if (!tiles.ContainsKey(p))
            return;

        Tile t = tiles[p];
        t.Shrink();
        //  높이 0이하면 파괴
        if (t.height <= 0)
        {
            tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    //  맵 저장
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
