using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //  ü�� ���� ��������
    private Stats stats;
    public int HP
    {
        get { return stats[StatTypes.HP]; }
        set { stats[StatTypes.HP] = value; }
    }
    public int MHP
    {
        get { return stats[StatTypes.MHP]; }
        set { stats[StatTypes.MHP] = value; }
    }
    private void Awake()
    {
        stats = GetComponent<Stats>();
    }
    //  �˸� ���, ����
    private void OnEnable()
    {
        this.AddObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }
    private void OnDisable()
    {
        this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    private void OnHPWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, 0, stats[StatTypes.MHP]));
    }
    //  Hp��ȭ
    private void OnMHPDidChange(object sender, object args)
    {
        int oldMHP = (int)args;
        if (MHP > oldMHP)
            HP += MHP - oldMHP;
        else
            HP = Mathf.Clamp(HP, 0, MHP);
    }
}
