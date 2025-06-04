using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// 파티를 일종의 게임 오브젝트로 따로 정의함
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

//  경험치를 분배하는 기능을 가진 스크립트
public class ExperienceManager : MonoBehaviour
{
    //  레벨 차이에 따른 경험치 추가 가중치
    private const float minLevelBonus = 2.0f;
    private const float maxLevelBonus = 0.5f;
    public static void AwardExperience(int amount, Party party)
    {
        //  파티 내 모든 캐릭터의 Rank 컴포넌트를 수집
        List<Rank> ranks = new List<Rank>(party.Count);
        for (int i = 0; i < party.Count; ++i)
        {
            Rank r = party[i].GetComponent<Rank>();
            if (r != null)
                ranks.Add(r);
        }

        //   파티 내 캐릭터들의 최소 및 최대 레벨 확인
        int min = int.MaxValue;
        int max = int.MinValue;
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            min = Mathf.Min(ranks[i].LVL, min);
            max = Mathf.Max(ranks[i].LVL, max);
        }

        // 각 캐릭터의 레벨에 따라 경험치 배분 가중치 계산
        float[] weights = new float[party.Count];
        float summedWeights = 0;
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            // 레벨 차를 기반으로 보정 비율 계산
            float percent = (float)(ranks[i].LVL - min) / (float)(max - min);
            weights[i] = Mathf.Lerp(minLevelBonus, maxLevelBonus, percent);
            summedWeights += weights[i];
        }

        //  가중치 기반으로 각 캐릭터에게 경험치 분배
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            int subAmount = Mathf.FloorToInt((weights[i] / summedWeights) * amount);
            ranks[i].EXP += subAmount;
        }
    }
}
