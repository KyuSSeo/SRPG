using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour
{
    //  ���� ���� ����
    #region Consts
    public const int minLevel = 1;
    public const int maxLevel = 99;
    public const int maxExperience = 999999;
    #endregion

    #region Fields / Properties
    // ���� ������ ����ġ�� ��ȯ
    public int LVL
    {
        get { return stats[StatTypes.LVL]; }
    }

    public int EXP
    {
        get { return stats[StatTypes.EXP]; }
        set { stats[StatTypes.EXP] = value; }
    }

    public float LevelPercent
    {
        get { return (float)(LVL - minLevel) / (float)(maxLevel - minLevel); }
    }

    private Stats stats;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        stats = GetComponent<Stats>();
    }

    //  �̺�Ʈ ���, ����
    private void OnEnable()
    {
        this.AddObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), stats);
        this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), stats);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), stats);
        this.RemoveObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), stats);
    }
    #endregion

    #region Event Handlers

    //  ����ġ ���� �˸� 
    private void OnExpWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        //  ������ �ִ�ġ������
        vce.AddModifier(new ClampValueModifier(int.MaxValue, EXP, maxExperience));
    }
    //  �� ����
    private void OnExpDidChange(object sender, object args)
    {
        stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
    }
    #endregion

    #region Public
    //  ����ġ ���� ����
    public static int ExperienceForLevel(int level)
    {
        //  Ease In Quad � ����
        float levelPercent = Mathf.Clamp01((float)(level - minLevel) / (float)(maxLevel - minLevel));
        return (int)EasingEquations.EaseInQuad(0, maxExperience, levelPercent);
    }

    //  ����ġ�� ���� �������
    public static int LevelForExperience(int exp)
    {
        int lvl = maxLevel;
        for (; lvl >= minLevel; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }

    //  �������� �ʱ�ȭ
    public void Init(int level)
    {
        stats.SetValue(StatTypes.LVL, level, false);
        stats.SetValue(StatTypes.EXP, ExperienceForLevel(level), false);
    }
    #endregion
}