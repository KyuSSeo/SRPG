using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터의 직업 클래스
public class Job : MonoBehaviour
{
    // 능력치
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
    // 기본 능력치
    public int[] baseStats = new int[statOrder.Length];
    //  성장치
    public float[] growStats = new float[statOrder.Length];
    private Stats stats;

    private void OnDestroy()

    {   // 레벨 변경 이벤트 등록 해제
        this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL));
    }
    //  직업 적용
    public void Employ()
    {
        //  능력치 가져오기
        stats = gameObject.GetComponentInParent<Stats>();
        //  레벨 업 이벤트 등록
        this.AddObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
       
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Activate(gameObject);
    }
    //  현제 적용된 직업 해제
    public void UnEmploy()
    {
        //  자식 오브젝트에서 컴포넌트 가져오기
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Deactivate();
        //  레벨업 이벤트 해제
        this.RemoveObserver(OnLvlChangeNotification, Stats.DidChangeNotification(StatTypes.LVL), stats);
        //  참조 제거
        stats = null;
    }

    // 기본 능력치 초기화
    public void LoadDefaultStats()
    {
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            stats.SetValue(type, baseStats[i], false);
        }
        // 현재 체력/마나를 최대값
        stats.SetValue(StatTypes.HP, stats[StatTypes.MHP], false);
        stats.SetValue(StatTypes.MP, stats[StatTypes.MMP], false);
    }

    // 레벨 변경 시 호출
    protected virtual void OnLvlChangeNotification(object sender, object args)
    {
        int oldValue = (int)args;
        int newValue = stats[StatTypes.LVL];
        // 반복적으로 스탯 증가
        for (int i = oldValue; i < newValue; ++i)
            LevelUp();
    }
    // 레벨업 처리
    private void LevelUp()
    {
        for (int i = 0; i < statOrder.Length; ++i)
        {
            StatTypes type = statOrder[i];
            //  정수부분, 소수부분 처리
            int whole = Mathf.FloorToInt(growStats[i]);
            float fraction = growStats[i] - whole;
            int value = stats[type];
            value += whole;

            if (Random.value > (1f - fraction))
                value++;
            stats.SetValue(type, value, false);
        }
        //  레벨업 후 채력, 마나 최대치 설정
        stats.SetValue(StatTypes.HP, stats[StatTypes.MHP], false);
        stats.SetValue(StatTypes.MP, stats[StatTypes.MMP], false);
    }
}
