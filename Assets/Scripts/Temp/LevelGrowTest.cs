using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  파티를 게임오브젝트로 관리
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

public class TestLevelGrowth : MonoBehaviour
{

    //  이벤트
    private void OnEnable()
    {
        this.AddObserver(OnLevelChange, Stats.DidChangeNotification(StatTypes.LVL));
        this.AddObserver(OnExperienceException, Stats.WillChangeNotification(StatTypes.EXP));
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnLevelChange, Stats.DidChangeNotification(StatTypes.LVL));
        this.RemoveObserver(OnExperienceException, Stats.WillChangeNotification(StatTypes.EXP));
    }

    //  검증, 출력
    private void Start()
    {
        VerifyLevelToExperienceCalculations();
        VerifySharedExperienceDistribution();
    }
    //  경험치-레벨 계산이 정확한지 검증
    private void VerifyLevelToExperienceCalculations()
    {
        for (int i = 1; i < 100; ++i)
        {
            int expLvl = Rank.ExperienceForLevel(i);
            int lvlExp = Rank.LevelForExperience(expLvl);

            if (lvlExp != i)
                Debug.Log(string.Format("Mismatch on level:{0} with exp:{1} returned:{2}", i, expLvl, lvlExp));
            else
                Debug.Log(string.Format("Level:{0} = Exp:{1}", lvlExp, expLvl));
        }
    }
    //  분배 결과 출력
    private void VerifySharedExperienceDistribution()
    {
        string[] names = new string[] { "Russell", "Brian", "Josh", "Ian", "Adam", "Andy" };

        Party heroes = new Party();
        for (int i = 0; i < names.Length; ++i)
        {
            GameObject actor = new GameObject(names[i]);
            actor.AddComponent<Stats>();
            Rank rank = actor.AddComponent<Rank>();
            rank.Init((int)UnityEngine.Random.Range(1, 5));
            heroes.Add(actor);
        }

        Debug.Log("===== Before Adding Experience ======");
        LogParty(heroes);

        Debug.Log("=====================================");
        ExperienceManager.AwardExperience(1000, heroes);

        Debug.Log("===== After Adding Experience ======");
        LogParty(heroes);
    }

    private void LogParty(Party p)
    {
        for (int i = 0; i < p.Count; ++i)
        {
            GameObject actor = p[i];
            Rank rank = actor.GetComponent<Rank>();
            Debug.Log(string.Format("Name:{0} Level:{1} Exp:{2}", actor.name, rank.LVL, rank.EXP));
        }
    }

    private void OnLevelChange(object sender, object args)
    {
        Stats stats = sender as Stats;
        Debug.Log(stats.name + " leveled up!");
    }

    private void OnExperienceException(object sender, object args)
    {
        GameObject actor = (sender as Stats).gameObject;
        ValueChangeException vce = args as ValueChangeException;
        int roll = UnityEngine.Random.Range(0, 5);
        switch (roll)
        {
            case 0:
                vce.FlipToggle();
                Debug.Log(string.Format("{0} would have received {1} experience, but we stopped it", actor.name, vce.delta));
                break;
            case 1:
                vce.AddModifier(new AddValueModifier(0, 1000));
                Debug.Log(string.Format("{0} would have received {1} experience, but we added 1000", actor.name, vce.delta));
                break;
            case 2:
                vce.AddModifier(new MultValueModifier(0, 3f));
                Debug.Log(string.Format("{0} would have received {1} experience, but we multiplied by 3", actor.name, vce.delta));
                break;
            default:
                Debug.Log(string.Format("{0} will receive {1} experience", actor.name, vce.delta));
                break;
        }
    }
}
