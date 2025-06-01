using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  Ǯ ���� ��Ʈ�ѷ�
public class GameObjectPoolController : MonoBehaviour
{
    // Ű ������� Ǯ�� �����ϴ� ��ųʸ�
    private static Dictionary<string, PoolData> pools = new Dictionary<string, PoolData>();
    // �̱��� �ν��Ͻ�
    private static GameObjectPoolController instance;
    private static GameObjectPoolController Instance
    {
        get
        {   
            //  �ν��Ͻ� ������ �����ϱ�
            if (instance == null)
                CreateSharedInstance();
            return instance;
        }
    }

    private void Awake()
    {
        // �ߺ� �ν��Ͻ� ����
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    #region Public
    // �ش� Ű�� Ǯ�� �ִ� ������ �缳��

    public static void SetMaxCount(string key, int maxCount)
    {
        if (!pools.ContainsKey(key))
            return;
        PoolData data = pools[key];
        data.maxCount = maxCount;
    }

    //  Ǯ �ʱ�ȭ
    public static bool AddEntry(string key, GameObject prefab, int prepopulate, int maxCount)
    {
        // �̹� �����ϸ� ���� ��ȯ
        if (pools.ContainsKey(key))
            return false;

        PoolData data = new PoolData();
        data.prefab = prefab;
        data.maxCount = maxCount;
        data.pool = new Queue<Poolable>(prepopulate);
        pools.Add(key, data);

        // �ʱ� ������Ʈ���� ����
        for (int i = 0; i < prepopulate; ++i)
            Enqueue(CreateInstance(key, prefab));
        //  Ǯ ���� ������ ��ȯ
        return true;
    }

    //  Ǯ ����
    public static void ClearEntry(string key)
    {
        if (!pools.ContainsKey(key))
            return;

        PoolData data = pools[key];

        // ť���� ������ ������Ʈ ����
        while (data.pool.Count > 0)
        {
            Poolable obj = data.pool.Dequeue();
            GameObject.Destroy(obj.gameObject);
        }
        // ��ųʸ����� ����
        pools.Remove(key);
    }

    // Ǯ ��ȯ, ��� ó��
    public static void Enqueue(Poolable sender)
    {
        // null�̰ų� �̹� Ǯ ���°ų� Ǯ Ű�� ���� ���
        if (sender == null || sender.isPooled || !pools.ContainsKey(sender.key))
            return;

        PoolData data = pools[sender.key];
        // �ִ� ���� �ʰ� �� ������Ʈ �ı�
        if (data.pool.Count >= data.maxCount)
        {
            GameObject.Destroy(sender.gameObject);
            return;
        }
        // ť�� �߰��ϰ� ���� ����
        data.pool.Enqueue(sender);
        sender.isPooled = true;
        sender.transform.SetParent(Instance.transform);
        sender.gameObject.SetActive(false);
    }

    // ������Ʈ�� Ǯ���� ����
    public static Poolable Dequeue(string key)
    {
        if (!pools.ContainsKey(key))
            return null;

        PoolData data = pools[key];

        //  Ǯ�� ����� ������Ʈ�� ���ڶ�� �����ϱ�
        if (data.pool.Count == 0)
            return CreateInstance(key, data.prefab);

        // ť���� ������ ��ȯ
        Poolable obj = data.pool.Dequeue();
        obj.isPooled = false;
        return obj;
    }
    #endregion

    #region Private
    // �̱��� ������Ʈ�� ����
    private static void CreateSharedInstance()
    {
        GameObject obj = new GameObject("GameObject Pool Controller");
        DontDestroyOnLoad(obj);
        instance = obj.AddComponent<GameObjectPoolController>();
    }

    private static Poolable CreateInstance(string key, GameObject prefab)
    {
        GameObject instance = Instantiate(prefab) as GameObject;
        Poolable p = instance.AddComponent<Poolable>();
        p.key = key;
        return p;
    }
    #endregion
}