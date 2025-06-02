using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// ��Ƽ�� ������ ���� ������Ʈ�� ���� ������
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

//  ����ġ�� �й��ϴ� ����� ���� ��ũ��Ʈ
public class ExperienceManager : MonoBehaviour
{
    //  ���� ���̿� ���� ����ġ �߰� ����ġ
    private const float minLevelBonus = 2.0f;
    private const float maxLevelBonus = 0.5f;
    public static void AwardExperience(int amount, Party party)
    {
        //  ��Ƽ �� ��� ĳ������ Rank ������Ʈ�� ����
        List<Rank> ranks = new List<Rank>(party.Count);
        for (int i = 0; i < party.Count; ++i)
        {
            Rank r = party[i].GetComponent<Rank>();
            if (r != null)
                ranks.Add(r);
        }

        //   ��Ƽ �� ĳ���͵��� �ּ� �� �ִ� ���� Ȯ��
        int min = int.MaxValue;
        int max = int.MinValue;
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            min = Mathf.Min(ranks[i].LVL, min);
            max = Mathf.Max(ranks[i].LVL, max);
        }

        // �� ĳ������ ������ ���� ����ġ ��� ����ġ ���
        float[] weights = new float[party.Count];
        float summedWeights = 0;
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            // ���� ���� ������� ���� ���� ���
            float percent = (float)(ranks[i].LVL - min) / (float)(max - min);
            weights[i] = Mathf.Lerp(minLevelBonus, maxLevelBonus, percent);
            summedWeights += weights[i];
        }

        //  ����ġ ������� �� ĳ���Ϳ��� ����ġ �й�
        for (int i = ranks.Count - 1; i >= 0; --i)
        {
            int subAmount = Mathf.FloorToInt((weights[i] / summedWeights) * amount);
            ranks[i].EXP += subAmount;
        }
    }
}
