using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ������ ���� Ŭ����
public class Job : MonoBehaviour
{
    // �ɷ�ġ
    public static readonly StatTypes[] statOrder = new StatTypes[]
    {
        StatTypes.MHP,
        StatTypes.MMP,
        StatTypes.ATK,
        StatTypes.DEF,
        StatTypes.MAT,
        StatTypes.MDF,
        StatTypes.SPD
    };
    // �⺻ �ɷ�ġ
    public int[] baseStats = new int[statOrder.Length];
    //  ����ġ
    public float[] growStats = new float[statOrder.Length];
    private Stats stats;

    private void OnDestroy()

    {   // ���� ���� �̺�Ʈ ��� ����
        this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL));
    }
    //  ���� ����
    public void Employ()
    {
        //  �ɷ�ġ ��������
        stats = gameObject.GetComponentInParent<Stats>();
        //  ���� �� �̺�Ʈ ���
        this.AddObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
       
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Activate(gameObject);
    }
    //  ���� ����� ���� ����
    public void UnEmploy()
    {
        //  �ڽ� ������Ʈ���� ������Ʈ ��������
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Deactivate();
        //  ������ �̺�Ʈ ����
        this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
        //  ���� ����
        stats = null;
    }

    // �⺻ �ɷ�ġ �ʱ�ȭ
    public void LoadDefaultStats()
    {
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            stats.SetValue(type, baseStats[i], false);
        }
        // ���� ü��/������ �ִ밪
        stats.SetValue(StatTypes.HP, stats[StatTypes.MHP], false);
        stats.SetValue(StatTypes.MP, stats[StatTypes.MMP], false);
    }

    // ���� ���� �� ȣ��
    protected virtual void OnLvlChangeNotification(object sender, object args)
    {
        int oldValue = (int)args;
        int newValue = stats[StatTypes.LVL];
        // �ݺ������� ���� ����
        for (int i = oldValue; i < newValue; ++i)
            LevelUp();
    }
    // ������ ó��
    private void LevelUp()
    {
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            //  �����κ�, �Ҽ��κ� ó��
            int whole = Mathf.FloorToInt(growStats[i]);
            float fraction = growStats[i] - whole;
            int value = stats[type];
            value += whole;

            if (Random.value > (1f - fraction))
                value++;
            stats.SetValue(type, value, false);
        }
        //  ������ �� ä��, ���� �ִ�ġ ����
        stats.SetValue(StatTypes.HP, stats[StatTypes.MHP], false);
        stats.SetValue(StatTypes.MP, stats[StatTypes.MMP], false);
    }
}
